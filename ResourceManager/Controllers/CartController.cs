using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ResourceManager.Data;
using ResourceManager.Models.Entities;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ResourceManager.Controllers
{
    public class CartController : Controller
    {
        private readonly ResourceContext _context;
        private readonly UserIdentityContext _userManager;


        public CartController(ResourceContext context, UserIdentityContext userManager)

        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Cart()
        {
            return View();
        }

        #region CRUD
        [HttpPost]
        public async Task<IActionResult> addCart(Guid projectId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return BadRequest("Invalid user ID");
            }

            bool projectExists = await _context.SendProjects
        .AnyAsync(sp => sp.projectId == projectId && sp.UserId == userId);

            if (projectExists)
            {
                return BadRequest("This project is already in your cart.");
            }

            var timeStamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");

            var cart = new SendProject

            {
                Id = Guid.NewGuid(),
                UserId = userId,
                projectId = projectId,
                timeSend = timeStamp
                
            };
            _context.SendProjects.Add(cart);
            await _context.SaveChangesAsync();


            var dmUser = await _userManager.Users
            .Where(u => _userManager.UserRoles
                .Any(ur => ur.UserId == u.Id && _userManager.Roles.Any(r => r.Id == ur.RoleId && r.Name == "DM")))
            .FirstOrDefaultAsync();

            if (dmUser != null)
            {
                var notice = new Notice
                {
                    Id = Guid.NewGuid(),
                    UserIdReceivedDM = Guid.Parse(dmUser.Id),  // Use the DM user's ID
                    UserIdSent = userId,  // Use the DM user's ID
                    projectId = projectId,
                    Content = "A project has been added to the cart.",

                    TimeCreate = timeStamp
                };

                _context.Notices.Add(notice);
                await _context.SaveChangesAsync();
            }
            return Ok(cart);


        }
        [HttpGet]
        public async Task<IActionResult> getAllCart()
        {
            var sendProjects = await _context.SendProjects.ToListAsync();
            var projects = await _context.Projects.ToListAsync();
            var users = await _userManager.Users.ToListAsync(); // Use _userManager to get users
            var projectAttachFiles = await _context.ProjectAttachFiles.ToListAsync();
            var attachFiles = await _context.AttachFiles.ToListAsync();

            var result = from sp in sendProjects
                         join p in projects on sp.projectId equals p.ProjectId
                         join u in users on sp.UserId.ToString() equals u.Id
                         join paf in projectAttachFiles on p.ProjectId equals paf.ProjectId into pafGroup
                         from paf in pafGroup.DefaultIfEmpty() // Left join to handle null values
                         join af in attachFiles on (paf != null ? paf.attachFileId : Guid.Empty) equals af.Id into afGroup
                         from af in afGroup.DefaultIfEmpty() // Left join to handle null values
                         group af by new
                         {
                             sp.Id,
                             p.projectName,
                             p.ProjectId,
                             u.FullName,
                             p.createDay,
                             p.dueDay,
                             sp.timeSend,
                             sp.isAccept
                         } into grouped
                         select new
                         {
                             grouped.Key.ProjectId,
                             grouped.Key.projectName,
                             UserName = grouped.Key.FullName,
                             grouped.Key.createDay,
                             grouped.Key.dueDay,
                             grouped.Key.timeSend,
                             grouped.Key.isAccept,
                             grouped.Key.Id,
                             
                             Attachments = grouped.Where(a => a != null).Select(a => new
                             {
                                 a.FileName,
                                 a.FilePath
                             }).ToList()
                         };

            return Ok(result.ToList());

        }
        [HttpDelete]

        [Route("/Cart/DeleteCart/{id}")]
        

        public async Task<IActionResult> DeleteCart(Guid id)
        {
            try
            {
                var cart = await _context.SendProjects.FirstOrDefaultAsync(x => x.Id == id);
                if (cart == null) return NotFound();
                _context.SendProjects.Remove(cart);
                await _context.SaveChangesAsync();
                return StatusCode(202);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut]
        [Route("/Cart/ToggleAccept/{id}")]
        public async Task<IActionResult> ToggleAccept(Guid id,  bool isAccept)
        {
            var sendProject = await _context.SendProjects.FindAsync(id);
            if (sendProject == null)
            {
                return NotFound();
            }

            // Update the isAccept property
            sendProject.isAccept = isAccept;

            // Save changes to the database
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }
        #endregion
    }
}

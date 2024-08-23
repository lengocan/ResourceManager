using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceManager.Data;
using ResourceManager.Models.Entities;
using System.Security.Claims;

namespace ResourceManager.Controllers
{
    public class NoticeController : Controller
    {
        private readonly ResourceContext _context;
        private readonly UserIdentityContext _userManager;
        public NoticeController(ResourceContext context, UserIdentityContext userManager)

        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetNoticesDM()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userIdReceivedDM))
            {
                return BadRequest("Invalid user ID");
            }

     
            var notices = await _context.Notices
                .Where(n => n.UserIdReceivedDM == userIdReceivedDM)
                .ToListAsync();

            
            var userIds = notices.Select(n => n.UserIdSent).Distinct().ToList();
            var userIdStrings = userIds.Select(id => id.ToString()).ToList();
            var users = await _userManager.Users
                .Where(u => userIdStrings.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.FullName);

            
            var result = notices.Select(n => new
            {
                n.Content,
                n.TimeCreate,
                UserSentName = users.ContainsKey(n.UserIdSent.ToString()) ? users[n.UserIdSent.ToString()] : "Unknown"
            });

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserNotices()
        {
            // Get the user ID of the currently logged-in user
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userIdReceived))
            {
                return BadRequest("Invalid user ID");
            }

            // Retrieve all notices for the logged-in user
            var notices = await _context.NoticeCompleteFromUsers
                .Where(n => n.UserIdReceived == userIdReceived)
                .ToListAsync();

            // Format the notices to return only relevant information
            var result = notices.Select(n => new
            {
                n.Content,
                n.TimeCreate
            });

            return Ok(result);
        }

        [HttpPut]
        [Route("/Notice/ToggleAccept/{id}")]
        public async Task<IActionResult> ToggleAccept(Guid id, bool isAccept)
        {
            var sendProject = await _context.SendProjects.FindAsync(id);
            if (sendProject == null)
            {
                return NotFound();
            }

            sendProject.isAccept = isAccept;
            await _context.SaveChangesAsync();





            var project = await _context.Projects
              .Where(p => p.ProjectId == sendProject.projectId)
              .Select(p => new { p.projectName })
              .FirstOrDefaultAsync();

            if (project == null)
            {
                return NotFound("Project not found.");
            }
            // Retrieve the ProjectAssign entry
            var projectAssigns = await _context.ProjectAssigns
           .Where(pa => pa.ProjectId == sendProject.projectId)
           .ToListAsync();

            if (projectAssigns != null)
            {
                foreach (var projectAssign in projectAssigns)
                {
                    var notice = new NoticeCompleteFromUser
                    {
                        Id = Guid.NewGuid(),
                        UserIdReceived = projectAssign.UserEmployeeId, // For each UserEmployeeId in ProjectAssign

                        projectId = sendProject.projectId,
                        Content = isAccept
                    ? $"{project.projectName} has been accepted."
                    : $"{project.projectName} has not been accepted.",
                        TimeCreate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
                    };

                    _context.NoticeCompleteFromUsers.Add(notice);
                }
                await _context.SaveChangesAsync();
            }

            return Ok(new { success = true });
        }

    }
}

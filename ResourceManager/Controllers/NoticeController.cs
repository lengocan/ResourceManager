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
        public async Task<IActionResult> GetNotices()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userIdReceivedDM))
            {
                return BadRequest("Invalid user ID");
            }

            // Fetch notices related to the user
            var notices = await _context.Notices
                .Where(n => n.UserIdReceivedDM == userIdReceivedDM)
                .ToListAsync();

            // Fetch user IDs and retrieve user details
            var userIds = notices.Select(n => n.UserIdSent).Distinct().ToList();
            var userIdStrings = userIds.Select(id => id.ToString()).ToList();
            var users = await _userManager.Users
                .Where(u => userIdStrings.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.FullName);

            // Prepare the notice data with user names
            var result = notices.Select(n => new
            {
                n.Content,
                n.TimeCreate,
                UserSentName = users.ContainsKey(n.UserIdSent.ToString()) ? users[n.UserIdSent.ToString()] : "Unknown"
            });

            return Ok(result);
        }
    }
}

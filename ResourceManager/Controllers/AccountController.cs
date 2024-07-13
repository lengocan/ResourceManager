using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using ResourceManager.Areas.Identity.Data;
using ResourceManager.Data;
using ResourceManager.Models.Entities;
using System.Drawing;
using System.Security.Claims;

namespace ResourceManager.Controllers
{
    
    
    public class AccountController : Controller
    {
        
        private readonly UserIdentityContext _identityContext;

        

        public AccountController(UserIdentityContext identityContext)
        {
           
            _identityContext = identityContext;
            
        }


        //Lay ra duoc thong tin tai khoan da dang ki
        [HttpGet]
        public async Task<IActionResult> GetCurrentAccount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }
            var user = await _identityContext.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
             return Ok(user);

            
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var item = await _identityContext.Users.OfType<UserEmployee>().ToListAsync();  
            return Ok(item);
        }

        [HttpPatch]
        public async Task<IActionResult> addInfoUser(string fullName, string dob, string address, string dayJoin, string team, bool isActive, string phoneNumber)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (UserId == null)
            {
                return Unauthorized();

            }
            var item = await _identityContext.Users.FindAsync(UserId);  
            if (item == null)
            {
                return NotFound();
            }
            if(item is UserEmployee user) {
                user.address = address;
                user.dayJoin = dayJoin;
                user.team = team;
                user.FullName = fullName;
                user.PhoneNumber = phoneNumber;
                user.dob = dob;
                user.IsActive = isActive;
                _identityContext.Users.Update(user);
                await _identityContext.SaveChangesAsync();


            }

            return Ok(item);    
                     
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

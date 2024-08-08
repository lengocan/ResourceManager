using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        private readonly UserManager<UserEmployee> _userManager;
        private readonly IUserStore<UserEmployee> _userStore;


        public AccountController(UserIdentityContext identityContext,
            UserManager<UserEmployee> userManager, IUserStore<UserEmployee> userStore)
        {

            _identityContext = identityContext;

            _userStore = userStore;
            _userManager = userManager;
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
                
                _identityContext.Users.Update(user);
                await _identityContext.SaveChangesAsync();


            }

            return Ok(item);    
                     
        }

        [HttpPost]
        public async Task<IActionResult> addUser(string email, string password,
        string fullName, string dob, string address, string dayJoin,
        string team, bool isActive, string phoneNumber)
        {
            var user = CreateUser();
            user.UserName = email;
            user.EmailConfirmed = true;
            user.Email = email;
            user.NormalizedEmail = email.ToUpper();
            user.NormalizedUserName = email.ToUpper();
            user.FullName = fullName;
            user.dob = dob;
            user.address = address;
            user.dayJoin = dayJoin;
            user.team = team;
            user.IsActive = true;
            user.PhoneNumber = phoneNumber;

            await _userStore.SetUserNameAsync(user, email, CancellationToken
                .None);

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return Ok(user);
            }
            return BadRequest(result.Errors);
        }
        public IActionResult Index()
        {
            return View();
        }
        private UserEmployee CreateUser()
        {
            try
            {
                return Activator.CreateInstance<UserEmployee>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(UserEmployee)}'. " +
                    $"Ensure that '{nameof(UserEmployee)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
    }
}

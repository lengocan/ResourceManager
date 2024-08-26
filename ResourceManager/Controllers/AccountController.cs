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
        private readonly SignInManager<UserEmployee> _signInManager;


        public AccountController(UserIdentityContext identityContext,
            UserManager<UserEmployee> userManager, IUserStore<UserEmployee> userStore, SignInManager<UserEmployee> signInManager)
        {

            _identityContext = identityContext;

            _userStore = userStore;
            _userManager = userManager;
            _signInManager = signInManager;
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
        //Show ra tat 
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            // Optionally, you can redirect to a specific URL or the home page
            return RedirectToLocal(returnUrl);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetUserRole()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return BadRequest("Invalid user ID");
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound("User not found");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            return Ok(new { role });
        }
    }
}

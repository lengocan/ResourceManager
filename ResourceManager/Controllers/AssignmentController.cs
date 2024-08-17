using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceManager.Areas.Identity.Data;
using ResourceManager.Data;
using System.Security.Claims;

namespace ResourceManager.Controllers
{
    
    public class AssignmentController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserEmployee> _userManager;
        private readonly UserIdentityContext _identityContext;


        public AssignmentController(RoleManager<IdentityRole> roleManager, UserManager<UserEmployee> userManager, UserIdentityContext identityContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _identityContext = identityContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Roles function here
        [Authorize(Roles = "DM")]
        public IActionResult Role()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return Ok(roles);
        }

        [Route("roles/{id}")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var items = await _roleManager.FindByIdAsync(id.ToString());

            return Ok(items);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole role)
        {
            
            role.NormalizedName = role.Name.ToUpper();
            var check = await _roleManager.FindByNameAsync(role.NormalizedName);
            if (check != null)
            {
                return BadRequest("Role already exists");
            }
            var result = await _roleManager.CreateAsync(role);
            return Ok(result);

        }
        //chua xai
        [Route("roles/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateRole(Guid id, IdentityRole role)
        {
            role.Id = id.ToString();
            var result = await _roleManager.UpdateAsync(role);
            return Ok(result);
        }


        [Route("/Assignment/DeleteRole/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteRole (Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            var result = await _roleManager.DeleteAsync(role);

            return Ok(result);
        }

        #region Perrmissions
        //Permission function here
        [Authorize(Roles = "DM")]
        public IActionResult Permission()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            var query = from ur in _identityContext.UserRoles
                        
                        join u in _identityContext.Users.OfType<UserEmployee>() on ur.UserId equals u.Id
                        join r in _identityContext.Roles on ur.RoleId equals r.Id
                        
                        select new
                        {
                            ur.UserId,
                            u.UserName,
                            u.Email,
                            
                            ur.RoleId,
                            RoleName = r.Name,


                            //custom
                            u.dob,
                            u.FullName,
                            u.address,
                            u.dayJoin,
                            u.team,
                            u.IsActive,
                            //custom
                        };
            var permissions = await query.ToListAsync();
            return Ok(permissions);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserRole()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || roles.Count == 0)
            {
                return NotFound("No roles found for the user.");
            }

            return Ok(new { Roles = roles });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission(string userId, string roleId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roleName = _roleManager.FindByIdAsync(roleId).Result?.Name;
            if (roleName == "")
            {
                return BadRequest("Role not found");
            }
            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }
        #endregion
    }
}

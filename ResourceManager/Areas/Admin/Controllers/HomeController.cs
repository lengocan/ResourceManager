using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ResourceManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    public class HomeController : Controller
    {
        [Authorize(Roles = "DM")]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "DM")]
        [Route("Employee")]
        public IActionResult Employee()
        {
            return View();
        }
       
       
    }
}

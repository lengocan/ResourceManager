using Microsoft.AspNetCore.Mvc;

namespace ResourceManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("Project")]
        public IActionResult Project()
        {
            return View();
        }
        [Route("Employee")]
        public IActionResult Employee()
        {
            return View();
        }
        [Route("Team")]
        public IActionResult Team()
        {
            return View();
        }
        [Route("Event")]
        public IActionResult Event()
        {
            return View();
        }
        [Route("Meeting")]
        public IActionResult Meeting()
        {
            return View();
        }
        [Route("Cart")]
        public IActionResult Cart()
        {
            return View();
        }
    }
}

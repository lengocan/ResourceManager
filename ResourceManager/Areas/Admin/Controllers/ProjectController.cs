using Microsoft.AspNetCore.Mvc;

namespace ResourceManager.Areas.Admin.Controllers
{
    public class ProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

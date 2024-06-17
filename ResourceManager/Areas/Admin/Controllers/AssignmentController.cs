using Microsoft.AspNetCore.Mvc;

namespace ResourceManager.Areas.Admin.Controllers
{
    public class AssignmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

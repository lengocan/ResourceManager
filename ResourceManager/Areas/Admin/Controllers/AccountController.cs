using Microsoft.AspNetCore.Mvc;

namespace ResourceManager.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

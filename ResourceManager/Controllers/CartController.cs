using Microsoft.AspNetCore.Mvc;

namespace ResourceManager.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
    }
}

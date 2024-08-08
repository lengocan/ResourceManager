using Microsoft.AspNetCore.Mvc;

namespace ResourceManager.Controllers
{
    public class BannerController : Controller
    {
        public IActionResult Banner()
        {
            return View();
        }

    }
}

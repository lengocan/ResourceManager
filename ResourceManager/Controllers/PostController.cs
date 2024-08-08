using Microsoft.AspNetCore.Mvc;

namespace ResourceManager.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Post()
        {
            return View();
        }
    }
}

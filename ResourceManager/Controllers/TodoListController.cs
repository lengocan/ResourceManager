using Microsoft.AspNetCore.Mvc;

namespace ResourceManager.Controllers
{
    public class TodoListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult TodoList()
        {
            return View();
        }
    }
}

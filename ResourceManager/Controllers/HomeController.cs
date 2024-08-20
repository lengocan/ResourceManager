using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourceManager.Models;
using System.Diagnostics;

namespace ResourceManager.Controllers
{
    
    

    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


       
       
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "DM")]
        public IActionResult Admin()
        {
            return View();
        }

     
       
        public IActionResult Employee()
        {
            return View();
        }

        
        
        /*[Route("Team")]
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
            return View();*/


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

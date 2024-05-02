using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TesteandoSRWebServer.Models;

namespace TesteandoSRWebServer.Controllers
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
            Console.WriteLine("Se inicio el index");
            return View();
        }

        public IActionResult Privacy()
        {
            Console.WriteLine("Se inicio el Privacy");
            return View();
        }

        public IActionResult Firebase() 
        {
            TestingFirebase testingFirebase = new();
            testingFirebase.GetAllUsers();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

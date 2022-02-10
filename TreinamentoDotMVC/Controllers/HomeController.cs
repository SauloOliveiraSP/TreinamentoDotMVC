using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TreinamentoDotMVC.Models;

namespace TreinamentoDotMVC.Controllers
{
    public class HomeController :Controller
    {
        private readonly ILogger<HomeController> _logger;

      static List<UserViewModel> usuarios = new List<UserViewModel>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Consultar()
        {
            return View(usuarios);
        }
        [HttpPost]
        public IActionResult Index(UserViewModel user)
        {
            usuarios.Add(user);
            return View("Consultar", usuarios);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
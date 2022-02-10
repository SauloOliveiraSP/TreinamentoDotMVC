using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TreinamentoDotMVC.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TreinamentoDotMVC.Controllers
{
    public class CadastroController : Controller
    {
        List<UserViewModel> usuarios = new List<UserViewModel>();
        // GET: /<controller>/


        public IActionResult Consultar()
        {
            return View(usuarios);
        }


        [HttpPost]
        public IActionResult Consultar(UserViewModel user)
        {
            usuarios.Add(user);
            return View("Consultar", usuarios);
        }
    }
}





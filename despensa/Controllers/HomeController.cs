using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using despensa.Models;

namespace despensa.Controllers
{

    public class HomeController : Controller
    {
        private readonly despensa1Context _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, despensa1Context context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*
        [HttpGet]
        public JsonResult Get()
        {
            Console.WriteLine("hola mundo 6456524");
            despensa1Context _context = new despensa1Context();
            var entradas = (from m in _context.Producto
                            where m.CodEstado == 1
                            orderby m.Nombre descending
                            select m).ToList();
            return Json(entradas);
        }*/
   

        [HttpGet]
        public JsonResult VerificarCorreo(string CorreoElectronico)
        {
            if (_context.Usuario.Any(u => u.CorreoElectronico == CorreoElectronico))
            {
                return Json(false);
            }
            return Json(true);
        }


    }
}

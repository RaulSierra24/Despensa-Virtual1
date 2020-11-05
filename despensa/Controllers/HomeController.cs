using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using despensa.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using X.PagedList;

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

        public async Task<IActionResult> Index()
        {
            var entradas = (from m in _context.Categoria.Include(c => c.EstadoNavigation)
                            where m.Estado == 3
                            orderby m.Nombre ascending
                            select m).ToList();
            ViewBag.layoutcategorias = entradas;
            var aux = await _context.Producto
                .Where(p => p.CodEstado == 3)
                .Include(p => p.CodCategoriaNavigation)
                .OrderByDescending(x => x.Cantidad).Take(6).ToListAsync();
            ViewBag.productoshome = aux;
            return View();
        }

        public async Task<IActionResult> Comentarios(int? page)
        {
            int id;
            if (this.User.Identity.IsAuthenticated)
            {
                ClaimsPrincipal currentUser = this.User;
                var identity = (ClaimsIdentity)currentUser.Identity;
                id = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                var usuario = await _context.Usuario
                .Include(u => u.CodEstadoNavigation)
                .Include(u => u.CodGeneoNavigation)
                .Include(u => u.CodRolNavigation)
                .FirstOrDefaultAsync(m => m.CodUsuario == id);
                ViewBag.micomentario = usuario;
            }
            var pageNumber = page ?? 1;
            var entradas = (from m in _context.Comentario.Include(u => u.CodClienteNavigation)
                            orderby m.Fecha descending
                            select m).ToList();
            var entrada = entradas.ToPagedList(pageNumber, 10);

            return View(entrada);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Createcomentario([Bind("Comentario1,hola")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                ClaimsPrincipal currentUser = this.User;
                var identity = (ClaimsIdentity)currentUser.Identity;
                comentario.Fecha = DateTime.Now; 
                comentario.CodCliente = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

                _context.Add(comentario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Comentarios", "Home");
            }

            return View(comentario);
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

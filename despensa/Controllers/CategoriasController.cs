using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using despensa.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using X.PagedList;

namespace despensa.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly despensa1Context _context;
        private readonly IWebHostEnvironment HostEnvironment;
  

        public CategoriasController(despensa1Context context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this.HostEnvironment = hostEnvironment;
        }

        // GET: Categorias
        [Authorize(Roles = "3,2,1")]
        public ActionResult Index(int? page, string a, string b, string c)
        {
            if (a =="PedidoSatisfactorio")
            {
                ViewBag.pedidossac = "verdadero";
                ViewBag.totalb = b;
                ViewBag.fechac = c;
            }
            var pageNumber = page ?? 1;
            var entradas = (from m in _context.Categoria.Include(c => c.EstadoNavigation)
                            where m.Estado == 3 
                            orderby m.Nombre ascending
                            select m).ToList();
            var unaPagina = entradas.ToPagedList(pageNumber, 9);
            return View(unaPagina);
        }
        [Authorize(Roles = "3")]
        public ActionResult IndexDesactivo()
        {
            var entradas = (from m in _context.Categoria.Include(c => c.EstadoNavigation)
                            where m.Estado == 4
                            orderby m.Nombre ascending
                            select m).ToList();
            return View(entradas);
        }

        // GET: Categorias/Details/5

        [Authorize(Roles = "3")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .Include(c => c.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.CodCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categorias/Create
        [Authorize(Roles = "3")]
        public IActionResult Create()
        {
            ViewData["Estado"] = new SelectList(_context.EstadoActividad, "CodEstado", "Estado");
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "3")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodCategoria,Nombre,Estado,ImageFie")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (categoria.ImageFie != null)
                {
                    string carpeta = HostEnvironment.WebRootPath;
                    string nombrearchivo = Path.GetFileNameWithoutExtension(categoria.ImageFie.FileName);
                    string extencion = Path.GetExtension(categoria.ImageFie.FileName);
                    categoria.Imagen = nombrearchivo = "cate" + DateTime.Now.ToString("yymmssfff") + extencion;
                    string path = Path.Combine(carpeta + "/image/", nombrearchivo);
                    using (var hola = new FileStream(path, FileMode.Create))
                    {
                        await categoria.ImageFie.CopyToAsync(hola);
                    }
                }
                else
                {
                    categoria.Imagen = "pordefecto123598562389562.png";
                }
                _context.Add(categoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Estado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", categoria.Estado);
            return View(categoria);
        }

        // GET: Categorias/Edit/5
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            ViewData["Estado"] = new SelectList(_context.EstadoActividad, "CodEstado", "Estado", categoria.Estado);
            return View(categoria);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Edit(int id, [Bind("CodCategoria,Nombre,Estado,Imagen,ImageFie")] Categoria categoria)
        {
            Console.WriteLine("entre");
            if (id != categoria.CodCategoria)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (categoria.ImageFie != null)
                    {
                        string carpeta = HostEnvironment.WebRootPath;
                        string nombrearchivo = Path.GetFileNameWithoutExtension(categoria.ImageFie.FileName);
                        string extencion = Path.GetExtension(categoria.ImageFie.FileName);
                        categoria.Imagen = nombrearchivo = "cate" + DateTime.Now.ToString("yymmssfff") + extencion;
                        string path = Path.Combine(carpeta + "/image/", nombrearchivo);
                        using (var hola = new FileStream(path, FileMode.Create))
                        {
                            await categoria.ImageFie.CopyToAsync(hola);
                        }
                    }



                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.CodCategoria))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Estado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", categoria.Estado);
            return View(categoria);
        }

        // GET: Categorias/Delete/5
        [Authorize(Roles = "4")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .Include(c => c.EstadoNavigation)
                .FirstOrDefaultAsync(m => m.CodCategoria == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categorias/Delete/5
        [Authorize(Roles = "4")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.CodCategoria == id);
        }
    }
}

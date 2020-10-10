using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using despensa.Models;
using X.PagedList;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace despensa.Controllers
{
    public class ProductosController : Controller
    {
        private readonly despensaContext _context;


        private readonly IWebHostEnvironment HostEnvironment;
        int categoriass;
        public ProductosController(despensaContext context, IWebHostEnvironment hostEnvironment)
        {
            this.HostEnvironment = hostEnvironment;
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index(int? page, int idcat)
        {
            // if (idcat==0)
            //{
            //   return RedirectToAction("","Categorias");
            //}
            categoriass = idcat;
            var pageNumber = page ?? 1;

            var entradas = (from m in _context.Producto
                            where m.CodEstado == 1
                            orderby m.Nombre descending
                            select m).ToList();
            Console.WriteLine("holiprr" + categoriass);


            if (idcat > 0)
            {
                Console.WriteLine("laskjdfljasdkl" + idcat);
                entradas = entradas.Where(a => a.CodCategoria == idcat).ToList();

            }

            var unaPagina = entradas.ToPagedList(pageNumber, 16);
            ViewBag.pagina = unaPagina;
            ViewData["codigo_cat"] = idcat;
            var despensaContext = _context.Producto.Include(p => p.CodEstadoNavigation).Include(p => p.CodMarcaNavigation).Include(p => p.CodProveedorNavigation);
            return View(await despensaContext.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.CodCategoriaNavigation)
                .Include(p => p.CodEstadoNavigation)
                .Include(p => p.CodMarcaNavigation)
                .Include(p => p.CodProveedorNavigation)
                .FirstOrDefaultAsync(m => m.CodProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["CodCategoria"] = new SelectList(_context.Categoria, "CodCategoria", "CodCategoria");
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado");
            ViewData["CodMarca"] = new SelectList(_context.Marca, "CodMarca", "CodMarca");
            ViewData["CodProveedor"] = new SelectList(_context.Proveedor, "CodProveedor", "CodProveedor");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodProducto,Nombre,FecCaducidad,PrecioCosto,PrecioVenta,ImageFie,Peso,CodEstado,Cantidad,CodProveedor,CodMarca,CodCategoria")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                if (producto.ImageFie != null)
                {
                    string carpeta = HostEnvironment.WebRootPath;
                    string nombrearchivo = Path.GetFileNameWithoutExtension(producto.ImageFie.FileName);
                    string extencion = Path.GetExtension(producto.ImageFie.FileName);
                    producto.Imagen = nombrearchivo = nombrearchivo + DateTime.Now.ToString("yymmssfff") + extencion;
                    string path = Path.Combine(carpeta + "/image/", nombrearchivo);
                    using (var hola = new FileStream(path, FileMode.Create))
                    {
                        await producto.ImageFie.CopyToAsync(hola);
                    }
                }
                else
                {
                    producto.Imagen = "pordefecto123598562389562.png";
                }
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodCategoria"] = new SelectList(_context.Categoria, "CodCategoria", "CodCategoria", producto.CodCategoria);
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", producto.CodEstado);
            ViewData["CodMarca"] = new SelectList(_context.Marca, "CodMarca", "CodMarca", producto.CodMarca);
            ViewData["CodProveedor"] = new SelectList(_context.Proveedor, "CodProveedor", "CodProveedor", producto.CodProveedor);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CodCategoria"] = new SelectList(_context.Categoria, "CodCategoria", "CodCategoria", producto.CodCategoria);
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", producto.CodEstado);
            ViewData["CodMarca"] = new SelectList(_context.Marca, "CodMarca", "CodMarca", producto.CodMarca);
            ViewData["CodProveedor"] = new SelectList(_context.Proveedor, "CodProveedor", "CodProveedor", producto.CodProveedor);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodProducto,Nombre,FecCaducidad,PrecioCosto,PrecioVenta,Imagen,Peso,CodEstado,Cantidad,CodProveedor,CodMarca,CodCategoria")] Producto producto)
        {
            if (id != producto.CodProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.CodProducto))
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
            ViewData["CodCategoria"] = new SelectList(_context.Categoria, "CodCategoria", "CodCategoria", producto.CodCategoria);
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", producto.CodEstado);
            ViewData["CodMarca"] = new SelectList(_context.Marca, "CodMarca", "CodMarca", producto.CodMarca);
            ViewData["CodProveedor"] = new SelectList(_context.Proveedor, "CodProveedor", "CodProveedor", producto.CodProveedor);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.CodCategoriaNavigation)
                .Include(p => p.CodEstadoNavigation)
                .Include(p => p.CodMarcaNavigation)
                .Include(p => p.CodProveedorNavigation)
                .FirstOrDefaultAsync(m => m.CodProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.CodProducto == id);
        }
    }
}

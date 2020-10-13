using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using despensa.Models;

namespace despensa.Controllers
{
    public class Productoes1Controller : Controller
    {
        private readonly despensa1Context _context;

        public Productoes1Controller(despensa1Context context)
        {
            _context = context;
        }

        // GET: Productoes1
        public async Task<IActionResult> Index()
        {
            var despensa1Context = _context.Producto.Include(p => p.CodCategoriaNavigation).Include(p => p.CodEstadoNavigation).Include(p => p.CodMarcaNavigation).Include(p => p.CodProveedorNavigation);
            return View(await despensa1Context.ToListAsync());
        }

        // GET: Productoes1/Details/5
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

        // GET: Productoes1/Create
        public IActionResult Create()
        {
            ViewData["CodCategoria"] = new SelectList(_context.Categoria, "CodCategoria", "CodCategoria");
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado");
            ViewData["CodMarca"] = new SelectList(_context.Marca, "CodMarca", "CodMarca");
            ViewData["CodProveedor"] = new SelectList(_context.Proveedor, "CodProveedor", "CodProveedor");
            return View();
        }

        // POST: Productoes1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodProducto,Nombre,FecCaducidad,PrecioCosto,PrecioVenta,Imagen,Peso,CodEstado,Cantidad,CodProveedor,CodMarca,CodCategoria")] Producto producto)
        {
            if (ModelState.IsValid)
            {
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

        // GET: Productoes1/Edit/5
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

        // POST: Productoes1/Edit/5
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

        // GET: Productoes1/Delete/5
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

        // POST: Productoes1/Delete/5
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

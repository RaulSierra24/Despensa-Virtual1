using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using despensa.Models;
using Microsoft.AspNetCore.Authorization;

namespace despensa.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly despensa1Context _context;

        public ProveedoresController(despensa1Context context)
        {
            _context = context;
        }

        [Authorize(Roles = "2,3")]// GET: Proveedores
        public async Task<IActionResult> Index()
        {
            var despensa1Context = _context.Proveedor.Include(p => p.CodEstadoNavigation);
            return View(await despensa1Context.ToListAsync());
        }

        [Authorize(Roles = "2,3")]// GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor
                .Include(p => p.CodEstadoNavigation)
                .FirstOrDefaultAsync(m => m.CodProveedor == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        [Authorize(Roles = "2,3")]// GET: Proveedores/Create
        public IActionResult Create()
        {
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado");
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        [Authorize(Roles = "2,3")]// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodProveedor,Proveedor1,CodEstado")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", proveedor.CodEstado);
            return View(proveedor);
        }

        [Authorize(Roles = "2,3")]// GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", proveedor.CodEstado);
            return View(proveedor);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "2,3")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodProveedor,Proveedor1,CodEstado")] Proveedor proveedor)
        {
            if (id != proveedor.CodProveedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorExists(proveedor.CodProveedor))
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
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", proveedor.CodEstado);
            return View(proveedor);
        }

        [Authorize(Roles = "2,3")]// GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedor = await _context.Proveedor
                .Include(p => p.CodEstadoNavigation)
                .FirstOrDefaultAsync(m => m.CodProveedor == id);
            if (proveedor == null)
            {
                return NotFound();
            }

            return View(proveedor);
        }

        [Authorize(Roles = "3")]// POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedor = await _context.Proveedor.FindAsync(id);
            _context.Proveedor.Remove(proveedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "3")]
        private bool ProveedorExists(int id)
        {
            return _context.Proveedor.Any(e => e.CodProveedor == id);
        }
    }
}

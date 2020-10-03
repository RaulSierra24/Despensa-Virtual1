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
    public class EstadoActividadsController : Controller
    {
        private readonly despensaContext _context;

        public EstadoActividadsController(despensaContext context)
        {
            _context = context;
        }

        // GET: EstadoActividads
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadoActividad.ToListAsync());
        }

        // GET: EstadoActividads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoActividad = await _context.EstadoActividad
                .FirstOrDefaultAsync(m => m.CodEstado == id);
            if (estadoActividad == null)
            {
                return NotFound();
            }

            return View(estadoActividad);
        }

        // GET: EstadoActividads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadoActividads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodEstado,Estado")] EstadoActividad estadoActividad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoActividad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoActividad);
        }

        // GET: EstadoActividads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoActividad = await _context.EstadoActividad.FindAsync(id);
            if (estadoActividad == null)
            {
                return NotFound();
            }
            return View(estadoActividad);
        }

        // POST: EstadoActividads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodEstado,Estado")] EstadoActividad estadoActividad)
        {
            if (id != estadoActividad.CodEstado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoActividad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoActividadExists(estadoActividad.CodEstado))
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
            return View(estadoActividad);
        }

        // GET: EstadoActividads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoActividad = await _context.EstadoActividad
                .FirstOrDefaultAsync(m => m.CodEstado == id);
            if (estadoActividad == null)
            {
                return NotFound();
            }

            return View(estadoActividad);
        }

        // POST: EstadoActividads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadoActividad = await _context.EstadoActividad.FindAsync(id);
            _context.EstadoActividad.Remove(estadoActividad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoActividadExists(int id)
        {
            return _context.EstadoActividad.Any(e => e.CodEstado == id);
        }
    }
}

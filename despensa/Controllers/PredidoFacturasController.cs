using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using despensa.Models;
using despensa.Helpers;

namespace despensa.Controllers
{
    public class PredidoFacturasController : Controller
    {
        private readonly despensaContext _context;

        public PredidoFacturasController(despensaContext context)
        {
            _context = context;
        }

        // GET: PredidoFacturas
        public async Task<IActionResult> Index()
        {
            var despensaContext = _context.PredidoFactura.Include(p => p.CodClienteNavigation).Include(p => p.CodEmpleadoNavigation).Include(p => p.CodEstadoNavigation);
            return View(await despensaContext.ToListAsync());
        }

        // GET: PredidoFacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predidoFactura = await _context.PredidoFactura
                .Include(p => p.CodClienteNavigation)
                .Include(p => p.CodEmpleadoNavigation)
                .Include(p => p.CodEstadoNavigation)
                .FirstOrDefaultAsync(m => m.CodFactura == id);
            if (predidoFactura == null)
            {
                return NotFound();
            }

            return View(predidoFactura);
        }

        // GET: PredidoFacturas/Create
        public IActionResult Create()
        {
            ViewData["CodCliente"] = new SelectList(_context.Cliente, "CodCliente", "CodCliente");
            ViewData["CodEmpleado"] = new SelectList(_context.Empleado, "CodEmpleado", "CodEmpleado");
            ViewData["CodEstado"] = new SelectList(_context.EstadoPedido, "CodEstado", "CodEstado");

            try
            {
                var entradas = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
             
                return View(entradas.ToList());
            }
            catch
            {
                List<Item> cart = new List<Item>();
                return View(cart.ToList());
            }

           

        }

        // POST: PredidoFacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodFactura,FecEmision,TotalVendido,TotalCosto,CodEmpleado,CodCliente,CodEstado")] PredidoFactura predidoFactura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(predidoFactura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodCliente"] = new SelectList(_context.Cliente, "CodCliente", "CodCliente", predidoFactura.CodCliente);
            ViewData["CodEmpleado"] = new SelectList(_context.Empleado, "CodEmpleado", "CodEmpleado", predidoFactura.CodEmpleado);
            ViewData["CodEstado"] = new SelectList(_context.EstadoPedido, "CodEstado", "CodEstado", predidoFactura.CodEstado);
            return View(predidoFactura);
        }

        // GET: PredidoFacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predidoFactura = await _context.PredidoFactura.FindAsync(id);
            if (predidoFactura == null)
            {
                return NotFound();
            }
            ViewData["CodCliente"] = new SelectList(_context.Cliente, "CodCliente", "CodCliente", predidoFactura.CodCliente);
            ViewData["CodEmpleado"] = new SelectList(_context.Empleado, "CodEmpleado", "CodEmpleado", predidoFactura.CodEmpleado);
            ViewData["CodEstado"] = new SelectList(_context.EstadoPedido, "CodEstado", "CodEstado", predidoFactura.CodEstado);
            return View(predidoFactura);
        }

        // POST: PredidoFacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodFactura,FecEmision,TotalVendido,TotalCosto,CodEmpleado,CodCliente,CodEstado")] PredidoFactura predidoFactura)
        {
            if (id != predidoFactura.CodFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(predidoFactura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PredidoFacturaExists(predidoFactura.CodFactura))
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
            ViewData["CodCliente"] = new SelectList(_context.Cliente, "CodCliente", "CodCliente", predidoFactura.CodCliente);
            ViewData["CodEmpleado"] = new SelectList(_context.Empleado, "CodEmpleado", "CodEmpleado", predidoFactura.CodEmpleado);
            ViewData["CodEstado"] = new SelectList(_context.EstadoPedido, "CodEstado", "CodEstado", predidoFactura.CodEstado);
            return View(predidoFactura);
        }

        // GET: PredidoFacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predidoFactura = await _context.PredidoFactura
                .Include(p => p.CodClienteNavigation)
                .Include(p => p.CodEmpleadoNavigation)
                .Include(p => p.CodEstadoNavigation)
                .FirstOrDefaultAsync(m => m.CodFactura == id);
            if (predidoFactura == null)
            {
                return NotFound();
            }

            return View(predidoFactura);
        }

        // POST: PredidoFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var predidoFactura = await _context.PredidoFactura.FindAsync(id);
            _context.PredidoFactura.Remove(predidoFactura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PredidoFacturaExists(int id)
        {
            return _context.PredidoFactura.Any(e => e.CodFactura == id);
        }
    }
}

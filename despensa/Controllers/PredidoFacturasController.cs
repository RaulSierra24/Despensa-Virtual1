using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using despensa.Models;
using despensa.Helpers;
using System.Security.Claims;
using MySql.Data.MySqlClient.Memcached;

namespace despensa.Controllers
{
    public class PredidoFacturasController : Controller
    {
        private readonly despensa1Context _context;

        public PredidoFacturasController(despensa1Context context)
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
            ViewData["CodCliente"] = new SelectList(_context.Usuario, "CodUsuario", "CodUsuario");
            ViewData["CodEmpleado"] = new SelectList(_context.Usuario, "CodUsuario", "CodUsuario");
            ViewData["CodEstado"] = new SelectList(_context.EstadoPedido, "CodEstado", "CodEstado");
            
           
          
                return View();
        
           

        }
        public async Task<IActionResult> Factura(int? id)
        {
            Console.WriteLine("hola "+id);
            if (id == null)
            {
                return NotFound();
            }

            ViewData["detalle_factura"] = _context.DetalleFactura.Where(a => a.CodFactura == id);
            var factura = await _context.PredidoFactura
                .Include(f => f.CodClienteNavigation)
                .Include(f => f.CodEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.CodFactura == id);
            var aux = _context.Usuario.Where(a => a.CodUsuario == factura.CodEmpleado);
            

            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: PredidoFacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodFactura,FecEmision,TotalVendido,TotalCosto,CodEmpleado,CodCliente,CodEstado")] PredidoFactura predidoFactura)
        {
            ClaimsPrincipal currentUser = this.User;
            var identity = (ClaimsIdentity)currentUser.Identity;
            decimal? totalxproducto = 0;
            decimal? totalxproducto1 = 0;
            decimal? totalventa = 0;
            decimal? totalcosto = 0;
            List<DetalleFactura> lista = new List<DetalleFactura>();
            try
            {
                var entradas = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                if (entradas==null)
                {
                    ViewBag.ErrorCarrito = 3;
                    return View();
                }
                foreach (var item in entradas)
                {
                    lista.Add(new DetalleFactura { Precio = item.Product.PrecioVenta, Costo = item.Product.PrecioCosto, CodProducto = item.Product.CodProducto, Cantidad = item.Quantity });
                    totalxproducto = item.Product.PrecioVenta * item.Quantity;
                    totalxproducto1 = item.Product.PrecioCosto * item.Quantity;
                    totalventa = totalventa + totalxproducto;
                    totalcosto = totalcosto + totalxproducto1;
                    var producto = await _context.Producto.FindAsync(item.Product.CodProducto);
                    if (producto.Cantidad < item.Quantity)
                    {
                        ViewBag.Inexistencia = producto;
                        ViewBag.ErrorCarrito = 2;
                        return View();
                    }
                }
                if (totalventa < 50)
                {
                    ViewBag.ErrorCarrito = 1;
                    return View();
                }
                else {
                    foreach (var item in entradas)
                    {
                        var producto = await _context.Producto.FindAsync(item.Product.CodProducto);
                        producto.Cantidad = producto.Cantidad - item.Quantity;
                        _context.Update(producto);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch
            {
                ViewBag.ErrorCarrito = 3;
                return View();
            }


            if (ModelState.IsValid)
            {
                predidoFactura.FecEmision= DateTime.Now;
                predidoFactura.TotalVendido = totalventa;
                predidoFactura.TotalCosto = totalcosto;
                predidoFactura.CodCliente = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                predidoFactura.CodEstado=1;
                predidoFactura.DetalleFactura = lista;
                _context.Add(predidoFactura);
                await _context.SaveChangesAsync();
                limpiarCarrito();
                return RedirectToAction("Details", "Usuarios");
            }
            ViewBag.Pedidofactura = "entre aqui";
            ViewData["CodCliente"] = new SelectList(_context.Usuario, "CodUsuario", "CodUsuario", predidoFactura.CodCliente);
            ViewData["CodEmpleado"] = new SelectList(_context.Usuario, "CodUsuario", "CodUsuario", predidoFactura.CodEmpleado);
            ViewData["CodEstado"] = new SelectList(_context.EstadoPedido, "CodEstado", "CodEstado", predidoFactura.CodEstado);
            Console.WriteLine("no  entre");
            return View(predidoFactura);
        }
        public void limpiarCarrito()
        {
            List<Item> cart = new List<Item>();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
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
            ViewData["CodCliente"] = new SelectList(_context.Usuario, "CodUsuario", "CodUsuario", predidoFactura.CodCliente);
            ViewData["CodEmpleado"] = new SelectList(_context.Usuario, "CodUsuario", "CodUsuario", predidoFactura.CodEmpleado);
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
            ViewData["CodCliente"] = new SelectList(_context.Usuario, "CodUsuario", "CodUsuario", predidoFactura.CodCliente);
            ViewData["CodEmpleado"] = new SelectList(_context.Usuario, "CodUsuario", "CodUsuario", predidoFactura.CodEmpleado);
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

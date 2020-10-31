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
using Microsoft.AspNetCore.Authorization;

namespace despensa.Controllers
{
    public class PredidoFacturasController : Controller
    {
        private readonly despensa1Context _context;

        public PredidoFacturasController(despensa1Context context)
        {
            _context = context;
        }
        [Authorize(Roles = "3,2")]
        // GET: PredidoFacturas
        public IActionResult PedidosPendientes()
        {
            var entradas = (from m in _context.PredidoFactura.Include(p => p.CodClienteNavigation).Include(p => p.CodEmpleadoNavigation).Include(p => p.CodEstadoNavigation)
                            where m.CodEstado == 1
                            orderby m.FecEmision ascending
                            select m).ToList();
            return View(entradas);
        }
        [Authorize(Roles = "3,2")]
        public IActionResult TodosPedidos(string Nombre, string FecFinal, string FecInici)
        {
            var entradas = (from m in _context.PredidoFactura.Include(p => p.CodClienteNavigation).Include(p => p.CodEmpleadoNavigation).Include(p => p.CodEstadoNavigation)
                            orderby m.FecEmision ascending
                            select m).ToList();
            Console.WriteLine("Filtro de predido factura: nombre: '"+Nombre+"' fecini: '"+FecFinal+"' fecfin: '"+FecInici+"'");

            if (Nombre!=""&&Nombre!=null){entradas = entradas.Where(a=>a.CodClienteNavigation.PrimerNombre.ToLower().Contains(Nombre.ToLower())||a.CodClienteNavigation.PrimerApellido.ToLower().Contains(Nombre.ToLower())).ToList();}
            if (FecInici != "" && FecFinal!="" && FecInici != null && FecFinal != null) {
                try
                {
                    DateTime fecini = DateTime.ParseExact(FecInici, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime fecfin = DateTime.ParseExact(FecFinal, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    entradas = entradas.Where(a => a.FecEmision>=fecini && a.FecEmision <= fecfin).ToList();
                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            return View(entradas);
        }

        // GET: PredidoFacturas/Details/5
        [Authorize(Roles = "3,2,1")]
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
            var detallefactura = (from m in _context.DetalleFactura.Include(p => p.CodProductoNavigation)
                                  where m.CodFactura == id
                                  select m).ToList();
            if (predidoFactura == null)
            {
                return NotFound();
            }
            ViewBag.cantidad = detallefactura.Count;
            ViewBag.detalleFactura = detallefactura;
            return View(predidoFactura);
        }

        // GET: PredidoFacturas/Create
        [Authorize(Roles = "3,2,1")]
        public async Task<IActionResult> Create()
        {
            ClaimsPrincipal currentUser = this.User;
            var identity = (ClaimsIdentity)currentUser.Identity;
            int id= Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var usuario = await _context.Usuario
                .Include(u => u.CodEstadoNavigation)
                .Include(u => u.CodGeneoNavigation)
                .Include(u => u.CodRolNavigation)
                .FirstOrDefaultAsync(m => m.CodUsuario == id);
            ViewData["CodCliente"] = new SelectList(_context.Usuario, "CodUsuario", "CodUsuario");
            ViewData["CodEmpleado"] = new SelectList(_context.Usuario, "CodUsuario", "CodUsuario");
            ViewData["CodEstado"] = new SelectList(_context.EstadoPedido, "CodEstado", "CodEstado");
                return View(usuario);
        }

        [Authorize(Roles = "3,2,1")]
        public async Task<IActionResult> Factura(int? id)
        {
            Console.WriteLine("hola "+id);
            if (id == null)
            {
                return NotFound();
            }
            var factura = await _context.PredidoFactura
                .Include(f => f.CodClienteNavigation)
                .Include(f => f.CodEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.CodFactura == id);
            var aux = _context.Usuario.Where(a => a.CodUsuario == factura.CodEmpleado);

            var detallefactura = (from m in _context.DetalleFactura.Include(p => p.CodProductoNavigation)
                                  where m.CodFactura == id
                                  select m).ToList();
            if (factura == null)
            {
                return NotFound();
            }
            ViewBag.cantidad = detallefactura.Count;
            ViewBag.detalleFactura = detallefactura;
            ViewBag.factura = factura;
            ViewBag.empleado = factura.CodEmpleadoNavigation.PrimerNombre+" "+factura.CodEmpleadoNavigation.SegundoNombre+" "+factura.CodEmpleadoNavigation.PrimerApellido+" "+factura.CodEmpleadoNavigation.SegundoApellido;
            return View();

        }

        // POST: PredidoFacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "3,2,1")]
        public async Task<IActionResult> Create([Bind("CodFactura,Direccion_entrega,FecEmision,TotalVendido,TotalCosto,CodEmpleado,CodCliente,CodEstado")] PredidoFactura predidoFactura)
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
                    if (item.Quantity == 0)
                    {
                        ViewBag.ErrorCarrito = 4;
                        return View();
                    }
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
        [Authorize(Roles = "3,2")]
        public async Task<IActionResult> Edit(int? id)
        {
            ClaimsPrincipal currentUser = this.User;
            var identity = (ClaimsIdentity)currentUser.Identity;
            if (id == null)
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }

            var predidoFactura = await _context.PredidoFactura
                .Include(p => p.CodClienteNavigation)
                .Include(p => p.CodEmpleadoNavigation)
                .Include(p => p.CodEstadoNavigation)
                .FirstOrDefaultAsync(m => m.CodFactura == id);
            var detallefactura = (from m in _context.DetalleFactura.Include(p => p.CodProductoNavigation)
                                  where m.CodFactura == id
                                  select m).ToList();
            if (predidoFactura == null )
            {
                return NotFound();
            }
            //borrar los view
            ViewBag.id = id;
            ViewBag.cantidad = detallefactura.Count;
            ViewBag.detalleFactura = detallefactura;
            ViewBag.NombreEmpleado= identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            ViewBag.ApelldoEmpleado = identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            return View(predidoFactura);
        }

        // POST: PredidoFacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "3,2")]
        public async Task<IActionResult> Edit(int id, [Bind("CodFactura,FecEmision,TotalVendido,TotalCosto,CodEmpleado,CodCliente,CodEstado")] PredidoFactura predidoFactura)
        {
            ClaimsPrincipal currentUser = this.User;
            var identity = (ClaimsIdentity)currentUser.Identity;
            var hola = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var rol = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value);
            if (id != predidoFactura.CodFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    despensa1Context db = new despensa1Context();
                    var aux = await db.PredidoFactura.FindAsync(id);
                    
                    if (rol==3)
                    {
                        aux.CodEstado = predidoFactura.CodEstado;
                    }
                    aux.CodEstado = 2;
                    aux.CodEmpleado = hola;
                    Factura factura = new Factura();
                    factura.Fecha = DateTime.Now;
                    factura.CodEmpleado = aux.CodEmpleado;
                    factura.CodCliente = aux.CodCliente;
                    _context.Update(aux);
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
                return RedirectToAction(nameof(PedidosPendientes));
            }
            ViewData["CodCliente"] = new SelectList(_context.Usuario, "CodUsuario", "CodUsuario", predidoFactura.CodCliente);
            ViewData["CodEmpleado"] = new SelectList(_context.Usuario, "CodUsuario", "CodUsuario", predidoFactura.CodEmpleado);
            ViewData["CodEstado"] = new SelectList(_context.EstadoPedido, "CodEstado", "CodEstado", predidoFactura.CodEstado);
            return View(predidoFactura);
        }

        // GET: PredidoFacturas/Delete/5
        [Authorize(Roles = "3")]
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
        [Authorize(Roles = "3")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detallefactura = (from m in _context.DetalleFactura
                                  where m.CodFactura == id
                                  select m).ToList();
            foreach (var item in detallefactura)
            {
                if (item.CodFactura == id)
                {
                    var auto3 = await _context.Producto.FindAsync(item.CodProducto);
                    auto3.Cantidad = auto3.Cantidad + item.Cantidad;
                    try
                    {
                        _context.Update(auto3);
                    }
                    catch (DbUpdateConcurrencyException)
                    {

                    }
                }
            }
            Console.WriteLine("eliminar: " + id);
            var bitacora = await _context.PredidoFactura.FindAsync(id);
            bitacora.CodEstado = 3;
            _context.Update(bitacora);
            //_context.PredidoFactura.Remove(predidoFactura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PedidosPendientes));
        }

        private bool PredidoFacturaExists(int id)
        {
            return _context.PredidoFactura.Any(e => e.CodFactura == id);
        }
    }
}

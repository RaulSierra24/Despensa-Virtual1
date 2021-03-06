﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using despensa.Models;
using X.PagedList;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace despensa.Controllers
{
    public class ProductoesController : Controller
    {
        private readonly despensa1Context _context;
        private readonly IWebHostEnvironment HostEnvironment;
        private readonly SqlConnection _conn;
        public ProductoesController(despensa1Context context, IWebHostEnvironment hostEnvironment)
        {
            this.HostEnvironment = hostEnvironment;
            _context = context;
        }

        // GET: Productoes
        [Authorize(Roles = "2,1,3")]
        public ActionResult Index(int? page, int idcat)
        {
            Console.WriteLine("hola"+idcat);
            if (idcat<=0)
            {
                return RedirectToAction("Index","Categorias");
            }
            ViewBag.categoriass = idcat;
            var pageNumber = page ?? 1;

            var entradas = (from m in _context.Producto.Include(p => p.CodEstadoNavigation).Include(p => p.CodMarcaNavigation).Include(p => p.CodProveedorNavigation).Include(p => p.CodCategoriaNavigation)
                            where m.CodEstado == 3 && m.Cantidad > 0 && m.CodCategoria == idcat
                            orderby m.Nombre descending
                            select m).ToList();

            var unaPagina = entradas.ToPagedList(pageNumber, 9);
            if (entradas.Count>0)
            {
                ViewBag.NombreCat = entradas[0].CodCategoriaNavigation.Nombre;  //cart[i].Product.Id
            }
            else
            {
                ViewBag.NombreCat = "No Hay Productos En Existencia";
            }
           
            return View(unaPagina);
        }

        [Authorize(Roles = "3")]
        public async Task<IActionResult> IndexDesactivo(int? page)
        {
            var pageNumber = page ?? 1;

            var entradas = (from m in _context.Producto
                            where m.CodEstado == 4 || m.Cantidad < 1
                            orderby m.Nombre descending
                            select m).ToList();
            var unaPagina = entradas.ToPagedList(pageNumber, 9);
            ViewBag.pagina = unaPagina;
            var despensaContext = _context.Producto.Include(p => p.CodEstadoNavigation).Include(p => p.CodMarcaNavigation).Include(p => p.CodProveedorNavigation);
            return View(await despensaContext.ToListAsync());
        }

        public async Task<IActionResult> BuscarProducto(int? page, string Buscar)
        {
            if (Buscar == null || Buscar =="")
            {
                return RedirectToAction("Index", "Categorias");
            }
            ViewBag.Buscar = Buscar;
            var pageNumber = page ?? 1;

            var entradas = (from m in _context.Producto
                            where m.CodEstado == 3 && m.Cantidad > 0
                            orderby m.Nombre descending
                            select m).ToList();


                entradas = entradas.Where(a => a.Nombre.ToLower().Contains(Buscar.ToLower())).ToList();

            var unaPagina = entradas.ToPagedList(pageNumber, 20);
            
            ViewBag.pagina = unaPagina;
            var despensaContext = _context.Producto.Include(p => p.CodEstadoNavigation).Include(p => p.CodMarcaNavigation).Include(p => p.CodProveedorNavigation);
            return View(await despensaContext.ToListAsync());
        }

        // GET: Productoes/Details/5
        [Authorize(Roles = "3,2,1")]
        public async Task<IActionResult> Details(int? id, int idcat)
        {
            ViewData["codigo_cat"] = idcat;
            if (id == null)
            {
                Console.WriteLine("entreaqui1");
                return NotFound();
            }
            var r = new Random();
            var aux = await _context.Producto
                .Include(p => p.CodCategoriaNavigation)
                .OrderByDescending(x => x.Cantidad).Take(10).ToArrayAsync();
            ViewBag.productosExtra = aux;

            var producto = await _context.Producto
                .Include(p => p.CodEstadoNavigation)
                .Include(p => p.CodMarcaNavigation)
                .Include(p => p.CodProveedorNavigation)
                .Include(p => p.CodCategoriaNavigation)   
                .FirstOrDefaultAsync(m => m.CodProducto == id);
            
            if (producto == null)
            {
                Console.WriteLine("entreaqui2");
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create[Authorize(Roles = "Administrador,Cliente,Empleado")]
        [Authorize(Roles = "3,2")]
        public IActionResult Create(int idcat)
        {
            if (idcat>0)
            {
                ViewBag.nombre_cat = _context.Categoria.Where(a => a.CodCategoria == idcat);
                ViewBag.codig_cat = idcat;
                ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "Estado");
                ViewData["CodCategoria"] = new SelectList(_context.Categoria, "CodCategoria", "CodCategoria");
                ViewData["CodMarca"] = new SelectList(_context.Marca, "CodMarca", "Marca1");
                ViewData["CodProveedor"] = new SelectList(_context.Proveedor, "CodProveedor", "Proveedor1");
                return View();
            }
            return RedirectToAction("Index", "Categorias");
            
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "3,2")]
        public async Task<IActionResult> Create([Bind("CodProducto,Nombre,FecCaducidad,PrecioCosto,PrecioVenta,ImageFie,Peso,CodEstado,Cantidad,CodProveedor,CodMarca,CodCategoria")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                if (producto.ImageFie != null)
                {
                    string carpeta = HostEnvironment.WebRootPath;
                    string nombrearchivo = Path.GetFileNameWithoutExtension(producto.ImageFie.FileName);
                    string extencion = Path.GetExtension(producto.ImageFie.FileName);
                    producto.Imagen = nombrearchivo = "produc" + DateTime.Now.ToString("yymmssfff") + extencion;
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
                return RedirectToAction("Index", "Productoes", new { idcat = producto.CodCategoria });
            }
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", producto.CodEstado);
            ViewData["CodMarca"] = new SelectList(_context.Marca, "CodMarca", "CodMarca", producto.CodMarca);
            ViewData["CodProveedor"] = new SelectList(_context.Proveedor, "CodProveedor", "CodProveedor", producto.CodProveedor);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        [Authorize(Roles = "3,2")]
        public async Task<IActionResult> Edit(int? id, int idcat)
        {
            
            if (id == null || idcat==0)
            {
                return NotFound();
            }
            ViewBag.codig_cat = idcat;
            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "Estado", producto.CodEstado);
            ViewData["CodMarca"] = new SelectList(_context.Marca, "CodMarca", "Marca1", producto.CodMarca);
            ViewData["CodProveedor"] = new SelectList(_context.Proveedor, "CodProveedor", "Proveedor1", producto.CodProveedor);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "3,2")]
        public async Task<IActionResult> Edit(int id, [Bind("Imagen,CodProducto,Nombre,FecCaducidad,PrecioCosto,PrecioVenta,ImageFie,Peso,CodEstado,Cantidad,CodProveedor,CodMarca,CodCategoria")] Producto producto)
        {
            Console.WriteLine("edit productos: "+producto.ImageFie);    
            if (id != producto.CodProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (producto.ImageFie != null)
                    {
                        string carpeta = HostEnvironment.WebRootPath;
                        string nombrearchivo = Path.GetFileNameWithoutExtension(producto.ImageFie.FileName);
                        string extencion = Path.GetExtension(producto.ImageFie.FileName);
                        producto.Imagen = nombrearchivo = "produc" + DateTime.Now.ToString("yymmssfff") + extencion;
                        string path = Path.Combine(carpeta + "/image/", nombrearchivo);
                        using (var hola = new FileStream(path, FileMode.Create))
                        {
                            await producto.ImageFie.CopyToAsync(hola);
                        }
                    }
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
                return RedirectToAction(nameof(Index), new { idcat = producto.CodCategoria });
            }
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", producto.CodEstado);
            ViewData["CodMarca"] = new SelectList(_context.Marca, "CodMarca", "CodMarca", producto.CodMarca);
            ViewData["CodProveedor"] = new SelectList(_context.Proveedor, "CodProveedor", "CodProveedor", producto.CodProveedor);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        [Authorize(Roles = "3,2")]
        public async Task<IActionResult> Delete(int? id, int idcat)
        {
            ViewData["codig_cat"] = idcat;
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
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

        // POST: Productoes/Delete/5
        [Authorize(Roles = "3,2")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { idcat = producto.CodCategoria });
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.CodProducto == id);
        }
    }
}

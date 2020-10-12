using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using despensa.Models;
using System.Security.Claims;

namespace despensa.Controllers
{
    public class ClientesController : Controller
    {
        private readonly despensaContext _context;

        public ClientesController(despensaContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var despensaContext = _context.Cliente.Include(c => c.CodEstadoNavigation).Include(c => c.CodGeneroNavigation).Include(c => c.CodRolNavigation);
            return View(await despensaContext.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.CodEstadoNavigation)
                .Include(c => c.CodGeneroNavigation)
                .Include(c => c.CodRolNavigation)
                .FirstOrDefaultAsync(m => m.CodCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado");
            ViewData["CodGenero"] = new SelectList(_context.Genero, "CodGenero", "CodGenero");
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "CodRol");
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodCliente,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Cui,Telefono,Direccion,Nit,CorreoElectronico,FecNacimiento,Contrasenia,CodGenero,CodRol,CodEstado")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", cliente.CodEstado);
            ViewData["CodGenero"] = new SelectList(_context.Genero, "CodGenero", "CodGenero", cliente.CodGenero);
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "CodRol", cliente.CodRol);
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", cliente.CodEstado);
            ViewData["CodGenero"] = new SelectList(_context.Genero, "CodGenero", "CodGenero", cliente.CodGenero);
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "CodRol", cliente.CodRol);
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodCliente,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Cui,Telefono,Direccion,Nit,CorreoElectronico,FecNacimiento,Contrasenia,CodGenero,CodRol,CodEstado")] Cliente cliente)
        {
            if (id != cliente.CodCliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.CodCliente))
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
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", cliente.CodEstado);
            ViewData["CodGenero"] = new SelectList(_context.Genero, "CodGenero", "CodGenero", cliente.CodGenero);
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "CodRol", cliente.CodRol);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .Include(c => c.CodEstadoNavigation)
                .Include(c => c.CodGeneroNavigation)
                .Include(c => c.CodRolNavigation)
                .FirstOrDefaultAsync(m => m.CodCliente == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.CodCliente == id);
        }





        [HttpGet]
        public IActionResult Login()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Empleado login, string ReturnUrl = "")
        {


            using (despensaContext dc = new despensaContext())
            {

                ClaimsIdentity identity = null;
                bool isAuthenticated = false;
                var ad1 = dc.Empleado.FirstOrDefaultAsync();
                var ad = dc.Empleado.FirstOrDefault();
                var c = dc.Empleado.Where(w => w.CorreoElectronico == login.CorreoElectronico).FirstOrDefault();



                if (c != null)
                {
                    if (string.Compare(Crypto.Hash(login.Contraseña), c.Contraseña) == 0)
                    {
                        var nombres = ad.PrimerNombre;

                        Console.WriteLine("Nombre Completo:" + nombres);
                        var nombre = c.PrimerNombre + c.PrimerApellido;

                        Console.WriteLine("Nombre Completo:" + nombre);
                        System.Diagnostics.Debug.WriteLine("nombre" + nombre);

                        var pers = dc.Rol.Where(p => p.CodRol == c.CodRol).FirstOrDefault();

                        Console.WriteLine("id_ROL:" + pers.CodRol);
                        System.Diagnostics.Debug.WriteLine("id_ROL:" + pers.CodRol);

                        identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, c.PrimerNombre),
                        new Claim(ClaimTypes.NameIdentifier, ""+c.),
                        new Claim(ClaimTypes.Role, pers.rol)
                        }, CookieAuthenticationDefaults.AuthenticationScheme);
                        isAuthenticated = true;


                        if (isAuthenticated)
                        {
                            //contador_sesion++;
                            var principal = new ClaimsPrincipal(identity);
                            var loginA = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                            return RedirectToAction("Index", "Home");

                        }

                        //int timeout = login.RememberMe ? 525600 : 20;  //52600 min=1año
                        //var ticket = new FormsAuthenticationTicket(login.Email, login.RememberMe, timeout);
                        //string encrypted = FormsAuthentication.Encrypt(ticket);
                        //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        //cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        //cookie.HttpOnly = true;
                        //Response.Cookies.Add(cookie);

                    }
                    else
                    {
                        RedirectToAction("Login");
                    }

                }
                else
                {
                    RedirectToAction("Login");
                }

            }

            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {


            var loginA = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //    contador_inicio_sesion++;
            //Console.WriteLine("contador index:"+TbEstudiantexcursoesController.contador3);
            //  TbEstudiantexcursoesController.contador3 = 0;
            //TbEstudiantexcursoesController.asCurso.Clear();
            return RedirectToAction("Login");

        }

        //Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout(string j)
        {
            var loginA = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}

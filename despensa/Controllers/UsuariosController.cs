using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using despensa.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;
using System.Net.Mail;

namespace despensa.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly despensa1Context _context;

        public UsuariosController(despensa1Context context)
        {
            _context = context;
        }

        // GET: Usuarios
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Index(int? page, string Nombre, string Apellido,string cui)
        {
            var pageNumber = page ?? 1;
            var entradas = (from m in _context.Usuario.Include(u => u.CodEstadoNavigation).Include(u => u.CodGeneoNavigation).Include(u => u.CodRolNavigation)
                            orderby m.CodRol descending
                            select m).ToList();

            if (Nombre != "" && Nombre != null) { ViewBag.nombres=Nombre;            entradas = entradas.Where(a => a.PrimerNombre.ToLower().Contains(Nombre.ToLower()) ).ToList(); }
            if (Apellido != "" && Apellido != null) { ViewBag.apellido = Apellido; entradas = entradas.Where(a => a.PrimerApellido.ToLower().Contains(Apellido.ToLower()) ).ToList(); }
            if (cui != "" && cui != null) { ViewBag.cui = cui; entradas = entradas.Where(a => a.Cui.Contains(cui.ToLower())).ToList(); }
            var entrada = entradas.ToPagedList(pageNumber, 3);
            return View(entrada);
        }

        // GET: Usuarios/Details/5
        [Authorize(Roles = "3,2,1")]    
        public async Task<IActionResult> Details(int? id)
        {
            ClaimsPrincipal currentUser = this.User;
            var identity = (ClaimsIdentity)currentUser.Identity;
            id = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.mispendientes = (from m in _context.PredidoFactura
                                     where m.CodCliente == id && m.CodEstado == 1
                                     orderby m.FecEmision descending
                                     select m).ToList();
            ViewBag.misEntregados = (from m in _context.PredidoFactura
                                     where m.CodCliente == id  && m.CodEstado == 2
                                     orderby m.FecEmision descending
                                     select m).ToList();
            ViewBag.misCancelados = (from m in _context.PredidoFactura
                                     where m.CodCliente == id  && m.CodEstado == 3
                                     orderby m.FecEmision descending
                                     select m).ToList();

            var usuario = await _context.Usuario
                .Include(u => u.CodEstadoNavigation)
                .Include(u => u.CodGeneoNavigation)
                .Include(u => u.CodRolNavigation)
                .FirstOrDefaultAsync(m => m.CodUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["CodGeneo"] = new SelectList(_context.Genero, "CodGenero", "Genero1", usuario.CodGeneo);
            return View(usuario);
        }

        // GET: Usuarios/Create
        [Authorize(Roles = "3")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrimerNombre,PrimerApellido,Direccion,CorreoElectronico,Contraseña,ConfirmarContraseña")] Usuario usuario)
        {
            if (usuario.Contraseña.Equals(usuario.ConfirmarContraseña))
            {
                if (ModelState.IsValid)
                {
                    string resetCode = Guid.NewGuid().ToString();
                    EnviarEmail(usuario.CorreoElectronico, resetCode);
                    usuario.Grid = resetCode;
                    usuario.Cui = "";
                    usuario.CodEstado = 4;
                    usuario.CodRol = 1;
                    usuario.Contraseña = Crypto.Hash(usuario.Contraseña);
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    ClaimsIdentity identity = null;
                    bool isAuthenticated = false;
                    identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, ""+usuario.PrimerNombre),
                        new Claim(ClaimTypes.Email, ""+usuario.SegundoNombre),
                        new Claim(ClaimTypes.NameIdentifier, ""+usuario.CodUsuario),
                        new Claim(ClaimTypes.Role, "")
                        }, CookieAuthenticationDefaults.AuthenticationScheme);

                    isAuthenticated = true;


                    if (isAuthenticated)
                    {
                        //contador_sesion++;
                        var principal = new ClaimsPrincipal(identity);
                        var loginA = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                            return RedirectToAction("Details", "Usuarios");
                    }
                }
            }
            else
            {
                ViewBag.Error1 = 1;
            }
            
            return View("Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "3,2,1")]
        public async Task<IActionResult> Cambiar_foto(int id, [Bind("CodUsuario,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Contraseña,ConfirmarContraseña,CodGenero,Cui,Telefono,Direccion,Nit,CorreoElectronico,FecNac,CodGeneo,CodRol,CodEstado")] Usuario usuario)
        {
            ClaimsPrincipal currentUser = this.User;
            var identity = (ClaimsIdentity)currentUser.Identity;
            id = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            Console.WriteLine("entre al editar" + id);
            if (ModelState.IsValid)
            {


                if (usuario.Contraseña.Equals(usuario.ConfirmarContraseña))
                {
                    var contraseña = usuario.Contraseña;
                    try
                    {
                        despensa1Context db = new despensa1Context();
                        var anterior = await db.Usuario.FindAsync(id);
                        usuario = anterior;
                        usuario.Contraseña = contraseña;
                        _context.Update(usuario);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UsuarioExists(usuario.CodUsuario))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Details", "Usuarios", new { id = id });
                }
                else
                {

                    ViewBag.Error1 = 1;
                    return View("Details");
                }

            }
            ViewData["CodGeneo"] = new SelectList(_context.Genero, "CodGenero", "Genero1", usuario.CodGeneo);
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "Estado", usuario.CodEstado);
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "Rol1", usuario.CodRol);
            return RedirectToAction("Details", "Usuarios", new { id = id });
        }

        [Authorize(Roles = "3")]
        public async Task<IActionResult> Edit(int? id)
        {
            /*
            ClaimsPrincipal currentUser = this.User;
            var identity = (ClaimsIdentity)currentUser.Identity;
            var id3 = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);*/

            if (id == null )
            {
                return NotFound();
            }




            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "Estado", usuario.CodEstado);
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "Rol1", usuario.CodRol);
            return View(usuario);
        }
        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "3,2,1")]
        public async Task<IActionResult> Edit(int id, [Bind("CodUsuario,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Contraseña,CodGenero,Cui,Telefono,Direccion,Nit,CorreoElectronico,FecNac,CodGeneo,CodRol,CodEstado")] Usuario usuario)
        {
            
            ClaimsPrincipal currentUser = this.User;
            var identity = (ClaimsIdentity)currentUser.Identity;
            id = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if (usuario.CodUsuario != id)
            {
                return RedirectToAction("Details", "Usuarios", new { id = id });
            }
            Console.WriteLine("entre al editar" + id);
            if (ModelState.IsValid)
            {
                Console.WriteLine("ensi entre en el segundo tre al editar" + id);
                try
                {
                    despensa1Context db = new despensa1Context();
                    var anterior = await db.Usuario.FindAsync(id);
                    usuario.CodUsuario = id;
                    usuario.CodEstado = anterior.CodEstado;
                    usuario.CodRol = anterior.CodRol;
                    usuario.Contraseña = anterior.Contraseña;

                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.CodUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Usuarios", new { id = id });
            }
            ViewData["CodGeneo"] = new SelectList(_context.Genero, "CodGenero", "Genero1", usuario.CodGeneo);
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "Estado", usuario.CodEstado);
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "Rol1", usuario.CodRol);
            return RedirectToAction("Details", "Usuarios", new { id = id });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "3,2,1")]
        public async Task<IActionResult> Cambiar_contaseña(int id, [Bind("CodUsuario,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Contraseña,ConfirmarContraseña,CodGenero,Cui,Telefono,Direccion,Nit,CorreoElectronico,FecNac,CodGeneo,CodRol,CodEstado")] Usuario usuario)
        {
            ClaimsPrincipal currentUser = this.User;
            var identity = (ClaimsIdentity)currentUser.Identity;
            id = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if (usuario.CodUsuario != id)
            {
                return RedirectToAction("Details", "Usuarios", new { id = id });
            }
            Console.WriteLine("entre al editar" + id);
            if (ModelState.IsValid)
            {

                if (usuario.Contraseña.Equals(usuario.ConfirmarContraseña))
                {
                    var contraseña = usuario.Contraseña;
                    try
                    {
                        despensa1Context db = new despensa1Context();
                        var anterior = await db.Usuario.FindAsync(id);
                        usuario = anterior;
                        usuario.Contraseña = Crypto.Hash(contraseña);
                        _context.Update(usuario);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UsuarioExists(usuario.CodUsuario))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Details", "Usuarios", new { id = id });
                }
                else
                {
                    
                    ViewBag.Error1 = 1;
                    return View("Details");
                }
                
            }
            ViewData["CodGeneo"] = new SelectList(_context.Genero, "CodGenero", "Genero1", usuario.CodGeneo);
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "Estado", usuario.CodEstado);
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "Rol1", usuario.CodRol);
            return RedirectToAction("Details", "Usuarios", new { id = id });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> UsuarioAdmin(int id, [Bind("CodUsuario,CodRol,CodEstado")] Usuario usuario)
        {
            Console.WriteLine("usuario admin: "+usuario.CodUsuario+" "+usuario.CodRol+" "+usuario.CodEstado+" "+id);
            ClaimsPrincipal currentUser = this.User;
            var identity = (ClaimsIdentity)currentUser.Identity;
            var id3 = Int32.Parse(identity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            if (usuario.CodUsuario == id3)
            {
                return RedirectToAction("Index", "Usuarios", new { id = id });
            }
            if (ModelState.IsValid)
            {
                    try
                    {
                        despensa1Context db = new despensa1Context();
                        var anterior = await db.Usuario.FindAsync(usuario.CodUsuario);
                        anterior.CodRol = usuario.CodRol;
                        anterior.CodEstado = usuario.CodEstado;
                        _context.Update(anterior);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UsuarioExists(usuario.CodUsuario))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Index", "Usuarios");

            }
            ViewData["CodGeneo"] = new SelectList(_context.Genero, "CodGenero", "Genero1", usuario.CodGeneo);
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "Estado", usuario.CodEstado);
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "Rol1", usuario.CodRol);
            return RedirectToAction("Index", "Usuarios", new { id = id });
        }

        // GET: Usuarios/Delete/5
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario
                .Include(u => u.CodEstadoNavigation)
                .Include(u => u.CodGeneoNavigation)
                .Include(u => u.CodRolNavigation)
                .FirstOrDefaultAsync(m => m.CodUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "3")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.CodUsuario == id);
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
        public ActionResult Login(Usuario login, string ReturnUrl)
        {

            Console.WriteLine("\""+login.Contraseña+"\"  \""+login.CorreoElectronico+"\"");
            using (despensa1Context dc = new despensa1Context())
            {
                ClaimsIdentity identity = null;
                bool isAuthenticated = false;
                var ad = dc.Usuario.FirstOrDefault();
                var c = dc.Usuario.Where(w => w.CorreoElectronico == login.CorreoElectronico).FirstOrDefault();
            //    Console.WriteLine("\"" + c.Contraseña + "\"  \"" + c.CorreoElectronico + "\"");
                if (c != null)
                {   
                    if (string.Compare(Crypto.Hash(login.Contraseña), c.Contraseña) == 0)
                    {
                        if (c.CodEstado==3) { 
                            var nombres = ad.PrimerNombre;

                            Console.WriteLine("Nombre Completo:" + nombres);
                            var nombre = c.PrimerNombre + c.PrimerApellido;

                            Console.WriteLine("Nombre Completo:" + nombre);
                            System.Diagnostics.Debug.WriteLine("nombre" + nombre);

                            var pers = dc.Rol.Where(p => p.CodRol == c.CodRol).FirstOrDefault();

                                Console.WriteLine("id_ROL:" + pers.CodRol);
                            System.Diagnostics.Debug.WriteLine("id_ROL:" + pers.CodRol);
                            identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, ""+c.PrimerNombre),
                            new Claim(ClaimTypes.Email, ""+c.SegundoNombre),
                            new Claim(ClaimTypes.NameIdentifier, ""+c.CodUsuario),
                            new Claim(ClaimTypes.Role, pers.CodRol+"")
                            }, CookieAuthenticationDefaults.AuthenticationScheme);

                            Console.WriteLine("rol \"" + pers.Rol1 + "\"");
                            isAuthenticated = true;


                            if (isAuthenticated)
                            {
                                //contador_sesion++;
                                var principal = new ClaimsPrincipal(identity);
                                var loginA = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                                Console.WriteLine("este es el return"+ReturnUrl);
                                if (ReturnUrl!="")
                                {
                                    return RedirectToAction("Index", "Categorias");
                                }
                                else
                                {
                                    return RedirectToAction(ReturnUrl);
                                }
                            
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
                            ViewBag.ActividadCuenta = 1;
                            RedirectToAction("Login");
                        }
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
            HttpContext.Session.Clear();

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
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }




        private void EnviarEmail(string EmailDestino, string token)
        {
            string EmailOrigen = "despensavirtual925@gmail.com";
            string Contraseña = "DespVirtual925";

            string url = "https://localhost:44383/Usuarios/ConfirmarCuenta/" + token;
            MailMessage oMailMensaje = new MailMessage(EmailOrigen, EmailDestino, "Confirmar Cuenta:", "<a href='" + url + "'>Click aqui para Confirmar tu cuenta</a>");
            oMailMensaje.IsBodyHtml = true;
            SmtpClient osmtpClient = new SmtpClient("smtp.gmail.com");
            osmtpClient.EnableSsl = true;
            osmtpClient.UseDefaultCredentials = false;
            osmtpClient.Port = 587;
            osmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);
            osmtpClient.Send(oMailMensaje);
            osmtpClient.Dispose();
        }


        public async Task<IActionResult> ConfirmarCuenta(string id)
        {
            if (ModelState.IsValid)
            {
                using (despensa1Context dc = new despensa1Context())
                {
                    var user = dc.Usuario.Where(a => a.Grid.Equals(id)).FirstOrDefault();
                    if (user != null)
                    {
                        user.CodEstado = 3;
                        user.Grid = null;
                        //  dc.Configuration.ValidateOnSaveEnabled = false;
                        _context.Update(user);
                        await _context.SaveChangesAsync();

                        ClaimsIdentity identity = null;
                        bool isAuthenticated = false;
                        identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Name, ""+user.PrimerNombre),
                        new Claim(ClaimTypes.Email, ""+user.SegundoNombre),
                        new Claim(ClaimTypes.NameIdentifier, ""+user.CodUsuario),
                        new Claim(ClaimTypes.Role, ""+user.CodRol)
                        }, CookieAuthenticationDefaults.AuthenticationScheme);

                        isAuthenticated = true;


                        if (isAuthenticated)
                        {
                            //contador_sesion++;
                            var principal = new ClaimsPrincipal(identity);
                            var loginA = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                            return RedirectToAction("Details", "Usuarios");
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
        }
    }
}



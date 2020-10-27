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
    public class Usuarios1Controller : Controller
    {
        private readonly despensa1Context _context;

        public Usuarios1Controller(despensa1Context context)
        {
            _context = context;
        }

        // GET: Usuarios1
        public async Task<IActionResult> Index()
        {
            var despensa1Context = _context.Usuario.Include(u => u.CodEstadoNavigation).Include(u => u.CodGeneoNavigation).Include(u => u.CodRolNavigation);
            return View(await despensa1Context.ToListAsync());
        }

        // GET: Usuarios1/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Usuarios1/Create
        public IActionResult Create()
        {
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado");
            ViewData["CodGeneo"] = new SelectList(_context.Genero, "CodGenero", "CodGenero");
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "CodRol");
            return View();
        }

        // POST: Usuarios1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        // GET: Usuarios1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", usuario.CodEstado);
            ViewData["CodGeneo"] = new SelectList(_context.Genero, "CodGenero", "CodGenero", usuario.CodGeneo);
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "CodRol", usuario.CodRol);
            return View(usuario);
        }

        // POST: Usuarios1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodUsuario,PrimerNombre,SegundoNombre,PrimerApellido,SegundoApellido,Contraseña,CodGenero,Cui,Telefono,Direccion,Nit,CorreoElectronico,FecNac,CodGeneo,CodRol,CodEstado,Grid,Grid2,ImagenPerfil,PedidoFavorito")] Usuario usuario)
        {
            if (id != usuario.CodUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodEstado"] = new SelectList(_context.EstadoActividad, "CodEstado", "CodEstado", usuario.CodEstado);
            ViewData["CodGeneo"] = new SelectList(_context.Genero, "CodGenero", "CodGenero", usuario.CodGeneo);
            ViewData["CodRol"] = new SelectList(_context.Rol, "CodRol", "CodRol", usuario.CodRol);
            return View(usuario);
        }

        // GET: Usuarios1/Delete/5
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

        // POST: Usuarios1/Delete/5
        [HttpPost, ActionName("Delete")]
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
    }
}

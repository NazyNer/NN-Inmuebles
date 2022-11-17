using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NN_Inmuebles.Models;

namespace NN_Inmuebles.Controllers
{
    [Authorize]
    public class DevolucionController : Controller
    {
        private readonly NN_InmueblesContext _context;

        public DevolucionController(NN_InmueblesContext context)
        {
            _context = context;
        }

        // GET: Devolucion
        public async Task<IActionResult> Index()
        {
            var nN_InmueblesContext = _context.Devolucion.Include(d => d.Casa).Include(d => d.Cliente);
            return View(await nN_InmueblesContext.ToListAsync());
        }

        // GET: Devolucion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Devolucion == null)
            {
                return NotFound();
            }

            var devolucion = await _context.Devolucion
                .Include(d => d.Casa)
                .Include(d => d.Cliente)
                .FirstOrDefaultAsync(m => m.DevolucionID == id);
            if (devolucion == null)
            {
                return NotFound();
            }

            return View(devolucion);
        }

        // GET: Devolucion/Create
        public IActionResult Create()
        {   
            ViewData["CasaID"] = new SelectList(_context.Casa.Where(x => x.Alquilada == true && x.Eliminada == false), "CasaID", "NombreCasa");
            ViewData["AlquilerID"] = new SelectList(_context.Alquiler, "AlquilerID", "CasaID", "ClienteID");
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "NombreCliente" , "ApellidoCliente");
            return View();
        }

        // POST: Devolucion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DevolucionID,FechaDevolucion,AlquilerID,ClienteID,CasaID,ClienteNombre,CasaNombre")] Devolucion devolucion)
        {
            if (ModelState.IsValid)
            {
                try
                {    
                    var Alquiler = (from a in _context.Alquiler where a.ClienteID == devolucion.ClienteID && a.CasaID == devolucion.CasaID select a).SingleOrDefault();
                    if(Alquiler != null)
                    {
                        if(Alquiler.FechaAlquiler < devolucion.FechaDevolucion)
                        {
                            var Casa = (from a in _context.Casa where a.CasaID == devolucion.CasaID select a).SingleOrDefault();
                            var Cliente = (from a in _context.Cliente where a.ClienteID == devolucion.ClienteID select a).SingleOrDefault();
                            devolucion.CasaNombre = Casa.NombreCasa;
                            devolucion.ClienteNombre = Cliente.NombreCliente + " " + Cliente.ApellidoCliente;
                            Casa.Alquilada = false;
                            _context.Add(devolucion);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }catch (System.Exception ex){
                    var error = ex;
                }
            }
            ViewData["CasaID"] = new SelectList(_context.Casa.Where(x => x.Alquilada == true && x.Eliminada == false), "CasaID", "NombreCasa");
            ViewData["AlquilerID"] = new SelectList(_context.Alquiler, "AlquilerID", "CasaID", "ClienteID");
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "NombreCliente", "ApellidoCliente");
            return View(devolucion);
        }

        // GET: Devolucion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Devolucion == null)
            {
                return NotFound();
            }

            var devolucion = await _context.Devolucion.FindAsync(id);
            if (devolucion == null)
            {
                return NotFound();
            }
            ViewData["CasaID"] = new SelectList(_context.Casa, "CasaID", "NombreCasa", devolucion.CasaID);
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ApellidoCliente", devolucion.ClienteID);
            return View(devolucion);
        }

        // POST: Devolucion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DevolucionID,FechaDevolucion,AlquilerID,ClienteID,CasaID,ClienteNombre,CasaNombre")] Devolucion devolucion)
        {
            if (id != devolucion.DevolucionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(devolucion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DevolucionExists(devolucion.DevolucionID))
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
            ViewData["CasaID"] = new SelectList(_context.Casa, "CasaID", "NombreCasa", devolucion.CasaID);
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "Apellido", devolucion.ClienteID);
            return View(devolucion);
        }

        // GET: Devolucion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Devolucion == null)
            {
                return NotFound();
            }

            var devolucion = await _context.Devolucion
                .Include(d => d.Casa)
                .Include(d => d.Cliente)
                .FirstOrDefaultAsync(m => m.DevolucionID == id);
            if (devolucion == null)
            {
                return NotFound();
            }

            return View(devolucion);
        }

        // POST: Devolucion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Devolucion == null)
            {
                return Problem("Entity set 'NN_InmueblesContext.Devolucion'  is null.");
            }
            var devolucion = await _context.Devolucion.FindAsync(id);
            if (devolucion != null)
            {
                _context.Devolucion.Remove(devolucion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DevolucionExists(int id)
        {
          return (_context.Devolucion?.Any(e => e.DevolucionID == id)).GetValueOrDefault();
        }
    }
}

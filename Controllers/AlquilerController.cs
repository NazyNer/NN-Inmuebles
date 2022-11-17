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
    public class AlquilerController : Controller
    {
        private readonly NN_InmueblesContext _context;

        public AlquilerController(NN_InmueblesContext context)
        {
            _context = context;
        }

        // GET: Alquiler
        public async Task<IActionResult> Index()
        {
            var nN_InmueblesContext = _context.Alquiler.Include(a => a.Casa).Include(a => a.Cliente);
            return View(await nN_InmueblesContext.ToListAsync());
        }

        // GET: Alquiler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alquiler == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquiler
                .Include(a => a.Casa)
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.AlquilerID == id);
            if (alquiler == null)
            {
                return NotFound();
            }

            return View(alquiler);
        }

        // GET: Alquiler/Create
        public IActionResult Create()
        {
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "NombreCliente", "ApellidoCliente");
            ViewData["CasaID"] = new SelectList(_context.Casa.Where(x => x.Alquilada == false && x.Eliminada == false), "CasaID", "NombreCasa");
            return View();
        }

        // POST: Alquiler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlquilerID,FechaAlquiler,ClienteID,CasaID,ClienteNombre,CasaNombre")] Alquiler alquiler)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Casa = (from a in _context.Casa where a.CasaID == alquiler.CasaID select a).SingleOrDefault(); 
                    var Cliente = (from a in _context.Cliente where a.ClienteID == alquiler.ClienteID select a).SingleOrDefault();
                    alquiler.CasaNombre = Casa.NombreCasa;
                    alquiler.ClienteNombre = Cliente.NombreCliente + " " + Cliente.ApellidoCliente;
                    Casa.Alquilada = true;
                    _context.Add(alquiler);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }catch (System.Exception ex){
                    var error = ex;
                }
            }
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "NombreCliente", "ApellidoCliente");
            ViewData["CasasID"] = new SelectList(_context.Casa.Where(x => x.Alquilada == false && x.Eliminada == false), "CasaID", "NombreCasa");
            return View(alquiler);
        }

        // GET: Alquiler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alquiler == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquiler.FindAsync(id);
            if (alquiler == null)
            {
                return NotFound();
            }
            ViewData["CasasID"] = new SelectList(_context.Casa, "CasaID", "NombreCasa", alquiler.CasaID);
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID", alquiler.ClienteID);
            return View(alquiler);
        }

        // POST: Alquiler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlquilerID,FechaAlquiler,ClienteID,CasaID,ClienteNombre,CasaNombre")] Alquiler alquiler)
        {
            if (id != alquiler.AlquilerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alquiler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlquilerExists(alquiler.AlquilerID))
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
             ViewData["CasasID"] = new SelectList(_context.Casa, "CasaID", "NombreCasa", alquiler.CasaID);
            ViewData["ClienteID"] = new SelectList(_context.Cliente, "ClienteID", "ClienteID", alquiler.ClienteID);
            return View(alquiler);
        }

        // GET: Alquiler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alquiler == null)
            {
                return NotFound();
            }

            var alquiler = await _context.Alquiler
                .Include(a => a.Casa)
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.AlquilerID == id);
            if (alquiler == null)
            {
                return NotFound();
            }

            return View(alquiler);
        }

        // POST: Alquiler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alquiler == null)
            {
                return Problem("Entity set 'NN_InmueblesContext.Alquiler'  is null.");
            }
            var alquiler = await _context.Alquiler.FindAsync(id);
            if (alquiler != null)
            {
                _context.Alquiler.Remove(alquiler);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlquilerExists(int id)
        {
          return (_context.Alquiler?.Any(e => e.AlquilerID == id)).GetValueOrDefault();
        }
    }
}

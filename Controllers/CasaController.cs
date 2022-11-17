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
    public class CasaController : Controller
    {
        private readonly NN_InmueblesContext _context;

        public CasaController(NN_InmueblesContext context)
        {
            _context = context;
        }

        // GET: Casa
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
              return _context.Casa != null ? 
                          View(await _context.Casa.ToListAsync()) :
                          Problem("Entity set 'NN_InmueblesContext.Casa'  is null.");
        }

        // GET: Casa/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Casa == null)
            {
                return NotFound();
            }

            var casa = await _context.Casa
                .FirstOrDefaultAsync(m => m.CasaID == id);
            if (casa == null)
            {
                return NotFound();
            }

            return View(casa);
        }

        // GET: Casa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Casa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CasaID,NombreCasa,Domicilio,NombreDueño,ImagenCasa,Alquilada,Eliminada")] Casa casa, IFormFile ImagenCasa)
        {
            if (ModelState.IsValid)
            {
                if(ImagenCasa != null && ImagenCasa.Length > 0)
                {
                    byte[]? CasaImagen = null;
                    using(var fs1 = ImagenCasa.OpenReadStream())
                    using(var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        CasaImagen = ms1.ToArray();
                    }
                    casa.ImagenCasa = CasaImagen;
                }
                _context.Add(casa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(casa);
        }

        // GET: Casa/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Casa == null)
            {
                return NotFound();
            }

            var casa = await _context.Casa.FindAsync(id);
            if (casa == null)
            {
                return NotFound();
            }
            return View(casa);
        }

        // POST: Casa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CasaId,NombreCasa,Domicilio,NombreDueño,ImagenCasa,Alquilada,Eliminada")] Casa casa)
        {
            if (id != casa.CasaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(casa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CasaExists(casa.CasaID))
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
            return View(casa);
        }

        // GET: Casa/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Casa == null)
            {
                return NotFound();
            }

            var casa = await _context.Casa
                .FirstOrDefaultAsync(m => m.CasaID == id);
            if (casa == null)
            {
                return NotFound();
            }

            return View(casa);
        }

        // POST: Casa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Casa == null)
            {
                return Problem("Entity set 'NN_InmueblesContext.Casa'  is null.");
            }
            var casa = await _context.Casa.FindAsync(id);
            if (casa != null)
            {
                _context.Casa.Remove(casa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CasaExists(int id)
        {
          return (_context.Casa?.Any(e => e.CasaID == id)).GetValueOrDefault();
        }
    }
}

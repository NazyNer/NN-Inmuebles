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
        public async Task<IActionResult> Index()
        {
            return _context.Casa != null ? 
                        View(await _context.Casa.ToListAsync()) :
                        Problem("Entity set 'NN_InmueblesContext.Casa'  is null.");
        }

        [AllowAnonymous]
        public async Task<IActionResult> IndexAnon()
        {
            var nN_InmueblesContext = _context.Casa.Where(a => a.Alquilada == false && a.Eliminada == false);
            return View(await nN_InmueblesContext.ToListAsync());

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
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("CasaID,NombreCasa,Domicilio,NombreDueño,ImagenCasa,Alquilada,Eliminada")] Casa casa, IFormFile ImagenCasa)
        {
            if (id != casa.CasaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
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


        // POST: Casa/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int ID)
        {
            var casa = await _context.Casa.FindAsync(ID);
            if(casa.Alquilada == true){
                return RedirectToAction(nameof(Index));
            }
            if(casa.Eliminada == true)
            {
                return RedirectToAction(nameof(Index));
            }
            if(casa != null)
            {
                var CasaAlquilada = (from a in _context.Alquiler where a.CasaID == ID select a).Count();
                if(CasaAlquilada == 0)
                {
                    _context.Casa.Remove(casa);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    casa.Eliminada = true;
                    casa.NombreCasa = casa.NombreCasa +  " ❌Eliminada❌";
                    _context.Update(casa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CasaExists(int ID)
        {
          return (_context.Casa?.Any(e => e.CasaID == ID)).GetValueOrDefault();
        }
    }
}

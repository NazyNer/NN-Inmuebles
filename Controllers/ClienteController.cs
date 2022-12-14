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
    public class ClienteController : Controller
    {
        private readonly NN_InmueblesContext _context;

        public ClienteController(NN_InmueblesContext context)
        {
            _context = context;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            return _context.Cliente != null ? 
                        View(await _context.Cliente.ToListAsync()) :
                        Problem("Entity set 'NN_InmueblesContext.Cliente'  is null.");
        }


        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteID,NombreCliente,ApellidoCliente,DNI,FechaNacimiento")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cliente == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteID,NombreCliente,ApellidoCliente,DNI,FechaNacimiento")] Cliente cliente)
        {
            if (id != cliente.ClienteID)
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
                    if (!ClienteExists(cliente.ClienteID))
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
            return View(cliente);
        }


        // POST: Cliente/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (_context.Cliente == null)
            {
                return Problem("'NN_InmueblesContext.Cliente'  es nulo.");
            }
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente != null)
            {
                var clienteAlquiler = (from a in _context.Alquiler where a.ClienteID == id select a).Count();
                if(clienteAlquiler == 0)
                {
                    _context.Cliente.Remove(cliente);
                    await _context.SaveChangesAsync();
                }
                else
                {

                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return (_context.Cliente?.Any(e => e.ClienteID == id)).GetValueOrDefault();
        }
    }
}

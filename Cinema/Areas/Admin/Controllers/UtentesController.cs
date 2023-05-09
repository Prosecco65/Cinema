using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cinema.DataAccess;
using Cinema.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UtentesController : Controller
    {
        private readonly CinemaDbContext _context;

        public UtentesController(CinemaDbContext context)
        {
            _context = context;
        }

        // GET: Utentes
        public async Task<IActionResult> Index()
        {
            return _context.Utente != null ?
                        View(await _context.Utente.ToListAsync()) :
                        Problem("Entity set 'CinemaDbContext.Utente'  is null.");
        }

        // GET: Utentes/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Utente == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // GET: Utentes/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Utente == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente.FindAsync(id);
            if (utente == null)
            {
                return NotFound();
            }
            return View(utente);
        }

        // POST: Utentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        // GET: Utentes/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Utente == null)
            {
                return NotFound();
            }

            var utente = await _context.Utente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utente == null)
            {
                return NotFound();
            }

            return View(utente);
        }

        // POST: Utentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            if (_context.Utente == null)
            {
                return Problem("Entity set 'CinemaDbContext.Utente'  is null.");
            }
            var utente = await _context.Utente.FindAsync(id);
            if (utente != null)
            {
                _context.Utente.Remove(utente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtenteExists(string id)
        {
            return (_context.Utente?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

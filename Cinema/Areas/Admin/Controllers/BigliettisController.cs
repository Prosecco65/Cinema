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
    public class BigliettisController : Controller
    {
        private readonly CinemaDbContext _context;

        public BigliettisController(CinemaDbContext context)
        {
            _context = context;
        }

        // GET: Bigliettis
        public async Task<IActionResult> Index()
        {
            var cinemaDbContext = _context.Biglietti.Include(b => b.Spettacolo).Include(b => b.Posti).Include(b => b.Utente);
            return View(await cinemaDbContext.ToListAsync());
        }

        // GET: Bigliettis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Biglietti == null)
            {
                return NotFound();
            }

            var biglietti = await _context.Biglietti
                .Include(b => b.Id_spettacolo)
                .Include(b => b.Posti)
                .Include(b => b.Utente)
                .FirstOrDefaultAsync(m => m.Id_biglietti == id);
            if (biglietti == null)
            {
                return NotFound();
            }

            return View(biglietti);
        }

        // GET: Bigliettis/Create
        public IActionResult Create()
        {
            ViewBag.Id_film = new SelectList(_context.Film, "Id_film", "Id_film");
            ViewBag.Id_posti = new SelectList(_context.Posti, "Id_posti", "Id_posti");
            ViewBag.Id = new SelectList(_context.Utente, "Id", "Id");
            return View();
        }

        // POST: Bigliettis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_biglietti,Id,Id_posti,Id_film")] Biglietti biglietti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(biglietti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_film"] = new SelectList(_context.Film, "Id_film", "Id_film", biglietti.Id_spettacolo);
            ViewData["Id_posti"] = new SelectList(_context.Posti, "Id_posti", "Id_posti", biglietti.Id_posti);
            ViewData["Id"] = new SelectList(_context.Utente, "Id", "Id", biglietti.Id);
            return View(biglietti);
        }

        // GET: Bigliettis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Biglietti == null)
            {
                return NotFound();
            }

            var biglietti = await _context.Biglietti.FindAsync(id);
            if (biglietti == null)
            {
                return NotFound();
            }
            ViewData["Id_film"] = new SelectList(_context.Film, "Id_film", "Id_film", biglietti.Id_spettacolo);
            ViewData["Id_posti"] = new SelectList(_context.Posti, "Id_posti", "Id_posti", biglietti.Id_posti);
            ViewData["Id"] = new SelectList(_context.Utente, "Id", "Id", biglietti.Id);
            return View(biglietti);
        }

        // POST: Bigliettis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_biglietti,Id,Id_posti,Id_film")] Biglietti biglietti)
        {
            if (id != biglietti.Id_biglietti)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(biglietti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BigliettiExists(biglietti.Id_biglietti))
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
            ViewData["Id_film"] = new SelectList(_context.Film, "Id_film", "Id_film", biglietti.Id_spettacolo);
            ViewData["Id_posti"] = new SelectList(_context.Posti, "Id_posti", "Id_posti", biglietti.Id_posti);
            ViewData["Id"] = new SelectList(_context.Utente, "Id", "Id", biglietti.Id);
            return View(biglietti);
        }

        // GET: Bigliettis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Biglietti == null)
            {
                return NotFound();
            }

            var biglietti = await _context.Biglietti
                .Include(b => b.Id_spettacolo)
                .Include(b => b.Posti)
                .Include(b => b.Utente)
                .FirstOrDefaultAsync(m => m.Id_biglietti == id);
            if (biglietti == null)
            {
                return NotFound();
            }

            return View(biglietti);
        }

        // POST: Bigliettis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Biglietti == null)
            {
                return Problem("Entity set 'CinemaDbContext.Biglietti'  is null.");
            }
            var biglietti = await _context.Biglietti.FindAsync(id);
            if (biglietti != null)
            {
                _context.Biglietti.Remove(biglietti);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BigliettiExists(int id)
        {
            return (_context.Biglietti?.Any(e => e.Id_biglietti == id)).GetValueOrDefault();
        }
    }
}

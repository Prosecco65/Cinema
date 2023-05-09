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
    public class SpettacoloesController : Controller
    {
        private readonly CinemaDbContext _context;

        public SpettacoloesController(CinemaDbContext context)
        {
            _context = context;
        }

        // GET: Spettacoloes
        public async Task<IActionResult> Index()
        {
            var cinemaDbContext = _context.Spettacolo.Include(s => s.Film).Include(s => s.Sala);
            return View(await cinemaDbContext.ToListAsync());
        }

        // GET: Spettacoloes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Spettacolo == null)
            {
                return NotFound();
            }

            var spettacolo = await _context.Spettacolo
                .Include(s => s.Film)
                .Include(s => s.Sala)
                .FirstOrDefaultAsync(m => m.Id_spettacolo == id);
            if (spettacolo == null)
            {
                return NotFound();
            }

            return View(spettacolo);
        }

        // GET: Spettacoloes/Create
        public IActionResult Create()
        {
            ViewData["Id_film"] = new SelectList(_context.Film, "Id_film", "Id_film");
            ViewData["Id_sala"] = new SelectList(_context.Sala, "Id_sala", "Id_sala");
            return View();
        }

        // POST: Spettacoloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_spettacolo,dataa,Id_sala,Id_film")] Spettacolo spettacolo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spettacolo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_film"] = new SelectList(_context.Film, "Id_film", "Id_film", spettacolo.Id_film);
            ViewData["Id_sala"] = new SelectList(_context.Sala, "Id_sala", "Id_sala", spettacolo.Id_sala);
            return View(spettacolo);
        }

        // GET: Spettacoloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Spettacolo == null)
            {
                return NotFound();
            }

            var spettacolo = await _context.Spettacolo.FindAsync(id);
            if (spettacolo == null)
            {
                return NotFound();
            }
            ViewData["Id_film"] = new SelectList(_context.Film, "Id_film", "Id_film", spettacolo.Id_film);
            ViewData["Id_sala"] = new SelectList(_context.Sala, "Id_sala", "Id_sala", spettacolo.Id_sala);
            return View(spettacolo);
        }

        // POST: Spettacoloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_spettacolo,dataa,Id_sala,Id_film")] Spettacolo spettacolo)
        {
            if (id != spettacolo.Id_spettacolo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spettacolo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpettacoloExists(spettacolo.Id_spettacolo))
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
            ViewData["Id_film"] = new SelectList(_context.Film, "Id_film", "Id_film", spettacolo.Id_film);
            ViewData["Id_sala"] = new SelectList(_context.Sala, "Id_sala", "Id_sala", spettacolo.Id_sala);
            return View(spettacolo);
        }

        // GET: Spettacoloes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Spettacolo == null)
            {
                return NotFound();
            }

            var spettacolo = await _context.Spettacolo
                .Include(s => s.Film)
                .Include(s => s.Sala)
                .FirstOrDefaultAsync(m => m.Id_spettacolo == id);
            if (spettacolo == null)
            {
                return NotFound();
            }

            return View(spettacolo);
        }

        // POST: Spettacoloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Spettacolo == null)
            {
                return Problem("Entity set 'CinemaDbContext.Spettacolo'  is null.");
            }
            var spettacolo = await _context.Spettacolo.FindAsync(id);
            if (spettacolo != null)
            {
                _context.Spettacolo.Remove(spettacolo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpettacoloExists(int id)
        {
            return (_context.Spettacolo?.Any(e => e.Id_spettacolo == id)).GetValueOrDefault();
        }
    }
}

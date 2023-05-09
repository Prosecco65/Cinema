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

namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ValutasController : Controller
    {
        private readonly CinemaDbContext _context;

        public ValutasController(CinemaDbContext context)
        {
            _context = context;
        }

        // GET: Valutas
        public async Task<IActionResult> Index()
        {
            var cinemaDbContext = _context.Valuta.Include(v => v.Film).Include(v => v.Utente);
            return View(await cinemaDbContext.ToListAsync());
        }

        // GET: Valutas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Valuta == null)
            {
                return NotFound();
            }

            var valuta = await _context.Valuta
                .Include(v => v.Film)
                .Include(v => v.Utente)
                .FirstOrDefaultAsync(m => m.Id_valutazione == id);
            if (valuta == null)
            {
                return NotFound();
            }

            return View(valuta);
        }

        // GET: Valutas/Create
        public IActionResult Create()
        {
            ViewData["Id_film"] = new SelectList(_context.Film, "Id_Film", "Id_Film");
            ViewData["Id"] = new SelectList(_context.Utente, "Id", "Id");
            return View();
        }

        // POST: Valutas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_valutazione,valutazione,Id_film,Id")] Valuta valuta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(valuta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_film"] = new SelectList(_context.Film, "Id_film", "Id_film", valuta.Id_film);
            ViewData["Id"] = new SelectList(_context.Utente, "Id", "Id", valuta.Id);
            return View(valuta);
        }

        // GET: Valutas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Valuta == null)
            {
                return NotFound();
            }

            var valuta = await _context.Valuta.FindAsync(id);
            if (valuta == null)
            {
                return NotFound();
            }
            ViewData["Id_film"] = new SelectList(_context.Film, "Id_Film", "Id_Film", valuta.Id_film);
            ViewData["Id"] = new SelectList(_context.Utente, "Id", "Id", valuta.Id);
            return View(valuta);
        }

        // POST: Valutas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_valutazione,valutazione,Id_film,Id")] Valuta valuta)
        {
            if (id != valuta.Id_valutazione)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valuta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValutaExists(valuta.Id_valutazione))
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
            ViewData["Id_film"] = new SelectList(_context.Film, "Id_Film", "Id_Film", valuta.Id_film);
            ViewData["Id"] = new SelectList(_context.Utente, "Id", "Id", valuta.Id);
            return View(valuta);
        }

        // GET: Valutas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Valuta == null)
            {
                return NotFound();
            }

            var valuta = await _context.Valuta
                .Include(v => v.Film)
                .Include(v => v.Utente)
                .FirstOrDefaultAsync(m => m.Id_valutazione == id);
            if (valuta == null)
            {
                return NotFound();
            }

            return View(valuta);
        }

        // POST: Valutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Valuta == null)
            {
                return Problem("Entity set 'CinemaDbContext.Valuta'  is null.");
            }
            var valuta = await _context.Valuta.FindAsync(id);
            if (valuta != null)
            {
                _context.Valuta.Remove(valuta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValutaExists(int id)
        {
            return (_context.Valuta?.Any(e => e.Id_valutazione == id)).GetValueOrDefault();
        }
    }
}

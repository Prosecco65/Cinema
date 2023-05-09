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
    public class PostisController : Controller
    {
        private readonly CinemaDbContext _context;

        public PostisController(CinemaDbContext context)
        {
            _context = context;
        }

        // GET: Postis
        public async Task<IActionResult> Index()
        {
            var cinemaDbContext = _context.Posti.Include(p => p.Sala);
            return View(await cinemaDbContext.ToListAsync());
        }

        // GET: Postis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posti == null)
            {
                return NotFound();
            }

            var posti = await _context.Posti
                .Include(p => p.Sala)
                .FirstOrDefaultAsync(m => m.Id_posti == id);
            if (posti == null)
            {
                return NotFound();
            }

            return View(posti);
        }

        // GET: Postis/Create
        public IActionResult Create()
        {
            ViewData["Id_sala"] = new SelectList(_context.Sala, "Id_sala", "Id_sala");
            return View();
        }

        // POST: Postis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_posti,costo,Id_sala")] Posti posti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(posti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_sala"] = new SelectList(_context.Sala, "Id_sala", "Id_sala", posti.Id_sala);
            return View(posti);
        }

        // GET: Postis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posti == null)
            {
                return NotFound();
            }

            var posti = await _context.Posti.FindAsync(id);
            if (posti == null)
            {
                return NotFound();
            }
            ViewData["Id_sala"] = new SelectList(_context.Sala, "Id_sala", "Id_sala", posti.Id_sala);
            return View(posti);
        }

        // POST: Postis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_posti,costo,Id_sala")] Posti posti)
        {
            if (id != posti.Id_posti)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostiExists(posti.Id_posti))
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
            ViewData["Id_sala"] = new SelectList(_context.Sala, "Id_sala", "Id_sala", posti.Id_sala);
            return View(posti);
        }

        // GET: Postis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posti == null)
            {
                return NotFound();
            }

            var posti = await _context.Posti
                .Include(p => p.Sala)
                .FirstOrDefaultAsync(m => m.Id_posti == id);
            if (posti == null)
            {
                return NotFound();
            }

            return View(posti);
        }

        // POST: Postis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Posti == null)
            {
                return Problem("Entity set 'CinemaDbContext.Posti'  is null.");
            }
            var posti = await _context.Posti.FindAsync(id);
            if (posti != null)
            {
                _context.Posti.Remove(posti);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostiExists(int id)
        {
            return (_context.Posti?.Any(e => e.Id_posti == id)).GetValueOrDefault();
        }
    }
}

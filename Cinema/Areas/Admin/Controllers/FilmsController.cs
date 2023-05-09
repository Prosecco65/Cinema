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
using System.Drawing.Drawing2D;
using Microsoft.Extensions.Hosting;

namespace Cinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]


    public class FilmsController : Controller
    {
        private readonly CinemaDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public FilmsController(CinemaDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Films
        public async Task<IActionResult> Index()
        {
            return _context.Film != null ?
                        View(await _context.Film.ToListAsync()) :
                        Problem("Entity set 'CinemaDbContext.Film'  is null.");
        }

        //#region API CALLS
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var productList = _context.Film.ToList();

        //    return Json(new { data = productList });
        //}

        //#endregion

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Film == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .FirstOrDefaultAsync(m => m.Id_film == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        //GET
        public IActionResult Upsert(int? id)
        {
            Film product = new();

            if (id == null || id == 0)
            {
                //create product
                return View(product);

            }
            else
            {
                //update product
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Film obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;

                if (file != null)
                {

                    //creiamo un nuovo nome per il file che l'utente ha caricato

                    //facciamo in modo che non possano esistere due file con lo stesso nome

                    string fileName = Guid.NewGuid().ToString();

                    var uploadDir = Path.Combine(wwwRootPath, "images", "film");

                    var fileExtension = Path.GetExtension(file.FileName);

                    var filePath = Path.Combine(uploadDir, fileName + fileExtension);

                    var fileUrlString = filePath[wwwRootPath.Length..].Replace(@"\\", @"\");

                    using (var fileStream = new FileStream(filePath, FileMode.Create))

                    {

                        file.CopyTo(fileStream);

                    }

                    obj.copertina = fileUrlString;

                }
                if (obj.Id_film is 0)
                {
                    _context.Film.Add(obj);
                    TempData["success"] = "Ho creato il film";
                }
                else
                {
                    _context.Film.Update(obj);
                    TempData["success"] = "Ho modificato il film";
                }

                _context.SaveChanges();

                TempData["success"] = "Product updated successfully";

                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Film == null)
            {
                return NotFound();
            }

            var film = await _context.Film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_film,titolo,genere,descrizione,durata,copertina")] Film film)
        {
            if (id != film.Id_film)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id_film))
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
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Film == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .FirstOrDefaultAsync(m => m.Id_film == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Film == null)
            {
                return Problem("Entity set 'CinemaDbContext.Film'  is null.");
            }
            var film = await _context.Film.FindAsync(id);
            if (film != null)
            {
                _context.Film.Remove(film);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return (_context.Film?.Any(e => e.Id_film == id)).GetValueOrDefault();
        }
    }
}

using Cinema.DataAccess;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Cinema.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CinemaDbContext _db;

        public HomeController(CinemaDbContext db)
        {
            _db = db;
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _db.Film.ToList();

            return Json(new { data = productList });
        }

        #endregion
        public IActionResult Index()
        {
            var sas = _db.Film.ToList();
            return View(sas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
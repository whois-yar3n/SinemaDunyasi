using SinemaDunyasi.Data;
using SinemaDunyasi.Models;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
namespace SinemaDunyasi.Controllers
{
    public class FilmController : Controller
    {
        private readonly AppDbContext _context;
        public FilmController(AppDbContext context)
        {
            _context = context;
        }
        // Listeleme
        public IActionResult Index(string sortOrder, string currentFilter, string searchString, int?
        page)
        {
            ViewData["CurrentSort"] = sortOrder;
            // Sıralama döngüsü: Varsayılan (ID) -> Artan -> Azalan -> Varsayılan
            ViewData["PuanSortParm"] = sortOrder == "puan_asc" ? "puan_desc" : (sortOrder ==
            "puan_desc" ? "" : "puan_asc");
            ViewData["YilSortParm"] = sortOrder == "yil_asc" ? "yil_desc" : (sortOrder == "yil_desc"
            ? "" : "yil_asc");
            if (searchString != null) { page = 1; }
            else { searchString = currentFilter; }
            ViewData["CurrentFilter"] = searchString;
            int pageNumber = page ?? 1;
            int pageSize = 6;
            var filmler = from f in _context.Filmler select f;
            if (!string.IsNullOrEmpty(searchString))
            {
                filmler = filmler.Where(f => f.Ad.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "puan_desc": filmler = filmler.OrderByDescending(f => f.Puan); break;
                case "puan_asc": filmler = filmler.OrderBy(f => f.Puan); break;
                case "yil_asc": filmler = filmler.OrderBy(f => f.Yil); break;
                case "yil_desc": filmler = filmler.OrderByDescending(f => f.Yil); break;
                default: filmler = filmler.OrderByDescending(f => f.Id); break;
            }
            return View(filmler.ToPagedList(pageNumber, pageSize));
        }
        public IActionResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var film = _context.Filmler.Find(id);
            if (film != null) { _context.Filmler.Remove(film); _context.SaveChanges(); }
            return RedirectToAction(nameof(Index));
        }
    }
}

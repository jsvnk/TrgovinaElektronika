using Microsoft.AspNetCore.Mvc;
using TrgovinaElektronika.Models;

namespace TrgovinaElektronika.Controllers
{
    public class IzdelekController : Controller
    {
        // Simulacija baze
        private static List<Izdelek> izdelki = new List<Izdelek>
        {
            new Izdelek { Id = 1, Naziv = "Prenosnik", Opis = "HP EliteBook 840", Cena = 1199.99m, Zaloga = 5 },
            new Izdelek { Id = 2, Naziv = "Miška", Opis = "Logitech G502", Cena = 59.99m, Zaloga = 10 },
            new Izdelek { Id = 3, Naziv = "Monitor", Opis = "Samsung 27\"", Cena = 229.99m, Zaloga = 3 }
        };

        public IActionResult Index()
        {
            return View(izdelki);
        }
        public static List<Izdelek> GetAll()
        {
            return izdelki;
        }
        public IActionResult Dodaj()
        {
            if (!JeAdmin())
                return Unauthorized();

            return View();
        }

        [HttpPost]
        public IActionResult Dodaj(Izdelek izdelek)
        {
            izdelek.Id = izdelki.Max(i => i.Id) + 1;
            izdelki.Add(izdelek);
            return RedirectToAction("Index");
        }

        public IActionResult Uredi(int id)
        {
            if (!JeAdmin())
                return Unauthorized();

            var izdelek = izdelki.FirstOrDefault(i => i.Id == id);
            if (izdelek == null) return NotFound();
            return View(izdelek);
        }

        [HttpPost]
        public IActionResult Uredi(Izdelek posodobljen)
        {
            if (!JeAdmin())
                return Unauthorized();

            var i = izdelki.FirstOrDefault(x => x.Id == posodobljen.Id);
            if (i == null) return NotFound();

            i.Naziv = posodobljen.Naziv;
            i.Opis = posodobljen.Opis;
            i.Cena = posodobljen.Cena;
            i.Zaloga = posodobljen.Zaloga;

            return RedirectToAction("Index");
        }

        public IActionResult Izbrisi(int id)
        {
            var i = izdelki.FirstOrDefault(x => x.Id == id);
            if (i != null) izdelki.Remove(i);
            return RedirectToAction("Index");
        }
        private bool JeAdmin()
        {
            return HttpContext.Session.GetString("Uporabnik") == "Admin";
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using TrgovinaElektronika.Models;

namespace TrgovinaElektronika.Controllers
{
    public class KosaricaController : Controller
    {
        private static List<PostavkaKosarice> kosarica = new List<PostavkaKosarice>();
        private static List<Narocilo> narocila = new List<Narocilo>();
        private static int zaporednaStevilka = 1;
        public static List<Narocilo> Narocila { get; set; } = new List<Narocilo>();

        public IActionResult Index()
        {
            return View(kosarica);
        }

        public IActionResult Dodaj(int id)
        {
            var izdelek = IzdelekController.GetAll().FirstOrDefault(i => i.Id == id);
            if (izdelek == null) return NotFound();

            var obstaja = kosarica.FirstOrDefault(p => p.IzdelekId == izdelek.Id);
            if (obstaja != null)
                obstaja.Kolicina++;
            else
                kosarica.Add(new PostavkaKosarice
                {
                    IzdelekId = izdelek.Id,
                    Naziv = izdelek.Naziv,
                    Cena = izdelek.Cena,
                    Kolicina = 1
                });

            return RedirectToAction("Index");
        }

        public IActionResult Izprazni()
        {
            kosarica.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Oddaj()
        {
            var ime = HttpContext.Session.GetString("Uporabnik");
            if (string.IsNullOrEmpty(ime))
                return RedirectToAction("Prijava", "Uporabnik");

            if (!kosarica.Any())
                return RedirectToAction("Index");

            var novo = new Narocilo
            {
                Id = zaporednaStevilka++,
                UporabnikIme = ime,
                Datum = DateTime.Now,
                Postavke = kosarica.Select(p => new PostavkaKosarice
                {
                    IzdelekId = p.IzdelekId,
                    Naziv = p.Naziv,
                    Cena = p.Cena,
                    Kolicina = p.Kolicina
                }).ToList()
            };

            narocila.Add(novo);
            Narocila.Add(novo);
            kosarica.Clear();

            return RedirectToAction("Pregled", new { id = novo.Id });
        }

        public IActionResult Pregled(int id)
        {
            var nar = narocila.FirstOrDefault(n => n.Id == id);
            if (nar == null)
                return NotFound();

            return View(nar);
        }
        public IActionResult MojaNarocila()
        {
            var ime = HttpContext.Session.GetString("Uporabnik");
            if (string.IsNullOrEmpty(ime))
                return RedirectToAction("Prijava", "Uporabnik");

            var moja = narocila.Where(n => n.UporabnikIme == ime).ToList();
            return View(moja);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using TrgovinaElektronika.Models;
using Microsoft.AspNetCore.Http;

namespace TrgovinaElektronika.Controllers
{
    public class UporabnikController : Controller
    {
        private static List<Uporabnik> uporabniki = new List<Uporabnik>
        {
            new Uporabnik { Id = 1, Email = "test@test.com", Geslo = "1234", Ime = "Test", Priimek = "Uporabnik" },
            new Uporabnik { Id = 2, Email = "admin@admin.com", Geslo = "admin", Ime = "Admin", Priimek = "Administrator" }
        };


        public IActionResult Prijava()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prijava(string email, string geslo)
        {
            var u = uporabniki.FirstOrDefault(x => x.Email == email && x.Geslo == geslo);
            if (u != null)
            {
                HttpContext.Session.SetString("Uporabnik", u.Ime);
                return RedirectToAction("Index", "Izdelek");
            }

            ViewBag.Napaka = "Napačen e-mail ali geslo.";
            return View();
        }

        public IActionResult Odjava()
        {
            HttpContext.Session.Remove("Uporabnik");
            return RedirectToAction("Prijava");
        }
        public IActionResult Registracija()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registracija(Uporabnik nov)
        {
            if (uporabniki.Any(u => u.Email == nov.Email))
            {
                ViewBag.Napaka = "Uporabnik s tem emailom že obstaja.";
                return View();
            }

            nov.Id = uporabniki.Count + 1;
            uporabniki.Add(nov);

            // samodejna prijava
            HttpContext.Session.SetString("Uporabnik", nov.Ime);

            return RedirectToAction("Index", "Izdelek");
        }

    }
}

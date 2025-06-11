using Microsoft.AspNetCore.Mvc;
using TrgovinaElektronika.Models;

namespace TrgovinaElektronika.Controllers
{
    public class NarociloController : Controller
    {
        // uporabi seznam iz KosaricaController – tukaj ga simuliramo
        private static List<Narocilo> narocila = new List<Narocilo>();

        public static void DodajNarocilo(Narocilo n)
        {
            narocila.Add(n);
        }

        public IActionResult Index()
        {
            var uporabnik = HttpContext.Session.GetString("Uporabnik");

            if (string.IsNullOrEmpty(uporabnik))
                return RedirectToAction("Prijava", "Uporabnik");

            var vsi = KosaricaController.Narocila;

            if (uporabnik == "Admin")
                return View(vsi);

            var moja = vsi.Where(n => n.UporabnikIme == uporabnik).ToList();
            return View(moja);
        }


        public IActionResult Podrobnosti(int id)
        {
            var uporabnik = HttpContext.Session.GetString("Uporabnik");
            if (string.IsNullOrEmpty(uporabnik))
                return RedirectToAction("Prijava", "Uporabnik");

            var narocilo = KosaricaController.Narocila.FirstOrDefault(n => n.Id == id);
            if (narocilo == null)
                return NotFound();

            if (uporabnik != "Admin" && narocilo.UporabnikIme != uporabnik)
                return Unauthorized();

            return View(narocilo);
        }

    }
}

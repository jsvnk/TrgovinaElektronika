using Microsoft.AspNetCore.Mvc;

namespace TrgovinaElektronika.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Uporabnik") != "Admin")
                return Unauthorized();

            return View();
        }
    }
}

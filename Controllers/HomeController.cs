﻿using Microsoft.AspNetCore.Mvc;

namespace TrgovinaElektronika.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

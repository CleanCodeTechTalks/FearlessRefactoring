using FoodTruckMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace FoodTruckMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(DateTime? startDate = null, DateTime? endDate = null)
        {

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

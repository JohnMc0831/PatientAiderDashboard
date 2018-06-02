using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientAiderDashboard.Models;
using PatientAiderDashboard.Repositories;

namespace PatientAiderDashboard.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITopicRepository db;
        public HomeController(ITopicRepository context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.GetEncounters());
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult EditTopic(int id)
        {
            return View("EditTopic", db.GetTopicById(id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

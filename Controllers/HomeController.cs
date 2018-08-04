using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using RandomPasscode.Models;

namespace RandomPasscode.Controllers
{
    public class HomeController : Controller
    {
        public string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        [HttpGet("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Counter") == null)
            {
                HttpContext.Session.SetInt32("Counter", 1);
                int? IntVariable = HttpContext.Session.GetInt32("Counter");
                string OurString = RandomString(14);
                ViewBag.RandomString = OurString;
                return View(IntVariable); 
            }
            else
            {
                int? IntVariable = HttpContext.Session.GetInt32("Counter");
                string OurString = HttpContext.Session.GetString("RandomString");
                ViewBag.RandomString = OurString;
                return View(IntVariable);
            }
        }

        [HttpGet("generate")]
        public IActionResult Generate()
        {
            ////Get increment and set our counter value to session
            int? IntVariable = HttpContext.Session.GetInt32("Counter");
            int counter = IntVariable.GetValueOrDefault() +1;
            HttpContext.Session.SetInt32("Counter", counter);
            ////Generate and set our RandomString value to session
            string ourString = RandomString(14);
            HttpContext.Session.SetString("RandomString", ourString);
            return RedirectToAction("Index");
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
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

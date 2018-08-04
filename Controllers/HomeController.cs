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
        [HttpGet("")]
        public IActionResult Index()
        {
            ////if our counter is null instantiate it at one and generate a random string
            if (HttpContext.Session.GetInt32("Counter") == null)
            {
                HttpContext.Session.SetInt32("Counter", 1);
                int? IntVariable = HttpContext.Session.GetInt32("Counter");
                HttpContext.Session.SetString("RandomString", RandomString(14));
                return View(); 
            }
            else
            {
                ///if counter exists just return us to view so we can display the updated randomstring and counter values
                return View();
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
            HttpContext.Session.SetString("RandomString", RandomString(14));
            return RedirectToAction("Index");
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            ////Generate a random string of alphanumeric characters of length Length
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

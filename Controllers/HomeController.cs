using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CST465_Armadillo.Controllers
{

    public class HomeController : Controller
    {
        private ArmadilloSettings _Settings;
        public HomeController(IOptionsSnapshot<ArmadilloSettings> settings)
        {
            _Settings = settings.Value;
        }
        public IActionResult Index()
        {

            //ViewData["MaxAnimals"] = _Settings.MaxAnimals;
            ViewData["Settings"] = _Settings;
            //IConfigurationBuilder builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("Candy.json");

            //var configuration = builder.Build();
            //List<Candy> candies = configuration.GetSection("Candy").Get<List<Candy>>();
            //return View(candies);
            return View();
        }
        
        public IActionResult OtherStuff()
        {
            return View();
        }
    }
}
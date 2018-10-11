using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArmadilloLib;

namespace CST465_Armadillo.Controllers
{
    
    public class ArmadilloController : Controller
    {
        
        public IActionResult Index()
        {
            ArmadilloFarm farm = new ArmadilloFarm();
            farm.FarmAnimals.Add(new Armadillo { Name = "Bob", Age = 5, IsPainted = false});
            farm.FarmAnimals.Add(new Armadillo { Name = "Roger", Age = 7, IsPainted = true });
            return View(farm);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Armadillo model)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                //code block
            }
            try
            {
                SqlConnection connection = new SqlConnection();
                {
                    //code block
                }
            }
            finally
            {
                if(connection != null)
                {
                    connection.Dispose();
                }
            }
                return View("ArmadilloCreated", model);
        }
        public IActionResult Painted()
        {
            ViewBag.Title = "Armadilloz!";
            dynamic myDyn = new { Hello = "Hootie" };
            ViewBag.MyDogSkip = 5;
            ViewBag.Whatever = "You got it";

            ArmadilloFarm farm = new ArmadilloFarm();
            farm.FarmAnimals.Add(new Armadillo { Name = "Bob", Age = 5, IsPainted = false });
            farm.FarmAnimals.Add(new Armadillo { Name = "Roger", Age = 7, IsPainted = true });
            return View(farm.FarmAnimals.Where(a => a.IsPainted));
        }
    }
}
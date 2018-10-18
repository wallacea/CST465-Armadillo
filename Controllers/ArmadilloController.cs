using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArmadilloLib;
using CST465_Armadillo.Models;

namespace CST465_Armadillo.Controllers
{
    
    public class ArmadilloController : Controller
    {
        //[Route("/Armadillo/{name}")]
        
        public IActionResult Index(string name="")
        {
            ArmadilloFarm farm = new ArmadilloFarm();
            if (!string.IsNullOrEmpty(name))
            {
                
                farm.FarmAnimals.Add(new Armadillo { Name = "Idid", Age = 5, IsPainted = false });
                farm.FarmAnimals.Add(new Armadillo { Name = "Ididnt", Age = 7, IsPainted = true });
            }
            else
            {
                
                farm.FarmAnimals.Add(new Armadillo { Name = "Bob", Age = 5, IsPainted = false });
                farm.FarmAnimals.Add(new Armadillo { Name = "Roger", Age = 7, IsPainted = true });
            }
            return View(farm);
        }
        
        [HttpGet]
        public IActionResult HTMLCreate()
        {
            return View();
        }
        [HttpGet]
        public IActionResult HTMLHelperCreate()
        {
            return View();
        }
        [HttpGet]
        public IActionResult HTMLHelperForCreate()
        {
            return View();
        }
        [HttpGet]
        public IActionResult TagHelperCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult HTMLCreate(ArmadilloModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return Create(model);
        }
        [HttpPost]
        public IActionResult HTMLHelperCreate(ArmadilloModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return Create(model);
        }
        [HttpPost]
        public IActionResult HTMLHelperForCreate(ArmadilloModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return Create(model);
        }
        [HttpPost]
        public IActionResult TagHelperCreate(ArmadilloModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            return Create(model);
        }
        protected IActionResult Create(ArmadilloModel model)
        {
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
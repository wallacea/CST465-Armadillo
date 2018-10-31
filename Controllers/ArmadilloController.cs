using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ArmadilloLib;
using CST465_Armadillo.Models;
using CST465_Armadillo.Repositories;
using Microsoft.Extensions.Configuration;
using System.IO;
using CST465_Armadillo.ExtensionMethods;
using Microsoft.Extensions.Options;

namespace CST465_Armadillo.Controllers
{

    public class ArmadilloController : Controller
    {
        private IArmadilloRepository _ArmadilloRepo;
        private ArmadilloFarm _Farm;
        private FarmSettings _Settings;
        public ArmadilloController(IOptionsSnapshot<FarmSettings> settings)
        {
            _Settings = settings.Value;
            //IConfigurationBuilder builder = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())

            // .AddJsonFile("farmsettings.json", optional: false, reloadOnChange: true)
            // .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            //var configuration = builder.Build();
            _ArmadilloRepo = new ArmadilloDBRepository();
            _Farm = new ArmadilloFarm();
            _Farm.FarmAnimals.AddRange(_ArmadilloRepo.GetList());
        }
        //[Route("/Armadillo/{name}")]
        //public IActionResult Index(string name="")
        public IActionResult Index()
        {   
            //FarmSettings farmSettings = new FarmSettings();
            //configuration.Bind(farmSettings);
//            FarmSettings farmSettings = configuration.Get<FarmSettings>();
            ViewBag.FarmSettings = _Settings;
            //ViewData["FarmSettings"] = _Settings;
            return View(_Farm);
        }
        public PartialViewResult FeaturedArmadillo()
        {

            return PartialView("_FeaturedArmadillo", _Farm.FeaturedArmadillo);
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
            //ArmadilloModel model = new ArmadilloModel();
            //model.PossibleHomelands =
            return View(new ArmadilloModel());
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
        [ValidateAntiForgeryToken]
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
            Armadillo armadillo = new Armadillo();
            armadillo.Name = model.Name;
            armadillo.Age = model.Age;
            armadillo.ShellHardness = model.ShellHardness ?? 0;
            armadillo.IsPainted = model.IsPainted;
            armadillo.Homeland = model.Homeland;
            _ArmadilloRepo.Save(armadillo);

            return RedirectToAction("Index", model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var armadillo = _ArmadilloRepo.Get(id);
            ArmadilloModel model = new ArmadilloModel()
            {
                ID = armadillo.ID,
                Name = armadillo.Name,
                Age = armadillo.Age,
                ShellHardness = armadillo.ShellHardness,
                IsPainted = armadillo.IsPainted,
                Homeland = armadillo.Homeland

            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ArmadilloModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Armadillo armadillo = new Armadillo();
            armadillo.ID = model.ID;
            armadillo.Name = model.Name;
            armadillo.Age = model.Age;
            armadillo.ShellHardness = model.ShellHardness ?? 0;
            armadillo.IsPainted = model.IsPainted;
            armadillo.Homeland = model.Homeland;
            _ArmadilloRepo.Save(armadillo);
            return RedirectToAction("Index", model);
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
        [HttpGet]
        public IActionResult BulkEdit()
        {
            List<ArmadilloModel> armadillos = new List<ArmadilloModel>();

            _Farm.FarmAnimals.ForEach(animal =>
            {
                armadillos.Add(animal.GetArmadilloModel());
            });
            //_Farm.FarmAnimals.ForEach(arm =>
            //armadillos.Add(new ArmadilloModel()
            //{
            //    ID = arm.ID,
            //    Name = arm.Name,
            //    Age = arm.Age,
            //    ShellHardness = arm.ShellHardness,
            //    Homeland = arm.Homeland,
            //    IsPainted = arm.IsPainted
            //}
            //));
            
            //armadillos.Add(new ArmadilloModel());
            

            return View(armadillos);
        }
        [HttpPost]
        public IActionResult BulkEdit(List<ArmadilloModel> model)//ArmadilloFarmModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            foreach (var armadillo in model)
            {
                _ArmadilloRepo.Save(armadillo.GetArmadilloObject());
            }
            //throw new Exception(model.Count.ToString());
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _ArmadilloRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
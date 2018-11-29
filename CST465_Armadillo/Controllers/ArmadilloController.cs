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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;

namespace CST465_Armadillo.Controllers
{

    public class ArmadilloController : Controller
    {
        private IArmadilloRepository _ArmadilloRepo;
        private ArmadilloFarm _Farm;
        private FarmSettings _Settings;
        private IMemoryCache _Cache;
        public ArmadilloController(IMemoryCache cache,IOptionsSnapshot<FarmSettings> settings, IArmadilloRepository armadilloRepo)
        {
            _Settings = settings.Value;
            _Cache = cache;
            //IConfigurationBuilder builder = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())

            // .AddJsonFile("farmsettings.json", optional: false, reloadOnChange: true)
            // .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            //var configuration = builder.Build();
            //_ArmadilloRepo = new ArmadilloDBRepository();
            _ArmadilloRepo = armadilloRepo;
            
            
        }
        //[Route("/Armadillo/{name}")]
        //public IActionResult Index(string name="")
        public async Task<IActionResult> Index()
        {
            _Farm = new ArmadilloFarm();
            _Farm.FarmAnimals.AddRange(await _ArmadilloRepo.GetList());
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
            ArmadilloModel model = new ArmadilloModel();
            model.OtherPossibleHomelands = new List<Homeland>
            {
                new Homeland(){ID=1, Name="United States of America"},
                new Homeland(){ID=2, Name="Tanzania"},
                new Homeland(){ID=3, Name="Mexico"}
            };
            return View(model);
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
            Armadillo armadillo = model.GetArmadilloObject();
            _ArmadilloRepo.Save(armadillo);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var armadillo = _ArmadilloRepo.Get(id);
            ArmadilloModel model = armadillo.GetArmadilloModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ArmadilloModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Armadillo armadillo = model.GetArmadilloObject();

            _ArmadilloRepo.Save(armadillo);
            return RedirectToAction("Index");
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
        [HttpGet]
        public IActionResult AddInstructions()
        {
            List<string> instructions = (List<string>)_Cache.Get("InstructionList");
            if(instructions == null)
            {
                instructions = new List<string>();
            }
            return View(instructions);
        }
        [HttpPost]
        public IActionResult SaveInstruction(string instruction)
        {
            List<string> instructions = (List<string>) _Cache.Get("InstructionList");
            if (instructions == null)
            {
                instructions = new List<string>();
            }
            instructions.Add(instruction);
            _Cache.Set("InstructionList", instructions);
            return RedirectToAction("AddInstructions");
        }
        [HttpPost]
        public IActionResult AddInstructions (List<string> instructions)
        {
            _Cache.Set("InstructionList",instructions);
            return RedirectToAction("AddInstructions");
        }
        public async Task<IActionResult> Search(string searchText)
        {
            List<Armadillo> searchResults = new List<Armadillo>();
            if (!string.IsNullOrEmpty(searchText))
            {
                searchResults = await _ArmadilloRepo.SearchList(searchText);
            }
            return View(searchResults);
        }
        public IActionResult View(int id)
        {
            Armadillo a = _ArmadilloRepo.Get(id);
            return View(a.GetArmadilloModel());
        }
        public IActionResult Settings()
        {
            //throw new Exception("An error occurred");
            //FarmSettings farmSettings = new FarmSettings();
            //configuration.Bind(farmSettings);
            //            FarmSettings farmSettings = configuration.Get<FarmSettings>();
            ViewBag.FarmSettings = _Settings;
            //ViewData["FarmSettings"] = _Settings;
            return View();
        }
        public IActionResult RefreshingList()
        {
            return View();
        }
    }
}
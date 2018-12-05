using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CST465_Armadillo.Models;
using CST465_Armadillo.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CST465_Armadillo.Controllers
{
    public class MultiAddController : Controller
    {
        IStudentRepository _StudentRepo;
        public MultiAddController(IStudentRepository studentRepo)
        {
            _StudentRepo = studentRepo;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            
            return View(_StudentRepo.GetList());
        }
        [HttpGet]
        public IActionResult AddEditStudent(string id)
        {
            Student student = null;
            if(!string.IsNullOrEmpty(id))
            {
                student = _StudentRepo.GetList().FirstOrDefault(s => s.Name == id);
            }
            if(student == null)
            {
                student = new Student();
            }
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEditStudent(Student model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            _StudentRepo.Save(model);
            return RedirectToAction("Index");
        }
    }
}

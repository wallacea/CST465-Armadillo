using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CST465_Armadillo.Controllers
{
    
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _RoleManager;
        private UserManager<IdentityUser> _UserManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {

            _RoleManager = roleManager;
            _UserManager = userManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRole(string RoleName)
        {
            IdentityRole role = new IdentityRole();
            role.Name = RoleName;
            IdentityResult result = _RoleManager.CreateAsync(role).Result;
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUserToRole(string Email, string RoleName)
        {
            IdentityUser user = _UserManager.FindByEmailAsync(Email).Result;
            IdentityResult result = _UserManager.AddToRoleAsync(user, RoleName).Result;
            //Check the status of the result
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(e => e.Description).Aggregate((a, b) => a + "," + b));

            }
            return RedirectToAction("Index");
        }
    }
}
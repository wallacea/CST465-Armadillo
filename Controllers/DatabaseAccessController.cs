using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CST465_Armadillo.Controllers
{
    public class DatabaseAccessController : Controller
    {
        private IConfiguration _Configuration;
        public DatabaseAccessController(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            string connection = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(_Configuration, "DefaultConnection");
            return View((object)connection);
        }
    }
}

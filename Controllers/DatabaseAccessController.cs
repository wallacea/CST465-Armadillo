using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CST465_Armadillo.Controllers
{
    public class DatabaseAccessController : Controller
    {
        private IConfiguration _Configuration;
        private FarmSettings _Settings;
        public DatabaseAccessController(IConfiguration configuration, IOptions<FarmSettings> settings)
        {
            _Configuration = configuration;
            _Settings = settings.Value;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            //string connection = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(_Configuration, "DefaultConnection");
            var settings = new FarmSettings();
            _Configuration.Bind("FarmSettings", settings);
            //string connection = _Settings.MaxAnimals.ToString();
            return View((object)settings.MaxAnimals.ToString());
        }
    }
}

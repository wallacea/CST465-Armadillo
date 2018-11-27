using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CST465_Armadillo.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CST465_Armadillo.Controllers
{
    public class CacheController : Controller
    {
        private ICacheRepository _CacheRepository;
        public CacheController(ICacheRepository cacheRepository)
        {
            _CacheRepository = cacheRepository;
        }
        // GET: /<controller>
        //[ResponseCache(Duration = 5)]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index()
        {
            ViewBag.CacheString = _CacheRepository.GetRandomNumber();
            return View();
        }
    }
}

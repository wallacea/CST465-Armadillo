using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadilloLib;
using CST465_Armadillo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CST465_Armadillo.WebServices
{
    [Route("/api/armadillo")]
    public class ArmadilloAPIController : ControllerBase
    {
        private IArmadilloRepository _Repository;
        
        public ArmadilloAPIController(IArmadilloRepository repo)
        {
            _Repository = repo;
        }
        public ActionResult<List<Armadillo>> GetAll()
        {
            return _Repository.GetList().Result;
        }
        [HttpGet("{id}")]
        public ActionResult<Armadillo> GetById(int id)
        {
            return _Repository.Get(id);
        }
    }
}
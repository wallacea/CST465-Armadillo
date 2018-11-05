using System;
using System.Collections.Generic;
using System.Linq;

namespace ArmadilloLib
{
    public class ArmadilloFarm
    {
        public Armadillo FeaturedArmadillo{
            get
            {
                Random r = new Random();

                var randomArmadillo = _Armadillos.Skip(r.Next(0, _Armadillos.Count)).Take(1).FirstOrDefault();
                return randomArmadillo;
            }
        }
        public List<Armadillo> FarmAnimals {get{return _Armadillos;}}
        private List<Armadillo> _Armadillos = new List<Armadillo>();
    }
}

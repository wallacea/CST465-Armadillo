using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadilloLib;
using CST465_Armadillo.Models;

namespace CST465_Armadillo.ExtensionMethods
{
    public static class ArmadilloExtensions
    {
        public static Armadillo GetArmadilloObject(this ArmadilloModel model)
        {
            Armadillo armadillo = new Armadillo();
            
            return armadillo;
        }
        public static ArmadilloModel GetArmadilloModel(this Armadillo armadillo)
        {
            ArmadilloModel model = new ArmadilloModel();
            model.ID = armadillo.ID;
            model.Name = armadillo.Name;
            model.Age = armadillo.Age;
            model.ShellHardness = armadillo.ShellHardness;
            model.IsPainted = armadillo.IsPainted;
            model.Homeland = armadillo.Homeland;

            return model;
        }
    }
}

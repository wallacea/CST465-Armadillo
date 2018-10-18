using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmadilloLib;

namespace CST465_Armadillo.CodeExamples
{
    public static class ExtensionMethod
    {
        public static void AddArmadillo(this ArmadilloFarm farm)
        {
            Random rand = new Random();
            string[] names = { "Caleb", "Dayton", "Doug", "Jack", "Jacob", "Justin", "Kevin", "Levi", "Other Levi", "Morgan", "Paul", "Other Paul", "Shaun" };
            Armadillo armadillo = new Armadillo();
            armadillo.Name = names[rand.Next(0, 15)];
            armadillo.Age = rand.Next(1, 21);
            armadillo.IsPainted = (rand.Next(0, 2) == 0);
            farm.FarmAnimals.Add(armadillo);
        }
    }
}

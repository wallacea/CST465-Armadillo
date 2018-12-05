using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CST465_Armadillo.Models
{
    public class Student
    {
        public string Name { get; set; }
        public List<string> Courses { get; set; } = new List<string>();
    }
}

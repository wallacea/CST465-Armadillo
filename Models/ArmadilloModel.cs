﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CST465_Armadillo.Models
{
    public class ArmadilloModel
    {
        [Required(ErrorMessage="You must give it a name!")]
        [DisplayName("Armadillo's Name")]
        [RegularExpression("^(Other\\s)?Paul$", ErrorMessage = "Only Paul or Other Paul are valid")]
        [DataType(DataType.Password)]
        public string Name { get; set; }
        [Required]
        [DisplayName("How old is ya?")]
        public int Age { get; set; }
        public int ShellHardness { get; set; }
        public bool IsPainted { get; set; }
    }
}
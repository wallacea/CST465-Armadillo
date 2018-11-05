using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CST465_Armadillo.Models
{
    public class ArmadilloModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "You must give it a name!")]
        [DisplayName("Armadillo's Name")]
        //[RegularExpression("^(Other\\s)?Paul$", ErrorMessage = "Only Paul or Other Paul are valid")]
        
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [DisplayName("How old is ya?")]
        public int Age { get; set; }
        [Required]
        public int? ShellHardness { get; set; }
        [DisplayName("Is Painted?")]
        public bool IsPainted { get; set; }
        
        [UIHint("HomelandDropdown")]
        public string Homeland { get; set; }

        public static  List<SelectListItem> PossibleHomelands = new List<SelectListItem>()
        {
            new SelectListItem("Tanzania", "1"),
            new SelectListItem("United States of America", "2"),
            new SelectListItem("Mexico", "3")
        };


    }
}

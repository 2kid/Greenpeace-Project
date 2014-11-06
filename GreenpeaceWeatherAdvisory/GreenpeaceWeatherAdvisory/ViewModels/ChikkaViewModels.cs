using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenpeaceWeatherAdvisory.Models
{
    public class RecipientViewModel
    {
        public string Status { get; set; }
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
        [Display(Name = "Farmer Name")]
        public string FarmerName { get; set; }
    }

}
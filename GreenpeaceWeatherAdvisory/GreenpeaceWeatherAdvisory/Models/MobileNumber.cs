using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenpeaceWeatherAdvisory.Models
{
    public class MobileNumber
    {
        public int MobileNumberID { get; set; }
        [Required, Display(Name = "Mobile Number"), DataType(DataType.PhoneNumber)]
        public Double MobileNo { get; set; }

        public virtual Farmer Farmer { get; set; }
    }
}
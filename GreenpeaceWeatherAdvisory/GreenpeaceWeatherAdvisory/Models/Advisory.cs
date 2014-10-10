using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenpeaceWeatherAdvisory.Models
{
    public class Advisory
    {
        public int AdvisoryID { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public String Message { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}", ApplyFormatInEditMode = true), Display(Name = "Date-Time Stamp"), DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
    }
}
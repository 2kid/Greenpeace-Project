using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreenpeaceWeatherAdvisory.Models
{
    public class FeedbackViewModel
    {
        public int request_id { get; set; }
        public int shortcode { get; set; }
        public string message { get; set; }
        public string mobile_number { get; set; }
        public DateTime timestamp { get; set; }
    }
}
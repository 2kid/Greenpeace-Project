using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenpeaceWeatherAdvisory.Models
{
  
    public class Recipient
    {
        public int RecipientId { get; set; }
        public string Status { get; set; }
        public int ContactId { get; set; }
        public int AdvisoryId { get; set; }

        public virtual Advisory Advisory { get; set; }
    }

    public class Advisory
    {
        public int AdvisoryId { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class Feedback
    {
        public int FeedbackId { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
        public string MobileNumber { get; set; }
        public int Shortcode { get; set; }
    }

}
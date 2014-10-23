using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenpeaceWeatherAdvisory.Models
{
    public class Region
   {
        [Key]
        public int RegionId { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class ContactDetail
    {
        [Key]
        public int ContactDetailId { get; set; }
        [Required]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [Required]
        public int FarmerId { get; set; }

        [ForeignKey("FarmerId")]
        public virtual Farmer Farmer { get; set; }
    }

    public class ChikkaMessage
    {
        [Key]
        public int ChikkaMessageId { get; set; }
        [Required]
        public string Message { get; set; }
        public string Status { get; set; }
        [Required]
        public int ContactId { get; set; }

        [ForeignKey("ContactId")]
        public virtual ContactDetail ContactDetail { get; set; }
    }

    public class ChikkaSendRequestVM
    {
        [Key]
        public int RegionId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public string ParameterString(string mobileNumber, int messageId)
        {
            return "message_type=" + Helper.Chikka.MessageType
                + "&mobile_number=" + mobileNumber
                + "&shortcode=" + Helper.Chikka.ShortCode
                + "&message_id=" + messageId
                + "&message=" + Message
                + "&client_id=" + Helper.Chikka.ClientId
                + "&secret_key=" + Helper.Chikka.SecretKey;
        }
    }


}
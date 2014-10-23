using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenpeaceWeatherAdvisory
{
    public sealed class Helper
    {
        public sealed class Constants
        {
            public static readonly string REQUEST_URL = "https://post.chikka.com/smsapi/request";
            public static readonly string MESSAGE_TYPE = "SEND";
            public static readonly string SHORTCODE = "29290326247";
            public static readonly string CLIENT_ID = "e5d1f1466d3800144075d9e35738b925fbeb54b905ef95727b614875f86bac2d";
            public static readonly string SECRET_KEY = "7b1e3aace57e3cd98fa72b068a2395de378aa24853de18f232ca22adfff3c477";
        }

        public sealed  class SendRequestFactory
        {
            public SendRequestFactory(string message)
            {
                this.Message = message;
            }

            public string Message { get; set; }

            public string ParameterString(string mobileNumber, int messageId)
            {
                return "message_type=" + Helper.Constants.MESSAGE_TYPE
                    + "&mobile_number=" + mobileNumber
                    + "&shortcode=" + Helper.Constants.SHORTCODE
                    + "&message_id=" + messageId
                    + "&message=" + Message
                    + "&client_id=" + Helper.Constants.CLIENT_ID
                    + "&secret_key=" + Helper.Constants.SECRET_KEY;
            }
        }

    }
}
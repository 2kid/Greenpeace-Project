using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenpeaceWeatherAdvisory
{
    public sealed class Helper
    {
        public sealed class Chikka
        {
            public static readonly string RequestUrl = "https://post.chikka.com/smsapi/request";
            public static readonly string MessageType = "SEND";
            public static readonly string ShortCode = "29290326247";
            public static readonly string ClientId = "e5d1f1466d3800144075d9e35738b925fbeb54b905ef95727b614875f86bac2d";
            public static readonly string SecretKey = "7b1e3aace57e3cd98fa72b068a2395de378aa24853de18f232ca22adfff3c477";
        }
    }
}
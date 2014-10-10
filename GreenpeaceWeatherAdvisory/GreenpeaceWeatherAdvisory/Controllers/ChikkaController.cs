using GreenpeaceWeatherAdvisory.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

// NOT FOR USE
namespace GreenpeaceWeatherAdvisory.Controllers
{
    public class ChikkaController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Chikka
        public ActionResult SendRequest()
        {
            ChikkaSendRequestVM vm = new ChikkaSendRequestVM();
            vm.Message = "HAHAHAHA!";

            using (WebClient wc = new WebClient())
            {
                try
                {   
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    string HtmlResult = wc.UploadString(Helper.Chikka.RequestUrl, vm.ParameterString("test",1));
                }
                catch (WebException exception)
                {
                    string responseText;

                    if (exception.Response != null)
                    {
                        var responseStream = exception.Response.GetResponseStream();

                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseText = reader.ReadToEnd();
                                ViewBag.Status = responseText;
                            }
                        }
                    }
                }

            }

            return View();
        }
    }
}
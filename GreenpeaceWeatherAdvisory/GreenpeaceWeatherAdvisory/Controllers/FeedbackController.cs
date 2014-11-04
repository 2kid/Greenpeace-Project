﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenpeaceWeatherAdvisory.Models;

namespace GreenpeaceWeatherAdvisory.Controllers
{
    public class FeedbackController : Controller
    {
        // GET: Feedback
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            IEnumerable<Feedback> list = db.Feedback;
            return View(list);
        }
    }
}
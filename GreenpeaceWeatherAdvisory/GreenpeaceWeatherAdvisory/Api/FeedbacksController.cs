using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using GreenpeaceWeatherAdvisory.Models;

namespace GreenpeaceWeatherAdvisory.Api
{
    public class FeedbacksController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: api/Feedbacks
        [ResponseType(typeof(Feedback))]
        public async Task<IHttpActionResult> PostFeedback(FeedbackAPIModel feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Feedback f = new Feedback();
            f.FeedbackId = feedback.request_id;
            f.Message = feedback.message;
            f.MobileNumber = feedback.mobile_number;
            f.TimeStamp = feedback.timestamp;

            db.Feedback.Add(f);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = f.FeedbackId }, feedback);
        }

    }
}
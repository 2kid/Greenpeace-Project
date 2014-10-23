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
        public async Task<IHttpActionResult> PostFeedback(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Feedback.Add(feedback);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = feedback.FeedbackId }, feedback);
        }

        private bool FeedbackExists(int id)
        {
            return db.Feedback.Count(e => e.FeedbackId == id) > 0;
        }
    }
}
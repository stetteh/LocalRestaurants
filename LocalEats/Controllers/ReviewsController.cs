using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using LocalEats.Models;
using Microsoft.AspNet.Identity;

namespace LocalEats.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult GetAllReviews()
        {
            var model = db.Reviews.Select(r => new
            {
                r.Text,
                r.Date
            });


            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetReview(int id)
        {
            var rest = db.Restaurants.Find(id);
            if (rest == null)
            {
                return Content("No restaurant Found.");
            }

            var model = new CreateReviewVm();

            return Json(model, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult SaveReview(CreateReviewVm model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { Message = "Missing Information." });

            }

            var userid = User.Identity.GetUserId();

            var newReview = new Review()
            {
                Text = model.Text,
                Date = DateTime.Now,
                CurrentUser = db.Users.Find(userid),
                Restaurant = db.Restaurants.Find(model.RestaurantId),
                Score = model.Score,

            };
            db.Reviews.Add(newReview);
            db.SaveChanges();


            return Json(new
            {
                newReview.Id,
                newReview.Date,
                newReview.CurrentUser.UserName,
                RestaurantId = newReview.Restaurant.Id,
                newReview.Score
            });
        }
    }
}

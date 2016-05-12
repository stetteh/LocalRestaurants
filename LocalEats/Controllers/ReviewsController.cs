using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalEats.Models;

namespace LocalEats.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reviews
        [HttpGet]
        public ActionResult GetReview(int id)
        {
            var restaurant = db.Restaurants.Find(id);

            if (restaurant == null)
            {
                return HttpNotFound("No Restaurant Found");
            }
            var model = new CreateReviewVm();
            restaurant.Id = model.RestaurantId;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveReview(CreateReviewVm model)
        {
            if (ModelState.IsValid)
            {
                var restaurant = db.Restaurants.Find(model.RestaurantId);

                var newReview = new Review()
                {
                    Restaurant = restaurant,
                    Text = model.Text,
                    Date = model.Date,
                    
                };
                restaurant.Reviews.Add(newReview);
                db.SaveChanges();
            }
            return Json(model);
        }
    }
}
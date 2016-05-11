using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LocalEats.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace LocalEats.Controllers
{
    public class RestaurantsTempController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RestaurantsTemp
        public ActionResult Index()
        {
            var model = db.Restaurants.ToList().Select(r => new RestaurantVm()
            {
                RestaurantId = r.Id,
                Name = r.Name,
                Street = r.StreetAddress,
                City = r.City,
                State = r.State,
                Zipcode = r.Zipcode,
              
                Description = r.Description,
                Category = r.Category,
                PossibleMenus = r.Menus.Select(m => new MenuVm() { Id = m.Id, Name = m.Name, Type = m.Type }),
                PossibleDrinks = r.Drinks.Select(d => new DrinkVm() { Id = d.Id, Name = d.Name })
            });

            return View(model);
        }

        // GET: RestaurantsTemp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // GET: RestaurantsTemp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestaurantsTemp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,StreetAddress,City,State,Zipcode,Hours,PhoneNumber,Category")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("CreateMenu", new { restaurantid = restaurant.Id });
            }

            return View(restaurant);
        }

        // GET: RestaurantsTemp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: RestaurantsTemp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,StreetAddress,City,State,Zipcode,Hours,PhoneNumber,Category")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // GET: RestaurantsTemp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: RestaurantsTemp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

  




  
   
    }
}

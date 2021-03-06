﻿using System;
using System.Collections.Generic;
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
    public class RestaurantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Restaurant
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
                PhoneNumber = r.Zipcode,
                Description = r.Description,
                Category = r.Category,
                PossibleMenus = r.Menus.Select(m => new MenuVm() {Id = m.Id, Type = m.Type}),
                PossibleDrinks = r.Drinks.Select(d => new DrinkVm() {Id = d.Id, Name = d.Name})
            });
            return View(model);
        }

        // Create Restaurant
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,StreetAddress,City,State,Zipcode,Hours,PhoneNumber,Category")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("RestaurantDetails", new { restaurantid = restaurant.Id });
            }
            return View(restaurant);
        }
        
        public ActionResult RestaurantDetails(int restaurantid)
        {
            var model = db.Restaurants.Find(restaurantid);
            model.Id = restaurantid;  
    
            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }

            var detailsView = new DetailsVm()
            {
                RestaurantId = restaurant.Id,
                Name = restaurant.Name,
                Street = restaurant.StreetAddress,
                State = restaurant.State,
                City = restaurant.City,
                Zipcode = restaurant.Zipcode,
                Description = restaurant.Description,
                PhoneNumber = restaurant.PhoneNumber,
                Category = restaurant.Category,
                PossibleMenus = restaurant.Menus.Select(m => new MenuVm() { Id = m.Id, Type = m.Type }),
                PossibleDrinks = restaurant.Drinks.Select(d => new DrinkVm() { Id = d.Id, Name = d.Name }),
                PossiblePhotos = restaurant.Photos.Select(p => new PhotoVm() {Id = p.Id, ImageUrl = "https://localdinning.blob.core.windows.net/" + p.Image})

            };
            return View(detailsView);
        }


        // GET: Restaurants/Edit/5
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

        // GET: Restaurant/Delete/5
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

        // POST: Restaurant/Delete/5
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

        public ActionResult RestaurantList(string restaurantCategory)
        {

            var restQry = db.Restaurants.OrderBy(d => d.Category).Select(d => d.Category.ToString()).Distinct();

            ViewBag.restaurantCategory = new SelectList(restQry);

            var model = from m in db.Restaurants.ToList().Select(r => new RestaurantVm()
            {
                RestaurantId = r.Id,
                Name = r.Name,
                Street = r.StreetAddress,
                City = r.City,
                State = r.State,
                Zipcode = r.Zipcode,
                PhoneNumber = r.PhoneNumber,
                Description = r.Description,
                Category = r.Category,
                PossibleMenus = r.Menus.Select(m => new MenuVm() { Id = m.Id, Type = m.Type }),
                PossibleDrinks = r.Drinks.Select(d => new DrinkVm() { Id = d.Id, Name = d.Name }),
                PossiblePhotos = r.Photos.Select(p => new PhotoVm() { Id = p.Id, ImageUrl = "https://localdinning.blob.core.windows.net/" + p.Image })
            })

            select m;

            if (!String.IsNullOrEmpty(restaurantCategory))
            {
                model = model.Where(x => x.Category.ToString() == restaurantCategory);

            }

            return View(model);
        }
       
        [HttpGet]
        public ActionResult UploadImages()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadImages(HttpPostedFileBase image)
        {
            var newFileName = $"{Guid.NewGuid()}.jpg";
            var location = SaveImage(image.InputStream, "dining", newFileName);


            return RedirectToAction("Index");
        }

        private static string SaveImage(Stream imageStream, string folder, string newFileName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount
                .Parse(
                    "DefaultEndpointsProtocol=https;AccountName=localdinning;AccountKey=1Fy6RiGAvfp318mV7PDGlyPeRZpWEIoBAzbbZOHJYS0h01cWaem7Z87PuEo++3sDK43KQbLzguin+FTdIQViQg==");

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(folder.ToLower());

            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });


            CloudBlockBlob blockBlob = container.GetBlockBlobReference(newFileName);
            blockBlob.UploadFromStream(imageStream);

            return $"{folder.ToLower()}/{newFileName}";
        }

        [HttpGet]
        public ActionResult AddPhoto(int id)
        {
            var model = new AddPhotoVM();
            var restaurant = db.Restaurants.Find(id);
            model.RestaurantName = restaurant.Name;
            model.RestaurantId = restaurant.Id;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddPhoto(AddPhotoVM newPhoto)
        {
            var restaurant = db.Restaurants.Find(newPhoto.RestaurantId);

            var newFileName = $"{Guid.NewGuid()}.jpg";
            var location = SaveImage(newPhoto.ImageUrl.InputStream, "Dining", newFileName);

            var p = new Photo() { Restaurant = restaurant, Image = location };
            restaurant.Photos.Add(p);
            db.SaveChanges();
            return RedirectToAction("AddPhoto");
        }
    }
}
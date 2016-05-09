﻿using System;
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
                RestautantId = r.Id,
                Name = r.Name,
                Street = r.StreetAddress,
                City = r.City,
                State = r.State,
                Zipcode = r.Zipcode,
                PhoneNumber = r.Zipcode,
                Description = r.Description,
                Features = r.Features,
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

        [HttpGet]
        public ActionResult CreateMenu(int restaurantid)
        {
            var rest = db.Restaurants.Find(restaurantid);
            var model = new CreateMenuVm() { RestaurantId = rest.Id };
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateMenu(CreateMenuVm model)
        {
            if (ModelState.IsValid)
            {
                var restaurant = db.Restaurants.Find(model.RestaurantId);

                var newMenu = new Menu()
                {
                    Restaurant = restaurant,
                    Name = model.Name,
                    Description = model.Description,
                    Type = model.Type
                };
                restaurant.Menus.Add(newMenu);
                db.SaveChanges();
                return RedirectToAction("CreateFood", new { menuid = newMenu.Id });
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateFood(int menuid)
        {
            var menu = db.Menus.Find(menuid);
            var model = new CreateFoodVm() { MenuId = menu.Id };
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateFood(CreateFoodVm model)
        {
            if (ModelState.IsValid)
            {
                var menu = db.Menus.Find(model.MenuId);

                var newfood = (new Food()
                {
                    ParentMenu = menu,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    FoodImage = model.Image,
                    Type = model.TypeType
                });
                menu.Foods.Add(newfood);
                db.SaveChanges();
            }
            return Content("done");
        }

        [HttpGet]
        public ActionResult CreateDrink(int restaurantid)
        {
            var rest = db.Restaurants.Find(restaurantid);
            var model = new CreateDrinkVm() { RestaurantId = rest.Id };
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateDrink(CreateDrinkVm model)
        {
            if (ModelState.IsValid)
            {
                var restaurant = db.Restaurants.Find(model.RestaurantId);

                var newDrink = new Drink()
                {
                    Restaurant = restaurant,
                    Name = model.Name,
                    Description = model.Description,
                    Type = model.Type
                };
                restaurant.Drinks.Add(newDrink);
                db.SaveChanges();
                return RedirectToAction("CreateDrink");
            }
            return View(model);
        }

        public ActionResult RestaurantList()
        {

            //var result = from e in db.Restaurants
            //    select e;

            //if (!String.IsNullOrEmpty(searchCity))
            //{
            //    result = result.Where(s => s.City.StartsWith(searchCity));
            //}

            //return View(result);

            var model = db.Restaurants.ToList().Select(r => new RestaurantVm()
            {
                RestautantId = r.Id,
                Name = r.Name,
                Street = r.StreetAddress,
                City = r.City,
                State = r.State,
                Zipcode = r.Zipcode,
                PhoneNumber = r.Zipcode,
                Description = r.Description,
                Features = r.Features,
                Category = r.Category,
                PossibleMenus = r.Menus.Select(m => new MenuVm() { Id = m.Id, Name = m.Name, Type = m.Type }),
                PossibleDrinks = r.Drinks.Select(d => new DrinkVm() { Id = d.Id, Name = d.Name }),
                PossiblePhotos = r.Photos.Select(p => new PhotoVm() { Id = p.Id, ImageUrl = "https://localdinning.blob.core.windows.net/" + p.Image })
                });

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

            return $"{folder}/{newFileName}";
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
            return RedirectToAction("RestaurantList");
        }
    }
}

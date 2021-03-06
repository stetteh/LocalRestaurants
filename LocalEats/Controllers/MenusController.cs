﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LocalEats.Models;
using Menu = LocalEats.Models.Menu;

namespace LocalEats.Controllers
{
    public class MenusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult CreateMenu(int id)
        {
            var model = new CreateMenuVm();
            var restaurant = db.Restaurants.Find(id);
            model.RestaurantId = restaurant.Id;
            return View(model);
            
        }

        [HttpPost]
        public ActionResult CreateMenu(CreateMenuVm model)
        {
            if (ModelState.IsValid)
            {
                var restaurant = db.Restaurants.Find(model.RestaurantId);

                var m = new Menu()
                {
                    Restaurant = restaurant,
                    Type = model.Type
                };
                restaurant.Menus.Add(m);
                db.SaveChanges();
                return RedirectToAction("CreateFood", new {menuid = m.Id });
              }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateFood(int menuid)
        {
            var model = new CreateFoodVm();
            var menu = db.Menus.Find(menuid);
            model.MenuId = menu.Id;
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateFood(CreateFoodVm model)
        {
            if (ModelState.IsValid)
            {
                var menu = db.Menus.Find(model.MenuId);

                var f = new Food()
                {
                    ParentMenu = menu,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Type = model.TypeType
                };
                menu.Foods.Add(f);
                db.SaveChanges();
                return RedirectToAction("CreateMenu", new { id = menu.Id});
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateDrink(int id)
        {

            var model = new CreateDrinkVm();
            var restaurant = db.Restaurants.Find(id);
            model.RestaurantId = restaurant.Id;
            model.Name = restaurant.Name;
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
    }
}
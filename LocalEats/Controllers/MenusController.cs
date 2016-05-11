using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LocalEats.Models;

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
             model.Name = restaurant.Name;
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
                    //Description = model.Description,
                    Type = model.Type
                };
                restaurant.Menus.Add(newMenu);
                db.SaveChanges();
                return RedirectToAction("CreateFood", new {menuid = newMenu.Id});
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateFood(int menuid)
        {
            var menu = db.Menus.Find(menuid);
            var model = new CreateFoodVm() {MenuId = menu.Id, Name = menu.Name};
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
            return RedirectToAction("CreateMenu");
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
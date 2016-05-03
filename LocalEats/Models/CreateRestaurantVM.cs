using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalEats.Models
{
    public class CreateRestaurantVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
        public DateTime Hours { get; set; }
        public int PhoneNumber { get; set; }
        public Category Category { get; set; }
        public Menu Menu { get; set; }
        public  Drink Drink { get; set; }
        public Photo Photo { get; set; }
    }

    public class CreateMenuVM
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateFoodVM
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodImage { get; set; }
        public double Price { get; set; }
        public MenuType Type { get; set; }
    }
}
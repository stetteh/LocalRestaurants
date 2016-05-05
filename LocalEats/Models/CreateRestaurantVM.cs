using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalEats.Models
{
    public class CreateRestaurantVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public int PhoneNumber { get; set; }
        public Category Category { get; set; }
    }

    public class CreateMenuVm
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public MenuType Type { get; set; }
    }

    public class CreateFoodVm
    {
        public int MenuId { get; set; }
        public string FoodName { get; set; }
        public string FoodDescription { get; set; }
        public string FoodImage { get; set; }
        public double FoodPrice { get; set; }
        public FoodType TypeType { get; set; }

    }

    public class CreateDrinkVm
    {
        public int RestaurantId { get; set; }
        public string DrinkName { get; set; }
        public string DrinkDescription { get; set; }
        public DrinkType Type { get; set; }
    }

    public class RestaurantVm
    {
        public int RestautantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantStreet { get; set; }
        public string RestaurantCity { get; set; }
        public string RestaurantState { get; set; }
        public int RestaurantZipcode { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public int RestaurantPhoneNumber { get; set; }
        public string RestaurantPhoto { get; set; }
        public Category Category { get; set; }
        public IEnumerable<MenuVm> PossibleMenus { get; set; } =  new List<MenuVm>();
        public IEnumerable<DrinkVm> PossibleDrinks { get; set; } = new List<DrinkVm>();
        
    }

    public class MenuVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MenuType Type { get; set; }
       
    }

    public class DrinkVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DrinkType Type { get; set; }
    }
}
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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public FoodType TypeType { get; set; }

    }

    public class CreateDrinkVm
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DrinkType Type { get; set; }
    }

    public class RestaurantVm
    {
        public int RestautantId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zipcode { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public int PhoneNumber { get; set; }
        public string Photo { get; set; }
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
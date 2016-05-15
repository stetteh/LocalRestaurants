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
        public string Zipcode { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public Category Category { get; set; }
    }

    public class CreateMenuVm
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
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

    public class AddPhotoVM
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public HttpPostedFileBase ImageUrl { get; set; }
    }

    public class RestaurantVm
    {
        private Restaurant restaurant;

        public RestaurantVm(Restaurant restaurant)
        {
            this.restaurant = restaurant;
        }

        public RestaurantVm()
        {
        }

        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Description { get; set; }
        public string  PhoneNumber { get; set; }
        public string Photo { get; set; }
        public Category Category { get; set; }
        public IEnumerable<MenuVm> PossibleMenus { get; set; } =  new List<MenuVm>();
        public IEnumerable<DrinkVm> PossibleDrinks { get; set; } = new List<DrinkVm>();
        public IEnumerable<PhotoVm> PossiblePhotos { get; set; } = new List<PhotoVm>();

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

    public class PhotoVm
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }

    public class RestaurantDetailsVm
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
    }

    public class CreateReviewVm
    {
        public int RestaurantId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
    }

    public class ReviewVm
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
        public ApplicationUser User { get; set; }
    }
}
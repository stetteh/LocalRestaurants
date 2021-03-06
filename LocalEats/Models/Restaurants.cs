﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;

namespace LocalEats.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public string Hours { get; set; }
        public Category Category { get; set; }
        
        public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
        public virtual ICollection<Drink> Drinks { get; set; } = new List<Drink>();
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public DrinkType Type { get; set; }
    }

    public class Photo
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }

    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
        public ApplicationUser CurrentUser { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }

    public enum Category
    {
        American = 1,
        BBQ,
        Asian,
        Mediterranean,
        Vegeterain,
        Southern, 
        Italian,
        Mexican,
        Buffet,
        African,
        Chinese,
        Cuban,
        Indian,
        Pizza
    }

    public enum DrinkType
    {
        Alcohol = 1,
        NonAlcoholic,
        Juice,
        HotDrinks,
        SoftDrinks,      
    }
}
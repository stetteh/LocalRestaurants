using System;
using System.Collections;
using System.Collections.Generic;

namespace LocalEats.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public MenuType Type { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
    }

    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FoodImage { get; set; }
        public double  Price { get; set; }
        public FoodType Type { get; set; }
        public virtual Menu ParentMenu { get; set; }
    }

    public enum MenuType
    {
        Breakfast = 1,
        Lunch,
        Dinner,
        AfterHours
    }

    public enum FoodType
    {
        Breakfast = 1,
        Lunch,
        Dinner,
        AfterHours
    }
}
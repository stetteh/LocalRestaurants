using System;
using System.Collections;
using System.Collections.Generic;

namespace LocalEats.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Restaurant Restaurant { get; set; }
        public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
    }

    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Double  Price { get; set; }
        public MenuType Type { get; set; }
        public Menu ParentMenu { get; set; }
    }

    public enum MenuType
    {
        Breakfast = 1,
        Lunch,
        Dinner,
        AfterHours
    }
}
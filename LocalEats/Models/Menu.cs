using System.Collections;
using System.Collections.Generic;

namespace LocalEats.Models
{
    public class Menu
    {
        public virtual ICollection<Food> Foods { get; set; } = new List<Food>();
    }

    public class Food
    {
        
    }

    public enum MenuType
    {
        Breakfast = 1,
        Lunch,
        Dinner,
        AfterHours
    }
}
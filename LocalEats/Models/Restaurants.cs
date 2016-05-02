using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;

namespace LocalEats.Models
{
    public class Restaurants
    {
        public int Id { get; set; }
        public  string Name { get; set; }
        public string location { get; set; }
        public DateTime Hours { get; set; }
        public int PhoneNumber { get; set; }
        public string Photos { get; set; }
        
        public virtual ICollection<Menu> Menus { get; set; }= new List<Menu>();
        public virtual ICollection<Drink> Drinks { get; set; } = new List<Drink>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

   

    public class Drink
    {
    }

    public class Review
    {
    }

    public enum Category
    {
        American = 1,
        BBQ,
        Asian,
        Mediterranean,
        Vegeterain,
        Southern,
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public int RestaurantId { get; set; }
    }
}

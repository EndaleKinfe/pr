using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CuisineType { get; set; }
        public string ImageUrl { get; set; }
    }
}

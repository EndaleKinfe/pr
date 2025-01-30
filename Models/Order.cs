using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int DeliveryManId { get; set; } // Nullable if no delivery assigned
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } // Pending, Preparing, Delivered, Canceled
        public decimal TotalAmount { get; set; }
        public string FoodId { get; set; }
    }

}

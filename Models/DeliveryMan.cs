using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pr.Models
{
    public class DeliveryMan
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string VehicleType { get; set; }
        public string LicenseNumber { get; set; }
        public bool IsAvailable { get; set; }
    }
}
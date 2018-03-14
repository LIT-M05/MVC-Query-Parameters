using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipAddress { get; set; }
        public string ShipName { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShipRegion { get; set; }
    }
}
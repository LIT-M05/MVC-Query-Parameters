using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class OrdersAndDate
    {
        public IEnumerable<Order> Orders { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}
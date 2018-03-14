using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class ProductsAndCatName
    {
        public IEnumerable<Product> Products { get; set; }
        public string CategoryName { get; set; }
    }
}
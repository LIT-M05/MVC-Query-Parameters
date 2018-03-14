using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class ProductSearchResutls
    {
        public IEnumerable<Product> Products { get; set; }
        public string SearchText { get; set; }
    }
}
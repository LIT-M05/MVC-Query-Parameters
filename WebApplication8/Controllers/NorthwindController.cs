using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;
using WebApplication8.Properties;

namespace WebApplication8.Controllers
{
    public class NorthwindController : Controller
    {
        public ActionResult Orders()
        {
            NorthwindManager mgr = new NorthwindManager(Settings.Default.ConStr);
            IEnumerable<Order> orders = mgr.GetOrders();
            OrdersAndDate obj = new OrdersAndDate
            {
                Orders = orders,
                CurrentDate = DateTime.Now
            };
            return View(obj);
        }

        public ActionResult OrderDetails()
        {
            NorthwindManager mgr = new NorthwindManager(Settings.Default.ConStr);
            return View(mgr.GetOrderDetailsFor97());
        }

        public ActionResult DetailsForOrder(int orderId)
        {
            NorthwindManager mgr = new NorthwindManager(Settings.Default.ConStr);
            return View(mgr.DetailsForOrder(orderId));
        }

        public ActionResult Categories()
        {
            NorthwindManager mgr = new NorthwindManager(Settings.Default.ConStr);
            return View(mgr.GetCategories());
        }

        public ActionResult Products(int catId)
        {
            NorthwindManager mgr = new NorthwindManager(Settings.Default.ConStr);
            ProductsAndCatName obj = new ProductsAndCatName
            {
                Products = mgr.GetProductsForCategory(catId),
                CategoryName = mgr.GetCategoryName(catId)
            };
            return View(obj);
        }

        public ActionResult ProductSearch()
        {
            return View();
        }

        public ActionResult SearchResults(string searchText)
        {
            NorthwindManager mgr = new NorthwindManager(Settings.Default.ConStr);
            return View(mgr.SearchProducts(searchText));
        }

        public ActionResult SearchBetter(string searchText)
        {
            if (String.IsNullOrEmpty(searchText))
            {
                return View(new ProductSearchResutls());
            }

            NorthwindManager mgr = new NorthwindManager(Settings.Default.ConStr);
            ProductSearchResutls data = new ProductSearchResutls
            {
                Products = mgr.SearchProducts(searchText),
                SearchText = searchText
            };
            return View(data);
        }
    }
}

//create an application that has the following pages:

// /northwind/categories - on this page, display a list of all categories from the 
//north wind database. The id or title should be a link, that when clicked takes you 
//to /northwind/products and displays a list of products for the category that was 
//clicked on

//as an added bonus, see if you can add the following feature: On the products page, 
//add an H1 that says "Products for {category name}" e.g. "Products for Beverages"
//based on the category currently being displayed
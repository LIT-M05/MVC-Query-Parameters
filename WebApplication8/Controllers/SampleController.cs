using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class SampleController : Controller
    {
        public ActionResult Index(string foo)
        {
            SampleData sd = new SampleData
            {
                Foo = foo
            };
            return View(sd);
        }

        public ActionResult FormDemo()
        {
            return View();
        }

        public ActionResult Search(string searchText)
        {
            SearchData sd = new SearchData();
            sd.SearchText = searchText;
            return View(sd);
        }
    }

}
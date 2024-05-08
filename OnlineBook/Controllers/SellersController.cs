using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBook.Controllers
{
    public class SellersController : Controller
    {
        // GET: Sellers
        public ActionResult SellerEntry()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
    }
}
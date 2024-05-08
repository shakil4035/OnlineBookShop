using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBook.Controllers
{
    public class CategorysController : Controller
    {
        // GET: Categorys
        public ActionResult New()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBook.Controllers
{
    public class AuthorsController : Controller
    {
        // GET: Authors
        public ActionResult New()
        {
            return View();
        }
    }
}
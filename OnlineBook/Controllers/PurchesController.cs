using Pims.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBook.Controllers
{
    public class PurchesController : Controller
    {
        // GET: Purches
        public ActionResult New(int?id)
        {
            ViewBag.AuthorId= new SelectList(new List<Author>(), "Id", "Name");
            return View();
        }
    }
}
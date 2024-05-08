using Pims.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBook.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult BooksEntry(int?id)
        {
            ViewBag.AuthorId = new SelectList(new List<Author>(), "Id", "Name");
            ViewBag.CategoryId = new SelectList(new List<Category>(), "Id", "Name");
            return View();
        }
    }
}
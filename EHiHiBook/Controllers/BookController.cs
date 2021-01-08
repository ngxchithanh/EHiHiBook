using EHiHiBook.Models;
using EHiHiBook.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EHiHiBook.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        SachEntities _db = new SachEntities();

        // GET: Home
        public ActionResult BookStore()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel());
        }

        public ActionResult BookDetail()
        {
            return View();
        }
    }
}
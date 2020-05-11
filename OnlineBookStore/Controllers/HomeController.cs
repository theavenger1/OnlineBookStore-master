using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var books = db.books.ToList(); ;
            return View(books);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.books.Where(aa => aa.book_title.Contains(searchName)
            || aa.book_description.Contains(searchName)
            || aa.category.category_name.Contains(searchName)).ToList();
            return View(result);
        }
    }
}
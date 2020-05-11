using BookStore.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.IO;
using Microsoft.AspNet.Identity;

namespace OnlineBookStore.Controllers
{
    [Authorize]
    public class booksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Books
        public ActionResult Index()
        {
            var books = db.books.ToList(); ;
            return View(books);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                book book = db.books.Find(id);
                if (book == null)
                {
                    return HttpNotFound();
                }
                return View(book);
            }
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.categoryId = new SelectList(db.categories, "Id", "category_name");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(book book, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"),upload.FileName);
                    upload.SaveAs(path);
                    book.book_image = upload.FileName;
                    book.userId= User.Identity.GetUserId();
                }
             //   ViewBag.ImgMsg = "Goooood";
              //  book.userId = User.Identity.GetUserId();
                db.books.Add(book);
                db.SaveChanges();
             //   return RedirectToAction("Index");
                return RedirectToAction("GetUserBooks", "books");
            }
            ViewBag.categoryId = new SelectList(db.categories, "Id", "category_name",book.categoryId);

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryId = new SelectList(db.categories, "Id", "category_name", book.categoryId);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(book book, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Uploads"), book.book_image);

                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    book.book_image = upload.FileName;
                }

                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.categoryId = new SelectList(db.categories, "Id", "category_name", book.categoryId);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            book book = db.books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            book book = db.books.Find(id);
            db.books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult GetUserBooks()
        {
            var UserId = User.Identity.GetUserId();
            var books = db.books.Where(aa => aa.userId == UserId).ToList();
            return View(books);

           
        }
    }
}

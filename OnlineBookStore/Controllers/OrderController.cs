using BookStore.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.IO;
using Microsoft.AspNet.Identity;
using System;

namespace OnlineBookStore.Controllers
{
    [Authorize(Roles = "Admins")]
    public class OrderController : Controller
    {
        private ApplicationDbContext BookStoreDB = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            var orders = BookStoreDB.orders.ToList();
            return View(orders);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(order order)
        {
            order.userId= User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                BookStoreDB.orders.Add(order);
                BookStoreDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var order = BookStoreDB.orders.Single(ww => ww.Id == id);
            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(order order)

        {
           
            BookStoreDB.Entry(order).State = System.Data.Entity.EntityState.Modified;
            
            BookStoreDB.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var order = BookStoreDB.orders.Single(ww => ww.Id == id);
            BookStoreDB.orders.Remove(order);
            BookStoreDB.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
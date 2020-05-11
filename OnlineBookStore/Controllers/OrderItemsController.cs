using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace OnlineBookStore.Controllers
{
    [Authorize(Roles = "Admins")]

    public class OrderItemsController : Controller
    {
        ApplicationDbContext BookStoreDB = new ApplicationDbContext();
        public ActionResult Index()
        {
            var orderItems = BookStoreDB.order_items.ToList();
            return View(orderItems);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(order_item orderItem)
        {
            if (ModelState.IsValid)
            {
                BookStoreDB.order_items.Add(orderItem);
                BookStoreDB.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var orderItem = BookStoreDB.order_items.Single(ww => ww.Id == id);
            return View(orderItem);
        }

        [HttpPost]
        public ActionResult Edit(order_item orderItem)
        {
            BookStoreDB.Entry(orderItem).State = System.Data.Entity.EntityState.Modified;
            BookStoreDB.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            BookStoreDB.order_items.Remove(BookStoreDB.order_items.Single(ww => ww.Id == id));
            BookStoreDB.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    [Authorize(Roles = "Admins")]
    public class HubController : Controller
    {
        private readonly ApplicationDbContext db;
        public HubController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var model = from r in db.hubs
                        orderby r.hub_name
                        select r;

            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(hub hub)
        {
            if (ModelState.IsValid)
            {
                db.hubs.Add(hub);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

      

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var hub = db.hubs.Find(id);
            if (hub == null)
            {
                return HttpNotFound();
            }

            return View(hub);

        }
       
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(hub hub)
        {
            if (ModelState.IsValid)
            {
                var entry = db.Entry(hub);
                entry.State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
       

            return View(hub);

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var hub = db.hubs.Find(id); 

            if (hub ==null )
            {
                return HttpNotFound();
            }
            return View(hub);

        }
      
        
        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            var hub = db.hubs.Find(id);
            db.hubs.Remove(hub);
            db.SaveChanges();


            return RedirectToAction("Index");

        }

    }

}
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
    public class HubAdminController : Controller
    {
        private readonly ApplicationDbContext db;
        public HubAdminController()
        {
            db = new ApplicationDbContext();
        }


        public ActionResult Index()
        {
            var model = from r in db.hubs_admins
                        orderby r.Id
                        select r;

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {

            ViewBag.h = new SelectList(db.hubs, "Id", "hub_name");

            return View();
        }
        public ActionResult Create(hubs_admins a, FormCollection f)
        {
            string Hubvalue = f["h"].ToString();
            a.hub_id = int.Parse(Hubvalue);
            if (ModelState.IsValid)
            {

                db.hubs_admins.Add(a);
                db.SaveChanges();
                return RedirectToAction("Details",new {id = a.Id });
            }
            return View();
       
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var a = db.hubs_admins.Find(id);
            if (a == null)
            {
                return HttpNotFound();
            }


            ViewBag.Hubs = new SelectList(db.hubs, "Id", "hub_name");
            return View(a);
        }
        public ActionResult Edit(hubs_admins a, FormCollection f)
        {
            string Hubvalue = f["Hubs"].ToString();

            if (ModelState.IsValid)
            {
                a.hub_id = int.Parse(Hubvalue);

                var entry = db.Entry(a);
                entry.State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(a);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var a = db.hubs_admins.Find(id);

            if (a == null)
            {
                return HttpNotFound();

            }
            return View(a);

        }
        public ActionResult Delete(int id, FormCollection form)
        {
            var a = db.hubs_admins.Find(id);
            db.hubs_admins.Remove(a);
            db.SaveChanges();


            return RedirectToAction("Index");

        }

        public ActionResult Details (int id)
        {
            var a = db.hubs_admins.Find(id);

            if (a == null)
            {
                return HttpNotFound();

            }
            return View(a);

        }
    }
}
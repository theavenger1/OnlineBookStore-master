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
    public class CostController : Controller
    {
        private readonly ApplicationDbContext db;

        public CostController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Index()

        {

            var model = from r in db.hub_travel_cost
                        orderby r.Id
                        
                        select r;
          

            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
           
            ViewBag.FromI = new SelectList(db.cities, "Id", "city_name");
            ViewBag.TOI = new SelectList(db.cities, "Id", "city_name");
            return View();

      
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(hub_travel_cost c, FormCollection f)
       
        {
            string Cityfrom= f["FromI"].ToString();
            string Cityto = f["TOI"].ToString();
            c.FromId = int.Parse(Cityfrom);
            c.ToId = int.Parse(Cityto);
            if (ModelState.IsValid)
            {
               

                db.hub_travel_cost.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cost = db.hub_travel_cost.Find(id);
            if (cost == null)
            {
                return HttpNotFound();
            }
            ViewBag.FromE = new SelectList(db.cities, "Id", "city_name");
            ViewBag.TOE = new SelectList(db.cities, "Id", "city_name");
            return View(cost);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(hub_travel_cost c, FormCollection f)
        {
            string Cityfrom = f["FromE"].ToString();
            string Cityto = f["TOE"].ToString();
            c.FromId = int.Parse(Cityfrom);
            c.ToId = int.Parse(Cityto);


            if (ModelState.IsValid)
            {
                var entry = db.Entry(c);
                entry.State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(c);

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var cost = db.hub_travel_cost.Find(id);

            if (cost == null)
            {
                return HttpNotFound();
            }
            return View(cost);

        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection form)
        {
            var cost = db.hub_travel_cost.Find(id);
            db.hub_travel_cost.Remove(cost);
            db.SaveChanges();


            return RedirectToAction("Index");

        }

    }
}
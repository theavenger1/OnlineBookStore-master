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
    public class CityController : Controller
    {
        private ApplicationDbContext db;

        public CityController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var model = from r in db.cities
                        orderby r.city_name
                        select r;

            return View(model);
        }



        [HttpGet]
        public ActionResult Create()
        {
          
            ViewBag.Govs = new SelectList(  db.govs, "Id", "gov_name");

            return View();
        }
        [HttpPost]
        public ActionResult Create(city c, FormCollection f)
        {
            string GovValue = f["Govs"].ToString();

            if (ModelState.IsValid)
            {

                c.gov_id = int.Parse(GovValue);


                db.cities.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
      
        
        
        [HttpGet]     
        public ActionResult Edit(int id)
        {
            var city = db.cities.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }


            ViewBag.Govs = new SelectList(db.govs, "Id", "gov_name");
            return View(city);


        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(city city ,FormCollection f )
        {

            string Govvalue = f["Govs"].ToString();

            if (ModelState.IsValid)
            {
               city.gov_id= int.Parse(Govvalue);

                var entry = db.Entry(city);
                entry.State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(city);
             
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var city = db.cities.Find(id);

            if (city == null)
            {
                return View("Not Found");
            }
            return View(city);

        }
        [HttpPost]

        public ActionResult Delete(int id, FormCollection form)
        {
            var city = db.cities.Find(id);
            db.cities.Remove(city);
            db.SaveChanges();


            return RedirectToAction("Index");

        }
    }
}
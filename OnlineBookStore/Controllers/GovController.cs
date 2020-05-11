using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;


namespace BookStore.Controllers
{
    [Authorize(Roles = "Admins")]
    public class GovController : Controller { 
    
    private readonly ApplicationDbContext db;
    public GovController()
    {
        db = new ApplicationDbContext();
    }

    public ActionResult Index()
    {
        var model = from r in db.govs
                    orderby r.Id
                    
                    select r;

        return View(model);
    }
            
    [HttpGet]
    public ActionResult Create()
    {
            //IEnumerable<SelectListItem> items = db.hubs.Select(c => new SelectListItem
            //{
            //    Value = "c.Id",
            //    Text = c.hub_name
            //});
            //ViewBag.Hubs = items;

      
             ViewBag.Hubs = new SelectList(db.hubs, "Id", "hub_name");
           
            return View();
    }
    [HttpPost]
    public ActionResult Create(gov g,FormCollection f )
    {
          string Hubvalue = f["Hubs"].ToString();

            if (ModelState.IsValid)
        {

                g.hub_id = int.Parse(Hubvalue);


            db.govs.Add(g);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View();
    }
    
     [HttpGet]
    public ActionResult Edit(int id )
        {
            var gov= db.govs.Find(id);
            if (gov == null)
            {
                return HttpNotFound();
            }


            ViewBag.Hubs = new SelectList(db.hubs, "Id", "hub_name");
            return View(gov);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
     public ActionResult Edit(gov gov, FormCollection f)
        {
            string Hubvalue = f["Hubs"].ToString();  

            if (ModelState.IsValid)
            {
                gov.hub_id = int.Parse(Hubvalue);

                var entry = db.Entry(gov);
                entry.State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gov);
       }

    [HttpGet]
    public ActionResult Delete(int id)
    {
        var gov = db.govs.Find(id);

        if (gov == null)
        {
            return View("Not Found");
        }
        return View(gov);

    }
    [HttpPost]

    public ActionResult Delete(int id, FormCollection form)
    {
        var gov = db.govs.Find(id);
        db.govs.Remove(gov);
        db.SaveChanges();


        return RedirectToAction("Index");

    }

}
}
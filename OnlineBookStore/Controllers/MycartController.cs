using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using Microsoft.AspNet.Identity;

namespace OnlineBookStore.Controllers { 
              
    [Authorize]
    public class MycartController : Controller
    {
        ApplicationDbContext BookStoreDB = new ApplicationDbContext();
        public ActionResult Index()
        {
          
                return View();
        }

        [HttpGet]
        public ActionResult Addtocart(int id )
        {
            book  b1 = new book();
            b1 = BookStoreDB.books.Find(id);
            // cost
            ApplicationUser u = BookStoreDB.Users.Find(User.Identity.GetUserId());

            var model = from r in BookStoreDB.hub_travel_cost
                         where r.FromId==b1.user.cityId && r.ToId == u.cityId
             
                        select r;
            
            //BookStoreDB.hub_travel_cost.Find(b1.user.cityId).To.city_name == BookStoreDB.Users.Find(User.Identity.GetUserId()).city.city_name)

            if (Session["cart"] == null)
            {
                List<order_item> cart = new List<order_item>();
                cart.Add(new order_item {book=b1 ,book_id=b1.Id, Quantity = 1  , Price = (b1.book_cost * model.FirstOrDefault().cost)});
                Session["cart"] = cart;
              
            }
            else
            {
                List<order_item> cart = (List<order_item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                    cart[index].Price = cart[index].Quantity * cart[index].book.book_cost;
                }
                else
                {
                    cart.Add(new order_item { book = b1 , book_id = b1.Id, Quantity = 1 ,Price=b1.book_cost});
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index", "Home");

        }
        private int isExist(int id)
        {
            List<order_item> cart = (List<order_item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].book.Id.Equals(id))
                    return i;
            return -1;
        }

       
        public ActionResult Create()
        {
            if(  Session["cart"] != null){
                order d1 = new order { userId = User.Identity.GetUserId(),  order_state = false  };

                foreach (var i in (List<order_item>)Session["cart"])
                {
                    order_item item1 = new order_item { order_id = d1.Id, book_id = i.book.Id, Price = i.Price, Quantity = i.Quantity };
                    BookStoreDB.order_items.Add(item1);
                }
                if (ModelState.IsValid)
                {
                  
                    BookStoreDB.orders.Add(d1);
                    BookStoreDB.SaveChanges();

                    Session["cart"] = null;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();

        }

     
        public ActionResult Delete(int id)
        {
            List<order_item> cart = (List<order_item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");

        }

    }
}
using EHiHiBook.Models;
using EHiHiBook.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EHiHiBook.Controllers
{
    public class HomeController : Controller
    {
        SachEntities _db = new SachEntities();

        // GET: Home
        public ActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel());
        }


        public ActionResult AddToCart(int bookId, string url)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var book = _db.Books.Find(bookId);
                cart.Add(new Item()
                {
                    Book = book,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var book = _db.Books.Find(bookId);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Book.BookID == bookId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Book = book,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Book.BookID == bookId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Book = book,
                                Quantity = 1
                            });
                        }
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect(url);
        }

        public ActionResult RemoveFromCart(int bookId)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Book.BookID == bookId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect("Index");
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        public ActionResult CheckOutDetails()
        {
            return View();
        }

        public ActionResult DecreaseQty(int bookId)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var book = _db.Books.Find(bookId);
                foreach (var item in cart)
                {
                    if (item.Book.BookID == bookId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Book = book,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect("Checkout");
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}
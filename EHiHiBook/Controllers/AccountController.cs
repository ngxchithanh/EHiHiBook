using EHiHiBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EHiHiBook.Controllers
{
    public class AccountController : Controller
    {
        SachEntities _db = new SachEntities();

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LogIn(User _user)
        {
            var check = _db.Users.Where(s => s.Email.Equals(_user.Email)
            && s.Pass.Equals(_user.Pass)).FirstOrDefault();
            if (check == null)
            {
                _user.LogInErrorMessage = "Error Email or Password! Try again!";
                return View("LogIn");
            }
            else
            {
                var test = _db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (test.Email != "admin@gmail.com") //neu khong phai admin
                {
                    Session["UserID"] = _user.UserID;
                    Session["Email"] = _user.Email;
                    return RedirectToAction("Index", "Home");
                }
                else //neu la admin
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.Email == _user.Email); //So sanh co Email da dang ky hay chua
                if (check == null) //neu Email dang su dung chua dang ky
                {
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Users.Add(_user); //Them user vao trong bang
                    _db.SaveChanges(); //Luu thay doi
                    return RedirectToAction("LogIn"); //Neu dang ky thanh cong thi dieu huong ve trang dang nhap
                }
                else
                {
                    ViewBag.error = "Email already exists! Please use another email!";
                    return View();
                }
            }
            return View();
        }
    }
}
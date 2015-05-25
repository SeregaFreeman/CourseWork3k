using CW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace CW.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private ShutterContext db = new ShutterContext();

        //LOGIN action
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LogViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "User");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(model);
        }

        //LOG OFF action
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        //VALIDATE function
        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;

            using (ShutterContext _db = new ShutterContext())
            {
                try
                {
                    User user = (from u in _db.Users
                                 where u.Login == login
                                 select u).FirstOrDefault();

                    if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                    {
                        isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        //REGISTER action

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            int id = 3;
            var role = db.Roles.Find(id);
            if (role != null)
            {
                user.RoleId = role.Id;
            }
            user.Password = Crypto.HashPassword(user.Password);
                
            if (ModelState.IsValid)
            {
                
                
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index", "User");
            }

            return View(user);
        }
    }
}

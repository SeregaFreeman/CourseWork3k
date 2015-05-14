using Shutter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Shutter.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private ShutterContext db = new ShutterContext();

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
                        return RedirectToAction("Index", "Post");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(model);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;

            using (ShutterContext _db = new ShutterContext())
            {
                try
                {
                    User user = (from u in _db.Users
                                 where u.Login == login && u.Password == password
                                 select u).FirstOrDefault();

                    if (user != null)
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


        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            SelectList roles = new SelectList(db.Roles, "Id", "Name");
            ViewBag.Roles = roles;

            return View(user);
        }
    }
}

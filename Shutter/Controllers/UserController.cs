using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shutter.Models;

namespace Shutter.Controllers
{
    [Authorize(Roles = "Admin, Moderator, User")]
    public class UserController : Controller
    {
        private ShutterContext db = new ShutterContext();

        [HttpGet]
        public ActionResult Index()
        {
                var users = db.Users.Include(u=>u.Role).ToList();
                return View(users);
                
        }
    }
 }

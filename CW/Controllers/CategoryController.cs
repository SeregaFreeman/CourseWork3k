using CW.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CW.Controllers
{
    public class CategoryController : Controller
    {
        ShutterContext db = new ShutterContext();

        // отображение категорий
        [HttpGet]
        public ActionResult Categories()
        {
            ViewBag.Categories = db.Categories;
            return View();
        }

        // Добавление категорий
        [HttpPost]
        public ActionResult Categories(Category cat)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(cat);
                db.SaveChanges();
            }
            ViewBag.Categories = db.Categories;
            return View(cat);
        }

        // Удаление категории по id
        public ActionResult DeleteCategory(int id)
        {
            Category cat = db.Categories.Find(id);
            db.Categories.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("Categories");
        }

    }
}

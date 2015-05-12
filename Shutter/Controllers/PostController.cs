using Shutter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Shutter.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        ShutterContext db = new ShutterContext();
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            // получаем текущего пользователя
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");
                return View();
            }
            return RedirectToAction("LogOff", "Account");
        }

        // Создание новой заявки
        [HttpPost]
        public ActionResult Create(Post post, HttpPostedFileBase error)
        {
            // получаем текущего пользователя
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("LogOff", "Account");
            }
            if (ModelState.IsValid)
            {
                // указываем статус добавлен у поста
                post.Status = (int)PostStatus.Posted;
                //получаем время открытия
                DateTime current = DateTime.Now;

                //Создаем запись о жизненном цикле поста
                Lifecycle newLifecycle = new Lifecycle() { Posted = current };
                post.Lifecycle = newLifecycle;

                //Добавляем жизненный цикл поста
                db.Lifecycles.Add(newLifecycle);

                // указываем пользователя поста
                post.UserId = user.Id;

                // если получен файл
                if (error != null)
                {
                    // Получаем расширение
                    string ext = error.FileName.Substring(error.FileName.LastIndexOf('.'));
                    // сохраняем файл по определенному пути на сервере
                    string path = current.ToString("dd/MM/yyyy H:mm:ss").Replace(":", "_").Replace("/", ".") + ext;
                    error.SaveAs(Server.MapPath("~/Files/" + path));
                    post.File = path;
                }
                //Добавляем заявку
                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(post);
        }

    }
}

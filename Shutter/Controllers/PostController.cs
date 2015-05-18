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
            // получаем текущего пользователя
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).FirstOrDefault();

            var posts = db.Posts.Where(r => r.UserId == user.Id) //получаем посты для текущего пользователя
                                    .Include(r => r.Category)  // добавляем категории
                                    .Include(r => r.Lifecycle)  // добавляем жизненный цикл постов
                                    .Include(r => r.User)         // добавляем данные о пользователях
                                    .OrderByDescending(r => r.Lifecycle.Posted); // упорядочиваем по дате по убыванию   

            return View(posts);
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

        // Создание нового поста
        [HttpPost]
        public ActionResult Create(Post post, HttpPostedFileBase picture)
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
                if (picture != null)
                {
                    // Получаем расширение
                    string ext = picture.FileName.Substring(picture.FileName.LastIndexOf('.'));
                    // сохраняем файл по определенному пути на сервере
                    string path = current.ToString("dd/MM/yyyy H:mm:ss").Replace(":", "_").Replace("/", ".") + ext;
                    picture.SaveAs(Server.MapPath("~/Uploads/" + path));
                    post.File = path;
                }
                //Добавляем пост в БД
                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(post);
        }

        // Удаление поста
        public ActionResult Delete(int id)
        {
            Post post = db.Posts.Find(id);
            // получаем текущего пользователя
            User user = db.Users.Where(m => m.Login == HttpContext.User.Identity.Name).First();
            if (post != null && post.UserId == user.Id)
            {
                Lifecycle lifecycle = db.Lifecycles.Find(post.LifecycleId);
                db.Lifecycles.Remove(lifecycle);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // действия для модератора
        [HttpGet]
        [Authorize(Roles = "Moderator")]
        public ActionResult Approve()
        {
            var posts = db.Posts.Include(r => r.User)
                                    .Include(r => r.Lifecycle)
                                    .Where(r => r.Status != (int)PostStatus.Approved);
            
            return View(posts);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator")]
        public ActionResult Approve(int? postId)
        {
            if (postId == null)
            {
                return RedirectToAction("Approve");
            }

            Post post = db.Posts.Find(postId);
            if (post == null)
            {
                return RedirectToAction("Approve");
            }
            
            post.Status = (int)PostStatus.Approved;
            Lifecycle lifecycle = db.Lifecycles.Find(post.LifecycleId);
            lifecycle.Approved = DateTime.Now;
            db.Entry(lifecycle).State = EntityState.Modified;

            db.Entry(post).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Approve");
        }
    }
}

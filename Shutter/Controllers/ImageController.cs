using Shutter.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shutter.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Uploader()
        {
            return View();
        }

        [HttpPost]
          public ActionResult Uploader(FormCollection form)
         {
       HttpPostedFileBase hpf = Request.Files["imagefile"] as HttpPostedFileBase;
              ImageManager.SaveImage(hpf);
   
              return RedirectToAction("uploader");
      }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PhotoController : Controller
    {
        static DeletePhotoNeeds needs = new DeletePhotoNeeds("mip");


        // GET: Photo
        public ActionResult Edit(string mip)
        {
            needs = new DeletePhotoNeeds(mip);
            return View(needs);
        }







    }
}
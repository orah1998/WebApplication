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
        static DeletePhotoNeeds needs = new DeletePhotoNeeds();



        public ActionResult Photos()
        {
            Photo pics = new Photo();
            return View(pics);
        }




        // GET: Photo
        public ActionResult Delete(string mip)
        {
            needs = new DeletePhotoNeeds(mip);
            return View(needs);
        }



        public ActionResult DeletePhoto()
        {
            needs.deletePhoto();
            return null;
        }



        public ActionResult view(string mip)
        {
            needs = new DeletePhotoNeeds(mip);
            mip = needs.deleting;
            string res = "";
            foreach (string item in mip.Split('\\'))
            {
                if (!item.Contains("Thumbnails"))
                {
                    res += item + "\\";
                }
            }
            
            res = res.Remove(res.Length - 1);


            needs = new DeletePhotoNeeds(res);
            return View(needs);
        }








    }
}
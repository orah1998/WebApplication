using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class FirstController : Controller
    {

        
        
       static DeletePhotoNeeds needs = new DeletePhotoNeeds("mip");
       static ToDeleteClient cli = new ToDeleteClient("mip");




        public ActionResult DeleteHandler()
        {
            cli.DeleteHandler();
            return null;
        }









        // GET: First
        public ActionResult ImageWeb()
        {
            ImageWebNeeds need = new ImageWebNeeds();
            return View(need);
        }

        
        public ActionResult Config()
        {
            ConfigClient client = new ConfigClient();
            return View(client);
        }

        
        
        // GET: First/Photos
        

        // GET: First/Create
        public ActionResult Create()
        {
            return View();
        }

        



        


        public ActionResult Edit(string mip)
        {
            needs= new DeletePhotoNeeds(mip);
            return View(needs);
        }




        
        [HttpGet]
        public ActionResult Confirmation(string mip)
        {
            cli = new ToDeleteClient(mip);
            return View(cli);
        }




    }
}

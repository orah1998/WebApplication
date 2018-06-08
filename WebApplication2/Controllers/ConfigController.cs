using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ConfigController : Controller
    {
        
        string str=null;
       static ToDeleteClient needs = new ToDeleteClient("mip");



        public ActionResult DeleteHandler()
        {
            needs.DeleteHandler();
            return null;
        }


       [HttpGet]
        public ActionResult Confirmation(string mip)
        {
            ToDeleteClient client = new ToDeleteClient(mip);
            return View(client);
        }




    }
}

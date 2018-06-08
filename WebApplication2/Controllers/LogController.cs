using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        public ActionResult Log()
        {
            LogNeeds need = new LogNeeds();
            return View(need);
        }


        [HttpPost]
        public ActionResult Log(LogNeeds logneeds)
        {
            return View(logneeds);
        }




    }
}
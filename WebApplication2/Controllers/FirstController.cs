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
        static List<Employee> employees = new List<Employee>()
        {
          new Employee  { ImagePath = "C:\\Users\\Operu\\Desktop\\Photo\\Naruto.png", LastName = "Aron", Email = "Stam@stam", Salary = 10000, Phone = "08-8888888" },
          new Employee  { ImagePath = "C:\\Users\\Operu\\Desktop\\Photo\\Sasuke.png", LastName = "Nisim", Email = "Stam@stam", Salary = 2000, Phone = "08-8888888" },
          new Employee   { ImagePath = "C:\\Users\\Operu\\Desktop\\Photo\\Kakashi.png", LastName = "Sinai", Email = "Stam@stam", Salary = 500, Phone = "08-8888888" }
        };

        
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

        [HttpGet]
        public JObject GetEmployee()
        {
            JObject data = new JObject();
            data["FirstName"] = "Kuky";
            data["LastName"] = "Mopy";
            return data;
        }

        [HttpPost]
        public JObject GetEmployee(string name, int salary)
        {
            foreach (var empl in employees)
            {
                if (empl.Salary > salary || name.Equals(name))
                {
                    JObject data = new JObject();
                    data["FirstName"] = empl.ImagePath;
                    data["LastName"] = empl.LastName;
                    data["Salary"] = empl.Salary;
                    return data;
                }
            }
            return null;
        }

        // GET: First/Photos
        

        // GET: First/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: First/Create
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                employees.Add(emp);

                return RedirectToAction("Photos");
            }
            catch
            {
                return View();
            }
        }





        public ActionResult DeletePhoto()
        {
            needs.deletePhoto();
            return null;
        }



        public ActionResult Edit(string mip)
        {
            needs= new DeletePhotoNeeds(mip);
            return View(needs);
        }





        // GET: First/Delete/5
        public ActionResult Delete(int id)
        {
            int i = 0;
            foreach (Employee emp in employees)
            {
                if (emp.ID.Equals(id))
                {
                    return View(emp);
                }
                i++;
            }
            return RedirectToAction("Error");
        }


        [HttpGet]
        public ActionResult Confirmation(string mip)
        {
            cli = new ToDeleteClient(mip);
            return View(cli);
        }




    }
}

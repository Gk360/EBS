using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeTravel.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;


using System.Data;
using System.Net;

namespace EmployeeTravel.Controllers
{
    public class EmployeeController : Controller
    {
        Entities dbcontext = new Entities();
        Entities1 db = new Entities1();



        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Home()
        {
            return View();
        }


        // GET: Employee
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]

        public ActionResult Login(string txtEmail,string txtpass)
        {
            //{

            //    SqlConnection cn = null;
            //    SqlCommand cmd = null;
            //    SqlDataReader dr = null;

            //    var query = cmd.CommandText = "SELECT EmployeeEmail FROM Group3.Employee";
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Connection = cn;

            //    cn.Open();

            //    if (employee.EmployeeEmail == query)
            //    {
            //        return RedirectToAction("/Rapport", employee);
            //    }

            //    cn.Close();

            //    return View();

            //}
            bool islogin = false;

            List<Employee> emplist = dbcontext.Employees.ToList();
            var logincheck = (from check in emplist
                              where check.EmployeeEmail == txtEmail && check.EmployeePassword == txtpass
                              select check).ToList();


            if(logincheck)

            return View("Home");
          






        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Register(Employee obj)

        {
            if (ModelState.IsValid)
            {
                Entities db = new Entities();
                db.Employees.Add(obj);
                db.SaveChanges();
            }
             return View(obj);
           
        }


        public ActionResult Travel()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Travel(Travel obj)

        {
            if (ModelState.IsValid)
            {
                Entities1 db = new Entities1();
                db.Travels.Add(obj);
                db.SaveChanges();
            }
            return View(obj);

        }


        public ActionResult FilterSearch(int? id)

        {



            var result = from p in db.Travels

                         where p.TravelID == id

                         select p;

            return View(result.ToList());





        }



        public ActionResult FilterCancelView(int? id)

        {
            var result = from p in db.Travels

                         where p.TravelID == id

                         select p;

            return View(result.ToList());

        }



        public ActionResult ViewOrder()

        {

            return View(db.Travels.ToList());

        }



        [HttpGet]
        public ActionResult Delete(int? id)

        {

            if (id == null)

            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            Travel travel = db.Travels.Find(id);

            if (travel == null)

            {

                return HttpNotFound();

            }

            return View(travel);

        }



        [HttpPost, ActionName("Delete")]

        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id)

        {

            Travel travel = db.Travels.Find(id);

            db.Travels.Remove(travel);

            db.SaveChanges();

            return RedirectToAction("Travel");

        }






















    }
}
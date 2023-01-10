using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hotel_Management_System.Models;

namespace Hotel_Management_System.Controllers
{
    public class EmployeesController : Controller
    {
        private Hotel_Management_SystemEntities db = new Hotel_Management_SystemEntities();

        // GET: Employees
        public ActionResult Index()
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                return View(db.Employees.ToList());
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Employee_ID,Employee_First_Name,Employee_Last_Name,Username,Password,Employee_Department,Employee_Address,Employee_Salary")] Employee employee)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (ModelState.IsValid)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Employees_Contact_No_s");
                }

                return View(employee);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Employee_ID,Employee_First_Name,Employee_Last_Name,Username,Password,Employee_Department,Employee_Address,Employee_Salary")] Employee employee)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employee employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                Employee employee = db.Employees.Find(id);
                db.Employees.Remove(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
    public enum Employees_Departments_Names
    {
        Owner, Management, Security, Food_and_Beverages, HouseKeeping, Maintenance
    }
}

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
    public class Employees_Contact_No_sController : Controller
    {
        private Hotel_Management_SystemEntities db = new Hotel_Management_SystemEntities();

        // GET: Employees_Contact_No_s
        public ActionResult Index()
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                var employees_Contact_No_s = db.Employees_Contact_No_s.Include(e => e.Employee).Include(e => e.Employee1);
                return View(employees_Contact_No_s.ToList());
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Employees_Contact_No_s/Details/5
        public ActionResult Details(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employees_Contact_No_s employees_Contact_No_s = db.Employees_Contact_No_s.Find(id);
                if (employees_Contact_No_s == null)
                {
                    return HttpNotFound();
                }
                return View(employees_Contact_No_s);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Employees_Contact_No_s/Create
        public ActionResult Create()
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_First_Name");
                ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_First_Name");
                return View();
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Employees_Contact_No_s/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ECID,Employee_ID,Employee_Contact_No_s")] Employees_Contact_No_s employees_Contact_No_s)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (ModelState.IsValid)
                {
                    db.Employees_Contact_No_s.Add(employees_Contact_No_s);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_First_Name", employees_Contact_No_s.Employee_ID);
                ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_First_Name", employees_Contact_No_s.Employee_ID);
                return View(employees_Contact_No_s);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Employees_Contact_No_s/Edit/5
        public ActionResult Edit(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employees_Contact_No_s employees_Contact_No_s = db.Employees_Contact_No_s.Find(id);
                if (employees_Contact_No_s == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_First_Name", employees_Contact_No_s.Employee_ID);
                ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_First_Name", employees_Contact_No_s.Employee_ID);
                return View(employees_Contact_No_s);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Employees_Contact_No_s/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ECID,Employee_ID,Employee_Contact_No_s")] Employees_Contact_No_s employees_Contact_No_s)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(employees_Contact_No_s).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_First_Name", employees_Contact_No_s.Employee_ID);
                ViewBag.Employee_ID = new SelectList(db.Employees, "Employee_ID", "Employee_First_Name", employees_Contact_No_s.Employee_ID);
                return View(employees_Contact_No_s);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Employees_Contact_No_s/Delete/5
        public ActionResult Delete(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Employees_Contact_No_s employees_Contact_No_s = db.Employees_Contact_No_s.Find(id);
                if (employees_Contact_No_s == null)
                {
                    return HttpNotFound();
                }
                return View(employees_Contact_No_s);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Employees_Contact_No_s/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                Employees_Contact_No_s employees_Contact_No_s = db.Employees_Contact_No_s.Find(id);
                db.Employees_Contact_No_s.Remove(employees_Contact_No_s);
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
}

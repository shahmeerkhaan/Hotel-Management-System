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
    public class EMPLOYEES_VIEWController : Controller
    {
        private Hotel_Management_SystemEntities db = new Hotel_Management_SystemEntities();

        // GET: EMPLOYEES_VIEW
        public ActionResult Index()
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                return View(db.EMPLOYEES_VIEWs.ToList());
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: EMPLOYEES_VIEW/Details/5
        public ActionResult Details(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EMPLOYEES_VIEW eMPLOYEES_VIEW = db.EMPLOYEES_VIEWs.Find(id);
                if (eMPLOYEES_VIEW == null)
                {
                    return HttpNotFound();
                }
                return View(eMPLOYEES_VIEW);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: EMPLOYEES_VIEW/Create
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

        // POST: EMPLOYEES_VIEW/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Employee_ID,Employee_First_name,Employee_Last_name,Username,Password,Employee_Department,Employee_Address,Employee_Salary,Employee_Contact_No_s")] EMPLOYEES_VIEW eMPLOYEES_VIEW)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (ModelState.IsValid)
                {
                    db.EMPLOYEES_VIEWs.Add(eMPLOYEES_VIEW);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(eMPLOYEES_VIEW);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: EMPLOYEES_VIEW/Edit/5
        public ActionResult Edit(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EMPLOYEES_VIEW eMPLOYEES_VIEW = db.EMPLOYEES_VIEWs.Find(id);
                if (eMPLOYEES_VIEW == null)
                {
                    return HttpNotFound();
                }
                return View(eMPLOYEES_VIEW);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: EMPLOYEES_VIEW/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Employee_ID,Employee_First_name,Employee_Last_name,Username,Password,Employee_Department,Employee_Address,Employee_Salary,Employee_Contact_No_s")] EMPLOYEES_VIEW eMPLOYEES_VIEW)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(eMPLOYEES_VIEW).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(eMPLOYEES_VIEW);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: EMPLOYEES_VIEW/Delete/5
        public ActionResult Delete(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                EMPLOYEES_VIEW eMPLOYEES_VIEW = db.EMPLOYEES_VIEWs.Find(id);
                if (eMPLOYEES_VIEW == null)
                {
                    return HttpNotFound();
                }
                return View(eMPLOYEES_VIEW);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: EMPLOYEES_VIEW/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                EMPLOYEES_VIEW eMPLOYEES_VIEW = db.EMPLOYEES_VIEWs.Find(id);
                db.EMPLOYEES_VIEWs.Remove(eMPLOYEES_VIEW);
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

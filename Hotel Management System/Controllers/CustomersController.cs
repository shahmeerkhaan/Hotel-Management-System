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
    public class CustomersController : Controller
    {
        private Hotel_Management_SystemEntities db = new Hotel_Management_SystemEntities();
        // GET: Customers
        public ActionResult Index()
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                return View(db.Customers.ToList());
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }
        public ActionResult Indexx()
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin != true)
            {
                return View(db.Customers.ToList());
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsValidUser)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer_ID,Customer_First_name,Customer_Last_name,Customer_Email_Address,Customer_Address,No__of_Peoples,Username,Password")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("", "Customer_s_Phone_No_s/Create");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsValidUser)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer_ID,Customer_First_name,Customer_Last_name,Customer_Email_Address,Customer_Address,No__of_Peoples,Username,Password")] Customer customer)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsValidUser)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(customer);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsValidUser)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer customer = db.Customers.Find(id);
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsValidUser)
            {
                Customer customer = db.Customers.Find(id);
                db.Customers.Remove(customer);
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

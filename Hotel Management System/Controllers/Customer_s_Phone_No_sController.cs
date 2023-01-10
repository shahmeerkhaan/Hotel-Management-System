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
    public class Customer_s_Phone_No_sController : Controller
    {
        private Hotel_Management_SystemEntities db = new Hotel_Management_SystemEntities();

        // GET: Customer_s_Phone_No_s
        public ActionResult Index()
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                var customer_s_Phone_No_s = db.Customer_s_Phone_No_s.Include(c => c.Customer).Include(c => c.Customer1);
                return View(customer_s_Phone_No_s.ToList());
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
                var customer_s_Phone_No_s = db.Customer_s_Phone_No_s.Include(c => c.Customer).Include(c => c.Customer1);
                return View(customer_s_Phone_No_s.ToList());
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Customer_s_Phone_No_s/Details/5
        public ActionResult Details(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer_s_Phone_No_s customer_s_Phone_No_s = db.Customer_s_Phone_No_s.Find(id);
                if (customer_s_Phone_No_s == null)
                {
                    return HttpNotFound();
                }
                return View(customer_s_Phone_No_s);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Customer_s_Phone_No_s/Create
        public ActionResult Create()
        {
            ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name");
            ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name");
            return View();
        }

        // POST: Customer_s_Phone_No_s/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CPID,Customer_ID,Customer_Phone_No_s")] Customer_s_Phone_No_s customer_s_Phone_No_s)
        {
            if (ModelState.IsValid)
            {
                db.Customer_s_Phone_No_s.Add(customer_s_Phone_No_s);
                db.SaveChanges();
                return RedirectToAction("", "Home/Login");
            }

            ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", customer_s_Phone_No_s.Customer_ID);
            ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", customer_s_Phone_No_s.Customer_ID);
            return View(customer_s_Phone_No_s);
        }

        //public List<SelectListItem> CNAMESINCPID()
        //{
        //    List<SelectListItem> CNAMESINCPID = new List<SelectListItem>(); 
        //    SelectList cnamesincpid = new SelectList(db.Customers, "Customer_ID", "Customer_First_name");
        //    foreach (var C_Names_ID in cnamesincpid)
        //    {
        //        CNAMESINCPID.Add(C_Names_ID);
        //    }
        //    return CNAMESINCPID;
        //}

        // GET: Customer_s_Phone_No_s/Edit/5
        public ActionResult Edit(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer_s_Phone_No_s customer_s_Phone_No_s = db.Customer_s_Phone_No_s.Find(id);
                if (customer_s_Phone_No_s == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", customer_s_Phone_No_s.Customer_ID);
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", customer_s_Phone_No_s.Customer_ID);
                return View(customer_s_Phone_No_s);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Customer_s_Phone_No_s/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CPID,Customer_ID,Customer_Phone_No_s")] Customer_s_Phone_No_s customer_s_Phone_No_s)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(customer_s_Phone_No_s).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", customer_s_Phone_No_s.Customer_ID);
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", customer_s_Phone_No_s.Customer_ID);
                return View(customer_s_Phone_No_s);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Customer_s_Phone_No_s/Delete/5
        public ActionResult Delete(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Customer_s_Phone_No_s customer_s_Phone_No_s = db.Customer_s_Phone_No_s.Find(id);
                if (customer_s_Phone_No_s == null)
                {
                    return HttpNotFound();
                }
                return View(customer_s_Phone_No_s);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Customer_s_Phone_No_s/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                Customer_s_Phone_No_s customer_s_Phone_No_s = db.Customer_s_Phone_No_s.Find(id);
                db.Customer_s_Phone_No_s.Remove(customer_s_Phone_No_s);
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

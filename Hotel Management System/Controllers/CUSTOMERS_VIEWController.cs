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
    public class CUSTOMERS_VIEWController : Controller
    {
        private Hotel_Management_SystemEntities db = new Hotel_Management_SystemEntities();

        // GET: CUSTOMERS_VIEW
        public ActionResult Index()
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                return View(db.CUSTOMERS_VIEWs.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public ActionResult Indexx()
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin != true)
            {
                return View(db.CUSTOMERS_VIEWs.ToList());
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: CUSTOMERS_VIEW/Details/5
        public ActionResult Details(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CUSTOMERS_VIEW cUSTOMERS_VIEW = db.CUSTOMERS_VIEWs.SingleOrDefault(model => model.Customer_ID == id);
                if (cUSTOMERS_VIEW == null)
                {
                    return HttpNotFound();
                }
                return View(cUSTOMERS_VIEW);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: CUSTOMERS_VIEW/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CUSTOMERS_VIEW/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer_ID,Customer_First_name,Customer_Last_name,Customer_Address,No__of_Peoples,Customer_Phone_No_s")] CUSTOMERS_VIEW cUSTOMERS_VIEW)
        {
            if (ModelState.IsValid)
            {
                db.CUSTOMERS_VIEWs.Add(cUSTOMERS_VIEW);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cUSTOMERS_VIEW);
        }

        // GET: CUSTOMERS_VIEW/Edit/5
        public ActionResult Edit(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CUSTOMERS_VIEW cUSTOMERS_VIEW = db.CUSTOMERS_VIEWs.Find(id);
                if (cUSTOMERS_VIEW == null)
                {
                    return HttpNotFound();
                }
                return View(cUSTOMERS_VIEW);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: CUSTOMERS_VIEW/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer_ID,Customer_First_name,Customer_Last_name,Customer_Address,No__of_Peoples,Customer_Phone_No_s")] CUSTOMERS_VIEW cUSTOMERS_VIEW)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cUSTOMERS_VIEW).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(cUSTOMERS_VIEW);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: CUSTOMERS_VIEW/Delete/5
        public ActionResult Delete(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CUSTOMERS_VIEW cUSTOMERS_VIEW = db.CUSTOMERS_VIEWs.Find(id);
                if (cUSTOMERS_VIEW == null)
                {
                    return HttpNotFound();
                }
                return View(cUSTOMERS_VIEW);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: CUSTOMERS_VIEW/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                CUSTOMERS_VIEW cUSTOMERS_VIEW = db.CUSTOMERS_VIEWs.Find(id);
                db.CUSTOMERS_VIEWs.Remove(cUSTOMERS_VIEW);
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

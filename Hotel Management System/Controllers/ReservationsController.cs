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
    public class ReservationsController : Controller
    {
        private Hotel_Management_SystemEntities db = new Hotel_Management_SystemEntities();

        // GET: Reservations
        public ActionResult Index()
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                var reservations = db.Reservations.Include(r => r.Customer).Include(r => r.Customer1).Include(r => r.Transaction).Include(r => r.Transaction1);
                return View(reservations.ToList());
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
                var reservations = db.Reservations.Include(r => r.Customer).Include(r => r.Customer1).Include(r => r.Transaction).Include(r => r.Transaction1);
                return View(reservations.ToList());
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsValidUser)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                return View(reservation);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            if (GlobalVariables.LOGINAUTHENTICATION())
            {
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name");
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name");
                ViewBag.Transaction_ID = new SelectList(db.Transactions, "Transaction_ID", "Transaction_Name");
                ViewBag.Transaction_ID = new SelectList(db.Transactions, "Transaction_ID", "Transaction_Name");
                return View();
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }
        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Reservation_ID,Customer_ID,Date_In,Date_Out,Transaction_ID")] Reservation reservation)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() && GlobalVariables.IsValidCustomerUser)
            {
                if (ModelState.IsValid)
                {
                    GlobalVariables.TID = reservation.Transaction_ID;
                    GlobalVariables.CID = reservation.Customer_ID;
                    GlobalVariables.DIN = reservation.Date_In;
                    GlobalVariables.DOT = reservation.Date_Out;
                    GlobalVariables.DAYSOFINTERVAL = db.sp_IOS(GlobalVariables.TID, GlobalVariables.CID, GlobalVariables.DIN, GlobalVariables.DOT);
                    reservation.Days_Interval_of_Stay = GlobalVariables.DAYSOFINTERVAL;
                    db.SaveChanges();
                    return RedirectToAction("", "Rooms/Create");
                }

                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", reservation.Customer_ID);
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", reservation.Customer_ID);
                ViewBag.Transaction_ID = new SelectList(db.Transactions, "Transaction_ID", "Transaction_Name", reservation.Transaction_ID);
                ViewBag.Transaction_ID = new SelectList(db.Transactions, "Transaction_ID", "Transaction_Name", reservation.Transaction_ID);
                return View(reservation);
            }
            else if (GlobalVariables.LOGINAUTHENTICATION() && GlobalVariables.IsValidUser)
            {
                if (ModelState.IsValid)
                {
                    GlobalVariables.TID = reservation.Transaction_ID;
                    GlobalVariables.CID = reservation.Customer_ID;
                    GlobalVariables.DIN = reservation.Date_In;
                    GlobalVariables.DOT = reservation.Date_Out;
                    GlobalVariables.DAYSOFINTERVAL = db.sp_IOS(GlobalVariables.TID, GlobalVariables.CID, GlobalVariables.DIN, GlobalVariables.DOT);
                    reservation.Days_Interval_of_Stay = GlobalVariables.DAYSOFINTERVAL;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", reservation.Customer_ID);
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", reservation.Customer_ID);
                ViewBag.Transaction_ID = new SelectList(db.Transactions, "Transaction_ID", "Transaction_Name", reservation.Transaction_ID);
                ViewBag.Transaction_ID = new SelectList(db.Transactions, "Transaction_ID", "Transaction_Name", reservation.Transaction_ID);
                return View(reservation);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsValidUser)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", reservation.Customer_ID);
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", reservation.Customer_ID);
                ViewBag.Transaction_ID = new SelectList(db.Transactions, "Transaction_ID", "Transaction_Name", reservation.Transaction_ID);
                ViewBag.Transaction_ID = new SelectList(db.Transactions, "Transaction_ID", "Transaction_Name", reservation.Transaction_ID);
                return View(reservation);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Reservation_ID,Transaction_ID,Customer_ID,Date_In,Date_Out,Days_Interval_of_Stay")] Reservation reservation)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsValidUser)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(reservation).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", reservation.Customer_ID);
                ViewBag.Customer_ID = new SelectList(db.Customers, "Customer_ID", "Customer_First_name", reservation.Customer_ID);
                ViewBag.Transaction_ID = new SelectList(db.Transactions, "Transaction_ID", "Transaction_Name", reservation.Transaction_ID);
                ViewBag.Transaction_ID = new SelectList(db.Transactions, "Transaction_ID", "Transaction_Name", reservation.Transaction_ID);
                return View(reservation);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsValidUser)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reservation reservation = db.Reservations.Find(id);
                if (reservation == null)
                {
                    return HttpNotFound();
                }
                return View(reservation);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsValidUser)
            {
                Reservation reservation = db.Reservations.Find(id);
                db.Reservations.Remove(reservation);
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
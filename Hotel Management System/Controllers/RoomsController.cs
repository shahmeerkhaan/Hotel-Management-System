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
    public class RoomsController : Controller
    {
        private Hotel_Management_SystemEntities db = new Hotel_Management_SystemEntities();

        // GET: Rooms
        public ActionResult Index()
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true && GlobalVariables.IsAdmin == true)
            {
                var rooms = db.Rooms.Include(r => r.Reservation).Include(r => r.Reservation1);
                return View(rooms.ToList());
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
                var rooms = db.Rooms.Include(r => r.Reservation).Include(r => r.Reservation1);
                return View(rooms.ToList());
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Room room = db.Rooms.Find(id);
                if (room == null)
                {
                    return HttpNotFound();
                }
                return View(room);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            using (var HMSDB = new Hotel_Management_SystemEntities())
            {
                if (GlobalVariables.LOGINAUTHENTICATION())
                {
                    var ReservationIdCredentials = HMSDB.Reservations.OrderByDescending(loggedin => GlobalVariables.CustomerID == loggedin.Customer_ID).FirstOrDefault();
                    GlobalVariables.ReservationID = ReservationIdCredentials.Reservation_ID;
                    ViewBag.Reservation_ID = new SelectList(db.Reservations, "Reservation_ID", "Reservation_ID");
                    ViewBag.Reservation_ID = new SelectList(db.Reservations, "Reservation_ID", "Reservation_ID");
                    return View();
                }
                else
                {
                    return RedirectToAction("", "Home/Login");
                }
            }
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Room_No,Room_Category,Rent,Reservation_ID")] Room room)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() && GlobalVariables.IsValidCustomerUser)
            {
                if (ModelState.IsValid)
                {
                    db.Rooms.Add(room);
                    db.SaveChanges();
                    return RedirectToAction("", "Rooms/Create");
                }
                ViewBag.Reservation_ID = new SelectList(db.Reservations, "Reservation_ID", "Reservation_ID", room.Reservation_ID);
                ViewBag.Reservation_ID = new SelectList(db.Reservations, "Reservation_ID", "Reservation_ID", room.Reservation_ID);
                return View(room);
            }
            else if (GlobalVariables.LOGINAUTHENTICATION() && GlobalVariables.IsValidUser)
            {
                if (ModelState.IsValid)
                {
                    db.Rooms.Add(room);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Reservation_ID = new SelectList(db.Reservations, "Reservation_ID", "Reservation_ID", room.Reservation_ID);
                ViewBag.Reservation_ID = new SelectList(db.Reservations, "Reservation_ID", "Reservation_ID", room.Reservation_ID);
                return View(room);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Room room = db.Rooms.Find(id);
                if (room == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Reservation_ID = new SelectList(db.Reservations, "Reservation_ID", "Reservation_ID", room.Reservation_ID);
                ViewBag.Reservation_ID = new SelectList(db.Reservations, "Reservation_ID", "Reservation_ID", room.Reservation_ID);
                return View(room);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Room_No,Room_Category,Rent,Reservation_ID")] Room room)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(room).State = EntityState.Modified;
                    db.SaveChanges();
                    if (GlobalVariables.IsValidUser || GlobalVariables.IsAdmin)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Create");
                    }
                }
                ViewBag.Reservation_ID = new SelectList(db.Reservations, "Reservation_ID", "Reservation_ID", room.Reservation_ID);
                ViewBag.Reservation_ID = new SelectList(db.Reservations, "Reservation_ID", "Reservation_ID", room.Reservation_ID);
                if (GlobalVariables.IsValidUser || GlobalVariables.IsAdmin)
                {
                    return View(room);
                }
                else
                {
                    return RedirectToAction("Create");
                }
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Room room = db.Rooms.Find(id);
                if (room == null)
                {
                    return HttpNotFound();
                }
                return View(room);
            }
            else
            {
                return RedirectToAction("", "Home/Login");
            }
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (GlobalVariables.LOGINAUTHENTICATION() == true)
            {
                Room room = db.Rooms.Find(id);
                db.Rooms.Remove(room);
                db.SaveChanges();
                if (GlobalVariables.IsValidUser || GlobalVariables.IsAdmin)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create");
                }
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

    public enum Room_Category
    {
        Single_Room, Double_Room, Triple_Room, Quad_Room, King_Room
    }
}

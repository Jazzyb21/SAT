﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.DATA.EF;
using PagedList;
using PagedList.Mvc;

namespace SAT.UI.MVC.Controllers
{
    public class StudentsController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: Students
        [Authorize]
        public ActionResult Index(/*string searchString, string currentSearch, int page = 1*/)
        {
            var students = db.Students.Include(s => s.StudentStatus);
            //if (searchString !=null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    searchString = currentSearch;
            //}

            //if (!String.IsNullOrEmpty(searchString))
            //{

            //}
            //int pageNumber = 2;
            return View(students.ToList());
        }

   

        // GET: Students/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase profilePic)
        {
            if (ModelState.IsValid)
            {
                #region Image Upload

                string imgName = "default.jpg";

                if (profilePic != null)
                {
                    string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imgName = Guid.NewGuid() + ext;

                        profilePic.SaveAs(Server.MapPath("~/Content/images/" + imgName));
                    }
                    else
                    {
                        imgName = "default.jpg";
                    }
                }

                student.PhotoUrl = imgName;


                #endregion
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase profilePic)
        {
            if (ModelState.IsValid)
            {
                #region Image Upload

                string imgName = "default.jpg";

                if (profilePic != null)
                {
                    string[] goodExts = { ".jpg", ".jpeg", ".gif", ".png" };

                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imgName = Guid.NewGuid() + ext;

                        profilePic.SaveAs(Server.MapPath("~/Content/images/" + imgName));
                    }
                    else
                    {
                        imgName = "default.jpg";
                    }
                }

                student.PhotoUrl = imgName;


                #endregion

                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            if (student.SSID == 2) //if student status is current
            {
                db.Students.Find(id).SSID = 3;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (student.SSID == 3) //if student is withdrawn
            {
                db.Students.Find(id).SSID = 2;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit", new { id = id });
            }

            //return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            if (student.SSID == 2) //if student status is current
            {
                db.Students.Find(id).SSID = 3;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (student.SSID == 3) //if student is withdrawn
            {
                db.Students.Find(id).SSID = 2;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit", id);
            }
            ////db.Students.Remove(student);
            //db.SaveChanges();
            //return RedirectToAction("Index");
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

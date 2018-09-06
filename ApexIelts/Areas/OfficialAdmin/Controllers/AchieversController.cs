using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminPaneNew.Areas.OfficialAdmin.Models;
using onlineportal.Areas.AdminPanel.Models;

namespace AdminPaneNew.Areas.OfficialAdmin.Controllers
{
    public class AchieversController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img;
        // GET: OfficialAdmin/Achievers
        public ActionResult Index()
        {
            return View(db.Achievers.ToList());
        }

        // GET: OfficialAdmin/Achievers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievers achievers = db.Achievers.Find(id);
            if (achievers == null)
            {
                return HttpNotFound();
            }
            return View(achievers);
        }

        // GET: OfficialAdmin/Achievers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OfficialAdmin/Achievers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Achieverid,Name,ShortDescription,Image,Course,date,Reading,Writing,Listening,Speaking,Overall")] Achievers achievers, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                achievers.date = System.DateTime.Now;
                achievers.Image = Help.uploadfile(file);
                db.Achievers.Add(achievers);
                db.SaveChanges();
                TempData["Success"] = "Saved Successfully";
                return RedirectToAction("Index");
            }

            return View(achievers);
        }

        // GET: OfficialAdmin/Achievers/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievers achievers = db.Achievers.Find(id);
            img = achievers.Image;
            if (achievers == null)
            {
                return HttpNotFound();
            }
            return View(achievers);
        }

        // POST: OfficialAdmin/Achievers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Achieverid,Name,ShortDescription,Image,Course,date,Reading,Writing,Listening,Speaking,Overall")] Achievers achievers, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                achievers.date = System.DateTime.Now;
                achievers.Image = file != null ? Help.uploadfile(file) : img;
                #region delete file
                string fullPath = Request.MapPath("~/UploadedFiles/" + img);
                if (img == achievers.Image)
                {
                }
                else
                {
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                #endregion
                db.Entry(achievers).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(achievers);
        }

        // GET: OfficialAdmin/Achievers/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievers achievers = db.Achievers.Find(id);
            if (achievers == null)
            {
                return HttpNotFound();
            }
            return View(achievers);
        }

        // POST: OfficialAdmin/Achievers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Achievers achievers = db.Achievers.Find(id);
            db.Achievers.Remove(achievers);
            db.SaveChanges();
            TempData["Success"] = "Deleted Successfully";
            return RedirectToAction("Index");
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

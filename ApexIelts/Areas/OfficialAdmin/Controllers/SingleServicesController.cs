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
    public class SingleServicesController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img;
        // GET: OfficialAdmin/SingleServices
        public ActionResult Index()
        {
            var singleServices = db.SingleServices.Include(s => s.Service);
            return View(singleServices.ToList());
        }

        // GET: OfficialAdmin/SingleServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SingleService singleService = db.SingleServices.Find(id);
            if (singleService == null)
            {
                return HttpNotFound();
            }
            return View(singleService);
        }

        // GET: OfficialAdmin/SingleServices/Create
        public ActionResult Create()
        {
            ViewBag.Serviceid = new SelectList(db.Services, "Serviceid", "Name");
            return View();
        }

        // POST: OfficialAdmin/SingleServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Singleid,Serviceid,Name,Description,Image,date,Keyword,MetaDescription")] SingleService singleService, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                singleService.date = System.DateTime.Now;
                singleService.Image = Help.uploadfile(file);
                db.SingleServices.Add(singleService);
                db.SaveChanges();
                TempData["Success"] = "Saved Successfully";
                return RedirectToAction("Index");
            }

            ViewBag.Serviceid = new SelectList(db.Services, "Serviceid", "Name", singleService.Serviceid);
            return View(singleService);
        }

        // GET: OfficialAdmin/SingleServices/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SingleService singleService = db.SingleServices.Find(id);
            img = singleService.Image;
            if (singleService == null)
            {
                return HttpNotFound();
            }
            ViewBag.Serviceid = new SelectList(db.Services, "Serviceid", "Name", singleService.Serviceid);
            return View(singleService);
        }

        // POST: OfficialAdmin/SingleServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Singleid,Serviceid,Name,Description,Image,date,Keyword,MetaDescription")] SingleService singleService, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                singleService.date = System.DateTime.Now;
                singleService.Image = file != null ? Help.uploadfile(file) : img;
                #region delete file
                string fullPath = Request.MapPath("~/UploadedFiles/" + img);
                if (img == singleService.Image)
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
                db.Entry(singleService).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            ViewBag.Serviceid = new SelectList(db.Services, "Serviceid", "Name", singleService.Serviceid);
            return View(singleService);
        }

        // GET: OfficialAdmin/SingleServices/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SingleService singleService = db.SingleServices.Find(id);
            if (singleService == null)
            {
                return HttpNotFound();
            }
            return View(singleService);
        }

        // POST: OfficialAdmin/SingleServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SingleService singleService = db.SingleServices.Find(id);
            db.SingleServices.Remove(singleService);
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

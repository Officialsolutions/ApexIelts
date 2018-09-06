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
    public class PagesController : Controller
    {
        private dbcontext db = new dbcontext();
        public static string img, thumb;
        // GET: OfficialAdmin/Pages
        public ActionResult Index()
        {
            return View(db.Pages.ToList());
        }

        // GET: OfficialAdmin/Pages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pages pages = db.Pages.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View(pages);
        }

        // GET: OfficialAdmin/Pages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OfficialAdmin/Pages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pagesid,Name,Description,ShortDescription,Image,date,Thumbnail,Keyword,MetaDescription")] Pages pages, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                pages.date = System.DateTime.Now;
                pages.Image = Help.uploadfile(file);
                pages.Thumbnail = Help.uploadfile(file);
                db.Pages.Add(pages);
                db.SaveChanges();
                TempData["Success"] = "Saved Successfully";
                return RedirectToAction("Index");
            }

            return View(pages);
        }

        // GET: OfficialAdmin/Pages/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pages pages = db.Pages.Find(id);
            img = pages.Image;
            thumb = pages.Thumbnail;
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View(pages);
        }

        // POST: OfficialAdmin/Pages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pagesid,Name,Description,ShortDescription,Image,date,Thumbnail,Keyword,MetaDescription")] Pages pages, HttpPostedFileBase file, Helper Help)
        {
            if (ModelState.IsValid)
            {
                pages.date = System.DateTime.Now;
                pages.Image = file != null ? Help.uploadfile(file) : img;
                #region delete file
                string fullPath = Request.MapPath("~/UploadedFiles/" + img);
                if (img == pages.Image)
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
                pages.Thumbnail = file != null ? Help.uploadfile(file) : thumb;
                #region delete file
                string fullPath2 = Request.MapPath("~/UploadedFiles/" + thumb);
                if (thumb == pages.Thumbnail)
                {
                }
                else
                {
                    if (System.IO.File.Exists(fullPath2))
                    {
                        System.IO.File.Delete(fullPath2);
                    }
                }
                #endregion
                db.Entry(pages).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(pages);
        }

        // GET: OfficialAdmin/Pages/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pages pages = db.Pages.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View(pages);
        }

        // POST: OfficialAdmin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pages pages = db.Pages.Find(id);
            db.Pages.Remove(pages);
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

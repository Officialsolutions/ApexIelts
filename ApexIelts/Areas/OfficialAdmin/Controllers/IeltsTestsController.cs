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
    public class IeltsTestsController : Controller
    {
        private dbcontext db = new dbcontext();
        Helper help = new Helper();
        public static string img,aud;
        // GET: OfficialAdmin/IeltsTests
        public ActionResult Index()
        {
            var ieltsTests = db.IeltsTests.Include(i => i.Category);
            return View(ieltsTests.ToList());
        }

        // GET: OfficialAdmin/IeltsTests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IeltsTest ieltsTest = db.IeltsTests.Find(id);
            if (ieltsTest == null)
            {
                return HttpNotFound();
            }
            return View(ieltsTest);
        }

        // GET: OfficialAdmin/IeltsTests/Create
        public ActionResult Create()
        {
            ViewBag.Categoryid = new SelectList(db.Categories, "Categoryid", "Name");
            return View();
        }

        // POST: OfficialAdmin/IeltsTests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ieltsid,Name,Categoryid,TestType,Image,Url,Audio,date")] IeltsTest ieltsTest, HttpPostedFileBase file,HttpPostedFileBase audio, Helper Help)
        {
            if (ModelState.IsValid)
            {
                ieltsTest.date = System.DateTime.Now;
                ieltsTest.Image = Help.uploadfile(file);
                ieltsTest.Audio = Help.uploadfile(audio);
                db.IeltsTests.Add(ieltsTest);
                db.SaveChanges();
                TempData["Success"] = "Saved Successfully";
                return RedirectToAction("Index");
            }
            ViewBag.Categoryid = new SelectList(db.Categories, "Categoryid", "Name", ieltsTest.Categoryid);
            return View(ieltsTest);
        }

        // GET: OfficialAdmin/IeltsTests/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IeltsTest ieltsTest = db.IeltsTests.Find(id);
            img = ieltsTest.Image;
            aud = ieltsTest.Audio;
            if (ieltsTest == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categoryid = new SelectList(db.Categories, "Categoryid", "Name", ieltsTest.Categoryid);
            return View(ieltsTest);
        }

        // POST: OfficialAdmin/IeltsTests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ieltsid,Name,Categoryid,TestType,Image,Url,Audio,date")] IeltsTest ieltsTest, HttpPostedFileBase file, HttpPostedFileBase audio, Helper Help)
        {
            if (ModelState.IsValid)
            {
                ieltsTest.date = System.DateTime.Now;
                ieltsTest.Image = file != null ? Help.uploadfile(file) : img;
           
                #region delete file
                string fullPath = Request.MapPath("~/UploadedFiles/" + img);
                if (img == ieltsTest.Image)
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
                ieltsTest.Audio = audio != null ? Help.uploadfile(audio) : aud;
                #region delete file
                string fullPath2 = Request.MapPath("~/UploadedFiles/" + aud);
                if (aud == ieltsTest.Audio)
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
                db.Entry(ieltsTest).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Success"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            ViewBag.Categoryid = new SelectList(db.Categories, "Categoryid", "Name", ieltsTest.Categoryid);
            return View(ieltsTest);
        }

        // GET: OfficialAdmin/IeltsTests/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IeltsTest ieltsTest = db.IeltsTests.Find(id);
            if (ieltsTest == null)
            {
                return HttpNotFound();
            }
            return View(ieltsTest);
        }

        // POST: OfficialAdmin/IeltsTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IeltsTest ieltsTest = db.IeltsTests.Find(id);
            db.IeltsTests.Remove(ieltsTest);
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
        [Authorize(Roles = "Admin")]
        public ActionResult AssignTest()
        {
            // ViewBag.Ieltsid = new SelectList(db.IeltsTests.Where(x => x.Category == null).ToList(), "Ieltsid", "Name");

            // return View(db.IeltsTests.Where(x => x.Categoryid ==null).ToList());
            return View(db.IeltsTests.ToList());
        }
        [HttpPost]
        public ActionResult AssignTest(int categoryname, IeltsTest ielts)
        {

            List<IeltsTest> ielts2 = new List<IeltsTest>();
            ielts2 = db.IeltsTests.Where(x => x.Categoryid == categoryname).ToList();
            return View(ielts2);
        }
        [HttpPost]
        //[HttpPost, ActionName("CheckTest")]
        //[ValidateAntiForgeryToken]
        public ActionResult CheckTest([Bind(Include = "AssignId,StudentId,Ieltsid,date,Status")] AssignTest assignTest, FormCollection coll, int[] departments, int[] departments2)
        {
            if (ModelState.IsValid)
            {
                assignTest.date = System.DateTime.Now;
                
                foreach (var a in departments2)
                {
                    foreach (var n in departments)
                    {
                        assignTest.Ieltsid = Convert.ToInt32(a);
                        assignTest.Studentid = Convert.ToInt32(n); 
                        assignTest.Status = "Active";
                        db.AssignTests.Add(assignTest);
                        db.SaveChanges();
                    }
                }
                TempData["Success"] = "Saved Successfully";
                return RedirectToAction("Index");
            }

            return View(assignTest);
        }
    }
}

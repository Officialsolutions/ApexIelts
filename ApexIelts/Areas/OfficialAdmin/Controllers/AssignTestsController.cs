using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminPaneNew.Areas.OfficialAdmin.Models;

namespace AdminPaneNew.Areas.OfficialAdmin.Controllers
{
    public class AssignTestsController : Controller
    {
        private dbcontext db = new dbcontext();

        // GET: OfficialAdmin/AssignTests
        public ActionResult Index()
        {
            var assignTests = db.AssignTests.Include(a => a.IeltsTest).Include(a => a.StudentDetail);
            return View(assignTests.ToList());
        }

        // GET: OfficialAdmin/AssignTests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignTest assignTest = db.AssignTests.Find(id);
            if (assignTest == null)
            {
                return HttpNotFound();
            }
            return View(assignTest);
        }

        // GET: OfficialAdmin/AssignTests/Create
      

        // GET: OfficialAdmin/AssignTests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignTest assignTest = db.AssignTests.Find(id);
            if (assignTest == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ieltsid = new SelectList(db.IeltsTests, "Ieltsid", "Name", assignTest.Ieltsid);
            ViewBag.Studentid = new SelectList(db.StudentDetails, "Studentid", "Name", assignTest.Studentid);
            return View(assignTest);
        }

        // POST: OfficialAdmin/AssignTests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Assignid,Studentid,Ieltsid,date,Status")] AssignTest assignTest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignTest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ieltsid = new SelectList(db.IeltsTests, "Ieltsid", "Name", assignTest.Ieltsid);
            ViewBag.Studentid = new SelectList(db.StudentDetails, "Studentid", "Name", assignTest.Studentid);
            return View(assignTest);
        }

        // GET: OfficialAdmin/AssignTests/Delete/5
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

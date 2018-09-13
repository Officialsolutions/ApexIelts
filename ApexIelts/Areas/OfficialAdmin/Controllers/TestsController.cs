using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminPaneNew.Areas.OfficialAdmin.Models;
using System.IO;
using onlineportal.Areas.AdminPanel.Models;

namespace AdminPaneNew.Areas.OfficialAdmin.Controllers
{
    public class TestsController : Controller
    {
        private dbcontext db = new dbcontext();
        // GET: OfficialAdmin/Tests
        public ActionResult Index()
        {
            return View(db.Tests.ToList());
        }

        // GET: OfficialAdmin/Tests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // GET: OfficialAdmin/Tests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OfficialAdmin/Tests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,zip")] Test test, HttpPostedFileBase zip)
        {
            if (ModelState.IsValid)
            {
                string isValid = string.Empty;
                try
                {
                    foreach (string upload in Request.Files)
                    {
                        if (Request.Files[upload].ContentType == "application/octet-stream") //Content type for .zip is application/octet-stream  
                        {
                            if (Request.Files[upload].FileName != "")
                            {
                                string path = AppDomain.CurrentDomain.BaseDirectory + "/UploadedFiles/";

                                //  string path = AppDomain.CurrentDomain.BaseDirectory + "/ App_Data / uploads /";
                                if (!Directory.Exists(path))
                                {
                                    // Try to create the directory.  
                                    DirectoryInfo di = Directory.CreateDirectory(path);
                                }
                                string filename = Path.GetFileName(Request.Files[upload].FileName);
                                filename = Guid.NewGuid().ToString().Substring(0, 4) + filename;
                                string pth = Path.Combine(path, filename);
                                Request.Files[upload].SaveAs(pth);
                                //isValid = CheckForTheIcon(pth);
                                isValid = (pth);
                                test.zip = filename;
                                db.Tests.Add(test);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            isValid = "Only .zip files are accepted.";
                        }
                    }
                    //return isValid;
                }
                catch (Exception)
                {
                    //  return "Oops!. Something went wrong";
                }



                return RedirectToAction("Index");
            }

            return View(test);
        }

        // GET: OfficialAdmin/Tests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: OfficialAdmin/Tests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,zip")] Test test)
        {
            if (ModelState.IsValid)
            {
                db.Entry(test).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(test);
        }

        // GET: OfficialAdmin/Tests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return HttpNotFound();
            }
            return View(test);
        }

        // POST: OfficialAdmin/Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Test test = db.Tests.Find(id);
            db.Tests.Remove(test);
            db.SaveChanges();
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
        //public ActionResult Downloads()
        //{
        //    var dir = new System.IO.DirectoryInfo(Server.MapPath("/UploadedFiles/"));
        //    System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");
        //    //List<string> items = new List<string>();
        //    //foreach (var file in fileNames)
        //    //{
        //    //    items.Add(file.Name);
        //    //}
        //    //return View(items);
        //    return View(fileNames);
        //}

        //public ActionResult Download(string ImageName)
        //{
        //    using (var zipStream = new ZipOutputStream(Response.OutputStream))
        //    {
        //        var FileVirtualPath = "/UploadedFiles/" + ImageName;


        //        Response.ContentType = "application/pdf";
        //        // set the file name of file
        //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + ImageName);
        //        // get file path from server side
        //        Response.TransmitFile(Server.MapPath("/UploadedFiles/" + ImageName));
        //        Response.End();
        //        //  return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        //    }
        //}
    }
}

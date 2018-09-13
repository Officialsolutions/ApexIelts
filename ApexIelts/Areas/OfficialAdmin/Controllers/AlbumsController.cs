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
using System.Collections;

namespace AdminPaneNew.Areas.OfficialAdmin.Controllers
{
    public class AlbumsController : Controller
    {
     
        private dbcontext db = new dbcontext();
        public static string img;
        public static string imags=null;

        int aid;
        // GET: OfficialAdmin/Albums
        public ActionResult Index()
        {
            return View(db.Albums.ToList());
        }
        public ActionResult Gallery(int id)
        {
            var gallery = db.Galleries.Where(x => x.Albumid == id).ToList();
            return View(gallery);
        }
        // GET: OfficialAdmin/Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: OfficialAdmin/Albums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OfficialAdmin/Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Albumid,AlbumName,Image,date")] Album album, HttpPostedFileBase file, Gallery gallery, IEnumerable<HttpPostedFileBase> file2, Helper Help)
        {
            if (ModelState.IsValid)
            {
                album.date = System.DateTime.Now;
                album.Image = Help.uploadfile(file);
                db.Albums.Add(album);
                db.SaveChanges();

                gallery.date = System.DateTime.Now;
            
                gallery.Albumid = db.Albums.Max(x => x.Albumid);
                foreach (var a in file2)
                {
                    gallery.Images = Help.uploadfile(a);

                    db.Galleries.Add(gallery);
                    
                    db.SaveChanges();
                }

                //gallery.Images = Help.uploadfile(file2);
                TempData["Success"] = "Saved Successfully";
                return RedirectToAction("Index");
            }

            return View(album);
        }

        // GET: OfficialAdmin/Albums/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //DataTable dt = new DataTable();
            //dt.Columns.Add("id", System.Type.GetType("System.Int32"));
            //dt.Columns.Add("Image");
            Album album = db.Albums.Find(id);
            aid = album.Albumid;
            img = album.Image;
            var gal = db.Galleries.Where(x => x.Albumid == album.Albumid).ToList();
            imags = null;
            if (gal != null)
            {
                foreach (var n in gal)
                {
                    if (imags == null)
                    {
                        imags = n.Images;
                    }
                    else
                    {
                        imags +=","+ n.Images;
                    }
                }
            }
            //imags = db.Galleries.FirstOrDefault(x => x.Albumid == album.Albumid).Images;
            //MyDt = db.Galleries.Where(x => x.Albumid == id).ToListAsync();
            //if (gallery != null)
            //{
            //    DataRow dr = new DataRow();
            //    dr["Image"]
            //    imags = gallery.Images;
            //}
            // db.Galleries.Find(id);

            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: OfficialAdmin/Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Albumid,AlbumName,Image,date")] Album album,Gallery gallery, HttpPostedFileBase file, IEnumerable<HttpPostedFileBase> file2, Helper Help)
        {
            if (ModelState.IsValid)
            {
                album.date = System.DateTime.Now;
                album.Image = file != null ? Help.uploadfile(file) : img;
                #region delete file
                string fullPath = Request.MapPath("~/UploadedFiles/" + img);
                if (img == album.Image)
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
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();

                ////////////////////////////////
                string[] gfil = imags.Split(',');
                gallery.date = System.DateTime.Now;
                gallery.Albumid = album.Albumid;
                foreach (var files in file2)
                {
                    if(files==null)
                    {
                       
                    }
                    else
                    {

                            //gallery.Images = Help.uploadfile(a);
                            gallery.Images = Help.uploadfile(files);
                            db.Galleries.Add(gallery);

                            db.SaveChanges();
                        

                        #region delete file
                        foreach (var galfile in gfil)
                        {
                            string fullPath2 = Request.MapPath("~/UploadedFiles/" + galfile);
                            if (img == gallery.Images)
                            {
                            }
                            else
                            {
                                if (System.IO.File.Exists(fullPath))
                                {
                                    System.IO.File.Delete(fullPath);
                                }
                            }
                        }
                        #endregion
                    }
                }
                    
                //if (gallery.Images = file2 != null ? Help.uploadfile(file2) : img)
              
                
                TempData["Success"] = "Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(album);
        }

        // GET: OfficialAdmin/Albums/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: OfficialAdmin/Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            TempData["Success"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gallery gallery = db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // POST: OfficialAdmin/Albums/Delete/5
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveConfirmed(int id)
        {
            Gallery gallery = db.Galleries.Find(id);
            db.Galleries.Remove(gallery);
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

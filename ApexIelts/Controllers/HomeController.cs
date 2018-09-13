using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminPaneNew.Areas.OfficialAdmin.Models;
using System.Web.Mail;
using System.Web.Security;

namespace ApexIelts.Controllers
{
    public class HomeController : Controller
    {
        dbcontext db = new dbcontext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult CompanyOverview()
        {
            Pages pp = db.Pages.Where(x => x.Pagesid == 2).FirstOrDefault();
            return View(pp);
        }
        public ActionResult FounderMessage()
        {
            Pages pp = db.Pages.Where(x => x.Pagesid == 3).FirstOrDefault();
            return View(pp);
        }
        public ActionResult WhyChooseUs()
        {
            Pages pp = db.Pages.Where(x => x.Pagesid == 4).FirstOrDefault();
            return View(pp);
        }
        public ActionResult OurMission()
        {
            Pages pp = db.Pages.Where(x => x.Pagesid == 5).FirstOrDefault();
            return View(pp);
        }
        public ActionResult OurVision()
        {
            Pages pp = db.Pages.Where(x => x.Pagesid == 6).FirstOrDefault();
            return View(pp);
        }
        public ActionResult Service(int id)
        {
            Service ss = db.Services.Where(x => x.Serviceid == id).FirstOrDefault();
            return View(ss);
        }
        public ActionResult ServiceDetails(int id)
        {
            SingleService sser = db.SingleServices.Where(x => x.Singleid == id).FirstOrDefault();
            return View(sser);
        }
      
        public ActionResult ShiningStars()
        {
            return View();
        }
        public ActionResult Albums()
        {
            return View();
        }
        public ActionResult Gallery(int id)
        {
            var gallery = db.Galleries.Where(x => x.Albumid == id).ToList();
            return View(gallery);
        }
        public ActionResult Video()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            Contact cc = db.Contacts.ToList().FirstOrDefault();
            return View(cc);
            //return View();
        }
        // GET: Default/Details/5
        public ActionResult StudentDahsboard(int id,int sid)
        {
            var assign = db.AssignTests.Where(x => x.IeltsTest.Category.Categoryid == id).Where(a => a.Studentid == sid);
            return View(assign.ToList());
        }
        public ActionResult StudentProfile()
        {
            return View();
        }
        // GET: Default/Create
        public ActionResult Test()
        {
            return View();
        }

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Email(string name, string phone, string email, string subject, string message)
        {


            System.Web.Mail.MailMessage Msg = new System.Web.Mail.MailMessage();
            // Sender e-mail address.
            Msg.From = email;
            // Recipient e-mail address.
            Msg.To = "info@apexielts.com";
            Msg.Subject = subject;
            Msg.Body = message;
            // your remote SMTP server IP.
            SmtpMail.SmtpServer = "198.38.83.183";//your ip address
            SmtpMail.Send(Msg);
            Msg = null;

            return RedirectToAction("ContactUs");

        }
        public ActionResult ServiceEmail(string name, string phone, string email, string subject, string message)
        {


            System.Web.Mail.MailMessage Msg = new System.Web.Mail.MailMessage();
            // Sender e-mail address.
            Msg.From = email;
            // Recipient e-mail address.
            Msg.To = "info@apexielts.com";
            Msg.Subject = subject;
            Msg.Body = message;
            // your remote SMTP server IP.
            SmtpMail.SmtpServer = "198.38.83.183";//your ip address
            SmtpMail.Send(Msg);
            Msg = null;

            return RedirectToAction("Service");

        }
        public ActionResult DetailEmail(string name, string phone, string email, string subject, string message)
        {


            System.Web.Mail.MailMessage Msg = new System.Web.Mail.MailMessage();
            // Sender e-mail address.
            Msg.From = email;
            // Recipient e-mail address.
            Msg.To = "info@apexielts.com";
            Msg.Subject = subject;
            Msg.Body = message;
            // your remote SMTP server IP.
            SmtpMail.SmtpServer = "198.38.83.183";//your ip address
            SmtpMail.Send(Msg);
            Msg = null;

            return RedirectToAction("ServiceDetails");

        }

        [HttpPost]
        public ActionResult Login(StudentDetail model, string returnUrl)
        {
            dbcontext db = new dbcontext();
            var dataItem = db.StudentDetails.Where(x => x.UserName == model.UserName && x.Password == model.Password).ToList();
            if (dataItem != null)
            {
                //FormsAuthentication.SetAuthCookie(dataItem.Usename, false);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                         && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    TempData["Success"] = "Login Successfully";
                    Session["student"] = dataItem.FirstOrDefault().Studentid;

                    return RedirectToAction("StudentProfile", "Home");

                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid user/pass");
                return View();
            }
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session["student"] = null;
            return RedirectToAction("Home");
        }
        public ActionResult DownloadFile(string file)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/UploadedFiles/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + file);
            string fileName = file;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
//using Download_Zip_Files_WebAPI_MVC.Models;
using AdminPaneNew.Areas.OfficialAdmin.Models;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ApexIelts.Controllers
{
public class ZipAPIController : ApiController
{
        dbcontext db = new dbcontext();

    [HttpPost]
    [Route("api/ZipAPI/DownloadZipFile")]
    public HttpResponseMessage DownloadZipFile(int id)
    {
        //Create HTTP Response.
        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

        //Create the Zip File.
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            zip.AddDirectoryByName("Files");
                List<IeltsTest> files = db.IeltsTests.Where(x => x.Ieltsid == id).ToList();
                foreach (var file in files)
                {
                   
                   // int id = n;
                   // files = db.IeltsTests.Where(x => x.Ieltsid == id).ToList();
                    zip.AddFile("/UploadedFiles/"+file.Audio, "Files");
                    zip.AddFile("/UploadedFiles/" + file.Image, "Files");
                    zip.AddFile("/UploadedFiles/" + file.Url, "Files");
                    //zip.AddFile("/UploadedFiles/");
                }

                //Set the Name of Zip File.
                string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                //Save the Zip File to MemoryStream.
                zip.Save(memoryStream);

                //Set the Response Content.
                response.Content = new ByteArrayContent(memoryStream.ToArray());

                //Set the Response Content Length.
                response.Content.Headers.ContentLength = memoryStream.ToArray().LongLength;

                //Set the Content Disposition Header Value and FileName.
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = zipName;

                //Set the File Content Type.
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/zip");
                return response;
            }
        }
    }
}
}

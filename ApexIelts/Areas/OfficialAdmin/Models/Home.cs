using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AdminPaneNew.Areas.OfficialAdmin.Models
{
    public class Home
    {

    }
    public class slider
    {
        [Key]
        public int Sliderid { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
        public string url { get; set; }

        public DateTime date { get; set; }

    }
    public class news
    {
        [Key]
        public int Newsid { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public string Thumbnail { get; set; }
        public DateTime date { get; set; }
    }
    public class Service
    {
        [Key]
        public int Serviceid { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string ShortDescription { get; set; }
        public string Image { get; set; }

        public DateTime date { get; set; }
        public string Thumbnail { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }
        public virtual ICollection<SingleService> SingleService { get; set; }
    }
    public class Testimonial
    {
        [Key]
        public int Testid { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime date { get; set; }
    }
    public class Album
    {
        [Key]
        public int Albumid { get; set; }
        public string AlbumName { get; set; }
        public string Image { get; set; }
        public DateTime date { get; set; }
        public virtual ICollection<Gallery> Galleries { get; set; }

    }
    public class Gallery
    {
        [Key]
        public int GalleryId { get; set; }
        [DisplayName("Album")]
        public int Albumid { get; set; }

        public virtual Album Albums { get; set; }
        public string Thumbnail { get; set; }
        public string Images { get; set; }
        public DateTime date { get; set; }
    }
    public class Clientlogo
    {
        [Key]
        public int Clientid { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }    
        public DateTime date { get; set; }

    }
    public class Logo
    {
        [Key]
        public int Logoid { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTime date { get; set; }

    }
    public class Contact
    {
        [Key]
        public int Contactid { get; set; }
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime date { get; set; }
    }
    public class Features
    {
        [Key]
        public int Featureid { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string ShortDescription { get; set; }
        public string Image { get; set; }

        public DateTime date { get; set; }
        public string Thumbnail { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }
    }
    public class Videos
    {
        [Key]
        public int Videoid { get; set; }
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public DateTime date { get; set; }

    }
    public class Account
    {
        [Key]
        public int Accountid { get; set; }
        [Required]
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Usename { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime date { get; set; }
    }
    public class StudentDetail
    {
        [Key]
        public int Studentid { get; set; }
        [Required]
        public string Name { get; set; }
        public string FatherName { get; set; }
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address.")]
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "You must provide a valid email address.")]
        public string Email { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DOB { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public string CourseType { get; set; }
        public string RollNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime date { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<AssignTest> AssignTest { get; set; }
    }
    public class Category
    {
        [Key]
        public int Categoryid { get; set; }
        public string Name { get; set; }
        public DateTime date { get; set; }
        public virtual ICollection<IeltsTest> IeltsTest { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
    public class IeltsTest
    {
        [Key]
        public int Ieltsid { get; set; }
        [Required]
        public string Name { get; set; }
        public int Categoryid { get; set; }
        public virtual Category Category { get; set; }
        public string TestType { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string Audio { get; set; }
        public DateTime date { get; set; }
        public virtual ICollection<AssignTest> AssignTest { get; set; }
    }
    public enum TestType
    {
        General,
        Academic
        
    }
    public class AssignTest
    {
        [Key]
        public int Assignid { get; set; }
        public int Studentid { get; set; }
        public virtual StudentDetail StudentDetail { get; set; }
        public int Ieltsid { get; set; }
        public virtual IeltsTest IeltsTest { get; set; }
        public DateTime date { get; set; }
        public string Status { get; set; }
    }
    public class Pages
    {
        [Key]
        public int Pagesid { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]
        public string ShortDescription { get; set; }
        public string Image { get; set; }

        public DateTime date { get; set; }
        public string Thumbnail { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }
    }
    public class Achievers
    {
        [Key]
        public int Achieverid { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        public string ShortDescription { get; set; }
        public string Image { get; set; }
        public string Course { get; set; }
        public DateTime date { get; set; }
        public string Reading { get; set; }
        public string Writing { get; set; }
        public string Listening { get; set; }
        public string Speaking { get; set; }
        public string Overall { get; set; }

    }
    public class Counter
    {
        [Key]
        public int Counterid { get; set; }
        [Required]
        public string Experience { get; set; }
        [Required]
        public string Customers { get; set; }
        public DateTime date { get; set; }
        [Required]
        public string Award { get; set; }
        [Required]
        public string Satisfied { get; set; }

    }
    public class SingleService
    {
        [Key]
        public int Singleid { get; set; }
        [DisplayName("Service")]
        public int Serviceid { get; set; }

        public virtual Service Service { get; set; }
        [Required]
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime date { get; set; }
        public string Keyword { get; set; }
        public string MetaDescription { get; set; }

    }
}
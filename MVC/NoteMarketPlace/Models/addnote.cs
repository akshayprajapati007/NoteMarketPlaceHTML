using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NoteMarketPlace.Models
{
    public class addnote
    {
        public int NoteID { get; set; }
        public int SellerID { get; set; }
        public int Status { get; set; }
        public Nullable<int> ActionBy { get; set; }
        public string AdminRemarks { get; set; }
        public Nullable<System.DateTime> PublishedDate { get; set; }
        [Required(ErrorMessage = "Please enter note title")]
        public string NoteTitle { get; set; }

        [Required(ErrorMessage = "Please select note category")]
        public int NoteCategoryID { get; set; }

        public HttpPostedFileBase DisplayPicture { get; set; }

        [Required(ErrorMessage = "Please select note attachment(s)")]
        public List<HttpPostedFileBase> NoteAttachment { get; set; }

        [Required(ErrorMessage = "Please select note type")]
        public int NoteTypeID { get; set; }

        public Nullable<int> NumberOfPages { get; set; }

        [Required(ErrorMessage = "Please enter note description")]
        public string NoteDescription { get; set; }
        public string UniversityInformation { get; set; }

        [Required(ErrorMessage = "Please select country")]
        public Nullable<int> CountryID { get; set; }
        public string Course { get; set; }
        public string CourseCode { get; set; }
        public string ProfessorName { get; set; }

        [Required(ErrorMessage = "Please select selling type")]
        public string SellType { get; set; }

        [Required(ErrorMessage = "Please enter selling price")]
        public decimal SellPrice { get; set; }

        public HttpPostedFileBase PreviewUpload { get; set; }

        public decimal Ratings { get; set; }
    }
}
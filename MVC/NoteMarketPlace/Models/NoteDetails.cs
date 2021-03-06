//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NoteMarketPlace.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NoteDetails
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoteDetails()
        {
            this.DownloadNotes = new HashSet<DownloadNotes>();
            this.NoteReviews = new HashSet<NoteReviews>();
            this.SellerNoteAttachment = new HashSet<SellerNoteAttachment>();
            this.SpamReports = new HashSet<SpamReports>();
        }
    
        public int NoteID { get; set; }
        public int SellerID { get; set; }
        public int Status { get; set; }
        public Nullable<int> ActionBy { get; set; }
        public string AdminRemarks { get; set; }
        public Nullable<System.DateTime> PublishedDate { get; set; }
        public string NoteTitle { get; set; }
        public int NoteCategoryID { get; set; }
        public string DisplayPicture { get; set; }
        public int NoteTypeID { get; set; }
        public Nullable<int> NumberOfPages { get; set; }
        public string NoteDescription { get; set; }
        public string UniversityInformation { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string Course { get; set; }
        public string CourseCode { get; set; }
        public string ProfessorName { get; set; }
        public string SellType { get; set; }
        public decimal SellPrice { get; set; }
        public string PreviewUpload { get; set; }
        public bool IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual Countries Countries { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DownloadNotes> DownloadNotes { get; set; }
        public virtual NoteCategories NoteCategories { get; set; }
        public virtual Users Users { get; set; }
        public virtual NoteType NoteType { get; set; }
        public virtual Users Users1 { get; set; }
        public virtual NoteStatus NoteStatus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoteReviews> NoteReviews { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SellerNoteAttachment> SellerNoteAttachment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpamReports> SpamReports { get; set; }
    }
}

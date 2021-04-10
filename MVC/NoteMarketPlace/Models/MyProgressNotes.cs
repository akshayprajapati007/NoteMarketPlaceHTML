namespace NoteMarketPlace.Models
{
    public class MyProgressNotes
    {
        public NoteDetails SellerNotes { get; set; }
        public NoteCategories Category { get; set; }
        public NoteStatus status { get; set; }
        public DownloadNotes downloadnote { get; set; }
        public Users u { get; set; }
        public Users u1 { get; set; }
        public UserProfile userprofile { get; set; }
        public Countries country { get; set; }
        public NoteReviews noteReview { get; set; }
        public SellerNoteAttachment sna { get; set; }
        public SpamReports spam { get; set; }
    }

    public class MyPublishNotes
    {
        public NoteDetails SellerNotes { get; set; }
        public NoteCategories Category { get; set; }
    }

    public class nd
    {
        public NoteDetails note { get; set; }
        public NoteCategories Category { get; set; }
        public Countries contryname { get; set; }
        public NoteReviews nr { get; set; }
    }

    public class userprofiledata
    {
        public NoteDetails note { get; set; }
        public Users u { get; set; }
        public UserProfile upd { get; set; }
    }

    public class reviewtable
    {
        public NoteDetails note { get; set; }
        public NoteReviews notereview { get; set; }
        public Users usr { get; set; }
    }
    public class spamtable
    {
        public NoteDetails note { get; set; }
        public SpamReports spamrpt { get; set; }
    }
    public class typeuser
    {
        public NoteType types { get; set; }
        public NoteCategories categorydata { get; set; }
        public Countries countrydata { get; set; }
        public Users user { get; set; }
    }

}

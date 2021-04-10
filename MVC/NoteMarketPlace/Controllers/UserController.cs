using System;
using System.Web.Mvc;
using System.Web.Security;
using NoteMarketPlace.Models;
using System.Linq;
using NoteMarketPlace.EmailTemplates;
using System.IO;
using System.Collections.Generic;
using System.IO.Compression;
using PagedList;
using NotesMarketPlace.EmailTemplates;

namespace NoteMarketPlace.Controllers
{
    public class UserController : Controller
    {

        //GET: SignUp
        public ActionResult signUp()
        {
            return View();
        }
        //POST: SignUp
        [AllowAnonymous]
        [HttpPost]
        public ActionResult signUp(signUp model)
        {

            if (ModelState.IsValid)
            {
                using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
                {
                    Users u = new Users();
                    u.FirstName = model.FirstName;
                    u.LastName = model.LastName;
                    u.EmailID = model.EmailID;
                    u.UserRoleID = 3;
                    u.IsActive = true;
                    u.CreatedDate = DateTime.Now;
                    u.Password = model.Password;
                    u.IsEmailVerified = false;

                    DBobj.Users.Add(u);
                    DBobj.SaveChanges();

                    if (u.UserID > 0)
                    {
                        ModelState.Clear();
                        ViewBag.IsSuccess = "<p><span><i class='fas fa-check-circle'></i></span> Your account has been successfully created </p>";
                        TempData["Name"] = model.FirstName;


                        //  Email Verification Link
                        var activationCode = model.Password;
                        var verifyUrl = "/User/VerifyAccount/" + activationCode;
                        var activationlink = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);


                        // Sending Email
                        EmailVerification.SendVerifyLinkEmail(u, activationlink);

                        return RedirectToAction("emailVerification", "User");
                    }
                }

            }
            return View();

        }



        //VerifyEmail
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                DBobj.Configuration.ValidateOnSaveEnabled = false; //avoid confirm password doesn't match issue on save changes
                var ema = DBobj.Users.Where(x => x.Password == id).FirstOrDefault();
                if (ema != null)
                {
                    ema.IsEmailVerified = true;
                    DBobj.SaveChanges();
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }

            TempData["Message"] = "<p>Your Email Is Verified You Can Login Here</p>";
            return RedirectToAction("login", "User");
        }



        //GET: Login
        public ActionResult login()
        {
            return View();
        }
        //POST: Login
        [AllowAnonymous]
        [HttpPost]
        public ActionResult login(Users model)
        {

            if (ModelState.IsValid)
            {
                using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
                {


                    bool isValid = DBobj.Users.Any(x => x.EmailID == model.EmailID && x.Password == model.Password && x.IsActive == true);
                    if (isValid)
                    {
                        if (DBobj.Users.Any(x => x.EmailID == model.EmailID && x.IsEmailVerified == true))
                        {
                            FormsAuthentication.SetAuthCookie(model.EmailID, false);
                            Session["EmailID"] = model.EmailID;
                            Users userdata = DBobj.Users.Where(x => x.EmailID == model.EmailID).FirstOrDefault();
                            Session["FullName"] = userdata.FirstName + " " + userdata.LastName;
                            Session["UserID"] = userdata.UserID;
                            if (userdata.UserRoleID != 3)
                            {
                                return RedirectToAction("dashboard", "Admin");
                            }
                            else
                            {
                                int upCheck = DBobj.UserProfile.Where(x => x.UserID == userdata.UserID).Count();
                                if (upCheck > 0)
                                {
                                    return RedirectToAction("searchNotes", "User");
                                }
                                else
                                {
                                    return RedirectToAction("userProfile", "User");
                                }
                            }

                        }
                        else
                        {
                            TempData["Message"] = "<p>Your email is not verified, Please verified it..!</p>";
                        }

                    }
                    else
                    {
                        ViewBag.incorrectPass = "<p>Email or password that you've entered is incorrect </p>";
                        return View();
                    }

                }
                return View();
            }
            return View();

        }



        //GET: Logout
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login","User");
        }



        //GET: EmailVerification
        public ActionResult emailVerification()
        {
            return View();
        }



        //GET: ForgotPassword
        [AllowAnonymous]
        public ActionResult forgot()
        {
            return View();
        }
        //POST: ForgotPassword
        [AllowAnonymous]
        [HttpPost]
        public ActionResult forgot(Users model)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                string allowedChars = "";
                string passwordString = "";
                string temp = "";

                bool isValid = DBobj.Users.Any(x => x.EmailID == model.EmailID);
                if (isValid)
                {
                    Users u = DBobj.Users.Where(x => x.EmailID == model.EmailID).FirstOrDefault();
                    allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
                    allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
                    allowedChars += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?";

                    char[] sep = { ',' };
                    string[] arr = allowedChars.Split(sep);
                    Random rand = new Random();

                    for (int i = 0; i < 6; i++)

                    {
                        temp = arr[rand.Next(0, arr.Length)];
                        passwordString += temp;
                    }

                    //  Save Password
                    u.Password = passwordString;
                    DBobj.SaveChanges();

                    //Sending new password on mail
                    ForgotEmail.SendOtpToEmail(u, passwordString);

                    TempData["SuccessMessage"] = "<p>New password sent to your registered email-address</p>";
                    return RedirectToAction("Login", "User");
                }
                return View();
            }
        }



        [Authorize]
        //GET: AddNote
        public ActionResult addNote(int? id)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                ViewBag.notecategoies = new SelectList(DBobj.NoteCategories.Where(x=>x.IsActive == true).ToList(), "NoteCategoryID", "CategoryName");
                ViewBag.notetypes = new SelectList(DBobj.NoteType.Where(x => x.IsActive == true).ToList(), "NoteTypeID", "TypeName");
                ViewBag.countries = new SelectList(DBobj.Countries.Where(x => x.IsActive == true).ToList(), "CountryID", "CountryName");
                if(id!= null)
                {
                    addnote adn = new addnote();
                    var noteattachdata = DBobj.SellerNoteAttachment.Where(x => x.NoteID == id).FirstOrDefault();
                    var notedata = DBobj.NoteDetails.Where(x => x.NoteID == id).FirstOrDefault();
                    adn.NoteTitle = notedata.NoteTitle;
                    adn.NoteCategoryID = notedata.NoteCategoryID;
                    adn.NoteTypeID = notedata.NoteTypeID;
                    adn.NumberOfPages = notedata.NumberOfPages;
                    adn.CountryID = notedata.CountryID;
                    adn.Course = notedata.Course;
                    adn.CourseCode = notedata.CourseCode;
                    adn.NoteDescription = notedata.NoteDescription;
                    adn.ProfessorName = notedata.ProfessorName;
                    adn.SellType = notedata.SellType;
                    adn.SellPrice = notedata.SellPrice;
                    adn.UniversityInformation = notedata.UniversityInformation;
                    var dpPath = notedata.DisplayPicture;
                    return View(adn);
                }
                return View();
            }
            
        }
        //POST: AddNote
        [Authorize]
        [HttpPost]
        public ActionResult addNote(addnote notedetails, string submit)
        {

            if (ModelState.IsValid)
            {
                if (notedetails.SellType == "Paid" && notedetails.PreviewUpload == null)
                {
                    using(NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
                    {
                        ViewBag.previewmessage = "Note Preview Is Required For Paid Note...";
                        ViewBag.notecategoies = new SelectList(DBobj.NoteCategories.Where(x => x.IsActive == true).ToList(), "NoteCategoryID", "CategoryName");
                        ViewBag.notetypes = new SelectList(DBobj.NoteType.Where(x => x.IsActive == true).ToList(), "NoteTypeID", "TypeName");
                        ViewBag.countries = new SelectList(DBobj.Countries.Where(x => x.IsActive == true).ToList(), "CountryID", "CountryName");
                        return View();
                    }
                }

                using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
                {
                    NoteDetails nd = new NoteDetails();
                    SellerNoteAttachment sna = new SellerNoteAttachment();
                    nd.SellerID = (int)Session["UserID"];
                    if (submit == "Save")
                    {
                        nd.Status = 1;
                    }
                    if (submit == "Publish")
                    {
                        nd.Status = 4;
                        nd.ModifiedDate = DateTime.Now;
                    }
                    nd.NoteTitle = notedetails.NoteTitle;
                    nd.NoteCategoryID = notedetails.NoteCategoryID;
                    nd.NoteTypeID = notedetails.NoteTypeID;
                    nd.NumberOfPages = notedetails.NumberOfPages;
                    nd.NoteDescription = notedetails.NoteDescription;
                    nd.UniversityInformation = notedetails.UniversityInformation;
                    nd.CountryID = notedetails.CountryID;
                    nd.Course = notedetails.Course;
                    nd.CourseCode = notedetails.CourseCode;
                    nd.ProfessorName = notedetails.ProfessorName;
                    nd.SellType = notedetails.SellType;
                    nd.SellPrice = notedetails.SellPrice;
                    nd.IsActive = true;
                    nd.CreatedBy = (int)Session["UserID"];
                    nd.CreatedDate = DateTime.Now;
                    nd.ModifiedDate = DateTime.Now;
                    nd.ModifiedBy = (int)Session["UserID"];

                    DBobj.NoteDetails.Add(nd);
                    DBobj.SaveChanges();

                    string path = Path.Combine(Server.MapPath("~/Member/" + Session["UserID"].ToString()), nd.NoteID.ToString());

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    if (notedetails.DisplayPicture != null && notedetails.DisplayPicture.ContentLength > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(notedetails.DisplayPicture.FileName);
                        string extension = Path.GetExtension(notedetails.DisplayPicture.FileName);
                        fileName = "DP_" + DateTime.Now.ToString("ddMMyyyy") + extension;
                        string finalpath = Path.Combine(path, fileName);
                        notedetails.DisplayPicture.SaveAs(finalpath);

                        nd.DisplayPicture = Path.Combine(("~/Member/" + Session["UserID"].ToString() + "/" + nd.NoteID.ToString() + "/"), fileName);
                        DBobj.SaveChanges();
                    }
                    if (notedetails.PreviewUpload != null && notedetails.PreviewUpload.ContentLength > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(notedetails.PreviewUpload.FileName);
                        string extension = Path.GetExtension(notedetails.PreviewUpload.FileName);
                        fileName = "PREVIEW_" + DateTime.Now.ToString("ddMMyyyy") + extension;
                        string finalpath = Path.Combine(path, fileName);
                        notedetails.PreviewUpload.SaveAs(finalpath);

                        nd.PreviewUpload = Path.Combine(("~/Member/" + Session["UserID"].ToString() + "/" + nd.NoteID.ToString() + "/"), fileName);
                        DBobj.SaveChanges();
                    }

                    string attachmentpath = Path.Combine(Server.MapPath("~/Member/" + Session["UserID"].ToString() + "/" + nd.NoteID.ToString()), "attachment");

                    if (!Directory.Exists(attachmentpath))
                    {
                        Directory.CreateDirectory(attachmentpath);
                    }
                    if (notedetails.NoteAttachment != null && notedetails.NoteAttachment[0].ContentLength > 0)
                    {
                        var count = 1;
                        var FilePath = "";
                        var FileName = "";
                        long FileSize = 0;
                        foreach (var file in notedetails.NoteAttachment)
                        {
                            FileSize += ((file.ContentLength) / 1024);
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            string extension = Path.GetExtension(file.FileName);
                            fileName = "Attachment_" + count + "_" + DateTime.Now.ToString("ddMMyyyy") + extension;
                            string finalpath = Path.Combine(attachmentpath, fileName);
                            file.SaveAs(finalpath);
                            FileName += fileName + ";";
                            FilePath += Path.Combine(("/Member/" + Session["UserID"].ToString() + "/" + nd.NoteID.ToString() + "/attachment/"), fileName) + ";";
                            count++;
                        }
                        sna.FileName = FileName;
                        sna.FilePath = FilePath;
                        sna.FilesSize = FileSize; //
                        sna.NoteID = nd.NoteID;
                        sna.IsActive = true;
                        sna.CreatedDate = DateTime.Now;
                        sna.CreatedBy = (int)Session["UserID"];
                        DBobj.SaveChanges();

                    }
                    DBobj.SellerNoteAttachment.Add(sna);
                    DBobj.SaveChanges();

                    //Sending mail to admin
                    if (submit == "Publish")
                    {
                        SellerRequestToAdmin.sellerRequestToAdmin(Session["FullName"].ToString(), nd.NoteTitle);
                    }

                    ViewBag.notecategoies = new SelectList(DBobj.NoteCategories.Where(x => x.IsActive == true).ToList(), "NoteCategoryID", "CategoryName");
                    ViewBag.notetypes = new SelectList(DBobj.NoteType.Where(x => x.IsActive == true).ToList(), "NoteTypeID", "TypeName");
                    ViewBag.countries = new SelectList(DBobj.Countries.Where(x => x.IsActive == true).ToList(), "CountryID", "CountryName");

                }
                ModelState.Clear();
                return RedirectToAction("dashboard", "User");
            }
            using(NotesMarketplaceEntities Dbobj= new NotesMarketplaceEntities())
            {
                ViewBag.notecategoies = new SelectList(Dbobj.NoteCategories.Where(x => x.IsActive == true).ToList(), "NoteCategoryID", "CategoryName");
                ViewBag.notetypes = new SelectList(Dbobj.NoteType.Where(x => x.IsActive == true).ToList(), "NoteTypeID", "TypeName");
                ViewBag.countries = new SelectList(Dbobj.Countries.Where(x => x.IsActive == true).ToList(), "CountryID", "CountryName");
                return View();
            }
        }


        
        [Authorize]
        [Route("deleteNote/id")]
        public ActionResult deleteNote(int? id)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                var dnote = DBobj.SellerNoteAttachment.FirstOrDefault(x => x.NoteID == id);
                var note = DBobj.NoteDetails.FirstOrDefault(x => x.NoteID == id);
                if (dnote != null && note != null)
                {
                    DBobj.SellerNoteAttachment.Remove(dnote);
                    DBobj.NoteDetails.Remove(note);
                    DBobj.SaveChanges();
                }
                return RedirectToAction("dashboard", "User");
            }
        }



        //GET: SearchNotes
        public ActionResult searchNotes(int? page, string search, string NoteTypeID, string NoteCategoryID, string UniversityInformation, string CountryID, string Course, decimal? Ratings)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();

            ViewBag.notecategoies = new SelectList(DBobj.NoteCategories.Where(x => x.IsActive == true).ToList().Distinct(), "NoteCategoryID", "CategoryName");
            ViewBag.notetypes = new SelectList(DBobj.NoteType.Where(x => x.IsActive == true).ToList().Distinct(), "NoteTypeID", "TypeName");
            ViewBag.countries = new SelectList(DBobj.Countries.Where(x => x.IsActive == true).ToList().Distinct(), "CountryID", "CountryName");
            ViewBag.universities = new SelectList(DBobj.NoteDetails.Where(x => x.IsActive == true).ToList().Distinct(), "UniversityInformation", "UniversityInformation");
            ViewBag.courses = new SelectList(DBobj.NoteDetails.Where(x => x.IsActive == true).ToList(), "Course", "Course");

            var notes = DBobj.NoteDetails.Where(x => x.IsActive == true && (x.NoteTitle.StartsWith(search) || search == null)).ToList();

            var seachedNotes = (from n in notes
                                join c in DBobj.Countries.ToList() on n.CountryID equals c.CountryID
                                where (n.Status == 2 && (n.NoteTypeID.ToString() == NoteTypeID || String.IsNullOrEmpty(NoteTypeID))
                                && (n.NoteCategoryID.ToString() == NoteCategoryID || String.IsNullOrEmpty(NoteCategoryID))
                                && (n.UniversityInformation.ToString() == UniversityInformation || String.IsNullOrEmpty(UniversityInformation))
                                && (n.CountryID.ToString() == CountryID || String.IsNullOrEmpty(CountryID))
                                && (n.Course.ToString() == Course || String.IsNullOrEmpty(Course)))
                                select new nd
                                {
                                    note = n,
                                    contryname = c
                                }).ToList();

            ViewBag.filterNotes = seachedNotes.ToPagedList(page ?? 1, 6);
            ViewBag.nd = seachedNotes.Count();

            if (Session["UserID"] != null)
            {
                int usid = (int)Session["UserID"];
                var dp = DBobj.UserProfile.Where(x => x.UserID == usid).Select(x => x.ProfilePicture).FirstOrDefault();
                Session["dpPath"] = dp;
            }
            
            return View();
        }
        //POST: SearchNotes
        [HttpPost]
        public ActionResult searchNotes(addnote model)
        {
            if (ModelState.IsValid)
            {
                using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
                {
                    var list = DBobj.NoteDetails.Where(x => x.NoteCategoryID == model.NoteCategoryID && x.NoteTypeID == model.NoteTypeID && x.CountryID == model.CountryID && x.Course == model.Course && x.UniversityInformation == model.UniversityInformation);
                    ViewBag.List = list;
                }
            }
            return View();
        }



        //GET: ContactUs
        public ActionResult contactUs()
        {
            return View();
        }
        //POST: ContactUs
        [HttpPost]
        public ActionResult contactUs(ContactUs model)
        {

            if (ModelState.IsValid)
            {
                using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
                {
                    ContactUs u = new ContactUs();
                    u.FullName = model.FullName;
                    u.EmailID = model.EmailID;
                    u.Subject = model.Subject;
                    u.Comments = model.Comments;
                    u.IsActive = true;
                    u.CreatedDate = DateTime.Now;

                    DBobj.ContactUs.Add(u);
                    DBobj.SaveChanges();

                    if (u.ContactUsID > 0)
                    {
                        ModelState.Clear();
                        ContactUsEmail.ContactUs(model.Subject, model.FullName, model.Comments);
                        return View();
                    }
                }

            }

            return View();
        }



        //GET: Notedetails
        [Route("noteDetails /{id}")]
        public ActionResult noteDetails(int? id)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {

                var ni = DBobj.NoteDetails.Where(x => x.NoteID == id).FirstOrDefault();
                NoteCategories noteCategory = DBobj.NoteCategories.Find(ni.NoteCategoryID);
                ViewBag.Category = noteCategory.CategoryName;
                Countries country = DBobj.Countries.Find(ni.CountryID);
                ViewBag.Country = country.CountryName;

                var reviewdetail = (from nr in DBobj.NoteReviews
                                    join n in DBobj.NoteDetails on nr.NoteID equals n.NoteID
                                    join us in DBobj.Users on nr.ReviewByID equals us.UserID
                                    orderby nr.CreatedDate descending
                                    select new MyProgressNotes
                                    {
                                        SellerNotes = n,
                                        noteReview = nr,
                                        u = us
                                    }).Take(3).ToList();

                ViewBag.reviewdetailbag = reviewdetail;
                ViewBag.reviewcount = reviewdetail.Count();
                ViewBag.ratingCount = DBobj.NoteReviews.Where(x => x.NoteID == id).Select(x => x.Ratings).Count();

                if (ViewBag.ratingcount > 0)
                {
                    ViewBag.ratingSum = DBobj.NoteReviews.Where(x => x.NoteID == id).Select(x => x.Ratings).Sum();
                }
                else
                {
                    ViewBag.ratingSum = "No Review Found !";
                }

                ViewBag.spamtotalcount = DBobj.SpamReports.Where(x => x.NoteID == id).Count();

                return View(ni);
            }
        }



        //GET: Dashboard
        [Authorize]
        public ActionResult dashboard(int? page, int? page2, string searchfilter, string submit)
        {

            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                int userid = (int)Session["UserID"];

                ViewBag.mySoldNotes = DBobj.DownloadNotes.Where(x => x.IsActive == true && x.SellerID == userid && x.IsSellerHasAllowedDownload==true).Count();
                var earning = DBobj.DownloadNotes.Where(x => x.IsActive == true && x.SellerID == userid && x.IsSellerHasAllowedDownload == true).Select(x => x.PurchasePrice).Sum();
                if (earning > 0)
                {
                    ViewBag.totalEarning = earning;
                }
                else
                {
                    ViewBag.totalEarning = 0;
                }

                ViewBag.myDownloadNotes = DBobj.DownloadNotes.Where(x => x.IsActive == true && x.BuyerID == userid && x.IsSellerHasAllowedDownload == true).Count();
                ViewBag.myRejectedNotes = DBobj.NoteDetails.Where(x => x.IsActive == true && x.SellerID == userid && x.Status == 3).Count();
                ViewBag.buyerRequests = DBobj.DownloadNotes.Where(x => x.IsActive == true && x.SellerID == userid).Count();

                List<NoteDetails> SellerNotes = null;
                if (submit == "Search")
                {
                    SellerNotes = DBobj.NoteDetails.Where(x => x.IsActive == true && x.SellerID == userid && (x.Status == 4 || x.Status == 1 || x.Status==5) &&
                                                        (x.NoteTitle.StartsWith(searchfilter) || searchfilter == null)).ToList();
                }
                else
                {
                    SellerNotes = DBobj.NoteDetails.Where(x => x.IsActive == true && x.SellerID == userid && (x.Status == 4 || x.Status == 1 || x.Status == 5)).ToList();
                }
                
                var ProgressNotes = (from sell in SellerNotes
                                     join cate in DBobj.NoteCategories.ToList() on sell.NoteCategoryID equals cate.NoteCategoryID
                                     orderby sell.CreatedDate descending
                                     join stu in DBobj.NoteStatus.ToList() on sell.Status equals stu.NoteStatusID
                                     select new MyProgressNotes
                                     {
                                         SellerNotes = sell,
                                         Category = cate,
                                         status = stu
                                     }).ToList();
                ViewBag.inProgressNotes = ProgressNotes.ToPagedList(page ?? 1,5);
                ViewBag.inProgressNotesCount = ProgressNotes.Count();

                List<NoteDetails> PublishedNote = null;
                if (submit == "Search2")
                {
                    PublishedNote = DBobj.NoteDetails.Where(x => x.IsActive == true && x.SellerID == userid && x.Status == 2 && 
                                                           (x.NoteTitle.StartsWith(searchfilter) || searchfilter == null)).ToList();
                }
                else
                {
                    PublishedNote = DBobj.NoteDetails.Where(x => x.IsActive == true && x.SellerID == userid && x.Status == 2).ToList();
                }
               
                var PublishedNoted = (from sell in PublishedNote
                                      join cate in DBobj.NoteCategories.ToList() on sell.NoteCategoryID equals cate.NoteCategoryID
                                      orderby sell.PublishedDate descending
                                      select new MyPublishNotes
                                      {
                                          SellerNotes = sell,
                                          Category = cate,

                                      }).ToList();
                ViewBag.publishedNote = PublishedNoted.ToPagedList(page2 ?? 1,5);
                ViewBag.publishedNoteCount = PublishedNoted.Count();

                return View();
            }

        }



        //GET: UserProfile
        [Authorize]
        public ActionResult userProfile()
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                ViewBag.countries = new SelectList(DBobj.Countries.ToList(), "CountryID", "CountryName");
                ViewBag.countryCode = new SelectList(DBobj.Countries.ToList(), "CountryID", "CountryCode");

                int id = (int)Session["UserID"];
                Users user = DBobj.Users.Where(x => x.UserID == id).FirstOrDefault();
                UserProfile userprofiles = DBobj.UserProfile.Where(x => x.UserID == id).FirstOrDefault();
                UserProfileData upd = new UserProfileData();

                if (user != null)
                {
                    upd.FirstName = user.FirstName;
                    upd.LastName = user.LastName;
                    upd.EmailID = user.EmailID;

                    if (userprofiles != null)
                    {
                        upd.DateOfBirth = userprofiles.DateOfBirth;
                        upd.PhoneNumber = userprofiles.PhoneNumber;
                        upd.AddressLine1 = userprofiles.AddressLine1;
                        upd.AddressLine2 = userprofiles.AddressLine2;
                        upd.City = userprofiles.City;
                        upd.State = userprofiles.State;
                        upd.ZipCode = userprofiles.ZipCode;
                        upd.University = userprofiles.University;
                        upd.College = userprofiles.College;
                        upd.CountryCode = userprofiles.CountryCode;
                        upd.CountryID = userprofiles.CountryID;
                        upd.Gender = userprofiles.Gender;
                        upd.ProfilePic = userprofiles.ProfilePicture;
                    }
                    Session["dpPath"] = userprofiles.ProfilePicture;
                    return View(upd);
                }
                Session["dpPath"] = userprofiles.ProfilePicture;
                return View();
            }
        }
        //POST: UserProfile
        [Authorize]
        [HttpPost]
        public ActionResult userProfile(UserProfileData upd)
        {
            if (ModelState.IsValid)
            {
                using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
                {
                    int id = (int)Session["UserID"];
                    ViewBag.countries = new SelectList(DBobj.Countries.ToList(), "CountryID", "CountryName");
                    ViewBag.countryCode = new SelectList(DBobj.Countries.ToList(), "CountryID", "CountryCode");
                    Users u = DBobj.Users.Where(x => x.UserID == id).FirstOrDefault();
                    u.FirstName = upd.FirstName;
                    u.LastName = upd.LastName;


                    int p = DBobj.UserProfile.Where(x => x.UserID == id).Count();
                    if (p > 0)
                    {
                        UserProfile profile = DBobj.UserProfile.Where(x => x.UserID == id).FirstOrDefault();

                        profile.UserID = (int)Session["UserID"];
                        profile.State = upd.State;
                        profile.CountryCode = upd.CountryCode;
                        profile.CountryID = upd.CountryID;
                        profile.AddressLine1 = upd.AddressLine1;
                        profile.AddressLine2 = upd.AddressLine2;
                        profile.DateOfBirth = upd.DateOfBirth;
                        profile.Gender = upd.Gender;
                        profile.IsActive = true;
                        profile.ModifiedDate = DateTime.Now;
                        profile.University = upd.University;
                        profile.ZipCode = upd.ZipCode;
                        profile.College = upd.College;
                        profile.City = upd.City;
                        profile.SecondaryEmailAddress = upd.SecondaryEmailAddress;
                        profile.PhoneNumber = upd.PhoneNumber;

                        DBobj.SaveChanges();
                        string path = Path.Combine(Server.MapPath("~/Member/" + Session["UserID"].ToString()));

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        if (upd.ProfilePicture != null && upd.ProfilePicture.ContentLength > 0)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(upd.ProfilePicture.FileName);
                            string extension = Path.GetExtension(upd.ProfilePicture.FileName);
                            fileName = "DP_" + DateTime.Now.ToString("ddMMyyyy") + extension;
                            string finalpath = Path.Combine(path, fileName);
                            upd.ProfilePicture.SaveAs(finalpath);

                            profile.ProfilePicture = Path.Combine(("/Member/" + Session["UserID"].ToString()) + "/", fileName);
                            DBobj.SaveChanges();
                        }

                    }
                    else
                    {
                        UserProfile profile = new UserProfile();

                        profile.UserID = (int)Session["UserID"];
                        profile.State = upd.State;
                        profile.CountryCode = upd.CountryCode;
                        profile.CountryID = upd.CountryID;
                        profile.AddressLine1 = upd.AddressLine1;
                        profile.AddressLine2 = upd.AddressLine2;
                        profile.DateOfBirth = upd.DateOfBirth;
                        profile.Gender = upd.Gender;
                        profile.IsActive = true;
                        profile.ModifiedDate = DateTime.Now;
                        profile.University = upd.University;
                        profile.ZipCode = upd.ZipCode;
                        profile.College = upd.College;
                        profile.City = upd.City;
                        profile.SecondaryEmailAddress = upd.SecondaryEmailAddress;
                        profile.PhoneNumber = upd.PhoneNumber;
                        DBobj.UserProfile.Add(profile);
                        DBobj.SaveChanges();

                        string path = Path.Combine(Server.MapPath("~/Member/" + Session["UserID"].ToString()));

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        if (upd.ProfilePicture != null && upd.ProfilePicture.ContentLength > 0)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(upd.ProfilePicture.FileName);
                            string extension = Path.GetExtension(upd.ProfilePicture.FileName);
                            fileName = "DP_" + DateTime.Now.ToString("ddMMyyyy") + extension;
                            string finalpath = Path.Combine(path, fileName);
                            upd.ProfilePicture.SaveAs(finalpath);

                            profile.ProfilePicture = Path.Combine(("/Member/" + Session["UserID"].ToString() + "/"), fileName);
                            DBobj.SaveChanges();
                            return RedirectToAction("dashboard", "User");
                        }
                    }
                }
            }
            
            using(NotesMarketplaceEntities DBobj=new NotesMarketplaceEntities())
            {
                ViewBag.countries = new SelectList(DBobj.Countries.ToList(), "CountryID", "CountryName");
                ViewBag.countryCode = new SelectList(DBobj.Countries.ToList(), "CountryID", "CountryCode");
                return RedirectToAction("dashboard", "User");
            }
            
        }



        //GET: BuyerRequests
        [Authorize]
        public ActionResult buyerRequests(int? page, string brsearch)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                int id = (int)Session["UserID"];

                var by = (from n in DBobj.NoteDetails.Where(x=> x.IsActive == true && (x.NoteTitle.StartsWith(brsearch) || brsearch == null))
                          join dn in DBobj.DownloadNotes on n.NoteID equals dn.NoteID
                          where dn.SellerID == id orderby dn.AttachmentDownloadDate descending
                          join cat in DBobj.NoteCategories on n.NoteCategoryID equals cat.NoteCategoryID
                          join usr in DBobj.Users on dn.BuyerID equals usr.UserID
                          join up in DBobj.UserProfile on dn.BuyerID equals up.UserID
                          join cc in DBobj.Countries on up.CountryID equals cc.CountryID
                          select new MyProgressNotes
                          {
                              downloadnote = dn,
                              u = usr,
                              SellerNotes = n,
                              Category = cat,
                              userprofile = up,
                              country = cc

                          }).ToList();

                ViewBag.BuyerRequests = by.ToPagedList(page ?? 1,5);
                ViewBag.BuyerRequestsCount = by.Count();
            }
            return View();
        }



        [Authorize]
        [Route("acceptDownloadRequest /{id}")]
        public ActionResult acceptDownloadRequest(int? id)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                var accepted = DBobj.DownloadNotes.Where(x => x.NoteID == id && x.IsSellerHasAllowedDownload == false).FirstOrDefault();
                accepted.IsSellerHasAllowedDownload = true;
                accepted.ModifiedBy = (int)Session["UserID"];
                accepted.ModifiedDate = DateTime.Now;
                var path = DBobj.SellerNoteAttachment.Where(x => x.NoteID == id && x.FilePath != null).FirstOrDefault();
                accepted.AttachmentPath = path.FilePath;
                DBobj.SaveChanges();

                int buyerId = accepted.BuyerID;
                var buyerInfo = DBobj.Users.Where(x => x.UserID == buyerId);
                foreach (var i in buyerInfo)
                {
                    ViewBag.buyerName = i.FirstName + " " + i.LastName;
                    ViewBag.emailId = i.EmailID;
                }

                ViewBag.sellerName = Session["FullName"];
                informBuyer.mailToBuyer(ViewBag.buyerName, ViewBag.emailId, ViewBag.sellerName);
            }
            return RedirectToAction("buyerRequests", "User");
        }



        //GET: ChangePassword
        [Authorize]
        public ActionResult changePassword()
        {
            return View();
        }
        //POST: ChangePassword
        [HttpPost]
        public ActionResult changePassword(ChangePass cp)
        {

            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())

            {
                int id = (int)Session["UserID"];
                Users u = DBobj.Users.Where(x => x.UserID == id).FirstOrDefault();
                if (u.Password == cp.Password)
                {
                    u.Password = cp.NewPassword;
                    DBobj.SaveChanges();
                    ViewBag.PassMessage = "<p><span><i class='fas fa-check-circle'></i></span> Your Password has been Changed successfully</p>";
                }

            }
            return View();
        }


        //GET: FAQ
        public ActionResult faq()
        {
            return View();
        }



        //GET: Home
        public ActionResult home()
        {
            return View();
        }



        //GET: MyDownloads
        [Authorize]
        public ActionResult myDownloads(string downloadSearch, int? page)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {

                DBobj.Configuration.LazyLoadingEnabled = false;
                int id = (int)Session["Userid"];

                var downloadNotes = (from dn in DBobj.DownloadNotes
                                     join n in DBobj.NoteDetails.Where(x => x.IsActive == true && (x.NoteTitle.StartsWith(downloadSearch) || downloadSearch == null)) on dn.NoteID equals n.NoteID
                                     where dn.BuyerID == id && dn.IsSellerHasAllowedDownload == true && dn.IsActive == true
                                     join nc in DBobj.NoteCategories on n.NoteCategoryID equals nc.NoteCategoryID
                                     join usr in DBobj.Users on dn.SellerID equals usr.UserID
                                     select new MyProgressNotes
                                     {
                                         u = usr,
                                         SellerNotes = n,
                                         downloadnote = dn,
                                         Category = nc
                                     }).ToList();

                ViewBag.DownloadNotes = downloadNotes.ToPagedList(page ?? 1, 5);
                ViewBag.DownloadNotesCount = downloadNotes.Count();
            }
            return View();
        }



        [Authorize]
        [Route("noteReview/noteID")]
        //GET: NoteReview
        public ActionResult noteReview(NoteReviews model, int noteID)
        {
            if (ModelState.IsValid)
            {
                using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
                {
                    int id = (int)Session["UserID"];

                    NoteReviews nr = new NoteReviews();
                    var downloaddata = DBobj.DownloadNotes.Where(x => x.NoteID == noteID && x.BuyerID == id).FirstOrDefault();
                    nr.AgainstDownloadID = downloaddata.DownloadNoteID;
                    nr.ReviewByID = id;
                    nr.Ratings = model.Ratings;
                    nr.Comments = model.Comments;
                    nr.NoteID = noteID;
                    nr.IsActive = true;
                    nr.CreatedDate = DateTime.Now;
                    nr.CreatedBy = id;
                    nr.ModifiedBy = id;
                    nr.ModifiedDate = DateTime.Now;

                    DBobj.NoteReviews.Add(nr);
                    DBobj.SaveChanges();

                }
            }
            return RedirectToAction("myDownloads", "User");
        }



        [Authorize]
        [Route("spamReport/noteID1")]
        //GET: SpamReport
        public ActionResult spamReport(string remark, int noteID1)
        {
            if (remark != null)
            {
                using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
                {
                    int user = (int)Session["UserID"];
                    var n = DBobj.NoteDetails.Where(x => x.NoteID == noteID1).FirstOrDefault();
                    var dn = DBobj.DownloadNotes.Where(x => x.NoteID == noteID1 && x.BuyerID == user).FirstOrDefault();
                    var s = DBobj.Users.Where(x => x.UserID == n.SellerID).FirstOrDefault();
                    int count = DBobj.SpamReports.Where(x => x.NoteID == noteID1 && x.ReportByID == user).Count();
                    if (count > 0)
                    {
                        ViewBag.spammsg = "You have already reported for this note";
                    }
                    else
                    {
                        SpamReports sr = new SpamReports();
                        sr.NoteID = noteID1;
                        sr.ReportByID = user;
                        sr.Remark = remark;
                        sr.AgainstDownloadID = dn.DownloadNoteID;
                        sr.IsActive = true;
                        sr.CreatedBy = user;
                        sr.ModifiedBy = user;
                        sr.ModifiedDate = DateTime.Now;
                        sr.CreatedDate = DateTime.Now;

                        DBobj.SpamReports.Add(sr);
                        DBobj.SaveChanges();

                        spamReportToAdmin.spamReport(s.FirstName, Session["FullName"].ToString(), n.NoteTitle);
                        return RedirectToAction("myDownloads", "User");
                    }
                    return RedirectToAction("myDownloads", "User");
                }
            }
            return RedirectToAction("myDownloads", "User");
        }



        [Authorize]
        //GET: MyRejectedNotes
        public ActionResult myRejectedNotes(int ? page, string mrsearch)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                int id = (int)Session["UserID"];
                List<MyProgressNotes> rejected = (from n in DBobj.NoteDetails.Where(x => x.IsActive == true && (x.NoteTitle.StartsWith(mrsearch) || mrsearch == null))
                                join cat in DBobj.NoteCategories on n.NoteCategoryID equals cat.NoteCategoryID
                                where n.Status == 3 && n.SellerID == id
                                select new MyProgressNotes
                                {
                                    SellerNotes = n,
                                    Category = cat
                                }).ToList();

                ViewBag.rejectedNote = rejected.ToPagedList(page ?? 1, 5);
                ViewBag.rejectedNoteCount = rejected.Count();
            }
            return View();
        }



        [Authorize]
        //GET: MySoldNotes
        public ActionResult mySoldNotes(int? page, string mssearch)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                int id = (int)Session["UserID"];
                List<MyProgressNotes> soldnotes = (from n in DBobj.NoteDetails.Where(x => x.IsActive == true && (x.NoteTitle.StartsWith(mssearch) || mssearch == null))
                                 join dn in DBobj.DownloadNotes on n.NoteID equals dn.NoteID
                                 where dn.IsSellerHasAllowedDownload == true && dn.SellerID == id
                                 join cat in DBobj.NoteCategories on n.NoteCategoryID equals cat.NoteCategoryID
                                 join usr in DBobj.Users on dn.BuyerID equals usr.UserID
                                 select new MyProgressNotes
                                 {
                                     SellerNotes = n,
                                     downloadnote = dn,
                                     Category = cat,
                                     u = usr
                                 }).ToList();

                ViewBag.mysoldnotes = soldnotes.ToPagedList(page ?? 1, 5);
                ViewBag.mysoldnotesCount = soldnotes.Count();
            }
            return View();
        }



        [Authorize]
        //GET: DownloadFlow
        public ActionResult downloadFlow(int id)
        {
            if (Session["UserID"] != null)
            {
                using (NotesMarketplaceEntities Dbobj = new NotesMarketplaceEntities())
                {
                    int sellerId = 0;

                    //Free Note
                    int isFree = Dbobj.NoteDetails.Where(x => x.NoteID == id && x.SellType == "Free").Count();
                    if (isFree > 0)
                    {
                        DownloadNotes dn = new DownloadNotes();
                        var notetitle = Dbobj.NoteDetails.Where(x => x.NoteID == id);
                        var sellerAttachement = Dbobj.SellerNoteAttachment.Where(x => x.NoteID == id).FirstOrDefault();

                        dn.BuyerID = (int)Session["UserID"];
                        dn.IsSellerHasAllowedDownload = true;
                        dn.IsPaid = false;
                        dn.IsAttachmentDownloaded = true;
                        dn.CreatedDate = DateTime.Now;
                        dn.CreatedBy = (int)Session["UserID"];
                        dn.IsActive = true;
                        dn.AttachmentDownloadDate = DateTime.Now;
                        dn.AttachmentPath = sellerAttachement.FilePath;
                        foreach (var iv in notetitle)
                        {
                            dn.NoteID = iv.NoteID;
                            dn.SellerID = iv.SellerID;
                            dn.PurchasePrice = iv.SellPrice;
                            dn.NoteTitle = iv.NoteTitle;
                            dn.NoteCategory = (iv.NoteCategoryID).ToString();
                            sellerId = iv.SellerID;
                        }

                        Dbobj.DownloadNotes.Add(dn);
                        Dbobj.SaveChanges();

                        //Return files

                        var filesPath = sellerAttachement.FilePath.Split(';');
                        var filesName = sellerAttachement.FileName.Split(';');
                        using (var ms = new MemoryStream())
                        {

                            using (var z = new ZipArchive(ms, ZipArchiveMode.Create, true))
                            {
                                foreach (var FilePath in filesPath)
                                {
                                    string FullPath = Path.Combine(Server.MapPath(FilePath));
                                    string FileName = Path.GetFileName(FullPath);
                                    if (FileName == "User")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        z.CreateEntryFromFile(FullPath, FileName);
                                    }
                                }
                            }
                            return File(ms.ToArray(), "application/zip", "Attachement.zip");
                        }
                    }


                    List<MyProgressNotes> Seller = (from n in Dbobj.NoteDetails
                                  join usr in Dbobj.Users on n.SellerID equals usr.UserID
                                  where n.NoteID == id
                                  select new MyProgressNotes
                                  {
                                      SellerNotes = n,
                                      u = usr
                                  }).ToList();

                    IEnumerable<NoteMarketPlace.Models.MyProgressNotes> sellername = Seller;
                    foreach (var iv in sellername)
                    {
                        ViewBag.SellerName = iv.u.FirstName + " " + iv.u.LastName;
                    }
                    var isPaid = Dbobj.NoteDetails.Where(x => x.NoteID == id && x.SellType == "Paid").Count();
                    if (isPaid > 0)
                    {
                        DownloadNotes dn = new DownloadNotes();
                        var notetitle = Dbobj.NoteDetails.Where(x => x.NoteID == id);

                        dn.BuyerID = (int)Session["UserID"];
                        dn.IsSellerHasAllowedDownload = false;
                        dn.IsPaid = true;
                        dn.IsAttachmentDownloaded = false;
                        dn.CreatedDate = DateTime.Now;
                        dn.CreatedBy = (int)Session["UserID"];
                        dn.IsActive = true;
                        dn.AttachmentDownloadDate = DateTime.Now;
                        foreach (var iv in notetitle)
                        {
                            dn.NoteID = iv.NoteID;
                            dn.SellerID = iv.SellerID;
                            dn.PurchasePrice = iv.SellPrice;
                            dn.NoteTitle = iv.NoteTitle;
                            dn.NoteCategory = (iv.NoteCategoryID).ToString();
                            sellerId = iv.SellerID;
                        }

                        Dbobj.DownloadNotes.Add(dn);
                        Dbobj.SaveChanges();

                        var sellerInfo = Dbobj.Users.Where(x => x.UserID == sellerId).ToList();
                        foreach (var s in sellerInfo)
                        {
                            ViewBag.sellerName = s.FirstName + " " + s.LastName;
                            ViewBag.sellerEmailId = s.EmailID;
                        }

                        string buyerName = (string)Session["FullName"];

                        //Send mail to owner
                        informSeller.SellerPublishNote(ViewBag.sellerName, ViewBag.sellerEmailId, buyerName);
                    }
                }
                return RedirectToAction("noteDetails", "User", new { id = id });
            }
            else
            {
                return RedirectToAction("login", "User");
            }
        }



        [Authorize]
        [Route("adminDownloadNote/id")]
        //GET: AdminDownloadNote
        public ActionResult userDownloadNote(int id)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                SellerNoteAttachment sellerAttachement = DBobj.SellerNoteAttachment.Where(x => x.NoteID == id).FirstOrDefault();

                //Return files

                var filesPath = sellerAttachement.FilePath.Split(';');
                var filesName = sellerAttachement.FileName.Split(';');
                using (var ms = new MemoryStream())
                {
                    using (var z = new ZipArchive(ms, ZipArchiveMode.Create, true))
                    {
                        foreach (var FilePath in filesPath)
                        {
                            string FullPath = Path.Combine(Server.MapPath(FilePath));
                            string FileName = Path.GetFileName(FullPath);
                            if (FileName == "userDownloadNote")
                            {
                                continue;
                            }
                            else
                            {
                                z.CreateEntryFromFile(FullPath, FileName);
                            }
                        }
                    }
                    return File(ms.ToArray(), "application/zip", "Attachement.zip");
                }
            }
        }
    }
}
using NoteMarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using System.IO.Compression;
using System.IO;
using NoteMarketPlace.EmailTemplates;

namespace NoteMarketPlace.Controllers
{
    public class AdminController : Controller
    {


        //GET: Login
        public ActionResult login()
        {
            return View();
        }


        //GET: Logout
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login", "User");
        }



        // GET: Admin
        [Authorize]
        [Route("addAdministrator/id")]
        public ActionResult addAdministrator(int? id)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();
            ViewBag.countryCode = new SelectList(DBobj.Countries.ToList(), "CountryID", "CountryCode");
            Users udetail = DBobj.Users.Where(x => x.UserID == id).FirstOrDefault();
            UserProfile updetail = DBobj.UserProfile.Where(x => x.UserID == id).FirstOrDefault();
            return View(udetail);
        }
        // POST: Admin
        [Authorize]
        [HttpPost]
        public ActionResult addAdministrator(adminSignUp model)
        {
            if (ModelState.IsValid)
            {

                using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
                {
                    Users u = new Users();
                    u.FirstName = model.FirstName;
                    u.LastName = model.LastName;
                    u.EmailID = model.EmailID;
                    u.UserRoleID = 2;
                    u.IsActive = true;
                    u.CreatedDate = DateTime.Now;
                    u.Password = "Admin@123";
                    u.IsEmailVerified = true;

                    DBobj.Users.Add(u);
                    DBobj.SaveChanges();

                    if (u.UserID > 0)
                    {
                        UserProfile up = new UserProfile();
                        up.UserID = u.UserID;
                        up.CountryCode = model.CountryCode;
                        up.PhoneNumber = model.PhoneNumber;
                        up.AddressLine1 = "null";
                        up.City = "null";
                        up.State = "null";
                        up.ZipCode = "null";
                        up.CountryID = 1;
                        up.CreatedDate = DateTime.Now;
                        up.IsActive = true;

                        DBobj.UserProfile.Add(up);
                        DBobj.SaveChanges();
                        ModelState.Clear();
                        var countrycode = DBobj.Countries.ToList();
                        ViewBag.countryCode = new SelectList(countrycode, "CountryCode", "CountryCode");
                        ViewBag.IsSuccess = "<p><span><i class='fas fa-check-circle'></i></span> Admin added successfully.</p>";
                    }
                }

            }
            
            return View();
        }


        
        // GET: AddType
        [Authorize]
        [Route("addType/id")]
        public ActionResult addType(int? id)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();
            NoteType typedata = DBobj.NoteType.Where(x => x.NoteTypeID == id).FirstOrDefault();
            return View(typedata);
            
        }
        // POST: AddType
        [Authorize]
        [HttpPost]
        public ActionResult addType(NoteType model)
        {
            int id = (int)Session["UserID"];
            if (ModelState.IsValid)
            {
                using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
                {
                    NoteType notetype = new NoteType();
                    notetype.TypeName = model.TypeName;
                    notetype.Description = model.Description;
                    notetype.IsActive = true;
                    notetype.ModifiedBy = id;
                    notetype.ModifiedDate = DateTime.Now;
                    notetype.CreatedDate = DateTime.Now;
                    notetype.CreatedBy = id;

                    DBobj.NoteType.Add(notetype);
                    DBobj.SaveChanges();

                    ModelState.Clear();
                    ViewBag.typeSuccess = "<p><span><i class='fas fa-check-circle'></i></span> Type added successfully.</p>";
                }

            }
            return View();
        }



        // GET: AddCountry
        [Authorize]
        [Route("addCountry/id")]
        public ActionResult addCountry(int? id)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();
            Countries countrydata = DBobj.Countries.Where(x => x.CountryID == id).FirstOrDefault();
            return View(countrydata);
        }
        // POST: AddCountry
        [Authorize]
        [HttpPost]
        public ActionResult addCountry(Countries model)
        {
            if (ModelState.IsValid)
            {
                using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
                {
                    Countries cr = new Countries();
                    cr.CountryCode = model.CountryCode;
                    cr.CountryName = model.CountryName;
                    cr.IsActive = true;
                    cr.CreatedDate = DateTime.Now;

                    DBobj.Countries.Add(cr);
                    DBobj.SaveChanges();

                    ModelState.Clear();
                    ViewBag.countrySuccess = "<p><span><i class='fas fa-check-circle'></i></span> Country added successfully.</p>";
                }
            }
            return View();
        }



        // GET: AddCategory
        [Authorize]
        [Route("addCategory/id")]
        public ActionResult addCategory(int? id)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();
            NoteCategories catdata = DBobj.NoteCategories.Where(x => x.NoteCategoryID == id).FirstOrDefault();
            return View(catdata);
        }
        // POST: AddCategory
        [Authorize]
        [HttpPost]
        public ActionResult addCategory(NoteCategories model)
        {
            if (ModelState.IsValid)
            {
                using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
                {
                    NoteCategories nc = new NoteCategories();
                    nc.CategoryName = model.CategoryName;
                    nc.Description = model.Description;
                    nc.IsActive = true;
                    nc.CreatedDate = DateTime.Now;

                    DBobj.NoteCategories.Add(nc);
                    DBobj.SaveChanges();

                    ModelState.Clear();
                    ViewBag.categorySuccess = "<p><span><i class='fas fa-check-circle'></i></span> Category added successfully.</p>";
                }
            }
            return View();
        }



        // GET: ChangePassword
        [Authorize]
        public ActionResult changePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
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



        // GET: ManageType
        [Authorize]
        public ActionResult manageType(string typesearch, int? page)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                IEnumerable<typeuser> typeuser = (from n in DBobj.NoteType.Where(x => x.IsActive == true && (x.TypeName.StartsWith(typesearch) || typesearch == null)).ToList()
                                join u in DBobj.Users.ToList() on n.CreatedBy equals u.UserID
                                select new typeuser
                                {
                                    types = n,
                                    user = u
                                });
                ViewBag.tulist = typeuser.ToPagedList(page ?? 1, 5);
                ViewBag.tulistCount = typeuser.Count();
                return View();
            }
                
        }
        // POST: ManageType
        [Authorize]
        [HttpPost]
        public ActionResult manageType(NoteType model)
        {
            return View();
        }



        // GET: ManageCategory
        [Authorize]
        public ActionResult manageCategory(string catsearch, int? page)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                IEnumerable<typeuser> catusr = (from n in DBobj.NoteCategories.Where(x => x.IsActive == true && (x.CategoryName.StartsWith(catsearch) || catsearch == null)).ToList()
                              join u in DBobj.Users.ToList() on n.CreatedBy equals u.UserID
                              select new typeuser
                              {
                                  categorydata = n,
                                  user = u
                              });
                ViewBag.culist = catusr.ToPagedList(page ?? 1, 5);
                ViewBag.culistCount = catusr.Count();
                return View();
            }
        }
        // POST: ManageCategory
        [HttpPost]
        [Authorize]
        public ActionResult manageCategory(NoteCategories model)
        {
            return View();
        }



        // GET: ManageCountry
        [Authorize]
        public ActionResult manageCountry(string countysearch, int? page)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                IEnumerable<typeuser> cousr = (from co in DBobj.Countries.Where(x => x.IsActive == true && (x.CountryName.StartsWith(countysearch) || countysearch == null)).ToList()
                             join u in DBobj.Users.ToList() on co.CreatedBy equals u.UserID
                             select new typeuser
                             {
                                 countrydata = co,
                                 user = u
                             });
                ViewBag.colist = cousr.ToPagedList(page ?? 1, 5);
                ViewBag.colistCount = cousr.Count();
                return View();
            }
        }
        // POST: ManageCountry
        [Authorize]
        [HttpPost]
        public ActionResult manageCountry(Countries model)
        {
            return View();
        }



        // GET: ManageSystemConfiguration
        [Authorize]
        public ActionResult manageSystemConfiguration()
        {
            using(NotesMarketplaceEntities DBobj=new NotesMarketplaceEntities())
            {
                var msc = DBobj.SystemConfigurations.ToList();
                ViewBag.msclist = msc;
                return View();
            }
            
        }
        // POST: ManageSystemConfiguration
        [Authorize]
        [HttpPost]
        public ActionResult manageSystemConfiguration(SystemConfigurations model)
        {
            return View();
        }



        // GET: ManageAdministrator
        [Authorize]
        public ActionResult manageAdministrator(int? page, string adminsearch)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();

            IQueryable<MyProgressNotes> admins = (from usr in DBobj.Users
                          where usr.UserRoleID == 2 && usr.IsActive == true && (usr.FirstName.StartsWith(adminsearch) || adminsearch == null)
                          join up in DBobj.UserProfile on usr.UserID equals up.UserID
                          select new MyProgressNotes
                          {
                              userprofile = up,
                              u = usr
                          });

            ViewBag.administrator = admins.ToPagedList(page ?? 1, 5);
            ViewBag.administratorCount = admins.Count();

            return View();
        }
        // POST: ManageAdministrator
        [Authorize]
        [HttpPost]
        public ActionResult manageAdministrator(Users model)
        {

            return View();
        }



        [Authorize]
        [Route("deleteType/id")]
        //GET: DeleteType
        public ActionResult deleteType(int id)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();
            var typedetail = DBobj.NoteType.Where(x => x.NoteTypeID == id).FirstOrDefault();
            typedetail.IsActive = false;
            DBobj.SaveChanges();
            return RedirectToAction("manageType", "Admin");
        }



        [Authorize]
        [Route("deleteCategory/id")]
        //GET: DeleteCategory
        public ActionResult deleteCategory(int id)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();
            var categorydetail = DBobj.NoteCategories.Where(x => x.NoteCategoryID == id).FirstOrDefault();
            categorydetail.IsActive = false;
            DBobj.SaveChanges();
            return RedirectToAction("manageCategory", "Admin");
        }



        [Authorize]
        [Route("deleteCountry/id")]
        //GET: DeleteCountry
        public ActionResult deleteCountry(int id)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();
            var countrydetail = DBobj.Countries.Where(x => x.CountryID == id).FirstOrDefault();
            countrydetail.IsActive = false;
            DBobj.SaveChanges();
            return RedirectToAction("manageCountry", "Admin");
        }



        [Authorize]
        [Route("deleteAdmin/id")]
        //GET: DeleteAdmin
        public ActionResult deleteAdmin(int id)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();
            var admindetail = DBobj.Users.Where(x => x.UserID == id).FirstOrDefault();
            admindetail.IsActive = false;
            DBobj.SaveChanges();
            return RedirectToAction("manageAdministrator", "Admin");
        }



        [Authorize]
        //GET: Dashboard
        public ActionResult dashboard(string dashsearch, int? Month, int? page)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();

            var seven = DateTime.Now.AddDays(-7);
            ViewBag.noteunderreview = DBobj.NoteDetails.Where(x => x.Status == 4 && x.IsActive == true).Count();
            ViewBag.downlodednote = DBobj.DownloadNotes.Where(x => x.IsActive == true && x.IsSellerHasAllowedDownload == true && x.CreatedDate > seven).Count();
            ViewBag.newregistration = DBobj.Users.Where(x => x.IsActive == true && x.CreatedDate > seven && x.UserRoleID == 3).Count();

            var notes = DBobj.NoteDetails.Where(x => x.Status == 2 && x.IsActive == true &&
            (x.NoteTitle.StartsWith(dashsearch) || dashsearch == null) && (x.PublishedDate.Value.Month == Month || String.IsNullOrEmpty(Month.ToString()))).ToList();
            var dnotes = DBobj.DownloadNotes.Where(x => x.IsSellerHasAllowedDownload == true && x.IsActive == true).ToList();
            var publishedNotes = (from n in notes
                                  join ct in DBobj.NoteCategories.ToList() on n.NoteCategoryID equals ct.NoteCategoryID
                                  join usr in DBobj.Users.ToList() on n.SellerID equals usr.UserID
                                  join sn in DBobj.SellerNoteAttachment.ToList() on n.NoteID equals sn.NoteID
                                  select new MyProgressNotes
                                  {
                                      Category = ct,
                                      u = usr,
                                      SellerNotes = n,
                                      sna = sn
                                  });

            ViewBag.publishednotes = publishedNotes.ToPagedList(page ?? 1, 5);
            ViewBag.publishednotesCount = publishedNotes.Count();
            return View();


        }



        //GET: Members
        [Authorize]
        public ActionResult members(int? page, string membersearch)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();
            var members = DBobj.Users.Where(x => x.IsActive == true && x.UserRoleID == 3 && ((x.FirstName + " " + x.LastName).StartsWith(membersearch) || membersearch == null)).ToList();

            ViewBag.members = members.ToPagedList(page ?? 1,5);
            ViewBag.membersCount = members.Count();

            return View();
        }


        
        [Authorize]
        [Route("memberDetails/id")]
        //GET: MemberDetails
        public ActionResult memberDetails(int id, int? page)
        {

            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();
            var userProfileDate = (from usr in DBobj.Users.Where(x => x.UserID == id).ToList()
                                   join updata in DBobj.UserProfile.Where(x => x.UserID == id).ToList() on usr.UserID equals updata.UserID
                                   join n in DBobj.NoteDetails on usr.UserID equals n.SellerID
                                   join cat in DBobj.NoteCategories on n.NoteCategoryID equals cat.NoteCategoryID
                                   join cn in DBobj.Countries on n.CountryID equals cn.CountryID
                                   join st in DBobj.NoteStatus on n.Status equals st.NoteStatusID
                                   select new MyProgressNotes
                                   {
                                       u = usr,
                                       userprofile = updata,
                                       SellerNotes = n,
                                       Category = cat,
                                       country = cn,
                                       status = st
                                   }).ToList();

            ViewBag.profile = userProfileDate.ToPagedList(page ?? 1, 5);
            ViewBag.profileCount = userProfileDate.Count();
            return View();
        }



        [Authorize]
        //GET: NotesUnderReview
        public ActionResult notesUnderReview(string FirstName,string nursearch, int? page)
        {
            using(NotesMarketplaceEntities DBobj=new NotesMarketplaceEntities())
            {                
                var adminnotesunderreview = (from n in DBobj.NoteDetails.Where(x => x.IsActive == true && (x.Status == 4 || x.Status == 5) && (x.NoteTitle.StartsWith(nursearch) || nursearch == null)).ToList()
                                             join cat in DBobj.NoteCategories.ToList() on n.NoteCategoryID equals cat.NoteCategoryID
                                             join usr in DBobj.Users.ToList() on n.SellerID equals usr.UserID where (usr.FirstName == FirstName || String.IsNullOrEmpty(FirstName))
                                             join stu in DBobj.NoteStatus.ToList() on n.Status equals stu.NoteStatusID
                                             select new MyProgressNotes
                                             {
                                                 u = usr,
                                                 SellerNotes = n,
                                                 Category = cat,
                                                 status = stu
                                             }).ToList();

                ViewBag.notesUnderReview = adminnotesunderreview.ToPagedList(page ?? 1,5);
                ViewBag.notesUnderReviewCount = adminnotesunderreview.Count();

                ViewBag.Sellers = new SelectList(DBobj.Users.Where(x=> x.IsActive == true && x.UserRoleID == 3).ToList(), "FirstName", "FirstName");

                return View();
            }
            
        }



        [Authorize]
        [Route("approveNotes/id")]
        //GET: ApproveNote
        public ActionResult approveNotes(int id)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                NoteDetails note = DBobj.NoteDetails.FirstOrDefault(x => x.NoteID == id);
                if (note != null)
                {
                    note.Status = 2;
                    note.PublishedDate = DateTime.Now;
                    note.ActionBy = (int)Session["UserID"];
                    note.ModifiedBy= (int)Session["UserID"];
                    note.ModifiedDate = DateTime.Now;
                    DBobj.SaveChanges();
                }
                return RedirectToAction("notesUnderReview", "Admin");
            }
        }



        [Authorize]
        [Route("rejectNotes/id")]
        //GET: RejectNotes
        public ActionResult rejectNotes(int id, string adminRemarks)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                NoteDetails note = DBobj.NoteDetails.FirstOrDefault(x => x.NoteID == id);
                if (note != null)
                {
                    note.Status = 3;
                    note.AdminRemarks = adminRemarks;
                    note.PublishedDate = DateTime.Now;
                    note.ActionBy = (int)Session["UserID"];
                    note.ModifiedBy = (int)Session["UserID"];
                    note.ModifiedDate = DateTime.Now;
                    DBobj.SaveChanges();
                }
                return RedirectToAction("notesUnderReview", "Admin");
            }
        }



        [Authorize]
        [Route("inReviewNotes/id")]
        //GET: InReviewNotes
        public ActionResult inReviewNotes(int id)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                NoteDetails note = DBobj.NoteDetails.FirstOrDefault(x => x.NoteID == id);
                if (note != null)
                {
                    note.Status = 5;
                    note.PublishedDate = DateTime.Now;
                    note.ActionBy = (int)Session["UserID"];
                    note.ModifiedBy = (int)Session["UserID"];
                    note.ModifiedDate = DateTime.Now;
                    DBobj.SaveChanges();
                }
                return RedirectToAction("notesUnderReview", "Admin");
            }
        }



        [Authorize]
        //GET: PublishedNotes
        public ActionResult publishedNotes(string pnsearch, string FirstName, int? page)
        {
            NotesMarketplaceEntities DBobj1 = new NotesMarketplaceEntities();

                List<MyProgressNotes> adminpublishednotes = (from n in DBobj1.NoteDetails.Where(x => x.IsActive == true && x.Status == 2  && (x.NoteTitle.StartsWith(pnsearch) || pnsearch == null)).ToList()
                                             join cat in DBobj1.NoteCategories.ToList() on n.NoteCategoryID equals cat.NoteCategoryID
                                             join usr in DBobj1.Users.ToList() on n.SellerID equals usr.UserID
                                             where usr.FirstName == FirstName || String.IsNullOrEmpty(FirstName)
                                             join usr1 in DBobj1.Users.ToList() on n.ActionBy equals usr1.UserID
                                             select new MyProgressNotes
                                             {
                                                 u = usr,
                                                 SellerNotes = n,
                                                 Category = cat,
                                                 u1=usr1
                                             }).ToList();

                ViewBag.publishedNotes = adminpublishednotes.ToPagedList(page ?? 1,5);
                ViewBag.publishedNotesCount = adminpublishednotes.Count();

                ViewBag.pnSellers = new SelectList(DBobj1.Users.Where(x => x.UserRoleID == 3).ToList(), "FirstName", "FirstName");

                return View();
                       
        }



        [Authorize]
        [Route("noteDetails/id")]
        //GET: NoteDetails
        public ActionResult noteDetails(int id)
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



        [Authorize]
        //GET: DownloadNotes
        public ActionResult downloadNotes(int? page, string Note, string Seller, string Buyer, string dnsearch)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                var admindownloadnotes = (from dn in DBobj.DownloadNotes
                                          join n in DBobj.NoteDetails.Where(x => x.IsActive == true && x.NoteTitle.StartsWith(dnsearch) || dnsearch == null) on dn.NoteID equals n.NoteID
                                          where (dn.IsSellerHasAllowedDownload == true && dn.AttachmentPath != null && dn.AttachmentDownloadDate != null
                                          && (n.NoteTitle == Note || String.IsNullOrEmpty(Note)))
                                          join nc in DBobj.NoteCategories on n.NoteCategoryID equals nc.NoteCategoryID
                                          join usr in DBobj.Users on dn.SellerID equals usr.UserID
                                          where (usr.FirstName == Seller || String.IsNullOrEmpty(Seller))
                                          join usr1 in DBobj.Users on dn.BuyerID equals usr1.UserID
                                          where (usr1.FirstName == Buyer || String.IsNullOrEmpty(Buyer))
                                          select new MyProgressNotes
                                          {
                                              u = usr,
                                              SellerNotes = n,
                                              Category = nc,
                                              u1 = usr1,
                                              downloadnote = dn

                                          }).ToList();

                ViewBag.downloadNote = admindownloadnotes.ToPagedList(page ?? 1,5);
                ViewBag.downloadNoteCount = admindownloadnotes.Count();

                ViewBag.dnSellers = new SelectList(DBobj.Users.Where(x => x.UserRoleID == 3).ToList(), "FirstName", "FirstName");
                ViewBag.dnBuyers = new SelectList(DBobj.Users.Where(x => x.UserRoleID == 3).ToList(), "FirstName", "FirstName");
                ViewBag.dnNote = new SelectList(DBobj.NoteDetails.ToList(), "NoteTitle", "NoteTitle");
                return View();
            }

        }



        [Authorize]
        //GET: MyProfile
        public ActionResult myProfile()
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();

            ViewBag.countryCode = new SelectList(DBobj.Countries.ToList(), "CountryID", "CountryCode");

            int usrid = (int)Session["UserID"];
            Users user = DBobj.Users.Where(x => x.UserID == usrid && x.IsActive == true).FirstOrDefault();
            UserProfile userprofile = DBobj.UserProfile.Where(x => x.UserID == usrid && x.IsActive == true).FirstOrDefault();
            UserProfileData upd = new UserProfileData();
            
            if (user != null)
            {
                upd.FirstName = user.FirstName;
                upd.LastName = user.LastName;
                upd.EmailID = user.EmailID;
                if (userprofile != null)
                {
                    upd.SecondaryEmailAddress = userprofile.SecondaryEmailAddress;
                    upd.CountryCode = userprofile.CountryCode;
                    upd.PhoneNumber = userprofile.PhoneNumber;
                    upd.ProfilePic = userprofile.ProfilePicture;
                }
                return View(upd);
            }
            return View();
        }



        [Authorize]
        //GET: RejectedNotes
        public ActionResult rejectedNotes(string rnsearch, string Seller, int? page)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();

            var adminRejectedNote = (from n in DBobj.NoteDetails.Where(x => x.IsActive == true && x.Status == 3 && (x.NoteTitle.StartsWith(rnsearch) || rnsearch == null)).ToList()
                                     join cat in DBobj.NoteCategories on n.NoteCategoryID equals cat.NoteCategoryID
                                     join usr in DBobj.Users on n.SellerID equals usr.UserID
                                     where (usr.FirstName == Seller || String.IsNullOrEmpty(Seller))
                                     join usr1 in DBobj.Users on n.ActionBy equals usr1.UserID
                                     select new MyProgressNotes
                                     {
                                         u = usr,
                                         SellerNotes = n,
                                         Category = cat,
                                         u1 = usr1,
                                     }).ToList();

            ViewBag.rejectedNote = adminRejectedNote.ToPagedList(page ?? 1, 5);
            ViewBag.rejectedNoteCount = adminRejectedNote.Count();

            ViewBag.dnSellers = new SelectList(DBobj.Users.Where(x => x.UserRoleID == 3).ToList(), "FirstName", "FirstName");
            return View();
        }



        [Authorize]
        [Route("adminDownloadNote/id")]
        //GET: AdminDownloadNote
        public ActionResult adminDownloadNote(int id)
        {
            using(NotesMarketplaceEntities DBobj=new NotesMarketplaceEntities())
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
                            if (FileName == "adminDownloadNote")
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



        //GET: Forgot
        public ActionResult forgot()
        {
            return View();
        }



        [Authorize]
        //GET: SpamReports
        public ActionResult spamReports(string spamsearch, int? page)
        {
            NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities();
            var spamReports = (from sr in DBobj.SpamReports
                               join usr in DBobj.Users on sr.ReportByID equals usr.UserID
                               join n in DBobj.NoteDetails on sr.NoteID equals n.NoteID
                               where n.IsActive == true && (n.NoteTitle.StartsWith(spamsearch) || spamsearch == null)
                               join c in DBobj.NoteCategories on n.NoteCategoryID equals c.NoteCategoryID
                               select new MyProgressNotes
                               {
                                   Category = c,
                                   u = usr,
                                   spam = sr,
                                   SellerNotes = n
                               }).ToList();
            ViewBag.spams = spamReports.ToPagedList(page ?? 1, 5);
            ViewBag.spamsCount = spamReports.Count();

            return View();
        }       



        [Authorize]
        [Route("unPublishNote/id")]
        //GET: Unpublish Note
        public ActionResult unPublishNote(int id, string adminRemarks)
        {
            using (NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                NoteDetails note = DBobj.NoteDetails.FirstOrDefault(x => x.NoteID == id);
                Users u = DBobj.Users.Where(x => x.UserID == note.SellerID).FirstOrDefault();
                if (note != null)
                {
                    note.Status = 6;
                    note.AdminRemarks = adminRemarks;
                    note.PublishedDate = DateTime.Now;
                    note.ActionBy = (int)Session["UserID"];
                    note.ModifiedBy = (int)Session["UserID"];
                    note.ModifiedDate = DateTime.Now;
                    DBobj.SaveChanges();
                    unPublishNoteToUser.unpublishNote(u.FirstName, u.EmailID, adminRemarks);
                }
                return RedirectToAction("dashboard", "Admin");
            }
        }



        //GET: DeactivateUser
        [Authorize]
        public ActionResult deactivateUser(int uid)
        {
            using(NotesMarketplaceEntities DBobj = new NotesMarketplaceEntities())
            {
                Users udetail = DBobj.Users.Where(x => x.UserID == uid).FirstOrDefault();
                IQueryable<NoteDetails> ndetail = DBobj.NoteDetails.Where(x => x.SellerID == uid);
                foreach(var i in ndetail)
                {
                    i.IsActive = false;
                    SellerNoteAttachment nddetails = DBobj.SellerNoteAttachment.Where(x => x.NoteID == i.NoteID).FirstOrDefault();
                    nddetails.IsActive = false;
                }
                udetail.IsActive = false;

                DBobj.SaveChanges();                
                return View("members", "Admin");
            }
        }

    }
}
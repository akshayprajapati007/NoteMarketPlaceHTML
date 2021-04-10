using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NoteMarketPlace.Models
{
    public class signUp
    {
        public int UserID { get; set; }
        public int UserRoleID { get; set; }
        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter valid email")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        [StringLength(24, ErrorMessage = "The {0} Must be at least {2} characters long.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Enter 6 to 24 digit,one upper latter,one lower latter,one special character")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Re-enter your password")]
        [Compare("Password", ErrorMessage = "Password doesn't match")]
        public string RePassword { get; set; }
        public bool IsEmailVerified { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public UserRoles UserRole { get; set; }
    }
}
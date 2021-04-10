using System.Net;
using System.Net.Mail;

namespace NotesMarketPlace.EmailTemplates
{
    public class informBuyer
    {
        public static void mailToBuyer(string buyerName, string emailId, string sellerName)
        {
            var fromEmail = new MailAddress("SupportEmail", "Notes Marketplace"); //need system email
            var toEmail = new MailAddress(emailId);
            var fromEmailPassword = "SupportEmailPassword"; // Replace with actual password
            string subject = sellerName + " Allows you to download a note";
            string msg = "";
            msg += "Hello " + buyerName + ",<br>";
            msg += "We would like to inform you that, " + sellerName + " Allows you to download a note. Please login and see My Download tabs to download particular note.";
            msg += "<br/><br/>Regards,<br/> Notes MarketPlace";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = msg,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
    }
}
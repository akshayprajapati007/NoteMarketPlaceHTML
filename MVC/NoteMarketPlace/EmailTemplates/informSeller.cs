using System.Net;
using System.Net.Mail;

namespace NotesMarketPlace.EmailTemplates
{
    public class informSeller
    {
        public static void SellerPublishNote(string sellerName, string sellerEmailId, string buyerName)
        {
            var fromEmail = new MailAddress("SupportEmail", "Notes Marketplace"); //need system email
            var toEmail = new MailAddress(sellerEmailId);
            var fromEmailPassword = "SupportEmailPassword"; // Replace with actual password
            string subject = buyerName + " wants to purchase your notes";
            string msg = "Hello,<br/>";
            msg += "Hello" + sellerName + ",<br>";
            msg += "We would like to inform you that, " + buyerName + " wants to purchase your notes. Please see Buyer Requests tab and allow download access to Buyer if you have received the payment from him.";
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
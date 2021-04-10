using System.Net;
using System.Net.Mail;

namespace NoteMarketPlace.EmailTemplates
{
    public class spamReportToAdmin
    {
        public static void spamReport(string sellerName, string memberName, string notetitle)
        {
            var fromEmail = new MailAddress("SupportEmail", "Notes Marketplace"); //need system email
            var toEmail = new MailAddress("SupportEmail2");
            var fromEmailPassword = "SupportEmailPassword"; // Replace with actual password
            string subject = memberName + " Reported an issue for " + notetitle;

            string msg = "Hello Admins,<br>";
            msg += "We want to inform you that," + memberName + "Reported an issue for" + sellerName + " Note with title " + notetitle + ". Please look at the notes and take required actions.";
            msg += " Regards,<br>  Notes Marketplace ";
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
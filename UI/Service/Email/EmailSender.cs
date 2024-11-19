using System.Net;
using System.Net.Mail;

namespace UI.Service.Email
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var frmMail = "PradeepMahor47@outlook.com";
            var pw = "Honor@84240_PMM";

            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
            client.Port = 587; // 25 587
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credential = new NetworkCredential(frmMail, pw);

            client.UseDefaultCredentials = true;
            client.Credentials = credential;
            client.EnableSsl = true;
            string MailSender = new MailAddress(frmMail, "Pradeepkumar R. Mahor").ToString();

            //var clint = new SmtpClient("smtp-mail.outlook.com")
            //{
            //    EnableSsl = true,
            //    Credentials = new NetworkCredential(frmMail, pw, "outlook.com"),
            //    UseDefaultCredentials = false,
            //    Port = 587
            //};

            return client.SendMailAsync(
                new MailMessage(
                    from: MailSender,
                    to: email,
                    subject,
                    message));
        }
    }
}
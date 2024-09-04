using System.Net;
using System.Net.Mail;

namespace CRMEngSystem.Services.Mail
{
    public static class SendMailService
    {
        public static bool Send(string subject, string body, string sender, string receiver, string extrareceiver, string password)
        {
            //password ??= Environment.GetEnvironmentVariable("EMAIL_PASSWORD")!;
            //gkoy pxqk kdey tbrv

            MailMessage mail = new()
            {
                From = new MailAddress(sender),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            try
            {
                mail.To.Add(receiver);
                if(!string.IsNullOrEmpty(extrareceiver)) mail.CC.Add(extrareceiver);

                SmtpClient smtpClient = new("smtp.gmail.com", 587) // SMTP сервер и порт (587 для TLS, 465 для SSL)
                {
                    Credentials = new NetworkCredential(sender, password),
                    EnableSsl = true
                }; 
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}

﻿using System.Net;
using System.Net.Mail;

namespace CRMEngSystem.Services.Mail
{
    public static class SendMailService
    {
        public static bool Send(string subject, string body, string sender, string receiver, string extrareceiver)
        {
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

                SmtpClient smtpClient = new("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(sender, Environment.GetEnvironmentVariable("EMAIL_PASSWORD")),
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

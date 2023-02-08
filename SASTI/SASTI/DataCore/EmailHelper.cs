using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SASTI.DataCore
{
    public class EmailHelper
    {
        public static void sendEmail(ref MailMessage mailMessage)
        {
            //The SmtpClient gets configuration from Web.Config
            SmtpClient smtp = new SmtpClient();
            smtp.Send(mailMessage);
        }
        protected void sendEmail(string subject,string body, string address)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.To.Add(address);
            EmailHelper.sendEmail(ref mailMessage);

            
        }
    }
}
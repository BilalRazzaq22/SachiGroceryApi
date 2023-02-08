using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace SASTI.Common
{
    public static class EmailHelper
    {
        public static string SendEmail(string To, string Subject, string BodyText)
        {
            if (IsValidEmail(To))
            {
                MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["ADMIN_EMAIL"].ToString(), To, Subject, BodyText);
                MailAddress Reply = new MailAddress(ConfigurationManager.AppSettings["ADMIN_EMAIL"].ToString(), "relay", System.Text.Encoding.UTF8);
                mailMessage.ReplyTo = Reply;


                SmtpClient smtp = new SmtpClient();
                try
                {
                    mailMessage.IsBodyHtml = true;
                    smtp.Send(mailMessage);

                    return "Success";
                }
                catch (Exception ex)
                {
                    return "Error";
                }
                finally
                {
                    mailMessage.Dispose();
                    smtp = null;
                }
            }
            else
            {
                return "Invalid EmailAddress";
            }

        }

        public static async Task<string> SendEmailAsync(string To, string Subject, string BodyText)
        {
            return await Task.Factory.StartNew(() =>
            {
                if (IsValidEmail(To))
                {
                    MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["ADMIN_EMAIL"].ToString(), To, Subject, BodyText);
                    MailAddress Reply = new MailAddress(ConfigurationManager.AppSettings["ADMIN_EMAIL"].ToString(), "relay", System.Text.Encoding.UTF8);
                    mailMessage.ReplyTo = Reply;


                    SmtpClient smtp = new SmtpClient();
                    try
                    {

                        mailMessage.IsBodyHtml = true;

                        smtp.Send(mailMessage);

                        return "Success";
                    }
                    catch (Exception ex)
                    {
                        return "Error";
                    }
                    finally
                    {
                        mailMessage.Dispose();
                        smtp = null;
                    }
                }
                else
                {
                    return "Invalid EmailAddress";
                }
            });

        }

        public static bool IsValidEmail(string emailAddress)
        {
            string patternStrict = @"^(([^<>()[\]\\.,;:\s@\""]+"
                  + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                  + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                  + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                  + @"[a-zA-Z]{2,}))$";
            Regex reStrict = new Regex(patternStrict);
            bool isStrictMatch = reStrict.IsMatch(emailAddress);
            return isStrictMatch;
        }
    }

    public static class StandardEmail
    {
        public static void ForgetPassword(string _ToUserName, string _Password, string _ToEmail)
        {
            string html = "<p>Hi " + _ToUserName + ",</p>";
            html += "<p><br><br>Thanks for using " + WebConfigurationManager.AppSettings["AppName"] + "!</p>";
            html += "<p><br>Your password is: " + _Password + "</p>";
            html += "<p><br>If you would like to change your password, please go to the profile area of the app.</p>";
            html += "<p><br>Thanks,</p>";
            html += "<p><br><strong>The " + WebConfigurationManager.AppSettings["AppName"] + " Team</strong></p>";

            EmailHelper.SendEmailAsync(_ToEmail, WebConfigurationManager.AppSettings["AppName"] + " : Forgot Password", html);

        }

        public static void PasswordUpdate(string _ToUserName, string _Password, string _ToEmail)
        {

            string html = "<p>Hi " + _ToUserName + ",</p>";
            html += "<p><br>Your password has been changed successfully.</p>";
            html += "<p><br>Thanks,</p>";
            html += "<p><br><strong>The " + WebConfigurationManager.AppSettings["AppName"] + " Team</strong></p>";

            EmailHelper.SendEmailAsync(_ToEmail, WebConfigurationManager.AppSettings["AppName"] + " : Password Changed", html);

        }
    }
}

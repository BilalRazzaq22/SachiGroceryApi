using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using SASTI.Controllers;
namespace SASTI.DataAccess
{
    public class SMSManager
    {
        #region Generate SMS
        BusinessCoreController controller = new BusinessCoreController();
        public int GenerateSMSAlert(string toNumber, string MessageText)
        {
            string MyUsername = "923183183341";
            string MyPassword = "Zong@123";
            string Masking = "SachiChakki";
            int count = 0;
            if (toNumber.Length == 12 && toNumber.Substring(0, 2).Equals("92"))
            {
                count++;
               var res= SendSMS(Masking, toNumber, MessageText, MyUsername, MyPassword);
            }
            return count;
        }
        #endregion

        #region Send SMS

        public static string SendSMS(string Masking, string toNumber, string MessageText, string MyUsername, string MyPassword)
        {
            //string URI = @"http://api.bizsms.pk/api-send-branded-sms.aspx?username=" + MyUsername + "&pass=" + MyPassword +
            //    "&text=" + MessageText + "&masking=" + Masking + "&destinationnum=" + toNumber + "&language=English";

            //WebRequest req = WebRequest.Create(URI);
            //WebResponse resp = req.GetResponse();
            //var sr = new System.IO.StreamReader(resp.GetResponseStream());
            //return sr.ReadToEnd().Trim();

            SMSApiService.QuickSMSResquest quickSMSResquest = new SMSApiService.QuickSMSResquest()
            {
                loginId = MyUsername,
                loginPassword = MyPassword,
                Destination = toNumber,
                Mask = Masking,
                Message = MessageText,
                ShortCodePrefered = "n",
                UniCode = "0"
            };

            SMSApiService.BasicHttpBinding_ICorporateCBS client = new SMSApiService.BasicHttpBinding_ICorporateCBS();
            string message = client.QuickSMS(quickSMSResquest);
            return message;
        }
        #endregion


    }
}
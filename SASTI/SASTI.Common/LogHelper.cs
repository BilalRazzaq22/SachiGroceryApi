using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.Common
{
    public static class LogHelper
    {
        public static void CreateLog(Exception ex)
        {
            string filePath = ConfigurationManager.AppSettings["RootPath"] + "ExceptionLog.txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Message :" + ex.Message + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "Inner Exception :" + ex.InnerException);
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
            }
            catch (Exception e)
            {
                string es = e.Message;
            }
            
        }
        public static void CreateLog(string log)
        {
            string filePath = ConfigurationManager.AppSettings["RootPath"] + "ExceptionLog.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine("Log :" + log + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
            }
            catch (Exception e)
            {
                
               string es = e.Message;
            }
               
            
        }
    }
}

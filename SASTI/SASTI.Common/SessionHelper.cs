using SASTI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SASTI.Common
{
    public sealed class SessionHelper
    {
        //private  _UserProfile = null;
        //public AdminUser UserProfile
        //{
        //    get { return _UserProfile; }
        //    set { _UserProfile = value; }
        //}

        //private static SessionHelper _Instance;
        //private SessionHelper() { }
        //public static SessionHelper Instance
        //{
        //    get
        //    {
        //        _Instance = new SessionHelper();
        //        if (HttpContext.Current.Session["__SASTISession__"] == null)
        //        {
        //            HttpContext.Current.Session["__SASTISession__"] = _Instance;
        //        }
        //        else
        //        {
        //            _Instance.UserProfile = HttpContext.Current.Session["__SASTISession__"] as AdminUser;
        //        }
        //        return _Instance;
        //    }
        //}
    }
}

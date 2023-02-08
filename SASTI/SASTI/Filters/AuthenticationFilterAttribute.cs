using SASTI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SASTI.Filters
{
    public class AuthenticationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //if (SessionHelper.Instance.UserProfile == null)
            //{
            //    context.HttpContext.Response.Redirect("~/admin/accounts/login");
            //}
            base.OnActionExecuting(context);
        }
    }
}
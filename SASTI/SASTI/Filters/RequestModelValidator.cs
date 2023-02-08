using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net;
using System.Net.Http.Headers;
using SASTI.Common;

namespace SASTI.Filters
{
    /// <summary>
    /// Request Model Validator class
    /// </summary>
    public class RequestModelValidator : ActionFilterAttribute
    {
        /// <summary>
        /// Occured before the action method is invoked
        /// </summary>
        /// <param name="actionContext">HttpActionContext value</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext != null)
            {
                // Validate ModelState
                if (!actionContext.ModelState.IsValid)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.OK, JsonResponse.GetResponseModel(Enums.ResponseCode.Failure, actionContext.ModelState, ""),
                    new MediaTypeHeaderValue("text/json"));
                    //actionContext.Response = actionContext.Request.CreateResponse(
                    //HttpStatusCode.OK, JsonResponse.GetResponse(Enums.ResponseCode.Failure, actionContext.ModelState.Values.FirstOrDefault().Errors[0].Exception.Message));
                }
            }
        }
    }
}
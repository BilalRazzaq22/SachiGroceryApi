
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SASTI.Controllers.Api
{
    public class NotificationController : ApiController
    {

        //[Route("api/GetNotificationCount/{UserId}")]
        //[HttpGet]
        //public ApiResponse GetNotificationCount(int UserId)
        //{
        //    try
        //    {
        //        var obj = new NotificationLogic();
        //        ApiResponse response = new ApiResponse();
        //        response.Response = new Dictionary<string, object>();
        //        response.Response.Add("NotificationCount", Numerics.GetInt(obj.GetNotificationCount(UserId)));


        //        return JsonResponse.GetResponse(Enums.ResponseCode.Success, response);
        //    }
        //    catch (Exception ex)
        //    {

        //        return JsonResponse.GetResponse(Enums.ResponseCode.Exception, Enums.ResponseCode.Exception);

        //    }

        //}
        //[Route("api/UpdateNotificationSetting")]
        //[HttpPost]
        //public ApiResponse UpdateNotificationSetting(InputNotifySetting input)
        //{
        //    try
        //    {
        //        var obj = new NotificationLogic();
        //        var response = obj.UpdateNotificationSetting(input);
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {

        //        return JsonResponse.GetResponse(Enums.ResponseCode.Exception, Enums.ResponseCode.Exception);

        //    }

        //}

        //[Route("api/GetNotifications/{UserId}/{PageNumber}")]
        //[HttpGet]
        //public ApiResponse GetNotifications(int UserId,int PageNumber)
        //{
        //    try
        //    {
        //        var obj = new NotificationLogic();
        //        var response= obj.GetNotifications(UserId,PageNumber,10,"UserNotificationId","desc");
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {

        //        return JsonResponse.GetResponse(Enums.ResponseCode.Exception, Enums.ResponseCode.Exception);

        //    }

        //}
    }
}

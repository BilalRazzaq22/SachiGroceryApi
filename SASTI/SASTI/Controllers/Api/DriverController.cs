using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using Anytime.DataAccess;
//using Anytime.DataCore;
using System.Data;
using SASTI.DataCore;
using SASTI.DataAccess;
using SASTI.Models;
using SASTI.Common;
using SASTI.Models.Dto;

namespace SASTI.Controllers
{
    public class DriverController : ApiController
    {
        BusinessCoreController controller = new BusinessCoreController();

        [HttpPost]
        [Route("api/markDelivered")]
        public ApiResponse markDelivered(ORDER o)
        {
            DataSet ds = controller.getOrdersByOrderId(o.ORDER_ID);

            string customerId = ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
            string branchId = ds.Tables[0].Rows[0]["BRANCH_ID"].ToString();
            string riderId = ds.Tables[0].Rows[0]["RIDER_ID"].ToString();

            DataSet customer_fcm_token = (!string.IsNullOrEmpty(customerId) && customerId != "0") ? controller.getUserFCMToken(customerId) : null;
            DataSet managers_fcm_token = (!string.IsNullOrEmpty(branchId)) ? controller.managerFCMToken(branchId) : null;
            DataSet rider_fcm_token = (!string.IsNullOrEmpty(riderId)) ? controller.getUserFCMToken(riderId) : null;
            string user_mobile = controller.getUserMobile(customerId);
            SMSManager smsmanager = new SMSManager();
            int result = controller.markDelivered(o.ORDER_ID);
            if (result > 0)
            {
                FCMPushNotification notification = new FCMPushNotification();

                if (customer_fcm_token != null)
                {
                    foreach (DataRow r in customer_fcm_token.Tables[0].Rows)
                    {
                        notification.SendNotification("Chaarsu", "Your order " + o.ORDER_ID + " is Delivered!!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                    }
                }

                if (rider_fcm_token != null)
                {
                    foreach (DataRow r in rider_fcm_token.Tables[0].Rows)
                    {
                        notification.SendNotification("Chaarsu", "Order " + o.ORDER_ID + " is Delivered!!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                    }
                }
                //notification.SendNotification("Chaarsu", "Your order " + o.ORDER_ID + " is Delivered!!! ", "news", "477625648329", customer_fcm_token);
                //notification.SendNotification("Chaarsu", "Order " + o.ORDER_ID + " is Delivered!!! ", "news", "477625648329", rider_fcm_token);
                smsmanager.GenerateSMSAlert(user_mobile, "Your Chaarsu order is delivered. It has been our absolute pleasure to serve you today!");

                if (managers_fcm_token != null)
                {
                    foreach (DataRow r in managers_fcm_token.Tables[0].Rows)
                    {
                        notification.SendNotification("Chaarsu", "Order " + o.ORDER_ID + " is Delivered!!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                    }
                }

                return JsonResponse.GetResponse(Enums.ResponseCode.Success, true);
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
        [HttpPost]
        [Route("api/markBounced")]
        public ApiResponse markBounced(ORDER o)
        {
            DataSet ds = controller.getOrdersByOrderId(o.ORDER_ID);
            DataSet customer_fcm_token = controller.getUserFCMToken(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
            DataSet managers_fcm_token = controller.managerFCMToken(ds.Tables[0].Rows[0]["BRANCH_ID"].ToString());
            DataSet rider_fcm_token = controller.getUserFCMToken(ds.Tables[0].Rows[0]["RIDER_ID"].ToString());

            int result = controller.markBounced(o.ORDER_ID, o.REJECTED_REASON);
            if (result > 0)
            {
                FCMPushNotification notification = new FCMPushNotification();
                foreach (DataRow r in customer_fcm_token.Tables[0].Rows)
                {
                    notification.SendNotification("Chaarsu", "Your order " + o.ORDER_ID + " is Bounced!!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                }

                foreach (DataRow r in rider_fcm_token.Tables[0].Rows)
                {
                    notification.SendNotification("Chaarsu", "Order " + o.ORDER_ID + " is Bounced!!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                }
                //notification.SendNotification("Chaarsu", "Your order " + o.ORDER_ID + " is Bounced!!! ", "news", "477625648329", customer_fcm_token);
                //notification.SendNotification("Chaarsu", "Order " + o.ORDER_ID + " is Bounced!!! ", "news", "477625648329", rider_fcm_token);

                foreach (DataRow r in managers_fcm_token.Tables[0].Rows)
                {
                    notification.SendNotification("Chaarsu", "Order " + o.ORDER_ID + " is Bounced!!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                }
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, true);
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Failure, Enums.ResponseCode.Failure);
            }
        }
        [HttpPost]
        [Route("api/rejectByDriver")]
        public ApiResponse rejectByDriver(ORDER o)
        {
            DataSet ds = controller.getOrdersByOrderId(o.ORDER_ID);
            DataSet customer_fcm_token = controller.getUserFCMToken(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
            DataSet managers_fcm_token = controller.managerFCMToken(ds.Tables[0].Rows[0]["BRANCH_ID"].ToString());
            DataSet rider_fcm_token = controller.getUserFCMToken(ds.Tables[0].Rows[0]["RIDER_ID"].ToString());

            int result = controller.rejectByDriver(o.ORDER_ID, o.REJECTED_REASON);
            if (result > 0)
            {
                FCMPushNotification notification = new FCMPushNotification();
                foreach (DataRow r in customer_fcm_token.Tables[0].Rows)
                {
                    notification.SendNotification("Chaarsu", "Your order " + o.ORDER_ID + " is Rejected By the Driver!!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                }

                foreach (DataRow r in rider_fcm_token.Tables[0].Rows)
                {
                    notification.SendNotification("Chaarsu", "Order " + o.ORDER_ID + " is Rejected!!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                }

                //notification.SendNotification("Chaarsu", "Your order " + o.ORDER_ID + " is Rejected By the Driver!!! ", "news", "477625648329", customer_fcm_token);
                //notification.SendNotification("Chaarsu", "Order " + o.ORDER_ID + " is Rejected!!! ", "news", "477625648329", rider_fcm_token);

                foreach (DataRow r in managers_fcm_token.Tables[0].Rows)
                {
                    notification.SendNotification("Chaarsu", "Order " + o.ORDER_ID + " is Rejected By the Driver!!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                }
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, true);
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Failure, Enums.ResponseCode.Failure);
            }
        }
        [HttpGet]
        [Route("api/getPreviousRides")]
        public ApiResponse getPreviousRides(int userId)
        {
            DataSet orders = controller.getPreviousRides(userId);
            orders.Tables[0].TableName = "RIDES";
            if (orders.Tables[0].Rows.Count > 0)
            {
                //string json = JsonConvert.SerializeObject(groups, Formatting.Indented);
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, orders));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
        [HttpGet]
        [Route("api/getAllBranchRiders")]
        public ApiResponse getAllBranchRiders(int branchId)
        {
            DataSet users = controller.getAllBranchRiders(branchId);
            users.Tables[0].TableName = "USERS";
            if (users.Tables[0].Rows.Count > 0)
            {
                //string json = JsonConvert.SerializeObject(groups, Formatting.Indented);
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, users));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
        [HttpGet]
        [Route("api/getAllRiders")]
        public ApiResponse getAllRiders()
        {
            DataSet users = controller.getAllRiders();
            users.Tables[0].TableName = "USERS";
            if (users.Tables[0].Rows.Count > 0)
            {
                //string json = JsonConvert.SerializeObject(groups, Formatting.Indented);
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, users));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
        [HttpGet]
        [Route("api/getAvailableBranchRiders")]
        public ApiResponse getAvailableBranchRiders(int branchId)
        {
            DataSet users = controller.getAvailableBranchRiders(branchId);
            users.Tables[0].TableName = "USERS";
            if (users.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, users));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
        [HttpGet]
        [Route("api/getAvailableRiders")]
        public ApiResponse getAvailableRiders()
        {
            DataSet users = controller.getAvailableRiders();
            users.Tables[0].TableName = "USERS";
            if (users.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, users));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
        [HttpPost]
        [Route("api/getRides")]
        public HttpResponseMessage getRides(HttpRequestMessage request, USER u)
        {
            DataSetDto dataSetDto = new DataSetDto();
            DataSet orders = controller.getRides(u.USER_ID);
            //orders.Tables[0].TableName = "ORDERS";
            if (orders.Tables[0].Rows.Count > 0)
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = orders;
                return request.CreateResponse(HttpStatusCode.OK, dataSetDto);
                //return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, orders));
            }
            else
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.NotFound;
                dataSetDto.Response.Message = "Record Not Found";
                dataSetDto.Response.Data = null;
                return request.CreateResponse(HttpStatusCode.NotFound, dataSetDto);
                //return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
        [HttpGet]
        [Route("api/searchByRiderName")]
        public ApiResponse searchByRiderName(string name)
        {
            DataSet riders = controller.searchByRiderName(name);
            if (riders.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, riders));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Newtonsoft.Json;
using SASTI.Models;
using SASTI.Controllers;
using SASTI.DataCore;
using SASTI.DataAccess;
using SASTI.Common;
using SASTI.Models.Dto;
using SASTI.Models.Models.Response;

namespace SASTI.Controllers
{
    public class OrderController : ApiController
    {
        BusinessCoreController controller = new BusinessCoreController();

        [Route("api/Order/SaveOrder")]
        [HttpPost]
        public ApiResponse SaveOrder(ORDER order)
        {
            try
            {
                order.CREATED_ON = DateTime.Now;
                order.STATUS = 1;
                order.MANUAL_DISCOUNT = 0;
                order.IS_ACTIVE = 1;
                if (order.CREATED_BY == null)
                    order.CREATED_BY = 0;

                if (order.COUPON_ID == null)
                {
                    order.COUPON_ID = -1;
                    order.COUPON_DISCOUNT = 0;

                }
                //order.BRANCH_ID = 1;//remove this when added To frontend
                //coupon work
                //deliverytime
                int result = controller.saveOrder(order);
                if (result > 0)
                {
                    DataSet user_fcm_token = controller.getUserFCMToken(order.CUSTOMER_ID.ToString());
                    DataSet managers_fcm_token = controller.managerFCMToken(order.BRANCH_ID.ToString());


                    string user_mobile = controller.getUserMobile(order.CUSTOMER_ID.ToString());
                    SMSManager smsmanager = new SMSManager();
                    smsmanager.GenerateSMSAlert(user_mobile, "Your Order ( " + order.ORDER_ID + " ) is placed Successfully !!!");

                    FCMPushNotification notification = new FCMPushNotification();
                    //notification.SendNotification("Chaarsu", "Your Order has been placed", "news", "477625648329", user_fcm_token);

                    foreach (DataRow r in managers_fcm_token.Tables[0].Rows)
                    {
                        notification.SendNotification("Chaarsu", "New Order!!!", "news", "477625648329", r["FCM_TOKEN"].ToString());
                    }

                    //EmailHelper.sendEmail("", "", "");

                    return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, result));
                }
                else
                    return JsonResponse.GetResponse(Enums.ResponseCode.Failure, result);
            }
            catch (Exception ex)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.InnerException + ex.Message);
            }

        }

        [Route("api/Order/SaveOrderProducts")]
        [HttpPost]
        public ApiResponse SaveOrderProducts(List<ORDER_PRODUCTS> orderProduct)
        {
            int orderCount = 0;
            foreach (ORDER_PRODUCTS product in orderProduct)
            {
                product.IS_ACTIVE = 1;
                int result = controller.saveOrderProducts(product);
                if (result > 0)
                    orderCount++;
            }
            if (orderCount == orderProduct.Count)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, true);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, false);
        }
        [Route("api/Order/dummy")]
        public ApiResponse dummy()
        {
            try
            {
                string user_mobile = "923134336333";
                SMSManager smsmanager = new SMSManager();
                smsmanager.GenerateSMSAlert(user_mobile, "Your Order has been placed");
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, "Success.");

            }
            catch (Exception ex)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.InnerException + ex.Message);
            }
        }
        [HttpPost]
        [Route("api/saveOrder2")]
        public ApiResponse saveOrder2(ORDER order)
        {
            try
            {
                DateTime currentdate;
                DateTime date1 = DateTime.UtcNow;
                TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                currentdate = TimeZoneInfo.ConvertTime(date1, tz);


                order.CREATED_ON = currentdate;
                order.STATUS = 1;
                order.MANUAL_DISCOUNT = 0;
                order.IS_ACTIVE = 1;
                if (order.CREATED_BY == null)
                    order.CREATED_BY = 0;

                if (order.COUPON_ID == null)
                {
                    order.COUPON_ID = -1;
                    order.COUPON_DISCOUNT = 0;

                }
                if (order.ADDED_BY == null)
                {
                    order.ADDED_BY = 2;//1 for admin, 2 for customer
                }
                //order.BRANCH_ID = 1;//remove this when added To frontend
                //coupon work
                //deliverytime
                DataSet orders = controller.saveOrder2(order);
                orders.Tables[0].TableName = "ORDERS";
                if (orders.Tables[0].Rows.Count > 0)
                {
                    string user_mobile = controller.getUserMobile(order.CUSTOMER_ID.ToString());
                    SMSManager smsmanager = new SMSManager();
                    smsmanager.GenerateSMSAlert(user_mobile, "Your Order has been placed" + order.ORDER_ID);

                    DataSet user_fcm_token = controller.getUserFCMToken(order.CUSTOMER_ID.ToString());
                    DataSet managers_fcm_token = controller.managerFCMToken(order.BRANCH_ID.ToString());


                    FCMPushNotification notification = new FCMPushNotification();
                    foreach (DataRow r in user_fcm_token.Tables[0].Rows)
                    {
                        notification.SendNotification("Chaarsu", "Your Order has been placed" + order.ORDER_ID, "news", "477625648329", r["FCM_TOKEN"].ToString());
                    }
                    //notification.SendNotification("Chaarsu", "Your Order has been placed" + order.ORDER_ID, "news", "477625648329", user_fcm_token);

                    foreach (DataRow r in managers_fcm_token.Tables[0].Rows)
                    {
                        notification.SendNotification("Chaarsu", "New Order!!!" + order.ORDER_ID, "news", "477625648329", r["FCM_TOKEN"].ToString());
                    }


                    return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, orders));
                }
                else
                    return JsonResponse.GetResponse(Enums.ResponseCode.Failure, orders);
            }
            catch (Exception ex)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.InnerException + ex.Message);
            }

        }
        [HttpPost]
        [Route("api/saveOrderProducts2")]
        public ApiResponse saveOrderProducts2(ORDER_PRODUCTS orderProduct)
        {
            DataSet orderProducts = controller.saveOrderProducts2(orderProduct);
            orderProducts.Tables[0].TableName = "ORDER_PRODUCTS";
            if (orderProducts.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, orderProducts);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, orderProducts);
        }
        [HttpPost]
        [Route("api/saveOrderWithProducts")]
        public ApiResponse saveOrderWithProducts(SaveOrderResponse saveOrderResponse)
        {
            OrderProductsDto orderProducts = controller.saveOrderWithProducts(saveOrderResponse.order, saveOrderResponse.orderProduct);
            return JsonResponse.GetResponse(Enums.ResponseCode.Success, orderProducts);
            //orderProducts.Tables[0].TableName = "ORDER_WITH_PRODUCTS";
            ////var empList = orderProducts.Tables[0].AsEnumerable();
            //if (orderProducts.Tables[0].Rows.Count > 0)
            //    return JsonResponse.GetResponse(Enums.ResponseCode.Success, orderProducts);
            //else
            //    return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, orderProducts);
        }
        [HttpPost]
        [Route("api/getCustomerPreviousOrders")]
        public ApiResponse getCustomerPreviousOrders(ORDER o)
        {
            //int cust_id = o.CUSTOMER_ID;
            DataSet orders = controller.getCustomerPreviousOrders(o.CUSTOMER_ID == null ? 0 : o.CUSTOMER_ID.Value, o.CREATED_BY == null ? 0 : o.CREATED_BY.Value);
            orders.Tables[0].TableName = "ORDERS";
            if (orders.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, orders);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, orders);
        }
        [HttpPost]
        [Route("api/getCustomerPreviousCompletedOrders")]
        public ApiResponse getCustomerPreviousCompletedOrders(ORDER o)
        {
            //int cust_id = o.CUSTOMER_ID;
            DataSet orders = controller.getCustomerPreviousCompletedOrders(o.CUSTOMER_ID == null ? 0 : o.CUSTOMER_ID.Value, o.CREATED_BY == null ? 0 : o.CREATED_BY.Value);
            orders.Tables[0].TableName = "ORDERS";
            if (orders.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, orders);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, orders);
        }

        [HttpPost]
        [Route("api/getAllCustomerOrders")]
        public ApiResponse getAllCustomerOrders(ORDER o)
        {
            //int cust_id = o.CUSTOMER_ID;
            DataSet orders = controller.getAllCustomerOrders(o.CUSTOMER_ID == null ? 0 : o.CUSTOMER_ID.Value, o.CREATED_BY == null ? 0 : o.CREATED_BY.Value);
            //orders.Tables[0].TableName = "ORDERS";
            if (orders.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, orders);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, orders);
        }

        [HttpGet]
        [Route("api/getCustomerOrderProducts")]
        public HttpResponseMessage getCustomerOrderProducts(HttpRequestMessage request, int orderId)
        {

            DataSetDto dataSetDto = controller.getCustomerProductsByOrderId(orderId);
            if (dataSetDto != null)
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = dataSetDto.Response.Data;
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

        //[AllowAnonymous]
        [HttpPost]
        [Route("api/getOrderProducts")]
        public ApiResponse getOrderProducts(ORDER o)
        {
            DataSet orderProducts = controller.getOrderProducts(o.ORDER_ID);
            orderProducts.Tables[0].TableName = "ORDER_PRODUCTS";
            if (orderProducts.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, orderProducts);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, orderProducts);
        }
        [HttpPost]
        [Route("api/getOrderProductsByStatus")]
        public ApiResponse getOrderProductsByStatus(ORDER o)
        {
            DataSet orderProducts = controller.getOrderProductsByStatus(o.STATUS);
            orderProducts.Tables[0].TableName = "ORDER_PRODUCTS";
            if (orderProducts.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, orderProducts);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, orderProducts);
        }
        [HttpPost]
        [Route("api/assignRider")]
        public ApiResponse assignRider([FromBody] UserOrders o)
        {
            int result = controller.assignRider(o.RIDER_ID, o.USER_ID, o.ORDER_ID, o.PICKUP_TIME);
            if (result > 0)
            {
                DataSet user_fcm_token = controller.getUserFCMToken(o.RIDER_ID.ToString());


                FCMPushNotification notification = new FCMPushNotification();
                foreach (DataRow r in user_fcm_token.Tables[0].Rows)
                {
                    notification.SendNotification("Chaarsu", "You got a new ride !!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                }
                //notification.SendNotification("Chaarsu", "You got a new ride !!! ", "news", "477625648329", user_fcm_token);
                if (o.BRANCH_ID != null)
                {
                    DataSet managers_fcm_token = controller.managerFCMToken(o.BRANCH_ID.ToString());
                    foreach (DataRow r in managers_fcm_token.Tables[0].Rows)
                    {
                        notification.SendNotification("Chaarsu", "Order is dispatched (Rider id : " + o.RIDER_ID + ") ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                    }
                }

                return JsonResponse.GetResponse(Enums.ResponseCode.Success, true);
            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, false);
        }
        [HttpPost]
        [Route("api/updateOrderStatus")]
        public ApiResponse updateOrderStatus(UserOrders o)
        {
            DataSet ds = controller.getOrdersByOrderId(o.ORDER_ID);
            string customerId = ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString();
            string branchId = ds.Tables[0].Rows[0]["BRANCH_ID"].ToString();
            string mobileNo = ds.Tables[0].Rows[0]["MOBILE"].ToString();

            DataSet customer_fcm_token = (!string.IsNullOrEmpty(customerId) && customerId != "0") ? controller.getUserFCMToken(customerId) : null;
            DataSet managers_fcm_token = (!string.IsNullOrEmpty(branchId)) ? controller.managerFCMToken(branchId) : null;
            string user_mobile = controller.getUserMobile(customerId);

            if (string.IsNullOrEmpty(user_mobile))
            {
                user_mobile = mobileNo;
            }

            SMSManager smsmanager = new SMSManager();

            int result = controller.updateOrderStatus(o.ORDER_ID, o.STATUS, o.USER_ID);
            if (result > 0)
            {

                if (o.STATUS == ApplicationConstants.PENDING_ORDER_STATUS)
                {
                    //customer ko
                    if (!string.IsNullOrEmpty(user_mobile))
                        smsmanager.GenerateSMSAlert(user_mobile, "Your Chaarsu Order " + o.ORDER_ID + " is processing. Download App http://bit.ly/ChaarsuDelivery");

                    FCMPushNotification notification = new FCMPushNotification();
                    if (customer_fcm_token != null)
                    {
                        foreach (DataRow r in customer_fcm_token.Tables[0].Rows)
                        {
                            notification.SendNotification("Chaarsu", "Your order is in process!!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                        }
                    }
                    //notification.SendNotification("Chaarsu", "Your order is in process!!! ", "news", "477625648329", customer_fcm_token);

                    if (managers_fcm_token != null)
                    {
                        foreach (DataRow r in managers_fcm_token.Tables[0].Rows)
                        {
                            notification.SendNotification("Chaarsu", "Order is in Process!!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                        }
                    }
                }
                else if (o.STATUS == ApplicationConstants.ASSIGNED_TO_FLOORMAN_ORDER_STATUS)
                {
                    //customer, maanger
                    if (!string.IsNullOrEmpty(user_mobile))
                        smsmanager.GenerateSMSAlert(user_mobile, "Your Chaarsu order is dispatched!");

                    FCMPushNotification notification = new FCMPushNotification();
                    foreach (DataRow r in customer_fcm_token.Tables[0].Rows)
                    {
                        notification.SendNotification("Chaarsu", "Your order is dispatched. Humaray paisay tyaar rakhien!", "news", "477625648329", r["FCM_TOKEN"].ToString());
                    }
                    //notification.SendNotification("Chaarsu", "Your order is dispatched. Humaray paisay tyaar rakhien!", "news", "477625648329", customer_fcm_token);

                    foreach (DataRow r in managers_fcm_token.Tables[0].Rows)
                    {
                        notification.SendNotification("Chaarsu", "Order is dispatched ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                    }
                }



                return JsonResponse.GetResponse(Enums.ResponseCode.Success, true);
            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, false);
        }
        [HttpPost]
        [Route("api/updateOrderProducts")]
        public ApiResponse updateOrderProducts(ORDER_PRODUCTS orderProducts)
        {
            int result = controller.updateOrderProducts(orderProducts);
            if (result > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, true);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, false);
        }
        [HttpGet]
        [Route("api/getBranchOrders")]
        public ApiResponse getBranchOrders(int branchId)
        {
            DataSet orders = controller.getBranchOrders(branchId);
            orders.Tables[0].TableName = "ORDERS";
            if (orders.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, orders);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, orders);
        }
        [HttpGet]
        [Route("api/getBranchNewOrders")]
        public ApiResponse getBranchNewOrders(int branchId, int index)
        {
            DataSet orders = controller.getBranchNewOrders(branchId, index);
            orders.Tables[0].TableName = "ORDERS";
            if (orders.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, orders);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, orders);
        }

        [HttpGet]
        [Route("api/getBranchProcessOrders")]
        public ApiResponse getBranchProcessOrders(int branchId, int index)
        {
            DataSet orders = controller.getBranchProcessOrders(branchId, index);
            orders.Tables[0].TableName = "ORDERS";
            if (orders.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, orders);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, orders);
        }
        [HttpGet]
        [Route("api/getBranchDispatchedOrders")]
        public ApiResponse getBranchDispatchedOrders(int branchId, int index)
        {
            DataSet orders = controller.getBranchDispatchedOrders(branchId, index);
            orders.Tables[0].TableName = "ORDERS";
            if (orders.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, orders);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, orders); ;
        }


        [HttpPost]
        [Route("api/deleteOrderProducts")]
        public ApiResponse deleteOrderProducts(ORDER_PRODUCTS orderProducts)
        {
            bool result = controller.deleteOrderProducts(orderProducts);
            if (result)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, result);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, result);
        }
        [HttpGet]
        [Route("api/getOrderStats")]
        public ApiResponse getOrderStats(int OrderID)
        {
            DataSet orders = controller.getOrderStats(OrderID);
            orders.Tables[0].TableName = "ORDERS";
            if (orders.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, orders);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, orders);
        }
        [HttpGet]
        [Route("api/getOrdersByOrderId")]
        public ApiResponse getOrdersByOrderId(int orderId)
        {
            DataSet orders = controller.getOrdersByOrderId(orderId);
            return JsonResponse.GetResponse(Enums.ResponseCode.Success, orders);
        }
        [HttpGet]
        [Route("api/getRiderOrders")]
        public ApiResponse getRiderOrders(int orderId, int rider_id)
        {
            DataSet orders = controller.getRiderOrders(orderId, rider_id);
            return JsonResponse.GetResponse(Enums.ResponseCode.Success, orders);
        }

        [HttpGet]
        [Route("api/updateRiderOrderStatus")]
        public HttpResponseMessage updateRiderOrderStatus(HttpRequestMessage request, int riderId, int orderId)
        {
            DataSetDto dataSetDto = new DataSetDto();
            string riderInTime = controller.getRiderOrderStatus(riderId, orderId);
            //orders.Tables[0].TableName = "ORDERS";
            if (!string.IsNullOrEmpty(riderInTime))
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = riderInTime;
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
        [Route("api/updateDiscount")]
        public HttpResponseMessage updateDiscount(HttpRequestMessage request, int disc, int user_id, int orderID)
        {
            DataSetDto dataSetDto = new DataSetDto();
            try
            {

                object totalOrderAmount = controller.GetOrderTotalAmount(orderID);

                if (disc > Convert.ToInt32(totalOrderAmount))
                {
                    dataSetDto.Response.Code = (int)HttpStatusCode.BadRequest;
                    dataSetDto.Response.Message = $"Your total order amount is {totalOrderAmount} and your discount amount {disc} is greater than {totalOrderAmount}";
                    dataSetDto.Response.Data = null;
                    return request.CreateResponse(HttpStatusCode.BadRequest, dataSetDto);
                }

                int result = controller.updateDiscount(disc, user_id, orderID);
                if (result > 0)
                {
                    dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                    dataSetDto.Response.Message = "Success";
                    dataSetDto.Response.Data = result;
                    return request.CreateResponse(HttpStatusCode.OK, dataSetDto);
                }
                else
                {
                    dataSetDto.Response.Code = (int)HttpStatusCode.BadRequest;
                    dataSetDto.Response.Message = "Not Found";
                    dataSetDto.Response.Data = null;
                    return request.CreateResponse(HttpStatusCode.OK, dataSetDto);
                }
            }
            catch (Exception ex)
            {

                dataSetDto.Response.Code = (int)HttpStatusCode.BadRequest;
                dataSetDto.Response.Message = "Not Found";
                dataSetDto.Response.Data = ex.Message;
                return request.CreateResponse(HttpStatusCode.BadRequest, dataSetDto);
            }

        }
        [HttpGet]
        [Route("api/redeemCode")]
        public ApiResponse redeemCode(string code)
        {
            try
            {
                DataSet ds = controller.redeemCode(code);
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, ds);
            }
            catch (Exception ex)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.Message);
            }

        }
        [HttpGet]
        [Route("api/updateCoupon")]
        public ApiResponse updateCoupon(int cId)
        {
            try
            {
                //temp change
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, cId);
                //int result = controller.updateCoupon(cId);
                //if (result > 0)
                //    return Request.CreateResponse(HttpStatusCode.OK, result);
                //else
                //    return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
            catch (Exception ex)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.Message);
            }

        }
        [HttpPost]
        [Route("api/rejectByManager")]
        public ApiResponse rejectByManager([FromBody] ORDER o)
        {
            DataSet ds = controller.getOrdersByOrderId(o.ORDER_ID);
            DataSet customer_fcm_token = controller.getUserFCMToken(ds.Tables[0].Rows[0]["CUSTOMER_ID"].ToString());
            DataSet managers_fcm_token = controller.managerFCMToken(ds.Tables[0].Rows[0]["BRANCH_ID"].ToString());


            int result = controller.rejectByManager(o.ORDER_ID, o.REJECTED_REASON);
            if (result > 0)
            {
                FCMPushNotification notification = new FCMPushNotification();
                foreach (DataRow r in customer_fcm_token.Tables[0].Rows)
                {
                    notification.SendNotification("Chaarsu", "Your order " + o.ORDER_ID + " is Rejected By the Manager!!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                }
                //notification.SendNotification("Chaarsu", "Your order " + o.ORDER_ID + " is Rejected By the Manager!!! ", "news", "477625648329", customer_fcm_token);

                foreach (DataRow r in managers_fcm_token.Tables[0].Rows)
                {
                    notification.SendNotification("Chaarsu", "Order is rejected!!! ", "news", "477625648329", r["FCM_TOKEN"].ToString());
                }




                return JsonResponse.GetResponse(Enums.ResponseCode.Success, true);


            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, false);
        }
    }
}

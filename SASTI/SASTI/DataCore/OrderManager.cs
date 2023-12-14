using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SASTI.DataAccess;
using System.Data;
using SASTI.Models;
using SASTI;
using SASTI.BusinessLayer;
using SASTI.Controllers;
using System.Threading.Tasks;
using SASTI.Models.Dto;

namespace SASTI.DataCore
{
    public class OrderManager : DABase
    {
        SASTIEntities db = new SASTIEntities();
        RiderOrderLogic riderOrderLogic = new RiderOrderLogic();
        const string QRY_GET_ORDER_BY_ORDER_ID = "SELECT * FROM ORDERS WHERE ORDER_ID = {0}";

        const string QRY_GET_PRODUCT_BY_ID = "select * from PRODUCTS where PRODUCT_ID = {0}";

        const string QRY_GET_CUSTOMER_PREVIOUS_ORDERS = "select O.*,B.ADDRESS from ORDERS O inner join BRANCHES B on O.BRANCH_ID = B.BRANCH_ID where O.CUSTOMER_ID = {0} and O.STATUS IN (1,2,3) order by CREATED_ON desc OFFSET {1} ROWS FETCH NEXT 100 ROWS ONLY";

        const string QRY_GET_CUSTOMER_PREVIOUS_COMPLETED_ORDERS = "select O.*,B.ADDRESS from ORDERS O inner join BRANCHES B on O.BRANCH_ID = B.BRANCH_ID where O.CUSTOMER_ID = {0} and O.STATUS IN (4,5,6,7) order by CREATED_ON desc OFFSET {1} ROWS FETCH NEXT 100 ROWS ONLY";

        const string QRY_GET_ALL_CUSTOMER_ORDERS = @"SELECT O.ORDER_ID,O.CUSTOMER_ID,O.NAME,O.MOBILE,O.ADDRESS,O.STATUS,O.CREATED_ON,O.CREATED_BY,O.ASSIGNED_ON,O.ASSIGNED_BY,
                                                    O.SENT_TO_RIDER_ON,O.SENT_TO_RIDER_BY,O.UPDATED_ON,O.UPDATED_BY,O.RIDER_ID,O.REJECTED_BY,O.REJECTED_REASON,O.DELIVERY_TIME,
                                                    O.IS_ACTIVE,O.BRANCH_ID,O.DELIVERY_DESCRIPTION,O.PAYMENT_MODE_ID,O.MANUAL_DISCOUNT,O.COUPON_ID,O.COUPON_DISCOUNT,O.PICKUP_TIME,O.IS_PACKAGE,O.ADDED_BY,
                                                    BR.ADDRESS,(SUM(B.UNIT_PRICE * OP.QUANTITY) - O.MANUAL_DISCOUNT + O.DeliveryFee) AS TOTAL_PRICE,O.DeliveryFee,COUNT(OP.ORDER_ID) AS TOTAL_ITEMS FROM ORDERS O
                                                    INNER JOIN BRANCHES BR on O.BRANCH_ID = BR.BRANCH_ID
                                                    INNER JOIN ORDER_PRODUCTS OP ON OP.ORDER_ID = O.ORDER_ID
                                                    INNER JOIN BARCODES B ON B.BAR_CODE = OP.BAR_CODE
                                                    INNER JOIN PRODUCTS P ON B.ITEM_CODE = P.OLD_PRODUCT_ID
                                                    WHERE O.CUSTOMER_ID = {0} AND B.bDEFAULT = 1 AND B.IsActive = 1 AND B.LOCNO = O.BRANCH_ID AND OP.IS_ACTIVE = 1 AND O.STATUS IN (1,2,3,4,5,6,7)
                                                    GROUP BY O.ORDER_ID,O.CUSTOMER_ID,O.NAME,O.MOBILE,O.ADDRESS,O.STATUS,O.CREATED_ON,O.CREATED_BY,O.ASSIGNED_ON,O.ASSIGNED_BY,
                                                    O.SENT_TO_RIDER_ON,O.SENT_TO_RIDER_BY,O.UPDATED_ON,O.UPDATED_BY,O.RIDER_ID,O.REJECTED_BY,O.REJECTED_REASON,O.DELIVERY_TIME,
                                                    O.IS_ACTIVE,O.BRANCH_ID,O.DELIVERY_DESCRIPTION,O.PAYMENT_MODE_ID,O.MANUAL_DISCOUNT,O.COUPON_ID,O.COUPON_DISCOUNT,O.PICKUP_TIME,O.IS_PACKAGE,O.ADDED_BY,BR.ADDRESS,O.DeliveryFee
                                                    order by CREATED_ON desc OFFSET {1} ROWS FETCH NEXT 100 ROWS ONLY";

        const string QRY_GET_ORDER_DETAILS = @"SELECT O.RIDER_ID,CASE WHEN (O.DeliveryFee IS NULL) THEN 0.0 ELSE O.DeliveryFee END AS DeliveryFee,OP.*,P.*,b.*,(select TOP 1 CHAARSU_IMAGE_PATH from PRODUCT_IMAGES where PRODUCT_ID = P.PRODUCT_ID) AS CHAARSU_IMAGE_PATH,
                                             (select TOP 1 CHAARSU_THUMBNAIL_PATH from PRODUCT_IMAGES where PRODUCT_ID = P.PRODUCT_ID) AS CHAARSU_THUMBNAIL_PATH FROM ORDER_PRODUCTS OP 
                                             INNER JOIN ORDERS O ON OP.ORDER_ID = O.ORDER_ID
                                             inner join BARCODES b on OP.BAR_CODE = B.BAR_CODE
                                             INNER JOIN PRODUCTS P on b.ITEM_CODE = P.OLD_PRODUCT_ID
                                             where OP.ORDER_ID = {0} and B.bDefault =1 and  B.IsActive =1 and b.LOCNO = o.BRANCH_ID and  OP.IS_ACTIVE =1 ";
        //@"SELECT OP.*,P.*,b.*,(select TOP 1 CHAARSU_IMAGE_PATH from PRODUCT_IMAGES where PRODUCT_ID = P.PRODUCT_ID) AS CHAARSU_IMAGE_PATH,
        //(select TOP 1 CHAARSU_THUMBNAIL_PATH from PRODUCT_IMAGES where PRODUCT_ID = P.PRODUCT_ID) AS CHAARSU_THUMBNAIL_PATH FROM ORDER_PRODUCTS OP 
        //                                            INNER JOIN PRODUCTS P on op.PRODUCT_ID = P.OLD_PRODUCT_ID
        //                                            inner join BARCODES b on OP.PRODUCT_ID = B.ITEM_CODE
        //                                            
        //                                            INNER JOIN ORDERS O ON OP.ORDER_ID = O.ORDER_ID
        //                                            where OP.ORDER_ID = {0} and B.bDefault =1 and  B.IsActive =1 and b.LOCNO = o.BRANCH_ID and  OP.IS_ACTIVE =1 ";

        const string QRY_GET_ORDER_BY_STATUS = "select * from ORDERS where STATUS = {0} order by CREATED_ON desc";

        const string QRY_ASSIGN_RIDER = "UPDATE ORDERS SET RIDER_ID = {0} , SENT_TO_RIDER_ON = '{1}', SENT_TO_RIDER_BY = {2},PICKUP_TIME = '{4}' where ORDER_ID = {3}";

        public const string QRY_MANAGER_REJECT = "Update ORDERS SET STATUS = 6, REJECTED_REASON = '{1}'where ORDER_ID = {0}";

        public const string QRY_BRANCH_ORDERS = "select O.*,B.ADDRESS from ORDERS O INNER JOIN BRANCHES B ON O.BRANCH_ID = B.BRANCH_ID where O.BRANCH_ID = {0} ORDER BY O.ORDER_ID DESC";

        public const string QRY_BRANCH_NEW_ORDERS = "select O.*,B.ADDRESS from ORDERS O INNER JOIN BRANCHES B ON O.BRANCH_ID = B.BRANCH_ID where O.BRANCH_ID = {0} AND O.STATUS =1  ORDER BY O.ORDER_ID DESC OFFSET {1} ROWS FETCH NEXT 100 ROWS ONLY";

        public const string QRY_BRANCH_PROCESS_ORDERS = "select O.*,B.ADDRESS from ORDERS O INNER JOIN BRANCHES B ON O.BRANCH_ID = B.BRANCH_ID where O.BRANCH_ID = {0} AND O.STATUS IN (2,3)  ORDER BY O.ORDER_ID DESC OFFSET {1} ROWS FETCH NEXT 100 ROWS ONLY";

        public const string QRY_BRANCH_DISPATCHED_ORDERS = "select O.*,B.ADDRESS from ORDERS O INNER JOIN BRANCHES B ON O.BRANCH_ID = B.BRANCH_ID where O.BRANCH_ID = {0} AND O.STATUS IN (4,5,6,7)  ORDER BY O.ORDER_ID DESC OFFSET {1} ROWS FETCH NEXT 100 ROWS ONLY";

        public const string QRY_DELETE_ORDER_PRODUCT = "UPDATE ORDER_PRODUCTS SET IS_ACTIVE = 0 , UPDATED_BY = {1}, UPDATED_ON = '{2}' Where OREDER_PRODUCT_ID = {0}";

        public const string QRY_ORDER_BILL = @"select SUM(B.UNIT_PRICE)*op.QUANTITY as BILL,SUM(DISC) AS DISCOUNT,SUM(UNIT_PRICE)*op.QUANTITY - SUM(DISC)*op.QUANTITY -o.MANUAL_DISCOUNT  AS TOTAL,o.MANUAL_DISCOUNT
        
                                    from orders o inner join order_products op on o.order_id= op.order_id 
                                            inner join BARCODES b on OP.PRODUCT_ID = B.ITEM_CODE
                                            where op.order_id = {0} AND B.bDEFAULT = 1 and B.IsActive =1 AND B.LOCNO = O.BRANCH_ID
                                            group by o.MANUAL_DISCOUNT,op.QUANTITY";

        public const string QRY_GET_RIDER_ORDERS = "select * from ORDERS where ORDER_ID = {0} and RIDER_ID = {1}";

        public const string QRY_GET_ORDER_TOTAL_AMOUNT = @"SELECT (SUM(B.UNIT_PRICE * OP.QUANTITY) - O.MANUAL_DISCOUNT) AS TOTAL_ORDER_PRICE FROM ORDERS O--
                                                           INNER JOIN ORDER_PRODUCTS OP ON OP.ORDER_ID = O.ORDER_ID
                                                           INNER JOIN BARCODES B ON B.BAR_CODE = OP.BAR_CODE
                                                           INNER JOIN PRODUCTS P ON B.ITEM_CODE = P.OLD_PRODUCT_ID
                                                           WHERE O.ORDER_ID = {0} AND B.bDEFAULT = 1 AND B.IsActive = 1 AND B.LOCNO = O.BRANCH_ID AND OP.IS_ACTIVE = 1
                                                           GROUP BY O.MANUAL_DISCOUNT";
        public const string QRY_UPDATE_DISC = "UPDATE ORDERS SET MANUAL_DISCOUNT = {0}, UPDATED_ON = '{1}', UPDATED_BY = {2} WHERE ORDER_ID = {3}";
        //public const string QRY_REDEEM_CODE = "select * from COUPONS where CONVERT(nvarchar(MAX),PROMO_TEXT + CODE) = '{0}' and IS_ACTIVE =1 and IS_USED = 0 or IS_USED = null";
        public const string QRY_REDEEM_CODE = "select * from COUPONS where PROMO = '{0}' and IS_ACTIVE =1 and IS_USED = 0 ";

        public const string QRY_UPDATE_COUPON = "update COUPONS set IS_USED = 1,IS_ACTIVE = 0 where COUPON_ID = {0}";

        public int saveOrder(ORDER order)
        {
            db.ORDERS.Add(order);
            db.SaveChanges();
            return order.ORDER_ID;
        }
        public int saveOrderProducts(ORDER_PRODUCTS orderProduct)
        {
            orderProduct.IS_ACTIVE = 1;
            db.ORDER_PRODUCTS.Add(orderProduct);
            db.SaveChanges();
            return orderProduct.OREDER_PRODUCT_ID;
        }
        public DataSet saveOrder2(ORDER order)
        {
            db.ORDERS.Add(order);
            db.SaveChanges();
            return ExecuteDataSet(string.Format(QRY_GET_ORDER_BY_ORDER_ID, order.ORDER_ID));
        }
        public DataSet saveOrderProducts2(ORDER_PRODUCTS orderProduct)
        {
            db.ORDER_PRODUCTS.Add(orderProduct);
            db.SaveChanges();
            return ExecuteDataSet(string.Format(QRY_GET_ORDER_DETAILS, orderProduct.ORDER_ID));
        }
        public OrderProductsDto saveOrderWithProducts(OrderDto orderDto, List<ORDER_PRODUCTS> orderProduct)
        {
            ORDER order = new ORDER()
            {
                ADDED_BY = orderDto.ADDED_BY,
                ADDRESS = orderDto.ADDRESS,
                ASSIGNED_BY = orderDto.ASSIGNED_BY,
                ASSIGNED_ON = orderDto.ASSIGNED_ON,
                BRANCH_ID = orderDto.BRANCH_ID,
                COUPON_DISCOUNT = orderDto.COUPON_DISCOUNT,
                COUPON_ID = orderDto.COUPON_ID,
                CREATED_BY = orderDto.CREATED_BY,
                CREATED_ON = orderDto.CREATED_ON,
                CUSTOMER_ID = orderDto.CUSTOMER_ID,
                DELIVERY_DESCRIPTION = orderDto.DELIVERY_DESCRIPTION,
                IS_ACTIVE = orderDto.IS_ACTIVE,
                IS_PACKAGE = orderDto.IS_PACKAGE,
                MANUAL_DISCOUNT = orderDto.MANUAL_DISCOUNT,
                MOBILE = orderDto.MOBILE,
                NAME = orderDto.NAME,
                ORDER_ID = orderDto.ORDER_ID,
                PAYMENT_MODE_ID = orderDto.PAYMENT_MODE_ID,
                PICKUP_TIME = orderDto.PICKUP_TIME,
                REJECTED_BY = orderDto.REJECTED_BY,
                REJECTED_REASON = orderDto.REJECTED_REASON,
                RIDER_ID = orderDto.RIDER_ID,
                SENT_TO_RIDER_BY = orderDto.SENT_TO_RIDER_BY,
                SENT_TO_RIDER_ON = orderDto.SENT_TO_RIDER_ON,
                STATUS = orderDto.STATUS,
                UPDATED_BY = orderDto.UPDATED_BY,
                UPDATED_ON = orderDto.UPDATED_ON,
                DeliveryFee = orderDto.DeliveryFee,
                EntryType = "MOBILE"
            };

            if (!string.IsNullOrEmpty(orderDto.DELIVERY_TIME))
            {
                order.DELIVERY_TIME = Convert.ToDateTime(orderDto.DELIVERY_TIME);
            }

            BusinessCoreController controller = new BusinessCoreController();
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


            db.ORDERS.Add(order);
            db.SaveChanges();


            SendAlertMessages(order);

            foreach (var item in orderProduct)
            {
                item.ORDER_ID = order.ORDER_ID;
                db.ORDER_PRODUCTS.Add(item);
            }
            db.SaveChanges();

            OrderProductsDto opd = new OrderProductsDto();
            opd.Order = order;
            opd.OrderProducts = orderProduct;

            orderProduct.ToList().ForEach(x =>
            {
                db.PRODUCTS.Where(a => a.PRODUCT_ID == x.PRODUCT_ID).ToList().ForEach(s =>
                    {
                        opd.Products.Add(s);
                    });

                db.BARCODES.Where(b => b.BAR_CODE == x.BAR_CODE && b.LOCNO == order.BRANCH_ID).ToList().ForEach(c =>
                    {
                        opd.Barcodes.Add(c);
                        opd.TotalPrice += Convert.ToDecimal(c.UNIT_PRICE) * Convert.ToInt32(x.QUANTITY);
                    });
            });

            if (opd.TotalPrice > 0)
            {
                opd.TotalPrice = opd.TotalPrice + (decimal)order.DeliveryFee;
            }
            opd.TotalItems = orderProduct.Count;
            //opd.TotalPrice = opd.Products.Sum(x => x.PRICE);

            //INNER JOIN ORDERS O ON OP.ORDER_ID = O.ORDER_ID
            //                                 inner join BARCODES b on OP.BAR_CODE = B.BAR_CODE
            //                                 INNER JOIN PRODUCTS P on b.ITEM_CODE = P.OLD_PRODUCT_ID
            //where OP.ORDER_ID = 1245 and B.bDefault =1 and  B.IsActive =1 and b.LOCNO = o.BRANCH_ID and  OP.IS_ACTIVE =1

            //var q = (from op in db.ORDER_PRODUCTS
            //         join o in db.ORDERS on op.ORDER_ID equals o.ORDER_ID
            //         join b in db.BARCODES on op.BAR_CODE equals b.BAR_CODE
            //         //join p in db.PRODUCTS on b.ITEM_CODE equals p.OLD_PRODUCT_ID
            //         where op.ORDER_ID == order.ORDER_ID && b.bDEFAULT == true && b.IsActive == true && b.LOCNO == o.BRANCH_ID && op.IS_ACTIVE == 1
            //         select new OrderProductsDto
            //         {
            //             Rider_Id = o.RIDER_ID,
            //             OrderProducts = orderProduct,
            //             Products = new List<PRODUCT>(
            //                 from p in db.PRODUCTS
            //                 where b.ITEM_CODE==p.OLD_PRODUCT_ID
            //                 select p
            //                 ),
            //             Barcode = b,
            //             //CHAARSU_IMAGE_PATH = (db.PRODUCT_IMAGES.FirstOrDefault(x => x.PRODUCT_ID == p.PRODUCT_ID) != null) ? db.PRODUCT_IMAGES.FirstOrDefault(x => x.PRODUCT_ID == p.PRODUCT_ID).CHAARSU_IMAGE_PATH : null,
            //             //CHAARSU_THUMBNAIL_PATH = (db.PRODUCT_IMAGES.FirstOrDefault(x => x.PRODUCT_ID == p.PRODUCT_ID) != null) ? db.PRODUCT_IMAGES.FirstOrDefault(x => x.PRODUCT_ID == p.PRODUCT_ID).CHAARSU_THUMBNAIL_PATH : null
            //         }).FirstOrDefault();





            return opd;// ExecuteDataSet(string.Format(QRY_GET_ORDER_DETAILS, orderProduct.ORDER_ID));
        }

        private void SendAlertMessages(ORDER order)
        {
            UserManager controller = new UserManager();
            if (order != null)
            {
                FCMPushNotification notification = new FCMPushNotification();
                if (order.CUSTOMER_ID != null)
                {
                    string user_mobile = controller.getUserMobile(order.CUSTOMER_ID.ToString());
                    SMSManager smsmanager = new SMSManager();
                    smsmanager.GenerateSMSAlert(user_mobile, "Your Order has been placed" + order.ORDER_ID);

                    DataSet user_fcm_token = controller.getUserFCMToken(order.CUSTOMER_ID.ToString());
                    foreach (DataRow r in user_fcm_token.Tables[0].Rows)
                    {
                        notification.SendNotification("Chaarsu", "Your Order has been placed" + order.ORDER_ID, "news", "477625648329", r["FCM_TOKEN"].ToString());
                    }
                    //notification.SendNotification("Chaarsu", "Your Order has been placed" + order.ORDER_ID, "news", "477625648329", user_fcm_token);
                }
                if (order.BRANCH_ID != null)
                {
                    DataSet managers_fcm_token = managerFCMToken(order.BRANCH_ID.ToString());
                    foreach (DataRow r in managers_fcm_token.Tables[0].Rows)
                    {
                        notification.SendNotification("Chaarsu", "New Order!!!" + order.ORDER_ID, "news", "477625648329", r["FCM_TOKEN"].ToString());
                    }
                }
            }
        }

        public DataSet getCustomerPreviousOrders(int? customerId, int index)
        {
            return ExecuteDataSet(string.Format(QRY_GET_CUSTOMER_PREVIOUS_ORDERS, customerId, index));
        }
        public DataSet getCustomerPreviousCompletedOrders(int? customerId, int index)
        {
            return ExecuteDataSet(string.Format(QRY_GET_CUSTOMER_PREVIOUS_COMPLETED_ORDERS, customerId, index));
        }
        public DataSet getAllCustomerOrders(int? customerId, int index)
        {
            //CustomerOrderDto customerOrderDto = new CustomerOrderDto();

            //var query = db.ORDERS.Where(x => x.CUSTOMER_ID == customerId && (x.STATUS == 1 || x.STATUS == 2
            //                                                                               || x.STATUS == 3 || x.STATUS == 4 || x.STATUS == 5 || x.STATUS == 6 || x.STATUS == 7)).ToList();

            //query.ForEach(x =>
            //{
            //    customerOrderDto.ORDERS.Add(new Order()
            //    {
            //        ADDED_BY = x.ADDED_BY,
            //        ADDRESS = x.ADDRESS,
            //        ASSIGNED_BY = x.ASSIGNED_BY,
            //        ASSIGNED_ON = x.ASSIGNED_ON,
            //        BRANCH_ID = x.BRANCH_ID,
            //        COUPON_DISCOUNT = x.COUPON_DISCOUNT,
            //        COUPON_ID = x.COUPON_ID,
            //        CREATED_BY = x.CREATED_BY,
            //        CREATED_ON = x.CREATED_ON,
            //        CUSTOMER_ID = x.CUSTOMER_ID,
            //        DELIVERY_DESCRIPTION = x.DELIVERY_DESCRIPTION,
            //        DELIVERY_TIME = x.DELIVERY_TIME,
            //        IS_ACTIVE = x.IS_ACTIVE,
            //        IS_PACKAGE = x.IS_PACKAGE,
            //        MANUAL_DISCOUNT = x.MANUAL_DISCOUNT,
            //        MOBILE = x.MOBILE,
            //        NAME = x.NAME,
            //        ORDER_ID = x.ORDER_ID,
            //        PAYMENT_MODE_ID = x.PAYMENT_MODE_ID,
            //        PICKUP_TIME = x.PICKUP_TIME,
            //        REJECTED_BY = x.REJECTED_BY,
            //        REJECTED_REASON = x.REJECTED_REASON,
            //        RIDER_ID = x.RIDER_ID,
            //        SENT_TO_RIDER_BY = x.SENT_TO_RIDER_BY,
            //        SENT_TO_RIDER_ON = x.SENT_TO_RIDER_ON,
            //        STATUS = x.STATUS,
            //        UPDATED_BY = x.UPDATED_BY,
            //        UPDATED_ON = x.UPDATED_ON
            //    });
            //});


            //foreach (var item in customerOrderDto.ORDERS)
            //{
            //    foreach (var items in db.ORDER_PRODUCTS.Where(x => x.ORDER_ID == item.ORDER_ID).ToList())
            //    {
            //        foreach (var p in db.BARCODES.Where(x => x.ITEM_CODE == items.PRODUCT_ID && items.BAR_CODE == x.BAR_CODE && item.BRANCH_ID == x.LOCNO && x.bDEFAULT == true && x.IsActive == true).ToList())
            //        {
            //            item.TotalItems++;
            //            item.TotalPrice += Convert.ToDecimal(p.UNIT_PRICE) * Convert.ToInt32(items.QUANTITY);
            //        }
            //    }
            //    item.TotalPrice = item.TotalPrice - Convert.ToDecimal(item.MANUAL_DISCOUNT);
            //}

            //return customerOrderDto;
            return ExecuteDataSet(string.Format(QRY_GET_ALL_CUSTOMER_ORDERS, customerId, index));
        }
        public DataSet getOrderProducts(int orderId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_ORDER_DETAILS, orderId));
        }

        public DataSetDto getCustomerProductsByOrderId(int orderId)
        {
            DataSetDto dataSetDto = null;
            DataSet ds = ExecuteDataSet($"select OP.ORDER_ID,OP.PRODUCT_ID from ORDER_PRODUCTS OP Where OP.ORDER_ID = {orderId}");
            OrderLogic orderLogic = new OrderLogic();
            if (ds.Tables[0].Rows.Count > 0)
            {
                List<int> productIds = new List<int>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    productIds.Add((int)ds.Tables[0].Rows[i]["PRODUCT_ID"]);
                }
                dataSetDto = orderLogic.GetCustomerProducts(productIds);
            }

            return dataSetDto;
        }

        public DataSet getOrderProductsByStatus(int status)
        {
            return ExecuteDataSet(string.Format(QRY_GET_ORDER_BY_STATUS, status));
        }
        public int assignRider(int rider_id, int user_id, int orderID, string pTime)
        {
            riderOrderLogic.UpdateRiderOutTime(rider_id, orderID);
            return ExecuteNonQuery(string.Format(QRY_ASSIGN_RIDER, rider_id, DateTime.Now.ToString(), user_id, orderID, pTime));
        }
        //public int rejectByManager(int orderId, string reason)
        //{
        //    return ExecuteNonQuery(string.Format(QRY_MANAGER_REJECT, orderId, reason));
        //}

        public int updateOrderStatus(int orderId, int currentStatus, int userId)
        {
            try
            {
                ORDER obj = db.ORDERS.Find(orderId);
                if (obj == null)
                    return -1;

                if (currentStatus == ApplicationConstants.PENDING_ORDER_STATUS)
                {
                    //customer ko
                    obj.STATUS = currentStatus + 1;
                    obj.ASSIGNED_BY = userId;
                    obj.ASSIGNED_ON = DateTime.Now;
                }
                else if (currentStatus == ApplicationConstants.ASSIGNED_TO_FLOORMAN_ORDER_STATUS)
                {
                    //customer, maanger

                    obj.STATUS = currentStatus + 1;
                    obj.SENT_TO_RIDER_BY = userId;
                    obj.SENT_TO_RIDER_ON = DateTime.Now;
                    //write call
                }
                //if (currentStatus == ApplicationConstants.DISPATCHED_ORDER_STATUS)
                //{
                //    obj.ASSIGNED_BY = userId;
                //    obj.ASSIGNED_ON = DateTime.Now;
                //}
                //else if (currentStatus == ApplicationConstants.PENDING_ORDER_STATUS)
                //{
                //    obj.ASSIGNED_BY = userId;
                //    obj.ASSIGNED_ON = DateTime.Now;
                //}

                int result = db.SaveChanges();
                if (result < 1)
                    return -1;
                return obj.ORDER_ID;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int updateOrderProducts(ORDER_PRODUCTS orderProducts)
        {
            try
            {
                ORDER_PRODUCTS obj = db.ORDER_PRODUCTS.Find(orderProducts.OREDER_PRODUCT_ID);
                if (obj == null)
                    return -1;

                obj.OREDER_PRODUCT_ID = orderProducts.OREDER_PRODUCT_ID;
                obj.ORDER_ID = orderProducts.ORDER_ID;
                obj.PRODUCT_ID = orderProducts.PRODUCT_ID;
                obj.QUANTITY = orderProducts.QUANTITY;
                obj.IS_ACTIVE = orderProducts.IS_ACTIVE;
                obj.UPDATED_BY = orderProducts.UPDATED_BY;
                obj.UPDATED_ON = DateTime.Now;

                int result = db.SaveChanges();
                if (result < 1)
                    return -1;
                return orderProducts.OREDER_PRODUCT_ID;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public DataSet getBranchOrders(int branchId)
        {
            return ExecuteDataSet(string.Format(QRY_BRANCH_ORDERS, branchId));
        }
        public DataSet getBranchNewOrders(int branchId, int index)
        {
            return ExecuteDataSet(string.Format(QRY_BRANCH_NEW_ORDERS, branchId, index));
        }
        public DataSet getBranchProcessOrders(int branchId, int index)
        {
            return ExecuteDataSet(string.Format(QRY_BRANCH_PROCESS_ORDERS, branchId, index));
        }
        public DataSet getBranchDispatchedOrders(int branchId, int index)
        {
            return ExecuteDataSet(string.Format(QRY_BRANCH_DISPATCHED_ORDERS, branchId, index));
        }
        //public bool updateOrderProducts(ORDER_PRODUCTS orderProducts)
        //{
        //    ORDER_PRODUCTS obj = db.ORDER_PRODUCTS.Find(orderProducts.OREDER_PRODUCT_ID);
        //    if (obj == null)
        //        return false;

        //    obj.ORDER_ID = orderProducts.ORDER_ID;
        //    obj.PRODUCT_ID = orderProducts.PRODUCT_ID;
        //    obj.QUANTITY = orderProducts.QUANTITY;
        //    obj.IS_ACTIVE = orderProducts.IS_ACTIVE;
        //    obj.UPDATED_BY = orderProducts.UPDATED_BY;
        //    obj.UPDATED_ON = DateTime.Now;

        //    int result = db.SaveChanges();
        //    if (result > 0)
        //        return true;
        //    else
        //        return false;
        //}
        public bool deleteOrderProducts(ORDER_PRODUCTS orderProducts)
        {
            int result = ExecuteNonQuery(string.Format(QRY_DELETE_ORDER_PRODUCT, orderProducts.OREDER_PRODUCT_ID, orderProducts.UPDATED_BY, DateTime.Now.ToString()));
            if (result > 0)
                return true;
            else
                return false;

        }
        public DataSet getOrderStats(int orderId)
        {
            return ExecuteDataSet(string.Format(QRY_ORDER_BILL, orderId));
        }
        public DataSet getOrdersByOrderId(int orderId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_ORDER_BY_ORDER_ID, orderId));
        }
        public DataSet getRiderOrders(int orderId, int rider_id)
        {
            return ExecuteDataSet(string.Format(QRY_GET_RIDER_ORDERS, orderId, rider_id));
        }
        public int rejectByManager(int orderId, string reason)
        {
            //user id needed
            return ExecuteNonQuery(string.Format(QRY_MANAGER_REJECT, orderId, reason));
        }
        //public int assignRider(int rider_id, int user_id, int orderID)
        //{
        //    return ExecuteNonQuery(string.Format(QRY_ASSIGN_RIDER, rider_id, DateTime.Now.ToString(), user_id, orderID));
        //}
        public object GetOrderTotalAmount(int orderID)
        {
            return ExecuteScalar(string.Format(QRY_GET_ORDER_TOTAL_AMOUNT, orderID));
        }
        public int updateDiscount(int disc, int user_id, int orderID)
        {
            return ExecuteNonQuery(string.Format(QRY_UPDATE_DISC, disc, DateTime.Now.ToString(), user_id, orderID));
        }
        public DataSet redeemCode(string code)
        {
            return ExecuteDataSet(string.Format(QRY_REDEEM_CODE, code));
        }
        public int updateCoupon(int couponId)
        {
            return ExecuteNonQuery(string.Format(QRY_UPDATE_COUPON, couponId));
        }
        public DataSet managerFCMToken(string branchId)
        {
            return ExecuteDataSet($"select ud.FCM_TOKEN from USERS u INNER JOIN USER_DEVICES ud ON u.USER_ID = ud.USER_ID where BRANCH_ID = {branchId} and USER_TYPE = 2 ");
        }

        public string UpdateRiderOrderStatus(int riderId, int orderId)
        {
            return riderOrderLogic.UpdateRiderInTime(riderId, orderId);
        }
    }
}
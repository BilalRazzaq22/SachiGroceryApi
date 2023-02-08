using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.Models.Dto
{
    public class CustomerOrderDto
    {
        public CustomerOrderDto()
        {
            ORDERS = new List<Order>();
        }
        
        public List<Order> ORDERS { get; set; }
    }

    public class Order
    {
        public int ORDER_ID { get; set; }
        public Nullable<int> CUSTOMER_ID { get; set; }
        public string NAME { get; set; }
        public string MOBILE { get; set; }
        public string ADDRESS { get; set; }
        public int STATUS { get; set; }
        public System.DateTime CREATED_ON { get; set; }
        public Nullable<int> CREATED_BY { get; set; }
        public Nullable<System.DateTime> ASSIGNED_ON { get; set; }
        public Nullable<int> ASSIGNED_BY { get; set; }
        public Nullable<System.DateTime> SENT_TO_RIDER_ON { get; set; }
        public Nullable<int> SENT_TO_RIDER_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_ON { get; set; }
        public Nullable<int> UPDATED_BY { get; set; }
        public Nullable<int> RIDER_ID { get; set; }
        public Nullable<int> REJECTED_BY { get; set; }
        public string REJECTED_REASON { get; set; }
        public Nullable<System.DateTime> DELIVERY_TIME { get; set; }
        public Nullable<int> IS_ACTIVE { get; set; }
        public Nullable<int> BRANCH_ID { get; set; }
        public string DELIVERY_DESCRIPTION { get; set; }
        public Nullable<int> PAYMENT_MODE_ID { get; set; }
        public Nullable<int> MANUAL_DISCOUNT { get; set; }
        public Nullable<int> COUPON_ID { get; set; }
        public Nullable<int> COUPON_DISCOUNT { get; set; }
        public Nullable<System.DateTime> PICKUP_TIME { get; set; }
        public Nullable<int> IS_PACKAGE { get; set; }
        public Nullable<byte> ADDED_BY { get; set; }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

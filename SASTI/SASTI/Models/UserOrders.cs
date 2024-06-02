using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SASTI.Models
{
    public class UserOrders
    {
        public int ORDER_ID { get; set; }
        public int STATUS { get; set; }
        public int RIDER_ID { get; set; }
        public int USER_ID { get; set; }
        public int BRANCH_ID { get; set; }
        public string PICKUP_TIME { get; set; }
        public bool IsActiveUser { get; set; }
    }
}
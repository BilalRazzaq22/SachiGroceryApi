using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SASTI
{
    public class ApplicationConstants
    {
        public const int PENDING_ORDER_STATUS = 1;
        public const int ASSIGNED_TO_FLOORMAN_ORDER_STATUS = 2;
        public const int DISPATCHED_ORDER_STATUS = 3;
        public const int DELIVERED_ORDER_STATUS = 4;
        public const int BOUNCED_ORDER_STATUS = 5;
        public const int REJECT_BY_MANAGER_ORDER_STATUS = 6;
        public const int REJECT_BY_RIDER_ORDER_STATUS = 7;
    }
}
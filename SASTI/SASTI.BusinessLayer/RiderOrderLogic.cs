using SASTI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.BusinessLayer
{
    public class RiderOrderLogic : AbstractFactory
    {
        private List<RIDER_ORDER> RiderOrders = new List<RIDER_ORDER>();
        public RiderOrderLogic()
        {
            RiderOrders = _riderOrder.Repository.GetAll();
        }

        public void UpdateRiderOutTime(int rider_id, int orderid)
        {
            var rider = RiderOrders.Where(x => x.RIDER_ID == rider_id && x.ORDER_ID == orderid).FirstOrDefault();
            if (rider != null)
            {
                rider.RIDER_ID = rider_id;
                rider.ORDER_ID = orderid;
                rider.IS_RIDER_BACK = false;
                rider.RIDER_TIME_OUT = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss tt");
                rider.UPDATED_ON = DateTime.Now;
            }
            else
            {
                rider = new RIDER_ORDER()
                {
                    RIDER_ID = rider_id,
                    ORDER_ID = orderid,
                    IS_RIDER_BACK = false,
                    RIDER_TIME_OUT = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss tt"),
                    CREATED_ON = DateTime.Now
                };
            }

            _riderOrder.Repository.SaveUpdate(rider, rider.RIDER_ORDER_ID);
        }

        public string UpdateRiderInTime(int rider_id, int orderid)
        {
            var rider = RiderOrders.Where(x => x.RIDER_ID == rider_id && x.ORDER_ID == orderid).FirstOrDefault();
            if (rider != null)
            {
                rider.RIDER_ID = rider_id;
                rider.ORDER_ID = orderid;
                rider.IS_RIDER_BACK = true;
                rider.RIDER_TIME_IN = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss tt");
                rider.UPDATED_ON = DateTime.Now;
            }
            else
            {
                rider = new RIDER_ORDER()
                {
                    RIDER_ID = rider_id,
                    ORDER_ID = orderid,
                    IS_RIDER_BACK = true,
                    RIDER_TIME_IN = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss tt"),
                    CREATED_ON = DateTime.Now
                };
            }

            _riderOrder.Repository.SaveUpdate(rider, rider.RIDER_ORDER_ID);
            return $"Rider back at {rider.RIDER_TIME_IN}";
        }
    }
}

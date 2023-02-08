using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SASTI.DataAccess;
using System.Data;
using SASTI.Models;

namespace SASTI.DataCore
{
    public class DriverManager : DABase
    {
        private SASTIEntities gEnt = new SASTIEntities();

        public const string QRY_MARK_DELIVERED = "Update ORDERS SET STATUS = 4 where ORDER_ID = {0}";
        public const string QRY_MARK_BOUNCED = "Update ORDERS SET STATUS = 5, REJECTED_REASON = '{1}' where ORDER_ID = {0}";
        public const string QRY_DRIVER_REJECT = "Update ORDERS SET STATUS = 7,REJECTED_REASON = '{1}' where ORDER_ID = {0}";
        public const string QRY_PREVIOUS_RIDES = "select O.*,B.ADDRESS from ORDERS O INNER JOIN BRANCHES B ON O.BRANCH_ID = B.BRANCH_ID where O.STATUS in (4,5) and O.RIDER_ID = {0} ORDER BY O.ORDER_ID DESC OFFSET 0 ROWS FETCH NEXT 30 ROWS ONLY";
        public const string QRY_GET_ALL_BRANCH_RIDERS = "select * from USERS where USER_TYPE = 5 and IS_ACTIVE = 1 and BRANCH_ID = {0} order by CREATED_ON desc";
        public const string QRY_GET_ALL_RIDERS = "select * from USERS where USER_TYPE = 5 and IS_ACTIVE = 1 order by CREATED_ON desc";
        public const string QRY_GET_AVAILABLE_BRANCH_RIDERS = "select * from USERS where USER_TYPE = 5 and IS_ACTIVE = 1 and IS_AVAILABLE = 1 and BRANCH_ID = {0} order by CREATED_ON desc";
        public const string QRY_GET_AVAILABLE_RIDERS = "select * from USERS where USER_TYPE = 5 and IS_ACTIVE = 1 and IS_AVAILABLE = 1  order by CREATED_ON desc";
        public const string QRY_GET_RIDES = "select O.*,B.ADDRESS from orders O INNER JOIN BRANCHES B ON O.BRANCH_ID = B.BRANCH_ID where RIDER_ID = {0} AND STATUS in (2,3) ORDER BY ORDER_ID DESC";
        public const string QRY_GET_RIDERS_BY_NAME = "SELECT * FROM USERS where USERNAME like '%{0}%'";

        public int markDelivered(int orderId)
        {
            //user id needed
            return ExecuteNonQuery(string.Format(QRY_MARK_DELIVERED, orderId));
        }
        public int markBounced(int orderId, string reason)
        {
            //user id needed
            return ExecuteNonQuery(string.Format(QRY_MARK_BOUNCED, orderId, reason));
        }

        public int rejectByDriver(int orderId, string reason)
        {
            //user id needed
            return ExecuteNonQuery(string.Format(QRY_DRIVER_REJECT, orderId, reason));
        }

        public DataSet getPreviousRides(int userId)
        {
            return ExecuteDataSet(string.Format(QRY_PREVIOUS_RIDES, userId));
        }
        public DataSet getAllBranchRiders(int branchId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_ALL_BRANCH_RIDERS, branchId));
        }
        public DataSet getAllRiders()
        {
            return ExecuteDataSet(QRY_GET_ALL_RIDERS);
        }
        public DataSet getAvailableBranchRiders(int branchId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_AVAILABLE_BRANCH_RIDERS, branchId));
        }
        public DataSet getAvailableRiders()
        {
            return ExecuteDataSet(QRY_GET_AVAILABLE_RIDERS);
        }
        public DataSet getRides(int user_id)
        {
            //return gEnt.USERS.FirstOrDefault(x => x.USER_ID == user_id);
            return ExecuteDataSet(string.Format(QRY_GET_RIDES, user_id));
        }
        public DataSet searchByRiderName(string name)
        {
            return ExecuteDataSet(string.Format(QRY_GET_RIDERS_BY_NAME, name));
        }
    }
}
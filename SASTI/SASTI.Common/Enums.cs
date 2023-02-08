using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.Common
{
    public static class Enums
    {
        public enum RecordStatus
        {
            Active,
            InActive,
            Deleted,
            New,
            Ongoing,
            LiveNow,
            Completed,
            Canceled,
            Pending,
            Accepted,
            Rejected
        }

        public enum UserRole
        {
            User,
            Celebrity,
            Admin,
            SuperAdmin,
            Influencer
        }

        public enum WalletType
        {
            PromoCode,
            UserEntry,
            ReferrerUserId,
            CreatContest,
            CanceledContest,
            SignedUpWithReferUserId,
            SignedUpWithPromoCode,
            FundDeposit,
            FundWithdraw
        }

        public enum PolicyParameters
        {
            ReferrerBonus
        }

        public enum ResponseCode
        {
            Success = 200,
            Failure,
            Unauthorized,
            Exception,
            Info,
            NotExists = 404,
            InCorrectPassword = 403
        }

        public enum Notificationtype
        {
            FriendRequest,
            ContestRequest,
            ContestCanceled,
            ContestInvitation,
            AcceptedRequest
        }
        public enum PaymentStatus
        {
            Paid,
            Authorized,
            Featured
        }

        public enum BoardStatus
        {
            IN,
            OUT
        }

        public enum MyContestListTypes
        {
            Upcoming,
            Live,
            History
        }
    }
}

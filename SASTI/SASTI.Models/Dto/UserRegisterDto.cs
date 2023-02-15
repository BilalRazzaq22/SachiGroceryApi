using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.Models.Dto
{
    public class UserRegisterDto
    {
        public int USER_ID { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public int USER_TYPE { get; set; }
        public Nullable<int> BRANCH_ID { get; set; }
        public string MOBILE_NO { get; set; }
        public string ADDRESS { get; set; }
        public string CITY { get; set; }
        public bool IS_ACTIVE { get; set; }
        public Nullable<System.DateTime> CREATED_ON { get; set; }
        public Nullable<int> CREATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_ON { get; set; }
        public Nullable<int> UPDATED_BY { get; set; }
        public Nullable<int> IS_AVAILABLE { get; set; }
        public string VEHICLE_NUMBER { get; set; }
        public string VEHICLE_DESCRIPTION { get; set; }
        public string USER_DEVICE_ID { get; set; }
        public string FCM_Token { get; set; }
        public string IMAGE_PATH { get; set; }
        public string EMAIL { get; set; }
        public string IPhoneId { get; set; }
        public string FaceBookId { get; set; }
        public Nullable<bool> IsSocialLogin { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SASTI.DataAccess;
using System.Data;
using SASTI.Models;
using SASTI.Models.Dto;
using SASTI.BusinessLayer;

namespace SASTI.DataCore
{
    public class UserManager : DABase
    {
        private SASTIEntities gEnt = new SASTIEntities();


        const string QRY_GET_USER_BY_ID = "SELECT * FROM USERS WHERE USER_ID = {0}";
        const string QRY_REGISTER_CUSTOMER = "insert into USERS values (@p1,@p2,4,@p3,@p4,@p5,@p6,1,@p7,null,null,null)";
        const string QRY_TEMP_CUSTOMER = "select * from temp_customers where temp_customer_id = {0}";
        const string QRY_AUTHENTICATE_USER = "select * from users where username = '{0}' and password = '{1}' and IS_ACTIVE = 1 ";
        const string QRY_AUTHENTICATE_USER_BY_MOBILE = "select * from users where ( MOBILE_NO = '{0}' or email='{0}' ) and IS_ACTIVE = 1 ";
        const string QRY_GET_ALL_ADDRESSES = "select * from USER_ADDRESSES where USER_ID = {0} AND IS_ACTIVE = 1";
        const string QRY_CHECK_MOBILE_EXIST = "select * from USERS where MOBILE_NO = '{0}'";
        const string QRY_CHECK_MOBILE_EMAIL_EXIST = "select * from USERS where MOBILE_NO = '{0}' or email='{1}'";
        const string QRY_CHECK_USERNAME_EXIST = "select * from USERS where username = '{0}'";
        const string QRY_GET_USERTYPENAME = "select * from USER_TYPES where DESCRIPTION like '%{0}%'";
        const string QRY_UPDATE_USER_DEVICE_ID = "update users set DEVICE_ID = '{0}' where USER_ID = {1}";

        const string QRY_EDIT_PROFILE = "update users set image_path = '{1}',UPDATED_BY = {0},UPDATED_ON = '{2}',USERNAME='{3}', PASSWORD = '{4}',ADDRESS = '{5}',CITY='{6}',MOBILE_NO='{7}' where USER_ID = {0}";

        const string QRY_EDIT_ADDRESS = "update USER_ADDRESSES set ADDRESS = '{1}',UPDATED_BY = USER_ID,UPDATED_ON = '{2}' where USER_ID = {0}";

        const string QRY_DELETE_ADDRESS = "update USER_ADDRESSES set IS_ACTIVE =0,UPDATED_BY = USER_ID,UPDATED_ON = '{1}' where USER_ID = {0}";

        const string QRY_DELETE_FAVOURITE_PRODUCTS = "update USER_FAVOURITES set is_Active =0,updated_on = '{2}' where user_id ={0} and product_id ={1} ";

        //        const string QRY_GET_ALL_FAVOURITES = @"SELECT B.UNIT_PRICE,B.DISC ,b.BAR_CODE,P.*,
        //(select top 1 CHAARSU_IMAGE_PATH from PRODUCT_IMAGES PI WHERE PI.PRODUCT_ID = P.PRODUCT_ID)CHAARSU_IMAGE_PATH,(select top 1 CHAARSU_THUMBNAIL_PATH from PRODUCT_IMAGES PI WHERE PI.PRODUCT_ID = P.PRODUCT_ID)
        //FROM USER_FAVOURITES uf
        //inner join PRODUCTS P on uf.PRODUCT_ID = P.OLD_PRODUCT_ID
        //INNER JOIN BARCODES B on P.OLD_PRODUCT_ID = B.ITEM_CODE
        //WHERE uf.USER_ID = {0} AND  uf.IS_ACTIVE = 1 AND B.bdefault =1 and b.isactive =1 and b.locno = (select BRANCH_ID FROM USERS WHERE USER_ID = uf.user_id) ";

        const string QRY_GET_ALL_FAVOURITES = @"SELECT 
                                                ISNULL((select case when QTY > P.THRESHOLD THEN 'YES' ELSE 'NO' END
                                                from STOCK where BRANCH_ID = (select BRANCH_ID FROM USERS WHERE USER_ID = uf.user_id) and PRODUCT_ID = P.OLD_PRODUCT_ID),'NO') AS AVAILABLE,
                                                B.UNIT_PRICE,B.DISC ,b.BAR_CODE,P.*,
                                                (select top 1 CHAARSU_IMAGE_PATH from PRODUCT_IMAGES PI WHERE PI.PRODUCT_ID = P.PRODUCT_ID)CHAARSU_IMAGE_PATH,(select top 1 CHAARSU_THUMBNAIL_PATH from PRODUCT_IMAGES PI WHERE PI.PRODUCT_ID = P.PRODUCT_ID)
                                                FROM USER_FAVOURITES uf
                                                inner join PRODUCTS P on uf.PRODUCT_ID = P.OLD_PRODUCT_ID
                                                INNER JOIN BARCODES B on P.OLD_PRODUCT_ID = B.ITEM_CODE
                                                WHERE uf.USER_ID = {0} AND B.bdefault =1 and b.isactive =1 and b.locno = (select BRANCH_ID FROM USERS WHERE USER_ID = uf.user_id)
                                                ";

        const string QRY_GET_SPECIFIC_FAVOURITES = @"SELECT B.UNIT_PRICE,B.DISC ,b.BAR_CODE,P.*,
(select top 1 CHAARSU_IMAGE_PATH from PRODUCT_IMAGES PI WHERE PI.PRODUCT_ID = P.PRODUCT_ID)CHAARSU_IMAGE_PATH,(select top 1 CHAARSU_THUMBNAIL_PATH from PRODUCT_IMAGES PI WHERE PI.PRODUCT_ID = P.PRODUCT_ID)
FROM USER_FAVOURITES uf
inner join PRODUCTS P on uf.PRODUCT_ID = P.OLD_PRODUCT_ID
INNER JOIN BARCODES B on P.OLD_PRODUCT_ID = B.ITEM_CODE
WHERE uf.USER_ID = {0} AND uf.PRODUCT_ID = {1}  AND  uf.IS_ACTIVE = 1 AND B.bdefault =1 and b.isactive =1 and b.locno = (select BRANCH_ID FROM USERS WHERE USER_ID = uf.user_id) ";

        const string QRY_RESET_PASSWORD = "UPDATE USERS SET USER_TYPE =4,PASSWORD = '{0}' WHERE MOBILE_NO ='{1}'";

        public DataSet ifMobileExist(string mobile, string email)
        {
            if (email != null && email.Length > 0)
            {
                return ExecuteDataSet(string.Format(QRY_CHECK_MOBILE_EMAIL_EXIST, mobile, email));

            }
            else if (mobile != null && mobile.Length > 0)
            {
                return ExecuteDataSet(string.Format(QRY_CHECK_MOBILE_EXIST, mobile));

            }
            else
            {
                return null;
            }

        }
        public DataSet ifMobileExist(string mobile)
        {
            return ExecuteDataSet(string.Format(QRY_CHECK_MOBILE_EXIST, mobile));
        }

        public DataSet ifUserNameExist(string uname)
        {
            return ExecuteDataSet(string.Format(QRY_CHECK_USERNAME_EXIST, uname));

        }
        public DataSet GetUserType(string userTypeName)
        {
            return ExecuteDataSet(string.Format(QRY_GET_USERTYPENAME, userTypeName));

        }
        public DataSet GetUserById(int userId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_USER_BY_ID, userId));
        }
        public DataSet GetAllAddressesByCustomerId(int userId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_ALL_ADDRESSES, userId));
        }
        public USER AuthenticateUser(string username, string password, int userTypeID)
        {
            try
            {
                if (userTypeID != 0)
                {
                    return gEnt.USERS.FirstOrDefault(x => x.USERNAME == username && x.PASSWORD == password && x.IS_ACTIVE == true && x.USER_TYPE == userTypeID);
                }
                else
                {
                    return gEnt.USERS.FirstOrDefault(x => x.USERNAME == username && x.PASSWORD == password && x.IS_ACTIVE == true);
                }

                //return ExecuteDataSet(string.Format(QRY_AUTHENTICATE_USER, username, password));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public USER authenticateUserByMobile(string mobile, int userTypeID)
        {
            try
            {
                return gEnt.USERS.FirstOrDefault(x => x.MOBILE_NO == mobile && x.USER_TYPE == userTypeID);
                //return ExecuteDataSet(string.Format(QRY_AUTHENTICATE_USER_BY_MOBILE, mobile));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DataSet RegisterCustomer(USER user)
        {
            try
            {
                user.CREATED_ON = DateTime.Now;
                user.USER_TYPE = 4;
                user.IS_ACTIVE = true;
                gEnt.USERS.Add(user);
                gEnt.SaveChanges();


                return ExecuteDataSet(string.Format(QRY_GET_USER_BY_ID, user.USER_ID));

            }
            catch (Exception)
            {
                throw;
            }
        }
        public USER RegisterUser(UserRegisterDto user)
        {
            try
            {
                bool createFlag = false;
                bool mobileFlag = false;
                if (user.MOBILE_NO != null)
                {
                    if (gEnt.USERS.Where(x => x.MOBILE_NO == user.MOBILE_NO).Count() == 0)
                    {
                        createFlag = true;
                    }
                    else
                    {
                        mobileFlag = true;
                    }
                }
                if (user.EMAIL != null && !mobileFlag && !createFlag)
                {
                    if (gEnt.USERS.Where(x => x.EMAIL == user.EMAIL).Count() == 0)
                    {
                        createFlag = true;
                    }
                }
                if (createFlag)
                {
                    USER u = new USER()
                    {
                        ADDRESS = user.ADDRESS,
                        BRANCH_ID = user.BRANCH_ID,
                        CITY = user.CITY,
                        CREATED_BY = user.CREATED_BY,
                        CREATED_ON = DateTime.Now,
                        EMAIL = user.EMAIL,
                        FaceBookId = user.FaceBookId,
                        IMAGE_PATH = user.IMAGE_PATH,
                        IPhoneId = user.IPhoneId,
                        IsSocialLogin = user.IsSocialLogin,
                        IS_ACTIVE = true,
                        IS_AVAILABLE = user.IS_AVAILABLE,
                        MOBILE_NO = user.MOBILE_NO,
                        PASSWORD = user.PASSWORD,
                        UPDATED_BY = user.UPDATED_BY,
                        UPDATED_ON = user.UPDATED_ON,
                        USERNAME = user.USERNAME,
                        USER_TYPE = user.USER_TYPE,
                        VEHICLE_DESCRIPTION = user.VEHICLE_DESCRIPTION,
                        VEHICLE_NUMBER = user.VEHICLE_NUMBER
                    };



                    //user.CREATED_ON = DateTime.Now;
                    ////user.USER_TYPE = 4;
                    //user.IS_ACTIVE = true;
                    gEnt.USERS.Add(u);
                    gEnt.SaveChanges();

                    var userObj = gEnt.USERS.FirstOrDefault(x => x.MOBILE_NO == user.MOBILE_NO);
                    if (userObj != null)
                    {
                        UsersLogic usersLogic = new UsersLogic();
                        UserRegisterDto userRegisterDto = new UserRegisterDto()
                        {
                            USER_ID = userObj.USER_ID,
                            FCM_Token = user.FCM_Token,
                            USER_DEVICE_ID = user.USER_DEVICE_ID
                        };

                        usersLogic.RegisterDeviceFCMToken(userRegisterDto);
                    }

                    return u;//ExecuteDataSet(string.Format(QRY_GET_USER_BY_ID, user.USER_ID));
                }
                return null;


                //int result = gEnt.SaveChanges();
                //if (result < 1)
                //    return -1;
                //return obj.USER_ID;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public DataSet saveCustomer(TEMP_CUSTOMERS customer)
        //{
        //    try
        //    {
        //        gEnt.TEMP_CUSTOMERS.Add(customer);
        //        gEnt.SaveChanges();
        //        return ExecuteDataSet(string.Format(QRY_TEMP_CUSTOMER, customer.TEMP_CUSTOMER_ID));
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        public USER_ADDRESSES saveNewAddress(USER_ADDRESSES customer)
        {
            customer.CREATED_ON = DateTime.Now;
            customer.IS_ACTIVE = true;
            gEnt.USER_ADDRESSES.Add(customer);
            gEnt.SaveChanges();
            return customer;

        }
        public int updateDeviceId(string deviceId, string userId)
        {
            return ExecuteNonQuery(string.Format(QRY_UPDATE_USER_DEVICE_ID, deviceId, userId));
        }
        public DataSet getUserFCMToken(string userId)
        {
            return ExecuteDataSet(string.Format("select FCM_Token from USER_DEVICES where USER_ID = {0}", userId));
            //object device = ExecuteScalar(string.Format("select FCM_Token from USER_DEVICES where USER_ID = {0}", userId));
            //if (device != null)
            //    return device.ToString();
            //else
            //    return DateTime.Now.ToString();
        }
        public string getUserMobile(string userId)
        {
            if (!string.IsNullOrEmpty(userId) && userId != "0")
            {
                object mobile = ExecuteScalar(string.Format("select MOBILE_NO from USERS where USER_ID = {0}", userId));
                if (mobile != null)
                    return mobile.ToString();
            }
            return "000000000000";
        }
        public DataSet authenticateWebUser(string username, string password)
        {
            return ExecuteDataSet(string.Format(QRY_AUTHENTICATE_USER, username, password));
        }
        public DataSet editProfile(USER user)
        {
            ExecuteNonQuery(string.Format(QRY_EDIT_PROFILE, user.USER_ID, user.IMAGE_PATH, DateTime.Now.ToString(), user.USERNAME, user.PASSWORD, user.ADDRESS, user.CITY, user.MOBILE_NO));
            return ExecuteDataSet(string.Format(QRY_GET_USER_BY_ID, user.USER_ID));
        }
        public DataSet editAddress(USER_ADDRESSES address)
        {
            ExecuteNonQuery(string.Format(QRY_EDIT_ADDRESS, address.USER_ID, address.ADDRESS, DateTime.Now.ToString()));
            return ExecuteDataSet(string.Format(QRY_GET_ALL_ADDRESSES, address.USER_ID));
        }
        public DataSet deleteAddress(USER_ADDRESSES address)
        {
            ExecuteNonQuery(string.Format(QRY_DELETE_ADDRESS, address.USER_ID, DateTime.Now.ToString()));
            return ExecuteDataSet(string.Format(QRY_GET_ALL_ADDRESSES, address.USER_ID));
        }
        public USER_FAVOURITES addFavouriteProducts(USER_FAVOURITES data)
        {
            data.CREATED_ON = DateTime.Now;
            data.IS_ACTIVE = 1;
            gEnt.USER_FAVOURITES.Add(data);
            gEnt.SaveChanges();
            return data;
        }
        public DataSet deleteFavouriteProducts(USER_FAVOURITES data)
        {
            ExecuteNonQuery(string.Format(QRY_DELETE_FAVOURITE_PRODUCTS, data.USER_ID, data.PRODUCT_ID, DateTime.Now.ToString()));
            return ExecuteDataSet(string.Format(QRY_GET_ALL_FAVOURITES, data.USER_ID));

        }
        public DataSet getAllFavourites(USER_FAVOURITES data)
        {
            return ExecuteDataSet(string.Format(QRY_GET_ALL_FAVOURITES, data.USER_ID));
        }
        public DataSet getSpecificFavourites(USER_FAVOURITES data)
        {
            return ExecuteDataSet(string.Format(QRY_GET_SPECIFIC_FAVOURITES, data.USER_ID, data.PRODUCT_ID));
        }
        public DataSet resetPassword(USER u)
        {
            DataSet ds = GetUserById(0);
            int result = ExecuteNonQuery(string.Format(QRY_RESET_PASSWORD, u.PASSWORD, u.MOBILE_NO));
            if (result > 0)
            {
                ds = ifMobileExist(u.MOBILE_NO);
                return ds;

            }
            else
                return ds;
        }
    }
}

using SASTI.Common;
using SASTI.Models;
using SASTI.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SASTI.BusinessLayer
{
    public class UsersLogic : AbstractFactory
    {

        public DataSetDto FacebookLogin(UserDto param)
        {
            DataSetDto dataSetDto = new DataSetDto();
            try
            {
                //if (string.IsNullOrEmpty(param.FaceBookId))
                //{
                //    return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "FacebookId is required.");
                //}
                //else if (string.IsNullOrEmpty(param.EMAIL))
                //{
                //    return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "Email is required.");
                //}
                //else
                //{
                var obj = _user.Repository.GetAll(x => x.FaceBookId == param.FaceBookId && x.IS_ACTIVE == true && x.IsSocialLogin == true).Select(x => new
                {
                    x.USERNAME,
                    x.USER_ID,
                    x.MOBILE_NO,
                    x.IMAGE_PATH,
                    x.IsSocialLogin,
                    x.DEVICE_ID,
                    x.CITY,
                    x.ADDRESS,
                    x.EMAIL,
                    x.FaceBookId,
                    x.IPhoneId
                }).FirstOrDefault();
                if (obj != null)
                {
                    var userDevice = _userDevices.Repository.FirstOrDefault(x => x.USER_ID == obj.USER_ID && x.DEVICE_ID == param.USER_DEVICE_ID);
                    if (userDevice != null)
                    {
                        userDevice.FCM_TOKEN = param.FCM_Token;
                        _userDevices.Repository.Update(userDevice);
                    }
                    else
                    {
                        USER_DEVICES userDevices = new USER_DEVICES()
                        {
                            USER_ID = obj.USER_ID,
                            DEVICE_ID = param.USER_DEVICE_ID,
                            FCM_TOKEN = param.FCM_Token
                        };
                        _userDevices.Repository.Add(userDevices);
                    }

                    dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                    dataSetDto.Response.Message = "Success";
                    dataSetDto.Response.Data = obj;
                    return dataSetDto;
                    //return JsonResponse.GetResponse(Enums.ResponseCode.Success, obj);
                }
                else
                {
                    USER u = new USER()
                    {
                        ADDRESS = param.ADDRESS,
                        BRANCH_ID = param.BRANCH_ID,
                        CITY = param.CITY,
                        CREATED_BY = param.CREATED_BY,
                        CREATED_ON = DateTime.Now,
                        EMAIL = param.EMAIL,
                        FaceBookId = param.FaceBookId,
                        IMAGE_PATH = param.IMAGE_PATH,
                        IPhoneId = param.IPhoneId,
                        IsSocialLogin = true,
                        IS_ACTIVE = true,
                        IS_AVAILABLE = param.IS_AVAILABLE,
                        MOBILE_NO = param.MOBILE_NO,
                        PASSWORD = "FACEBOOK",
                        UPDATED_BY = param.UPDATED_BY,
                        UPDATED_ON = DateTime.Now,
                        USERNAME = param.USERNAME,
                        USER_TYPE = param.USER_TYPE_ID,
                        VEHICLE_DESCRIPTION = param.VEHICLE_DESCRIPTION,
                        VEHICLE_NUMBER = param.VEHICLE_NUMBER
                    };

                    //param.CREATED_ON = DateTime.Now;
                    //param.UPDATED_ON = DateTime.Now;
                    //param.IS_ACTIVE = true;
                    //param.PASSWORD = "FACEBOOK";
                    //param.IsSocialLogin = true;

                    _user.Repository.Add(u);

                    obj = _user.Repository.GetAll(x => x.FaceBookId == param.FaceBookId && x.IS_ACTIVE == true && x.IsSocialLogin == true).Select(x => new
                    {
                        x.USERNAME,
                        x.USER_ID,
                        x.MOBILE_NO,
                        x.IMAGE_PATH,
                        x.IsSocialLogin,
                        x.DEVICE_ID,
                        x.CITY,
                        x.ADDRESS,
                        x.EMAIL,
                        x.FaceBookId,
                        x.IPhoneId
                    }).FirstOrDefault();


                    USER_DEVICES userDevices = new USER_DEVICES()
                    {
                        USER_ID = obj.USER_ID,
                        DEVICE_ID = param.USER_DEVICE_ID,
                        FCM_TOKEN = param.FCM_Token
                    };
                    _userDevices.Repository.Add(userDevices);

                    dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                    dataSetDto.Response.Message = "Success";
                    dataSetDto.Response.Data = obj;
                    return dataSetDto;
                    //return JsonResponse.GetResponse(Enums.ResponseCode.Success, obj);
                }

                //}

            }
            catch (Exception ex)
            {
                LogHelper.CreateLog(ex);
                dataSetDto.Response.Code = (int)HttpStatusCode.BadRequest;
                dataSetDto.Response.Message = ex.Message;
                dataSetDto.Response.Data = null;
                return dataSetDto;
                //return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.Message);
            }
        }
        public ApiResponse IPhoneLogin(USER param)
        {
            try
            {
                if (string.IsNullOrEmpty(param.IPhoneId))
                {
                    return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "Iphone is required.");
                }
                if (string.IsNullOrEmpty(param.EMAIL))
                {
                    return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "Email is required.");
                }
                else
                {
                    var obj = _user.Repository.GetAll(x => x.IPhoneId == param.IPhoneId && x.IS_ACTIVE == true && x.IsSocialLogin == true).Select(x => new
                    {
                        x.USERNAME,
                        x.USER_ID,
                        x.MOBILE_NO,
                        x.IMAGE_PATH,
                        x.IsSocialLogin,
                        x.DEVICE_ID,
                        x.IPhoneId,
                        x.FaceBookId,
                        x.CITY,
                        x.ADDRESS,
                        x.EMAIL
                    }).FirstOrDefault();
                    if (obj != null)
                    {
                        return JsonResponse.GetResponse(Enums.ResponseCode.Success, obj);
                    }
                    else
                    {
                        param.CREATED_ON = DateTime.Now;
                        param.UPDATED_ON = DateTime.Now;
                        param.IS_ACTIVE = true;
                        param.PASSWORD = "IPHONE";
                        param.IsSocialLogin = true;

                        _user.Repository.Add(param);

                        obj = _user.Repository.GetAll(x => x.IPhoneId == param.IPhoneId && x.IS_ACTIVE == true && x.IsSocialLogin == true).Select(x => new
                        {
                            x.USERNAME,
                            x.USER_ID,
                            x.MOBILE_NO,
                            x.IMAGE_PATH,
                            x.IsSocialLogin,
                            x.DEVICE_ID,
                            x.IPhoneId,
                            x.FaceBookId,
                            x.CITY,
                            x.ADDRESS,
                            x.EMAIL
                        }).FirstOrDefault();

                        return JsonResponse.GetResponse(Enums.ResponseCode.Success, obj);
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.CreateLog(ex);
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.Message);
            }
        }

        public DataSetDto UpdateUserProfile(UserProfileDto param)
        {
            DataSetDto dataSetDto = new DataSetDto();
            try
            {
                var userObj = _user.Repository.GetAll().FirstOrDefault(x => x.USER_ID == param.USER_ID && x.IS_ACTIVE == true);

                if (userObj != null)
                {
                    if (!string.IsNullOrEmpty(param.OLDPASSWORD))
                    {
                        if (param.OLDPASSWORD != userObj.PASSWORD)
                        {
                            dataSetDto.Response.Code = (int)HttpStatusCode.Unauthorized;
                            dataSetDto.Response.Message = "old password not matching with your existing password";
                            dataSetDto.Response.Data = null;
                            return dataSetDto;
                        }
                    }

                    userObj.IMAGE_PATH = param.IMAGE_PATH;
                    if (!string.IsNullOrEmpty(param.USERNAME))
                        userObj.USERNAME = param.USERNAME;
                    if (!string.IsNullOrEmpty(param.OLDPASSWORD) && !string.IsNullOrEmpty(param.NEWPASSWORD))
                        userObj.PASSWORD = param.NEWPASSWORD;
                    if (!string.IsNullOrEmpty(param.EMAIL))
                        userObj.EMAIL = param.EMAIL;
                    userObj.UPDATED_BY = param.USER_ID;
                    userObj.UPDATED_ON = DateTime.Now;
                    userObj.ADDRESS = param.ADDRESS;
                    userObj.CITY = param.CITY;
                    if (!string.IsNullOrEmpty(param.MOBILE_NO))
                        userObj.MOBILE_NO = param.MOBILE_NO;

                    _user.Repository.Update(userObj);

                    dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                    dataSetDto.Response.Message = "Success";
                    dataSetDto.Response.Data = userObj;
                    return dataSetDto;
                }
                else
                {
                    dataSetDto.Response.Code = (int)HttpStatusCode.BadRequest;
                    dataSetDto.Response.Message = "Unable to find user record";
                    dataSetDto.Response.Data = null;
                    return dataSetDto;
                }
                return dataSetDto;
            }
            catch (Exception ex)
            {
                LogHelper.CreateLog(ex);
                dataSetDto.Response.Code = (int)HttpStatusCode.BadRequest;
                dataSetDto.Response.Message = "Unable to update user profile";
                dataSetDto.Response.Data = ex;
                return dataSetDto;
            }
        }

        public DataSetDto LogOut(UserDto param)
        {
            DataSetDto dataSetDto = new DataSetDto();
            try
            {
                var userObj = _userDevices.Repository.GetAll().FirstOrDefault(x => x.USER_ID == param.USER_ID && x.DEVICE_ID == param.USER_DEVICE_ID);

                if (userObj != null)
                {
                    userObj.FCM_TOKEN = null;
                    _userDevices.Repository.Update(userObj);

                    dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                    dataSetDto.Response.Message = "User logged out successfully";
                    dataSetDto.Response.Data = userObj;
                }
                else
                {
                    dataSetDto.Response.Code = (int)HttpStatusCode.BadRequest;
                    dataSetDto.Response.Message = "Unable to log out user";
                    dataSetDto.Response.Data = null;
                }
                return dataSetDto;
            }
            catch (Exception ex)
            {
                LogHelper.CreateLog(ex);
                dataSetDto.Response.Code = (int)HttpStatusCode.BadRequest;
                dataSetDto.Response.Message = "Unable to log out user";
                dataSetDto.Response.Data = ex;
                return dataSetDto;
            }
        }

        public void CheckDeviceFCMToken(UserDto userDto, USER user)
        {
            var userDevice = _userDevices.Repository.FirstOrDefault(x => x.USER_ID == user.USER_ID && x.DEVICE_ID == userDto.USER_DEVICE_ID);
            if (userDevice != null)
            {
                userDevice.FCM_TOKEN = userDto.FCM_Token;
                _userDevices.Repository.Update(userDevice);
            }
            else
            {
                USER_DEVICES userDevices = new USER_DEVICES()
                {
                    USER_ID = user.USER_ID,
                    DEVICE_ID = userDto.USER_DEVICE_ID,
                    FCM_TOKEN = userDto.FCM_Token
                };
                _userDevices.Repository.Add(userDevices);
            }
        }

        public void RegisterDeviceFCMToken(UserRegisterDto userRegisterDto)
        {
            USER_DEVICES userDevices = new USER_DEVICES()
            {
                USER_ID = userRegisterDto.USER_ID,
                DEVICE_ID = userRegisterDto.USER_DEVICE_ID,
                FCM_TOKEN = userRegisterDto.FCM_Token
            };
            _userDevices.Repository.Add(userDevices);
        }

        public List<USER_ADDRESSES> SaveUserAddresses(UserAddressDto userAddress)
        {
            //var userAdd = _userAddresses.Repository.GetAll().Where(x => x.USER_ID == userAddress.UserId).ToList();
            
            foreach (var item in userAddress.UserAddresses)
            {
                var ua = _userAddresses.Repository.GetAll().FirstOrDefault(x => x.USER_ID == userAddress.UserId && x.LATITUDE == item.Latitude && x.LONGITUDE == item.Longitude && x.IS_ACTIVE == true);
                if (ua == null)
                {
                    USER_ADDRESSES uaddress = new USER_ADDRESSES()
                    {
                        CREATED_ON = DateTime.Now,
                        IS_ACTIVE = true,
                        ADDRESS = item.Name,
                        USER_ID = userAddress.UserId,
                        LATITUDE = item.Latitude,
                        LONGITUDE = item.Longitude,
                        BRANCH_ID=item.BranchId
                    };
                    _userAddresses.Repository.Add(uaddress);
                }
                else
                {
                    ua.ADDRESS = item.Name;
                    ua.UPDATED_ON = DateTime.Now;
                    ua.IS_ACTIVE = true;

                    //USER_ADDRESSES uaddress = new USER_ADDRESSES()
                    //{
                    //    USER_ADDRESS_ID = ua.USER_ADDRESS_ID,
                    //    UPDATED_ON = DateTime.Now,
                    //    IS_ACTIVE = true,
                    //    ADDRESS = item.Name,
                    //    USER_ID = userAddress.UserId,
                    //    LATITUDE = ua.LATITUDE,
                    //    LONGITUDE = ua.LONGITUDE,
                    //    BRANCH_ID = ua.BRANCH_ID
                    //};
                    _userAddresses.Repository.Update(ua);
                }
                
            }
            return _userAddresses.Repository.GetAll().Where(x => x.USER_ID == userAddress.UserId).ToList();
        }

        //public AjaxResponse UpdateUser(InputUserModel param)
        //{
        //    try
        //    {

        //        var user_ = _frontuser.Repository.GetAll(x => x.Email == param.Email && x.UserId != param.UserId).FirstOrDefault();
        //       if(user_==null)
        //       {
        //           var entity = _frontuser.Repository.GetById(param.UserId);
        //           if(entity!=null)
        //           {
        //               entity.Email = param.Email;
        //               entity.Password = StringCipher.Encrypt(param.Password);
        //               entity.RecordStatus = param.RecordStatus;
        //               entity.FullName = param.FullName;
        //               _frontuser.Repository.Update(entity);

        //               return new AjaxResponse(true, ResponseMessage.Updated_Successfully);
        //           }
        //           else
        //           {
        //               return new AjaxResponse(false, ResponseMessage.User_Not_Found);
        //           }
        //       }
        //       else
        //       {
        //           return new AjaxResponse(false, ResponseMessage.User_already_exists_with_email);
        //       }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.CreateLog(ex);
        //        return new AjaxResponse(false, ResponseMessage.Exception_Something_Went_Wrong);
        //    }
        //}
        //public AjaxResponse DeletePromoCode(int PromoCodeId)
        //{
        //    try
        //    {

        //        _promocode.Repository.DeletePermanent(PromoCodeId);

        //        return new AjaxResponse(true, ResponseMessage.Delete_Successfully);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.CreateLog(ex);
        //        return new AjaxResponse(false, ResponseMessage.Exception_Something_Went_Wrong);
        //    }
        //}
        //public AjaxResponse GetAllPromoCodes()
        //{
        //    try
        //    {
        //        List<PromoCodeModel> entitlistview = new List<PromoCodeModel>();
        //        var entitylist = _promocode.Repository.GetAll().ToList();
        //        foreach(var promo in entitylist )
        //        {

        //            PromoCodeModel entity = new PromoCodeModel()
        //            {
        //                PromoCodeId = promo.PromoCodeId,
        //                PromoCodeName = promo.PromoCodeName,
        //                Code = promo.Code,
        //                ValidFrom = Convert.ToDateTime(GetESTDateTime(promo.ValidFrom)).ToString("MM/dd/yyyy"),
        //                ValidTo = Convert.ToDateTime(GetESTDateTime(promo.ValidTo)).ToString("MM/dd/yyyy"),
        //                Value = promo.Value,
        //                ValueUnit = promo.ValueUnit,
        //                UsageLimit=promo.UsageLimit
        //            };

        //            entitlistview.Add(entity);

        //        }
        //        return new AjaxResponse(true, "", true, entitlistview);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.CreateLog(ex);
        //        return new AjaxResponse(false, ResponseMessage.Exception_Something_Went_Wrong);
        //    }
        //}
        //public AjaxResponse GetAllUsers(int PageIndex, int PageSize, string SortColumn, string SortOrder, string EmailName, DateTime? DateFrom, DateTime? DateTo)
        //{
        //    try
        //    {
        //        List<UsersModel> listview = new List<UsersModel>();
        //        var entitylist = _sp.GetAllUsers(PageIndex, PageSize, SortColumn, SortOrder, EmailName, DateFrom, DateTo);
        //        foreach (var user in entitylist)
        //        {

        //            UsersModel entity = new UsersModel()
        //            {
        //                UserId = user.UserId,
        //                Email = user.Email,
        //                FullName =  user.FullName,
        //                ImageUrl = string.IsNullOrEmpty(user.ImageUrl) ? null : user.ImageUrl,
        //                Phone = user.Phone,
        //                DeviceToken = user.DeviceToken,
        //                Notifications = user.Notifications,
        //                DeviceType = user.DeviceType,
        //                RecordStatus = user.RecordStatus,
        //                RowIndex = user.RowIndex,
        //                TotalPages=user.TotalPages,
        //                TotalRecords = user.TotalRecords,
        //                Start = user.Start,
        //                End = user.End,
        //                UserReservationCount=user.UserReservationCount,
        //                CreatedDate = Convert.ToDateTime(GetESTDateTime(user.CreatedDate)).ToString("MM/dd/yyyy"),
        //                ModifiedDate = Convert.ToDateTime(GetESTDateTime(user.ModifiedDate)).ToString("MM/dd/yyyy"),
        //                Latitude = user.Latitude,
        //                Longitude = user.Longitude
        //            };
        //            listview.Add(entity);

        //        }
        //        return new AjaxResponse(true, "", true, listview);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.CreateLog(ex);
        //        return new AjaxResponse(false, ResponseMessage.Exception_Something_Went_Wrong);
        //    }
        //}

        //public AjaxResponse GetUserByUserId(int PageIndex, int PageSize, string SortColumn, string SortOrder, int UserId)
        //{
        //    try
        //    {
        //        UsersModel obj = null;
        //        var user = _frontuser.Repository.GetById(UserId);
        //        if (user != null)
        //        {
        //            obj = new UsersModel()
        //            {
        //                UserId = user.UserId,
        //                Email = user.Email,
        //                FullName = user.FullName,
        //                ImageUrl = string.IsNullOrEmpty(user.ImageUrl) ? null : user.ImageUrl,
        //                Phone = user.Phone,
        //                Password=user.Password==null?null:StringCipher.Decrypt(user.Password),
        //                DeviceToken = user.DeviceToken,
        //                Notifications = user.Notifications,
        //                DeviceType = user.DeviceType,
        //                RecordStatus = user.RecordStatus,
        //                CreatedDate = Convert.ToDateTime(GetESTDateTime(user.CreatedDate)).ToString("MM/dd/yyyy"),
        //                ModifiedDate = Convert.ToDateTime(GetESTDateTime(user.ModifiedDate)).ToString("MM/dd/yyyy"),
        //                Latitude = user.Latitude,
        //                Longitude = user.Longitude,
        //                reservation = GetAllUserReservationByUserId(PageIndex,PageSize,SortColumn,SortOrder,user.UserId)
        //            };
        //        }



        //        return new AjaxResponse(true, "", true, obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.CreateLog(ex);
        //        return new AjaxResponse(false, ResponseMessage.Exception_Something_Went_Wrong);
        //    }
        //}


        //public List<ViewReservation> GetAllUserReservationByUserId(int PageIndex, int PageSize, string SortColumn, string SortOrder, int UserId)
        //{
        //    try
        //    {
        //        var userRes = _sp.GetAllUserReservationByUserId(PageIndex, PageSize, SortColumn, SortOrder, UserId);
        //        ViewReservation obj;

        //        List<ViewReservation> listobj = new List<ViewReservation>();

        //        foreach (var res in userRes)
        //        {
        //            obj = new ViewReservation()
        //            {
        //                UserReservationId = res.UserReservationId,
        //                UserId = res.UserId,
        //                BoardId = res.BoardId,
        //                StartTime = DateTimeString(res.StartTime),
        //                EndTime = DateTimeString(res.EndTime),
        //                Amount = res.Amount,
        //                ReservationStatus = res.ReservationStatus,
        //                PaymentStatus = res.PaymentStatus,
        //                TotalDistance = res.TotalDistance,
        //                RecordStatus = res.RecordStatus,
        //                CreatedDate = res.CreatedDate,
        //                ModifiedDate = res.ModifiedDate,
        //                RowIndex = res.RowIndex,
        //                TotalRecords = res.TotalRecords,
        //                TotalPages = res.TotalPages,
        //                Start = res.Start,
        //                dwelltime = Getdwelltime(res.StartTime, res.EndTime),
        //                End = res.End
        //            };

        //            listobj.Add(obj);
        //        }

        //        return listobj;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.CreateLog(ex);
        //        return null;
        //    }
        //}
        //public string Getdwelltime(DateTime? Start, DateTime? End)
        //{
        //    try
        //    {
        //        string dwelltime = "Pending";
        //        if (End != null)
        //        {


        //            TimeSpan span = (Convert.ToDateTime(GetESTDateTime(Convert.ToDateTime(End))) - Convert.ToDateTime(GetESTDateTime(Convert.ToDateTime(Start))));
        //            dwelltime = span.Hours + ":" + span.Minutes;
        //        }
        //        return dwelltime;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;

        //    }
        //}
        //public string DateTimeString(DateTime? Date)
        //{
        //    try
        //    {
        //        string returndate = "Pending";
        //        if (Date != null)
        //        {
        //            DateTime datetime = Convert.ToDateTime(GetESTDateTime(Convert.ToDateTime(Date)));
        //            returndate = datetime.ToString("MM/dd/yyyy h:m");
        //        }


        //        return returndate;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;

        //    }
        //}
    }
}

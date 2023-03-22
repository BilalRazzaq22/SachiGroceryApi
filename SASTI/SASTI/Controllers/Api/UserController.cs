using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Newtonsoft.Json;
using SASTI.Common;
using SASTI.Models;
using SASTI.BusinessLayer;
using SASTI.Models.Dto;

namespace SASTI.Controllers.Api
{
    public class UserController : ApiController
    {
        //[HttpPost]
        //[Route("api/User/signup")]
        //public ApiResponse Signup()
        //{
        //    try
        //    {

        //        if (HttpContext.Current.Request.Form["Email"] == null && HttpContext.Current.Request.Form["Email"] == "")
        //        {
        //            return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "Email cannot be null or empty");
        //        }
        //        if (HttpContext.Current.Request.Form["Name"] == null && HttpContext.Current.Request.Form["Name"] == "")
        //        {
        //            return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "User name cannot be null or empty");
        //        }
        //        if (HttpContext.Current.Request.Form["Password"] == null && HttpContext.Current.Request.Form["Password"] == "")
        //        {
        //            return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "Password cannot be null or empty");
        //        }
        //        if (HttpContext.Current.Request.Form["DeviceType"] == null && HttpContext.Current.Request.Form["DeviceType"] == "")
        //        {
        //            return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "Deviice type cannot be null or empty");
        //        }
        //        else
        //        {
        //            ApiSignup obj;
        //            if (HttpContext.Current.Request.Files.Count > 0)
        //            {
        //                ImageResponse imgResponse = FileHelper.GetImage(HttpContext.Current.Request.Files, "Image", "uploads/users", 400, 400, true);
        //                if (imgResponse.IsSuccess == true)
        //                {
        //                    obj = new ApiSignup()
        //                    {
        //                        Email = HttpContext.Current.Request.Form["Email"],
        //                        Name = HttpContext.Current.Request.Form["Name"],
        //                        Password = HttpContext.Current.Request.Form["Password"],
        //                        UserImage = imgResponse.ThumbnailURL,
        //                        Latitude = HttpContext.Current.Request.Form["Latitude"],
        //                        Longitude = HttpContext.Current.Request.Form["Longitude"],
        //                        DeviceType = HttpContext.Current.Request.Form["DeviceType"],
        //                        DeviceToken = HttpContext.Current.Request.Form["DeviceToken"]

        //                    };




        //                }
        //                else
        //                {
        //                    obj = new ApiSignup()
        //                    {
        //                        Email = HttpContext.Current.Request.Form["Email"],
        //                        Name = HttpContext.Current.Request.Form["Name"],
        //                        Password = HttpContext.Current.Request.Form["Password"],
        //                        UserImage = "",
        //                        Latitude = HttpContext.Current.Request.Form["Latitude"],
        //                        Longitude = HttpContext.Current.Request.Form["Longitude"],
        //                        DeviceType = HttpContext.Current.Request.Form["DeviceType"],
        //                        DeviceToken = HttpContext.Current.Request.Form["DeviceToken"]

        //                    };
        //                }


        //            }
        //            else
        //            {
        //                obj = new ApiSignup()
        //                {
        //                    Email = HttpContext.Current.Request.Form["Email"],
        //                    Name = HttpContext.Current.Request.Form["Name"],
        //                    Password = HttpContext.Current.Request.Form["Password"],
        //                    UserImage = "",
        //                    Latitude = HttpContext.Current.Request.Form["Latitude"],
        //                    Longitude = HttpContext.Current.Request.Form["Longitude"],
        //                    DeviceType = HttpContext.Current.Request.Form["DeviceType"],
        //                    DeviceToken = HttpContext.Current.Request.Form["DeviceToken"]

        //                };
        //            }
        //            var response = new AccountLogic().SignupApi(obj);
        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return JsonResponse.GetResponse(Enums.ResponseCode.Exception, Enums.ResponseCode.Exception);
        //    }
        //}
        //[Route("api/User/signinwithfacebook")]
        //[HttpPost]
        //public ApiResponse Signinwithfacebook(ApiSigninFromFacebook param)
        //{
        //    if (param != null)
        //    {
        //        if (ModelState.IsValid)
        //        {

        //            ImageResponse responseImage = FileHelper.SaveImageByUrl(param.ImageUrl);
        //            var obj = new AccountLogic();
        //            if(responseImage.IsSuccess)
        //                 param.ImageUrl = responseImage.ImageURL;

        //            ApiResponse response = obj.SigninApiWithFacebook(param);
        //            return response;
        //        }
        //        else
        //        {
        //            return JsonResponse.GetResponse(Enums.ResponseCode.Failure, ModelState.Values.FirstOrDefault().Errors[0].ErrorMessage);
        //        }
        //    }
        //    else
        //    {
        //        return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "Invalid request");
        //    }
        //}
        //[Route("api/User/signin")]
        //[HttpPost]
        //public ApiResponse Signin(ApiSignin param)
        //{
        //    if (param != null)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var obj = new AccountLogic();
        //            ApiResponse response = obj.SigninApi(param);
        //            return response;
        //        }
        //        else
        //        {
        //            return JsonResponse.GetResponse(Enums.ResponseCode.Failure, ModelState.Values.FirstOrDefault().Errors[0].ErrorMessage);
        //        }
        //    }
        //    else
        //    {
        //        return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "Invalid request");
        //    }
        //}
        //[HttpPost]
        //[Route("api/User/getforgetpassword")]
        //public ApiResponse GetForgetPassword(dynamic data)
        //{
        //    try
        //    {
        //        var obj = new AccountLogic();
        //        return obj.GetForgotPassword(data);
        //    }
        //    catch (Exception ex)
        //    {

        //        return JsonResponse.GetResponse(Enums.ResponseCode.Exception, Enums.ResponseCode.Exception);
        //    }

        //}

        //[Route("api/User/GetUserInformationById/{UserId}")]
        //[HttpGet]
        //public ApiResponse GetUserInformationById(int UserId)
        //{
        //    try
        //    {
        //        var obj = new AccountLogic();

        //        return JsonResponse.GetResponse(Enums.ResponseCode.Success, obj.GetUserById(UserId, "detail"));
        //    }
        //    catch (Exception ex)
        //    {

        //        return JsonResponse.GetResponse(Enums.ResponseCode.Exception, Enums.ResponseCode.Exception);

        //    }

        //}
        ////[Route("api/User/ChangePassword")]
        ////[HttpPut]
        ////public ApiResponse ChangePassword(InputChangePassword input)
        ////{
        ////    try
        ////    {
        ////        var obj = new AccountLogic();
        ////        var response=obj.ChangePassword(input);
        ////        return response;
        ////    }
        ////    catch (Exception ex)
        ////    {

        ////        return JsonResponse.GetResponse(Enums.ResponseCode.Exception, Enums.ResponseCode.Exception);

        ////    }

        ////}
        //[HttpPost]
        //[Route("api/User/UpdateUserInformation")]
        //public ApiResponse UpdateUserInformation()
        //{
        //    try
        //    {

        //        if (HttpContext.Current.Request.Form["Email"] == null && HttpContext.Current.Request.Form["Email"] == "")
        //        {
        //            return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "Email cannot be null or empty");
        //        }
        //        if (HttpContext.Current.Request.Form["Name"] == null && HttpContext.Current.Request.Form["Name"] == "")
        //        {
        //            return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "User name cannot be null or empty");
        //        }
        //        if (HttpContext.Current.Request.Form["UserId"] == null && HttpContext.Current.Request.Form["UserId"] == "")
        //        {
        //            return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "User Id cannot be null or empty");
        //        }
        //        else
        //        {
        //            ApiSignup obj=new ApiSignup();
        //            obj.UserId = Convert.ToInt32(HttpContext.Current.Request.Form["UserId"]);
        //            obj.Email = HttpContext.Current.Request.Form["Email"];
        //            obj.Name = HttpContext.Current.Request.Form["Name"];
        //            if (HttpContext.Current.Request.Files.Count > 0)
        //            {
        //                ImageResponse imgResponse = FileHelper.GetImage(HttpContext.Current.Request.Files, "Image", "uploads/users", 400, 400, true);
        //                if (imgResponse.IsSuccess == true)
        //                        obj.UserImage = imgResponse.ThumbnailURL;
        //            }

        //            if (HttpContext.Current.Request.Form["Password"] != null && HttpContext.Current.Request.Form["Password"] != "")
        //                obj.Password = HttpContext.Current.Request.Form["Password"];


        //            var response = new AccountLogic().UpdateUserInformation(obj);
        //            return response;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return JsonResponse.GetResponse(Enums.ResponseCode.Exception, Enums.ResponseCode.Exception);
        //    }
        //}

        BusinessCoreController controller = new BusinessCoreController();
        [HttpGet]
        [Route("api/GetUserById")]
        public ApiResponse GetUserById(int userId)
        {
            DataSet user = controller.GetUserById(userId);
            user.Tables[0].TableName = "USERS";
            if (user.Tables[0].Rows.Count > 0)
            {
                user.Tables[0].TableName = "USERS";
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);
        }
        [HttpPost]
        [Route("api/authenticateUser")]
        // POST api/<controller>
        public HttpResponseMessage authenticateUser(HttpRequestMessage request, UserDto u)
        {
            try
            {
                DataSetDto dataSetDto = new DataSetDto();
                UsersLogic usersLogic = new UsersLogic();
                string username = u.USERNAME.Trim();
                string password = u.PASSWORD.Trim();

                if (string.IsNullOrEmpty(u.USER_DEVICE_ID))
                {
                    dataSetDto.Response.Code = (int)HttpStatusCode.NotFound;
                    dataSetDto.Response.Message = "Please provide your mobile device ID";
                    dataSetDto.Response.Data = null;
                    return request.CreateResponse(HttpStatusCode.Forbidden, dataSetDto);
                }

                USER user = controller.AuthenticateUser(username, password,u.USER_TYPE_ID);
                if (user != null)
                {
                    usersLogic.CheckDeviceFCMToken(u, user);
                    //user.Tables[0].TableName = "USERS";
                    // SessionManager.SetUserSession(int.Parse(user.Tables[0].Rows[0][0].ToString()));
                    //SessionManager.SetUsernameSession(user.Tables[0].Rows[0][1].ToString());
                    //if (u.DEVICE_ID == null || u.DEVICE_ID.Equals(user.DEVICE_ID) == false)
                    //{
                    //    controller.updateDeviceId(u.DEVICE_ID == null ? DateTime.Now.ToString() : u.DEVICE_ID, user.USER_ID.ToString());

                    //}
                    //USER u2 = controller.AuthenticateUser(username, password);
                    //u2.Tables[0].TableName = "USERS";
                    //return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);
                    dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                    dataSetDto.Response.Message = "Success";
                    dataSetDto.Response.Data = user;
                    return request.CreateResponse(HttpStatusCode.OK, dataSetDto);

                }
                else
                {
                    //return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);
                    dataSetDto.Response.Code = (int)HttpStatusCode.NotFound;
                    dataSetDto.Response.Message = "User not found";
                    dataSetDto.Response.Data = null;
                    return request.CreateResponse(HttpStatusCode.NotFound, dataSetDto);
                }
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, ex.Message + ex.InnerException);
            }
        }

        [HttpPost]
        [Route("api/authenticateUserByMobile")]
        // POST api/<controller>
        public HttpResponseMessage authenticateUserByMobile(HttpRequestMessage request, UserDto u)
        {
            DataSetDto dataSetDto = new DataSetDto();
            UsersLogic usersLogic = new UsersLogic();
            try
            {
                string mobile = u.MOBILE_NO.Trim();
                string password = u.PASSWORD.Trim();
                //int userTypeID = 0;
                //DataSet ds = controller.GetUserType(u.USER_TYPE_NAME);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    userTypeID = (int)ds.Tables[0].Rows[0]["USER_TYPE_ID"];
                //}
                //DataSet user = controller.authenticateUserByMobile(mobile);
                //if (user.Tables[0].Rows.Count > 0)
                //{
                //    user.Tables[0].TableName = "USERS";
                //   // SessionManager.SetUserSession(int.Parse(user.Tables[0].Rows[0][0].ToString()));
                //  //  SessionManager.SetUsernameSession(user.Tables[0].Rows[0][1].ToString());
                //    if (u.DEVICE_ID == null || u.DEVICE_ID.Equals(user.Tables[0].Rows[0]["DEVICE_ID"].ToString()) == false)
                //    {
                //        controller.updateDeviceId(u.DEVICE_ID == null ? DateTime.Now.ToString() : u.DEVICE_ID, user.Tables[0].Rows[0][0].ToString());

                //    }
                //    //DataSet u2 = controller.AuthenticateUser(mobile, password);
                //    DataSet u2 = controller.authenticateUserByMobile(mobile);
                //    u2.Tables[0].TableName = "USERS";
                //    return JsonResponse.GetResponse(Enums.ResponseCode.Success, u2);

                //}
                if (string.IsNullOrEmpty(u.USER_DEVICE_ID))
                {
                    dataSetDto.Response.Code = (int)HttpStatusCode.NotFound;
                    dataSetDto.Response.Message = "Please provide your mobile device ID";
                    dataSetDto.Response.Data = null;
                    return request.CreateResponse(HttpStatusCode.Forbidden, dataSetDto);
                }

                USER user = controller.authenticateUserByMobile(mobile, u.USER_TYPE_ID);
                if (user != null)
                {
                    //if (u.USER_DEVICE_ID == null || u.USER_DEVICE_ID != user.DEVICE_ID)
                    //{
                    //    controller.updateDeviceId(u.USER_DEVICE_ID == null ? DateTime.Now.ToString() : u.USER_DEVICE_ID, user.USER_ID.ToString());
                    //}
                    usersLogic.CheckDeviceFCMToken(u, user);

                    if (u.PASSWORD != user.PASSWORD)
                    {
                        dataSetDto.Response.Code = (int)HttpStatusCode.Forbidden;
                        dataSetDto.Response.Message = "The password you entered is incorrect";
                        dataSetDto.Response.Data = null;
                        return request.CreateResponse(HttpStatusCode.Forbidden, dataSetDto);
                        //return JsonResponse.GetResponse(Enums.ResponseCode.InCorrectPassword, user);
                    }

                    dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                    dataSetDto.Response.Message = "Success";
                    dataSetDto.Response.Data = user;
                    return request.CreateResponse(HttpStatusCode.OK, dataSetDto);
                    //DataSet u2 = controller.AuthenticateUser(mobile, password);
                    //DataSet u2 = controller.authenticateUserByMobile(mobile);
                    //u2.Tables[0].TableName = "USERS";
                }
                else
                {
                    dataSetDto.Response.Code = (int)HttpStatusCode.NotFound;
                    dataSetDto.Response.Message = "User not found";
                    dataSetDto.Response.Data = null;
                    //return new HttpResponseMessage(HttpStatusCode.NotModified);
                    return request.CreateResponse(HttpStatusCode.NotFound, dataSetDto);
                }
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, ex.Message + ex.InnerException);
            }
        }

        [HttpPost]
        [Route("api/RegisterCustomer")]
        // POST api/<controller>
        public ApiResponse RegisterCustomer(USER u)
        {
            try
            {
                //int result = controller.RegisterCustomer(u);
                u.IS_AVAILABLE = 1;
                DataSet user = controller.RegisterCustomer(u);
                if (user.Tables[0].Rows.Count > 0)
                {
                    user.Tables[0].TableName = "USERS";
                    //SessionManager.SetUserSession(int.Parse(user.Tables[0].Rows[0][0].ToString()));
                    //SessionManager.SetUsernameSession(user.Tables[0].Rows[0][1].ToString());
                    return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

                }
                else
                    return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);
            }
            catch (Exception ex)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.Message + ex.InnerException);
            }
        }
        [HttpPost]
        [Route("api/RegisterUser")]
        // POST api/<controller>
        public HttpResponseMessage RegisterUser(HttpRequestMessage request, UserRegisterDto u)
        {
            DataSetDto dataSetDto = new DataSetDto();
            try
            {
                //int result = controller.RegisterCustomer(u);
                u.IS_AVAILABLE = 1;
                USER user = controller.RegisterUser(u);
                if (user != null /*&& user.Tables[0].Rows.Count > 0*/)
                {
                    dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                    dataSetDto.Response.Message = "Success";
                    dataSetDto.Response.Data = user;
                    return request.CreateResponse(HttpStatusCode.OK, dataSetDto);
                    //user.Tables[0].TableName = "USERS";
                    //SessionManager.SetUserSession(int.Parse(user.Tables[0].Rows[0][0].ToString()));
                    //SessionManager.SetUsernameSession(user.Tables[0].Rows[0][1].ToString());
                    //return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

                }
                else
                {
                    dataSetDto.Response.Code = (int)HttpStatusCode.NotFound;
                    dataSetDto.Response.Message = "User already exists";
                    dataSetDto.Response.Data = null;
                    return request.CreateResponse(HttpStatusCode.OK, dataSetDto);
                    //return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);
                }
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, ex.Message + ex.InnerException);
                //return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.Message + ex.InnerException);
            }
        }
        [HttpPost]
        [Route("api/saveCustomer")]
        // POST api/<controller>
        public ApiResponse saveCustomer(TEMP_CUSTOMERS customer)
        {
            try
            {
                DataSet user = controller.saveCustomer(customer);
                if (user.Tables[0].Rows.Count > 0)
                {
                    user.Tables[0].TableName = "TEMP_USERS";
                    //SessionManager.SetUserSession(int.Parse(user.Tables[0].Rows[0][0].ToString()));
                    //SessionManager.SetUsernameSession(user.Tables[0].Rows[0][1].ToString());
                    return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

                }
                else
                    return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);
            }
            catch (Exception ex)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.Message);
            }
        }
        public ApiResponse saveNewAddress(USER_ADDRESSES customer)
        {
            try
            {
                if (customer.USER_ID == null)
                {
                    return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, "");
                }
                USER_ADDRESSES u = controller.saveNewAddress(customer);
                if (u != null)
                {
                    return JsonResponse.GetResponse(Enums.ResponseCode.Success, u);

                }
                else
                    return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, u);
            }
            catch (Exception ex)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/GetAllAddressesByCustomerId")]
        public ApiResponse GetAllAddressesByCustomerId(int userId)
        {
            DataSet addresses = controller.GetAllAddressesByCustomerId(userId);
            addresses.Tables[0].TableName = "USER_ADDRESSES";
            if (addresses.Tables[0].Rows.Count > 0)
            {
                addresses.Tables[0].TableName = "USER_ADDRESSES";
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, addresses);

            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, addresses);
        }
        [HttpGet]
        [Route("api/ifMobileExist")]
        public ApiResponse ifMobileExist(string mobile)
        {
            DataSet users = controller.ifMobileExist(mobile);
            return JsonResponse.GetResponse(Enums.ResponseCode.Success, users);
        }
        [HttpGet]
        [Route("api/ifUserNameExist")]
        public ApiResponse ifUserNameExist(string uname)
        {
            DataSet users = controller.ifUserNameExist(uname);
            return JsonResponse.GetResponse(Enums.ResponseCode.Success, users);
        }
        [HttpGet]
        [Route("api/ifUserNameExist2")]
        public ApiResponse ifUserNameExist2(string uname, string deviceId)
        {
            DataSet user = controller.ifUserNameExist(uname);
            user.Tables[0].TableName = "USERS";

            if (user.Tables[0].Rows.Count > 0)
            {
                controller.updateDeviceId(deviceId == null ? DateTime.Now.ToString() : deviceId, user.Tables[0].Rows[0][0].ToString());
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);
        }
        [HttpGet]
        [Route("api/ifUserMobileExist2")]
        public ApiResponse ifUserMobileExist2(string mobile, string deviceId, string email = "")
        {
            DataSet user = controller.ifMobileExist(mobile, email);
            if (user == null)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, "Not Exist.");
            }
            user.Tables[0].TableName = "USERS";

            if (user.Tables[0].Rows.Count > 0)
            {
                controller.updateDeviceId(deviceId == null ? DateTime.Now.ToString() : deviceId, user.Tables[0].Rows[0][0].ToString());
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);


        }
        [HttpPost]
        [Route("api/authenticateWebUser")]
        public ApiResponse authenticateWebUser(USER u)
        {
            USER user = controller.AuthenticateUser(u.USERNAME, u.PASSWORD);
            if (user != null)
            {
                //user.Tables[0].TableName = "USERS";
                // SessionManager.SetUserSession(int.Parse(user.Tables[0].Rows[0][0].ToString()));
                // SessionManager.SetUsernameSession(user.Tables[0].Rows[0][1].ToString());
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.Unauthorized, user);
        }
        [HttpPost]
        [Route("api/saveWebUser")]
        public ApiResponse saveWebUser(USER u)
        {
            u.IS_AVAILABLE = 1;
            DataSet user = controller.RegisterCustomer(u);
            if (user.Tables[0].Rows.Count > 0)
            {
                user.Tables[0].TableName = "USERS";
                //  SessionManager.SetUserSession(int.Parse(user.Tables[0].Rows[0][0].ToString()));
                //  SessionManager.SetUsernameSession(user.Tables[0].Rows[0][1].ToString());
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);
        }
        [HttpPost]
        [Route("api/editProfile")]
        public HttpResponseMessage editProfile(HttpRequestMessage request, UserProfileDto u)
        {
            UsersLogic usersLogic = new UsersLogic();
            DataSetDto dataSetDto = usersLogic.UpdateUserProfile(u);
            return request.CreateResponse(HttpStatusCode.OK, dataSetDto);
            //DataSet user = controller.editProfile(u);
            //if (user.Tables[0].Rows.Count > 0)
            //{
            //    return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

            //}
            //else
            //    return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);
        }
        [HttpPost]
        [Route("api/editAddress")]
        public ApiResponse editAddress(USER_ADDRESSES u)
        {
            DataSet user = controller.editAddress(u);
            if (user.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);
        }
        [HttpPost]
        [Route("api/deleteAddress")]
        public ApiResponse deleteAddress(USER_ADDRESSES u)
        {
            DataSet user = controller.deleteAddress(u);
            if (user.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);
        }
        [HttpPost]
        [Route("api/addFavouriteProducts")]
        public ApiResponse addFavouriteProducts(USER_FAVOURITES u)
        {
            USER_FAVOURITES user = controller.addFavouriteProducts(u);
            if (user != null)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);
        }

        [HttpPost]
        [Route("api/markFavouriteProducts")]
        public HttpResponseMessage markFavouriteProducts(HttpRequestMessage request, USER_FAVOURITES u)
        {
            FavouriteProductLogic logic = new FavouriteProductLogic();

            return request.CreateResponse(HttpStatusCode.OK, logic.AddUserFavouriteProducts(u));

            //USER_FAVOURITES user = controller.addFavouriteProducts(u);
            //if (user != null)
            //{
            //    return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

            //}
            //else
            //    return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);
        }

        [HttpPost]
        [Route("api/deleteFavouriteProducts")]
        public ApiResponse deleteFavouriteProducts(USER_FAVOURITES u)
        {
            try
            {
                DataSet user = controller.deleteFavouriteProducts(u);
                if (user.Tables[0].Rows.Count > 0)
                {
                    return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

                }
                else
                    return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, user);
            }
            catch (Exception ex)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.Message);
            }
        }
        [HttpPost]
        [Route("api/getAllFavourites")]
        public HttpResponseMessage getAllFavourites(HttpRequestMessage request, USER_FAVOURITES u)
        {
            //var response = objlogic.GetUserFavouriteProducts(u);
            //if (response.Response.Data != null)
            //{
            //    return request.CreateResponse(HttpStatusCode.OK, response);
            //}
            //else
            //{
            //    return request.CreateResponse(HttpStatusCode.BadRequest, response);
            //}
            FavouriteProductLogic logic = new FavouriteProductLogic();

            return request.CreateResponse(HttpStatusCode.OK, logic.GetUserFavouriteProducts(u));
        }


        [HttpPost]
        [Route("api/getSpecificFavourites")]
        public ApiResponse getSpecificFavourites(USER_FAVOURITES u)
        {
            DataSet user = controller.getSpecificFavourites(u);
            user.Tables[0].TableName = "PRODUCTS";

            if (user.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, "");
        }
        [HttpPost]
        [Route("api/resetPassword")]
        public ApiResponse resetPassword(USER u)
        {
            try
            {

                DataSet user = controller.resetPassword(u);
                if (user.Tables[0].Rows.Count > 0)
                {
                    user.Tables[0].TableName = "USERS";
                    // SessionManager.SetUserSession(int.Parse(user.Tables[0].Rows[0][0].ToString()));
                    //  SessionManager.SetUsernameSession(user.Tables[0].Rows[0][1].ToString());
                    if (u.DEVICE_ID == null || u.DEVICE_ID.Equals(user.Tables[0].Rows[0]["DEVICE_ID"].ToString()) == false)
                    {
                        controller.updateDeviceId(u.DEVICE_ID == null ? DateTime.Now.ToString() : u.DEVICE_ID, user.Tables[0].Rows[0][0].ToString());

                    }

                    return JsonResponse.GetResponse(Enums.ResponseCode.Success, user);

                }
                else
                    return JsonResponse.GetResponse(Enums.ResponseCode.Unauthorized, user);
            }
            catch (Exception ex)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.InnerException + ex.Message);
            }
        }
        [HttpPost]
        [Route("api/FacebookLogin")]
        public HttpResponseMessage FacebookLogin(HttpRequestMessage request, UserDto u)
        {
            DataSetDto dataSetDto = new DataSetDto();
            UsersLogic objlogic = new UsersLogic();

            if (string.IsNullOrEmpty(u.FaceBookId))
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.Forbidden;
                dataSetDto.Response.Message = "FacebookId is required";
                dataSetDto.Response.Data = null;
                return request.CreateResponse(HttpStatusCode.Forbidden, dataSetDto);
                //return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "FacebookId is required.");
            }

            if (string.IsNullOrEmpty(u.EMAIL))
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.Forbidden;
                dataSetDto.Response.Message = "Email is required";
                dataSetDto.Response.Data = null;
                return request.CreateResponse(HttpStatusCode.Forbidden, dataSetDto);
                //return JsonResponse.GetResponse(Enums.ResponseCode.Failure, "FacebookId is required.");
            }
            return request.CreateResponse(HttpStatusCode.OK, objlogic.FacebookLogin(u));
            //return objlogic.FacebookLogin(u);
        }

        [HttpPost]
        [Route("api/DeleteUser")]
        public HttpResponseMessage DeleteUser(HttpRequestMessage request, USER u)
        {
            DataSetDto dataSetDto = new DataSetDto();
            UsersLogic objlogic = new UsersLogic();

            //var result = objlogic.DeleteUser(u);

            //if (result != null)
            //{
            //    dataSetDto.Response.Code = (int)HttpStatusCode.OK;
            //    dataSetDto.Response.Message = "User deleted sucessfully";
            //    dataSetDto.Response.Data = result;
            //}
            //else
            //{
            //    dataSetDto.Response.Code = (int)HttpStatusCode.BadRequest;
            //    dataSetDto.Response.Message = "Unable to delete user";
            //    dataSetDto.Response.Data = null;
            //}
            return request.CreateResponse(HttpStatusCode.OK, dataSetDto);
        }

        [HttpPost]
        [Route("api/IPhoneLogin")]
        public ApiResponse IPhoneLogin(USER u)
        {
            UsersLogic objlogic = new UsersLogic();
            return objlogic.IPhoneLogin(u);
        }

        [HttpPost]
        [Route("api/LogOut")]
        public HttpResponseMessage LogOut(HttpRequestMessage request, UserDto u)
        {
            UsersLogic objlogic = new UsersLogic();
            return request.CreateResponse(HttpStatusCode.OK, objlogic.LogOut(u));
        }
    }
}
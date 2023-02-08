using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.Common
{
    public class JsonResponse
    {
        public static ApiResponse GetResponse(Enums.ResponseCode responseMessage, object data)
        {
            ApiResponse response = new ApiResponse();
            response.Response = new Dictionary<string, object>();
            response.Response.Add("Code", Numerics.GetInt(responseMessage));
            switch (responseMessage)
            {
                case Enums.ResponseCode.Success:
                    response.Response.Add("Message", "Success");
                    break;
                case Enums.ResponseCode.Failure:
                    response.Response.Add("Message", data);
                    break;
                case Enums.ResponseCode.Unauthorized:
                    response.Response.Add("Message", "Unauthorized");
                    break;
                case Enums.ResponseCode.Exception:
                    response.Response.Add("Message", "Exception: " + data);
                    break;
                case Enums.ResponseCode.Info:
                    response.Response.Add("Message", data);
                    break;
                case Enums.ResponseCode.NotExists:
                    response.Response.Add("Message", "Not Found");
                    break; 
                case Enums.ResponseCode.InCorrectPassword:
                    response.Response.Add("Message", "The password you entered is incorrect");
                    break;
            }
            var d = new Dictionary<string, object>();
            d.Add("Response", data);
            if ((responseMessage == Enums.ResponseCode.Success || responseMessage == Enums.ResponseCode.NotExists) && data != null)
            {
                response.Response.Add("Data", data);
            }
            return response;
        }
        public static string GetResponseString(Enums.ResponseCode responseMessage)
        {
            return JsonConvert.SerializeObject(GetResponse(Enums.ResponseCode.Unauthorized, null));
        }
        public static ApiResponse GetResponseModel(Enums.ResponseCode responseMessage, dynamic data, string KeyName = "Message")
        {
            ApiResponse response = new ApiResponse();
            response.Response = new Dictionary<string, object>();
            response.Response.Add("Code", Numerics.GetInt(responseMessage));

            string _errors = "";
            foreach (var state in data)
            {
                foreach (var error in state.Value.Errors)
                {
                    if (error.ErrorMessage != "")
                    {
                        _errors = _errors + error.ErrorMessage + " \n ";
                    }

                    if (error.Exception != null && error.Exception.Message != "")
                    {
                        if (error.Exception.Message.ToString().Contains("Required property"))
                        {
                            string _MyMessage = error.Exception.Message.ToString().Substring(0, error.Exception.Message.ToString().IndexOf(",")).Replace("Required property '", "The ").Replace("' not found in JSON. Path ''", " field is required.");
                            _errors = _errors + _MyMessage + " \n ";
                        }
                        else
                        {
                            _errors = _errors + error.Exception.Message + " \n ";
                        }
                    }
                }
            }



            switch (responseMessage)
            {
                case Enums.ResponseCode.Success:
                    response.Response.Add("Message", "Success");
                    break;
                case Enums.ResponseCode.Failure:
                    response.Response.Add("Message", _errors);
                    break;
                case Enums.ResponseCode.Unauthorized:
                    response.Response.Add("Message", "Unauthorized");
                    break;
                //case ResponseCode.BadRequest:
                //    response.Response.Add("Message", " The request is invalid.");
                //    response.Response.Add("ModelState", data);
                //    break;

                case Enums.ResponseCode.Exception:
                    response.Response.Add("Message", "Exception: " + data);
                    break;
                case Enums.ResponseCode.Info:
                    response.Response.Add("Message", _errors);
                    response.Response.Add("DataObject", new Dictionary<string, object>());
                    break;
            }

            if (responseMessage == Enums.ResponseCode.Success && data != null)
            {
                response.Response.Add(KeyName, data);
            }
            return response;
        }
    }
    public class ApiResponse
    {
        public Dictionary<string, object> Response { get; set; }
    }
}

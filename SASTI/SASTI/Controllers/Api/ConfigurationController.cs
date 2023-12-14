using System.Web.Http;
using System.Data;
using SASTI.Common;
using System.Net.Http;
using SASTI.Models.Dto;
using System.Net;

namespace SASTI.Controllers.Api
{
    public class ConfigurationController : ApiController
    {
        [HttpGet]
        [Route("api/GetConfiguration")]
        public HttpResponseMessage GetConfiguration(HttpRequestMessage request)
        {
            ConfigurationDto configurationDto = new ConfigurationDto()
            {
                DeliveryFee = 100,
                BaseUrl = "https://api.sachigrocery.pk/"
            };

            DataSetDto dataSetDto = new DataSetDto();
            dataSetDto.Response.Code = (int)HttpStatusCode.OK;
            dataSetDto.Response.Message = "Success";
            dataSetDto.Response.Data = configurationDto;
            return request.CreateResponse(HttpStatusCode.OK, dataSetDto);
        }
    }
}

using SASTI.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.BusinessLayer
{
    public class CouponLogic : AbstractFactory
    {
        public DataSetDto GetAllCoupons()
        {
            DataSetDto dataSetDto = new DataSetDto();
            var result = _coupon.Repository.GetAll();

            dataSetDto.Response.Code = (int)HttpStatusCode.OK;
            dataSetDto.Response.Message = "Success";
            dataSetDto.Response.Data = result;

            return dataSetDto;
        }

        public DataSetDto GetCouponById(int couponId)
        {
            DataSetDto dataSetDto = new DataSetDto();
            var result = _coupon.Repository.GetById(couponId);

            if (result != null)
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = result;
            }
            else
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.NotFound;
                dataSetDto.Response.Message = "Not Found";
                dataSetDto.Response.Data = result;
            }

            return dataSetDto;
        }
    }
}

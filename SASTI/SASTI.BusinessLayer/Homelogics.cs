
using SASTI.Common;
using SASTI.Models;
using SASTI.Models.Dto;
using SASTI.Models.Models;
using SASTI.Models.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SASTI.BusinessLayer
{
    public class Homelogics : AbstractFactory
    {

        public DataSetDto LoadHomeData(int PageIndex, int PageSize)
        {
            DataSetDto dataSetDto = new DataSetDto();
            try
            {
                LoadHomeDataResponse res = new LoadHomeDataResponse();
                var query = (from pro in _products.Repository.GetAll(x => x.IS_ACTIVE == true).Skip((PageIndex - 1) * PageSize).Take(PageSize)
                             select new ProductResponse()
                             {
                                 AVG_COST = pro.AVG_COST,
                                 BRAND = pro.BRAND,
                                 CATEGORY_ID = pro.CATEGORY_ID,
                                 COLOR = pro.COLOR,
                                 CREATED_BY = pro.CREATED_BY,
                                 CREATED_DATE = pro.CREATED_DATE,
                                 DESCRIPTION = pro.DESCRIPTION,
                                 DEVICE_TYPE = pro.DEVICE_TYPE,
                                 DISCOUNT = pro.DISCOUNT,
                                 DISCOUNT_PERCENTAGE = pro.DISCOUNT_PERCENTAGE,
                                 FLAVOR = pro.FLAVOR,
                                 HAS_IMAGE = pro.HAS_IMAGE,
                                 HAS_THUMBNAIL_IMAGE = pro.HAS_THUMBNAIL_IMAGE,
                                 IMPORTED = pro.IMPORTED,
                                 IS_ACTIVE = pro.IS_ACTIVE,
                                 IS_EXEMPTED = pro.IS_EXEMPTED,
                                 IS_FEATURED = pro.IS_FEATURED,
                                 MODIFIED_BY = pro.MODIFIED_BY,
                                 MODIFIED_DATE = pro.MODIFIED_DATE,
                                 NAME = pro.NAME,
                                 OLD_PRODUCT_ID = pro.OLD_PRODUCT_ID,
                                 PACKING = pro.PACKING,
                                 PRICE = pro.PRICE,
                                 PRICE2 = pro.PRICE2,
                                 PRODUCT_ID = pro.PRODUCT_ID,
                                 PRODUCT_NAME_URL = pro.PRODUCT_NAME_URL,
                                 SEC_SUB_CATEGORY_A = pro.SEC_SUB_CATEGORY_A,
                                 SEC_SUB_CATEGORY_B = pro.SEC_SUB_CATEGORY_B,
                                 SUB_CATEGORY_ID = pro.SUB_CATEGORY_ID,
                                 TAG = pro.TAG,
                                 THRESHOLD = pro.THRESHOLD,
                                 TYPES = pro.TYPES,
                                 UNIT_OF_MEASUREMENT = pro.UNIT_OF_MEASUREMENT,
                                 VENDOR_ID = pro.VENDOR_ID,
                                 IS_FAVOURITE = pro.IS_FAVOURITE,
                                 ProductImages = (from pi in _productImages.Repository.GetAll(x => x.PRODUCT_ID == pro.PRODUCT_ID).ToList()
                                                  select new PRODUCT_IMAGES()
                                                  {
                                                      PRODUCT_ID = pro.PRODUCT_ID,
                                                      ADMIN_IMAGE_PATH = pi.ADMIN_IMAGE_PATH,
                                                      ADMIN_THUMNAIL_IMAGE_PATH = pi.ADMIN_THUMNAIL_IMAGE_PATH,
                                                      CHAARSU_IMAGE_PATH = pi.CHAARSU_IMAGE_PATH,
                                                      CHAARSU_THUMBNAIL_PATH = pi.CHAARSU_THUMBNAIL_PATH,
                                                      PRODUCT_IMAGE_ID = pi.PRODUCT_IMAGE_ID
                                                  }).ToList(),
                                 Barcode = GetProductBarcode(pro)
                             }).ToList();

                res.HotProducts = query;
                res.Groups = _group.Repository.GetAll();
                res.Banners = _banner.Repository.GetAll().Where(x => x.IsActive == true && x.BannerType == "Mobile").ToList();

                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = res;
                //Dictionary<string, object> resposne = new Dictionary<string, object>();
                //var objProudcts = _products.Repository.GetAll(x => x.IS_ACTIVE == true && x.IS_FEATURED == true).Skip((PageIndex - 1) * PageSize).Take(PageSize);
                //resposne.Add("HotProducts", objProudcts);
                //var objProudctImages = _productImages.Repository.GetAll().Skip((PageIndex - 1) * PageSize).Take(PageSize);
                //resposne.Add("HotProductImages", objProudctImages);
                //var objGroups = _group.Repository.GetAll();
                //resposne.Add("Groups", objGroups);
                //var objBanners = _banner.Repository.GetAll(x => x.BannerType == "Mobile");
                //resposne.Add("Banners", objBanners);

                //return JsonResponse.GetResponse(Enums.ResponseCode.Success, res);
                return dataSetDto;
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

        private BARCODE GetProductBarcode(PRODUCT pro)
        {
            BARCODE barcode = null;

            barcode = _barcode.Repository.FirstOrDefault(x => x.ITEM_CODE == pro.OLD_PRODUCT_ID && x.bDEFAULT == true && x.IsActive == true);
            if (barcode == null)
            {
                barcode = _barcode.Repository.FirstOrDefault(x => x.ITEM_CODE == pro.PRODUCT_ID && x.bDEFAULT == true && x.IsActive == true);
            }
            return barcode;
        }
    }
}

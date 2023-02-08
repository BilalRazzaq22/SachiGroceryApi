using SASTI.BusinessLayer.ViewModel;
using SASTI.Common;
using SASTI.Models;
using SASTI.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.BusinessLayer
{
    public class FavouriteProductLogic : AbstractFactory
    {
        public DataSetDto AddUserFavouriteProducts(USER_FAVOURITES u)
        {
            var user = _userFavouriteProduct.Repository.FirstOrDefault(x => x.USER_ID == u.USER_ID && x.PRODUCT_ID == u.PRODUCT_ID);
            if (user == null)
            {
                _userFavouriteProduct.Repository.Add(u);
            }
            else
            {
                user.PRODUCT_ID = u.PRODUCT_ID;
                user.IS_FAVOURITE = u.IS_FAVOURITE;
                user.USER_ID = u.USER_ID;
                _userFavouriteProduct.Repository.Update(user);
            }

            var pro1 = _products.Repository.FirstOrDefault(x => x.PRODUCT_ID == u.PRODUCT_ID);
            pro1.IS_FAVOURITE = u.IS_FAVOURITE;
            _products.Repository.Update(pro1);

            return GetUserFavouriteProducts(u);
        }

        public DataSetDto GetUserFavouriteProducts(USER_FAVOURITES u)
        {
            UserFavouriteProductResponse response = new UserFavouriteProductResponse();
            var result = new List<USER_FAVOURITES>();
            if(u.PRODUCT_ID != null)
            {
                result = _userFavouriteProduct.Repository.GetAll().Where(x => x.USER_ID == u.USER_ID && x.PRODUCT_ID == u.PRODUCT_ID).ToList();
            }
            else
            {
                result = _userFavouriteProduct.Repository.GetAll().Where(x => x.USER_ID == u.USER_ID).ToList();
            }
             
            foreach (var item in result)
            {
                if (item.PRODUCT_ID != null)
                {
                    PRODUCT pro = _products.Repository.FirstOrDefault(x => x.PRODUCT_ID == item.PRODUCT_ID);
                    if (pro != null)
                    {
                        response.Products.Add(new ProductModel()
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
                        });
                    }
                }
            }



            DataSetDto dataSetDto = new DataSetDto();
            dataSetDto.Response.Code = (int)HttpStatusCode.OK;
            dataSetDto.Response.Message = "Success";
            dataSetDto.Response.Data = response;

            return dataSetDto;
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

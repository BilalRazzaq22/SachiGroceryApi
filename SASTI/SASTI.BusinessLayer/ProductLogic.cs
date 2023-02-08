
using SASTI.BusinessLayer.ViewModel;
using SASTI.Common;
using SASTI.Models;
using SASTI.Models.Dto;
using SASTI.Models.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SASTI.BusinessLayer
{
    public class ProductLogic : AbstractFactory
    {

        public DataSetDto ProductListData(int PageSize, int GroupId)
        {
            DataSetDto dataSetDto = new DataSetDto();
            try
            {
                Dictionary<string, object> resposne = new Dictionary<string, object>();
                var objCat = _cateogry.Repository.GetAll(x => x.IsActive == true && x.GROUP_ID == GroupId);
                //List<ProductDataModel> responseList = new List<ProductDataModel>();
                ProductDataModel response = new ProductDataModel();
                if (objCat?.Count > 0)
                {
                    foreach (var cat in objCat)
                    {
                        List<SubCatModel> subCatList = new List<SubCatModel>();
                        var objSubCat = _subCategory.Repository.GetAll(x => x.IsActive == true && x.CATEGORY_ID == cat.CATEGORY_ID);
                        if (objSubCat?.Count > 0)
                        {

                            foreach (var subcat in objSubCat)
                            {
                                //List<ProductModel> productList = new List<ProductModel>();
                                //var objProduct = _products.Repository.GetAll(x => x.IS_ACTIVE == true && x.SUB_CATEGORY_ID == subcat.SUB_CATEGORY_ID).Take(PageSize).ToList();
                                //if (objProduct?.Count > 0)
                                //{

                                //    foreach (var pro in objProduct)
                                //    {
                                //        var objProductImage = _productImages.Repository.Get(x => x.PRODUCT_ID == pro.PRODUCT_ID);
                                //        productList.Add(new ProductModel()
                                //        {
                                //            AVG_COST=pro.AVG_COST,
                                //            DESCRIPTION=pro.DESCRIPTION,
                                //            DISCOUNT=pro.DISCOUNT,
                                //            DISCOUNT_PERCENTAGE=pro.DISCOUNT_PERCENTAGE,
                                //            ImageUrl= objProductImage==null?"": objProductImage.CHAARSU_IMAGE_PATH,
                                //            NAME=pro.NAME,
                                //            PRICE=pro.PRICE,
                                //            PRICE2=pro.PRICE2,
                                //            PRODUCT_ID=pro.PRODUCT_ID
                                //        });
                                //    }
                                //}

                                subCatList.Add(new SubCatModel()
                                {
                                    SUB_CATEGORY_ID = subcat.SUB_CATEGORY_ID,
                                    DESCRIPTION = subcat.DESCRIPTION,
                                    APP_IMAGE_PATH = subcat.APP_IMAGE_PATH,
                                    CATEGORY_ID = subcat.CATEGORY_ID
                                });

                            }
                        }

                        //responseList.Add(new ProductDataModel()
                        //{
                        //    DESCRIPTION=cat.DESCRIPTION,
                        //    CATEGORY_ID=cat.CATEGORY_ID,
                        //    subCatList= subCatList,
                        //    GROUP_ID=cat.GROUP_ID
                        //});
                        response.Categories.Add(new CatModel
                        {
                            DESCRIPTION = cat.DESCRIPTION,
                            CATEGORY_ID = cat.CATEGORY_ID,
                            GROUP_ID = cat.GROUP_ID,
                            subCatList = subCatList
                        });
                    }
                }

                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = response;
                return dataSetDto;
                //return JsonResponse.GetResponse(Enums.ResponseCode.Success, response);

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

        public DataSetDto LoadMoreProduct(int PageIndex, int PageSize, int SubCategory)
        {
            DataSetDto dataSetDto = new DataSetDto();
            try
            {
                int skipPage = (PageIndex - 1) * PageSize;
                //List<ProductModel> productList = new List<ProductModel>();
                List<PRODUCT> products = _products.Repository.GetAll(x => x.IS_ACTIVE == true && x.SUB_CATEGORY_ID == SubCategory).ToList();
                int totalPages = (products.Count + PageSize - 1) / PageSize;
                ProductResponse response = new ProductResponse();
                int PageSizePossible = 0;
                var objProduct = products.Skip(skipPage).Take(PageSize).ToList();
                response.PageIndex = PageIndex;
                response.PageSize = PageSize;
                response.TotalPages = totalPages;
                if (totalPages > (PageIndex + 1))
                {
                    response.NextPage = PageIndex + 1;
                }
                //response.NextPage = (totalPages > (PageIndex + 1)) ? PageIndex + 1 : Convert.ToInt32("");
                if (objProduct?.Count > 0)
                {
                    foreach (var pro in objProduct)
                    {
                        var objProductImage = _productImages.Repository.Get(x => x.PRODUCT_ID == pro.PRODUCT_ID);
                        response.Products.Add(new ProductModel()
                        {
                            //AVG_COST = pro.AVG_COST,
                            //DESCRIPTION = pro.DESCRIPTION,
                            //DISCOUNT = pro.DISCOUNT,
                            //DISCOUNT_PERCENTAGE = pro.DISCOUNT_PERCENTAGE,
                            ////ImageUrl = objProductImage == null ? "" : objProductImage.CHAARSU_IMAGE_PATH,
                            //NAME = pro.NAME,
                            //PRICE = pro.PRICE,
                            //PRICE2 = pro.PRICE2,
                            //PRODUCT_ID = pro.PRODUCT_ID,
                            //PACKING = pro.PACKING

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


                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = response;
                return dataSetDto;
                //return JsonResponse.GetResponse(Enums.ResponseCode.Success, response);

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

        public DataSetDto GetProductByName(DataSet ds)
        {
            DataSetDto dataSetDto = new DataSetDto();
            SearchProductResponse response = new SearchProductResponse();
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].AsEnumerable())
                    {
                        ProductModel proResponse = new ProductModel();
                        proResponse.AVG_COST = (dataRow.Field<decimal?>("AVG_COST") != null) ? dataRow.Field<decimal?>("AVG_COST") : null;
                        proResponse.BRAND = (dataRow.Field<string>("BRAND") != null) ? dataRow.Field<string>("BRAND") : null;
                        proResponse.CATEGORY_ID = (dataRow.Field<int?>("CATEGORY_ID") != null) ? dataRow.Field<int?>("CATEGORY_ID") : null;
                        proResponse.COLOR = (dataRow.Field<string>("COLOR") != null) ? dataRow.Field<string>("COLOR") : null;
                        proResponse.CREATED_BY = (dataRow.Field<int?>("CREATED_BY") != null) ? dataRow.Field<int?>("CREATED_BY") : null;
                        proResponse.CREATED_DATE = (dataRow.Field<DateTime?>("CREATED_DATE") != null) ? dataRow.Field<DateTime?>("CREATED_DATE") : null;
                        proResponse.DESCRIPTION = (dataRow.Field<string>("DESCRIPTION") != null) ? dataRow.Field<string>("DESCRIPTION") : null;
                        proResponse.DEVICE_TYPE = (dataRow.Field<string>("DEVICE_TYPE") != null) ? dataRow.Field<string>("DEVICE_TYPE") : null;
                        proResponse.DISCOUNT = (dataRow.Field<double?>("DISCOUNT") != null) ? dataRow.Field<double?>("DISCOUNT") : null;
                        proResponse.DISCOUNT_PERCENTAGE = (dataRow.Field<int?>("DISCOUNT_PERCENTAGE") != null) ? dataRow.Field<int?>("DISCOUNT_PERCENTAGE") : null;
                        proResponse.FLAVOR = (dataRow.Field<string>("FLAVOR") != null) ? dataRow.Field<string>("FLAVOR") : null;
                        proResponse.HAS_IMAGE = (dataRow.Field<bool?>("HAS_IMAGE") != null) ? dataRow.Field<bool?>("HAS_IMAGE") : null;
                        proResponse.HAS_THUMBNAIL_IMAGE = (dataRow.Field<bool?>("HAS_THUMBNAIL_IMAGE") != null) ? dataRow.Field<bool?>("HAS_THUMBNAIL_IMAGE") : null;
                        proResponse.IMPORTED = (dataRow.Field<bool?>("IMPORTED") != null) ? dataRow.Field<bool?>("IMPORTED") : null;
                        proResponse.IS_ACTIVE = (dataRow.Field<bool?>("IS_ACTIVE") != null) ? dataRow.Field<bool?>("IS_ACTIVE") : null;
                        proResponse.IS_EXEMPTED = (dataRow.Field<bool?>("IS_EXEMPTED") != null) ? dataRow.Field<bool?>("IS_EXEMPTED") : null;
                        proResponse.IS_FEATURED = (dataRow.Field<bool?>("IS_FEATURED") != null) ? dataRow.Field<bool?>("IS_FEATURED") : null;
                        proResponse.MODIFIED_BY = (dataRow.Field<int?>("MODIFIED_BY") != null) ? dataRow.Field<int?>("MODIFIED_BY") : null;
                        proResponse.MODIFIED_DATE = (dataRow.Field<DateTime?>("MODIFIED_DATE") != null) ? dataRow.Field<DateTime?>("MODIFIED_DATE") : null;
                        proResponse.NAME = (dataRow.Field<string>("NAME") != null) ? dataRow.Field<string>("NAME") : null;
                        proResponse.OLD_PRODUCT_ID = (dataRow.Field<int?>("OLD_PRODUCT_ID") != null) ? dataRow.Field<int?>("OLD_PRODUCT_ID") : null;
                        proResponse.PACKING = (dataRow.Field<string>("PACKING") != null) ? dataRow.Field<string>("PACKING") : null;
                        proResponse.PRICE = (dataRow.Field<decimal?>("PRICE") != null) ? dataRow.Field<decimal?>("PRICE") : null;
                        proResponse.PRICE2 = (dataRow.Field<int?>("PRICE2") != null) ? dataRow.Field<int?>("PRICE2") : null;
                        proResponse.PRODUCT_ID = dataRow.Field<int>("PRODUCT_ID");
                        proResponse.PRODUCT_NAME_URL = (dataRow.Field<string>("PRODUCT_NAME_URL") != null) ? dataRow.Field<string>("PRODUCT_NAME_URL") : null;
                        proResponse.SEC_SUB_CATEGORY_A = (dataRow.Field<int?>("SEC_SUB_CATEGORY_A") != null) ? dataRow.Field<int?>("SEC_SUB_CATEGORY_A") : null;
                        proResponse.SEC_SUB_CATEGORY_B = (dataRow.Field<int?>("SEC_SUB_CATEGORY_B") != null) ? dataRow.Field<int?>("SEC_SUB_CATEGORY_B") : null;
                        proResponse.SUB_CATEGORY_ID = (dataRow.Field<int?>("SUB_CATEGORY_ID") != null) ? dataRow.Field<int?>("SUB_CATEGORY_ID") : null;
                        proResponse.TAG = (dataRow.Field<string>("TAG") != null) ? dataRow.Field<string>("TAG") : null;
                        proResponse.THRESHOLD = (dataRow.Field<int?>("THRESHOLD") != null) ? dataRow.Field<int?>("THRESHOLD") : null;
                        proResponse.TYPES = (dataRow.Field<string>("TYPES") != null) ? dataRow.Field<string>("TYPES") : null;
                        proResponse.UNIT_OF_MEASUREMENT = (dataRow.Field<string>("UNIT_OF_MEASUREMENT") != null) ? dataRow.Field<string>("UNIT_OF_MEASUREMENT") : null;
                        proResponse.VENDOR_ID = (dataRow.Field<int?>("VENDOR_ID") != null) ? dataRow.Field<int?>("VENDOR_ID") : null;
                        proResponse.IS_FAVOURITE = (dataRow.Field<bool?>("IS_FAVOURITE") != null) ? dataRow.Field<bool?>("IS_FAVOURITE") : null;
                        PRODUCT_IMAGES pImg = new PRODUCT_IMAGES()
                        {
                            PRODUCT_ID = (dataRow.Field<int?>("PRODUCT_ID") != null) ? dataRow.Field<int?>("PRODUCT_ID") : null,
                            ADMIN_IMAGE_PATH = (dataRow.Field<string>("ADMIN_IMAGE_PATH") != null) ? dataRow.Field<string>("ADMIN_IMAGE_PATH") : null,
                            ADMIN_THUMNAIL_IMAGE_PATH = (dataRow.Field<string>("ADMIN_THUMNAIL_IMAGE_PATH") != null) ? dataRow.Field<string>("ADMIN_THUMNAIL_IMAGE_PATH") : null,
                            CHAARSU_IMAGE_PATH = (dataRow.Field<string>("CHAARSU_IMAGE_PATH") != null) ? dataRow.Field<string>("CHAARSU_IMAGE_PATH") : null,
                            CHAARSU_THUMBNAIL_PATH = (dataRow.Field<string>("CHAARSU_THUMBNAIL_PATH") != null) ? dataRow.Field<string>("CHAARSU_THUMBNAIL_PATH") : null,
                            PRODUCT_IMAGE_ID = dataRow.Field<int>("PRODUCT_IMAGE_ID")
                        };

                        //if (dict.ContainsKey(proResponse.PRODUCT_ID))
                        //{
                        //    dict.Add(proResponse.PRODUCT_ID, pImg);
                        //}
                        //else
                        //{
                        //    dict.Add(proResponse.PRODUCT_ID, pImg);
                        //}


                        BARCODE barcode = new BARCODE()
                        {
                            BARTYPE = dataRow.Field<short>("BARTYPE"),
                            BAR_CODE = dataRow.Field<string>("BAR_CODE"),
                            bDEFAULT = dataRow.Field<bool>("bDEFAULT"),
                            bRandomDisc = dataRow.Field<bool>("bRandomDisc"),
                            CDATE = dataRow.Field<DateTime>("CDATE"),
                            CUSER = dataRow.Field<int>("CUSER"),
                            DEALER_PRICE1 = dataRow.Field<decimal>("DEALER_PRICE1"),
                            DEALER_PRICE2 = dataRow.Field<decimal>("DEALER_PRICE2"),
                            DISC = dataRow.Field<decimal>("DISC"),
                            DiscEndDate = dataRow.Field<DateTime>("DiscEndDate"),
                            DiscEndTime = dataRow.Field<DateTime>("DiscEndTime"),
                            DiscStartDate = dataRow.Field<DateTime>("DiscStartDate"),
                            DiscStartTime = dataRow.Field<DateTime>("DiscStartTime"),
                            DISC_TYPE = dataRow.Field<short>("DISC_TYPE"),
                            IsActive = dataRow.Field<bool>("IsActive"),
                            ITEM_CODE = dataRow.Field<int>("ITEM_CODE"),
                            LOCNO = dataRow.Field<short>("LOCNO"),
                            LOYALTY_DISC = dataRow.Field<decimal>("LOYALTY_DISC"),
                            PACK_CODE = dataRow.Field<short>("PACK_CODE"),
                            MDATE = dataRow.Field<DateTime>("MDATE"),
                            MUSER = dataRow.Field<int>("MUSER"),
                            PACK_DESC = dataRow.Field<string>("PACK_DESC"),
                            RF_MARGIN = dataRow.Field<decimal>("RF_MARGIN"),
                            STAFF_DISC = dataRow.Field<decimal>("STAFF_DISC"),
                            UNIT = dataRow.Field<decimal>("UNIT"),
                            UNIT_PRICE = dataRow.Field<decimal>("UNIT_PRICE")
                        };

                        proResponse.Barcode = barcode;


                        
                            proResponse.ProductImages.Add(pImg);
                        response.Products.Add(proResponse);
                    }

                }

                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = response;
                return dataSetDto;
            }
            catch (Exception ex)
            {
                LogHelper.CreateLog(ex);
                dataSetDto.Response.Code = (int)HttpStatusCode.BadRequest;
                dataSetDto.Response.Message = ex.Message;
                dataSetDto.Response.Data = null;
                return dataSetDto;
            }
        }

        //public DataSetDto GetProductByName(string name)
        //{
        //    DataSetDto dataSetDto = new DataSetDto();
        //    try
        //    {
        //        List<PRODUCT> products = _products.Repository.GetAll(x => x.NAME.Contains(name) && x.IS_ACTIVE == true).ToList();
        //        SearchProductResponse response = new SearchProductResponse();
        //        if (products.Count > 0)
        //        {
        //            foreach (var pro in products)
        //            {
        //                var objProductImage = _productImages.Repository.Get(x => x.PRODUCT_ID == pro.PRODUCT_ID);
        //                response.Products.Add(new ProductModel()
        //                {
        //                    //AVG_COST = pro.AVG_COST,
        //                    //DESCRIPTION = pro.DESCRIPTION,
        //                    //DISCOUNT = pro.DISCOUNT,
        //                    //DISCOUNT_PERCENTAGE = pro.DISCOUNT_PERCENTAGE,
        //                    ////ImageUrl = objProductImage == null ? "" : objProductImage.CHAARSU_IMAGE_PATH,
        //                    //NAME = pro.NAME,
        //                    //PRICE = pro.PRICE,
        //                    //PRICE2 = pro.PRICE2,
        //                    //PRODUCT_ID = pro.PRODUCT_ID,
        //                    //PACKING = pro.PACKING

        //                    AVG_COST = pro.AVG_COST,
        //                    BRAND = pro.BRAND,
        //                    CATEGORY_ID = pro.CATEGORY_ID,
        //                    COLOR = pro.COLOR,
        //                    CREATED_BY = pro.CREATED_BY,
        //                    CREATED_DATE = pro.CREATED_DATE,
        //                    DESCRIPTION = pro.DESCRIPTION,
        //                    DEVICE_TYPE = pro.DEVICE_TYPE,
        //                    DISCOUNT = pro.DISCOUNT,
        //                    DISCOUNT_PERCENTAGE = pro.DISCOUNT_PERCENTAGE,
        //                    FLAVOR = pro.FLAVOR,
        //                    HAS_IMAGE = pro.HAS_IMAGE,
        //                    HAS_THUMBNAIL_IMAGE = pro.HAS_THUMBNAIL_IMAGE,
        //                    IMPORTED = pro.IMPORTED,
        //                    IS_ACTIVE = pro.IS_ACTIVE,
        //                    IS_EXEMPTED = pro.IS_EXEMPTED,
        //                    IS_FEATURED = pro.IS_FEATURED,
        //                    MODIFIED_BY = pro.MODIFIED_BY,
        //                    MODIFIED_DATE = pro.MODIFIED_DATE,
        //                    NAME = pro.NAME,
        //                    OLD_PRODUCT_ID = pro.OLD_PRODUCT_ID,
        //                    PACKING = pro.PACKING,
        //                    PRICE = pro.PRICE,
        //                    PRICE2 = pro.PRICE2,
        //                    PRODUCT_ID = pro.PRODUCT_ID,
        //                    PRODUCT_NAME_URL = pro.PRODUCT_NAME_URL,
        //                    SEC_SUB_CATEGORY_A = pro.SEC_SUB_CATEGORY_A,
        //                    SEC_SUB_CATEGORY_B = pro.SEC_SUB_CATEGORY_B,
        //                    SUB_CATEGORY_ID = pro.SUB_CATEGORY_ID,
        //                    TAG = pro.TAG,
        //                    THRESHOLD = pro.THRESHOLD,
        //                    TYPES = pro.TYPES,
        //                    UNIT_OF_MEASUREMENT = pro.UNIT_OF_MEASUREMENT,
        //                    VENDOR_ID = pro.VENDOR_ID,
        //                    ProductImages = (from pi in _productImages.Repository.GetAll(x => x.PRODUCT_ID == pro.PRODUCT_ID).ToList()
        //                                     select new PRODUCT_IMAGES()
        //                                     {
        //                                         PRODUCT_ID = pro.PRODUCT_ID,
        //                                         ADMIN_IMAGE_PATH = pi.ADMIN_IMAGE_PATH,
        //                                         ADMIN_THUMNAIL_IMAGE_PATH = pi.ADMIN_THUMNAIL_IMAGE_PATH,
        //                                         CHAARSU_IMAGE_PATH = pi.CHAARSU_IMAGE_PATH,
        //                                         CHAARSU_THUMBNAIL_PATH = pi.CHAARSU_THUMBNAIL_PATH,
        //                                         PRODUCT_IMAGE_ID = pi.PRODUCT_IMAGE_ID
        //                                     }).ToList(),
        //                    Barcode = GetProductBarcode(pro)
        //                });

        //            }
        //        }


        //        dataSetDto.Response.Code = (int)HttpStatusCode.OK;
        //        dataSetDto.Response.Message = "Success";
        //        dataSetDto.Response.Data = response;
        //        return dataSetDto;
        //        //return JsonResponse.GetResponse(Enums.ResponseCode.Success, response);

        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.CreateLog(ex);
        //        dataSetDto.Response.Code = (int)HttpStatusCode.BadRequest;
        //        dataSetDto.Response.Message = ex.Message;
        //        dataSetDto.Response.Data = null;
        //        return dataSetDto;
        //        //return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.Message);
        //    }
        //}

        public DataSetDto SearchProducts(SearchProductDto searchProduct)
        {
            DataSetDto dataSetDto = new DataSetDto();
            List<PRODUCT> products = new List<PRODUCT>();
            try
            {
                string name = searchProduct.Name;
                string packing = searchProduct.Packing;
                string categoryId = searchProduct.CategoryId;
                int catId = Convert.ToInt32(searchProduct.CategoryId);
                if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(packing) && string.IsNullOrEmpty(categoryId))
                {
                    products = _products.Repository.GetAll(x => x.NAME.Contains(name)).ToList();
                }
                else if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(packing) && string.IsNullOrEmpty(categoryId))
                {
                    products = _products.Repository.GetAll(x => x.PACKING.Contains(packing)).ToList();
                }
                else if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(packing) && !string.IsNullOrEmpty(categoryId))
                {
                    products = _products.Repository.GetAll(x => x.CATEGORY_ID == catId).ToList();
                }
                else if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(packing) && string.IsNullOrEmpty(categoryId))
                {
                    products = _products.Repository.GetAll(x => x.NAME.Contains(name) && x.PACKING.Contains(packing)).ToList();
                }
                else if (!string.IsNullOrEmpty(name) && string.IsNullOrEmpty(packing) && !string.IsNullOrEmpty(categoryId))
                {
                    products = _products.Repository.GetAll(x => x.NAME.Contains(name) && x.CATEGORY_ID == catId).ToList();
                }
                else if (string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(packing) && !string.IsNullOrEmpty(categoryId))
                {
                    products = _products.Repository.GetAll(x => x.NAME.Contains(packing) && x.CATEGORY_ID == catId).ToList();
                }
                else if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(packing) && !string.IsNullOrEmpty(categoryId))
                {
                    products = _products.Repository.GetAll(x => x.NAME.Contains(name) && x.PACKING.Contains(packing) && x.CATEGORY_ID == catId).ToList();
                }

                SearchProductResponse response = new SearchProductResponse();
                if (products.Count > 0)
                {
                    foreach (var pro in products)
                    {
                        var objProductImage = _productImages.Repository.Get(x => x.PRODUCT_ID == pro.PRODUCT_ID);
                        response.Products.Add(new ProductModel()
                        {
                            //AVG_COST = pro.AVG_COST,
                            //DESCRIPTION = pro.DESCRIPTION,
                            //DISCOUNT = pro.DISCOUNT,
                            //DISCOUNT_PERCENTAGE = pro.DISCOUNT_PERCENTAGE,
                            ////ImageUrl = objProductImage == null ? "" : objProductImage.CHAARSU_IMAGE_PATH,
                            //NAME = pro.NAME,
                            //PRICE = pro.PRICE,
                            //PRICE2 = pro.PRICE2,
                            //PRODUCT_ID = pro.PRODUCT_ID,
                            //PACKING = pro.PACKING

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


                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = response;
                return dataSetDto;
                //return JsonResponse.GetResponse(Enums.ResponseCode.Success, response);

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

        public DataSetDto GetRecommendedProducts(DataSet ds, int pageindex = 1, int pageSize = 10)
        {
            DataSetDto dataSetDto = new DataSetDto();
            Models.Models.Response.RecommendedProductResponse recommendedProduct = new Models.Models.Response.RecommendedProductResponse();
            Dictionary<int, PRODUCT_IMAGES> dict = new Dictionary<int, PRODUCT_IMAGES>();
            recommendedProduct.PageIndex = pageindex;
            recommendedProduct.PageSize = pageSize;
            recommendedProduct.NextPage = pageindex + 1;
            if (ds.Tables[0].Rows.Count > 0)
                recommendedProduct.TotalPages = Convert.ToInt32(ds.Tables[0].Rows[0].Field<decimal?>("AVG_COST"));

            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].AsEnumerable())
                    {
                        Models.Models.Response.ProductResponse proResponse = new Models.Models.Response.ProductResponse();
                        proResponse.AVG_COST = (dataRow.Field<decimal?>("AVG_COST") != null) ? dataRow.Field<decimal?>("AVG_COST") : null;
                        proResponse.BRAND = (dataRow.Field<string>("BRAND") != null) ? dataRow.Field<string>("BRAND") : null;
                        proResponse.CATEGORY_ID = (dataRow.Field<int?>("CATEGORY_ID") != null) ? dataRow.Field<int?>("CATEGORY_ID") : null;
                        proResponse.COLOR = (dataRow.Field<string>("COLOR") != null) ? dataRow.Field<string>("COLOR") : null;
                        proResponse.CREATED_BY = (dataRow.Field<int?>("CREATED_BY") != null) ? dataRow.Field<int?>("CREATED_BY") : null;
                        proResponse.CREATED_DATE = (dataRow.Field<DateTime?>("CREATED_DATE") != null) ? dataRow.Field<DateTime?>("CREATED_DATE") : null;
                        proResponse.DESCRIPTION = (dataRow.Field<string>("DESCRIPTION") != null) ? dataRow.Field<string>("DESCRIPTION") : null;
                        proResponse.DEVICE_TYPE = (dataRow.Field<string>("DEVICE_TYPE") != null) ? dataRow.Field<string>("DEVICE_TYPE") : null;
                        proResponse.DISCOUNT = (dataRow.Field<double?>("DISCOUNT") != null) ? dataRow.Field<double?>("DISCOUNT") : null;
                        proResponse.DISCOUNT_PERCENTAGE = (dataRow.Field<int?>("DISCOUNT_PERCENTAGE") != null) ? dataRow.Field<int?>("DISCOUNT_PERCENTAGE") : null;
                        proResponse.FLAVOR = (dataRow.Field<string>("FLAVOR") != null) ? dataRow.Field<string>("FLAVOR") : null;
                        proResponse.HAS_IMAGE = (dataRow.Field<bool?>("HAS_IMAGE") != null) ? dataRow.Field<bool?>("HAS_IMAGE") : null;
                        proResponse.HAS_THUMBNAIL_IMAGE = (dataRow.Field<bool?>("HAS_THUMBNAIL_IMAGE") != null) ? dataRow.Field<bool?>("HAS_THUMBNAIL_IMAGE") : null;
                        proResponse.IMPORTED = (dataRow.Field<bool?>("IMPORTED") != null) ? dataRow.Field<bool?>("IMPORTED") : null;
                        proResponse.IS_ACTIVE = (dataRow.Field<bool?>("IS_ACTIVE") != null) ? dataRow.Field<bool?>("IS_ACTIVE") : null;
                        proResponse.IS_EXEMPTED = (dataRow.Field<bool?>("IS_EXEMPTED") != null) ? dataRow.Field<bool?>("IS_EXEMPTED") : null;
                        proResponse.IS_FEATURED = (dataRow.Field<bool?>("IS_FEATURED") != null) ? dataRow.Field<bool?>("IS_FEATURED") : null;
                        proResponse.MODIFIED_BY = (dataRow.Field<int?>("MODIFIED_BY") != null) ? dataRow.Field<int?>("MODIFIED_BY") : null;
                        proResponse.MODIFIED_DATE = (dataRow.Field<DateTime?>("MODIFIED_DATE") != null) ? dataRow.Field<DateTime?>("MODIFIED_DATE") : null;
                        proResponse.NAME = (dataRow.Field<string>("NAME") != null) ? dataRow.Field<string>("NAME") : null;
                        proResponse.OLD_PRODUCT_ID = (dataRow.Field<int?>("OLD_PRODUCT_ID") != null) ? dataRow.Field<int?>("OLD_PRODUCT_ID") : null;
                        proResponse.PACKING = (dataRow.Field<string>("PACKING") != null) ? dataRow.Field<string>("PACKING") : null;
                        proResponse.PRICE = (dataRow.Field<decimal?>("PRICE") != null) ? dataRow.Field<decimal?>("PRICE") : null;
                        proResponse.PRICE2 = (dataRow.Field<int?>("PRICE2") != null) ? dataRow.Field<int?>("PRICE2") : null;
                        proResponse.PRODUCT_ID = dataRow.Field<int>("PRODUCT_ID");
                        proResponse.PRODUCT_NAME_URL = (dataRow.Field<string>("PRODUCT_NAME_URL") != null) ? dataRow.Field<string>("PRODUCT_NAME_URL") : null;
                        proResponse.SEC_SUB_CATEGORY_A = (dataRow.Field<int?>("SEC_SUB_CATEGORY_A") != null) ? dataRow.Field<int?>("SEC_SUB_CATEGORY_A") : null;
                        proResponse.SEC_SUB_CATEGORY_B = (dataRow.Field<int?>("SEC_SUB_CATEGORY_B") != null) ? dataRow.Field<int?>("SEC_SUB_CATEGORY_B") : null;
                        proResponse.SUB_CATEGORY_ID = (dataRow.Field<int?>("SUB_CATEGORY_ID") != null) ? dataRow.Field<int?>("SUB_CATEGORY_ID") : null;
                        proResponse.TAG = (dataRow.Field<string>("TAG") != null) ? dataRow.Field<string>("TAG") : null;
                        proResponse.THRESHOLD = (dataRow.Field<int?>("THRESHOLD") != null) ? dataRow.Field<int?>("THRESHOLD") : null;
                        proResponse.TYPES = (dataRow.Field<string>("TYPES") != null) ? dataRow.Field<string>("TYPES") : null;
                        proResponse.UNIT_OF_MEASUREMENT = (dataRow.Field<string>("UNIT_OF_MEASUREMENT") != null) ? dataRow.Field<string>("UNIT_OF_MEASUREMENT") : null;
                        proResponse.VENDOR_ID = (dataRow.Field<int?>("VENDOR_ID") != null) ? dataRow.Field<int?>("VENDOR_ID") : null;
                        proResponse.IS_FAVOURITE = (dataRow.Field<bool?>("IS_FAVOURITE") != null) ? dataRow.Field<bool?>("IS_FAVOURITE") : null;
                        PRODUCT_IMAGES pImg = new PRODUCT_IMAGES()
                        {
                            PRODUCT_ID = (dataRow.Field<int?>("PRODUCT_ID") != null) ? dataRow.Field<int?>("PRODUCT_ID") : null,
                            ADMIN_IMAGE_PATH = (dataRow.Field<string>("ADMIN_IMAGE_PATH") != null) ? dataRow.Field<string>("ADMIN_IMAGE_PATH") : null,
                            ADMIN_THUMNAIL_IMAGE_PATH = (dataRow.Field<string>("ADMIN_THUMNAIL_IMAGE_PATH") != null) ? dataRow.Field<string>("ADMIN_THUMNAIL_IMAGE_PATH") : null,
                            CHAARSU_IMAGE_PATH = (dataRow.Field<string>("CHAARSU_IMAGE_PATH") != null) ? dataRow.Field<string>("CHAARSU_IMAGE_PATH") : null,
                            CHAARSU_THUMBNAIL_PATH = (dataRow.Field<string>("CHAARSU_THUMBNAIL_PATH") != null) ? dataRow.Field<string>("CHAARSU_THUMBNAIL_PATH") : null,
                            PRODUCT_IMAGE_ID = dataRow.Field<int>("PRODUCT_IMAGE_ID")
                        };

                        //if (dict.ContainsKey(proResponse.PRODUCT_ID))
                        //{
                        //    dict.Add(proResponse.PRODUCT_ID, pImg);
                        //}
                        //else
                        //{
                        //    dict.Add(proResponse.PRODUCT_ID, pImg);
                        //}


                        BARCODE barcode = new BARCODE()
                        {
                            BARTYPE = dataRow.Field<short>("BARTYPE"),
                            BAR_CODE = dataRow.Field<string>("BAR_CODE"),
                            bDEFAULT = dataRow.Field<bool>("bDEFAULT"),
                            bRandomDisc = dataRow.Field<bool>("bRandomDisc"),
                            CDATE = dataRow.Field<DateTime>("CDATE"),
                            CUSER = dataRow.Field<int>("CUSER"),
                            DEALER_PRICE1 = dataRow.Field<decimal>("DEALER_PRICE1"),
                            DEALER_PRICE2 = dataRow.Field<decimal>("DEALER_PRICE2"),
                            DISC = dataRow.Field<decimal>("DISC"),
                            DiscEndDate = dataRow.Field<DateTime>("DiscEndDate"),
                            DiscEndTime = dataRow.Field<DateTime>("DiscEndTime"),
                            DiscStartDate = dataRow.Field<DateTime>("DiscStartDate"),
                            DiscStartTime = dataRow.Field<DateTime>("DiscStartTime"),
                            DISC_TYPE = dataRow.Field<short>("DISC_TYPE"),
                            IsActive = dataRow.Field<bool>("IsActive"),
                            ITEM_CODE = dataRow.Field<int>("ITEM_CODE"),
                            LOCNO = dataRow.Field<short>("LOCNO"),
                            LOYALTY_DISC = dataRow.Field<decimal>("LOYALTY_DISC"),
                            PACK_CODE = dataRow.Field<short>("PACK_CODE"),
                            MDATE = dataRow.Field<DateTime>("MDATE"),
                            MUSER = dataRow.Field<int>("MUSER"),
                            PACK_DESC = dataRow.Field<string>("PACK_DESC"),
                            RF_MARGIN = dataRow.Field<decimal>("RF_MARGIN"),
                            STAFF_DISC = dataRow.Field<decimal>("STAFF_DISC"),
                            UNIT = dataRow.Field<decimal>("UNIT"),
                            UNIT_PRICE = dataRow.Field<decimal>("UNIT_PRICE")
                        };

                        proResponse.Barcode = barcode;


                        if (recommendedProduct.RecommendedProducts.Any(x => x.PRODUCT_ID == proResponse.PRODUCT_ID))
                        {
                            var result = recommendedProduct.RecommendedProducts.FirstOrDefault(x => x.PRODUCT_ID == proResponse.PRODUCT_ID);
                            if (result != null)
                            {
                                if (!result.ProductImages.Any(x => x.PRODUCT_IMAGE_ID == pImg.PRODUCT_IMAGE_ID))
                                {
                                    result.ProductImages.Add(pImg);
                                }
                            }
                        }
                        else
                        {
                            proResponse.ProductImages.Add(pImg);
                            recommendedProduct.RecommendedProducts.Add(proResponse);
                        }
                    }
                }

                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = recommendedProduct;
                return dataSetDto;
            }
            catch (Exception ex)
            {
                LogHelper.CreateLog(ex);
                dataSetDto.Response.Code = (int)HttpStatusCode.BadRequest;
                dataSetDto.Response.Message = ex.Message;
                dataSetDto.Response.Data = null;
                return dataSetDto;
            }
        }

        public DataSetDto GetProductByUrl(string url)
        {
            DataSetDto dataSetDto = new DataSetDto();
            try
            {
                PRODUCT pro = _products.Repository.FirstOrDefault(x => x.PRODUCT_NAME_URL == url);
                SearchProductResponse response = new SearchProductResponse();
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

                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = response;
                return dataSetDto;
            }
            catch (Exception ex)
            {
                LogHelper.CreateLog(ex);
                dataSetDto.Response.Code = (int)HttpStatusCode.BadRequest;
                dataSetDto.Response.Message = ex.Message;
                dataSetDto.Response.Data = null;
                return dataSetDto;
            }
        }
    }
}

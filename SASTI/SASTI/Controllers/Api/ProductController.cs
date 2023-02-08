using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Newtonsoft.Json;
using SASTI.Models;
using SASTI.Controllers;
using SASTI.Common;
using SASTI.BusinessLayer;
using SASTI.Models.Dto;
using System.Drawing.Printing;

namespace SASTI.Controllers
{
    public class ProductController : ApiController
    {
        BusinessCoreController controller = new BusinessCoreController();
        [HttpGet]
        [Route("api/GetProductById")]
        public HttpResponseMessage GetProductById(HttpRequestMessage request,int productId,int bID)
        {
            DataSetDto dataSetDto = new DataSetDto();
            DataSet groups = controller.GetProductById(productId,bID);
            groups.Tables[0].TableName = "PRODUCT";
            if (groups.Tables[0].Rows.Count > 0)
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = groups;
                return request.CreateResponse(HttpStatusCode.OK, dataSetDto);
                //return JsonResponse.GetResponse(Enums.ResponseCode.Success,new AjaxResponse(true, "", true, dataSetDto.Response.Data));
            }
            else
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.NotFound;
                dataSetDto.Response.Message = "Not Found";
                dataSetDto.Response.Data = null;
                return request.CreateResponse(HttpStatusCode.NotFound, dataSetDto);
                // return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, dataSetDto);
            }
        }
        [HttpGet]
        [Route("api/GetProductById2")]
        public ApiResponse GetProductById2(int productId, int bID)
        {
            DataSet groups = controller.GetProductById2(productId, bID);
            groups.Tables[0].TableName = "PRODUCT";
            if (groups.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, groups));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
        [HttpPost]
        [Route("api/GetAllCategoriesByGroupId")]

        public ApiResponse GetAllCategoriesByGroupId(int groupId)
        {
            DataSet categories = controller.GetAllCategoriesByGroupId(groupId);
            if (categories.Tables[0].Rows.Count > 0)
            {

                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, categories));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
         [HttpPost]
        [Route("api/GetAllSubCategoriesByCategoryId")]

        public ApiResponse GetAllSubCategoriesByCategoryId(int categoryId)
        {
            DataSet subcategories = controller.GetAllSubCategoriesByCategoryId(categoryId);
            if (subcategories.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, subcategories));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
        [HttpGet]
        [Route("api/GetAllSubCategoriesByCategoryIds")]
        public ApiResponse GetAllSubCategoriesByCategoryIds(int categoryId)
        {
            DataSet subcategories = controller.GetAllSubCategoriesByCategoryId(categoryId);
            if (subcategories.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, subcategories));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }

        [HttpGet]
        [Route("api/GetAllSubCategoriesByGroupId")]
        public ApiResponse GetAllSubCategoriesByGroupId(int groupId)
        {
            DataSet subcategories = controller.GetAllSubCategoriesByGroupId(groupId);
            if (subcategories.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, subcategories));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }


        //shoaib
        [HttpGet]
        [Route("api/GetAllBrandsByCategoryIds")]
        public ApiResponse GetAllBrandsByCategoryIds(string categoryId)
        {
            DataSet brands = controller.GetAllBrandsByCategoryIds(categoryId==null?"0":categoryId);
            //if (brands.Tables[0].Rows.Count > 0)
            //{

            return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, brands));
            //}
            //else
            //{
            //    return Request.CreateResponse(HttpStatusCode.OK, "");
            //}
        }
        [HttpGet]
        [Route("api/GetAllPackingsBySubCategoryIds")]
        public ApiResponse GetAllPackingsBySubCategoryIds(string subcategoryId)
        {
            DataSet brands = controller.GetAllPackingsBySubCategoryIds(subcategoryId==null?"0":subcategoryId);
            //if (brands.Tables[0].Rows.Count > 0)
            //{

            return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, brands));
            //}
            //else
            //{
            //    return Request.CreateResponse(HttpStatusCode.OK, "");
            //}
        }
        [HttpGet]
        [Route("api/GetAllProductsByBrandsAndCategoryId")]
        public ApiResponse GetAllProductsByBrandsAndCategoryId(string brands, int categoryId,string subcatIds)
        {
            DataSet products = controller.GetAllProductsByBrandsAndCategoryId(brands, categoryId, subcatIds);
            if (products.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, products));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
        [HttpGet]
        [Route("api/GetAllProductsBySubCategoryIdTOP6")]
        public ApiResponse GetAllProductsBySubCategoryIdTOP6(int subCategoryId, int bId)
        {
            DataSet products = controller.GetAllProductsBySubCategoryIdTOP6(subCategoryId, 1);
            if (products.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, products));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }

            //shoaib
         [HttpGet]
        [Route("api/GetAvailableProductByName")]
        public ApiResponse GetAvailableProductByName(int branchId,  string p_name)
        {
            DataSet products = controller.GetAvailableProductByName(branchId,p_name);
            products.Tables[0].TableName = "PRODUCTS";
            if (products.Tables[0].Rows.Count > 0)
            {
                //string json = JsonConvert.SerializeObject(groups, Formatting.Indented);
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, products));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
        [HttpGet]
        [Route("api/updateStock")]
        public ApiResponse updateStock(int branchId, int product_id, int qty)
        {
            int result = controller.updateStock(branchId, product_id,qty);
            //controller.saveTriggerLogs("SynchronizeStock on UPDATE IN PS000");
            if (result > 0)
            {
                controller.saveTriggerLogs("SynchronizeStock on UPDATE IN PS000");
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, result));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
        [HttpGet]
        [Route("api/getStock")]
        public ApiResponse getStock()
        {
            DataSet stock = controller.getStock();
            stock.Tables[0].TableName = "STOCK";
            if (stock.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, stock));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }

        [HttpGet]
        [Route("api/getFeaturedProducts")]
        public ApiResponse getFeaturedProducts(int branch_id)
        {
            DataSet products = controller.getFeaturedProducts(branch_id);
            products.Tables[0].TableName = "PRODUCTS";
            if (products.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, products));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, Enums.ResponseCode.NotExists);
            }
        }
        [HttpGet]
        [Route("api/searchProductsByFilters")]
        public ApiResponse searchProductsByFilters(string name, int min_price, int max_price,int subcategoryId)
        {
            DataSet products = new DataSet();
            DataTable dt = new DataTable();
            products.Tables.Add(dt);
            products.Tables[0].TableName = "PRODUCTS";
            if(subcategoryId <= 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Failure, new AjaxResponse(true, "", true, products));
            }
            if(min_price >= max_price)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Failure, new AjaxResponse(true, "", true, products));
                
            }
            products = controller.searchProductsByFilters(name, min_price, max_price, subcategoryId);
            products.Tables[0].TableName = "PRODUCTS";
            if (products.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, products));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, products);
            }
        }
        [HttpGet]
        [Route("api/searchProducts")]
        public ApiResponse searchProducts(string name, int min_price, int max_price, int branchId)
        {
            DataSet products = new DataSet();
            DataTable dt = new DataTable();
            products.Tables.Add(dt);
            products.Tables[0].TableName = "PRODUCTS";
            if (branchId < 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Failure, new AjaxResponse(true, "", true, products));
            }
            if (min_price >= max_price)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Failure, new AjaxResponse(true, "", true, products));
            }
            products = controller.searchProducts(name, min_price, max_price, branchId);
            products.Tables[0].TableName = "PRODUCTS";
            if (products.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, products));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Failure, new AjaxResponse(true, "", true, products));
            }
 
        }
        [HttpGet]
        [Route("api/updatePriceAndDiscount")]
        public ApiResponse updatePriceAndDiscount(decimal price, int product_id, int disc, int bdefault,int actv,string bcode)
        {
            int result = controller.updatePriceAndDiscount(price, product_id, disc,bdefault,actv,bcode);
            if (result > 0)
            {
                controller.saveTriggerLogs("SynchronizePrice on Update ON BARCODES");

                //string json = JsonConvert.SerializeObject(groups, Formatting.Indented);
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, result));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, new AjaxResponse(true, "", true, result));
            }
        }
        [HttpGet]
        [Route("api/addPriceAndDiscount")]
        public ApiResponse addPriceAndDiscount(int locno, int productid, int packcode, string packdesc, int unit, int bdef, string barcode, decimal price, int disc, int active)
        {
            
            int result = controller.addPriceAndDiscount(locno, productid, packcode, packdesc, unit, bdef, barcode, price, disc, active);
            if (result > 0)
            {
                controller.saveTriggerLogs("SynchronizePrice on Insert ON BARCODES");

                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, result));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, new AjaxResponse(true, "", true, result));
            }
        }
    
        [HttpGet]
        [Route("api/addStock")]
        public ApiResponse addStock(int branchId, int product_id, int qty)
        {
            int result = controller.addStock(branchId, product_id, qty);
            if (result > 0)
            {
                controller.saveTriggerLogs("SynchronizeAddStock on Insert in PS000 ");
                //string json = JsonConvert.SerializeObject(groups, Formatting.Indented);
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, result));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, new AjaxResponse(true, "", true, result));
            }
        }
        [HttpGet]
        [Route("api/addProduct")]
        public ApiResponse addProduct(decimal cost, int product_id, string desc, string desclong, string uom)
        {
            int result = controller.addProduct(cost, product_id, desc, desclong, uom);
            if (result > 0)
            {
                controller.saveTriggerLogs("SynchronizeITEMADD on Insert in Item info ");
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, result));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, new AjaxResponse(true, "", true, result));
            }
        }
        [HttpGet]
        [Route("api/updateProduct")]
        public ApiResponse updateProduct(decimal cost, int product_id, string desc, string desclong, string uom)
        {
            int result = controller.updateProduct(cost, product_id, desc, desclong, uom);
            if (result > 0)
            {
                controller.saveTriggerLogs("SynchronizeITEMUpdate on Update in Item info ");
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, result));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, new AjaxResponse(true, "", true, result));
            }
        }


        [HttpGet]
        [Route("api/getHotProducts")]
        public ApiResponse getHotProducts()
        {
            try
            {
                DataSet products = controller.getHotProducts();
                //products.Tables[0].TableName = "PRODUCTS";
                if (products.Tables[0].Rows.Count > 0)
                {

                    return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, products));
                }
                else
                {
                    return JsonResponse.GetResponse(Enums.ResponseCode.Failure, new AjaxResponse(true, "", true, products));
                }
            }
            catch(Exception ex)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Exception, ex.Message);
            }
        }
        [HttpGet]
        [Route("api/searchAllProducts")]
        public ApiResponse searchAllProducts(string name, int min_price, int max_price, int branchId)
        {
            DataSet products = new DataSet();
            DataTable dt = new DataTable();
            products.Tables.Add(dt);
            products.Tables[0].TableName = "PRODUCTS";
            if (branchId < 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Failure, new AjaxResponse(true, "", true, products));
            }
            if (min_price >= max_price)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Failure, new AjaxResponse(true, "", true, products));
              
            }
            products = controller.searchAllProducts(name, min_price, max_price, branchId);
            products.Tables[0].TableName = "PRODUCTS";
            if (products.Tables[0].Rows.Count > 0)
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, products));
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, new AjaxResponse(true, "", true, products));
            }

        }

        [HttpGet]
        [Route("api/GetProductListData")]
        public HttpResponseMessage GetProductListData(HttpRequestMessage request, int PageSize, int GroupId)
        {
            ProductLogic objlogic = new ProductLogic();
            var response = objlogic.ProductListData(PageSize, GroupId);
            if (response.Response.Data != null)
            {
                return request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

        [HttpGet]
        [Route("api/LoadMoreProducts")]
        public HttpResponseMessage LoadMoreProducts(HttpRequestMessage request, int PageIndex,int PageSize, int SubCategory)
        {
            ProductLogic objlogic = new ProductLogic();
            var response = objlogic.LoadMoreProduct(PageIndex,PageSize, SubCategory);
            if (response.Response.Data != null)
            {
                return request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }
        
        [HttpGet]
        [Route("api/GetProductByName")]
        public HttpResponseMessage GetProductByName(HttpRequestMessage request, string name)
        {
            DataSet ds = controller.getSearchProducts(name);

            ProductLogic objlogic = new ProductLogic();
            var response = objlogic.GetProductByName(ds);  
            if (response.Response.Data != null)
            {
                return request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }
        
        [HttpGet]
        [Route("api/GetProductByUrl")]
        public HttpResponseMessage GetProductByUrl(HttpRequestMessage request, string product_url)
        {
            ProductLogic objlogic = new ProductLogic();
            var response = objlogic.GetProductByUrl(product_url);
            if (response.Response.Data != null)
            {
                return request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

        //[HttpPost]
        //[Route("api/SearchProducts")]
        //public HttpResponseMessage SearchProducts(HttpRequestMessage request, SearchProductDto searchProduct)
        //{
        //    ProductLogic objlogic = new ProductLogic();
        //    var response = objlogic.SearchProducts(searchProduct);
        //    if (response.Response.Data != null)
        //    {
        //        return request.CreateResponse(HttpStatusCode.OK, response);
        //    }
        //    else
        //    {
        //        return request.CreateResponse(HttpStatusCode.BadRequest, response);
        //    }
        //}

        [HttpGet]
        [Route("api/GetRecommendedProducts")]
        public HttpResponseMessage GetRecommendedProducts(HttpRequestMessage request,int pageindex = 1,int pageSize = 10)
        {

            DataSet ds = controller.getRecommendedProducts(pageindex, pageSize);

            ProductLogic objlogic = new ProductLogic();
            var response = objlogic.GetRecommendedProducts(ds,pageindex,pageSize);
            if (response.Response.Data != null)
            {
                return request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Newtonsoft.Json;
using SASTI.Controllers;
using SASTI.DataAccess;
using SASTI.DataCore;
using SASTI.Common;
using SASTI.BusinessLayer;
using SASTI.Models.Dto;

namespace SASTI.Controllers
{
    public class GeneralController : ApiController
    {
        BusinessCoreController controller = new BusinessCoreController();

        [HttpGet]
        [Route("api/GetAllGroups")]
        public ApiResponse GetAllGroups()
        {
            DataSet groups = controller.GetAllGroups();
            groups.Tables[0].TableName = "GROUPS";
            if (groups.Tables[0].Rows.Count > 0)
            {
                //string json = JsonConvert.SerializeObject(groups, Formatting.Indented);
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, groups);
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, groups);
            }
        }
        [HttpGet]
        [Route("api/GetAllCategoriesByGroupId")]
        public ApiResponse GetAllCategoriesByGroupId(int groupId)
        {
            DataSet categories = controller.GetAllCategoriesByGroupId(groupId);
            categories.Tables[0].TableName = "CATEGORIES";
            if (categories.Tables[0].Rows.Count > 0)
            {
                //string json = JsonConvert.SerializeObject(categories, Formatting.Indented);
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, categories);
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, categories);
            }
        }

        [HttpGet]
        [Route("api/GetAllCategories")]
        public HttpResponseMessage GetAllCategories(HttpRequestMessage request)
        {
            DataSetDto dataSetDto = new DataSetDto();
            DataSet categories = controller.GetAllCategories();
            categories.Tables[0].TableName = "CATEGORIES";

            if (categories.Tables[0].Rows.Count > 0)
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = categories;
                return request.CreateResponse(HttpStatusCode.OK, categories);
            }
            else
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.NotFound;
                dataSetDto.Response.Message = "Not Found";
                dataSetDto.Response.Data = null;
                return request.CreateResponse(HttpStatusCode.NotFound, dataSetDto);
            }
        }

        [HttpGet]
        [Route("api/GetAllSubCategoriesByCategoryId")]
        public ApiResponse GetAllSubCategoriesByCategoryId(int categoryId)
        {
            DataSet subcategories = controller.GetAllSubCategoriesByCategoryId(categoryId);
            if (subcategories.Tables[0].Rows.Count > 0)
            {
                subcategories.Tables[0].TableName = "SUB_CATEGORIES";
                //string json = JsonConvert.SerializeObject(subcategories, Formatting.Indented);
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, subcategories);
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, subcategories);
            }
        }
        [HttpGet]
        [Route("api/GetAllProductsBySubCategoryId")]
        public ApiResponse GetAllProductsBySubCategoryId(int subCategoryId, int bId)
        {
            DataSet products = controller.GetAllProductsBySubCategoryId(subCategoryId, bId);
            if (products.Tables[0].Rows.Count > 0)
            {
                products.Tables[0].TableName = "PRODUCTS";
                //string json = JsonConvert.SerializeObject(products, Formatting.Indented);
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, products);
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, products);
            }
        }
        [HttpGet]
        [Route("api/LoadSubCategoriesAndProductsByCategoryId")]
        public ApiResponse LoadSubCategoriesAndProductsByCategoryId(int category)
        {
            DataSet subcategories = controller.GetAllSubCategoriesByCategoryId(category);
            subcategories.Tables[0].TableName = "SUB_CATEGORIES";
            string html = "", prodhtml = "";

            if (subcategories.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in subcategories.Tables[0].Rows)
                {
                    //html = "";
                    prodhtml = "";
                    int noOfCarasouls = 1;
                    int subCategoryId = int.Parse(row[Entities.SUB_CATEGORIES.SUB_CATEGORY_ID].ToString());
                    string carousel_id = "myCarousel" + row[Entities.SUB_CATEGORIES.SUB_CATEGORY_ID].ToString();
                    string block1 = "<div class=\"top-brands detail-title\"><div class=\"after-menu-bar container\"><div class=\"col-md-6 pro-t\"><h2>" + row[Entities.SUB_CATEGORIES.DESCRIPTION].ToString() + "</h2></div><div class=\"col-md-3 pro-s\"><div class=\"input-group stylish-input-group\"><input type=\"text\" class=\"form-control\" placeholder=\"Search\"><span class=\"input-group-addon\"><button type=\"button\"><span class=\"glyphicon glyphicon-search\"></span></button></span></div></div><div class=\"col-md-3 pro-d\"><select class=\"select-o\"><option>Select</option></select></div></div>";
                    string block2 = "<div id=\"thumbnail-slider\" ><div class=\"agile_top_brands_grids\"><div class=\"inner\"><div id=\"" + carousel_id + "\" class=\"carousel slide\"><div class=\"carousel-inner\">";
                    string block3 = "", block4 = "", block5 = "", block6 = "", block7 = "", block8 = "", block9 = "";

                    DataSet products = controller.GetAllProductsBySubCategoryId(subCategoryId, 1);

                    if (products.Tables[0].Rows.Count > 0)
                    {
                        if (products.Tables[0].Rows.Count <= 6)
                        {
                            noOfCarasouls = 1;
                        }
                        else if (products.Tables[0].Rows.Count % 6 == 0)
                        {
                            noOfCarasouls = products.Tables[0].Rows.Count / 6;
                        }
                        else
                        {
                            noOfCarasouls = (products.Tables[0].Rows.Count / 6) + 1;
                        }

                        block3 = "<div class=\"item active\"><div class=\"row-fluid\">";
                        prodhtml = prodhtml + block3;
                        ////if no products
                        //if (products.Tables[0].Rows.Count == 0) 
                        //{
                        //    block4 = "</div></div>";
                        //}
                        //else
                        //{

                        //}
                        int j = 0;
                        foreach (DataRow prow in products.Tables[0].Rows)
                        {

                            if (j % 6 == 0 && j != 0)
                            {
                                //for closing current and opening new row if its last row
                                block4 = "</div></div>";
                                block5 = "<div class=\"item \"><div class=\"row-fluid\">";
                                prodhtml = prodhtml + block4 + block5;
                            }
                            //display products
                            block6 = "<div class=\"col-md-2 abc\"><a href=\"#x\" class=\"\"><img src=\"" + prow[Entities.PRODUCTS.CHAARSU_IMAGE_PATH].ToString() + "\" alt=\"Image\" class=\"pro-img\" /></a><p class=\"detail\">Price: Rs." + prow[Entities.PRODUCTS.PRICE].ToString() + "<br>Name: " + prow[Entities.PRODUCTS.NAME].ToString() + "</p><div class=\"overlay\"><div class=\"snipcart-details top_brand_home_details\"><fieldset><input type=\"button\" name=\"button\" onclick=\" addToCart(" + prow[Entities.PRODUCTS.PRODUCT_ID].ToString() + ") \" value=\"Add to cart\" class=\"button button1 \" /></fieldset></div></div></div>";
                            prodhtml = prodhtml + block6;
                            if (j == products.Tables[0].Rows.Count - 1)
                            {
                                //close if last item
                                block7 = "</div></div>";
                                prodhtml = prodhtml + block7;
                            }

                            j++;
                        }
                    }
                    block8 = "</div><a class=\"left carousel-control\" href=\"" + carousel_id + "\" data-slide=\"prev\">‹</a><a class=\"right carousel-control\" href=\"" + carousel_id + "\" data-slide=\"next\">›</a></div><ol class=\"carousel-indicators\">";
                    if (noOfCarasouls > 0)
                    {
                        for (var k = 0; k < noOfCarasouls; k++)
                        {
                            if (k == 0)
                            {
                                block9 = "<li data-target=\"#" + carousel_id + "\" data-slide-to=\"" + k + "\" class=\"active\" ></li>";
                            }
                            else
                            {

                                block9 = "<li data-target=\"#" + carousel_id + "\" data-slide-to=\"" + k + "\"  ></li>";
                            }
                        }
                    }
                    html = html + block1 + block2 + prodhtml + block8 + block9;
                    html = html + "</ol></div></div></div>";
                    //closing div of first div
                    html = html + "</div>";

                }

                return JsonResponse.GetResponse(Enums.ResponseCode.Success, html);
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, html);
            }
        }
        [HttpGet]
        [Route("api/LoadTopMenu")]
        public ApiResponse LoadTopMenu()
        {
            string html = "";

            DataSet groups = controller.GetAllGroups();
            groups.Tables[0].TableName = "GROUPS";
            if (groups.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < groups.Tables[0].Rows.Count; i++)
                {
                    string innerimage = "";
                    innerimage = groups.Tables[0].Rows[i][Entities.GROUPS.RELATIVE_IMAGE_NAME].ToString();
                    string endchunk = "<div class=\"col-md-3\"><img src=\"" + innerimage + " \" class=\"m_menu_image\" /></div><div class=\"col-md-4 sub-menu-text\"><p>Our philosophy is simple. Focus on one thing<br>and be the best at it. For us, thats blowouts!</p></div></div></li>";

                    string temp = groups.Tables[0].Rows[i][Entities.GROUPS.RELATIVE_IMAGE_NAME].ToString();
                    string[] arr = temp.Split('/');
                    string[] data = arr[1].Split('.');



                    string startchunk = "<li class=\"main\"><a class=\"dropdown-content\" href=\"#\"><img id=\"" + data[0] + "\" src=\"" + groups.Tables[0].Rows[i][Entities.GROUPS.RELATIVE_IMAGE_NAME].ToString() + "\">" + groups.Tables[0].Rows[i][Entities.GROUPS.NAME].ToString() + "  </a><div class=\"megamenu\" aria-hidden=\"true\">";
                    string innerChunk = "<div class=\"col-md-1\"></div><div class=\"col-md-2\"><h4 class=\"sub-menu-title\">" + groups.Tables[0].Rows[i][Entities.GROUPS.NAME].ToString() + "</h4><ul class=\"unstyled\">";
                    int groupId = int.Parse(groups.Tables[0].Rows[i][0].ToString());
                    string leftLi = "", rightLi = "", l_r_end = "</ul></div>", r_start = "<div class=\"col-md-2\"><ul class=\"unstyled us1\">";
                    DataSet categories = controller.GetAllCategoriesByGroupId(groupId);
                    categories.Tables[0].TableName = "CATEGORIES";
                    int count = categories.Tables[0].Rows.Count;
                    for (int j = 0; j < count; j++)
                    {
                        if (j < 6)
                        {
                            leftLi += "<li><a href=\"javascript:void(0);\" onclick = \" setCatId(" + categories.Tables[0].Rows[j][Entities.CATEGORIES.CATEGORY_ID].ToString() + ")\">" + categories.Tables[0].Rows[j][Entities.CATEGORIES.DESCRIPTION].ToString() + "</a></li>";
                        }
                        else
                        {
                            rightLi += "<li><a href=\"javascript:void(0);\" onclick = \" setCatId(" + categories.Tables[0].Rows[j][Entities.CATEGORIES.CATEGORY_ID].ToString() + ")\">" + categories.Tables[0].Rows[j][Entities.CATEGORIES.DESCRIPTION].ToString() + "</a></li>";
                        }
                    }
                    html += startchunk + innerChunk + leftLi + l_r_end + r_start + rightLi + l_r_end + endchunk;
                }
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, html);
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, html);
            }


        }
        [HttpGet]
        [Route("api/showMainSection")]
        public ApiResponse showMainSection(int category)
        {
            DataSet subcategories = controller.GetAllSubCategoriesByCategoryId(category);
            subcategories.Tables[0].TableName = "SUB_CATEGORIES";
            string html = "";
            string closingDiv = "<div class=\"clearfix\"></div><div class=\"w3ls_w3l_banner_nav_right_grid\"></div>";
            string subCatScript = "";
            if (subcategories.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in subcategories.Tables[0].Rows)
                {
                    int subCategoryId = int.Parse(row[Entities.SUB_CATEGORIES.SUB_CATEGORY_ID].ToString());

                    string subCatHtml = "<div class=\"mainsectionsubcats\" id=\"mainsectionsubcats_" + row[Entities.SUB_CATEGORIES.DESCRIPTION].ToString() + "\"><div class=\"after-menu-bar container\"><div class=\"col-md-6 pro-t\"><h2>" + row[Entities.SUB_CATEGORIES.DESCRIPTION].ToString() + "</h2></div><div class=\"col-md-3 pro-s\"></div><div class=\"col-md-3 pro-d\"></div></div>";
                    string prodStartDiv = "<div class=\"top-brands detail-title first-pro row\"><div class=\"agile_top_brands_grids\"><ul class=\"gallery\">";
                    string prodEndDiv = "</div><div class=\"more more_" + subCategoryId + "\"><span class=\"fa m_arrow fa-caret-down\" style=\"font-size:25px\" aria-hidden=\"true\"></span></div></div></div>";
                    string prodDiv = "", defaultImage = "http://admin.chaarsu.pk/Product_images/default.jpg";


                    string endUl = "</ul>";
                    string moreDivStart = "<ul class=\"m_more_products" + subCategoryId + " m_products_style \">";

                    DataSet products6 = controller.GetAllProductsBySubCategoryIdTOP6(subCategoryId, 1);
                    if (products6.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow prow in products6.Tables[0].Rows)
                        {
                            if (prow[Entities.PRODUCTS.CHAARSU_IMAGE_PATH].ToString() == string.Empty)
                                prodDiv += "<li class='productlis' packingfilters=\"" + prow[Entities.PRODUCTS.PACKING].ToString() + "\" filterbrand=\"" + prow[Entities.PRODUCTS.BRAND].ToString() + "\"><div class=\"tile abc\"><img onclick=\" showProductDetail(" + prow[Entities.PRODUCTS.PRODUCT_ID].ToString() + ")\" src=\"" + defaultImage + "\"><p class=\"detail price\">Rs." + float.Parse(prow["UNIT_PRICE"].ToString()).ToString() + "</p><p class=\"detail\">" + prow[Entities.PRODUCTS.NAME].ToString() + "</p><hr class=\"pro-underline\"><div class=\"overlay\"><div class=\"snipcart-details top_brand_home_details\"><fieldset><input type=\"button\" name=\"button\" onclick=\" addToCart(" + prow[Entities.PRODUCTS.PRODUCT_ID].ToString() + ") \"  class=\"button button1 hvr-pulse\" /></fieldset></div></div></div></li>";
                            else
                                prodDiv += "<li class='productlis' packingfilters=\"" + prow[Entities.PRODUCTS.PACKING].ToString() + "\" filterbrand=\"" + prow[Entities.PRODUCTS.BRAND].ToString() + "\"><div class=\"tile abc\"><img onclick=\" showProductDetail(" + prow[Entities.PRODUCTS.PRODUCT_ID].ToString() + ")\" src=\"" + prow[Entities.PRODUCTS.CHAARSU_IMAGE_PATH].ToString() + "\"><p class=\"detail price\">Rs." + float.Parse(prow["UNIT_PRICE"].ToString()).ToString() + "</p><p class=\"detail\">" + prow[Entities.PRODUCTS.NAME].ToString() + "</p><hr class=\"pro-underline\"><div class=\"overlay\"><div class=\"snipcart-details top_brand_home_details\"><fieldset><input type=\"button\" name=\"button\" onclick=\" addToCart(" + prow[Entities.PRODUCTS.PRODUCT_ID].ToString() + ") \"  class=\"button button1 hvr-pulse\" /></fieldset></div></div></div></li>";

                        }
                    }

                    prodDiv += endUl;
                    bool flag = false;
                    DataSet products = controller.GetAllProductsBySubCategoryIdFROM7ONWARDS(subCategoryId, 1);
                    if (products.Tables[0].Rows.Count > 0)
                    {

                        flag = true;
                        prodDiv += moreDivStart;
                        foreach (DataRow prow in products.Tables[0].Rows)
                        {
                            if (prow[Entities.PRODUCTS.CHAARSU_IMAGE_PATH].ToString() == string.Empty)
                                prodDiv += "<li class='productlis' packingfilters=\"" + prow[Entities.PRODUCTS.PACKING].ToString() + "\" filterbrand=\"" + prow[Entities.PRODUCTS.BRAND].ToString() + "\"><div class=\"tile abc\"><img src=\"" + defaultImage + "\"><p class=\"detail price\">" + prow[Entities.PRODUCTS.AVG_COST].ToString() + "</p><p class=\"detail\">" + prow[Entities.PRODUCTS.NAME].ToString() + "</p><hr class=\"pro-underline\"><div class=\"overlay\"><div class=\"snipcart-details top_brand_home_details\"><fieldset><input type=\"button\" name=\"button\" onclick=\" addToCart(" + prow[Entities.PRODUCTS.PRODUCT_ID].ToString() + ") \"  class=\"button button1 hvr-pulse\" /></fieldset></div></div></div></li>";
                            else
                                prodDiv += "<li class='productlis' packingfilters=\"" + prow[Entities.PRODUCTS.PACKING].ToString() + "\" filterbrand=\"" + prow[Entities.PRODUCTS.BRAND].ToString() + "\"><div class=\"tile abc\"><img src=\"" + prow[Entities.PRODUCTS.CHAARSU_IMAGE_PATH].ToString() + "\"><p class=\"detail price\">" + prow[Entities.PRODUCTS.AVG_COST].ToString() + "</p><p class=\"detail\">" + prow[Entities.PRODUCTS.NAME].ToString() + "</p><hr class=\"pro-underline\"><div class=\"overlay\"><div class=\"snipcart-details top_brand_home_details\"><fieldset><input type=\"button\" name=\"button\" onclick=\" addToCart(" + prow[Entities.PRODUCTS.PRODUCT_ID].ToString() + ") \"  class=\"button button1 hvr-pulse\" /></fieldset></div></div></div></li>";

                        }
                    }
                    if (flag)
                        prodDiv += endUl;

                    html += subCatHtml + prodStartDiv + prodDiv + prodEndDiv;
                    //script 
                    subCatScript += "$(\".more_" + subCategoryId + "\").click(function () {$(\".m_more_products" + subCategoryId + "\").show();});";

                }
                string endmostscript = "<script>$(document).ready(function () {$(\".more_products_arrow\").click(function () {$(\".m_more_products\").show();});" + subCatScript + "$(\".hvr-pulse\").click(function () {$(this).css(\"animation-iteration-count\", \"0\");$(this).css(\"opacity\", \"0.5\");$(this).css(\"background\", \"url(images/teal-plus.png) no-repeat 45px 3px\");});$(\".remove-product\").click(function () {});});</script>";

                html += closingDiv + endmostscript;
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, html);
            }
            else
            {
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, html);
            }


        }
        [HttpGet]
        [Route("api/showMainSectionByGroup")]
        public ApiResponse showMainSectionByGroup(int group)
        {
            string html = "";
            DataSet categories = controller.GetAllCategoriesByGroupId(group);
            foreach (DataRow Crow in categories.Tables[0].Rows)
            {
                int CategoryId = int.Parse(Crow[Entities.CATEGORIES.CATEGORY_ID].ToString());
                DataSet subcategories = controller.GetAllSubCategoriesByCategoryId(CategoryId);
                subcategories.Tables[0].TableName = "SUB_CATEGORIES";

                string closingDiv = "<div class=\"clearfix\"></div><div class=\"w3ls_w3l_banner_nav_right_grid\"></div>";
                string subCatScript = "";
                if (subcategories.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in subcategories.Tables[0].Rows)
                    {
                        int subCategoryId = int.Parse(row[Entities.SUB_CATEGORIES.SUB_CATEGORY_ID].ToString());

                        string subCatHtml = "<div class=\"mainsectionsubcats\" id=\"mainsectionsubcats_" + row[Entities.SUB_CATEGORIES.DESCRIPTION].ToString() + "\"><div class=\"after-menu-bar container\"><div class=\"col-md-6 pro-t\"><h2>" + row[Entities.SUB_CATEGORIES.DESCRIPTION].ToString() + "</h2></div><div class=\"col-md-3 pro-s\"></div><div class=\"col-md-3 pro-d\"></div></div>";
                        string prodStartDiv = "<div class=\"top-brands detail-title first-pro row\"><div class=\"agile_top_brands_grids\"><ul class=\"gallery\">";
                        string prodEndDiv = "</div><div class=\"more more_" + subCategoryId + "\"><span class=\"fa m_arrow fa-caret-down\" style=\"font-size:25px\" aria-hidden=\"true\"></span></div></div></div>";
                        string prodDiv = "", defaultImage = "http://admin.chaarsu.pk/Product_images/default.jpg";


                        string endUl = "</ul>";
                        string moreDivStart = "<ul class=\"m_more_products" + subCategoryId + " m_products_style \">";

                        DataSet products6 = controller.GetAllProductsBySubCategoryIdTOP6(subCategoryId, 1);
                        if (products6.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow prow in products6.Tables[0].Rows)
                            {
                                if (prow[Entities.PRODUCTS.CHAARSU_IMAGE_PATH].ToString() == string.Empty)
                                    prodDiv += "<li class='productlis' packingfilters=\"" + prow[Entities.PRODUCTS.PACKING].ToString() + "\" filterbrand=\"" + prow[Entities.PRODUCTS.BRAND].ToString() + "\"><div class=\"tile abc\"><img onclick=\" showProductDetail(" + prow[Entities.PRODUCTS.PRODUCT_ID].ToString() + ")\" src=\"" + defaultImage + "\"><p class=\"detail price\">Rs." + float.Parse(prow["UNIT_PRICE"].ToString()).ToString() + "</p><p class=\"detail\">" + prow[Entities.PRODUCTS.NAME].ToString() + "</p><hr class=\"pro-underline\"><div class=\"overlay\"><div class=\"snipcart-details top_brand_home_details\"><fieldset><input type=\"button\" name=\"button\" onclick=\" addToCart(" + prow[Entities.PRODUCTS.PRODUCT_ID].ToString() + ") \"  class=\"button button1 hvr-pulse\" /></fieldset></div></div></div></li>";
                                else
                                    prodDiv += "<li class='productlis' packingfilters=\"" + prow[Entities.PRODUCTS.PACKING].ToString() + "\" filterbrand=\"" + prow[Entities.PRODUCTS.BRAND].ToString() + "\"><div class=\"tile abc\"><img onclick=\" showProductDetail(" + prow[Entities.PRODUCTS.PRODUCT_ID].ToString() + ")\" src=\"" + prow[Entities.PRODUCTS.CHAARSU_IMAGE_PATH].ToString() + "\"><p class=\"detail price\">Rs." + float.Parse(prow["UNIT_PRICE"].ToString()).ToString() + "</p><p class=\"detail\">" + prow[Entities.PRODUCTS.NAME].ToString() + "</p><hr class=\"pro-underline\"><div class=\"overlay\"><div class=\"snipcart-details top_brand_home_details\"><fieldset><input type=\"button\" name=\"button\" onclick=\" addToCart(" + prow[Entities.PRODUCTS.PRODUCT_ID].ToString() + ") \"  class=\"button button1 hvr-pulse\" /></fieldset></div></div></div></li>";

                            }
                        }

                        prodDiv += endUl;
                        bool flag = false;
                        DataSet products = controller.GetAllProductsBySubCategoryIdFROM7ONWARDS(subCategoryId, 1);
                        if (products.Tables[0].Rows.Count > 0)
                        {

                            flag = true;
                            prodDiv += moreDivStart;
                            foreach (DataRow prow in products.Tables[0].Rows)
                            {
                                if (prow[Entities.PRODUCTS.CHAARSU_IMAGE_PATH].ToString() == string.Empty)
                                    prodDiv += "<li class='productlis' packingfilters=\"" + prow[Entities.PRODUCTS.PACKING].ToString() + "\" filterbrand=\"" + prow[Entities.PRODUCTS.BRAND].ToString() + "\"><div class=\"tile abc\"><img src=\"" + defaultImage + "\"><p class=\"detail price\">" + prow[Entities.PRODUCTS.AVG_COST].ToString() + "</p><p class=\"detail\">" + prow[Entities.PRODUCTS.NAME].ToString() + "</p><hr class=\"pro-underline\"><div class=\"overlay\"><div class=\"snipcart-details top_brand_home_details\"><fieldset><input type=\"button\" name=\"button\" onclick=\" addToCart(" + prow[Entities.PRODUCTS.PRODUCT_ID].ToString() + ") \"  class=\"button button1 hvr-pulse\" /></fieldset></div></div></div></li>";
                                else
                                    prodDiv += "<li class='productlis' packingfilters=\"" + prow[Entities.PRODUCTS.PACKING].ToString() + "\" filterbrand=\"" + prow[Entities.PRODUCTS.BRAND].ToString() + "\"><div class=\"tile abc\"><img src=\"" + prow[Entities.PRODUCTS.CHAARSU_IMAGE_PATH].ToString() + "\"><p class=\"detail price\">" + prow[Entities.PRODUCTS.AVG_COST].ToString() + "</p><p class=\"detail\">" + prow[Entities.PRODUCTS.NAME].ToString() + "</p><hr class=\"pro-underline\"><div class=\"overlay\"><div class=\"snipcart-details top_brand_home_details\"><fieldset><input type=\"button\" name=\"button\" onclick=\" addToCart(" + prow[Entities.PRODUCTS.PRODUCT_ID].ToString() + ") \"  class=\"button button1 hvr-pulse\" /></fieldset></div></div></div></li>";

                            }
                        }
                        if (flag)
                            prodDiv += endUl;

                        html += subCatHtml + prodStartDiv + prodDiv + prodEndDiv;
                        //script 
                        subCatScript += "$(\".more_" + subCategoryId + "\").click(function () {$(\".m_more_products" + subCategoryId + "\").show();});";

                    }
                    string endmostscript = "<script>$(document).ready(function () {$(\".more_products_arrow\").click(function () {$(\".m_more_products\").show();});" + subCatScript + "$(\".hvr-pulse\").click(function () {$(this).css(\"animation-iteration-count\", \"0\");$(this).css(\"opacity\", \"0.5\");$(this).css(\"background\", \"url(images/teal-plus.png) no-repeat 45px 3px\");});$(\".remove-product\").click(function () {});});</script>";

                    html += closingDiv + endmostscript;

                }
                else
                {
                    return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, html);
                }


            }
            return JsonResponse.GetResponse(Enums.ResponseCode.Success, html);




        }
        [HttpGet]
        [Route("api/getNearestBranch")]
        public HttpResponseMessage getNearestBranch(HttpRequestMessage request, double latitude, double longitude)
        {
            DataSetDto dataSetDto = new DataSetDto();
            DataSet branches = controller.getNearestBranch(latitude, longitude);
            if (branches == null)
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.NotFound;
                dataSetDto.Response.Message = "Cannot find branch location, Please try again.";
                dataSetDto.Response.Data = null;
                return request.CreateResponse(HttpStatusCode.NotFound, dataSetDto);
            }
            branches.Tables[0].TableName = "BRANCHES";
            if (branches.Tables[0].Rows.Count > 0)
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = branches;
                return request.CreateResponse(HttpStatusCode.OK, dataSetDto);
            }
            else
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.NotFound;
                dataSetDto.Response.Message = "Not Found";
                dataSetDto.Response.Data = null;
                return request.CreateResponse(HttpStatusCode.NotFound, dataSetDto);
            }

        }
        [HttpGet]
        [Route("api/GetActiveBranches")]
        public ApiResponse GetActiveBranches()
        {
            DataSet branches = controller.GetActiveBranches();
            branches.Tables[0].TableName = "BRANCHES";
            if (branches.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, branches);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, branches);
        }
        [HttpGet]
        [Route("api/GetPaymentModes")]
        public ApiResponse GetPaymentModes()
        {
            DataSet p_modes = controller.GetPaymentModes();
            p_modes.Tables[0].TableName = "GROUPS";
            if (p_modes.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, p_modes);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, p_modes);
        }
        [HttpGet]
        [Route("api/GetAreas")]
        public ApiResponse GetAreas()
        {
            DataSet areas = controller.GetAreas();
            areas.Tables[0].TableName = "AREAS";
            if (areas.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, areas);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, areas);
        }
        [HttpGet]
        [Route("api/getTriggerLogs")]
        public ApiResponse getTriggerLogs()
        {
            DataSet logs = controller.getTriggerLogs();
            logs.Tables[0].TableName = "LOGS";
            if (logs.Tables[0].Rows.Count > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, logs);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, logs);
        }
        [HttpGet]
        [Route("api/test")]
        public ApiResponse test()
        {
            int result = controller.test();
            if (result > 0)
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, result);
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, result);

        }
        [HttpGet]
        [Route("api/testPush")]
        public ApiResponse testPush()
        {
            FCMPushNotification notification = new FCMPushNotification();
            notification.SendNotification("test", "Hi shoaib", "news", "477625648329", "dE9NhbrJxa8:APA91bHQLAeVcGTZ-YdM9kIt-dfQQCGzQPdJ28j0F6UiMoHcdtqF7UEivehYVKQ2feAt-lDn9vaVyjvkTAVLg-1GifhMzVNb1qXlwNR38PEulmoWHW7uXjyaJ7-9TOP5rcqWmTrr42Mz");
            return JsonResponse.GetResponse(Enums.ResponseCode.Success, true);

        }
        [HttpGet]
        [Route("api/GetAllActiveAds")]
        public ApiResponse GetAllActiveAds()
        {
            DataSet products = controller.GetAllActiveAds();
            if (products.Tables[0].Rows.Count > 0)
            {
                string json = JsonConvert.SerializeObject(products, Formatting.Indented);
                return JsonResponse.GetResponse(Enums.ResponseCode.Success, json);
            }
            else
                return JsonResponse.GetResponse(Enums.ResponseCode.NotExists, "");
        }
        [HttpGet]
        [Route("api/setViewCategory")]
        public ApiResponse setViewCategory(int catId)
        {
            //SessionManager.RemoveViewGroupSession();
            //SessionManager.SetViewCategorySession(catId);
            return JsonResponse.GetResponse(Enums.ResponseCode.Success, catId);
        }
        [HttpGet]
        [Route("api/setViewGroup")]
        public ApiResponse setViewGroup(int groupId)
        {
            //SessionManager.SetViewGroupSession(groupId);
            return JsonResponse.GetResponse(Enums.ResponseCode.Success, groupId);
        }
        [HttpGet]
        [Route("api/LoadHomeData")]
        public HttpResponseMessage LoadHomeData(HttpRequestMessage request, int PageIndex, int PageSize)
        {
            Homelogics _logic = new Homelogics();
            var response = _logic.LoadHomeData(PageIndex, PageSize);
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
        [Route("api/getAllCoupons")]
        public HttpResponseMessage getAllCoupons(HttpRequestMessage request)
        {
            CouponLogic couponLogic = new CouponLogic();
            return request.CreateResponse(HttpStatusCode.OK, couponLogic.GetAllCoupons());
        }

        [HttpGet]
        [Route("api/getCouponsById")]
        public HttpResponseMessage getCouponsById(HttpRequestMessage request, int couponId)
        {
            CouponLogic couponLogic = new CouponLogic();
            return request.CreateResponse(HttpStatusCode.OK, couponLogic.GetCouponById(couponId));
        }

        [HttpGet]
        [Route("api/getCouponByPromoCode")]
        public HttpResponseMessage getCouponByPromoCode(HttpRequestMessage request, string promoCode)
        {
            DataSetDto dataSetDto = new DataSetDto();
            DataSet coupon = controller.GetCouponByPromoCode(promoCode);

            if(coupon.Tables[0].Rows.Count > 0)
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.OK;
                dataSetDto.Response.Message = "Success";
                dataSetDto.Response.Data = coupon;
                return request.CreateResponse(HttpStatusCode.OK, dataSetDto);
            }
            else
            {
                dataSetDto.Response.Code = (int)HttpStatusCode.NotFound;
                dataSetDto.Response.Message = "Not Found";
                dataSetDto.Response.Data = null;
                return request.CreateResponse(HttpStatusCode.NotFound, dataSetDto);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SASTI.DataAccess;
using System.Data;
namespace SASTI.DataCore
{
    public class GeneralManager : DABase
    {
        const string QRY_GET_GROUPS = "SELECT * FROM GROUPS";
        const string QRY_GET_CATEGORIES_BY_GROUP_ID = "select * from categories where group_id = {0}";
        const string QRY_GET_CATEGORIES = "SELECT CATEGORY_ID,DESCRIPTION AS CATEGORY_NAME FROM CATEGORIES";
        const string QRY_GET_SUBCATEGORIES_BY_CATEGORY_ID = "select * from sub_categories where category_id = {0}";
        const string QRY_GET_SUBCATEGORIES_BY_GROUP_ID = @"select SC.SUB_CATEGORY_ID,SC.DESCRIPTION from SUB_CATEGORIES SC
                                                            INNER JOIN CATEGORIES C ON SC.CATEGORY_ID = C.CATEGORY_ID
                                                            INNER JOIN GROUPS G ON C.GROUP_ID = G.GROUP_ID 
                                                            WHERE G.GROUP_ID = {0}";


        //        const string QRY_GET_SUBCATEGORIES_AND_PRODUCTS = @"select b.BAR_CODE ,b.UNIT_PRICE,b.DISC, SC.DESCRIPTION AS SUBCATEGORY,pimg.CHAARSU_IMAGE_PATH , P.*,CEILING(p.AVG_COST) as PRICE from PRODUCTS P
        //inner join SUB_CATEGORIES SC on P.SUB_CATEGORY_ID = SC.SUB_CATEGORY_ID 
        //inner join PRODUCT_IMAGES PIMG on P.PRODUCT_ID = PIMG.PRODUCT_ID
        //inner join BARCODES B on P.OLD_PRODUCT_ID = b.ITEM_CODE
        //WHERE SC.SUB_CATEGORY_ID = {0} and b.IsActive = 1 and b.bDEFAULT = 1 and b.LOCNO = {1} and P.IS_ACTIVE =1";

        const string QRY_GET_SUBCATEGORIES_AND_PRODUCTS = @"select b.BAR_CODE ,b.UNIT_PRICE,b.DISC, SC.DESCRIPTION AS SUBCATEGORY,pimg.CHAARSU_IMAGE_PATH , P.*,CEILING(p.AVG_COST) as PRICE 
,ISNULL((select case when QTY > P.THRESHOLD THEN 'YES' ELSE 'NO' END
from STOCK where BRANCH_ID = {1} and PRODUCT_ID = P.OLD_PRODUCT_ID),'NO') AS AVAILABLE
from PRODUCTS P
inner join SUB_CATEGORIES SC on P.SUB_CATEGORY_ID = SC.SUB_CATEGORY_ID 
inner join PRODUCT_IMAGES PIMG on P.PRODUCT_ID = PIMG.PRODUCT_ID
inner join BARCODES B on P.OLD_PRODUCT_ID = b.ITEM_CODE
WHERE (SC.SUB_CATEGORY_ID = {0} or P.SEC_SUB_CATEGORY_A = {0} or P.SEC_SUB_CATEGORY_B = {0} ) and b.IsActive = 1 and b.bDEFAULT = 1 and b.LOCNO = 1 and P.IS_ACTIVE =1";


        const string QRY_GET_SUBCATEGORIES_AND_PRODUCTS_TOP6 = @"select distinct top 6 b.UNIT_PRICE,b.DISC, SC.DESCRIPTION AS SUBCATEGORY,pimg.CHAARSU_IMAGE_PATH , P.*,CEILING(p.AVG_COST) as PRICE from PRODUCTS P
inner join SUB_CATEGORIES SC on P.SUB_CATEGORY_ID = SC.SUB_CATEGORY_ID 
inner join PRODUCT_IMAGES PIMG on P.PRODUCT_ID = PIMG.PRODUCT_ID
inner join BARCODES B on P.OLD_PRODUCT_ID = b.ITEM_CODE
WHERE SC.SUB_CATEGORY_ID = {0} and b.IsActive = 1 and b.bDEFAULT = 1 and b.LOCNO = {1} and P.IS_ACTIVE =1";

        const string QRY_GET_SUBCATEGORIES_AND_PRODUCTS_NEXT_ALL = @"select distinct b.UNIT_PRICE,b.DISC, SC.DESCRIPTION AS SUBCATEGORY,pimg.CHAARSU_IMAGE_PATH , P.*,CEILING(p.AVG_COST) as PRICE from PRODUCTS P
inner join SUB_CATEGORIES SC on P.SUB_CATEGORY_ID = SC.SUB_CATEGORY_ID 
inner join PRODUCT_IMAGES PIMG on P.PRODUCT_ID = PIMG.PRODUCT_ID
inner join BARCODES B on P.OLD_PRODUCT_ID = b.ITEM_CODE
WHERE SC.SUB_CATEGORY_ID = {0} and b.IsActive = 1 and b.bDEFAULT = 1 and b.LOCNO = {1} and P.IS_ACTIVE =1 order by p.PRODUCT_ID asc offset 6 rows fetch next 10  rows only";
        //shoaib1
        const string QRY_GET_BRANDS_BY_CATEGORY_ID = @"select distinct brand from PRODUCTS where [SUB_CATEGORY_ID] in({0}) and BRAND is not null";
        const string QRY_GET_PACKINGS_BY_SUB_CATEGORY_ID = @"select distinct PACKING from PRODUCTS where [SUB_CATEGORY_ID] in({0}) and PACKING is not null";
        const string QRY_GET_PRODUCTS_BY_BRANDS_AND_CATEGORY_ID = @"select distinct b.UNIT_PRICE,b.DISC, SC.DESCRIPTION AS SUBCATEGORY,pimg.CHAARSU_IMAGE_PATH , P.*,CEILING(p.AVG_COST) as PRICE from PRODUCTS P
inner join SUB_CATEGORIES SC on P.SUB_CATEGORY_ID = SC.SUB_CATEGORY_ID 
inner join PRODUCT_IMAGES PIMG on P.PRODUCT_ID = PIMG.PRODUCT_ID
inner join BARCODES B on P.OLD_PRODUCT_ID = b.ITEM_CODE
WHERE P.CATEGORY_ID = {0} and b.IsActive = 1 and b.bDEFAULT = 1 and P.SUB_CATEGORY_ID in({1})and  {2}";
        const string QRY_GET_ACTIVE_ADS = @"select * from ADS where IS_ACTIVE='Active'";
        //shoaib1


        const string QRY_SAVE_TRIGGER_LOGS = "insert into API_LOGS values ('{0}','{1}')";
        const string QRY_GET_TRIGGER_LOGS = "select * from API_LOGS";
        public DataSet GetAllGroups()
        {
            return ExecuteDataSet(QRY_GET_GROUPS);
        }
        public DataSet GetAllCategoriesByGroupId(int groupId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_CATEGORIES_BY_GROUP_ID, groupId));
        }
        public DataSet GetAllCategories()
        {
            return ExecuteDataSet(string.Format(QRY_GET_CATEGORIES));
        }
        public DataSet GetAllSubCategoriesByCategoryId(int categoryId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_SUBCATEGORIES_BY_CATEGORY_ID, categoryId));
        }
        public DataSet GetAllSubCategoriesByGroupId(int groupId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_SUBCATEGORIES_BY_GROUP_ID, groupId));
        }
        //shoaib2
        public DataSet GetAllBrandsByCategoryIds(string categoryId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_BRANDS_BY_CATEGORY_ID, categoryId));
        }
        public DataSet GetAllPackingsBySubCategoryIds(string subcategoryId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_PACKINGS_BY_SUB_CATEGORY_ID, subcategoryId));
        }
        public DataSet GetAllProductsByBrandsAndCategoryId(string brands, int categoryId, string subcatIds)
        {
            return ExecuteDataSet(string.Format(QRY_GET_PRODUCTS_BY_BRANDS_AND_CATEGORY_ID, categoryId, subcatIds, brands));
        }
        public DataSet GetAllActiveAds()
        {
            return ExecuteDataSet(string.Format(QRY_GET_ACTIVE_ADS));
        }
        //shoaib2
        public DataSet GetAllProductsBySubCategoryId(int subCategoryId, int bId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_SUBCATEGORIES_AND_PRODUCTS, subCategoryId, bId));
        }
        public DataSet GetAllProductsBySubCategoryIdTOP6(int subCategoryId, int bId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_SUBCATEGORIES_AND_PRODUCTS_TOP6, subCategoryId, bId));
        }
        public DataSet GetAllProductsBySubCategoryIdFROM7ONWARDS(int subCategoryId, int bId)
        {
            return ExecuteDataSet(string.Format(QRY_GET_SUBCATEGORIES_AND_PRODUCTS_NEXT_ALL, subCategoryId, bId));
        }
        public DataSet getNearestBranch(double latitude, double longitude)
        {
            double EarthRadius = 6400000.0, minD = 6400000.0;
            int nearestBranchId = 0;
            double bLat = 0;
            double bLong = 0;

            DataSet branches = ExecuteDataSet("select * from branches where is_active =1");
            foreach (DataRow row in branches.Tables[0].Rows)
            {
                if (!string.IsNullOrEmpty(row[2].ToString()))
                    bLat = double.Parse(row[2].ToString());
                if (!string.IsNullOrEmpty(row[3].ToString()))
                    bLong = double.Parse(row[3].ToString());

                Double latDistance = DegreeToRadian(bLat - latitude);
                Double lonDistance = DegreeToRadian(bLong - longitude);
                Double a = Math.Sin(latDistance / 2) * Math.Sin(latDistance / 2)
                        + Math.Cos(DegreeToRadian(latitude)) * Math.Cos(DegreeToRadian(bLat))
                        * Math.Sin(lonDistance / 2) * Math.Sin(lonDistance / 2);
                Double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                Double distance = EarthRadius * c;
                if (distance < minD)
                {
                    minD = distance;
                    nearestBranchId = int.Parse(row[0].ToString());
                }
            }

            DataSet ds = ExecuteDataSet($"select * from BRANCHES where BRANCH_ID = {nearestBranchId} AND IS_ACTIVE = 1");
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            return null;
        }
        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public DataSet GetActiveBranches()
        {
            return ExecuteDataSet("select * from BRANCHES where IS_ACTIVE = 1");
        }
        public DataSet GetPaymentModes()
        {
            return ExecuteDataSet("select * from PAYMENT_MODES ");
        }
        public DataSet GetAreas()
        {
            return ExecuteDataSet("select * from Areas ");
        }

        public DataSet GetCouponByPromoCode(string promoCode)
        {
            return ExecuteDataSet($"select * from coupons where promo = '{promoCode}'");
        }
        public int test()
        {
            int result = 0;
            return result;
        }

        public int saveTriggerLogs(string desc)
        {
            return ExecuteNonQuery(string.Format(QRY_SAVE_TRIGGER_LOGS, desc, DateTime.Now.ToString()));
        }
        public DataSet getTriggerLogs()
        {
            return ExecuteDataSet(string.Format(QRY_GET_TRIGGER_LOGS));
        }
    }



}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SASTI.DataAccess;
using System.Data;
namespace SASTI.DataCore
{
    public class ProductManager : DABase
    {
        const string QRY_GET_PRODUCT_BY_ID = @"select P.*,b.* from PRODUCTS P
                                               INNER JOIN BARCODES B on p.OLD_PRODUCT_ID = b.ITEM_CODE
                                               where PRODUCT_ID = {0} AND B.IsActive = 1 AND B.bDEFAULT =1 AND B.LOCNO = {1}";

        const string QRY_GET_PRODUCT_BY_ID2 = @"select P.*,b.*,(select TOP 1 CHAARSU_IMAGE_PATH from PRODUCT_IMAGES  where PRODUCT_ID = P.PRODUCT_ID) as CHAARSU_IMAGE_PATH
                                               from PRODUCTS P
                                               INNER JOIN BARCODES B on p.OLD_PRODUCT_ID = b.ITEM_CODE
                                               where PRODUCT_ID = {0} AND B.IsActive = 1 AND B.bDEFAULT =1 AND B.LOCNO = {1}";


        const string QRY_GET_AVAILABLE_PRODUCTS_BY_NAME = @"select S.PRODUCT_ID, P.NAME from STOCK S
                                                            inner join BRANCHES B on S.BRANCH_ID = B.BRANCH_ID
                                                            inner join PRODUCTS P on S.PRODUCT_ID = P.PRODUCT_ID
                                                            where B.IS_ACTIVE =1 and S.BRANCH_ID = {0}
                                                            and P.NAME LIKE '%{1}%'";
        const string QRY_UPDATE_STOCK = "update stock set QTY = {0} where BRANCH_ID = {1} and PRODUCT_ID = {2}";
        const string QRY_ADD_STOCK = "insert into STOCK values ({0},{1},{2},1)";
        const string QRY_GET_FEATURED_PRODUCTS = @"select P.NAME,S.BRANCH_ID
                                                from PRODUCTS P
                                                inner join STOCK S on P.PRODUCT_ID = S.PRODUCT_ID
                                                where P.IS_FEATURED = 1 and S.BRANCH_ID = {0}";
        const string QRY_GET_FILTERED_PRODUCTS = "SELECT * FROM PRODUCTS where 1=1 ";
//        const string QRY_GET_PRODUCTS = @"select P.*,BC.BAR_CODE,BC.UNIT_PRICE,BC.DISC ,(select top 1 CHAARSU_IMAGE_PATH from PRODUCT_IMAGES PI WHERE PI.PRODUCT_ID = P.PRODUCT_ID)CHAARSU_IMAGE_PATH,(select top 1 CHAARSU_THUMBNAIL_PATH from PRODUCT_IMAGES PI WHERE PI.PRODUCT_ID = P.PRODUCT_ID)
//                                        from PRODUCTS P
//                                        INNER JOIN BARCODES BC ON P.OLD_PRODUCT_ID = BC.ITEM_CODE
//
//                                        WHERE 1=1  AND BC.IsActive =1 and BC.bDEFAULT =1 and P.IS_ACTIVE =1  and BC.LOCNO = {0} ";
        const string QRY_GET_PRODUCTS = @"        select P.*,BC.BAR_CODE,BC.UNIT_PRICE,BC.DISC ,(select top 1 CHAARSU_IMAGE_PATH from PRODUCT_IMAGES PI WHERE PI.PRODUCT_ID = P.PRODUCT_ID)CHAARSU_IMAGE_PATH,(select top 1 CHAARSU_THUMBNAIL_PATH from PRODUCT_IMAGES PI WHERE PI.PRODUCT_ID = P.PRODUCT_ID)
,ISNULL((select case when QTY > P.THRESHOLD THEN 'YES' ELSE 'NO' END
from STOCK where BRANCH_ID = {0} and PRODUCT_ID = P.OLD_PRODUCT_ID),'NO') AS AVAILABLE
from PRODUCTS P
INNER JOIN BARCODES BC ON P.OLD_PRODUCT_ID = BC.ITEM_CODE
WHERE 1=1  AND BC.IsActive =1 and BC.bDEFAULT =1 and P.IS_ACTIVE =1  and BC.LOCNO = {0}";

        const string QRY_GET_ALL_PRODUCTS = @"select P.*,BC.BAR_CODE,BC.UNIT_PRICE,BC.DISC ,(select top 1 CHAARSU_IMAGE_PATH from PRODUCT_IMAGES PI WHERE PI.PRODUCT_ID = P.PRODUCT_ID)CHAARSU_IMAGE_PATH,(select top 1 CHAARSU_THUMBNAIL_PATH from PRODUCT_IMAGES PI WHERE PI.PRODUCT_ID = P.PRODUCT_ID)
                                        from PRODUCTS P
                                        INNER JOIN BARCODES BC ON P.OLD_PRODUCT_ID = BC.ITEM_CODE

                                        WHERE 1=1  AND BC.IsActive =1 and BC.bDEFAULT =1   and BC.LOCNO = {0} ";

        const string QRY_UPDATE_PRICE_DISCOUNT = "update BARCODES set UNIT_PRICE = {0}, DISC = {2},bDEFAULT = {3},IsActive = {4} where ITEM_CODE = {1} and BAR_CODE = {5}";

        const string QRY_INSERT_PRICE_DISCOUNT = "INSERT INTO BARCODES VALUES ({0},{1},{2},'{3}',{4},{5},'{6}',{7},0,{8},0,GETDATE(),0,GETDATE(),{9})";


        const string QRY_INSERT_PRODUCT = "insert into PRODUCTS values ({0},'{1}','{2}',1,'{3}',{4},0,0,0,0,'{5}',0,null,0,null,null,null,null,null,null,0,1,1,0,0,0,0,0,0,0,0)";

        const string QRY_UPDATE_PRODUCT = "update PRODUCTS set AVG_COST = {0},UNIT_OF_MEASUREMENT = '{1}',MODIFIED_BY = 0, MODIFIED_DATE = '{2}' where OLD_PRODUCT_ID = {3}";

        const string QRY_HOT_PRODUCTS = @"SELECT top 6 op.PRODUCT_ID,B.UNIT_PRICE,B.BAR_CODE,B.DISC,
                                        (select top 1 CHAARSU_IMAGE_PATH from PRODUCT_IMAGES PI WHERE PI.PRODUCT_ID = P.PRODUCT_ID)CHAARSU_IMAGE_PATH 
                                        ,count(*) as QTY_ORDERED,P.*
                                        FROM ORDER_PRODUCTS op
                                        inner join PRODUCTS P on op.PRODUCT_ID = P.OLD_PRODUCT_ID

                                        INNER JOIN BARCODES B on P.OLD_PRODUCT_ID = B.ITEM_CODE
                                        GROUP BY op.PRODUCT_ID,B.UNIT_PRICE,B.BAR_CODE,B.DISC,
                                        P.PRODUCT_ID,P.OLD_PRODUCT_ID,P.NAME,P.DESCRIPTION,P.SUB_CATEGORY_ID,P.UNIT_OF_MEASUREMENT,P.AVG_COST,P.PRICE,
                                        P.DISCOUNT_PERCENTAGE,P.IMPORTED,P.IS_ACTIVE,P.CREATED_DATE,P.CREATED_BY,P.MODIFIED_DATE,P.MODIFIED_BY,P.COLOR,
                                        P.BRAND,P.FLAVOR,P.PACKING,P.TAG,P.TYPES,P.DISCOUNT,P.SEC_SUB_CATEGORY_A,P.SEC_SUB_CATEGORY_B,P.PRICE2,
                                        P.HAS_IMAGE,P.HAS_THUMBNAIL_IMAGE,P.VENDOR_ID,P.CATEGORY_ID,P.IS_FEATURED,P.THRESHOLD,P.IS_EXEMPTED
                                        ORDER BY count(*) DESC";

//        const string QRY_GET_RECOMMENDED_PRODUCTS = @"SELECT RP.PRODUCT_ID,P.OLD_PRODUCT_ID,RP.PRODUCT_NAME AS NAME,P.DESCRIPTION,P.SUB_CATEGORY_ID,P.UNIT_OF_MEASUREMENT,P.AVG_COST,P.PRICE,P.DISCOUNT_PERCENTAGE,
//P.IMPORTED,P.IS_ACTIVE,P.CREATED_DATE,P.CREATED_BY,P.MODIFIED_DATE,P.MODIFIED_BY,P.COLOR,P.BRAND,P.FLAVOR,P.PACKING,P.TAG,P.TYPES,P.DISCOUNT,
//P.SEC_SUB_CATEGORY_A,P.SEC_SUB_CATEGORY_B,P.PRICE2,P.HAS_IMAGE,P.HAS_THUMBNAIL_IMAGE,P.VENDOR_ID,P.CATEGORY_ID,P.IS_FAVOURITE,P.THRESHOLD,P.IS_FEATURED,
//P.IS_EXEMPTED,P.PRODUCT_NAME_URL,P.DEVICE_TYPE,P.IS_FAVOURITE FROM RECOMMENDED_PRODUCTS RP
//INNER JOIN PRODUCTS P ON RP.PRODUCT_ID = P.PRODUCT_ID


//SELECT RP.PRODUCT_ID,PIM.PRODUCT_IMAGE_ID,PIM.ADMIN_IMAGE_PATH,PIM.ADMIN_THUMNAIL_IMAGE_PATH,
//PIM.CHAARSU_IMAGE_PATH,PIM.CHAARSU_THUMBNAIL_PATH FROM RECOMMENDED_PRODUCTS RP
//INNER JOIN PRODUCT_IMAGES PIM on RP.PRODUCT_ID = PIM.PRODUCT_ID";
        
        const string QRY_GET_RECOMMENDED_PRODUCTS = @"SELECT RP.PRODUCT_ID,P.OLD_PRODUCT_ID,RP.PRODUCT_NAME AS NAME,P.DESCRIPTION,P.SUB_CATEGORY_ID,P.UNIT_OF_MEASUREMENT,P.AVG_COST,P.PRICE,P.DISCOUNT_PERCENTAGE,
P.IMPORTED,P.IS_ACTIVE,P.CREATED_DATE,P.CREATED_BY,P.MODIFIED_DATE,P.MODIFIED_BY,P.COLOR,P.BRAND,P.FLAVOR,P.PACKING,P.TAG,P.TYPES,P.DISCOUNT,
P.SEC_SUB_CATEGORY_A,P.SEC_SUB_CATEGORY_B,P.PRICE2,P.HAS_IMAGE,P.HAS_THUMBNAIL_IMAGE,P.VENDOR_ID,P.CATEGORY_ID,P.IS_FAVOURITE,P.THRESHOLD,
P.IS_FEATURED,P.IS_EXEMPTED,P.PRODUCT_NAME_URL,P.DEVICE_TYPE,P.IS_FAVOURITE,
PIM.PRODUCT_IMAGE_ID,PIM.ADMIN_IMAGE_PATH,PIM.ADMIN_THUMNAIL_IMAGE_PATH,
PIM.CHAARSU_IMAGE_PATH,PIM.CHAARSU_THUMBNAIL_PATH,
B.LOCNO,B.ITEM_CODE,B.PACK_CODE,B.PACK_DESC,B.UNIT,B.bDEFAULT,B.BAR_CODE,B.BARTYPE,B.UNIT_PRICE,B.DEALER_PRICE1,B.DEALER_PRICE2,
B.RF_MARGIN,B.CUSER,B.CDATE,B.MUSER,B.MDATE,B.bRandomDisc,B.DiscStartDate,B.DiscEndDate,B.DiscStartTime,B.DiscEndTime,B.DISC_TYPE,
B.DISC,B.LOYALTY_DISC,B.STAFF_DISC,B.IsActive
--ISNULL((SELECT TOP 1 PIM.PRODUCT_IMAGE_ID FROM PRODUCT_IMAGES PIM WHERE PIM.PRODUCT_ID = RP.PRODUCT_ID),0) AS PRODUCT_IMAGE_ID,
--ISNULL((SELECT TOP 1 PIM.ADMIN_IMAGE_PATH FROM PRODUCT_IMAGES PIM WHERE PIM.PRODUCT_ID = RP.PRODUCT_ID),0) AS ADMIN_IMAGE_PATH,
--ISNULL((SELECT TOP 1 PIM.ADMIN_THUMNAIL_IMAGE_PATH FROM PRODUCT_IMAGES PIM WHERE PIM.PRODUCT_ID = RP.PRODUCT_ID),0) AS ADMIN_THUMNAIL_IMAGE_PATH,
--ISNULL((SELECT TOP 1 PIM.CHAARSU_IMAGE_PATH FROM PRODUCT_IMAGES PIM WHERE PIM.PRODUCT_ID = RP.PRODUCT_ID),0) AS CHAARSU_IMAGE_PATH,
--ISNULL((SELECT TOP 1 PIM.CHAARSU_THUMBNAIL_PATH FROM PRODUCT_IMAGES PIM WHERE PIM.PRODUCT_ID = RP.PRODUCT_ID),0) AS CHAARSU_THUMBNAIL_PATH
FROM RECOMMENDED_PRODUCTS RP
INNER JOIN PRODUCTS P ON RP.PRODUCT_ID = P.PRODUCT_ID
INNER JOIN PRODUCT_IMAGES PIM ON RP.PRODUCT_ID = PIM.PRODUCT_ID
INNER JOIN BARCODES B ON P.PRODUCT_ID = B.ITEM_CODE
WHERE B.bDEFAULT = 1 AND B.IsActive = 1 AND B.LOCNO = 1
--GROUP BY P.PRODUCT_ID,P.OLD_PRODUCT_ID,RP.PRODUCT_ID,RP.PRODUCT_NAME,P.DESCRIPTION,P.SUB_CATEGORY_ID,P.UNIT_OF_MEASUREMENT,P.AVG_COST,P.PRICE,P.DISCOUNT_PERCENTAGE,
--P.IMPORTED,P.IS_ACTIVE,P.CREATED_DATE,P.CREATED_BY,P.MODIFIED_DATE,P.MODIFIED_BY,P.COLOR,P.BRAND,P.FLAVOR,P.PACKING,P.TAG,P.TYPES,P.DISCOUNT,
--P.SEC_SUB_CATEGORY_A,P.SEC_SUB_CATEGORY_B,P.PRICE2,P.HAS_IMAGE,P.HAS_THUMBNAIL_IMAGE,P.VENDOR_ID,P.CATEGORY_ID,P.IS_FAVOURITE,P.THRESHOLD,
--P.IS_FEATURED,P.IS_EXEMPTED,P.PRODUCT_NAME_URL,P.DEVICE_TYPE,P.IS_FAVOURITE,
--PIM.PRODUCT_IMAGE_ID,PIM.ADMIN_IMAGE_PATH,PIM.ADMIN_THUMNAIL_IMAGE_PATH,
--PIM.CHAARSU_IMAGE_PATH,PIM.CHAARSU_THUMBNAIL_PATH";
        
        public DataSet GetProductById(int productId,int bID)
        {
            return ExecuteDataSet(string.Format(QRY_GET_PRODUCT_BY_ID, productId,bID));
        }
        public DataSet GetProductById2(int productId, int bID)
        {
            return ExecuteDataSet(string.Format(QRY_GET_PRODUCT_BY_ID2, productId, bID));
        }
        public DataSet GetAvailableProductByName(int branchId,string p_name)
        {
            return ExecuteDataSet(string.Format(QRY_GET_AVAILABLE_PRODUCTS_BY_NAME, branchId, p_name));
        }
        public int updateStock(int branch_id,int product_id,int qty)
        {
            return ExecuteNonQuery(string.Format(QRY_UPDATE_STOCK,qty,branch_id,product_id));
        }
        public int updatePriceAndDiscount(decimal price, int product_id, int disc, int bdefault, int actv, string bcode)
        {
            return ExecuteNonQuery(string.Format(QRY_UPDATE_PRICE_DISCOUNT, price, product_id, disc,bdefault,actv,bcode));
        }
        public int addPriceAndDiscount(int locno, int productid, int packcode, string packdesc, int unit, int bdef, string barcode, decimal price, int disc, int active)
        {
            return ExecuteNonQuery(string.Format(QRY_INSERT_PRICE_DISCOUNT, locno,productid,packcode,packdesc,unit,bdef,barcode,price,disc,active));
        }
        public DataSet getStock()
        {
            return ExecuteDataSet("select top 10 * from stock order by branch_id desc");
        }
        public DataSet getFeaturedProducts(int branch_id)
        {
            return ExecuteDataSet(string.Format(QRY_UPDATE_STOCK,branch_id));
        }
        public DataSet searchProductsByFilters(string name, int min_price, int max_price,int subCategoryId)
        {
            string query = QRY_GET_FILTERED_PRODUCTS;
            //WHERE NAME LIKE '%%' AND PRICE >= 3 and PRICE <= 4
            if(name != string.Empty)
            {
                query += " AND NAME LIKE '%" + name + "%' ";
            }
            if(min_price >= 0)
            {
                query += " AND PRICE >=" + min_price;
            }
            if (max_price >= 0)
            {
                query += " AND PRICE <=" + max_price;
            }
            query += " AND SUB_CATEGORY_ID = " + subCategoryId;

            return ExecuteDataSet(query);
        }

        public DataSet searchProducts(string name, int min_price, int max_price, int branchId)
        {
            string query = string.Format(QRY_GET_PRODUCTS,branchId);
            //WHERE NAME LIKE '%%' AND PRICE >= 3 and PRICE <= 4
            if (name != null && name != string.Empty)
            {
                query += " AND P.NAME LIKE '%" + name + "%' ";
            }
            if (min_price >= 0)
            {
                query += " AND BC.UNIT_PRICE >=" + min_price;
            }
            if (max_price >= 0)
            {
                query += " AND BC.UNIT_PRICE <=" + max_price;
            }
            //query += " AND SUB_CATEGORY_ID = " + subCategoryId;

            return ExecuteDataSet(query);
        }

        public DataSet searchAllProducts(string name, int min_price, int max_price, int branchId)
        {
            string query = string.Format(QRY_GET_ALL_PRODUCTS, branchId);
            //WHERE NAME LIKE '%%' AND PRICE >= 3 and PRICE <= 4
            if (name != null && name != string.Empty)
            {
                query += " AND P.NAME LIKE '%" + name + "%' ";
            }
            if (min_price >= 0)
            {
                query += " AND BC.UNIT_PRICE >=" + min_price;
            }
            if (max_price >= 0)
            {
                query += " AND BC.UNIT_PRICE <=" + max_price;
            }
            //query += " AND SUB_CATEGORY_ID = " + subCategoryId;

            return ExecuteDataSet(query);
        }

        public int addStock(int branch_id, int product_id, int qty)
        {
            return ExecuteNonQuery(string.Format(QRY_ADD_STOCK,  branch_id, product_id,qty));
        }
        public int addProduct(decimal cost,int product_id,string desc,string desclong,string uom)
        {
            return ExecuteNonQuery(string.Format(QRY_INSERT_PRODUCT, product_id,desc,desclong,uom,cost,DateTime.Now.ToString()));
        }
        public int updateProduct(decimal cost, int product_id, string desc, string desclong, string uom)
        {
            return ExecuteNonQuery(string.Format(QRY_UPDATE_PRODUCT, cost,uom,DateTime.Now.ToString(),product_id));
        }
        
        public DataSet getHotProducts()
        {
            return ExecuteDataSet(QRY_HOT_PRODUCTS);
        }
        
        public DataSet getRecommendedProducts(int pageindex, int pageSize)
        {

            List<StoredProcedureParams> prm = new List<StoredProcedureParams>();
            prm.Add(new StoredProcedureParams(pageindex.ToString(), DbType.Int32) { ParamName = "@PageIndex", ParamType = DbType.Int32,ParamValue=pageindex.ToString() });
            prm.Add(new StoredProcedureParams(pageSize.ToString(), DbType.Int32) { ParamName = "@PageSize", ParamType = DbType.Int32, ParamValue = pageSize.ToString() });

            return ExecuteStoredProcedure("SPGetAllRecommendedProducts", prm);
        }

        public DataSet getSearchProducts(string str)
        {

            List<StoredProcedureParams> prm = new List<StoredProcedureParams>();
            prm.Add(new StoredProcedureParams(str, DbType.String) { ParamName = "@ProductName", ParamType = DbType.String, ParamValue = str });

            return ExecuteStoredProcedure("SPGetSearchProducts", prm);
        }
    }
}
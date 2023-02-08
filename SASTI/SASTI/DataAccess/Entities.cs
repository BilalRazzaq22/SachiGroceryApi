using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SASTI.DataAccess
{
    public class Entities
    {
        #region GROUPS
        public static class GROUPS
        {
            public const string TABLE_NAME = "GROUPS";

            public const string GROUP_ID = "GROUP_ID";
            public const string NAME = "NAME";
            public const string IMAGE_NAME = "IMAGE_NAME";
            public const string RELATIVE_IMAGE_NAME = "RELATIVE_IMAGE_NAME";
            
            public static DataTable GetDataTable()
            {
                DataTable dt = new DataTable(TABLE_NAME);

                dt.Columns.Add(GROUP_ID, typeof(int));
                dt.Columns.Add(NAME, typeof(string));
                dt.Columns.Add(IMAGE_NAME, typeof(string));
                dt.Columns.Add(RELATIVE_IMAGE_NAME, typeof(string));
                return dt;
            }
        }
        #endregion

        #region CATEGORIES
        public static class CATEGORIES
        {
            public const string TABLE_NAME = "CATEGORIES";

            public const string CATEGORY_ID = "CATEGORY_ID";
            public const string DESCRIPTION = "DESCRIPTION";

            public static DataTable GetDataTable()
            {
                DataTable dt = new DataTable(TABLE_NAME);

                dt.Columns.Add(CATEGORY_ID, typeof(int));
                dt.Columns.Add(DESCRIPTION, typeof(string));
                return dt;
            }
        }
        #endregion
        #region SUB_CATEGORIES
        public static class SUB_CATEGORIES
        {
            public const string TABLE_NAME = "SUB_CATEGORIES";

            public const string SUB_CATEGORY_ID = "SUB_CATEGORY_ID";
            public const string CATEGORY_ID = "CATEGORY_ID";
            public const string DESCRIPTION = "DESCRIPTION";
            public const string CREATED_BY = "CREATED_BY";
            public const string CREATED_DATE = "CREATED_DATE";
            public const string MODIFIED_BY = "MODIFIED_BY";
            public const string MODIFIED_DATE = "MODIFIED_DATE";
            public const string IsActive = "IsActive";
            public static DataTable GetDataTable()
            {
                DataTable dt = new DataTable(TABLE_NAME);

                dt.Columns.Add(SUB_CATEGORY_ID, typeof(int));
                dt.Columns.Add(CATEGORY_ID, typeof(int));
                dt.Columns.Add(DESCRIPTION, typeof(string));
                dt.Columns.Add(CREATED_BY, typeof(int));
                dt.Columns.Add(CREATED_DATE, typeof(DateTime));
                dt.Columns.Add(MODIFIED_BY, typeof(int));
                dt.Columns.Add(MODIFIED_DATE, typeof(DateTime));
                dt.Columns.Add(IsActive, typeof(int));
                return dt;
            }
        }
        #endregion

        #region PRODUCTS
        public static class PRODUCTS
        {
            public const string TABLE_NAME = "PRODUCTS";

            public const string PRODUCT_ID = "PRODUCT_ID";
            public const string NAME = "NAME";
            public const string DESCRIPTION = "DESCRIPTION";
            public const string VENDOR_ID = "VENDOR_ID";
            public const string CATEGORY_ID = "CATEGORY_ID";
            public const string SUB_CATEGORY_ID = "SUB_CATEGORY_ID";
            public const string UNIT_OF_MEASUREMENT = "UNIT_OF_MEASUREMENT";
            public const string AVG_COST = "AVG_COST";
            public const string TAGS = "TAGS";
            public const string TYPE = "TYPE";
            public const string FLAVORS = "FLAVORS";
            public const string BRAND_ID = "BRAND_ID";
            public const string CREATED_BY = "CREATED_BY";
            public const string CREATED_DATE = "CREATED_DATE";
            public const string MODIFIED_BY = "MODIFIED_BY";
            public const string MODIFIED_DATE = "MODIFIED_DATE";
            public const string IS_ACTIVE = "IS_ACTIVE";

            public const string SUBCATEGORY = "SUBCATEGORY";
            public const string CHAARSU_IMAGE_PATH = "CHAARSU_IMAGE_PATH";
            public const string PRICE = "PRICE";
            //shoaib
            public const string BRAND = "BRAND";
            public const string PACKING = "PACKING";
            //shoaib


            public static DataTable GetDataTable()
            {
                DataTable dt = new DataTable(TABLE_NAME);

                dt.Columns.Add(PRODUCT_ID, typeof(int));
                dt.Columns.Add(NAME, typeof(string));
                dt.Columns.Add(DESCRIPTION, typeof(string));
                dt.Columns.Add(VENDOR_ID, typeof(int));
                dt.Columns.Add(CATEGORY_ID, typeof(int));
                dt.Columns.Add(SUB_CATEGORY_ID, typeof(int));
                dt.Columns.Add(UNIT_OF_MEASUREMENT, typeof(string));
                dt.Columns.Add(AVG_COST, typeof(string));
                dt.Columns.Add(TAGS, typeof(int));
                dt.Columns.Add(TYPE, typeof(int));
                dt.Columns.Add(FLAVORS, typeof(string));
                dt.Columns.Add(BRAND_ID, typeof(int));
                dt.Columns.Add(CREATED_BY, typeof(int));
                dt.Columns.Add(CREATED_DATE, typeof(DateTime));
                dt.Columns.Add(MODIFIED_BY, typeof(int));
                dt.Columns.Add(MODIFIED_DATE, typeof(DateTime));
                dt.Columns.Add(IS_ACTIVE, typeof(int));
                dt.Columns.Add(SUBCATEGORY, typeof(string));
                dt.Columns.Add(PRICE, typeof(string));
                dt.Columns.Add(CHAARSU_IMAGE_PATH, typeof(string));
                //shoaib
                dt.Columns.Add(BRAND, typeof(string));
                dt.Columns.Add(PACKING, typeof(string));
                //shoaib
                return dt;
            }
        }
        #endregion
    }
}
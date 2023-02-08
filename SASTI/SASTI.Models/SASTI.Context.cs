﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SASTI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SASTIEntities : DbContext
    {
        public SASTIEntities()
            : base("name=SASTIEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AD> ADS { get; set; }
        public virtual DbSet<AREA> AREAS { get; set; }
        public virtual DbSet<BARCODESTest> BARCODESTests { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<BRANCH> BRANCHES { get; set; }
        public virtual DbSet<BRAND> BRANDS { get; set; }
        public virtual DbSet<CART> CARTs { get; set; }
        public virtual DbSet<CATEGORy> CATEGORIES { get; set; }
        public virtual DbSet<ContactU> ContactUs { get; set; }
        public virtual DbSet<COUPON> COUPONS { get; set; }
        public virtual DbSet<GROUP> GROUPS { get; set; }
        public virtual DbSet<ITEMINFO> ITEMINFOes { get; set; }
        public virtual DbSet<ORDER_PRODUCTS> ORDER_PRODUCTS { get; set; }
        public virtual DbSet<ORDER_STATUSES> ORDER_STATUSES { get; set; }
        public virtual DbSet<ORDER> ORDERS { get; set; }
        public virtual DbSet<PACKAGE_PRODUCTS> PACKAGE_PRODUCTS { get; set; }
        public virtual DbSet<PACKAGE> PACKAGES { get; set; }
        public virtual DbSet<PAYMENT_MODES> PAYMENT_MODES { get; set; }
        public virtual DbSet<PRODUCT_BRANDS> PRODUCT_BRANDS { get; set; }
        public virtual DbSet<PRODUCT_COLORS> PRODUCT_COLORS { get; set; }
        public virtual DbSet<PRODUCT_FLAVORS> PRODUCT_FLAVORS { get; set; }
        public virtual DbSet<PRODUCT_IMAGES> PRODUCT_IMAGES { get; set; }
        public virtual DbSet<PRODUCT_LEVELS> PRODUCT_LEVELS { get; set; }
        public virtual DbSet<PRODUCT_PACKINGS> PRODUCT_PACKINGS { get; set; }
        public virtual DbSet<PRODUCT_TAGS> PRODUCT_TAGS { get; set; }
        public virtual DbSet<PRODUCT_TYPES> PRODUCT_TYPES { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTS { get; set; }
        public virtual DbSet<ProductTest> ProductTests { get; set; }
        public virtual DbSet<PS000> PS000 { get; set; }
        public virtual DbSet<RIDER_ORDER> RIDER_ORDER { get; set; }
        public virtual DbSet<SM> SMS { get; set; }
        public virtual DbSet<SMS_TYPES> SMS_TYPES { get; set; }
        public virtual DbSet<STOCK> STOCKs { get; set; }
        public virtual DbSet<SUB_CATEGORIES> SUB_CATEGORIES { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TEMP_CUSTOMERS> TEMP_CUSTOMERS { get; set; }
        public virtual DbSet<USER_ADDRESSES> USER_ADDRESSES { get; set; }
        public virtual DbSet<USER_TYPES> USER_TYPES { get; set; }
        public virtual DbSet<USER> USERS { get; set; }
        public virtual DbSet<VENDOR_BRANCHES> VENDOR_BRANCHES { get; set; }
        public virtual DbSet<VENDOR> VENDORS { get; set; }
        public virtual DbSet<BarcodeNewTest> BarcodeNewTests { get; set; }
        public virtual DbSet<BARCODE> BARCODES { get; set; }
        public virtual DbSet<PS000Test> PS000Test { get; set; }
        public virtual DbSet<TEST> TESTs { get; set; }
        public virtual DbSet<BANNER> BANNERS { get; set; }
        public virtual DbSet<RECOMMENDED_PRODUCTS> RECOMMENDED_PRODUCTS { get; set; }
        public virtual DbSet<USER_FAVOURITES> USER_FAVOURITES { get; set; }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int SP_GETRECOMMENDEDPRODUCTS()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_GETRECOMMENDEDPRODUCTS");
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int SP_InsertBarcode()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_InsertBarcode");
        }
    
        public virtual int SP_InsertItemInfo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_InsertItemInfo");
        }
    
        public virtual int SP_InsertProduct()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_InsertProduct");
        }
    
        public virtual int SP_InsertProductFromItemInfo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_InsertProductFromItemInfo");
        }
    
        public virtual int SP_InsertStock()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_InsertStock");
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int SP_UpdateStock()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateStock");
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    
        public virtual int SpGetAllBanners(Nullable<int> pageIndex, Nullable<int> pageSize, string sortColumn, string sortOrder, string searchText)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            var searchTextParameter = searchText != null ?
                new ObjectParameter("SearchText", searchText) :
                new ObjectParameter("SearchText", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpGetAllBanners", pageIndexParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter, searchTextParameter);
        }
    
        public virtual int SpGetAllBannersHome(Nullable<int> pageIndex)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpGetAllBannersHome", pageIndexParameter);
        }
    
        public virtual ObjectResult<SpGetAllBlogs_Result> SpGetAllBlogs(Nullable<int> pageIndex, Nullable<int> pageSize, string sortColumn, string sortOrder, string searchText)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            var searchTextParameter = searchText != null ?
                new ObjectParameter("SearchText", searchText) :
                new ObjectParameter("SearchText", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpGetAllBlogs_Result>("SpGetAllBlogs", pageIndexParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter, searchTextParameter);
        }
    
        public virtual ObjectResult<SpGetAllCartItemsByUserId_Result> SpGetAllCartItemsByUserId(Nullable<int> pageIndex, Nullable<int> pageSize, string sortColumn, string sortOrder, string searchText, Nullable<int> userId)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            var searchTextParameter = searchText != null ?
                new ObjectParameter("SearchText", searchText) :
                new ObjectParameter("SearchText", typeof(string));
    
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpGetAllCartItemsByUserId_Result>("SpGetAllCartItemsByUserId", pageIndexParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter, searchTextParameter, userIdParameter);
        }
    
        public virtual ObjectResult<SpGetAllOrders_Result> SpGetAllOrders(Nullable<int> pageIndex, Nullable<int> pageSize, string sortColumn, string sortOrder, string searchText, Nullable<int> oStID, string oDateFrom, string oDateTo)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            var searchTextParameter = searchText != null ?
                new ObjectParameter("SearchText", searchText) :
                new ObjectParameter("SearchText", typeof(string));
    
            var oStIDParameter = oStID.HasValue ?
                new ObjectParameter("oStID", oStID) :
                new ObjectParameter("oStID", typeof(int));
    
            var oDateFromParameter = oDateFrom != null ?
                new ObjectParameter("oDateFrom", oDateFrom) :
                new ObjectParameter("oDateFrom", typeof(string));
    
            var oDateToParameter = oDateTo != null ?
                new ObjectParameter("oDateTo", oDateTo) :
                new ObjectParameter("oDateTo", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpGetAllOrders_Result>("SpGetAllOrders", pageIndexParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter, searchTextParameter, oStIDParameter, oDateFromParameter, oDateToParameter);
        }
    
        public virtual ObjectResult<SpGetAllOrdersByCustomerId_Result> SpGetAllOrdersByCustomerId(Nullable<int> pageIndex, Nullable<int> pageSize, string sortColumn, string sortOrder, string searchText, Nullable<int> oStID, Nullable<int> customerId)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            var searchTextParameter = searchText != null ?
                new ObjectParameter("SearchText", searchText) :
                new ObjectParameter("SearchText", typeof(string));
    
            var oStIDParameter = oStID.HasValue ?
                new ObjectParameter("oStID", oStID) :
                new ObjectParameter("oStID", typeof(int));
    
            var customerIdParameter = customerId.HasValue ?
                new ObjectParameter("CustomerId", customerId) :
                new ObjectParameter("CustomerId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpGetAllOrdersByCustomerId_Result>("SpGetAllOrdersByCustomerId", pageIndexParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter, searchTextParameter, oStIDParameter, customerIdParameter);
        }
    
        public virtual int SpGetAllProductReviews(Nullable<int> pageIndex, Nullable<int> pageSize, string sortColumn, string sortOrder, string searchText)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            var searchTextParameter = searchText != null ?
                new ObjectParameter("SearchText", searchText) :
                new ObjectParameter("SearchText", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpGetAllProductReviews", pageIndexParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter, searchTextParameter);
        }
    
        public virtual int SpGetAllProductReviewsHome(Nullable<int> pageIndex)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpGetAllProductReviewsHome", pageIndexParameter);
        }
    
        public virtual ObjectResult<SpGetAllProducts_Result> SpGetAllProducts(Nullable<int> pageIndex, Nullable<int> pageSize, string sortColumn, string sortOrder, string searchText, string categoryId, string subCategoryId, string groupId)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            var searchTextParameter = searchText != null ?
                new ObjectParameter("SearchText", searchText) :
                new ObjectParameter("SearchText", typeof(string));
    
            var categoryIdParameter = categoryId != null ?
                new ObjectParameter("CategoryId", categoryId) :
                new ObjectParameter("CategoryId", typeof(string));
    
            var subCategoryIdParameter = subCategoryId != null ?
                new ObjectParameter("SubCategoryId", subCategoryId) :
                new ObjectParameter("SubCategoryId", typeof(string));
    
            var groupIdParameter = groupId != null ?
                new ObjectParameter("GroupId", groupId) :
                new ObjectParameter("GroupId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpGetAllProducts_Result>("SpGetAllProducts", pageIndexParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter, searchTextParameter, categoryIdParameter, subCategoryIdParameter, groupIdParameter);
        }
    
        public virtual int SPGetAllRecommendedProducts(Nullable<int> pageIndex, Nullable<int> pageSize)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SPGetAllRecommendedProducts", pageIndexParameter, pageSizeParameter);
        }
    
        public virtual ObjectResult<SpGetBlogDetailByBlogTitleUrl_Result> SpGetBlogDetailByBlogTitleUrl(string blogTitleUrl)
        {
            var blogTitleUrlParameter = blogTitleUrl != null ?
                new ObjectParameter("BlogTitleUrl", blogTitleUrl) :
                new ObjectParameter("BlogTitleUrl", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpGetBlogDetailByBlogTitleUrl_Result>("SpGetBlogDetailByBlogTitleUrl", blogTitleUrlParameter);
        }
    
        public virtual ObjectResult<SpGetHomeBlogs_Result> SpGetHomeBlogs(Nullable<int> pageIndex)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpGetHomeBlogs_Result>("SpGetHomeBlogs", pageIndexParameter);
        }
    
        public virtual ObjectResult<SpGetHomeProducts_Result> SpGetHomeProducts(Nullable<int> categoryId)
        {
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("CategoryId", categoryId) :
                new ObjectParameter("CategoryId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpGetHomeProducts_Result>("SpGetHomeProducts", categoryIdParameter);
        }
    
        public virtual ObjectResult<SpGetProductDetailByProductNameUrl_Result> SpGetProductDetailByProductNameUrl(string productNameUrl)
        {
            var productNameUrlParameter = productNameUrl != null ?
                new ObjectParameter("ProductNameUrl", productNameUrl) :
                new ObjectParameter("ProductNameUrl", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpGetProductDetailByProductNameUrl_Result>("SpGetProductDetailByProductNameUrl", productNameUrlParameter);
        }
    
        public virtual ObjectResult<SpGetProductImagesByProductId_Result> SpGetProductImagesByProductId(Nullable<int> productId)
        {
            var productIdParameter = productId.HasValue ?
                new ObjectParameter("ProductId", productId) :
                new ObjectParameter("ProductId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpGetProductImagesByProductId_Result>("SpGetProductImagesByProductId", productIdParameter);
        }
    
        public virtual int SpGetProductReviewsByProductId(Nullable<long> pRODUCT_ID, Nullable<int> pageIndex, Nullable<int> pageSize, string sortColumn, string sortOrder, string searchText)
        {
            var pRODUCT_IDParameter = pRODUCT_ID.HasValue ?
                new ObjectParameter("PRODUCT_ID", pRODUCT_ID) :
                new ObjectParameter("PRODUCT_ID", typeof(long));
    
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            var searchTextParameter = searchText != null ?
                new ObjectParameter("SearchText", searchText) :
                new ObjectParameter("SearchText", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpGetProductReviewsByProductId", pRODUCT_IDParameter, pageIndexParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter, searchTextParameter);
        }
    
        public virtual ObjectResult<SpGetUserProfileById_Result> SpGetUserProfileById(Nullable<int> userId)
        {
            var userIdParameter = userId.HasValue ?
                new ObjectParameter("UserId", userId) :
                new ObjectParameter("UserId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SpGetUserProfileById_Result>("SpGetUserProfileById", userIdParameter);
        }
    
        public virtual int SpGetWishList(Nullable<int> pageIndex, Nullable<int> pageSize, string sortColumn, string sortOrder, string searchText, Nullable<int> uSER_ID)
        {
            var pageIndexParameter = pageIndex.HasValue ?
                new ObjectParameter("PageIndex", pageIndex) :
                new ObjectParameter("PageIndex", typeof(int));
    
            var pageSizeParameter = pageSize.HasValue ?
                new ObjectParameter("PageSize", pageSize) :
                new ObjectParameter("PageSize", typeof(int));
    
            var sortColumnParameter = sortColumn != null ?
                new ObjectParameter("SortColumn", sortColumn) :
                new ObjectParameter("SortColumn", typeof(string));
    
            var sortOrderParameter = sortOrder != null ?
                new ObjectParameter("SortOrder", sortOrder) :
                new ObjectParameter("SortOrder", typeof(string));
    
            var searchTextParameter = searchText != null ?
                new ObjectParameter("SearchText", searchText) :
                new ObjectParameter("SearchText", typeof(string));
    
            var uSER_IDParameter = uSER_ID.HasValue ?
                new ObjectParameter("USER_ID", uSER_ID) :
                new ObjectParameter("USER_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SpGetWishList", pageIndexParameter, pageSizeParameter, sortColumnParameter, sortOrderParameter, searchTextParameter, uSER_IDParameter);
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using SASTI;
using SASTI.DataCore;
using SASTI.Models;
using SASTI.Models.Dto;

namespace SASTI.Controllers
{
    public class BusinessCoreController
    {
        #region GENERAL
        public DataSet GetAllGroups()
        {
            return new GeneralManager().GetAllGroups();
        }
        public int test()
        {
            return new GeneralManager().test();
        }
        public DataSet GetActiveBranches()
        {
            return new GeneralManager().GetActiveBranches();
        }
        public DataSet GetPaymentModes()
        {
            return new GeneralManager().GetPaymentModes();
        }
        public DataSet GetAllCategoriesByGroupId(int groupId)
        {
            return new GeneralManager().GetAllCategoriesByGroupId(groupId);
        }
        public DataSet GetAllCategories()
        {
            return new GeneralManager().GetAllCategories();
        }
        public DataSet GetAllSubCategoriesByCategoryId(int categoryId)
        {
            return new GeneralManager().GetAllSubCategoriesByCategoryId(categoryId);
        }
        public DataSet GetAllSubCategoriesByGroupId(int groupId)
        {
            return new GeneralManager().GetAllSubCategoriesByGroupId(groupId);
        }
        //shoaib
        public DataSet GetAllBrandsByCategoryIds(string categoryId)
        {
            return new GeneralManager().GetAllBrandsByCategoryIds(categoryId);
        }
        public DataSet GetAllPackingsBySubCategoryIds(string subcategoryId)
        {
            return new GeneralManager().GetAllPackingsBySubCategoryIds(subcategoryId);
        }
        public DataSet GetAllProductsByBrandsAndCategoryId(string brands, int categoryId, string subcatIds)
        {
            return new GeneralManager().GetAllProductsByBrandsAndCategoryId(brands, categoryId, subcatIds);
        }
        public DataSet GetAllActiveAds()
        {
            return new GeneralManager().GetAllActiveAds();
        }
        //shoaib
        public DataSet GetAllProductsBySubCategoryId(int subCategoryId, int bId)
        {
            return new GeneralManager().GetAllProductsBySubCategoryId(subCategoryId, bId);
        }
        public DataSet GetAllProductsBySubCategoryIdTOP6(int subCategoryId, int bId)
        {
            return new GeneralManager().GetAllProductsBySubCategoryIdTOP6(subCategoryId, bId);
        }
        public DataSet GetAllProductsBySubCategoryIdFROM7ONWARDS(int subCategoryId, int bId)
        {
            return new GeneralManager().GetAllProductsBySubCategoryIdFROM7ONWARDS(subCategoryId, bId);
        }
        public DataSet getNearestBranch(double latitude, double longitude)
        {
            return new GeneralManager().getNearestBranch(latitude, longitude);
        }
        public int saveTriggerLogs(string desc)
        {
            return new GeneralManager().saveTriggerLogs(desc);
        }
        public DataSet getTriggerLogs()
        {
            return new GeneralManager().getTriggerLogs();
        }
        public DataSet GetAreas()
        {
            return new GeneralManager().GetAreas();
        }

        public DataSet GetCouponByPromoCode(string promoCode)
        {
            return new GeneralManager().GetCouponByPromoCode(promoCode);
        }
        #endregion

        #region PRODUCT
        public DataSet GetProductById(int productId, int bID)
        {
            return new ProductManager().GetProductById(productId, bID);
        }
        public DataSet GetProductById2(int productId, int bID)
        {
            return new ProductManager().GetProductById2(productId, bID);
        }
        public DataSet GetAvailableProductByName(int branchId, string p_name)
        {
            return new ProductManager().GetAvailableProductByName(branchId, p_name);
        }
        public int updateStock(int branch_id, int product_id, int qty)
        {
            return new ProductManager().updateStock(branch_id, product_id, qty);
        }
        public int updatePriceAndDiscount(decimal price, int product_id, int disc, int bdefault, int actv, string bcode)
        {
            return new ProductManager().updatePriceAndDiscount(price, product_id, disc, bdefault, actv, bcode);
        }
        public int addPriceAndDiscount(int locno, int productid, int packcode, string packdesc, int unit, int bdef, string barcode, decimal price, int disc, int active)
        {
            return new ProductManager().addPriceAndDiscount(locno, productid, packcode, packdesc, unit, bdef, barcode, price, disc, active);
        }
        public DataSet getStock()
        {
            return new ProductManager().getStock();
        }
        public DataSet getFeaturedProducts(int branch_id)
        {
            return new ProductManager().getFeaturedProducts(branch_id);
        }
        public DataSet searchProductsByFilters(string name, int min_price, int max_price, int subCategoryId)
        {
            return new ProductManager().searchProductsByFilters(name, min_price, max_price, subCategoryId);
        }
        public DataSet searchProducts(string name, int min_price, int max_price, int branchId)
        {
            return new ProductManager().searchProducts(name, min_price, max_price, branchId);
        }
        public int addStock(int branch_id, int product_id, int qty)
        {
            return new ProductManager().addStock(branch_id, product_id, qty);
        }
        public int addProduct(decimal cost, int product_id, string desc, string desclong, string uom)
        {
            return new ProductManager().addProduct(cost, product_id, desc, desclong, uom);
        }
        public int updateProduct(decimal cost, int product_id, string desc, string desclong, string uom)
        {
            return new ProductManager().updateProduct(cost, product_id, desc, desclong, uom);
        }
        public DataSet getHotProducts()
        {
            return new ProductManager().getHotProducts();
        }

        public DataSet getRecommendedProducts(int pageindex, int pageSize)
        {
            return new ProductManager().getRecommendedProducts(pageindex, pageSize);
        }

        public DataSet getSearchProducts(string str)
        {
            return new ProductManager().getSearchProducts(str);
        }

        public DataSet searchAllProducts(string name, int min_price, int max_price, int branchId)
        {
            return new ProductManager().searchAllProducts(name, min_price, max_price, branchId);
        }
        #endregion

        #region USER
        public DataSet GetUserById(int userId)
        {
            return new UserManager().GetUserById(userId);
        }
        public DataSet GetAllAddressesByCustomerId(int userId)
        {
            return new UserManager().GetAllAddressesByCustomerId(userId);
        }
        public USER AuthenticateUser(string username, string password, int userTypeID = 0)
        {
            return new UserManager().AuthenticateUser(username, password, userTypeID);
        }
        public USER authenticateUserByMobile(string mobile, int userTypeID)
        {
            return new UserManager().authenticateUserByMobile(mobile, userTypeID);
        }
        public DataSet RegisterCustomer(USER u)
        {
            return new UserManager().RegisterCustomer(u);
        }
        public USER RegisterUser(UserRegisterDto u)
        {
            return new UserManager().RegisterUser(u);
        }
        //public DataSet saveCustomer(TEMP_CUSTOMERS customer)
        //{
        //    return new UserManager().saveCustomer(customer);
        //}
        public USER_ADDRESSES saveNewAddress(USER_ADDRESSES customer)
        {
            return new UserManager().saveNewAddress(customer);
        }
        public DataSet ifMobileExist(string mobile)
        {
            return new UserManager().ifMobileExist(mobile);
        }
        public DataSet ifMobileExist(string mobile, string email)
        {
            return new UserManager().ifMobileExist(mobile, email);
        }
        public DataSet ifUserNameExist(string uname)
        {
            return new UserManager().ifUserNameExist(uname);
        }
        public DataSet GetUserType(string userTypeName)
        {
            return new UserManager().GetUserType(userTypeName);
        }
        public int updateDeviceId(string deviceId, string userId)
        {
            return new UserManager().updateDeviceId(deviceId, userId);
        }
        public DataSet getUserFCMToken(string userId)
        {
            return new UserManager().getUserFCMToken(userId);
        }
        public string getUserMobile(string userId)
        {
            return new UserManager().getUserMobile(userId);
        }
        public DataSet editProfile(USER user)
        {
            return new UserManager().editProfile(user);
        }
        public DataSet editAddress(USER_ADDRESSES address)
        {
            return new UserManager().editAddress(address);
        }
        public DataSet deleteAddress(USER_ADDRESSES address)
        {
            return new UserManager().deleteAddress(address);
        }
        public USER_FAVOURITES addFavouriteProducts(USER_FAVOURITES data)
        {
            return new UserManager().addFavouriteProducts(data);
        }
        public DataSet deleteFavouriteProducts(USER_FAVOURITES data)
        {
            return new UserManager().deleteFavouriteProducts(data);
        }
        public DataSet getAllFavourites(USER_FAVOURITES data)
        {
            return new UserManager().getAllFavourites(data);
        }
        public DataSet getSpecificFavourites(USER_FAVOURITES data)
        {
            return new UserManager().getSpecificFavourites(data);
        }
        public DataSet resetPassword(USER u)
        {
            return new UserManager().resetPassword(u);
        }
        #endregion

        #region ORDER
        public int saveOrder(ORDER order)
        {
            return new OrderManager().saveOrder(order);
        }

        public int saveOrderProducts(ORDER_PRODUCTS orderProduct)
        {
            return new OrderManager().saveOrderProducts(orderProduct);
        }
        public DataSet saveOrder2(ORDER order)
        {
            return new OrderManager().saveOrder2(order);
        }
        public DataSet saveOrderProducts2(ORDER_PRODUCTS orderProduct)
        {
            return new OrderManager().saveOrderProducts2(orderProduct);
        }
        public OrderProductsDto saveOrderWithProducts(OrderDto order, List<ORDER_PRODUCTS> orderProduct)
        {
            return new OrderManager().saveOrderWithProducts(order, orderProduct);
        }
        public DataSet getCustomerPreviousOrders(int customerId, int index)
        {
            return new OrderManager().getCustomerPreviousOrders(customerId, index);
        }
        public DataSet getCustomerPreviousCompletedOrders(int customerId, int index)
        {
            return new OrderManager().getCustomerPreviousCompletedOrders(customerId, index);
        }
        public DataSet getAllCustomerOrders(int customerId, int index)
        {
            return new OrderManager().getAllCustomerOrders(customerId, index);
        }
        public DataSet getOrderProducts(int orderId)
        {
            return new OrderManager().getOrderProducts(orderId);
        } public DataSetDto getCustomerProductsByOrderId(int orderId)
        {
            return new OrderManager().getCustomerProductsByOrderId(orderId);
        }
        public DataSet getOrderProductsByStatus(int status)
        {
            return new OrderManager().getOrderProductsByStatus(status);
        }
        public int assignRider(int rider_id, int user_id, int orderID, string pTime)
        {
            return new OrderManager().assignRider(rider_id, user_id, orderID, pTime);
        }
        //public int rejectByManager(int orderId, string reason)
        //{
        //    return new OrderManager().rejectByManager(orderId, reason);
        //}

        public int updateOrderStatus(int orderId, int currentStatus, int userId)
        {
            return new OrderManager().updateOrderStatus(orderId, currentStatus, userId);
        }
        public int updateOrderProducts(ORDER_PRODUCTS orderProducts)
        {
            return new OrderManager().updateOrderProducts(orderProducts);
        }
        public DataSet getBranchOrders(int branchId)
        {
            return new OrderManager().getBranchOrders(branchId);
        }
        public DataSet getBranchNewOrders(int branchId, int index)
        {
            return new OrderManager().getBranchNewOrders(branchId, index);
        }
        public DataSet getBranchProcessOrders(int branchId, int index)
        {
            return new OrderManager().getBranchProcessOrders(branchId, index);
        }
        public DataSet getBranchDispatchedOrders(int branchId, int index)
        {
            return new OrderManager().getBranchDispatchedOrders(branchId, index);
        }
        public bool deleteOrderProducts(ORDER_PRODUCTS orderProducts)
        {
            return new OrderManager().deleteOrderProducts(orderProducts);
        }
        public DataSet getOrderStats(int orderId)
        {
            return new OrderManager().getOrderStats(orderId);
        }
        public DataSet getOrdersByOrderId(int orderId)
        {
            return new OrderManager().getOrdersByOrderId(orderId);
        }
        public DataSet getRiderOrders(int orderId, int rider_id)
        {
            return new OrderManager().getRiderOrders(orderId, rider_id);
        }

        public string getRiderOrderStatus(int riderId, int orderId)
        {
            return new OrderManager().UpdateRiderOrderStatus(riderId, orderId);
        }

        public object GetOrderTotalAmount(int orderID)
        {
            return new OrderManager().GetOrderTotalAmount(orderID);
        }
        public int updateDiscount(int disc, int user_id, int orderID)
        {
            return new OrderManager().updateDiscount(disc, user_id, orderID);
        }
        public DataSet redeemCode(string code)
        {
            return new OrderManager().redeemCode(code);
        }
        public int updateCoupon(int couponId)
        {
            return new OrderManager().updateCoupon(couponId);
        }
        public DataSet managerFCMToken(string branchId)
        {
            return new OrderManager().managerFCMToken(branchId);

        }
        public int rejectByManager(int orderId, string reason)
        {
            return new OrderManager().rejectByManager(orderId, reason);
        }
        #endregion

        #region DRIVER
        public int markDelivered(int orderId)
        {
            return new DriverManager().markDelivered(orderId);
        }
        public int markBounced(int orderId, string reason)
        {
            return new DriverManager().markBounced(orderId, reason);
        }

        public int rejectByDriver(int orderId, string reason)
        {
            return new DriverManager().rejectByDriver(orderId, reason);
        }
        public DataSet getPreviousRides(int userId)
        {
            return new DriverManager().getPreviousRides(userId);
        }
        public DataSet getAllBranchRiders(int branchId)
        {
            return new DriverManager().getAllBranchRiders(branchId);
        }
        public DataSet getAllRiders()
        {
            return new DriverManager().getAllRiders();
        }
        public DataSet getAvailableBranchRiders(int branchId)
        {
            return new DriverManager().getAvailableBranchRiders(branchId);
        }
        public DataSet getAvailableRiders()
        {
            return new DriverManager().getAvailableRiders();
        }
        public DataSet getRides(int user_id)
        {
            return new DriverManager().getRides(user_id);
        }
        public DataSet searchByRiderName(string name)
        {
            return new DriverManager().searchByRiderName(name);
        }
        #endregion
    }


}

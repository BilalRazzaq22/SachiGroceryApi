
using SASTI.DataLayer;
using SASTI.DataLayer.Repository;
using SASTI.DataLayer.UnitOfWork;
using SASTI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.BusinessLayer
{
    public abstract class AbstractFactory
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly Lazy<GenericRepository<USER>> _LazyUser;
        private readonly Lazy<GenericRepository<BANNER>> _LazyBanner;
        private readonly Lazy<GenericRepository<GROUP>> _LazyGroup;
        private readonly Lazy<GenericRepository<PRODUCT>> _Lazyproduct;
        private readonly Lazy<GenericRepository<CATEGORy>> _LazyCategory;
        private readonly Lazy<GenericRepository<SUB_CATEGORIES>> _LazySubCat;
        private readonly Lazy<GenericRepository<PRODUCT_IMAGES>> _LazyProductImage;
        private readonly Lazy<GenericRepository<RIDER_ORDER>> _RiderOrder;
        private readonly Lazy<GenericRepository<USER_FAVOURITES>> _UserFavouriteProduct;
        private readonly Lazy<GenericRepository<COUPON>> _Coupon;
        private readonly Lazy<GenericRepository<BARCODE>> _Barcode;
        private readonly Lazy<GenericRepository<RECOMMENDED_PRODUCTS>> _RecommendedProducts;
        private readonly Lazy<GenericRepository<USER_DEVICES>> _UserDevices;
        //private readonly Lazy<GenericRepository<UserReservation>> _LazyReservation;
        //private readonly Lazy<GenericRepository<UserNotification>> _LazyNotify;
        protected readonly Lazy<SpRepository> _LazySp;
        public AbstractFactory()
        {
            _unitOfWork = new UnitOfWork();

            _LazyUser = new Lazy<GenericRepository<USER>>(() => new GenericRepository<USER>(_unitOfWork));
            _LazyBanner = new Lazy<GenericRepository<BANNER>>(() => new GenericRepository<BANNER>(_unitOfWork));
            _LazyGroup = new Lazy<GenericRepository<GROUP>>(() => new GenericRepository<GROUP>(_unitOfWork));
            _Lazyproduct = new Lazy<GenericRepository<PRODUCT>>(() => new GenericRepository<PRODUCT>(_unitOfWork));
            _LazyCategory = new Lazy<GenericRepository<CATEGORy>>(() => new GenericRepository<CATEGORy>(_unitOfWork));
            _LazySubCat = new Lazy<GenericRepository<SUB_CATEGORIES>>(() => new GenericRepository<SUB_CATEGORIES>(_unitOfWork));
            _LazyProductImage = new Lazy<GenericRepository<PRODUCT_IMAGES>>(() => new GenericRepository<PRODUCT_IMAGES>(_unitOfWork));
            _RiderOrder = new Lazy<GenericRepository<RIDER_ORDER>>(() => new GenericRepository<RIDER_ORDER>(_unitOfWork));
            _UserFavouriteProduct = new Lazy<GenericRepository<USER_FAVOURITES>>(() => new GenericRepository<USER_FAVOURITES>(_unitOfWork));
            _Coupon = new Lazy<GenericRepository<COUPON>>(() => new GenericRepository<COUPON>(_unitOfWork));
            _Barcode = new Lazy<GenericRepository<BARCODE>>(() => new GenericRepository<BARCODE>(_unitOfWork));
            _RecommendedProducts = new Lazy<GenericRepository<RECOMMENDED_PRODUCTS>>(() => new GenericRepository<RECOMMENDED_PRODUCTS>(_unitOfWork));
            _UserDevices = new Lazy<GenericRepository<USER_DEVICES>>(() => new GenericRepository<USER_DEVICES>(_unitOfWork));
            //_LazyBoard = new Lazy<GenericRepository<Board>>(() => new GenericRepository<Board>(_unitOfWork));
            //_LazyReservation = new Lazy<GenericRepository<UserReservation>>(() => new GenericRepository<UserReservation>(_unitOfWork));

            _LazySp = new Lazy<SpRepository>();
        }

        protected GenericRepository<USER> _user { get { return _LazyUser.Value; } }
        protected GenericRepository<BANNER> _banner { get { return _LazyBanner.Value; } }
        protected GenericRepository<GROUP> _group { get { return _LazyGroup.Value; } }
        protected GenericRepository<PRODUCT> _products{ get { return _Lazyproduct.Value; } }
        protected GenericRepository<CATEGORy> _cateogry { get { return _LazyCategory.Value; } }
        protected GenericRepository<SUB_CATEGORIES> _subCategory { get { return _LazySubCat.Value; } }
        protected GenericRepository<PRODUCT_IMAGES> _productImages { get { return _LazyProductImage.Value; } }
        protected GenericRepository<RIDER_ORDER> _riderOrder { get { return _RiderOrder.Value; } }
        protected GenericRepository<USER_FAVOURITES> _userFavouriteProduct { get { return _UserFavouriteProduct.Value; } }
        protected GenericRepository<COUPON> _coupon { get { return _Coupon.Value; } }
        protected GenericRepository<BARCODE> _barcode { get { return _Barcode.Value; } }
        protected GenericRepository<RECOMMENDED_PRODUCTS> _recommendedProducts { get { return _RecommendedProducts.Value; } }
        protected GenericRepository<USER_DEVICES> _userDevices { get { return _UserDevices.Value; } }
        //protected GenericRepository<Board> _board { get { return _LazyBoard.Value; } }
        //protected GenericRepository<UserReservation> _userreservation { get { return _LazyReservation.Value; } }
        protected SpRepository _sp { get { return _LazySp.Value; } }
        public string GenRandomReferrerCodes()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            //var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        //public object GetPolicyParameterValue(string key)
        //{
        //    var obj = _policyParameter.Repository.FirstOrDefault(p => p.ParameterName == key);
        //    if (obj != null)
        //    {
        //        return obj.ParameterValue;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
       
   
        public DateTime? GetESTDateTime(DateTime utcDateTime)
        {
            try
            {
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime? easternTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, easternZone);
                return easternTime;
            }
            catch (Exception)
            {
                DateTime? dt = null;
                return dt;
            }
        }
        public DateTime? GetESTDateTime(DateTime? utcDateTime)
        {
            try
            {
                DateTime datetimeutc =Convert.ToDateTime(utcDateTime);
                TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                DateTime? easternTime = TimeZoneInfo.ConvertTimeFromUtc(datetimeutc, easternZone);
                return easternTime;
            }
            catch (Exception)
            {
                DateTime? dt = null;
                return dt;
            }
        }

    }
}

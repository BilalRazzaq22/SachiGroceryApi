using SASTI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.DataLayer
{
    public class SpRepository
    {
        SASTIEntities db;
        //public int InsertStation(string StationName,string Email,string Password,string Address,string City,string State,string Zip,string Phone,string ImageUrl,decimal? latitude,decimal? longitude,string RecordStatus,decimal? SalesTax,bool? IsPrivate)
        //{
        //    ObjectParameter OutParam = new ObjectParameter("OutParam", typeof(int));
        //    using (db = new SASTIEntities())
        //    {
        //        db.SpInsertStation(StationName, Email, Password, Address, City, State, Zip, Phone, ImageUrl, latitude, longitude, RecordStatus, DateTime.UtcNow, DateTime.UtcNow, SalesTax, IsPrivate, OutParam);
        //        return Convert.ToInt32(OutParam.Value);
        //    }
        //}
        //public int UpdateStation(int StationId, string StationName, string Address, string City, string State, string Zip, string Phone, string ImageUrl, decimal? latitude, decimal? longitude, string RecordStatus, decimal? SalesTax, bool? IsPrivate)
        //{
        //    using (db = new SASTIEntities())
        //    {
        //        return db.SpUpdateStation(StationId, StationName, Address, City, State, Zip, Phone, ImageUrl, latitude, longitude, RecordStatus, DateTime.UtcNow, SalesTax, IsPrivate);
        //    }
        //}

        //public List<SpGetAllStations_Result> GetAllStations(int PageIndex, int PageSize, string SortColumn, string SortOrder, int Stationid,string StationName)
        //{
        //    using (db = new SASTIEntities())
        //    {
        //        return db.SpGetAllStations(PageIndex, PageSize, SortColumn, SortOrder, Stationid, StationName).ToList();
        //    }
        //}
        //public List<SpGetAllBoardsByStationId_Result> GetAllBoardsByStationId(int StationId)
        //{
        //    using (db = new SASTIEntities())
        //    {
        //        return db.SpGetAllBoardsByStationId(StationId).ToList();
        //    }
        //}
      
    }
}

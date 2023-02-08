//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class ITEMINFO
    {
        public int ITEM_CODE { get; set; }
        public string HV_CODE { get; set; }
        public Nullable<byte> ITEM_TYPE { get; set; }
        public Nullable<byte> ITEM_ATTRIB { get; set; }
        public Nullable<bool> IsRECIPE { get; set; }
        public Nullable<bool> SERIALIZED { get; set; }
        public Nullable<bool> EXPIRY_ITEM { get; set; }
        public Nullable<bool> FEATURED { get; set; }
        public string UOM { get; set; }
        public Nullable<decimal> BATCH_QTY { get; set; }
        public Nullable<short> DEPT_CODE { get; set; }
        public Nullable<short> GRCODE { get; set; }
        public Nullable<short> SUBGRCODE { get; set; }
        public Nullable<int> CATCODE { get; set; }
        public Nullable<int> BRAND_CODE { get; set; }
        public Nullable<int> DESIGN_CD { get; set; }
        public Nullable<short> CLR_CODE { get; set; }
        public string SUPP_CODE { get; set; }
        public Nullable<short> MAKE_CODE { get; set; }
        public Nullable<int> SIZE_CODE { get; set; }
        public Nullable<int> AUTH_CODE { get; set; }
        public Nullable<int> PUB_CODE { get; set; }
        public string EDITION { get; set; }
        public string CLASS { get; set; }
        public string ISBN { get; set; }
        public string ITEM_DESC { get; set; }
        public string ITEM_DESC_LONG { get; set; }
        public Nullable<decimal> COST_MARGIN { get; set; }
        public string SEX { get; set; }
        public string SEASON { get; set; }
        public string AGE { get; set; }
        public string FABRIC { get; set; }
        public string SPEED { get; set; }
        public string PLY { get; set; }
        public string PCD { get; set; }
        public string HOLES { get; set; }
        public string SPH { get; set; }
        public string CYL { get; set; }
        public string AXIS { get; set; }
        public string ADDS { get; set; }
        public string COMMENT { get; set; }
        public Nullable<bool> EXEMPT { get; set; }
        public Nullable<decimal> VAT { get; set; }
        public Nullable<decimal> GST { get; set; }
        public Nullable<bool> OPEN_PRICE { get; set; }
        public Nullable<bool> FRACTIONAL { get; set; }
        public Nullable<bool> ALLOWQTY { get; set; }
        public Nullable<decimal> MATERIAL_COST { get; set; }
        public Nullable<decimal> INGR_COST { get; set; }
        public Nullable<decimal> PKG_COST { get; set; }
        public Nullable<decimal> LAB_COST { get; set; }
        public Nullable<decimal> OH_COST { get; set; }
        public Nullable<decimal> OTH_COST { get; set; }
        public Nullable<decimal> WASTAGE { get; set; }
        public Nullable<decimal> COST_PRICE1 { get; set; }
        public Nullable<decimal> COST_PRICE2 { get; set; }
        public Nullable<decimal> COST_PRICE3 { get; set; }
        public Nullable<decimal> NET_COST { get; set; }
        public Nullable<decimal> AVG_COST { get; set; }
        public Nullable<decimal> AVG_COST1 { get; set; }
        public Nullable<decimal> AVG_COST2 { get; set; }
        public Nullable<decimal> AVG_COST3 { get; set; }
        public Nullable<decimal> F_PUR_PRICE { get; set; }
        public Nullable<decimal> LAST_PUR_PRICE1 { get; set; }
        public Nullable<decimal> LAST_PUR_PRICE2 { get; set; }
        public Nullable<decimal> LAST_PUR_PRICE3 { get; set; }
        public string LAST_SUPP { get; set; }
        public string LAST_SUPP1 { get; set; }
        public string LAST_SUPP2 { get; set; }
        public Nullable<decimal> FREE_QTY { get; set; }
        public Nullable<decimal> FREE_QTY_AVG_COST { get; set; }
        public Nullable<decimal> ITEM_DISC { get; set; }
        public Nullable<bool> bDISCOUNTED { get; set; }
        public Nullable<decimal> DISC_QTY { get; set; }
        public Nullable<decimal> SALE_DISC { get; set; }
        public Nullable<System.DateTime> CDATE { get; set; }
        public Nullable<int> CUSER { get; set; }
        public Nullable<System.DateTime> MDATE { get; set; }
        public Nullable<int> MUSER { get; set; }
        public Nullable<bool> EMPTY { get; set; }
        public Nullable<bool> bNEW { get; set; }
        public Nullable<bool> NeedsReplication { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SASTI.Models
{
    public class OrderProductsDto
    {
        public OrderProductsDto()
        {
            Barcodes = new List<BARCODE>();
            OrderProducts = new List<ORDER_PRODUCTS>();
            Products = new List<PRODUCT>();
        }
        public int TotalItems { get; set; }
        public decimal TotalPrice { get; set; }
        public int? Rider_Id { get; set; }
        public ORDER Order { get; set; }
        public List<BARCODE> Barcodes { get; set; }
        public List<ORDER_PRODUCTS> OrderProducts { get; set; }
        public List<PRODUCT> Products { get; set; }
    }
}
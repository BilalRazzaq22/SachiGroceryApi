using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SASTI.DataAccess
{
    public class PhoneOrder
    {
        int product_id { get; set; }
        string product_name { get; set; }
        int amountOrdered { get; set; }
    }
}
using SASTI.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.Models.Models.Response
{
    public class SaveOrderResponse
    {
        public OrderDto order { get; set; }
        public List<ORDER_PRODUCTS> orderProduct { get; set; }
    }
}

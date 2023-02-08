using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.Models.Models.Response
{
    public class CustomerProductResponse
    {
        public CustomerProductResponse()
        {
            CustomerProducts = new List<ProductResponse>();
        }
        public List<ProductResponse> CustomerProducts { get; set; }
        public List<GROUP> Groups { get; set; }
        public List<BANNER> Banners { get; set; }
    }
}

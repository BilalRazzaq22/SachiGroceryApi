using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.Models.Models.Response
{
    public class RecommendedProductResponse
    {
        public RecommendedProductResponse()
        {
            RecommendedProducts = new List<ProductResponse>();
        }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int NextPage { get; set; }
        public List<ProductResponse> RecommendedProducts { get; set; }
    }
}

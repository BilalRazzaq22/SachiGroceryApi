using SASTI.Models.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.Models.Models
{
    public class LoadHomeDataResponse
    {
        public List<ProductResponse> HotProducts { get; set; }
        public List<GROUP> Groups { get; set; }
        public List<BANNER> Banners { get; set; }
    }
}

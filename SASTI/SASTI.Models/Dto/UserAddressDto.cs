using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.Models.Dto
{
    public class UserAddressDto
    {
        public int UserId { get; set; }
        public List<address> UserAddresses { get; set; }
    }

    public class address
    {
        public string Name { get; set; }
        public int BranchId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}

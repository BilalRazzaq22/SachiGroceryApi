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
    }
}

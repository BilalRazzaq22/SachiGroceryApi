using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SASTI.Models.Dto
{
    public class DataSetDto
    {
        public DataSetDto()
        {
            Response = new ResponseDto();
        }
        public ResponseDto Response { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PSTokenView:ResponseBase
    {
        public int UserID { get; set; }
        public string UserAccount { get; set; }
        public List<string> FunctionCodes { get; set; }
    }
}

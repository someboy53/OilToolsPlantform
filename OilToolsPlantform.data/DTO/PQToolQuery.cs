using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PQToolQuery : ListRequestBase
    {
        public Nullable<int> CatSID { get; set; }
        public string SearchStr { get; set; }
        public string ToolName { get; set; }
        public string CatSName { get; set; }
        public string CatFName { get; set; }
    }
}

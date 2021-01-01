using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PQCaseQuery:ListRequestBase
    {
        public Nullable<int> CatSID { get; set; }
        public string CaseContent { get; set; }
        public string ToolName { get; set; }
        public string CatSName { get; set; }
        public string CatFName { get; set; }
    }
}
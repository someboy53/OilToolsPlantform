using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PSOrgView:ViewResponseBase
    {
        public int OrgID { get; set; }
        public string OrgName { get; set; }
        public int ParentID { get; set; }
        public string ParentName { get; set; }
        public string FullPath { get; set; }
        public string Description { get; set; }
    }
}

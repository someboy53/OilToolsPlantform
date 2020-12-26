using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PQOrgModify:RequestBase
    {
        public int OrgID { get; set; }
        public string OrgName { get; set; }
        public int ParentID { get; set; }
        public string Description { get; set; }
        public string IsDel { get; set; }

        public PQOrgModify() { 
            this.IsDel = "0";
        }
    }
}

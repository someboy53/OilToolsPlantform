using System.Collections.Generic;

namespace OilToolsPlantform.Data.DTO
{
    public class PSRightView:ResponseBase
    {
        public string FunctionCode { get; set; }
        public List<DTO.LRole> Roles { get; set; }

        public List<int> RoleIDs { get; set; }
        public PSRightView()
        {
            this.Roles = new List<LRole>();
            this.RoleIDs = new List<int>();
        }
    }
}
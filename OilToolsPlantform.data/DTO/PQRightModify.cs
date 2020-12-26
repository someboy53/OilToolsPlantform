using System.Collections.Generic;
namespace OilToolsPlantform.Data.DTO
{
    public class PQRightModify : RequestBase
    {
        public string FunctionCode { get; set; }
        public List<KeyValuePair<int,string>> RoleIDs { get; set; }
        public PQRightModify() {
            this.RoleIDs = new List<KeyValuePair<int, string>>();
        }
    }
}
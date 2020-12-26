using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PSUserView:ViewResponseBase
    {
        public int UserID { get; set; }

        public string UserAccount { get; set; }

        public string UserName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string LastLoginTime { get; set; }

        public string CreateTime { get; set; }

        public string UpdateTime { get; set; }

        public int OrgID { get; set; }

        public string FullPath { get; set; }

        public int RoleID { get; set; }

        public string RoleName { get; set; }

        public string CellPhone { get; set; }

        public string Email { get; set; }

        public string WorkNumber { get; set; }

        public List<System.Collections.Generic.KeyValuePair<int, string>> RoleList { get; set; }

        public List<System.Collections.Generic.KeyValuePair<int, string>> OrgList { get; set; }

        public PSUserView()
        {
            this.RoleList = new List<KeyValuePair<int, string>>();
            this.OrgList = new List<KeyValuePair<int, string>>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PQUserQuery : ListRequestBase
    {
        public string UserName { get; set; }

        public string UserAccount { get; set; }

        public string OrgName { get; set; }

        public string Cellphone { get; set; }

        public string WorkNumber { get; set; }
        public string Enabled { get; set; }
    }
}

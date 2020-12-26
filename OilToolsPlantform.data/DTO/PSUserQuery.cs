using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PSUserQuery : ListResponseBase
    {

        public List<LUser> data { get; set; }

        public PSUserQuery()
        {
            this.data = new List<LUser>();
        }
    }
}

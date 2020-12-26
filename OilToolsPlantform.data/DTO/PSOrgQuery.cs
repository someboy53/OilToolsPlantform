using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PSOrgQuery:ListResponseBase
    {
        public List<Data.DTO.LOrg> data;

        public PSOrgQuery()
        {
            this.data = new List<LOrg>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PSCatQuery : ListResponseBase
    {
        public List<LCatQuery> catQueryList { get; set; }

        public PSCatQuery()
        {
            this.catQueryList = new List<LCatQuery>();
        }
    }
}

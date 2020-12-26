using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PSCatSQuery:ListResponseBase
    {
        public List<KeyValuePair<int, string>> data { get; set; }

        public PSCatSQuery()
        {
            this.data = new List<KeyValuePair<int, string>>();
        }
    }
}
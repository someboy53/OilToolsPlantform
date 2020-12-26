using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PQPasswordModify:RequestBase
    {
        public string OldPass { get; set; }

        public string NewPass1 { get; set; }

        public string NewPass2 { get; set; }
    }
}

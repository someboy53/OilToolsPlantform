using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PQCatQuery : ListRequestBase
    {
        public string Level { get; set; }
        public int CatFID { get; set; }
    }
}

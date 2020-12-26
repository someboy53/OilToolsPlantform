using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PQCatEnable:RequestBase
    {
        public int ID { get; set; }
        public int Level { get; set; }
        public Boolean Enabled { get; set; }
    }
}

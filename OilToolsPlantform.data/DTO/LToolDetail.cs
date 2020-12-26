using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class LToolDetail
    {

        public int ToolDetailID { get; set; }
        public int ToolID { get; set; }
        public int DetailIcon { get; set; }
        public string IconName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IsDel { get; set; }
    }
}

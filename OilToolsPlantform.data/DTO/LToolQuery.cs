using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class LToolQuery
    {
        public int ToolID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PicType { get; set; }
        public int PicID { get; set; }
        public string PicName { get; set; }
        public string StoreName { get; set; }
        public string Path { get; set; }
        public string ThambName { get; set; }

        public string CatFName { get; set; }

        public string CatSName { get; set; }

        public string CaseContent { get; set; }

        public Nullable<int> DetailCount { get; set; }

        public int Status { get; set; }

        public string Enabled { get; set; }

        public string CanAudit { get; set; }
    }
}

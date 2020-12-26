using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PQCatCreate : RequestBase
    {
        public int Level { get; set; }

        public int CatID { get; set; }

        public int ParentID { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public List<LPicture> Pictures { get; set; }
    }
}

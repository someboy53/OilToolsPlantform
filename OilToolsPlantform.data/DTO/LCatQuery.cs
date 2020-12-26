using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class LCatQuery
    {
        public int CatID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CatPicID { get; set; }
        public string PicType { get; set; }
        public int PicID { get; set; }
        public string PicName { get; set; }
        public string StoreName { get; set; }
        public string ThambName { get; set; }
        public string Path { get; set; }
    }
}

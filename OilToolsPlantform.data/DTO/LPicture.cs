using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class LPicture
    {
        public int PicID { get; set; }

        public string PicName { get; set; }

        public string StoreName { get; set; }

        public string Path { get; set; }

        public string ThambName { get; set; }

        public string PicType { get; set; }
        
        public string IsDel { get; set; }

        public LPicture() {
            PicType = "0";
            IsDel = "0";
        }
    }
}

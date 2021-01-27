using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PSToolView : ResponseBase
    {
        public int ToolID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<LPicture> PictureList { get; set; }
        public List<LToolDetail> ToolDetailList { get; set; }

        public int CatSID { get; set; }

        public int CatFID { get; set; }

        public List<System.Collections.Generic.KeyValuePair<int, string>> CatFList { get; set; }
        
        public List<System.Collections.Generic.KeyValuePair<int, string>> CatSList { get; set; }

        public List<string> AuditHist { get; set; }
        public PSToolView()
        {
            this.PictureList = new List<LPicture>();
            this.ToolDetailList = new List<LToolDetail>();
            this.AuditHist = new List<string>();
        }
    }
}

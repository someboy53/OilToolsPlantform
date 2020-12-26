using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PQToolModify : RequestBase
    {
        public int ToolID { get; set; }

        public int CatSID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int IntroID { get; set; }
        public int FuncID { get; set; }
        public int ParamID { get; set; }
        public int CaseID { get; set; }
        public int StockID { get; set; }
        public int ContactID { get; set; }
        public string ShowIntro { get; set; }
        public string IntroDesc { get; set; }
        public string ShowFunc { get; set; }
        public string FuncDesc { get; set; }
        public string ShowParam { get; set; }
        public string ParamDesc { get; set; }
        public string ShowCase { get; set; }
        public string CaseDesc { get; set; }
        public string ShowStock { get; set; }
        public string StockDesc { get; set; }
        public string ShowContact { get; set; }
        public string ContactDesc { get; set; }

        public List<LPicture> Pictures { get; set; }

        public List<LToolDetail> ToolDetails { get; set; }

        public string IsDel { get; set; }

        public PQToolModify()
        {
            this.Pictures = new List<LPicture>();
            this.ToolDetails = new List<LToolDetail>();
            this.IsDel = "0";
        }
    }
}

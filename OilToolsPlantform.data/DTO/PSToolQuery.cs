using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class PSToolQuery:ListResponseBase
    {
        /// <summary>
        /// 供前台使用的数据。
        /// </summary>
        public List<LToolQuery> ToolQueryList { get; set; }
        
        /// <summary>
        /// 供后台使用的数据
        /// </summary>
        public List<LToolQuery> data { get; set; }
        public string CatSName { get; set; }

        public PSToolQuery()
        {
            this.ToolQueryList = new List<LToolQuery>();
            this.data = new List<LToolQuery>();
        }
    }
}

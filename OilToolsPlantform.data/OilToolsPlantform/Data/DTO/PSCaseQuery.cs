using System.Collections.Generic;

namespace OilToolsPlantform.Data.DTO
{
    public class PSCaseQuery:ListResponseBase
    {
        /// <summary>
        /// 供后台使用的数据
        /// </summary>
        public List<LToolQuery> data { get; set; }

        public PSCaseQuery()
        {
            this.data = new List<LToolQuery>();
        }
    }
}
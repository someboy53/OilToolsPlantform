using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class ListResponseBase:ResponseBase
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int MaxPage { get; set; }
        /// <summary>
        /// 总数据数
        /// </summary>
        public int MaxNum { get; set; }

        public int count { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    public class ListRequestBase:RequestBase
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页的行数
        /// </summary>
        public int PageRow { get; set; }

        public ListRequestBase()
        {
            this.Page = 1;
            this.PageRow = -1;
        }
    }
}

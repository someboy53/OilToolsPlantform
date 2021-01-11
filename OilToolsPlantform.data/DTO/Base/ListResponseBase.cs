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

        public string CanModify { get; set; }
        public string CanView { get; set; }
        public string CanAudit { get; set; }

        public ListResponseBase()
        {
            this.CanModify = "0";
            this.CanAudit = "0";
            this.CanView = "0";
        }

        public void SetRight(List<string> functionCodes,string func)
        {
            if (functionCodes.Contains(func + "_modify"))
                this.CanModify = "1";
            if (functionCodes.Contains(func + "_audit"))
                this.CanAudit = "1";
            if (functionCodes.Contains(func + "_view"))
                this.CanView = "1";
        }
    }
}

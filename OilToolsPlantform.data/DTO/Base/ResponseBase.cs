using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    [Serializable]
    public class ResponseBase
    {
        public ResponseBase()
        {
            this.CurTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.ErrorCode = "A_0";
        }
        /// <summary>
        /// 错误代码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 当前时间,如取得,返回时间字符串,如未取得,返回空
        /// </summary>
        public string CurTime { get; set; }
        public string code { get; set; }
        public string msg { get; set; }
    }
}

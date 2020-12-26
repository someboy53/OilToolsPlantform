using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    [Serializable]
    public class RequestBase
    {
        public RequestBase()
        {
            //this.clientInfo = new ClientInfo();
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UID
        { get; set; }

        /// <summary>
        /// 账户号
        /// </summary>
        public string AccountNumber { get; set; }

        /// <summary>
        /// 请求页面地址
        /// </summary>
        public string CurrentPageURL { get; set; }

        /// <summary>
        /// 客户端信息
        /// </summary>
        //public ClientInfo clientInfo { get; set; }
    }

}

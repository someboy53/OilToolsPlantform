using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DTO
{
    //正确时返回的json对象
    //{
    //    "total":2,
    //    "count":2,
    //    "data":{
    //    "openid":["OPENID1","OPENID2"]},
    //    "next_openid":"NEXT_OPENID",
    //    "errcode":40013,
    //    "errmsg":"invalid appid"
    //}
    public class WCUserList
    {
        public int total { get; set; }
        public int count { get; set; }
        public OpenIDs data { get; set; }
        public string next_openid { get; set; }
        public int errcode { get; set; }
        public string errmsg{ get; set; }
    }
    public class OpenIDs
    {
        public List<string> openid;
        public OpenIDs()
        {
            this.openid = new List<string>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using com.Origin.CommonLibrary.Framework;
using OilToolsPlantform.Data.DTO;

namespace OilToolsPlantform.Data.BLL
{
    /// <summary>
    /// 其它杂项
    /// </summary>
    public class cOtherBLL : BllBase
    {
        #region 首页相关

        public DTO.PSWelcomeView WelcomeView(DTO.PQWelcomeView request)
        {
            DTO.PSWelcomeView response = new DTO.PSWelcomeView();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    //查询数量
                    DAL.cOtherDAL dal = new DAL.cOtherDAL(con);
                    dal.CountQueryBuild();
                    dal.SetSBPage("select isnull(sum(te.ViewCount),0) ViewCount,isnull(sum(te.LikeCount),0) LikeCount,isnull(sum(te.FavCount),0) FavCount,isnull(sum(te.CommentCount),0) CommentCount,isnull(sum(1),0) ToolCount,(select count(1) from tbWechatFan where subscribe='1') FanCount from tbToolExt te");
                    response = dal.Query<DTO.PSWelcomeView>()[0];

                    dal.AnaQueryBuild();
                    response.data = dal.Query<DTO.LCountAna>();
                    //修改
                    response.ErrorCode = "A_0";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cOtherBLL.WelcomeView出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        public DTO.PSWechatFanQuery WechatFanQuery(DTO.PQWechatFanQuery request)
        {
            DTO.PSWechatFanQuery response = new DTO.PSWechatFanQuery();
            try
            {
                DAL.cOtherDAL dal = new DAL.cOtherDAL(con);
                dal.WechatFanQueryBuild(request);
                response.data = dal.Query<DTO.LWechatFanQuery>();
                int num = dal.Count();
                response.Page = request.Page;
                response.MaxNum = num;
                response.count = num;
                response.MaxPage = MathExpansion.CaculatPage(num, request.PageRow);

                response.ErrorMessage = rm.GetString(response.ErrorCode);
                response.code = response.ErrorCode == "A_0" ? "0" : response.ErrorCode;
                response.msg = response.ErrorMessage;
            }
            catch (Exception ex)
            {
                LogHelper.Error("cOtherBLL.WechatFanQuery！", ex);
                throw;
            }
            return response;
        }

        public DTO.PSLogQuery LogQuery(DTO.PQLogQuery request)
        {
            DTO.PSLogQuery response = new DTO.PSLogQuery();
            try
            {
                DAL.cOtherDAL dal = new DAL.cOtherDAL(con);
                dal.LogQueryBuild(request);
                response.data = dal.Query<DTO.LLog>();
                int num = dal.Count();
                response.Page = request.Page;
                response.MaxNum = num;
                response.count = num;
                response.MaxPage = MathExpansion.CaculatPage(num, request.PageRow);

                response.ErrorMessage = rm.GetString(response.ErrorCode);
                response.code = response.ErrorCode == "A_0" ? "0" : response.ErrorCode;
                response.msg = response.ErrorMessage;
            }
            catch (Exception ex)
            {
                LogHelper.Error("cOtherBLL.LogQuery！", ex);
                throw;
            }
            return response;
        }

        private const string GET_ACCESS_TOKEN_URL = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        private const string GET_USER_LIST_URL = "https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}";
        private const string GET_USER_INFO_URL = "https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token={0}";
        private const string GET_USER_INFO_REQUEST = "{\"user_list\":[{0}]}";
        private const string GET_USER_INFO_REQUEST_DETAIL = "{\"openid\":\"{0}\",\"lang\":\"zh_CN\"}";

        /// <summary>
        /// 取微信粉丝，先取列表，再更新详情
        /// </summary>
        /// <returns></returns>
        public ResponseBase PullFans()
        {
            ResponseBase response = new ResponseBase();
            response.ErrorCode = "A_0";
            try
            {
                string temp = string.Empty;
                temp = HttpAspxPostHtmlInfo(string.Format(GET_ACCESS_TOKEN_URL, System.Configuration.ConfigurationManager.AppSettings["APPID"], System.Configuration.ConfigurationManager.AppSettings["APPSECRET"]), "", "get");
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                ht = (System.Collections.Hashtable)PluSoft.Utils.JSON.Decode(temp, ht.GetType());
                if (!ht.ContainsKey("access_token"))
                {
                    response.ErrorCode = "A_" + ht["errcode"].ToString();
                    response.ErrorMessage = ht["errmsg"].ToString();
                    return response;
                }
                string nextOpenID = "";
                int curCount = 0;
                temp = HttpAspxPostHtmlInfo(string.Format(GET_USER_LIST_URL, ht.ContainsKey("access_token"), nextOpenID), "", "get");
                DTO.WCUserList userList = new WCUserList();
                userList = (DTO.WCUserList)PluSoft.Utils.JSON.Decode(temp, userList.GetType());
                DAL.cOtherDAL dal = new DAL.cOtherDAL(con);
                curCount += userList.count;
                //循环取OpenID列表并写入数据库
                while (userList.total > curCount)
                {
                    nextOpenID = userList.next_openid;
                    temp = HttpAspxPostHtmlInfo(string.Format(GET_USER_LIST_URL, ht.ContainsKey("access_token"), nextOpenID), "", "get");
                    userList = (DTO.WCUserList)PluSoft.Utils.JSON.Decode(temp, userList.GetType());
                    dal.AddOpenIDList(userList.data.openid);
                    curCount += userList.count;
                }
                //循环从数据库中读取1周未更新的前三十条openID，并更新其信息
                List<string> openIDList = dal.GetOpenIDListNeedUpdate(7);
                StringBuilder sb = new StringBuilder();
                while (openIDList.Count > 0)
                {
                    sb.Append("{\"user_list\":[");
                    //组装openID列表
                    foreach (string openID in openIDList)
                    {
                        sb.AppendFormat(GET_USER_INFO_REQUEST_DETAIL, openID);
                        sb.Append(",");
                    }
                    sb.Append("]}");
                    temp = HttpAspxPostHtmlInfo(string.Format(GET_USER_INFO_URL, ht.ContainsKey("access_token")), sb.ToString(), "post");
                    List<DTO.LWechatFanQuery> data = new List<LWechatFanQuery>();
                    data = (List<DTO.LWechatFanQuery>)JsonHelper.JsonToObject(temp, data.GetType());
                    dal.UpdateUserInfoList(data);
                    openIDList = dal.GetOpenIDListNeedUpdate(7);
                }
            }
            catch 
            {
                response.ErrorCode = "A_444444";
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        /// <summary>
        /// 多次调用Post请求返回 HTML信息
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postString"></param>
        /// <returns></returns>
        public static string HttpAspxPostHtmlInfo(string url, string postString, string type)
        {
            byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，事先要看下抓取网页的编码方式  
            System.Net.WebClient webClient = new System.Net.WebClient();
            if (type == "post")
                webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            byte[] responseData = webClient.UploadData(url, "POST", postData);//得到返回字符流  
            var retString = Encoding.UTF8.GetString(responseData);//解码返回请求的html 内容
            return retString;
        }

        #endregion
    }
}

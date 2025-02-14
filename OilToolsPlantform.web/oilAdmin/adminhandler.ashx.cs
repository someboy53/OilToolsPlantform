﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Text;
using System.Collections;

namespace OilToolsPlantform.oilAdmin
{
    /// <summary>
    /// adminhandler 的摘要说明
    /// </summary>
    public class adminhandler : IHttpHandler
    {
        #region 内部处理

        //upload不记录20201226
        private static List<string> methodList = new List<string>() { "Query","Modify","View","Audit","login" };

        private string jsonStr;

        public string AddJsonInfo(string strJson, string key, object value)
        {
            if (value is int || value is double || value is string || value is System.Text.StringBuilder || value is bool || value is decimal)
            {
                strJson = strJson.Insert(strJson.Length - 1, ",\"" + key + "\":\"" + value + "\"");
            }
            else
            {
                strJson = strJson.Insert(strJson.Length - 1, ",\"" + key + "\":" + PluSoft.Utils.JSON.Encode(value));
            }
            return strJson;
        }

        /// <summary>
        /// 主处理函数
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            string methodName = string.Empty;
            try
            {
                //if (!BaseData.isTrue)
                //{
                //    if (Session.Count > 10)
                //    {
                //        Session.RemoveAt(Session.Count - 1);
                //        return;
                //    }
                //}
                HttpRequest Request = context.Request;
                methodName = Request["method"];
                Type type = this.GetType();
                MethodInfo method = type.GetMethod(methodName);
                if (method == null) throw new Exception("method is null");
                jsonStr = Request["json"];
                if (string.IsNullOrEmpty(jsonStr))
                {
                    jsonStr = "{a:1}";
                }
                jsonStr = System.Web.HttpUtility.UrlDecode(jsonStr);
                //过滤掉登录方法和登录初始化方法, 验证认证
                Data.DTO.PSTokenView response = new Data.DTO.PSTokenView();
                if (methodName != "login" && methodName != "getFans")
                {
                    Data.DTO.PQTokenView request = new Data.DTO.PQTokenView();
                    request.token = Request["token"];
                    Data.BLL.cUserBLL client = new Data.BLL.cUserBLL();
                    response = client.TokenView(request);
                    if (response.ErrorCode != "A_0")
                    {
                        //说明token令牌不正确
                        context.Response.Write(this.Obj2Json(response));
                        return;
                    }
                    else
                    {
                        jsonStr = AddJsonInfo(jsonStr, "UID", response.UserID);
                        jsonStr = AddJsonInfo(jsonStr, "AccountNumber", response.UserAccount);
                        jsonStr = AddJsonInfo(jsonStr, "CurrentPageURL", context.Server.UrlEncode(Request.UrlReferrer.ToString()));
                    }
                }
                string respStr = string.Empty;
                if (methodName.EndsWith("Query"))
                {
                    if (string.IsNullOrEmpty(Request["page"]))
                    {
                        jsonStr = AddJsonInfo(jsonStr, "Page", 0);
                        jsonStr = AddJsonInfo(jsonStr, "PageRow", -1);
                    }
                    else
                    {
                        jsonStr = AddJsonInfo(jsonStr, "Page", Request["page"]);
                        jsonStr = AddJsonInfo(jsonStr, "PageRow", Request["limit"]);
                    }
                }
                if (methodName.Equals("uploadImg"))
                {
                    respStr = this.uploadImg(context.Server, Request.Files);
                }
                else if (methodName.Equals("initQuery"))
                {
                    respStr = this.initQuery(context.Server, response.FunctionCodes);
                    respStr = AddJsonInfo(respStr, "AccountNumber", response.UserAccount);
                }
                else
                {
                    BeforeInvoke(methodName, response.FunctionCodes);
                    respStr = (string)method.Invoke(this, null);
                }
                context.Response.Write(respStr);
            }
            catch (Exception ex)
            {
                Hashtable result = new Hashtable();

                result["ErrorCode"] = ex.Message;
                result["ErrorMessage"] = "errorCode_" + ex.Message;
                result["ErrorStackTrace"] = ex.StackTrace.ToString();

                String errorJson = this.Obj2Json(result);
                context.Response.Clear();
                context.Response.Write(errorJson);
            }
            finally
            {
                AfterInvoke(methodName);
            }
        }

        //权限管理
        protected void BeforeInvoke(String methodName,List<string> funcodes)
        {
            //Hashtable user = GetUser();
            //if (user.role == "admin" && methodName == "remove") throw .    
            if (methodName == "login" || methodName == "passwordModify" || methodName == "regist" || methodName == "getFans")
                return;
            foreach (string method in methodList)
            {
                if (methodName.Contains(method))
                {
                    string op = methodName.Replace(method, "").ToLower();
                    if (!funcodes.Contains(string.Format("{0}_{1}", op, method.ToLower())))
                        throw new Exception("没有权限");
                }
            }
        }
        //日志管理
        protected void AfterInvoke(String methodName)
        {
            try
            {
                Data.DTO.PQLogModify request = new Data.DTO.PQLogModify();
                request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQLogModify;
                foreach (string method in methodList)
                {
                    if (methodName.Contains(method))
                    {
                        string op = methodName.Replace(method, "");
                        request.Operate = op;
                        request.Function = method;
                        break;
                    }
                }
                Data.BLL.cUserBLL user = new Data.BLL.cUserBLL();
                user.LogModify(request);
            }
            catch { 
                //不处理日志错误
            }
        }

        /// <summary>
        /// 对象转Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string Obj2Json(object obj)
        {
            return PluSoft.Utils.JSON.Encode(obj);
        }

        protected object Json2Obj(string json, Type type)
        {
            return PluSoft.Utils.JSON.Decode(json, type);
        }

        /// <summary>
        /// 将权限值写入菜单的对象中，方便在每个页面打开时取值
        /// </summary>
        /// <param name="server">server context 对象</param>
        /// <param name="functionCodes">当前用户的权限列表</param>
        /// <returns></returns>
        public string initQuery(HttpServerUtility server, List<string> functionCodes)
        {
            string response = string.Empty;
            System.IO.TextReader tr = System.IO.File.OpenText(server.MapPath("./api/init.json"));
            response = tr.ReadToEnd();
            tr.Close();
            tr.Dispose();
            Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(response);
            //开始重新生成初始化数据以设置菜单显示
            foreach (Newtonsoft.Json.Linq.JToken jt in jo["menuInfo"])
            {
                Newtonsoft.Json.Linq.JToken jtTag = jt["tag"];
                string tagStr = string.Empty;
                if (jtTag != null)
                    tagStr = jtTag.ToString();
                //第一层菜单
                if (tagStr!=string.Empty)
                {
                    //有权限控
                    if (!functionCodes.Contains(tagStr))
                    {
                        jt.Remove();
                        continue;
                    }
                    else
                    {
                        string func = tagStr.Split('_')[0];
                        foreach (string funright in functionCodes)
                        {
                            if (funright.IndexOf(func) > -1)
                                jt["access"] = jt["access"] + "," + funright.Replace(func + "_", "");
                        }
                    }
                }
                //开始下一级
                foreach (Newtonsoft.Json.Linq.JToken ch in jt["child"])
                {
                    Newtonsoft.Json.Linq.JToken jtTag1 = ch["tag"];
                    string tagStr1 = string.Empty;
                    if (jtTag1 != null)
                        tagStr1 = jtTag1.ToString();
                    //第一层菜单
                    if (tagStr1 != string.Empty)
                    {
                        //有权限控
                        if (!functionCodes.Contains(tagStr1))
                        {
                            ch.Remove();
                            continue;
                        }
                        else
                        {
                            string func = tagStr1.Split('_')[0];
                            foreach (string funright in functionCodes)
                            {
                                if (funright.IndexOf(func) > -1)
                                    ch["access"] = ch["access"] + "," + funright.Replace(func + "_", "");
                            }
                        }
                    }
                    //开始下一级
                    foreach (Newtonsoft.Json.Linq.JToken ch1 in ch["child"])
                    {
                        Newtonsoft.Json.Linq.JToken jtTag2 = ch1["tag"];
                        string tagStr2 = string.Empty;
                        if (jtTag2 != null)
                            tagStr2 = jtTag2.ToString();
                        //第一层菜单
                        if (tagStr2 != string.Empty)
                        {
                            //有权限控
                            if (!functionCodes.Contains(tagStr2))
                            {
                                ch1.Remove();
                                continue;
                            }
                            else
                            {
                                string func = tagStr2.Split('_')[0];
                                foreach (string funright in functionCodes)
                                {
                                    if (funright.IndexOf(func) > -1)
                                        ch1["access"] = ch1["access"] + "," + funright.Replace(func + "_", "");
                                }
                            }
                        }
                        //不进行下一级了
                    }

                }
            }
            return jo.ToString();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion

        #region 登陆处理
        /// <summary>
        /// 取工具详情，所有数据一起返回
        /// </summary>
        /// <returns></returns>
        public string login()
        {
            Data.DTO.PQLogin request = new Data.DTO.PQLogin();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQLogin;
            Data.DTO.PSLogin response = new Data.DTO.PSLogin();
            Data.BLL.cUserBLL client = new Data.BLL.cUserBLL();
            response = client.Login(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        public string regist()
        {
            Data.DTO.PQRegist request = new Data.DTO.PQRegist();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQRegist;
            Data.DTO.PSRegist response = new Data.DTO.PSRegist();
            Data.BLL.cUserBLL client = new Data.BLL.cUserBLL();
            response = client.Regist(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        #endregion

        #region 用户处理
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <returns></returns>
        public string userQuery()
        {
            Data.DTO.PQUserQuery request = new Data.DTO.PQUserQuery();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQUserQuery;
            Data.DTO.PSUserQuery response = new Data.DTO.PSUserQuery();
            Data.BLL.cUserBLL client = new Data.BLL.cUserBLL();
            response = client.UserQuery(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        public string userModify()
        {
            Data.DTO.PQUserModify request = new Data.DTO.PQUserModify();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQUserModify;
            Data.DTO.PSUserModify response = new Data.DTO.PSUserModify();
            Data.BLL.cUserBLL client = new Data.BLL.cUserBLL();
            response = client.UserModify(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        public string userView()
        {
            Data.DTO.PQUserView request = new Data.DTO.PQUserView();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQUserView;
            Data.DTO.PSUserView response = new Data.DTO.PSUserView();
            Data.BLL.cUserBLL client = new Data.BLL.cUserBLL();
            response = client.UserView(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        public string passwordModify()
        {
            Data.DTO.PQPasswordModify request = new Data.DTO.PQPasswordModify();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQPasswordModify;
            Data.DTO.PSPasswordModify response = new Data.DTO.PSPasswordModify();
            Data.BLL.cUserBLL client = new Data.BLL.cUserBLL();
            response = client.PasswordModify(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        #endregion

        #region 组织处理
        public string orgQuery()
        {
            Data.DTO.PQOrgQuery request = new Data.DTO.PQOrgQuery();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQOrgQuery;
            Data.DTO.PSOrgQuery response = new Data.DTO.PSOrgQuery();
            Data.BLL.cUserBLL client = new Data.BLL.cUserBLL();
            response = client.OrgQuery(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        public string orgModify()
        {
            Data.DTO.PQOrgModify request = new Data.DTO.PQOrgModify();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQOrgModify;
            Data.DTO.PSOrgModify response = new Data.DTO.PSOrgModify();
            Data.BLL.cUserBLL client = new Data.BLL.cUserBLL();
            response = client.OrgModify(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        #endregion

        #region 工具处理
        public string toolQuery()
        {
            Data.DTO.PQToolQuery request = new Data.DTO.PQToolQuery();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQToolQuery;
            Data.DTO.PSToolQuery response = new Data.DTO.PSToolQuery();
            Data.BLL.cToolBLL client = new Data.BLL.cToolBLL();
            response = client.ToolQuery(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        public string toolModify()
        {
            Data.DTO.PQToolModify request = new Data.DTO.PQToolModify();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQToolModify;
            Data.DTO.PSToolModify response = new Data.DTO.PSToolModify();
            Data.BLL.cToolBLL client = new Data.BLL.cToolBLL();
            response = client.ToolModify(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }


        public string toolAudit()
        {
            Data.DTO.PQToolAudit request = new Data.DTO.PQToolAudit();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQToolAudit;
            Data.DTO.PSToolAudit response = new Data.DTO.PSToolAudit();
            Data.BLL.cToolBLL client = new Data.BLL.cToolBLL();
            response = client.ToolAudit(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        public string toolView()
        {
            Data.DTO.PQToolView request = new Data.DTO.PQToolView();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQToolView;
            Data.DTO.PSToolView response = new Data.DTO.PSToolView();
            Data.BLL.cToolBLL client = new Data.BLL.cToolBLL();
            response = client.ToolView(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        public string catSQuery()
        {
            Data.DTO.PQCatSQuery request = new Data.DTO.PQCatSQuery();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQCatSQuery;
            Data.DTO.PSCatSQuery response = new Data.DTO.PSCatSQuery();
            Data.BLL.cToolBLL client = new Data.BLL.cToolBLL();
            response = client.CatSQuery(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        #endregion

        #region 案例处理
        public string caseQuery()
        {
            Data.DTO.PQCaseQuery request = new Data.DTO.PQCaseQuery();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQCaseQuery;
            Data.DTO.PSCaseQuery response = new Data.DTO.PSCaseQuery();
            Data.BLL.cToolBLL client = new Data.BLL.cToolBLL();
            response = client.CaseQuery(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        #endregion

        #region 权限处理
        public string rightQuery()
        {
            Data.DTO.PQRightQuery request = new Data.DTO.PQRightQuery();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQRightQuery;
            Data.DTO.PSRightQuery response = new Data.DTO.PSRightQuery();
            Data.BLL.cUserBLL client = new Data.BLL.cUserBLL();
            response = client.RightQuery(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        public string rightModify()
        {
            Data.DTO.PQRightModify request = new Data.DTO.PQRightModify();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQRightModify;
            Data.DTO.PSRightModify response = new Data.DTO.PSRightModify();
            Data.BLL.cUserBLL client = new Data.BLL.cUserBLL();
            response = client.RightModify(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }


        public string rightView()
        {
            Data.DTO.PQRightView request = new Data.DTO.PQRightView();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQRightView;
            Data.DTO.PSRightView response = new Data.DTO.PSRightView();
            Data.BLL.cUserBLL client = new Data.BLL.cUserBLL();
            response = client.RightView(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        public string roleModify()
        {
            Data.DTO.PQRoleModify request = new Data.DTO.PQRoleModify();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQRoleModify;
            Data.DTO.PSRoleModify response = new Data.DTO.PSRoleModify();
            Data.BLL.cUserBLL client = new Data.BLL.cUserBLL();
            response = client.RoleModify(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }
        #endregion

        #region 欢迎处理
        public string welcomeView()
        {
            Data.DTO.PQWelcomeView request = new Data.DTO.PQWelcomeView();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQWelcomeView;
            Data.DTO.PSWelcomeView response = new Data.DTO.PSWelcomeView();
            Data.BLL.cOtherBLL client = new Data.BLL.cOtherBLL();
            response = client.WelcomeView(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }
        #endregion

        #region 微信处理
        public string fansQuery()
        {
            Data.DTO.PQWechatFanQuery request = new Data.DTO.PQWechatFanQuery();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQWechatFanQuery;
            Data.DTO.PSWechatFanQuery response = new Data.DTO.PSWechatFanQuery();
            Data.BLL.cOtherBLL client = new Data.BLL.cOtherBLL();
            response = client.WechatFanQuery(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        /// <summary>
        /// 由内部定时器调用，通知WEB去批量取微信的粉丝数据
        /// 不需要有参数后台处理
        /// </summary>
        /// <returns></returns>
        public string getFans()
        {
            Data.DTO.ResponseBase response = new Data.DTO.ResponseBase();
            Data.BLL.cOtherBLL client = new Data.BLL.cOtherBLL();
            response = client.PullFans();
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }
        #endregion

        #region 日志处理
        public string logQuery()
        {
            Data.DTO.PQLogQuery request = new Data.DTO.PQLogQuery();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQLogQuery;
            Data.DTO.PSLogQuery response = new Data.DTO.PSLogQuery();
            Data.BLL.cOtherBLL client = new Data.BLL.cOtherBLL();
            response = client.LogQuery(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }
        #endregion

        #region 上传处理
        public string uploadImg(HttpServerUtility server, HttpFileCollection files)
        {
            Data.DTO.PQUploadImg request = new Data.DTO.PQUploadImg();
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQUploadImg;

            if (files.Count == 0)
                return "";
            string path = string.Empty;
            string name = string.Empty;
            if (request.ID == 0)
            {
                request.ID = 0 - ConvertDateTimeInt(DateTime.Now);
            }
            switch (request.Type)
            {
                case "t":
                    //工具图片
                    path = "/img/tool/";
                    name = string.Format("tool_{0}", request.ID);
                    break;
                case "f":
                    //分类一图片
                    path = "/img/cat/";
                    name = string.Format("catf_{0}", request.ID);
                    break;
                case "s":
                    //分类一图片
                    path = "/img/cat/";
                    name = string.Format("cats_{0}", request.ID);
                    break;
                case "d":
                    //工具详情
                    path = "/img/det/";
                    name = string.Format("det_{0}", request.ID);
                    break;
                default:
                    //其它图片，包括工具明细的图片
                    path = "/img/view/";
                    name = string.Format("cats_{0}_{1}", request.ID, DateTime.Now.ToString("yyMMddHHmmss"));
                    break;
            }
            List<Hashtable> finfos = new List<Hashtable>();
            int i = 0;
            for (i = 0; i < files.Count; i++)
            {
                Hashtable finfo = new Hashtable();
                HttpPostedFile file = files[i];
                string oExt = System.IO.Path.GetExtension(file.FileName);
                string fname = string.Format("{0}_{1}{2}", name, i, oExt);
                file.SaveAs(server.MapPath(path + fname));
                finfo["StoreName"] = fname;
                finfo["Path"] = path;
                finfos.Add(finfo);
            }
            System.Collections.Hashtable response = new Hashtable();
            response.Add("ErrorMessage", "操作成功");
            response.Add("ErrorCode", "A_0");
            response.Add("total", i);
            response.Add("files", finfos);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        private int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        #endregion
    }
}
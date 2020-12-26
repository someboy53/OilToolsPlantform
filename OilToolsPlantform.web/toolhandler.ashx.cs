using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Text;
using System.Collections;

namespace OilToolsPlantform
{
    /// <summary>
    /// toolhandler 处理所有前台请求
    /// </summary>
    public class toolhandler : IHttpHandler
    {

        #region 内部处理

        private string jsonStr;

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
                //过滤掉登录方法和登录初始化方法, 取资源文件
                //if (methodName != "LoginInit" && methodName != "Login" && methodName != "GetResourcesStrings" && userInfo == null)
                //    throw new Exception("001");

                Type type = this.GetType();
                MethodInfo method = type.GetMethod(methodName);
                if (method == null) throw new Exception("method is null");
                jsonStr=Request["json"];
                BeforeInvoke(methodName);
                string response = (string)method.Invoke(this, null);
                context.Response.Write(response);
            }
            catch (Exception ex)
            {
                Hashtable result = new Hashtable();
                string errMsg = ex.Message;
                if (errMsg == "001")
                {
                    result["errorCode"] = errMsg;
                    result["errorMessage"] = "errorCode_" + errMsg;
                    result["errorStackTrace"] = ex.StackTrace.ToString();
                }
                else
                {
                    switch (ex.GetType().FullName)
                    {
                        case "System.ServiceModel.EndpointNotFoundException":
                            errMsg = "应用服务器已停止或正在维护，请稍后再试！";
                            break;
                        case "System.TimeoutException":
                            errMsg = "应用服务器已停止或正在维护，连接超时！";
                            break;
                        case "System.Reflection.TargetInvocationException":
                            errMsg = "应用服务器已停止或正在维护，请稍后再试！";
                            break;
                    }

                    result["errorCode"] = -1;
                    result["errorMessage"] = errMsg;
                    result["errorStackTrace"] = "";
                }
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
        protected void BeforeInvoke(String methodName)
        {
            //Hashtable user = GetUser();
            //if (user.role == "admin" && methodName == "remove") throw .      
        }
        //日志管理
        protected void AfterInvoke(String methodName)
        {

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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 取一级目录
        /// </summary>
        public string getCatF()
        {
            Data.DTO.PQCatQuery request = new Data.DTO.PQCatQuery();
            request.Level = "1";
            request.Page = 1;
            request.PageRow = 10000;
            Data.DTO.PSCatQuery response = new Data.DTO.PSCatQuery();
            Data.BLL.cToolBLL client = new Data.BLL.cToolBLL();
            response = client.QueryCat(request);
            string strResponse = this.Obj2Json(response);
            //strResponse = strResponse.Insert(strResponse.Length - 1, ",companyType:\"" + userInfo.CompanyType + "\"");
            return strResponse;
        }

        /// <summary>
        /// 取二级目录
        /// </summary>
        /// <returns></returns>
        public string getCatS()
        {
            Data.DTO.PQCatQuery request = new Data.DTO.PQCatQuery();
            jsonStr = System.Web.HttpUtility.UrlDecode(jsonStr);
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQCatQuery;
            request.Page = 1;
            request.PageRow = 10000;
            Data.DTO.PSCatQuery response = new Data.DTO.PSCatQuery();
            Data.BLL.cToolBLL client = new Data.BLL.cToolBLL();
            response = client.QueryCat(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        /// <summary>
        /// 取工具列表，如不传二级目录ID，则查所有
        /// </summary>
        /// <returns></returns>
        public string getTool()
        {
            Data.DTO.PQToolQuery request = new Data.DTO.PQToolQuery();
            jsonStr = System.Web.HttpUtility.UrlDecode(jsonStr);
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQToolQuery;
            Data.DTO.PSToolQuery response = new Data.DTO.PSToolQuery();
            Data.BLL.cToolBLL client = new Data.BLL.cToolBLL();
            response = client.QueryTool(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }

        /// <summary>
        /// 取工具详情，所有数据一起返回
        /// </summary>
        /// <returns></returns>
        public string ToolView()
        {
            Data.DTO.PQToolView request = new Data.DTO.PQToolView();
            jsonStr = System.Web.HttpUtility.UrlDecode(jsonStr);
            request = this.Json2Obj(jsonStr, request.GetType()) as Data.DTO.PQToolView;
            Data.DTO.PSToolView response = new Data.DTO.PSToolView();
            Data.BLL.cToolBLL client = new Data.BLL.cToolBLL();
            response = client.ViewTool(request);
            string strResponse = this.Obj2Json(response);
            return strResponse;
        }
    }
}
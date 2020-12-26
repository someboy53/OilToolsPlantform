using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using com.Origin.CommonLibrary.Framework;

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
        #endregion
    }
}

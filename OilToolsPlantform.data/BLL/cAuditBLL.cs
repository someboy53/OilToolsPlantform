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
    public class cAuditBLL : BllBase
    {
        #region 首页相关
        /// <summary>
        /// 审核用方法，提交、审核都处理
        /// </summary>
        /// <param name="TargetTableName">目标表名</param>
        /// <param name="TargetTableID">目标表ID</param>
        /// <param name="Pass">1-不通过2-返回修改4-通过</param>
        /// <param name="UserID">当前用户ID</param>
        /// <returns></returns>
        public bool Audit(string TargetTableName, int TargetTableID, int Pass, int UserID,string PassDesc)
        {
            try
            {
                Models.tbUser user = con.tbUser.Find(UserID);
                Models.tbUser draftUser = new Models.tbUser();
                //找到审核信息，如果没有，说明是第一步，需要提交
                List<Models.tbAudit> objs = con.tbAudit.Where(p => p.TargetTableName == TargetTableName && p.TargetTableID == TargetTableID).OrderByDescending(p => p.AuditID).ToList();
                Models.tbAudit audit = new Models.tbAudit();
                if (objs.Count == 0)
                {
                    audit = con.tbAudit.Create();
                    audit.TargetTableName = TargetTableName;
                    audit.TargetTableID = TargetTableID;
                    //audit.Status = 3;
                    audit.NextAuditOrgID = user.OrgID;
                    audit.NextAuditStep = 1;
                    draftUser = user;
                }
                else
                {
                    audit = con.tbAudit.Find(objs[0].AuditID);
                    audit.NextAuditStep += 1;
                    draftUser = con.tbUser.Find(audit.AuditID1);
                }
                //取出审核顺序
                string InPersonOrgID = System.Configuration.ConfigurationManager.AppSettings["InPersonOrgID"];
                string OutPersonOrgID = System.Configuration.ConfigurationManager.AppSettings["OutPersonOrgID"];
                string InToolAuditProcess = System.Configuration.ConfigurationManager.AppSettings["InToolAuditProcess"];
                string OutToolAuditProcess = System.Configuration.ConfigurationManager.AppSettings["OutToolAuditProcess"];
                string AuditProcess = string.Empty;
                //取出单据的制单人的信息
                if (draftUser.tbOrg.RealPath.Contains("/" + InPersonOrgID + "/"))
                {
                    //说明是内部人员
                    AuditProcess = InToolAuditProcess;
                }
                else if (draftUser.tbOrg.RealPath.Contains("/" + OutPersonOrgID + "/"))
                {
                    //说明是外部人员
                    AuditProcess = OutToolAuditProcess;
                }
                else
                {
                    throw new Exception("A_CON_ERROR_ORG");
                }
                //判断当前是第几步
                if (audit.NextAuditOrgID != user.OrgID)
                {
                    //说明当前不为此人审核
                    throw new Exception("A_AUDIT_ORG_ERROR");
                }
                switch (audit.NextAuditStep)
                {
                    case 1:
                        audit.AuditID1 = UserID;
                        audit.AuditName1 = user.UserName;
                        audit.AuditStatus1 = 0;
                        audit.AuditDate1 = DateTime.Now;
                        audit.AuditAdvice1 = PassDesc;
                        break;
                    case 2:
                        audit.AuditID2 = UserID;
                        audit.AuditName2 = user.UserName;
                        audit.AuditStatus2 = Pass;
                        audit.AuditDate2 = DateTime.Now;
                        audit.AuditAdvice2 = PassDesc;
                        break;
                    case 3:
                        audit.AuditID3 = UserID;
                        audit.AuditName3 = user.UserName;
                        audit.AuditStatus3 = Pass;
                        audit.AuditDate3 = DateTime.Now;
                        audit.AuditAdvice3 = PassDesc;
                        break;
                    case 4:
                        audit.AuditID4 = UserID;
                        audit.AuditName4 = user.UserName;
                        audit.AuditStatus4 = Pass;
                        audit.AuditDate4 = DateTime.Now;
                        audit.AuditAdvice4 = PassDesc;
                        break;
                    case 5:
                        audit.AuditID5 = UserID;
                        audit.AuditName5 = user.UserName;
                        audit.AuditStatus5 = Pass;
                        audit.AuditDate5 = DateTime.Now;
                        audit.AuditAdvice5 = PassDesc;
                        break;
                    default:
                        throw new Exception("A_AUDIT_OUT_RANGE");
                }
                string[] steps;
                if (AuditProcess.Length > 0)
                    steps = AuditProcess.Split(',');
                else
                    steps = new string[] { };
                int currentStep = 0;
                if (audit.NextAuditStep > steps.Length)
                {
                    //超过了
                    if (Pass == 4)
                    {
                        switch (TargetTableName)
                        {
                            case "tbTool":
                                Models.tbTool t = con.tbTool.Find(TargetTableID);
                                t.Enabled = "1";
                                break;
                        }
                    }
                    audit.Status = Pass;
                    audit.NextAuditStep = 0;
                    currentStep = 0;
                }
                else
                {
                    if (Pass == 4)
                    {
                        currentStep = int.Parse(steps[(int)audit.NextAuditStep - 1]);
                        audit.Status = 3;
                    }
                    else
                    {
                        audit.Status = Pass;
                    }
                }
                audit.LastAuditDate = DateTime.Now;
                audit.LastAuditName = user.UserName;
                audit.NextAuditOrgID = currentStep;
                if (objs.Count == 0)
                    con.tbAudit.Add(audit);
                con.SaveChanges();
            }
            catch (Exception ex)
            {
                LogHelper.Error("cAuditBLL.Audit出错！", ex);
                throw;
            }
            return true;
        }

        public List<string> GetAuditHist(string TargetTableName,int TargetTableID)
        {
            List<string> resp = new List<string>();
            Models.tbAudit audit = new Models.tbAudit();
            List<Models.tbAudit> objs = con.tbAudit.Where(p => p.TargetTableName == TargetTableName && p.TargetTableID == TargetTableID).OrderByDescending(p => p.AuditID).ToList();
            if (objs.Count > 0)
            {
                audit = objs[0];
                string respTemp = "{0}于{1}{2},审核意见为:{3}";
                string[] staticStatus = new string[] { "提交", "审核不通过", "草稿", "审核中", "审核通过" };
                if (audit.AuditID1 != null && audit.AuditID1 > 0)
                {
                    //0-作废1-不通过2-草稿3-审核中4-审核完成
                    resp.Add(string.Format(respTemp, audit.AuditName1, ((DateTime)(audit.AuditDate1)).ToString("yyyy-MM-dd HH:mm:ss"), staticStatus[(int)audit.AuditStatus1], audit.AuditAdvice1));
                }
                if (audit.AuditID2 != null && audit.AuditID2 > 0)
                {
                    //0-作废1-不通过2-草稿3-审核中4-审核完成
                    resp.Add(string.Format(respTemp, audit.AuditName2, ((DateTime)(audit.AuditDate2)).ToString("yyyy-MM-dd HH:mm:ss"), staticStatus[(int)audit.AuditStatus2], audit.AuditAdvice2));
                }
                if (audit.AuditID3 != null && audit.AuditID3 > 0)
                {
                    //0-作废3-不通过2-草稿3-审核中4-审核完成
                    resp.Add(string.Format(respTemp, audit.AuditName3, ((DateTime)(audit.AuditDate3)).ToString("yyyy-MM-dd HH:mm:ss"), staticStatus[(int)audit.AuditStatus3], audit.AuditAdvice3));
                }
                if (audit.AuditID4 != null && audit.AuditID4 > 0)
                {
                    //0-作废4-不通过2-草稿3-审核中4-审核完成
                    resp.Add(string.Format(respTemp, audit.AuditName4, ((DateTime)(audit.AuditDate4)).ToString("yyyy-MM-dd HH:mm:ss"), staticStatus[(int)audit.AuditStatus4], audit.AuditAdvice4));
                }
                if (audit.AuditID5 != null && audit.AuditID5 > 0)
                {
                    //0-作废5-不通过2-草稿3-审核中4-审核完成
                    resp.Add(string.Format(respTemp, audit.AuditName5, ((DateTime)(audit.AuditDate5)).ToString("yyyy-MM-dd HH:mm:ss"), staticStatus[(int)audit.AuditStatus5], audit.AuditAdvice5));
                }
                return resp;
            }
            else
            {
                return resp;
            }
        }

        #endregion
    }
}

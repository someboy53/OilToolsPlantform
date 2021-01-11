using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using com.Origin.CommonLibrary.Framework;

namespace OilToolsPlantform.Data.BLL
{
    /// <summary>
    /// 个人中心
    /// </summary>
    public class cUserBLL : BllBase
    {

        #region 用户资料及认证

        /// <summary>
        /// 修改当前用户密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DTO.PSPasswordModify PasswordModify(DTO.PQPasswordModify request)
        {
            DTO.PSPasswordModify response = new DTO.PSPasswordModify();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    if (request.NewPass2 != request.NewPass1)
                    {
                        response.ErrorCode = "A_TWO_NEW_NOT_PAIR";
                        response.ErrorMessage = rm.GetString(response.ErrorCode);
                        return response;
                    }
                    Models.tbUser obj = con.tbUser.Find(request.UID);
                    if (obj == null)
                    {
                        response.ErrorCode = "A_PARAM_ERROR";
                        response.ErrorMessage = rm.GetString(response.ErrorCode);
                        return response;
                    }
                    //根据老密码生成密文
                    SHA1 sha = SHA1.Create();
                    ASCIIEncoding enc = new ASCIIEncoding();
                    byte[] oData = enc.GetBytes(request.OldPass + obj.Salt);
                    byte[] dData = sha.ComputeHash(oData);
                    string pass = BitConverter.ToString(dData).Replace("-", "");
                    if (obj.UserPass.Trim() != pass)
                    {
                        response.ErrorCode = "A_OLD_PASS_NOT_RIGHT";
                        response.ErrorMessage = rm.GetString(response.ErrorCode);
                        return response;
                    }
                    //根据新密码生成密文
                    byte[] oData1 = enc.GetBytes(request.NewPass1 + obj.Salt);
                    byte[] dData1 = sha.ComputeHash(oData1);
                    string pass1 = BitConverter.ToString(dData1).Replace("-", "");
                    obj.UserPass = pass1;
                    con.SaveChanges();
                    scope.Complete();
                    response.ErrorCode = "A_0";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cUserBLL.PasswordModify出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        public DTO.PSTokenView TokenView(DTO.PQTokenView request)
        {
            DTO.PSTokenView response = new DTO.PSTokenView();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    DateTime tmp = DateTime.Now.AddDays(-7);
                    List<Models.tbToken> t = con.tbToken.Where(p => p.CreateDate > tmp && p.token == request.token).OrderByDescending(p => p.TokenID).ToList();
                    if (t.Count > 0)
                    {
                        response.UserID = t[0].UserID;
                        response.UserAccount = t[0].UserAccount;
                        Models.tbUser u = con.tbUser.Find(response.UserID);
                        response.FunctionCodes = u.tbRole.tbRoleDetail.Select(p => p.FunctionCode).ToList();
                        response.ErrorCode = "A_0";
                    }
                    else
                    {
                        //登陆令牌有误，需要重新登陆
                        response.ErrorCode = "A_999";
                    }
                    response.code = response.ErrorCode == "A_0" ? "0" : response.ErrorCode;
                    response.msg = response.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cUserBLL.UserView出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DTO.PSUserModify UserModify(DTO.PQUserModify request)
        {
            DTO.PSUserModify response = new DTO.PSUserModify();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    Models.tbUser u;
                    if ("1".Equals(request.IsDel))
                    {
                        u = con.tbUser.Find(request.UserID);
                        con.tbUser.Remove(u);
                    }
                    else if ("2".Equals(request.IsDel))
                    {
                        u = con.tbUser.Find(request.UserID);
                        u.Salt = new Random().Next(900000).ToString().PadLeft(6, '0');
                        SHA1 sha = SHA1.Create();
                        ASCIIEncoding enc = new ASCIIEncoding();
                        byte[] oData = enc.GetBytes("123456" + u.Salt);
                        byte[] dData = sha.ComputeHash(oData);
                        u.UserPass = BitConverter.ToString(dData).Replace("-", "");
                        u.UpdateTime = DateTime.Now;
                    }
                    else
                    {
                        if (request.UserID == 0)
                        {
                            //新增
                            u = con.tbUser.Create();
                            u.UserName = request.UserName;
                            u.UserAccount = request.UserAccount;
                            u.Salt = new Random().Next(900000).ToString().PadLeft(6, '0');
                            SHA1 sha = SHA1.Create();
                            ASCIIEncoding enc = new ASCIIEncoding();
                            byte[] oData = enc.GetBytes("123456" + u.Salt);
                            byte[] dData = sha.ComputeHash(oData);
                            u.UserPass = BitConverter.ToString(dData).Replace("-", "");
                            u.StartDate = DateTime.Now;
                            u.EndDate = u.StartDate.AddYears(2);
                            u.CreateTime = DateTime.Now;
                            u.UpdateTime = DateTime.Now;
                            u.Enabled = "1";
                            u.OrgID = request.OrgID;
                            u.CellPhone = request.CellPhone;
                            u.Email = request.Email;
                            u.WorkNumber = request.WorkNumber;
                            u.RoleID = request.RoleID;
                            con.tbUser.Add(u);
                        }
                        else
                        {
                            if (request.Enabled == null)
                            {
                                //修改
                                u = con.tbUser.Find(request.UserID);
                                u.UserName = request.UserName;
                                u.UserAccount = request.UserAccount;
                                u.UpdateTime = DateTime.Now;
                                u.Enabled = "1";
                                u.OrgID = request.OrgID;
                                u.CellPhone = request.CellPhone;
                                u.EndDate = DateTime.Parse(request.EndDate);
                                u.Email = request.Email;
                                u.WorkNumber = request.WorkNumber;
                                u.RoleID = request.RoleID;
                            }
                            else
                            {
                                if (request.Enabled == "1")
                                {
                                    //激活
                                    u = con.tbUser.Find(request.UserID);
                                    u.Enabled = "1";
                                    u.StartDate = DateTime.Now;
                                    u.EndDate = DateTime.Now.AddYears(2);
                                    string tmp = System.Configuration.ConfigurationManager.AppSettings["defaultRoleForOutPerson"];
                                    if (!string.IsNullOrEmpty(tmp))
                                        u.RoleID = int.Parse(tmp);
                                }
                            }
                        }
                    }
                    con.SaveChanges();
                    scope.Complete();
                    response.ErrorCode = "A_0";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cUserBLL.UserModify出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        /// <summary>
        /// 查看用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DTO.PSUserView UserView(DTO.PQUserView request)
        {
            DTO.PSUserView response = new DTO.PSUserView();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    Models.tbUser u = new Models.tbUser();
                    //修改
                    if (request.UserID > 0)
                    {
                        u = con.tbUser.Find(request.UserID);
                        response.UserID = u.UserID;
                        response.UserAccount = u.UserAccount;
                        response.UserName = u.UserName;
                        response.StartDate = u.StartDate.ToString("yyyy-MM-dd");
                        response.EndDate = u.EndDate.ToString("yyyy-MM-dd");
                        response.LastLoginTime = u.LastLoginTime == null ? "" : ((DateTime)u.LastLoginTime).ToString("yyyy-MM-dd HH:mm:ss");
                        response.CreateTime = u.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        response.UpdateTime = u.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        response.OrgID = (int)u.OrgID;
                        Models.tbOrg o = con.tbOrg.Find(response.OrgID);
                        response.FullPath = o.FullPath;
                        response.RoleID = (int)u.RoleID;
                        Models.tbRole r = con.tbRole.Find(response.RoleID);
                        response.RoleName = r.RoleName;
                        response.CellPhone = u.CellPhone;
                        response.Email = u.Email;
                        response.WorkNumber = u.WorkNumber;
                    }
                    List<Models.tbOrg> orgs = con.tbOrg.OrderBy(p => p.RealPath).ToList();
                    foreach (Models.tbOrg org in orgs)
                    {
                        response.OrgList.Add(new KeyValuePair<int, string>(org.OrgID, org.FullPath));
                    }
                    List<Models.tbRole> roles = con.tbRole.Where(p => p.RoleType == "1").OrderBy(p => p.RoleID).ToList();
                    foreach (Models.tbRole role in roles)
                    {
                        response.RoleList.Add(new KeyValuePair<int, string>(role.RoleID, role.RoleName));
                    }
                    response.ErrorCode = "A_0";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cUserBLL.UserView出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DTO.PSUserQuery UserQuery(DTO.PQUserQuery request)
        {
            DTO.PSUserQuery response = new DTO.PSUserQuery();
            try
            {
                DAL.cUserDAL dal = new DAL.cUserDAL(con);
                dal.UserQueryBuild(request);
                response.data = dal.Query<DTO.LUser>();
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
                LogHelper.Error("cUserBLL.QueryUser出错！", ex);
                throw;
            }
            return response;
        }
        #endregion

        #region 用户登陆

        public DTO.PSLogin Login(DTO.PQLogin request)
        {
            DTO.PSLogin response = new DTO.PSLogin();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    List<Models.tbUser> objs = con.tbUser.Where(p => p.UserAccount == request.account && p.Enabled == "1").ToList<Models.tbUser>();
                    if (objs.Count != 1)
                    {
                        response.ErrorCode = "A_DATA_ERROR";
                        response.ErrorMessage = rm.GetString(response.ErrorCode);
                        return response;
                    }
                    //根据老密码生成密文
                    SHA1 sha = SHA1.Create();
                    ASCIIEncoding enc = new ASCIIEncoding();
                    byte[] oData = enc.GetBytes(request.passwd + objs[0].Salt);
                    byte[] dData = sha.ComputeHash(oData);
                    string pass = BitConverter.ToString(dData).Replace("-", "");

                    if (objs[0].UserPass.Trim() != pass)
                    {
                        response.ErrorCode = "A_PASSWORD_ERROR";
                        response.ErrorMessage = rm.GetString(response.ErrorCode);
                        return response;
                    }
                    //生成一个令牌来确定身份，同时存入数据库
                    byte[] tData = enc.GetBytes(request.account + objs[0].Salt + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    byte[] cData = sha.ComputeHash(tData);
                    string token = BitConverter.ToString(cData).Replace("-", "");
                    response.token = token;
                    //存储token
                    Models.tbToken t = con.tbToken.Create();
                    t.token = token;
                    t.UserAccount = request.account;
                    t.UserID = objs[0].UserID;
                    t.CreateDate = DateTime.Now;
                    con.tbToken.Add(t);
                    con.SaveChanges();
                    scope.Complete();
                    response.ErrorCode = "A_0";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cUserBLL.Login出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        public DTO.PSRegist Regist(DTO.PQRegist request)
        {
            DTO.PSRegist response = new DTO.PSRegist();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    List<Models.tbUser> objs = con.tbUser.Where(p => p.UserAccount == request.account).ToList<Models.tbUser>();
                    if (objs.Count >= 1)
                    {
                        response.ErrorCode = "A_DATA_ERROR";
                        response.ErrorMessage = rm.GetString(response.ErrorCode);
                        return response;
                    }
                    //根据老密码生成密文
                    Models.tbUser obj = new Models.tbUser();
                    obj.Salt = new Random().Next(100000, 999999).ToString();
                    SHA1 sha = SHA1.Create();
                    ASCIIEncoding enc = new ASCIIEncoding();
                    byte[] oData = enc.GetBytes(request.passwd + obj.Salt);
                    byte[] dData = sha.ComputeHash(oData);
                    obj.UserPass = BitConverter.ToString(dData).Replace("-", "");
                    obj.UserName = request.name;
                    obj.CreateTime = DateTime.Now;
                    obj.Email = "";
                    obj.Enabled = "0";
                    string tmp = System.Configuration.ConfigurationManager.AppSettings["OutPersonOrgID"];
                    obj.CellPhone = request.phone;
                    obj.StartDate = DateTime.Now;
                    obj.EndDate = DateTime.Now;
                    obj.UserAccount = request.account;
                    if (!string.IsNullOrEmpty(tmp))
                    {
                        obj.OrgID = int.Parse(tmp);
                        con.SaveChanges();
                        scope.Complete();
                        response.ErrorCode = "A_0";
                    }
                    else
                    {
                        response.ErrorCode = "A_OUT_PERSON_ORG_NOT_SET";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cUserBLL.Regist出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        public DTO.PSLogModify LogModify(DTO.PQLogModify request)
        {
            DTO.PSLogModify response = new DTO.PSLogModify();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    Models.tbLog log = con.tbLog.Create();
                    log.AccountNumber = request.AccountNumber;
                    log.UserID = request.UID;
                    log.Request = request.Request;
                    log.Response = request.Response;
                    log.HappenDate = DateTime.Now;
                    log.Description = string.Format("{0} {1} from {2}", request.Operate, request.Function, request.CurrentPageURL);
                    con.tbLog.Add(log);
                    con.SaveChanges();
                    scope.Complete();
                    response.ErrorCode = "A_0";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cUserBLL.LogModify出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        #endregion

        #region 组织相关

        public DTO.PSOrgView OrgView(DTO.PQOrgView request)
        {
            DTO.PSOrgView response = new DTO.PSOrgView();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    Models.tbOrg o;
                    //修改
                    o = con.tbOrg.Find(request.OrgID);
                    response.OrgID = o.OrgID;
                    response.OrgName = o.OrgName;
                    response.Description = o.Description;
                    response.FullPath = o.FullPath;
                    response.ParentID = o.ParentID;

                    Models.tbOrg p = con.tbOrg.Find(response.ParentID);
                    response.ParentName = p.OrgName;
                    response.ErrorCode = "A_0";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cUserBLL.UserView出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        public DTO.PSOrgModify OrgModify(DTO.PQOrgModify request)
        {
            DTO.PSOrgModify response = new DTO.PSOrgModify();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    Models.tbOrg o;
                    if ("1".Equals(request.IsDel))
                    {
                        o = con.tbOrg.Find(request.OrgID);
                        con.tbOrg.Remove(o);
                    }
                    else
                    {
                        if (request.OrgID == 0)
                        {
                            //新增
                            o = con.tbOrg.Create();
                            o.OrgName = request.OrgName;
                            o.Description = request.Description;
                            o.CreateTime = DateTime.Now;
                            o.UpdateTime = DateTime.Now;
                            o = con.tbOrg.Add(o);
                            con.SaveChanges();
                            Models.tbOrg p = con.tbOrg.Find(request.ParentID);
                            if (p != null)
                            {
                                o.ParentID = p.OrgID;
                                o.RealPath = p.RealPath + o.OrgID + "/";
                                o.FullPath = p.FullPath + o.OrgName + "/";
                            }
                            else
                            {
                                o.ParentID = -1;
                                o.RealPath = "/" + o.OrgID + "/";
                                o.FullPath = "/" + o.OrgName + "/";
                            }
                        }
                        else
                        {
                            //修改
                            o = con.tbOrg.Find(request.OrgID);
                            o.OrgName = request.OrgName;
                            o.Description = request.Description;
                            Models.tbOrg p = con.tbOrg.Find(o.ParentID);
                            o.FullPath = p.FullPath + o.OrgName + "/";
                            o.UpdateTime = DateTime.Now;
                        }
                    }
                    con.SaveChanges();
                    scope.Complete();
                    response.ErrorCode = "A_0";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cUserBLL.OrgModify出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        public DTO.PSOrgQuery OrgQuery(DTO.PQOrgQuery request)
        {
            DTO.PSOrgQuery response = new DTO.PSOrgQuery();
            try
            {
                DAL.cUserDAL dal = new DAL.cUserDAL(con);
                dal.OrgQueryBuild(request);
                response.data = dal.Query<Data.DTO.LOrg>();
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
                LogHelper.Error("cUserBLL.QueryUser出错！", ex);
                throw;
            }
            return response;
        }

        public DTO.PSRightQuery RightQuery(DTO.PQRightQuery request)
        {
            DTO.PSRightQuery response = new DTO.PSRightQuery();
            try
            {
                DAL.cUserDAL dal = new DAL.cUserDAL(con);
                dal.RightQueryBuild(request);
                response.data = dal.Query<Data.DTO.LRight>();
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
                LogHelper.Error("cUserBLL.QueryUser出错！", ex);
                throw;
            }
            return response;
        }

        public DTO.PSRightView RightView(DTO.PQRightView request)
        {
            DTO.PSRightView response = new DTO.PSRightView();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    response.FunctionCode = request.FunctionCode.Replace(':', '_');
                    List<Models.tbRoleDetail> tmps = con.tbRoleDetail.Where(p => p.FunctionCode == response.FunctionCode).ToList();
                    foreach (Models.tbRoleDetail tmp in tmps)
                    {
                        response.RoleIDs.Add(tmp.RoleID == null ? 0 : (int)tmp.RoleID);
                    }
                    response.Roles = con.tbRole.Select(p => new DTO.LRole() { RoleID = p.RoleID, RoleName = p.RoleName, RoleType = p.RoleType }).ToList();

                    response.ErrorCode = "A_0";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cUserBLL.UserView出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        public DTO.PSRightModify RightModify(DTO.PQRightModify request)
        {
            DTO.PSRightModify response = new DTO.PSRightModify();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    foreach (KeyValuePair<int, string> tmp in request.RoleIDs)
                    {
                        DAL.cUserDAL dal = new DAL.cUserDAL(con);
                        dal.RightModify(request.FunctionCode, tmp.Key, tmp.Value);
                    }
                    con.SaveChanges();
                    scope.Complete();
                    response.ErrorCode = "A_0";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cUserBLL.RightModify出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        public DTO.PSRoleModify RoleModify(DTO.PQRoleModify request)
        {
            DTO.PSRoleModify response = new DTO.PSRoleModify();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    Models.tbRole role = new Models.tbRole();
                    if ("1".Equals(request.IsDel))
                    {
                        role = con.tbRole.Find(request.RoleID);
                        con.tbRole.Remove(role);
                    }
                    else
                    {
                        if (request.RoleID > 0)
                        {
                            role = con.tbRole.Find(request.RoleID);
                            role.RoleName = request.RoleName;
                            role.RoleType = request.RoleType;
                        }
                        else
                        {
                            role = con.tbRole.Create();
                            role.RoleName = request.RoleName;
                            role.RoleType = request.RoleType;
                            role = con.tbRole.Add(role);
                        }
                    }
                    con.SaveChanges();
                    response.RoleID = role.RoleID;
                    response.RoleName = role.RoleName;
                    scope.Complete();
                    response.ErrorCode = "A_0";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cUserBLL.RoleModify出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }
        #endregion
    }
}

using OilToolsPlantform.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DAL
{
    public class cUserDAL : DalBase
    {
        public cUserDAL(Models.OilToolsPlantformEntities _con)
            : base(_con)
        {
        }


        internal void UserQueryBuild(DTO.PQUserQuery request)
        {

            this.AppendInit();
            this.AppendFieldRownum("u.UserID desc");
            this.AppendFieldStr("u.[UserID]");
            this.AppendFieldStr("u.[UserAccount]");
            this.AppendFieldDate("u.[StartDate]");
            this.AppendFieldDate("u.[EndDate]");
            this.AppendFieldTime("u.[LastLoginTime]");
            this.AppendFieldTime("u.[CreateTime]");
            this.AppendFieldTime("u.[UpdateTime]");
            this.AppendFieldStr("u.[Enabled]");
            this.AppendFieldStr("u.[OrgID]");
            this.AppendFieldStr("o.[FullPath]");
            this.AppendFieldStr("u.[CellPhone]");
            this.AppendFieldStr("u.[Email]");
            this.AppendFieldStr("u.[WorkNumber]");
            this.AppendFieldStr("u.[RoleID]");
            this.AppendFieldStr("r.[RoleName]");
            this.AppendFieldStr("u.[UserName]");
            this.AppendFromStr("from tbUser u,tbOrg o,tbRole r");
            this.AppendFromStr("where u.orgid=o.orgid and u.roleid=r.roleid");
            this.AppendWhereContains("o.FullPath", request.OrgName);
            this.AppendWhereContains("u.UserName", request.UserName);
            this.AppendWhereLike("u.CellPhone", request.Cellphone);
            this.AppendWhereLike("u.UserAccount", request.UserAccount);
            this.AppendWhereLike("u.WorkNumber", request.WorkNumber);
            this.AppendComplete(request.Page, request.PageRow);
        }

        public void OrgQueryBuild(DTO.PQOrgQuery request)
        {
            this.AppendInit();
            this.AppendFieldRownum("o.OrgID desc");
            this.AppendFieldStr("o.[OrgID]");
            this.AppendFieldStr("o.[OrgName]");
            this.AppendFieldStr("o.[RealPath]");
            this.AppendFieldStr("o.[FullPath]");
            this.AppendFieldStr("o.[UpdateTime]");
            this.AppendFieldStr("o.[CreateTime]");
            this.AppendFieldStr("o.[Description]");
            this.AppendFieldStr("o.[ParentID]");
            this.AppendFromStr("from tbOrg o");
            this.AppendFromStr("where 1=1");
            this.AppendComplete(request.Page, request.PageRow);
        }

        public void RightQueryBuild(PQRightQuery request)
        {
            this.AppendInit();
            this.AppendFieldRownum("rd.functionCode desc");
            this.AppendFieldStr("rd.functionCode");
            this.AppendFieldStr("r.roleid");
            this.AppendFieldStr("r.rolename");
            this.AppendFieldStr("r.roletype");
            this.AppendFromStr("from tbRole r, tbRoleDetail rd");
            this.AppendFromStr("where r.roleid=rd.roleid");
            this.AppendWhereEqual("rd.functionCode", request.FunctionCode);
            this.AppendComplete(request.Page, request.PageRow);
        }

        public void RightModify(string functionCode, int key, string value)
        {
            functionCode = functionCode.Replace(':', '_');
            if ("1".Equals(value))
            {
                //增加此权限
                con.Database.ExecuteSqlCommand(string.Format("if not exists (select 1 from tbRoleDetail rd where rd.FunctionCode='{1}' and rd.RoleID={0})" +
     " insert into tbRoleDetail(FunctionCode, RoleID) values('{1}', {0}); ", key, functionCode));
            }
            else
            {
                //删除此权限
                con.Database.ExecuteSqlCommand(string.Format("delete tbRoleDetail where FunctionCode='{1}' and RoleID={0}", key, functionCode));
            }
        }
    }
}

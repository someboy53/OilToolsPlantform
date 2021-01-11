using OilToolsPlantform.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DAL
{
    public class cToolDAL : DalBase
    {
        public cToolDAL(Models.OilToolsPlantformEntities _con)
            : base(_con)
        {
        }

        /// <summary>
        /// 一级目录查询（所有）
        /// </summary>
        /// <param name="request"></param>
        public void CatFQueryBuild(DTO.PQCatQuery request)
        {
            this.AppendInit();
            this.AppendFieldRownum("cfp.SortOrder desc,cf.CatFID");
            this.AppendFieldStr("cf.CatFID as CatID");
            this.AppendFieldStr("cf.Name");
            this.AppendFieldStr("cf.Description");
            this.AppendFieldStr("cfp.CatFPicID as CatPicID");
            this.AppendFieldStr("cfp.PicType");
            this.AppendFieldStr("p.PicID");
            this.AppendFieldStr("p.PicName");
            this.AppendFieldStr("p.StoreName");
            this.AppendFieldStr("p.ThambName");
            this.AppendFieldStr("p.Path");
            this.AppendFromStr("from tbCatF cf,tbCatFPic cfp,tbPic p where cf.CatFID=cfp.CatFID and cfp.PicID=p.PicID and cf.Enabled='1'");
            this.AppendComplete(request.Page, request.PageRow);
        }

        /// <summary>
        /// 二级目录查询（所有）
        /// </summary>
        /// <param name="request"></param>
        public void CatSQueryBuild(DTO.PQCatQuery request)
        {
            this.AppendInit();
            this.AppendFieldRownum("csp.SortOrder desc,cs.CatSID");
            this.AppendFieldStr("cs.CatSID as CatID");
            this.AppendFieldStr("cs.Name");
            this.AppendFieldStr("cs.Description");
            this.AppendFieldStr("csp.CatSPicID as CatPicID");
            this.AppendFieldStr("csp.PicType");
            this.AppendFieldStr("p.PicID");
            this.AppendFieldStr("p.PicName");
            this.AppendFieldStr("p.StoreName");
            this.AppendFieldStr("p.ThambName");
            this.AppendFieldStr("p.Path");
            this.AppendFromStr("from tbCats cs,tbCatsPic csp,tbPic p where cs.CatSID=csp.CatSID and csp.PicID=p.PicID and cs.Enabled='1'");
            this.AppendWhereEqual("cs.CatFID", request.CatFID);
            this.AppendComplete(request.Page, request.PageRow);
        }

        /// <summary>
        /// 工具查询查询（所有）
        /// </summary>
        /// <param name="request"></param>
        public void ToolQueryBuild(DTO.PQToolQuery request)
        {
            this.AppendInit();
            this.AppendFieldRownum("t.SortOrder desc,t.ToolID desc");
            this.AppendFieldStr("t.ToolID");
            this.AppendFieldStr("t.Name");
            this.AppendFieldStr("t.Description");
            this.AppendFieldStr("p.PicType");
            this.AppendFieldStr("p.PicID");
            this.AppendFieldStr("p.PicName");
            this.AppendFieldStr("p.StoreName");
            this.AppendFieldStr("p.Path");
            this.AppendFieldStr("p.ThambName");
            this.AppendFromStr("from tbTool t,(select ROW_NUMBER() over (partition by tp.toolID order by tp.sortorder desc) no1,tp.ToolID,tp.PicType,p.* from tbToolPic tp,tbPic p where tp.PicID=p.PicID and tp.Enabled='1') p");
            this.AppendFromStr("where t.ToolID=p.ToolID and p.no1=1 and t.Enabled='1'");
            this.AppendWhereEqual("t.CatSID", request.CatSID);
            this.AppendWhereContains("t.SearchStr", request.SearchStr);
            this.AppendComplete(request.Page, request.PageRow);
        }

        /// <summary>
        /// 工具查询查询（后台）
        /// </summary>
        /// <param name="request"></param>
        public void ToolQueryBuild1(DTO.PQToolQuery request)
        {
            this.AppendInit();
            this.AppendFieldRownum("t.SortOrder desc,t.ToolID desc");
            this.AppendFieldStr("t.ToolID");
            this.AppendFieldStr("t.Name");
            this.AppendFieldStr("s.Name CatSName");
            this.AppendFieldStr("f.Name CatFName");
            this.AppendFieldStr("t.Description");
            this.AppendFieldStr("ISNULL(d.cc,0) DetailCount");
            this.AppendFieldStr("t.Enabled");
            this.AppendFieldStr("au.status");
            this.AppendFieldStr("case when au.status='3' and au.NextAuditOrgID=(select u.orgID from tbUser u where u.UserID=" + request.UID.ToString() +") then '1' else '0' canAudit");
            this.AppendFromStr("from tbTool t left join (select td.ToolID,count(1) cc from tbToolDetail td group by td.ToolID) d on t.ToolID=d.ToolID left join (select row_number() over (partition by a.TargetTableID order by a.AuditID desc) n,a.TargetTableID,a.status from tbAudit a where a.TargetTableName='tbTool') au on t.ToolID=au.TargetTableID and au.n=1,tbCatS s,tbCatF f");
            this.AppendFromStr("where t.CatSID=s.CatSID and s.CatFID=f.CatFID");
            this.AppendWhereLike("t.name", request.ToolName);
            this.AppendWhereLike("s.name", request.CatSName);
            this.AppendWhereLike("f.name", request.CatFName);
            this.AppendWhereContains("t.SearchStr", request.SearchStr);
            this.AppendComplete(request.Page, request.PageRow);
        }

        public void ViewTool(PQToolView request)
        {
            con.Database.ExecuteSqlCommand("update tbToolExt set ViewCount=ViewCount+1 where toolid=" + request.ToolID);
        }

        internal void CaseQueryBuild(PQCaseQuery request)
        {
            this.AppendInit();
            this.AppendFieldRownum("t.SortOrder desc,t.ToolID desc");
            this.AppendFieldStr("t.ToolID");
            this.AppendFieldStr("t.Name");
            this.AppendFieldStr("s.Name CatSName");
            this.AppendFieldStr("f.Name CatFName");
            this.AppendFieldStr("t.Description");
            this.AppendFieldStr("td.Description CaseContent");
            this.AppendFromStr("from tbTool t,tbCatS s,tbCatF f,tbToolDetail td");
            this.AppendFromStr("where t.CatSID=s.CatSID and s.CatFID=f.CatFID and t.toolID=td.toolID and td.IconName='case.png'");
            this.AppendWhereLike("t.name", request.ToolName);
            this.AppendWhereLike("s.name", request.CatSName);
            this.AppendWhereLike("f.name", request.CatFName);
            this.AppendWhereContains("td.Description", request.CaseContent);
            this.AppendComplete(request.Page, request.PageRow);
        }
    }
}

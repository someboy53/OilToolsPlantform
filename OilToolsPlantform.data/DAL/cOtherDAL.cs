using OilToolsPlantform.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OilToolsPlantform.Data.DAL
{
    public class cOtherDAL : DalBase
    {
        public void SetSBPage(string sql)
        {
            base.sbPage.Clear();
            base.sbPage.Append(sql);
        }

        public cOtherDAL(Models.OilToolsPlantformEntities _con)
            : base(_con)
        {
        }


        public void CountQueryBuild()
        {

            this.AppendInit();
            this.AppendFieldRownum("te.ToolExtID");
            this.AppendFieldStr("isnull(sum(te.ViewCount),0) ViewCount");
            this.AppendFieldStr("isnull(sum(te.LikeCount),0) LikeCount");
            this.AppendFieldStr("isnull(sum(te.FavCount),0) FavCount");
            this.AppendFieldStr("isnull(sum(te.CommentCount),0) CommentCount");
            this.AppendFromStr("from tbToolExt te");
            this.AppendComplete(1, 20);
        }

        public void AnaQueryBuild()
        {
            this.AppendInit();
            this.AppendFieldRownum("ca.Day");
            this.AppendFieldStr("datepart(weekday,ca.Day) Day");
            this.AppendFieldStr("ca.FanAdd");
            this.AppendFieldStr("ca.ToolAdd");
            this.AppendFieldStr("ca.ViewAdd");
            this.AppendFromStr("from tbCountAna ca where ca.Day > GETDATE()-7");
            this.AppendComplete(1, 20);
        }

        public void WechatFanQueryBuild(DTO.PQWechatFanQuery request)
        {
            this.AppendInit();
            this.AppendFieldRownum("wf.WechatFanID");
            this.AppendFieldStr("wf.openid");
            this.AppendFieldStr("wf.subscribe");
            this.AppendFieldStr("wf.nickname");
            this.AppendFieldStr("wf.sex");
            this.AppendFieldStr("wf.city");
            this.AppendFieldStr("wf.country");
            this.AppendFieldStr("wf.province");
            this.AppendFieldStr("wf.language");
            this.AppendFieldStr("wf.subscribe_time");
            this.AppendFieldStr("wf.unionid");
            this.AppendFieldStr("wf.remark");
            this.AppendFieldStr("wf.groupid");
            this.AppendFieldStr("wf.tagid_list");
            this.AppendFieldStr("wf.subscribe_scene");
            this.AppendFieldStr("wf.qr_scene");
            this.AppendFieldStr("wf.qr_scene_str");
            this.AppendFromStr("from tbWechatFan wf");
            this.AppendComplete(request.Page, request.PageRow);
        }

        internal void LogQueryBuild(PQLogQuery request)
        {
            this.AppendInit();
            this.AppendFieldRownum("l.[LogID] desc");
            this.AppendFieldStr("l.[LogID]");
            this.AppendFieldStr("l.[AccountNumber]");
            this.AppendFieldStr("l.[UserID]");
            this.AppendFieldStr("l.[Request]");
            this.AppendFieldStr("l.[Response]");
            this.AppendFieldStr("l.[Description]");
            this.AppendFieldTime("l.[HappenDate]");
            this.AppendFromStr("from tbLog l where 1=1");
            this.AppendWhereContains("l.[AccountNumber]", request.Account);
            string endDate = string.Empty;
            if (!string.IsNullOrWhiteSpace(request.HappenDate))
            {
                endDate = DateTime.Parse(request.HappenDate).AddDays(1).ToString("yyyy-MM-dd");
            }
            this.AppendWhereDate("l.[HappenDate]", request.HappenDate,endDate);
            this.AppendComplete(request.Page, request.PageRow);
        }
    }
}

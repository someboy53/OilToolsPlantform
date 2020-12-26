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
            this.AppendFieldStr("openid");
            this.AppendFieldStr("subscribe");
            this.AppendFieldStr("nickname");
            this.AppendFieldStr("sex");
            this.AppendFieldStr("city");
            this.AppendFieldStr("country");
            this.AppendFieldStr("province");
            this.AppendFieldStr("language");
            this.AppendFieldStr("subscribe_time");
            this.AppendFieldStr("unionid");
            this.AppendFieldStr("remark");
            this.AppendFieldStr("groupid");
            this.AppendFieldStr("tagid_list");
            this.AppendFieldStr("subscribe_scene");
            this.AppendFieldStr("qr_scene");
            this.AppendFieldStr("qr_scene_str");
            this.AppendFromStr("from tbWechatFan wf");
            this.AppendComplete(request.Page, request.PageRow);
        }
    }
}

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

        public void LogQueryBuild(PQLogQuery request)
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

        /// <summary>
        /// 将OpenID列表更新到数据库中
        /// </summary>
        /// <param name="openIds"></param>
        public void AddOpenIDList(List<string> openIds)
        {
            string sqlStr = "if not exists (select 1 from tbWechatFan where openid='{0}') insert into tbWechatFan (openid) values ('{0}') else update tbWechatFan set subscribe='1',updatetime=getdate()-8 where openid='{0}' and subscribe='0';\n";
            System.Text.StringBuilder sb = new StringBuilder();
            int tag = 0;
            foreach (string openid in openIds)
            {
                tag++;
                sb.AppendFormat(sqlStr, openid);
                if (tag % 500 == 0)
                {
                    this.UpdateAndDelete(sb.ToString());
                    sb.Clear();
                }
            }
            if (sb.Length>0)
            {
                this.UpdateAndDelete(sb.ToString());
                sb.Clear();
            }
        }

        /// <summary>
        /// 从数据库中取更新时间为七天以上或无更新时间的openID，获取信息并更新一次获取N个
        /// </summary>
        /// <param name="timeout"></param>
        public List<string> GetOpenIDListNeedUpdate(int timeout)
        {
            this.AppendInit();
            this.AppendFieldRownum("f.WechatFanID");
            this.AppendFieldStr("f.openid");
            this.AppendFromStr("from tbWechatFan f where f.updatetime<getdate()-7");
            this.AppendComplete(1, 30);
            List<string> tmp = new List<string>();
            tmp = this.Query<string>().ToList();
            return tmp;
        }

        /// <summary>
        /// 一次性将获取的用户信息更新到数据库，有多少更新多少
        /// </summary>
        /// <param name="data"></param>
        public void UpdateUserInfoList(List<LWechatFanQuery> users)
        {
            //更新信息语句
            string sqlStr0 = "UPDATE tbWechatFan SET subscribe={0},nickname={1},sex={2},city={3},country={4},province={5},language={6},headimgurl={7},subscribe_time={8},unionid={9},remark={10},groupid={11},tagid_list={12},subscribe_scene={13},qr_scene={14},qr_scene_str={15},updatetime=getdate() WHERE openid={16};\n";
            //更新非订阅语句
            string sqlStr1 = "update tbWechatFan Set subscribe=0,updatetime=getdate() where openid={0}";
            System.Text.StringBuilder sb = new StringBuilder();
            foreach (LWechatFanQuery user in users)
            {
                if (user.subscribe == "0")
                    sb.AppendFormat(sqlStr1, user.openid);
                else
                    sb.AppendFormat(sqlStr0, user.subscribe, user.nickname, user.sex, user.city, user.country, user.province, user.language, user.headimgurl, user.subscribe_time, user.unionid, user.remark, user.groupid, user.tagid_list, user.subscribe_scene, user.qr_scene, user.qr_scene_str, user.openid);
            }
            this.UpdateAndDelete(sb.ToString());
        }
    }
}

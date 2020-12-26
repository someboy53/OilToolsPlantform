using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using com.Origin.CommonLibrary.Framework;
using OilToolsPlantform.Data.DTO;

namespace OilToolsPlantform.Data.BLL
{
    /// <summary>
    /// 工具使用
    /// </summary>
    public class cToolBLL : BllBase
    {
        #region 后台使用方法

        /// <summary>
        /// 创建1、2级目录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DTO.PSCatCreate CreateCat(DTO.PQCatCreate request)
        {
            DTO.PSCatCreate response = new DTO.PSCatCreate();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    if (request.Level == 1)
                    {
                        Models.tbCatF newCatF;
                        Models.tbPic pic = new Models.tbPic();
                        if (request.CatID == 0)
                        {
                            newCatF = con.tbCatF.Create();
                            newCatF.Name = request.Name;
                            newCatF.NameJP = Spell.ChineseJP(request.Name);
                            newCatF.Description = request.Description;
                            newCatF.CreateUser = request.AccountNumber;
                            newCatF.CreateTime = DateTime.Now;
                            newCatF.UpdateUser = request.AccountNumber;
                            newCatF.UpdateTime = DateTime.Now;
                            newCatF.SortOrder = 0;
                            newCatF.Enabled = "1";
                            newCatF = con.tbCatF.Add(newCatF);
                            //图片
                            foreach (DTO.LPicture picture in request.Pictures)
                            {
                                pic = con.tbPic.Create();
                                pic.PicName = picture.PicName;
                                pic.StoreName = picture.StoreName;
                                pic.ThambName = picture.ThambName;
                                pic.Path = picture.Path;
                                pic.CreateUser = request.AccountNumber;
                                pic.CreateTime = DateTime.Now;
                                pic = con.tbPic.Add(pic);
                                Models.tbCatFPic catFPic = new Models.tbCatFPic();
                                catFPic.CatFID = newCatF.CatFID;
                                catFPic.PicID = pic.PicID;
                                catFPic.PicType = picture.PicType;
                                catFPic.SortOrder = 0;
                                catFPic.Enabled = "1";
                                catFPic = con.tbCatFPic.Add(catFPic);
                                newCatF.tbCatFPic.Add(catFPic);
                            }
                        }
                        else
                        {
                            newCatF = con.tbCatF.Find(request.CatID);
                            newCatF.Name = request.Name;
                            newCatF.NameJP = Spell.ChineseJP(request.Name);
                            newCatF.Description = request.Description;
                            newCatF.CreateUser = request.AccountNumber;
                            newCatF.CreateTime = DateTime.Now;
                            newCatF.UpdateUser = request.AccountNumber;
                            newCatF.UpdateTime = DateTime.Now;
                            newCatF.SortOrder = 0;
                            //图片
                            foreach (DTO.LPicture picture in request.Pictures)
                            {
                                if (picture.PicID == 0)
                                {
                                    pic = con.tbPic.Create();
                                    pic.PicName = picture.PicName;
                                    pic.StoreName = picture.StoreName;
                                    pic.ThambName = picture.ThambName;
                                    pic.Path = picture.Path;
                                    pic.CreateUser = request.AccountNumber;
                                    pic.CreateTime = DateTime.Now;
                                    pic = con.tbPic.Add(pic);
                                    Models.tbCatFPic catFPic = new Models.tbCatFPic();
                                    catFPic.CatFID = newCatF.CatFID;
                                    catFPic.PicID = pic.PicID;
                                    catFPic.PicType = picture.PicType;
                                    catFPic.SortOrder = 0;
                                    catFPic.Enabled = "1";
                                    catFPic = con.tbCatFPic.Add(catFPic);
                                    newCatF.tbCatFPic.Add(catFPic);
                                }
                                else
                                {
                                    //已经有了，看是否需要删除
                                    if ("1".Equals(picture.IsDel))
                                    {
                                        pic = con.tbPic.Find(picture.PicID);
                                        con.tbCatFPic.RemoveRange(pic.tbCatFPic);
                                        con.tbPic.Remove(pic);
                                    }
                                }
                            }
                        }
                        if (con.Entry<Models.tbCatF>(newCatF).State != System.Data.Entity.EntityState.Unchanged || con.Entry<Models.tbPic>(pic).State != System.Data.Entity.EntityState.Unchanged)
                            con.SaveChanges();
                    }
                    else
                    {
                        Models.tbCatS newCatS;
                        Models.tbPic pic = new Models.tbPic();
                        if (request.CatID == 0)
                        {
                            newCatS = con.tbCatS.Create();
                            newCatS.Name = request.Name;
                            newCatS.NameJP = Spell.ChineseJP(request.Name);
                            newCatS.NameQP = Spell.ChineseQP(request.Name);
                            newCatS.CatFID = request.ParentID;
                            newCatS.Description = request.Description;
                            newCatS.CreateUser = request.AccountNumber;
                            newCatS.CreateTime = DateTime.Now;
                            newCatS.UpdateUser = request.AccountNumber;
                            newCatS.UpdateTime = DateTime.Now;
                            newCatS.SortOrder = 0;
                            newCatS.Enabled = "1";
                            con.tbCatS.Add(newCatS);
                            //图片
                            foreach (DTO.LPicture picture in request.Pictures)
                            {
                                pic = con.tbPic.Create();
                                pic.PicName = picture.PicName;
                                pic.StoreName = picture.StoreName;
                                pic.ThambName = picture.ThambName;
                                pic.Path = picture.Path;
                                pic.CreateUser = request.AccountNumber;
                                pic.CreateTime = DateTime.Now;
                                pic = con.tbPic.Add(pic);
                                Models.tbCatSPic catSPic = new Models.tbCatSPic();
                                catSPic.PicID = pic.PicID;
                                catSPic.CatSID = newCatS.CatSID;
                                catSPic.PicType = picture.PicType;
                                catSPic.SortOrder = 0;
                                catSPic.Enabled = "1";
                                catSPic = con.tbCatSPic.Add(catSPic);
                                newCatS.tbCatSPic.Add(catSPic);
                            }
                        }
                        else
                        {
                            newCatS = con.tbCatS.Find(request.CatID);
                            newCatS.Name = request.Name;
                            newCatS.NameJP = Spell.ChineseJP(request.Name);
                            newCatS.NameQP = Spell.ChineseQP(request.Name);
                            newCatS.Description = request.Description;
                            newCatS.UpdateUser = request.AccountNumber;
                            newCatS.UpdateTime = DateTime.Now;
                            newCatS.SortOrder = 0;
                            //图片
                            foreach (DTO.LPicture picture in request.Pictures)
                            {
                                if (picture.PicID == 0)
                                {
                                    pic = con.tbPic.Create();
                                    pic.PicName = picture.PicName;
                                    pic.StoreName = picture.StoreName;
                                    pic.ThambName = picture.ThambName;
                                    pic.Path = picture.Path;
                                    pic.CreateUser = request.AccountNumber;
                                    pic.CreateTime = DateTime.Now;
                                    pic = con.tbPic.Add(pic);
                                    Models.tbCatSPic catSPic = new Models.tbCatSPic();
                                    catSPic.CatSID = newCatS.CatSID;
                                    catSPic.PicID = pic.PicID;
                                    catSPic.PicType = picture.PicType;
                                    catSPic.SortOrder = 0;
                                    catSPic.Enabled = "1";
                                    catSPic = con.tbCatSPic.Add(catSPic);
                                    newCatS.tbCatSPic.Add(catSPic);
                                }
                                else
                                {
                                    //已经有了，看是否需要删除
                                    if ("1".Equals(picture.IsDel))
                                    {
                                        pic = con.tbPic.Find(picture.PicID);
                                        con.tbCatSPic.RemoveRange(pic.tbCatSPic);
                                        con.tbPic.Remove(pic);
                                    }
                                }
                            }
                        }
                        if (con.Entry<Models.tbCatS>(newCatS).State != System.Data.Entity.EntityState.Unchanged || con.Entry<Models.tbPic>(pic).State != System.Data.Entity.EntityState.Unchanged)
                            con.SaveChanges();
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cToolBLL.CreateCat出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        public PSCatSQuery CatSQuery(PQCatSQuery request)
        {
            PSCatSQuery response = new PSCatSQuery();
            response.data = this.CatSQuery(request.CatFID).Select(p => new KeyValuePair<int, string>(p.CatSID, p.Name)).ToList();
            return response;
        }

        /// <summary>
        /// 对目录进行启禁用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DTO.ResponseBase CatEnable(DTO.PQCatEnable request)
        {
            return base.ObjEnable(request.Level == 1 ? "tbCatF" : "tbCatS", request.AccountNumber, request.ID, request.Enabled);
        }

        /// <summary>
        /// 查询工具
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DTO.PSToolQuery ToolQuery(DTO.PQToolQuery request)
        {
            DTO.PSToolQuery response = new DTO.PSToolQuery();
            try
            {
                DAL.cToolDAL dal = new DAL.cToolDAL(con);
                dal.ToolQueryBuild1(request);
                response.data = dal.Query<DTO.LToolQuery>();
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

        /// <summary>
        /// 创建工具
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DTO.PSToolModify ToolModify(DTO.PQToolModify request)
        {
            DTO.PSToolModify response = new DTO.PSToolModify();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    Models.tbTool tool = new Models.tbTool();
                    Models.tbPic pic = new Models.tbPic();
                    Models.tbToolDetail toolDetail = new Models.tbToolDetail();
                    Models.tbToolExt toolExt = new Models.tbToolExt();
                    if ("1".Equals(request.IsDel))
                    {
                        tool = con.tbTool.Find(request.ToolID);
                        foreach (Models.tbToolDetail tmp in tool.tbToolDetail)
                        {
                            con.tbToolDetail.Remove(tmp);
                        }
                        foreach (Models.tbToolPic tmp in tool.tbToolPic)
                        {
                            Models.tbPic p = con.tbPic.Find(tmp.PicID);
                            con.tbPic.Remove(p);
                            con.tbToolPic.Remove(tmp);
                        }
                        foreach (Models.tbToolExt tmp in tool.tbToolExt)
                        {
                            con.tbToolExt.Remove(tmp);
                        }
                        con.tbTool.Remove(tool);
                    }
                    else
                    {
                        string searchStr = string.Empty;
                        if (request.ToolID == 0)
                        {
                            tool = con.tbTool.Create();
                            tool.CatSID = request.CatSID;
                            tool.Name = request.Name;
                            tool.NameJP = Spell.ChineseJP(request.Name);
                            tool.NameQP = Spell.ChineseQP(request.Name);
                            tool.Description = request.Description;
                            tool.CreateUser = request.AccountNumber;
                            tool.CreateTime = DateTime.Now;
                            tool.UpdateUser = request.AccountNumber;
                            tool.UpdateTime = DateTime.Now;
                            tool.SortOrder = 0;
                            tool.Enabled = "1";
                            tool=con.tbTool.Add(tool);
                            searchStr = string.Format("|-|{0}|-|{1}|-|{2}|-|{3}", tool.Name, tool.NameJP, tool.NameQP, tool.Description.Length > 100 ? tool.Description.Substring(0, 100) : tool.Description);
                            //图片
                            foreach (DTO.LPicture picture in request.Pictures)
                            {
                                pic = con.tbPic.Create();
                                pic.PicName = picture.StoreName;
                                pic.StoreName = picture.StoreName;
                                pic.ThambName = string.Format("{0}_min{1}", System.IO.Path.GetFileNameWithoutExtension(picture.StoreName), System.IO.Path.GetExtension(picture.StoreName));
                                pic.Path = picture.Path;
                                pic.CreateUser = request.AccountNumber;
                                pic.CreateTime = DateTime.Now;
                                pic = con.tbPic.Add(pic);
                                Models.tbToolPic toolPic = new Models.tbToolPic();
                                toolPic.ToolID = tool.ToolID;
                                toolPic.PicID = pic.PicID;
                                toolPic.PicType = picture.PicType;
                                toolPic.SortOrder = 0;
                                toolPic.Enabled = "1";
                                toolPic = con.tbToolPic.Add(toolPic);
                                tool.tbToolPic.Add(toolPic);
                            }
                            if (request.ToolDetails.Count == 0)
                            {
                                //先组装tooldetail列表
                                if ("on".Equals(request.ShowIntro))
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.IntroID, Name = "工具介绍", Description = request.IntroDesc, DetailIcon = 0, IconName = "intro.png" });
                                else
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.IntroID, IsDel = "1" });
                                if ("on".Equals(request.ShowFunc))
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.FuncID, Name = "功能特点", Description = request.FuncDesc, DetailIcon = 0, IconName = "func.png" });
                                else
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.FuncID, IsDel = "1" });
                                if ("on".Equals(request.ShowParam))
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.ParamID, Name = "技术参数", Description = request.ParamDesc, DetailIcon = 0, IconName = "param.png" });
                                else
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.ParamID, IsDel = "1" });
                                if ("on".Equals(request.ShowCase))
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.CaseID, Name = "应用案例", Description = request.CaseDesc, DetailIcon = 0, IconName = "case.png" });
                                else
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.CaseID, IsDel = "1" });
                                if ("on".Equals(request.ShowStock))
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.StockID, Name = "库存情况", Description = request.StockDesc, DetailIcon = 0, IconName = "stock.png" });
                                else
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.StockID, IsDel = "1" });
                                if ("on".Equals(request.ShowContact))
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.ContactID, Name = "联系我们", Description = request.ContactDesc, DetailIcon = 0, IconName = "contact.png" });
                                else
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.ContactID, IsDel = "1" });
                            }
                            //详情
                            foreach (DTO.LToolDetail detail in request.ToolDetails)
                            {
                                toolDetail = con.tbToolDetail.Create();
                                toolDetail.ToolID = tool.ToolID;
                                toolDetail.DetailIcon = detail.DetailIcon;
                                if (toolDetail.DetailIcon == 0)
                                    toolDetail.IconName = detail.IconName;
                                else
                                    toolDetail.IconName = "DetailIcon" + toolDetail.DetailIcon.ToString() + ".png";
                                toolDetail.Name = detail.Name;
                                toolDetail.NameJP = Spell.ChineseJP(detail.Name);
                                toolDetail.NameQP = Spell.ChineseQP(detail.Name);
                                toolDetail.Description = detail.Description;
                                toolDetail.CreateUser = request.AccountNumber;
                                toolDetail.CreateTime = DateTime.Now;
                                toolDetail.UpdateUser = request.AccountNumber;
                                toolDetail.UpdateTime = DateTime.Now;
                                toolDetail.SortOrder = 0;
                                toolDetail.Enabled = "1";
                                con.tbToolDetail.Add(toolDetail);
                                searchStr += string.Format("|-|{0}", tool.Description.Length > 50 ? tool.Description.Substring(0, 50) : tool.Description);
                            }
                            toolExt = con.tbToolExt.Create();
                            toolExt.ToolID = tool.ToolID;
                            toolExt.CommentCount = 0;
                            toolExt.FavCount = 0;
                            toolExt.ViewCount = 0;
                            toolExt.LikeCount = 0;
                            con.tbToolExt.Add(toolExt);
                        }
                        else
                        {
                            tool = con.tbTool.Find(request.ToolID);
                            tool.Name = request.Name;
                            tool.NameJP = Spell.ChineseJP(request.Name);
                            tool.NameQP = Spell.ChineseQP(request.Name);
                            tool.Description = request.Description;
                            tool.UpdateUser = request.AccountNumber;
                            tool.UpdateTime = DateTime.Now;
                            searchStr = string.Format("|-|{0}|-|{1}|-|{2}|-|{3}", tool.Name, tool.NameJP, tool.NameQP, tool.Description.Length>100? tool.Description.Substring(0, 100): tool.Description);
                            //图片
                            foreach (DTO.LPicture picture in request.Pictures)
                            {
                                if (picture.PicID == 0)
                                {
                                    pic = con.tbPic.Create();
                                    pic.PicName = picture.StoreName;
                                    pic.StoreName = picture.StoreName;
                                    pic.ThambName = string.Format("{0}_min{1}", System.IO.Path.GetFileNameWithoutExtension(picture.StoreName), System.IO.Path.GetExtension(picture.StoreName));
                                    pic.Path = picture.Path;
                                    pic.CreateUser = request.AccountNumber;
                                    pic.CreateTime = DateTime.Now;
                                    pic = con.tbPic.Add(pic);
                                    Models.tbToolPic toolPic = new Models.tbToolPic();
                                    toolPic.ToolID = tool.ToolID;
                                    toolPic.PicID = pic.PicID;
                                    toolPic.PicType = picture.PicType;
                                    toolPic.SortOrder = 0;
                                    toolPic.Enabled = "1";
                                    toolPic = con.tbToolPic.Add(toolPic);
                                    tool.tbToolPic.Add(toolPic);
                                }
                                else
                                {
                                    //已经有了，看是否需要删除
                                    if ("1".Equals(picture.IsDel))
                                    {
                                        pic = con.tbPic.Find(picture.PicID);
                                        con.tbToolPic.RemoveRange(pic.tbToolPic);
                                        con.tbPic.Remove(pic);
                                    }
                                    else
                                    {
                                        //更新图片路径即可
                                        pic = con.tbPic.Find(picture.PicID);
                                        pic.PicName = picture.StoreName;
                                        pic.StoreName = picture.StoreName;
                                        pic.ThambName = string.Format("{0}_min{1}", System.IO.Path.GetFileNameWithoutExtension(picture.StoreName), System.IO.Path.GetExtension(picture.StoreName));
                                        pic.Path = picture.Path;
                                        pic.CreateTime = DateTime.Now;
                                        pic.CreateUser = request.AccountNumber;
                                    }
                                }
                            }
                            if (request.ToolDetails.Count == 0)
                            {
                                //先组装tooldetail列表
                                if ("on".Equals(request.ShowIntro))
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.IntroID, Name = "工具介绍", Description = request.IntroDesc, DetailIcon = 0, IconName = "intro.png" });
                                else
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.IntroID, IsDel = "1" });
                                if ("on".Equals(request.ShowFunc))
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.FuncID, Name = "功能特点", Description = request.FuncDesc, DetailIcon = 0, IconName = "func.png" });
                                else
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.FuncID, IsDel = "1" });
                                if ("on".Equals(request.ShowParam))
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.ParamID, Name = "技术参数", Description = request.ParamDesc, DetailIcon = 0, IconName = "param.png" });
                                else
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.ParamID, IsDel = "1" });
                                if ("on".Equals(request.ShowCase))
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.CaseID, Name = "应用案例", Description = request.CaseDesc, DetailIcon = 0, IconName = "case.png" });
                                else
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.CaseID, IsDel = "1" });
                                if ("on".Equals(request.ShowStock))
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.StockID, Name = "库存情况", Description = request.StockDesc, DetailIcon = 0, IconName = "stock.png" });
                                else
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.StockID, IsDel = "1" });
                                if ("on".Equals(request.ShowContact))
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.ContactID, Name = "联系我们", Description = request.ContactDesc, DetailIcon = 0, IconName = "contact.png" });
                                else
                                    request.ToolDetails.Add(new LToolDetail() { ToolID = request.ToolID, ToolDetailID = request.ContactID, IsDel = "1" });
                            }
                            //详情
                            foreach (DTO.LToolDetail detail in request.ToolDetails)
                            {
                                if (detail.ToolDetailID == 0)
                                {
                                    if (!"1".Equals(detail.IsDel))
                                    {
                                        toolDetail = con.tbToolDetail.Create();
                                        toolDetail.ToolID = detail.ToolID;
                                        toolDetail.DetailIcon = detail.DetailIcon;
                                        if (toolDetail.DetailIcon == 0)
                                            toolDetail.IconName = detail.IconName;
                                        else
                                            toolDetail.IconName = "DetailIcon" + toolDetail.DetailIcon.ToString() + ".png";
                                        toolDetail.Name = detail.Name;
                                        toolDetail.NameJP = Spell.ChineseJP(detail.Name);
                                        toolDetail.NameQP = Spell.ChineseQP(detail.Name);
                                        toolDetail.Description = detail.Description.Length > 50 ? detail.Description.Substring(0, 50) : detail.Description;
                                        toolDetail.CreateUser = request.AccountNumber;
                                        toolDetail.CreateTime = DateTime.Now;
                                        toolDetail.UpdateUser = request.AccountNumber;
                                        toolDetail.UpdateTime = DateTime.Now;
                                        toolDetail.SortOrder = 0;
                                        toolDetail.Enabled = "1";
                                        toolDetail = con.tbToolDetail.Add(toolDetail);
                                    }
                                }
                                else
                                {
                                    if ("1".Equals(detail.IsDel))
                                    {
                                        toolDetail = con.tbToolDetail.Find(detail.ToolDetailID);
                                        con.tbToolDetail.Remove(toolDetail);
                                    }
                                    else
                                    {
                                        toolDetail = con.tbToolDetail.Find(detail.ToolDetailID);
                                        toolDetail.DetailIcon = detail.DetailIcon;
                                        if (toolDetail.DetailIcon == 0)
                                            toolDetail.IconName = detail.IconName;
                                        else
                                            toolDetail.IconName = "DetailIcon" + toolDetail.DetailIcon.ToString() + ".png";
                                        toolDetail.Name = detail.Name;
                                        toolDetail.NameJP = Spell.ChineseJP(detail.Name);
                                        toolDetail.NameQP = Spell.ChineseQP(detail.Name);
                                        toolDetail.Description = detail.Description;
                                        toolDetail.UpdateUser = request.AccountNumber;
                                        toolDetail.UpdateTime = DateTime.Now;
                                        searchStr += string.Format("|-|{0}", detail.Description.Length>50? detail.Description.Substring(0, 50): detail.Description);
                                    }
                                }
                            }
                        }
                        tool.SearchStr = searchStr.Length > 500 ? searchStr.Substring(0, 500) : searchStr;
                    }
                    if (con.Entry<Models.tbTool>(tool).State != System.Data.Entity.EntityState.Unchanged || con.Entry<Models.tbPic>(pic).State != System.Data.Entity.EntityState.Unchanged || con.Entry<Models.tbToolDetail>(toolDetail).State != System.Data.Entity.EntityState.Unchanged)
                        con.SaveChanges();
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cToolBLL.ToolModify出错！", ex);
                throw;
            }
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            return response;
        }

        /// <summary>
        /// 查看工具
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DTO.PSToolView ToolView(DTO.PQToolView request)
        {
            DTO.PSToolView response = new DTO.PSToolView();
            try
            {
                using (var scope = new System.Transactions.TransactionScope())
                {
                    if (request.ToolID > 0)
                    {
                        Models.tbTool tool = new Models.tbTool();
                        tool = con.tbTool.Find(request.ToolID);
                        response.ToolID = tool.ToolID;
                        response.Name = tool.Name;
                        response.Description = tool.Description;
                        response.CatSID = tool.CatSID;
                        List<Models.tbToolPic> toolPics = con.tbToolPic.Where(p => p.ToolID == tool.ToolID).ToList();
                        Models.tbPic pic = new Models.tbPic();
                        if (toolPics.Count > 0)
                        {
                            pic = con.tbPic.Find(toolPics[0].PicID);
                            DTO.LPicture lpic = new DTO.LPicture() { PicID = pic.PicID, PicName = pic.PicName, Path = pic.Path, PicType = toolPics[0].PicType, StoreName = pic.StoreName, ThambName = pic.ThambName };
                            response.PictureList.Add(lpic);
                        }
                        Models.tbCatS cats = con.tbCatS.Find(tool.CatSID);
                        response.CatFID = cats.CatFID;
                        if (tool.tbToolDetail.Count > 0)
                        {
                            foreach (Models.tbToolDetail td in tool.tbToolDetail)
                            {
                                response.ToolDetailList.Add(new LToolDetail() { ToolID = td.ToolID, Name = td.Name, ToolDetailID = td.ToolDetailID, Description = td.Description, DetailIcon = td.DetailIcon, IconName = td.IconName, IsDel = "0" });
                            }
                        }
                        response.CatSList = this.CatSQuery(response.CatFID).Select(p => new KeyValuePair<int, string>(p.CatSID, p.Name)).ToList();
                    }
                    response.CatFList = this.CatFQuery().Select(p => new KeyValuePair<int, string>(p.CatFID, p.Name)).ToList();

                    response.ErrorCode = "A_0";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("cToolBLL.ToolView！", ex);
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

        /// <summary>
        /// 对工具进行启禁用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DTO.ResponseBase ToolEnable(DTO.PQToolEnable request)
        {
            return base.ObjEnable("tbTool", request.AccountNumber, request.ID, request.Enabled);
        }

        /// <summary>
        /// 查询所有的一级目录
        /// </summary>
        /// <returns></returns>
        public List<Models.tbCatF> CatFQuery()
        {
            return con.tbCatF.Where(p => p.Enabled == "1").OrderByDescending(p => p.SortOrder).ThenByDescending(p => p.CatFID).ToList();
        }

        /// <summary>
        /// 查询一级目录下的二级目录
        /// </summary>
        /// <param name="CatFID"></param>
        /// <returns></returns>
        private List<Models.tbCatS> CatSQuery(int CatFID)
        {
            return con.tbCatS.Where(p => p.Enabled == "1" && p.CatFID == CatFID).OrderByDescending(p => p.SortOrder).ThenByDescending(p => p.CatSID).ToList();
        }

        /// <summary>
        /// 查询二级目录下的工具
        /// </summary>
        /// <param name="CatSID"></param>
        /// <returns></returns>
        public List<Models.tbTool> ToolQuery(int CatSID)
        {
            return con.tbTool.Where(p => p.CatSID == CatSID).OrderByDescending(p => p.SortOrder).ThenByDescending(p => p.ToolID).ToList();
        }

        /// <summary>
        /// 根据字符串查找符合的工具
        /// </summary>
        /// <param name="SearchStr"></param>
        /// <returns></returns>
        public List<Models.tbTool> ToolSearch(string SearchStr)
        {
            return con.tbTool.Where(p => p.Name.Contains(SearchStr) || p.NameJP.Contains(SearchStr) || p.NameQP.Contains(SearchStr) || p.Description.Contains(SearchStr)).OrderByDescending(p => p.ToolID).ToList();
        }
        #endregion

        #region 前台方法
        /// <summary>
        /// 查询一、二级目录，不分页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DTO.PSCatQuery QueryCat(DTO.PQCatQuery request)
        {
            DTO.PSCatQuery response = new DTO.PSCatQuery();
            try
            {
                DAL.cToolDAL dal = new DAL.cToolDAL(con);
                if ("1".Equals(request.Level))
                {
                    //一级目录
                    dal.CatFQueryBuild(request);
                }
                else
                {
                    dal.CatSQueryBuild(request);
                }
                response.catQueryList = dal.Query<DTO.LCatQuery>();
                int num = dal.Count();
                response.Page = request.Page;
                response.MaxNum = num;
                response.MaxPage = MathExpansion.CaculatPage(num, request.PageRow);

                response.ErrorMessage = rm.GetString(response.ErrorCode);
            }
            catch (Exception ex)
            {
                LogHelper.Error("cToolBLL.QueryCat出错！", ex);
                throw;
            }
            return response;
        }

        /// <summary>
        /// 查询工具
        /// 1.根据二级目录名
        /// 2.根据搜索的字符串
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DTO.PSToolQuery QueryTool(DTO.PQToolQuery request)
        {
            DTO.PSToolQuery response = new DTO.PSToolQuery();
            try
            {
                DAL.cToolDAL dal = new DAL.cToolDAL(con);
                dal.ToolQueryBuild(request);
                response.ToolQueryList = dal.Query<DTO.LToolQuery>();
                if (request.CatSID != null)
                {
                    Models.tbCatS cat = con.tbCatS.Find(request.CatSID);
                    response.CatSName = cat.Name;
                }
                int num = dal.Count();
                response.Page = request.Page;
                response.MaxNum = num;
                response.MaxPage = MathExpansion.CaculatPage(num, request.PageRow);

                response.ErrorMessage = rm.GetString(response.ErrorCode);
            }
            catch (Exception ex)
            {
                LogHelper.Error("cToolBLL.QueryTool出错！", ex);
                throw;
            }
            return response;
        }

        /// <summary>
        /// 根据工具ID返回工具的查看数据
        /// </summary>
        /// <param name="ToolID"></param>
        /// <returns></returns>
        public DTO.PSToolView ViewTool(DTO.PQToolView request)
        {
            DTO.PSToolView view = new DTO.PSToolView();
            Models.tbTool tool = con.tbTool.Find(request.ToolID);
            view.ToolID = tool.ToolID;
            view.Name = tool.Name;
            view.Description = tool.Description;
            foreach (Models.tbToolPic tpic in tool.tbToolPic)
            {
                DTO.LPicture picture = new DTO.LPicture();
                picture.PicType = tpic.PicType;
                picture.PicID = tpic.tbPic.PicID;
                picture.PicName = tpic.tbPic.PicName;
                picture.StoreName = tpic.tbPic.StoreName;
                picture.ThambName = tpic.tbPic.ThambName;
                picture.Path = tpic.tbPic.Path;
                view.PictureList.Add(picture);
            }
            foreach (Models.tbToolDetail detail in tool.tbToolDetail)
            {
                DTO.LToolDetail td = new DTO.LToolDetail();
                td.ToolID = detail.ToolID;
                td.ToolDetailID = detail.ToolDetailID;
                td.Name = detail.Name;
                td.IconName = detail.IconName;
                td.Description = detail.Description;
                view.ToolDetailList.Add(td);
            }
            Data.DAL.cToolDAL dal = new DAL.cToolDAL(con);
            dal.ViewTool(request);
            return view;
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;

namespace OilToolsPlantform.Data.DAL
{
    /// <summary>
    /// 所有Bll的基类
    /// 主要处理BLL对EF的Context的处理
    /// </summary>
    public class DalBase
    {
        /// <summary>
        /// 传入的数据库对象
        /// </summary>
        protected Models.OilToolsPlantformEntities con;
        public DalBase(Models.OilToolsPlantformEntities _con)
        {
            this.con = _con;
        }

        /// <summary>
        /// 根据表名启用禁用ID
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="accountNumber">当前用户帐号</param>
        /// <param name="id">目标ID</param>
        /// <param name="enabled">启禁用</param>
        /// <returns>成功失败</returns>
        public Boolean ObjEnable(string tableName,string accountNumber, int id, string enabled)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("UPDATE ");
            sbSql.Append(tableName);
            sbSql.Append(" SET Enabled='").Append(enabled).Append("'");
            sbSql.Append(",UpdateTime=GETDATE(),UpdateUser='").Append(accountNumber).Append("'");
            sbSql.Append(" WHERE ").Append(tableName.Remove(0,3)).Append("ID=").Append(id.ToString());
            int i=this.UpdateAndDelete(sbSql.ToString());
            if (i == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 对表中的数量进行增加
        /// </summary>
        /// <param name="TargetTableName">目标表名</param>
        /// <param name="CountName">数量名:Comment,AuditComment,View,Favorite,Like,PreOrder,Buy</param>
        /// <param name="TargetTableID">目标表ID</param>
        internal void CountUp(string TargetTableName, string CountName, int TargetTableID)
        {
            string tmp = string.Empty;
            if(TargetTableName.Length>4)
                tmp = string.Format("update {0} set {1}Count={1}Count + 1 where {3}ID={2}", TargetTableName, CountName, TargetTableID.ToString(), TargetTableName.Remove(0, 3));
            else
                tmp = string.Format("update tbcBaseCount set {1}Count={1}Count + 1 where TargetID={2} and TableType='{0}'", TargetTableName, CountName, TargetTableID.ToString());
            this.UpdateAndDelete(tmp);
            //System.Threading.Tasks.Task.Factory.StartNew(() => this.UpdateAndDelete(tmp));
        }

        /// <summary>
        /// 对表中数量进行减少
        /// </summary>
        /// <param name="TargetTableName">目标表名</param>
        /// <param name="CountName">数量名:Comment,AuditComment,View,Favorite,Like,PreOrder,Buy</param>
        /// <param name="TargetTableID">目标表ID</param>
        internal void CountDown(string TargetTableName, string CountName, int TargetTableID)
        {
            string tmp = string.Empty;
            if(TargetTableName.Length>4)
                tmp = string.Format("update {0} set {1}Count={1}Count - 1 where {3}ID={2}", TargetTableName, CountName, TargetTableID.ToString(), TargetTableName.Remove(0, 3));
            else
                tmp = string.Format("update tbcBaseCount set {1}Count={1}Count - 1 where TargetID={2} and TableType='{0}'", TargetTableName, CountName, TargetTableID.ToString());
            this.UpdateAndDelete(tmp);
            //System.Threading.Tasks.Task.Factory.StartNew(() => this.UpdateAndDelete(tmp));
        }

        #region SQL处理
        /// <summary>
        /// 存储当前DAL的分页语句
        /// </summary>
        protected StringBuilder sbPage { get; set; }
        /// <summary>
        /// 存储当前DAL的计数语句
        /// </summary>
        protected StringBuilder sbCount { get; set; }

        /// <summary>
        /// 初始化Append
        /// </summary>
        public void AppendInit()
        {
            this.sbPage = new StringBuilder();
            this.sbCount = new StringBuilder();
            sbPage.Append("select * from (select");
            sbCount.Append("select count(1)");
        }
        /// <summary>
        /// 增加查询字符串的RowNum
        /// </summary>
        /// <param name="fieldName"></param>
        public void AppendFieldRownum(string fieldName)
        {
            sbPage.Append(string.Format(" ROW_NUMBER() OVER (ORDER BY {0}) rowNum", fieldName));
        }
        /// <summary>
        /// 增加查询字符串的Select
        /// </summary>
        /// <param name="content"></param>
        public void AppendFieldStr(string fieldName)
        {
            sbPage.Append(string.Format(",{0}", fieldName));
        }
        /// <summary>
        /// 增加查询字符串的日期Select
        /// </summary>
        /// <param name="fieldName"></param>
        public void AppendFieldDate(string fieldName)
        {
            string[] tmp = fieldName.Split('.');
            string aliasName = tmp[0];
            fieldName = tmp[1];
            sbPage.Append(string.Format(",CONVERT(CHAR(10),{0}.{1},120) as {1}", aliasName, fieldName));
        }
        /// <summary>
        /// 增加查询字符串的时间Select
        /// </summary>
        /// <param name="fieldName"></param>
        public void AppendFieldTime(string fieldName)
        {
            string[] tmp = fieldName.Split('.');
            string aliasName = tmp[0];
            fieldName = tmp[1];
            sbPage.Append(string.Format(",CONVERT(CHAR(19),{0}.{1},120) as {1}", aliasName, fieldName));
        }
        /// <summary>
        /// 增加查询字符串的时间Select,此时间可能为空
        /// </summary>
        /// <param name="fieldName"></param>
        public void AppendFieldTimeNullable(string fieldName)
        {
            string[] tmp = fieldName.Split('.');
            string aliasName = tmp[0];
            fieldName = tmp[1];
            sbPage.Append(string.Format(",case when {0}.{1} is null then '' else CONVERT(CHAR(10),{0}.{1},120) end AS {1}", aliasName, fieldName));
        }
        /// <summary>
        /// 增加查询字符串的时间Select,此时间可能为空
        /// </summary>
        /// <param name="fieldName"></param>
        public void AppendFieldOnlyTime(string fieldName)
        {
            string[] tmp = fieldName.Split('.');
            string aliasName = tmp[0];
            fieldName = tmp[1];
            sbPage.Append(string.Format(",case when {0}.{1} is null then '' else SubString(CONVERT(CHAR(16),{0}.{1},120),12,5) end AS {1}", aliasName, fieldName));
        }
        /// <summary>
        /// 增加From语句
        /// </summary>
        /// <param name="content"></param>
        public void AppendFromStr(string content)
        {
            sbPage.Append(" " + content);
            sbCount.Append(" " + content);
        }

        /// <summary>
        /// 增加条件字符串,完全自定
        /// </summary>
        /// <param name="whereStr"></param>
        public void AppendWhereStr(string whereStr)
        {
            if (!"".Equals(whereStr) && whereStr != null)
            {
                string tmp = string.Format(" and {0}", whereStr);
                sbPage.Append(tmp);
                sbCount.Append(tmp);
            }
        }

        /// <summary>
        /// 增加条件Like语句,右Like
        /// </summary>
        /// <param name="filedName"></param>
        /// <param name="value"></param>
        public void AppendWhereLike(string filedName, string value)
        {
            if (!"".Equals(value) && value != null)
            {
                string tmp = string.Empty;
                tmp = string.Format(" and {0} like '{1}%'", filedName, value);
                sbPage.Append(tmp);
                sbCount.Append(tmp);
            }
        }
        /// <summary>
        /// 增加条件Like语句,全Like
        /// </summary>
        /// <param name="filedName"></param>
        /// <param name="value"></param>
        public void AppendWhereContains(string filedName, string value)
        {
            if (!"".Equals(value) && value != null)
            {
                string tmp = string.Empty;
                tmp = string.Format(" and {0} like '%{1}%'", filedName, value);
                sbPage.Append(tmp);
                sbCount.Append(tmp);
            }
        }
        /// <summary>
        /// 增加条件等于语句
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public void AppendWhereEqual(string fieldName, object value)
        {
            string tmp = string.Empty;
            if (value != null)
            {
                if (value.GetType().Equals(typeof(string)))
                {
                    if (!"".Equals(value.ToString()))
                        tmp = string.Format(" and {0}='{1}'", fieldName, value);
                }
                else
                {
                    tmp = string.Format(" and {0}={1}", fieldName, value);
                }
                sbPage.Append(tmp);
                sbCount.Append(tmp);
            }
        }

        /// <summary>
        /// 增加条件等于二级分类语句
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        public void AppendWhereValue2(string fieldName, string value1, string value2)
        {
            string tmp = string.Empty;
            //一级分类不能为空或无值
            if (value1 == null)
                return;
            else if (value1 == string.Empty)
                return;
            tmp = string.Format(" and {0}1='{1}'", fieldName, value1);
            if (value2 != null)
            {
                if (value2 != string.Empty)
                {
                    tmp = string.Format(" and {0}2='{1}'", fieldName, value2);
                }
            }
            sbPage.Append(tmp);
            sbCount.Append(tmp);
        }

        /// <summary>
        /// 增加条件Like名称语句
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public void AppendWhereName(string fieldName, string value)
        {
            if (!"".Equals(value) && value != null)
            {
                string[] tmps = fieldName.Split('.');
                string aliasName = tmps[0];
                fieldName = tmps[1];
                string tmp = string.Format(" and ({1}.{2} like '{0}%' or {1}.NameQP like '{0}%' or {1}.NameJP like '{0}%')", value, aliasName, fieldName);
                sbPage.Append(tmp);
                sbCount.Append(tmp);
            }
        }
        /// <summary>
        /// 增加条件In语句
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="obj"></param>
        public void AppendWhereIn(string fieldName, List<int?> obj)
        {
            string tmp = string.Empty;
            if (obj.Count > 0)
            {
                tmp = string.Format(" and {0} in ({1})", fieldName, string.Join(",", obj.ToArray()));
            }
            else
                tmp = string.Format(" and {0} in ({1})", fieldName, "0");
            sbPage.Append(tmp);
            sbCount.Append(tmp);
        }
        /// <summary>
        /// 增加条件In语句
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="obj"></param>
        public void AppendWhereIn(string fieldName, List<int> obj)
        {
            string tmp = string.Empty;
            if (obj.Count > 0)
            {
                tmp = string.Format(" and {0} in ({1})", fieldName, string.Join(",", obj.ToArray()));
            }
            else
                tmp = string.Format(" and {0} in ({1})", fieldName, "0");
            sbPage.Append(tmp);
            sbCount.Append(tmp);

        }

        /// <summary>
        /// 增加时间段条件
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="start">开始日期</param>
        /// <param name="end">结束日期</param>
        public void AppendWhereDate(string fieldName, string start, string end)
        {
            string tmp = string.Empty;
            if (start == null) start = "";
            if (end == null) end = "";
            if ("".Equals(start) && !"".Equals(end))
                tmp = string.Format(" and {0} <= '{1}'", fieldName, end);
            if (!"".Equals(start) && "".Equals(end))
                tmp = string.Format(" and {0} >= '{1}'", fieldName, start);
            if (!"".Equals(start) && !"".Equals(end))
                tmp = string.Format(" and {0} between '{1}' and '{2}'", fieldName, start, end);
            else
                return;
            sbPage.Append(tmp);
            sbCount.Append(tmp);
        }

        /// <summary>
        /// Append结束
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageRow"></param>
        public void AppendComplete(int page, int pageRow)
        {
            sbPage.Append(") tb where 1 =1 ");
            //分页
            if (page > 0 && pageRow > 0)
            {
                sbPage.Append(" and tb.rowNum > ").Append((page - 1) * pageRow);
                sbPage.Append(" and tb.rowNum <=").Append((page) * pageRow);
            }
            sbPage.Append(" order by tb.rowNum ");
        }
        #endregion

 
        #region IRepository<T> 成员

        //执行SQL并返回list<T1>的方法
        public List<T1> Query<T1>(string sql)
        {
            List<T1> list = con.Database.SqlQuery<T1>(sql).ToList();
            return list;
        }

        /// <summary>
        /// 执行分页语句
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public List<T1> Query<T1>()
        {
            return this.Query<T1>(sbPage.ToString());
        }

        //通过传入的查询条数的SQL返回查询条数
        public int Count(string sql)
        {
            var num = con.Database.SqlQuery<int>(sql).ToList();
            if (num.Count == 0)
            {
                return 0;
            }
            else
            {
                return num[0];
            }
        }

        /// <summary>
        /// 执行计数语句
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return this.Count(sbCount.ToString());
        }
        /// <summary>
        /// 执行更新和删除sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int UpdateAndDelete(string sql)
        {
            int num = con.Database.ExecuteSqlCommand(sql);
            return num;
        }
        #endregion
    }
}

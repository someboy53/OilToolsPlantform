using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;

namespace OilToolsPlantform.Data.BLL
{
    /// <summary>
    /// 所有Bll的基类
    /// 主要处理BLL对EF的Context的处理
    /// </summary>
    public class BllBase : IDisposable
    {
        public Models.OilToolsPlantformEntities con;
        public static string RootPath = System.Configuration.ConfigurationManager.AppSettings["RootPath"];
        public ResourceManager rm;
        public BllBase()
        {
            //获取数据库连接
            con = new Models.OilToolsPlantformEntities();
            rm = new ResourceManager("OilToolsPlantform.data.ErrorCode", typeof(ErrorCode).Assembly);
            //在日志中写入执行的SQL
            con.Database.Log = s => com.Origin.CommonLibrary.Framework.LogHelper.SQL(s);
        }

        ~BllBase()
        {
            //垃圾回收器将调用该方法，因此参数需要为false。
            Dispose(false);
        }

        #region 清除原文件方法
        /// <summary>
        /// 删除传入的文件
        /// 只删除了HTML文件,而文件引用的其它文件未处理,如有必要,后期需开发一个单独的程序对所有HTML中的引用文件进行遍历,并将其在数据库中记录在案
        /// </summary>
        /// <param name="path"></param>
        protected void FileRemove(string path)
        {
            if ("".Equals(path) || path == null)
                return;
            string realPath = System.IO.Path.Combine(RootPath, path);
            if (System.IO.File.Exists(realPath))
            {
                //文件存在,删除
                System.IO.File.Delete(realPath);
            }
        }
        #endregion

        #region 启禁用方法
        protected DTO.ResponseBase ObjEnable(string tableName, string accountNumber, int id, Boolean enabled)
        {
            DTO.ResponseBase response = new DTO.ResponseBase();
            response.ErrorCode = "1";
            response.ErrorMessage = rm.GetString(response.ErrorCode);
            if (id > 0)
            {
                DAL.DalBase dbase = new DAL.DalBase(con);
                if (dbase.ObjEnable(tableName, accountNumber, id, enabled ? "1" : "0"))
                {
                    response.ErrorCode = "0";
                    response.ErrorMessage = rm.GetString(response.ErrorCode);
                }
            }
            return response;
        }
        #endregion
        
        #region IDisposable实现
        /// <summary>
        /// 是否已经调用了 Dispose(bool disposing)方法。
        ///     应该定义成 private 的，这样可以使基类和子类互不影响。
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// 所有回收工作都由该方法完成。
        ///     子类应重写(override)该方法。
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            // 避免重复调用 Dispose 。
            if (!disposed) return;

            // 适应多线程环境，避免产生线程错误。
            lock (this)
            {
                if (disposing)
                {
                    // ------------------------------------------------
                    // 在此处写释放托管资源的代码
                    // (1) 有 Dispose() 方法的，调用其 Dispose() 方法。
                    // (2) 没有 Dispose() 方法的，将其设为 null。
                    // 例如：
                    //     xxDataTable.Dispose();
                    //     xxDataAdapter.Dispose();
                    //     xxString = null;
                    // ------------------------------------------------
                    con.Dispose();
                    rm = null;
                }

                // ------------------------------------------------
                // 在此处写释放非托管资源
                // 例如：
                //     文件句柄等
                // ------------------------------------------------
                disposed = true;
            }
        }

        /// <summary>
        /// 该方法由程序调用，在调用该方法之后对象将被终结。
        ///     该方法定义在IDisposable接口中。
        /// </summary>
        public void Dispose()
        {
            //因为是由程序调用该方法的，因此参数为true。
            Dispose(true);
            //因为我们不希望垃圾回收器再次终结对象，因此需要从终结列表中去除该对象。
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 调用 Dispose() 方法，回收资源。
        /// </summary>
        public void Close()
        {
            Dispose();
        }
        #endregion


    }
}

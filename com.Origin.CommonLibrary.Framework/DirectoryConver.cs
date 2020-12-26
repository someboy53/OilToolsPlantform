using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.Origin.CommonLibrary.Framework
{
    public class DirectoryConver
    {
        /// <summary>
        /// 物理路径文件转换成虚拟路径文件
        /// </summary>
        /// <param name="Server">页面Server对象</param>
        /// <param name="PhysicalFile">物理路径及文件名</param>
        /// <returns>返回虚拟路径文件</returns>
        public static string PhysicalToVirtual(HttpServerUtility Server, string PhysicalFile)
        {
            string rootPath = Server.MapPath("/ ");
            string mTemp = PhysicalFile.Remove(0, rootPath.Length);
            mTemp = "/" + mTemp.Replace("\\", "/");

            return mTemp;
        }
    }
}
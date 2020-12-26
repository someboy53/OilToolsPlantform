using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.Origin.CommonLibrary.Framework
{
    public static class MathExpansion
    {
        public static double Round(double value, int decimals)
        {

            if (value > 0)
                return Convert.ToDouble(decimal.Truncate((Convert.ToDecimal(value) * (decimal)(Math.Pow(10, decimals)) + 0.5M)) / (decimal)(Math.Pow(10, decimals)));
            else
                return Convert.ToDouble(decimal.Truncate((Convert.ToDecimal(value) * (decimal)(Math.Pow(10, decimals)) - 0.5M)) / (decimal)(Math.Pow(10, decimals)));

        }
        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <param name="sumNum">总记录数</param>
        /// <param name="pageRow">每页显示条数</param>
        /// <returns>返回总页数</returns>
        public static int CaculatPage(int sumNum, int pageRow)
        {
            int sumPage = 0;
            pageRow = pageRow == 0 ? 1 : pageRow;
            sumPage = sumNum / pageRow;
            if (sumNum % pageRow != 0)
            {
                sumPage += 1;
            }
            return sumPage;
        }
    }
}
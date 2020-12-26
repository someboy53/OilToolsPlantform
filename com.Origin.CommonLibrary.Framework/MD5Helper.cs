using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace com.Origin.CommonLibrary.Framework
{
    public class MD5Helper
    {
        public static string GetMD5(string OriginStr)
        {
            byte[] result = Encoding.UTF8.GetBytes(OriginStr);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }
    }
}

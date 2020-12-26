using System;
using System.Runtime.InteropServices;


namespace com.Origin.CommonLibrary.Framework
{
    public class RequestMachine
    {
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        public RequestMachine()
        {
        }

        #region 客户端MAC地址

        public string HostMAC()
        {
            //***默认返回IP
            string reMac = System.Web.HttpContext.Current.Request.UserHostAddress;

            try
            {
                string userip = System.Web.HttpContext.Current.Request.UserHostAddress;
                string strClientIP = System.Web.HttpContext.Current.Request.UserHostAddress.ToString().Trim();
                Int32 ldest = inet_addr(strClientIP); //目的地的ip
                Int32 lhost = inet_addr("");   //本地服务器的ip
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");
                //****本机localhost访问，直接返回
                if (mac_src == "0")
                {
                    //***获取客户端机器名
                    string[] computername = System.Net.Dns.GetHostEntry(System.Web.HttpContext.Current.Request.ServerVariables["remote_addr"]).HostName.ToString().Split(new Char[] { '.' }); computername[0].ToString();

                    string clientName = "";
                    //***存在客户端机器名，获取
                    if (computername.Length > 0)
                    {
                        clientName = computername[0].ToString();
                    }
                    else
                    {
                        clientName = System.Web.HttpContext.Current.Request.UserHostName.ToString();
                    }

                    reMac = clientName;
                }
                else
                {//***非localhost访问，获取客户端IP+MAC地址
                    while (mac_src.Length < 12)
                    {
                        mac_src = mac_src.Insert(0, "0");
                    }

                    string mac_dest = "";

                    for (int i = 0; i < 11; i++)
                    {
                        if (0 == (i % 2))
                        {
                            if (i == 10)
                            {
                                mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                            }
                            else
                            {
                                mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                            }
                        }
                    }

                    reMac = mac_dest;
                }
            }
            catch 
            {//****出错，直接返回客户端IP
                reMac = System.Web.HttpContext.Current.Request.UserHostName;
            }

            return reMac;
        }

        #endregion

        #region 客户端IP地址

        public string HostIP()
        {
            //***默认返回IP
            string reMac = System.Web.HttpContext.Current.Request.UserHostAddress;

            try
            {
                string userip = System.Web.HttpContext.Current.Request.UserHostAddress;
                string strClientIP = System.Web.HttpContext.Current.Request.UserHostAddress.ToString().Trim();
                Int32 ldest = inet_addr(strClientIP); //目的地的ip
                Int32 lhost = inet_addr("");   //本地服务器的ip
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");
                //****本机localhost访问，直接返回IP
                if (mac_src == "0")
                {
                    //***获取客户端机器IP
                    reMac = System.Web.HttpContext.Current.Request.UserHostAddress;
                }
                else
                {//***非localhost访问，获取客户端IP
                    reMac = userip;
                }
            }
            catch 
            {//****出错，直接返回客户端IP
                reMac = System.Web.HttpContext.Current.Request.UserHostAddress;
            }

            return reMac;
        }

        #endregion

    }
}
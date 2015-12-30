using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Web;

namespace System.Web
{
    /// <summary>
    /// 获取请求的 客户端信息
    /// </summary>
    public class ReqBrowser
    {
        /// <summary>
        /// 当前请求上下文
        /// </summary>
        public static HttpRequest req
        {
            get { return HttpContext.Current.Request; }
        }
        /// <summary>
        /// 获取 客户端请求的IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            if (req.ServerVariables["HTTP_VIA"] != null)
                return req.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
            else
                return req.ServerVariables["REMOTE_ADDR"];
        }
        /// <summary>
        /// 获取 请求浏览器的代理字符串
        /// </summary>
        /// <returns></returns>
        public static string GetUserAgent()
        {
            return req.Headers["User-Agent"];
        }
        /// <summary>
        /// 获取浏览器名称
        /// </summary>
        /// <returns></returns>
        public static string GetBrowser()
        {
            return req.Browser.Browser;
        }
        /// <summary>
        /// 获取浏览器的版本号
        /// </summary>
        /// <returns></returns>
        public static string GetBrowserVersion()
        {
            return req.Browser.Version;
        }
        /// <summary>
        /// 获取客户端操作系统
        /// </summary>
        /// <returns></returns>
        public static string GetPlatform()
        {
            return req.Browser.Platform;
        }


        /// <summary>
        /// 获取客户端信息
        /// </summary>
        /// <returns></returns>
        public static BrowserInfo GetBrowseInfo()
        {
            BrowserInfo info = new BrowserInfo();
            info.IP = GetIP();
            info.UserAgent = GetUserAgent();
            info.Browser = GetBrowser();
            info.BrowseVersion = GetBrowserVersion();
            info.Platform = GetPlatform();
            return info;
        }
    }
    /// <summary>
    /// 客户端信息
    /// </summary>
    public class BrowserInfo
    {
        /// <summary>
        /// 请求IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 请求浏览器代理
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// 请求浏览器名称
        /// </summary>
        public string Browser { get; set; }
        /// <summary>
        /// 请求浏览器版本
        /// </summary>
        public string BrowseVersion { get; set; }
        /// <summary>
        /// 请求操作系统平台
        /// </summary>
        public string Platform { get; set; }

        /// <summary>
        /// 判断请求是否来自移动终端
        /// </summary>
        public bool IsFromMobile
        {
            get
            {
                string userAgent = this.UserAgent;
                if (string.IsNullOrEmpty(userAgent))
                    return false;

                return CheckMobile(userAgent);
            }
        }

        /// <summary>
        /// 根据客户端代理，判断请求是否来自移动终端
        /// </summary>
        /// <param name="userAgent">浏览器客户端代理</param>
        /// <returns></returns>
        public static bool CheckMobile(string userAgent)
        {
            if (userAgent.IndexOf("Noki") > -1 || // Nokia phones and emulators     
                userAgent.IndexOf("Android") > -1 ||   //Android 手机
                  userAgent.IndexOf("iPhone") > -1 ||  //iPhone  手机
                userAgent.IndexOf("Eric") > -1 || // Ericsson WAP phones and emulators     
                userAgent.IndexOf("WapI") > -1 || // Ericsson WapIDE 2.0     
                userAgent.IndexOf("MC21") > -1 || // Ericsson MC218     
                userAgent.IndexOf("AUR") > -1 || // Ericsson R320     
                userAgent.IndexOf("R380") > -1 || // Ericsson R380     
                userAgent.IndexOf("UP.B") > -1 || // UP.Browser     
                userAgent.IndexOf("WinW") > -1 || // WinWAP browser     
                userAgent.IndexOf("UPG1") > -1 || // UP.SDK 4.0     
                userAgent.IndexOf("upsi") > -1 || //another kind of UP.Browser     
                userAgent.IndexOf("QWAP") > -1 || // unknown QWAPPER browser     
                userAgent.IndexOf("Jigs") > -1 || // unknown JigSaw browser     
                userAgent.IndexOf("Java") > -1 || // unknown Java based browser     
                userAgent.IndexOf("Alca") > -1 || // unknown Alcatel-BE3 browser (UP based)    


                userAgent.IndexOf("MITS") > -1 || // unknown Mitsubishi browser     
                userAgent.IndexOf("MOT-") > -1 || // unknown browser (UP based)     
                userAgent.IndexOf("My S") > -1 ||//  unknown Ericsson devkit browser      
                userAgent.IndexOf("WAPJ") > -1 ||//Virtual WAPJAG www.wapjag.de     
                userAgent.IndexOf("fetc") > -1 ||//fetchpage.cgi Perl script from www.wapcab.de 


                userAgent.IndexOf("ALAV") > -1 || //yet another unknown UP based browser     
                userAgent.IndexOf("Wapa") > -1 || //another unknown browser (Web based "Wapalyzer")    
                userAgent.IndexOf("UCWEB") > -1 || //another unknown browser (Web based "Wapalyzer")    
                userAgent.IndexOf("BlackBerry") > -1 || //another unknown browser (Web based "Wapalyzer")                     
                userAgent.IndexOf("J2ME") > -1 || //another unknown browser (Web based "Wapalyzer")              
                userAgent.IndexOf("Oper") > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

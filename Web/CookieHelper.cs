using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace System.Web
{
    /// <summary>
    /// Asp.net Cookie访问帮助类
    /// 操作cookie，制定
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 当前请求上线文
        /// </summary>
        private static HttpRequest Request
        {
            get { return HttpContext.Current.Request; }
        }
        /// <summary>
        /// 判断当前是否是本地
        /// </summary>
        public static bool IsLocal
        {
            get { return Request.Url.Host.Contains("localhost"); }
        }
        /// <summary>
        /// 获取WebConfig中的二级域名
        /// </summary>
        public static string DomainName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings.Get("DomainName");
            }
        }


        #region 获取cookie
        /// <summary>  
        /// 获取指定Cookie值  
        /// </summary>  
        /// <param name="name">name</param>  
        /// <returns></returns>  
        public static string GetString(string name)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[name];
            string str = string.Empty;
            if (cookie != null)
            {
                str = cookie.Value;
            }
            return str;
        }
        /// <summary>
        /// 获取int? 类型
        /// </summary>
        /// <param name="name">键</param>
        /// <returns></returns>
        public static int? GetInt(string name)
        {
            string value = GetString(name);
            int result;
            if (int.TryParse(value, out result))
                return result;
            return null;
        }
        #endregion

        #region 保存Cookie
        /// <summary>  
        /// 添加一个Cookie（24小时过期）  
        /// </summary>  
        /// <param name="name"></param>  
        /// <param name="value"></param>  
        public static void Set(string name, string value)
        {
            Set(name, value, DateTime.Now.AddDays(1.0));
        }
        /// <summary>  
        /// 添加一个Cookie  
        /// </summary>  
        /// <param name="name">cookie名</param>  
        /// <param name="value">cookie值</param>  
        /// <param name="expires">过期时间 DateTime</param>  
        public static void Set(string name, string value, DateTime expires)
        {
            //默认为 全局域的cookie
            HttpCookie cookie = new HttpCookie(name)
            {
                Value = value,
                Expires = expires,
                Path = "/"
            };
            if (IsLocal==false)
            {
                cookie.Domain = DomainName;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// 保存cookie 到客户端
        /// 只允许http访问，session模式
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set_Http(string key, string value)
        {
            HttpCookie cookie = new HttpCookie(key, value);
            cookie.Path = "/";
            cookie.HttpOnly = true;
            if (IsLocal==false)
            {
                cookie.Domain = DomainName;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        #endregion

        #region 删除cookie
        /// <summary>  
        /// 清除指定Cookie  
        /// </summary>  
        /// <param name="name">指定键</param>  
        public static void Remove(string name)
        {
            Set(name, "", DateTime.Now.AddDays(-1));
        }
        /// <summary>
        /// 删除指定的cookie
        /// </summary>
        /// <param name="names">指定的键列表</param>
        public static void RemoveRange(params string[] names)
        {
            foreach (var item in names)
            {
                Remove(item);
            }
        }
        #endregion
    }
}

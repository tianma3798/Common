using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace System.Web
{
    /// <summary>
    /// 常用请求参数获取帮助类 3.0
    /// 支持MVC路由参数过滤s
    /// </summary>
    public partial class RequestHelper
    {
        /// <summary>
        /// 请求上下文
        /// </summary>
        public HttpRequest req;
        /// <summary>
        /// 相应上下文
        /// </summary>
        public HttpResponse resp;
        /// <summary>
        /// 构造初始化
        /// </summary>
        public RequestHelper()
        {
            req = HttpContext.Current.Request;
            resp = HttpContext.Current.Response;
        }

        #region 获取String类型的值
        /// <summary>
        /// 获取QueryString参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>字符串，结果值</returns>
        public string GetStringQuery(string key)
        {
            string val = req.QueryString.Get(key);
            if (string.IsNullOrEmpty(val))
                return "";
            return val;
        }
        /// <summary>
        /// 获取Form参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>字符串，结果值</returns>
        public string GetStringForm(string key)
        {
            string val = req.Form.Get(key);
            if (string.IsNullOrEmpty(val))
                return "";
            return val;
        }
        /// <summary>
        /// 获取RouteData参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public string GetStringRoute(string key)
        {
            string val = GetRouteValue(key) as string;
            if (string.IsNullOrEmpty(val))
                return "";
            return val;
        }

        /// <summary>
        /// 获取请求参数，先检测RouteData，再检索Form，再检索QueryString
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>字符串结果</returns>
        public string GetString(string key)
        {
            string result = GetStringRoute(key);
            if (result != "")
                return result;
            result = GetStringForm(key);
            if (result != "")
                return result;
            result = GetStringQuery(key);
            return result;
        }
        /// <summary>
        /// 获取请求参数，先检索Form，在检索QueryString
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defVal">默认值</param>
        /// <returns>字符串结果</returns>
        public string GetString(string key, string defVal)
        {
            string result = GetString(key);
            if (result == "")
                return defVal;
            return result;
        }
        #endregion


        #region 获取Int类型的值
        /// <summary>
        /// 获取QueryString参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>可空int类型</returns>
        public int? GetIntQuery(string key)
        {
            int result = 0;
            if (int.TryParse(GetStringQuery(key), out result))
            {
                return result;
            }
            return null;
        }
        /// <summary>
        /// 获取Form参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>可空int类型</returns>
        public int? GetIntForm(string key)
        {
            int result = 0;
            if (int.TryParse(GetStringForm(key), out result))
            {
                return result;
            }
            return null;
        }
        /// <summary>
        /// 获取RouteData参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public int? GetIntRoute(string key)
        {
            string val = GetStringRoute(key);
            int result;
            if (int.TryParse(val, out result))
            {
                return result;
            }
            return null;
        }


        /// <summary>
        /// 获取请求参数，先检索RouteData,再检索Form，再检索QueryString
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>可空int类型</returns>
        public int? GetInt(string key)
        {
            int? result = GetIntRoute(key);
            if (result != null)
                return result;
            result = GetIntForm(key);
            if (result != null)
                return result.Value;
            result = GetIntQuery(key);
            return result;
        }
        /// <summary>
        /// 获取请求参数，先检索Form，在检索QueryString
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defVal">默认值</param>
        /// <returns>int类型</returns>
        public int GetInt(string key, int defVal)
        {
            int? result = GetInt(key);
            if (result == null)
                return defVal;
            return result.Value;
        }
        #endregion


        #region 获取DateTime类型
        /// <summary>
        /// 获取QueryString参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>可空DateTime类型</returns>
        public DateTime? GetDateTimeQuery(string key)
        {
            DateTime date;
            if (DateTime.TryParse(GetStringQuery(key), out date))
            {
                return date;
            }
            return null;
        }
        /// <summary>
        /// 获取Form参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>可空DateTime类型</returns>
        public DateTime? GetDateTimeForm(string key)
        {
            DateTime date;
            if (DateTime.TryParse(GetStringForm(key), out date))
            {
                return date;
            }
            return null;
        }
        /// <summary>
        /// 获取RouteData数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DateTime? GetDateTimeRoute(string key)
        {
            string val = GetStringRoute(key);
            DateTime result;
            if (DateTime.TryParse(val, out result))
            {
                return result;
            }
            return null;
        }

        /// <summary>
        /// 获取请求参数，先检索Form，在检索QueryString
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>可空DateTime类型</returns>
        public DateTime? GetDateTime(string key)
        {
            DateTime? date = GetDateTimeRoute(key);
            if (date != null)
                return date;
            date = GetDateTimeForm(key);
            if (date != null)
                return date.Value;
            date = GetDateTimeQuery(key);
            return date;
        }
        /// <summary>
        /// 获取请求参数，先检索Form，在检索QueryString
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defVal">默认值</param>
        /// <returns>DateTime类型</returns>
        public DateTime GetDateTime(string key, DateTime defVal)
        {
            DateTime? date = GetDateTime(key);
            if (date == null)
                return defVal;
            return date.Value;
        }
        #endregion


        #region 获取Decimal类型的值
        /// <summary>
        /// 获取QueryString参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>可空int类型</returns>
        public decimal? GetDecimalQuery(string key)
        {
            decimal result = 0;
            if (decimal.TryParse(GetStringQuery(key), out result))
            {
                return result;
            }
            return null;
        }
        /// <summary>
        /// 获取Form参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>可空int类型</returns>
        public decimal? GetDecimalForm(string key)
        {
            decimal result = 0;
            if (decimal.TryParse(GetStringForm(key), out result))
            {
                return result;
            }
            return null;
        }
        /// <summary>
        /// 获取RouteData参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public decimal? GetDecimalRoute(string key)
        {
            string val = GetStringRoute(key);
            decimal result;
            if (decimal.TryParse(val, out result))
            {
                return result;
            }
            return null;
        }


        /// <summary>
        /// 获取请求参数，先检索Decimal,先检索Form，在检索QueryString
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>可空int类型</returns>
        public decimal? GetDecimal(string key)
        {
            decimal? result = GetDecimalRoute(key);
            if (result != null)
                return result;
            result = GetDecimalForm(key);
            if (result != null)
                return result.Value;
            result = GetDecimalQuery(key);
            return result;
        }
        /// <summary>
        /// 获取请求参数，先检索Form，在检索QueryString
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defVal">默认值</param>
        /// <returns>int类型</returns>
        public decimal GetDecimal(string key, decimal defVal)
        {
            decimal? result = GetDecimal(key);
            if (result == null)
                return defVal;
            return result.Value;
        }
        #endregion


        #region 获取或这是路由参数
        /// <summary>
        /// 获取路由参数
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>object数据</returns>
        public object GetRouteValue(string key)
        {
            return req.RequestContext.RouteData.Values[key];
        }
        /// <summary>
        /// 添加路由参数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public void SetRouteValue(string key, object value)
        {
            req.RequestContext.RouteData.Values.Add(key,value);
        }
        #endregion

        #region 其他公共方法
        /// <summary>
        /// 输入字符串
        /// </summary>
        /// <param name="str"></param>
        public void ResponseText(string str)
        {
            resp.Write(str);
        }
        ///// <summary>
        ///// SHA1 加密方法
        ///// </summary>
        ///// <param name="source"></param>
        ///// <returns></returns>
        //public string Sha1(string source)
        //{
        //    return FormsAuthentication.HashPasswordForStoringInConfigFile(source, "SHA1");
        //}
        #endregion
    }
}
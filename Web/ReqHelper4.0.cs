using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;

namespace System.Web
{
    /// <summary>
    /// 访问请求上下文参数 静态类
    /// </summary>
    public static class ReqHelper
    {
        /// <summary>
        /// 请求上下文
        /// </summary>
        public static HttpRequest req
        {
            get { return HttpContext.Current.Request; }
        }
        /// <summary>
        /// 响应上下文
        /// </summary>
        public static HttpResponse resp
        {
            get { return HttpContext.Current.Response; }
        }

        #region 获取String类型的值
        /// <summary>
        /// 获取QueryString参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>字符串，结果值</returns>
        public static string GetStringQuery(string key)
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
        public static string GetStringForm(string key)
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
        public static string GetStringRoute(string key)
        {
            string val = GetRouteValue(key) as string;
            if (string.IsNullOrEmpty(val))
                return "";
            return val;
        }
        /// <summary>
        /// 获取RouteData参数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defvalue">默认值</param>
        /// <returns>值</returns>
        public static string GetStringRoute(string key, string defvalue)
        {
            string val = GetRouteValue(key) as string;
            if (string.IsNullOrEmpty(val))
                return defvalue;
            return val;
        }

        /// <summary>
        /// 获取请求参数，先检测RouteData，再检索Form，再检索QueryString
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>字符串结果</returns>
        public static string GetString(string key)
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
        public static string GetString(string key, string defVal)
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
        public static int? GetIntQuery(string key)
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
        public static int? GetIntForm(string key)
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
        public static int? GetIntRoute(string key)
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
        public static int? GetInt(string key)
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
        public static int GetInt(string key, int defVal)
        {
            int? result = GetInt(key);
            if (result == null)
                return defVal;
            return result.Value;
        }
        #endregion



        #region 获取bollean类型
        /// <summary>
        /// 获取QueryString参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>可空bool类型</returns>
        public static bool? GetBoolQuery(string key)
        {
            bool result = true;
            if (bool.TryParse(GetStringQuery(key), out result))
            {
                return result;
            }
            return null;
        }
        /// <summary>
        /// 获取Form参数
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>可空bool类型</returns>
        public static bool? GetBoolForm(string key)
        {
            bool result = true;
            if (bool.TryParse(GetStringForm(key), out result))
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
        public static bool? GetBoolRotate(string key)
        {
            bool result = true;
            if (bool.TryParse(GetStringRoute(key), out result))
            {
                return result;
            }
            return null;
        }
        /// <summary>
        /// 获取请求参数，先检索RouteData,再检索Form，再检索QueryString
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>可空bool类型</returns>
        public static bool? GetBool(string key)
        {
            bool result = true;
            if (bool.TryParse(GetString(key), out result))
            {
                return result;
            }
            return null;
        }
        /// <summary>
        /// 获取请求参数，先检索Form，在检索QueryString
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="defVal">默认值</param>
        /// <returns>bool类型</returns>
        public static bool GetBool(string key, bool defVal)
        {
            bool? result = GetBool(key);
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
        public static DateTime? GetDateTimeQuery(string key)
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
        public static DateTime? GetDateTimeForm(string key)
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
        public static DateTime? GetDateTimeRoute(string key)
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
        public static DateTime? GetDateTime(string key)
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
        public static DateTime GetDateTime(string key, DateTime defVal)
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
        public static decimal? GetDecimalQuery(string key)
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
        public static decimal? GetDecimalForm(string key)
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
        public static decimal? GetDecimalRoute(string key)
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
        public static decimal? GetDecimal(string key)
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
        public static decimal GetDecimal(string key, decimal defVal)
        {
            decimal? result = GetDecimal(key);
            if (result == null)
                return defVal;
            return result.Value;
        }
        #endregion


        #region 获取路由参数
        /// <summary>
        /// 获取路由参数
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns>object数据</returns>
        public static object GetRouteValue(string key)
        {
            return req.RequestContext.RouteData.Values[key];
        }
        /// <summary>
        /// 传递到路由处理程序但未使用的自定义值的集合
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static object GetTokenValue(string key)
        {
            return req.RequestContext.RouteData.DataTokens[key];
        }
        /// <summary>
        /// 传递到路由处理程序但未使用的自定义值的集合
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static string GetStringToken(string key)
        {
            object obj = GetTokenValue(key);
            if (obj == null)
                return "";
            return obj.ToString();
        }

        ///// <summary>
        ///// 获取当前请求的action 名称（小写名称）
        ///// </summary>
        ///// <returns></returns>
        //public static string GetAction()
        //{
        //    return req.RequestContext.RouteData.DataTokens["action"].ToString();
        //}
        #endregion


        #region 获取string 列表 并转换成 List<int>
        /// <summary>
        /// 获取 string 列表,来自路由参数
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static List<int> GetListRoute(string key)
        {
            List<int> list = new List<int>();
            string result = GetStringRoute(key);
            if (result != ""&&result!="0")
            {
                list = result.Split(',').Select(q=>Convert.ToInt32(q)).ToList();
            }
            return list;
        }
        /// <summary>
        /// 获取 string 列表，来自Post参数
        /// </summary>
        /// <returns></returns>
        public static List<int> GetListForm(string key)
        {
            List<int> list = new List<int>();
            string result = GetStringForm(key);
            if (result != "" && result != "0")
            {
                list = result.Split(',').Select(q => Convert.ToInt32(q)).ToList();
            }
            return list;
        }
        #endregion


        #region 其他公共方法
        /// <summary>
        /// 输入字符串
        /// </summary>
        /// <param name="str"></param>
        public static void ResponseText(string str)
        {
            resp.Write(str);
        }
        /// <summary>
        /// SHA1 加密方法
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Sha1(string source)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(source, "SHA1");
        }

        /// <summary>
        /// 根据 时间段类型，获取 判断条件
        /// </summary>
        /// <param name="timeSpan">时间段类型</param>
        /// <returns></returns>
        public static string GetMySqlByTimeSpan(int timeSpan)
        {
            if (timeSpan == 1)//本天
            {
                return " and date(AddTime)=curdate()";
            }
            else if (timeSpan == 2)//本周
            {
                return " and year(AddTime)=year(now()) and week(AddTime)=week(now())";
            }
            else if (timeSpan == 3)//本月
            {
                return " and year(AddTime)=year(now()) and month(AddTime)=month(now())";
            }
            else if (timeSpan == 4)//本年
            {
                return " and year(AddTime)=year(now())";
            }
            return "";
        }
        #endregion


        #region 请求参数处理
        /// <summary>
        /// 依次获取当前请求的 query 和form  值的集合
        /// </summary>
        /// <returns></returns>
        public static NameValueCollection GetParams()
        {
            NameValueCollection coll = new NameValueCollection();
            //1.先处理 querystring
            foreach (string item in req.QueryString.Keys)
            {
                if (coll.Get(item) == null) //如果不存在添加
                {
                    coll.Add(item, req.QueryString[item]);
                }
                else //如果存在 修改
                {
                    coll.Set(item, req.QueryString[item]);
                }
            }
            //2.再处理表单
            foreach (string item in req.Form.Keys)
            {
                if (coll.Get(item) == null) //如果不存在添加
                {
                    coll.Add(item, req.QueryString[item]);
                }
                else //如果存在 修改
                {
                    coll.Set(item, req.QueryString[item]);
                }
            }
            return coll;
        }

        /// <summary>
        /// 获取汇总模块的链接处理
        /// </summary>
        /// <returns></returns>
        public static string GetUrl_Params(string key = null, string value = null,bool isPager=false)
        {
            NameValueCollection col = req.QueryString;

            List<string> allKeys = new List<string>();
            foreach (string item in col.Keys)
            {
                if (string.IsNullOrEmpty(item) == false
                    && item != key
                    && allKeys.Any(q => q == item) == false)
                    allKeys.Add(item);
            }
            if (key != null)
                allKeys.Add(key);
            allKeys = allKeys.OrderBy(q => q).ToList();

            //生成所有参数的链接
            StringBuilder builder = new StringBuilder();
            foreach (string item in allKeys)
            {
                string currentValue = "";
                //获取参数结果
                if (item == key)
                {
                    currentValue = value;
                }
                else
                {
                    currentValue = req.QueryString[item];
                }
                //如果只不存在，不添加该项
                if (string.IsNullOrEmpty(currentValue))
                    continue;
                //如果不是分页参数，去除分页条件
                if (isPager == false)
                {
                    if (item.Equals("pageindex", StringComparison.CurrentCultureIgnoreCase))
                    {
                        continue;
                    }
                }
                builder.Append("&").Append(item);
                builder.Append("=").Append(currentValue);
            }
            string result = builder.ToString().ToLower();
            if (result.Length > 2)
            {
                result = result.Substring(1);
                result = "?" + result;
            }
            return result;
        }


        /// <summary>
        /// 获取汇总模块的链接处理
        /// </summary>
        /// <returns></returns>
        public static string GetUrl_Collect(string key = null, object value = null, bool isPager = false)
        {
            return req.Url.LocalPath + GetUrl_Params(key, value == null ? null : value.ToString(), isPager);
        }
        #endregion
    }
}
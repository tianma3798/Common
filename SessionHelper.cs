using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace System.Web
{
    /// <summary>
    /// 站点下 Session 访问帮助类
    /// </summary>
    public class SessionHelper
    {
        private static HttpSessionState _Session
        {
            get { return HttpContext.Current.Session; }
        }

        #region 获取值
        /// <summary>
        /// 获取 字符串值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            object obj = _Session[key];
            if (obj == null)
                return "";
            return obj.ToString();
        }
        /// <summary>
        /// 获取int？ 类型
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int? GetInt(string key)
        {
            object obj = _Session[key];
            if (obj == null)
                return null;
            return Convert.ToInt32(obj);
        }
        #endregion

        #region 保存值
        /// <summary>
        /// 设置 字符串值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeout">过期时间（分钟）</param>

        public static void SetValue(string key, string value, int timeout)
        {
            _Session[key] = value;
            _Session.Timeout = timeout;
        }
        /// <summary>
        /// 设置 字符串值(使用默认时间 20分钟)
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetValue(string key, string value)
        {
            _Session[key] = value;
        }
        #endregion

        #region 移除值
        /// <summary>
        /// 移除Session 中的值
        /// </summary>
        /// <param name="key">键</param>
        public static void Remove(string key)
        {
            _Session.Remove(key);
        }
        /// <summary>
        /// 移除Session 中的值
        /// </summary>
        /// <param name="keys">多个键</param>
        public static void RemoveRange(params string[] keys)
        {
            foreach (var item in keys)
            {
                Remove(item);
            }
        }
        #endregion

        #region 短信验证码
        /// <summary>
        /// 保存 短信验证码
        /// </summary>
        /// <param name="value"></param>
        public static void SetValue_SMS(string value)
        {
            SetValue("SMS_Code", value);
        }
        /// <summary>
        /// 获取短信  验证码
        /// </summary>
        /// <returns></returns>
        public static string GetValue_SMS()
        {
            return GetString("SMS_Code");
        }
        #endregion
    }
}

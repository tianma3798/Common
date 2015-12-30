using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 常用时间相关方法操作
    /// </summary>
    public class DateTimeHelper
    {
        #region 数字相关
        /// <summary>
        /// 获取指定位数的数字 字符
        /// </summary>
        /// <param name="count">数字的个数</param>
        /// <returns></returns>
        public static string GetNumber_Ran(int count)
        {
            Random _random = new Random();
            StringBuilder builder = new StringBuilder(count);
            for (int i = 0; i < count; i++)
            {
                builder.Append(_random.Next(0, 10));
            }
            return builder.ToString();
        }
        #endregion

        #region 时间相关
        /// <summary>
        /// 根据当前时间生成编号
        /// 年月日 、小时、分钟、秒
        /// </summary>
        /// <returns></returns>
        public static string GetTimeCode()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmss");
        }
        /// <summary>
        /// 获取 时间编号+4位随机数
        /// </summary>
        /// <returns></returns>
        public static string GetTimeCode_Ran()
        {
            return GetTimeCode() + GetNumber_Ran(4);
        }

        /// <summary>
        /// 根据当前时间问候
        /// </summary>
        /// <returns></returns>
        public static string SayHello_Time()
        {
            int hour = DateTime.Now.Hour;
            if (hour > 0 && hour <= 12)
            {
                return "早上好";
            }
            else if (hour > 12 && hour <= 18)
            {
                return "下午好";
            }
            return "晚上好";
        }
        #endregion

        #region 周一相关
        /// <summary>
        /// 获取当前日期对应的周一
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentMonday()
        {
            return GetMonday(DateTime.Now);
        }
        /// <summary>
        /// 获取指定日期的当前周的周一
        /// </summary>
        /// <param name="end">指定的日期</param>
        /// <returns></returns>
        public static DateTime GetMonday(DateTime end)
        {
            int spanDiay = end.DayOfWeek.GetHashCode() - 1;
            spanDiay = spanDiay < 0 ? 6 : spanDiay;
            return end.AddDays(-1 * spanDiay);
        }
        #endregion


    }
}

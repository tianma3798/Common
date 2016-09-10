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
        #region 数字随机数、随机字符等
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
        /// <summary>
        /// 获取指定个数的随机 字符（字母+数字）
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <param name="showAll">使用全部字符</param>
        /// <returns></returns>
        public static string GetCode_Ran(int count, bool showAll = false)
        {
            StringBuilder builder = new StringBuilder(count);
            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = null;
            if (showAll)
            {
                //显示全字符
                character = new char[]{'0','1','2', '3', '4', '5', '6','7', '8', '9',
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H','I', 'J', 'K', 'L', 'M', 'N','O', 'P','Q', 'R', 'S','T', 'U','V', 'W', 'X', 'Y','Z' };
            }
            else
            {
                //过滤掉了部分容易混淆的字符
                character = new char[] { '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            }
            if (count > character.Length)
                throw new Exception("GetCode_Ran 的 count值不能大于源字符的总长度");
            Random rnd = new Random();
            //生成验证码字符串 
            for (int i = 0; i < count; i++)
            {
                builder.Append(character[rnd.Next(character.Length)]);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// 常用正则表达式整理
    /// </summary>
    public class RegexExpression
    {
        //英文字母
        public static readonly string Character = @"^[A-Za-z]+$";
        //数字
        public static readonly string Number = @"^(-?\d+)(\.\d+)?$";
        //正整数
        public static readonly string PositiveInteger = "^([+]?[1-9])[0-9]*$";
        //Ipv4
        public static readonly string Ipv4 = @"^((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))$";
        //手机号
        public static readonly string Phone = @"^1[3-8]+\d{9}$";
        //邮箱
        public static readonly string EMail = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        //URL 地址
        // public static readonly string URL = @"(https?|ftp|file|ws)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
    }
    /// <summary>
    /// 常用正则表达式及模板使用
    /// </summary>
    public class RegexHelper
    {

        #region 数字类型
        /// <summary>
        /// 1.验证一个字符串是否是整数
        /// </summary>
        /// <param name="str">指定字符串</param>
        /// <returns></returns>
        public static bool IsPositiveInteger(string str)
        {
            return IsMatch(RegexExpression.PositiveInteger, str);
        }
        /// <summary>
        /// 验证输入内容是否是数字
        /// </summary>
        /// <param name="str">指定字符串</param>
        /// <returns></returns>
        public static bool IsNumber(string str)
        {
            return IsMatch(RegexExpression.Number, str);
        }
        #endregion

        #region 已定义模板匹配
        /// <summary>
        /// 2.验证一个字符串是否是Ipv4 地址
        /// </summary>
        /// <param name="str">指定字符串</param>
        /// <returns></returns>
        public static bool IsIpv4(string str)
        {
            return IsMatch(RegexExpression.Ipv4, str);
        }
        /// <summary>
        /// 4.验证字符串是否是手机号
        /// </summary>
        /// <param name="str">指定字符串</param>
        /// <returns></returns>
        public static bool IsPhone(string str)
        {
            return IsMatch(RegexExpression.Phone, str);
        }
        /// <summary>
        /// 5.验证字符串是否是邮箱
        /// </summary>
        /// <param name="str">指定字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string str)
        {
            return IsMatch(RegexExpression.EMail, str);
        }
        #endregion



        #region 常用判断
        /// <summary>
        /// 判断富文本是否为空
        /// 是否只有任意的空白符
        ///------ 包括空格，制表符(Tab)，换行符，中文全角空格等
        /// </summary>
        /// <param name="str">判断字符串</param>
        /// <returns></returns>
        public static bool IsNull_Rich(string str)
        {
            if (string.IsNullOrEmpty(str))
                return true;
            Regex reg = new Regex(@"\S", RegexOptions.Multiline);
            return !reg.IsMatch(str);
        }
        /// <summary>
        /// 判断是否是英文字符
        /// </summary>
        public static bool IsCharacter(string str, bool ignoreCase = false)
        {
            if (ignoreCase)
                return IsMatch(RegexExpression.Character, str);
            return IsMatchIgnoreCase(RegexExpression.Character, str);
        }
        /// <summary>
        /// 判断一个字符串，是否匹配指定的表达式(区分大小写的情况下)
        /// </summary>
        /// <param name="expression">正则表达式</param>
        /// <param name="str">要匹配的字符串</param>
        /// <returns></returns>
        public static bool IsMatch(string expression, string str)
        {
            Regex reg = new Regex(expression);
            if (string.IsNullOrEmpty(str))
                return false;
            return reg.IsMatch(str);
        }
        /// <summary>
        /// 判断一个字符串，是否匹配指定的表达式(不区分大小写的情况下)
        /// </summary>
        /// <param name="expression">正则表达式</param>
        /// <param name="str">要匹配的字符串</param>
        /// <returns></returns>
        public static bool IsMatchIgnoreCase(string expression, string str)
        {
            Regex reg = new Regex(expression, RegexOptions.IgnoreCase);
            return reg.IsMatch(str);
        }
        #endregion



        #region Url操作
        /// <summary>
        /// 验证是否是URL链接
        /// </summary>
        /// <param name="str">指定字符串</param>
        /// <returns></returns>
        public static bool IsURL(string str)
        {
            string pattern = @"^(https?|ftp|file|ws)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
            return IsMatch(pattern, str);
        }
        /// <summary>
        /// 获取字符串中所有的链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<string> MatchsURL(string input)
        {
            string pattern = @"(https?|ftp|file|ws)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            return Matchs(input, pattern);
        }
        #endregion


        #region 域名操作
        /// <summary>
        /// 验证字符串是否是域名
        /// </summary>
        /// <param name="str">指定字符串</param>
        /// <returns></returns>
        public static bool IsDomain(string str)
        {
            string pattern = @"^[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+$";
            return IsMatch(pattern, str);
        }
        /// <summary>
        /// 匹配获取字符串中所有的域名
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<string> MatchsDomain(string input)
        {
            string pattern = @"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+";
            return Matchs(input, pattern);
        }
        #endregion

        #region 常用替换
        /// <summary>
        /// 字符串的替换
        /// </summary>
        public static string Replace(string input, string expression, string str)
        {
            return Regex.Replace(input, expression, str, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }
        /// <summary>
        /// 替换掉空行
        /// </summary>
        /// <param name="input">源字符串</param>
        /// <returns></returns>
        public static string RemoveEmptyLine(string input)
        {
            //return Replace(input, @"\r\n", "");

            return Regex.Replace(input, @"\r\n", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }
        #endregion

        /// <summary>
        /// 匹配结果  返回匹配结果的数组
        /// </summary>
        /// <param name="input"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static List<string> Matchs(string input, string expression)
        {
            List<string> list = new List<string>();
            MatchCollection collection = Regex.Matches(input, expression, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            foreach (Match item in collection)
            {
                if (item.Success)
                {
                    list.Add(item.Value);
                }
            }
            return list;
        }
    }
}
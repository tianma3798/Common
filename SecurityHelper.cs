using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using System.Web.Security;
using System.Web;

namespace Common
{
    /// <summary>
    /// 数据加密、解密处理方法
    /// </summary>
    public class SecurityHelper
    {
        #region HTML 编码/解码 HttpUtility.HtmlEncode/HtmlDecode
        /// <summary>
        /// HTML编码处理
        /// </summary>
        /// <param name="source">源代码</param>
        /// <returns></returns>
        public static string HtmlEncode(string source)
        {
            return HttpUtility.HtmlEncode(source);
        }
        /// <summary>
        /// HTML 解码处理
        /// </summary>
        /// <param name="source">源代码</param>
        /// <returns></returns>
        public static string HtmlDecode(string source)
        {
            return HttpUtility.HtmlDecode(source);
        }
        #endregion

        #region  URL编码/解码  HttpUtility.UrlEncode/UrlDecode  
        /// <summary>
        /// UrlEncode
        /// utf-8格式加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string UrlEncode(string source)
        {
            return HttpUtility.UrlEncode(source);
        }
        /// <summary>
        /// UrlDecode
        /// utf-8格式解密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns></returns>
        public static string UrlDecode(string source)
        {
            return HttpUtility.UrlDecode(source);
        }
        #endregion

        #region md5
        /// <summary>
        /// MD5加密字符串（32位大写）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            string result = BitConverter.ToString(md5.ComputeHash(bytes));
            return result.Replace("-", "");
        }
        /// <summary>
        /// MD5加密字符串（16位大写）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string MD5_16(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            string result = BitConverter.ToString(md5.ComputeHash(bytes), 4, 8);
            return result.Replace("-", "");
        }
        #endregion

        #region sha1 
        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <returns>返回40位UTF8 大写</returns>
        public static string SHA1(string content)
        {
            return SHA1(content, Encoding.UTF8);
        }
        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <param name="encode">指定加密编码</param>
        /// <returns>返回40位大写字符串</returns>
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }
        #endregion

        #region DES
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text">加密文本</param> 
        /// <param name="sKey">加密密匙</param> 
        /// <returns></returns> 
        public static string DESEncrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text">解密文本</param> 
        /// <param name="sKey">解密密匙</param> 
        /// <returns></returns> 
        public static string DESDecrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        #endregion

        #region 16进制字符串转换
        ///// <summary>
        ///// 将普通的字符串转换成16进制的字符串
        ///// </summary>
        ///// <param name="s"></param>
        ///// <returns></returns>
        //public static string StringToHex(string s)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    //sb.Append("0x");
        //    char[] hexs = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        //    int len = s.Length;
        //    char[] cs = s.ToCharArray();
        //    for (int i = 0; i < len; ++i)
        //    {
        //        sb.Append(hexs[cs[i] >> 4]);
        //        sb.Append(hexs[cs[i] & 0xf]);
        //    }

        //    return sb.ToString();
        //}
        ///// <summary>
        ///// 将16进制的字符串转换成普通的字符串
        ///// </summary>
        ///// <param name="s"></param>
        ///// <returns></returns>
        //public static string HexToString(string s)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    int len = s.Length;

        //    char c;
        //    for (int i = 0; i < len; i += 2)
        //    {
        //        c = Convert.ToChar(Convert.ToInt16("0x" + s.Substring(i, 2), 16));
        //        sb.Append(c);
        //    }

        //    return sb.ToString();
        //}
        #endregion

        #region base64
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="encode">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encode, string source)
        {
            string result = "";
            byte[] bytes = encode.GetBytes(source);
            try
            {
                result = Convert.ToBase64String(bytes);
            }
            catch
            {
                result = source;
            }
            return result;
        }
        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string EncodeBase64(string source)
        {
            return EncodeBase64(Encoding.UTF8, source);
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="encode">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(Encoding encode, string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }
        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(string result)
        {
            return DecodeBase64(Encoding.UTF8, result);
        }
        #endregion
    }
}

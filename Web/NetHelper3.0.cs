using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Net;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Threading.Tasks;

namespace System.Net
{
    /// <summary>
    /// http协议访问方法封装
    /// </summary>
    public class NetHelper
    {
        /// <summary>
        /// 指定Url地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求链接地址</param>
        /// <returns></returns>
        public static string Get(string url)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            try
            {
                //获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                stream.Close();
            }
            return result;
        }
        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="dic">请求参数定义</param>
        /// <returns></returns>
        public static string Get(string url, Dictionary<string, string> dic)
        {
            string result = "";
            StringBuilder builder = new StringBuilder();
            builder.Append(url);
            if (dic.Count > 0)
            {
                builder.Append("?");
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
            }
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(builder.ToString());
            //添加参数
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            try
            {
                //获取内容
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }
            finally
            {
                stream.Close();
            }
            return result;
        }
        /// <summary>
        /// 指定Post地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求后台地址</param>
        /// <returns></returns>
        public static string Post(string url)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        /// <summary>
        /// 指定Post地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="url">请求后台地址</param>
        /// <returns></returns>
        public static string Post(string url,Dictionary<string,string> dic)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            #region 添加Post 参数
            StringBuilder builder = new StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            //byte[] data = Encoding.ASCII.GetBytes(builder.ToString());
            byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        /// <summary>
        /// 下载外网文件到指定位置
        /// </summary>
        /// <param name="url">下载的地址</param>
        /// <param name="target">保存的地址</param>
        /// <returns></returns>
        public static bool DownLoad(string url, string target)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadFile(url, target);
                return true;
            }
            catch (Exception ex)
            {
                // ex.Message += "下载外网文件失败," + ex.Message;
                throw new Exception("下载外网文件失败," + ex.Message);
            }
        }
        /// <summary>
        /// 异步下载外网文件到指定位置
        /// </summary>
        /// <param name="url">下载的地址</param>
        /// <param name="target">保存的地址</param>
        /// <returns></returns>
        public static bool DownLoadAsnc(string url, string target)
        {
            try
            {
                WebClient client = new WebClient();
                //使用异步下载
                client.DownloadFileAsync(new Uri(url), target);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 下载文件，返回下载任务
        /// </summary>
        /// <param name="url">下载地址</param>
        /// <param name="target">保存地址</param>
        /// <returns></returns>
        public static Task DownLoadTaskAsnc(string url, string target)
        {
            WebClient client = new WebClient();
            //使用异步下载
            return client.DownloadFileTaskAsync(new Uri(url), target);
        }
        /// <summary>
        /// 下载第三方用户的头像,并保存到当前用户的文件夹
        /// ----目前没有用到
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string DownLoadTouX(string url, int userid)
        {
            //  string userpath = HttpContext.Current.Server.MapPath("~/content/AboveUploadFiles/" + userid);
            string userpath = @"H:\蔚蓝留学网\WLLiuXue\WLLiuXue\Content\AboveUploadFiles\" + userid;

            string username = Guid.NewGuid().ToString() + ".png";
            string result = userid + "\\" + username;

            //判断文件夹是否存在
            if (Directory.Exists(userpath) == false)
            {
                Directory.CreateDirectory(userpath);
            }
            //文件全名
            userpath = userpath + "\\" + username;

            //下载图片，并格式准换
            try
            {
                WebClient client = new WebClient();
                Stream stream = client.OpenRead(url);
                Bitmap bit = new Bitmap(stream);


                //图片格式转换
                //Graphics g = Graphics.FromImage(bit);
                //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //g.SmoothingMode = SmoothingMode.HighQuality;
                //g.CompositingQuality = CompositingQuality.HighQuality;
                //Bitmap png = new Bitmap(bit, bit.Width, bit.Height);
                //g.DrawImage(png, 0, 0);
                //png.Save(userpath, Drawing.Imaging.ImageFormat.Png);
                //g.Dispose();
                //png.Dispose();


                bit.Save(userpath, Drawing.Imaging.ImageFormat.Png);

                stream.Close();
                client.Dispose();

                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
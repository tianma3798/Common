using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Common
{
    /// <summary>
    /// 获取当前 程序的 Webconfig 中的配置
    /// </summary>
    public static class ConfigValue
    {
        /// <summary>
        /// 获取网站的根目录
        /// H:/蔚蓝留学网/WLLiuXue/WLLiuXue
        /// 没有反斜杠
        /// </summary>
        public static string RootPath
        {
            get
            {
                return Get("RootPath");
            }
        }
        /// <summary>
        /// 数据存储位置
        /// 有反斜杠
        /// </summary>
        public static string DataPath
        {
            get
            {
                string path = RootPath + "/data/";
                if (Directory.Exists(path) == false)
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        /// <summary>
        /// 上传临时目录，追加反斜杠
        /// </summary>
        public static string TempFile
        {
            get { return Get("TempFile"); }
        }
        /// <summary>
        /// 上传目录，追加反斜杠
        /// </summary>
        public static string UploadPath
        {
            get { return Get("B_UploadPath"); }
        }

        /// <summary>
        /// 判断是否启用错误处理
        /// </summary>
        public static bool IsCustomError
        {
            get {
                string value = Get("CustomError");
                if (value == "1")
                    return true;

                return false;
            }
        }


        /// <summary>
        /// 获取节点值,字符串类型
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isNull">值是否可以为空或空字符串</param>
        /// <returns></returns>
        public static string Get(string key, bool isNull = false)
        {
            string value = System.Configuration.ConfigurationManager.AppSettings.Get(key);
            if (isNull)
                return value;
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("获取config文件中的节点失败，key=" + key);
            }
            return value;
        }
        /// <summary>
        /// 获取节点值，
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isNull">值是否可以为空</param>
        /// <returns></returns>
        public static int GetInt(string key, bool isNull = false)
        {
            string value = Get(key, isNull);
            int number = 0;
            if (int.TryParse(value, out number))
            {
                return number;
            }
            if (isNull == false)
                throw new Exception("获取config文件中的节点int类型失败，key=" + key);
            return number;
        }
    }
}

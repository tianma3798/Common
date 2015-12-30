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
                return getAppSetting("RootPath");
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
        /// 判断是否启用错误处理
        /// </summary>
        public static bool IsCustomError
        {
            get {
                string value = getAppSetting("CustomError");
                if (value == "1")
                    return true;

                return false;
            }
        }

        private static string getAppSetting(string key)
        {
           return System.Configuration.ConfigurationManager.AppSettings.Get(key);
        }
    }
}

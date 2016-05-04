using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Microsoft.Win32;
using System.Reflection;

namespace Common
{
    /// <summary>
    /// 常用路径使用封装
    /// </summary>
    public class LocalPathHelper
    {
        //windows当前用户的注册表键
        private static RegistryKey folders;
        static LocalPathHelper()
        {
            folders = OpenRegistryKey(Registry.CurrentUser, @"/software/microsoft/windows/currentversion/explorer/shell folders");
        }
        #region 系统路径
        /// <summary>
        /// widnows用户桌面路径
        /// </summary>
        public static string DesktopPath
        {
            get { return GetPath("Desktop"); }
        }
        /// <summary>
        /// windows用户字体目录路径
        /// </summary>
        public static string FontsPath
        {
            get { return GetPath("Fonts"); }
        }
        /// <summary>
        /// windows用户网络邻居路径
        /// </summary>
        public static string NetHoodPath
        {
            get { return GetPath("Nethood"); }
        }
        /// <summary>
        /// windows用户我的文档路径
        /// </summary>
        public static string PersonalPath
        {
            get { return GetPath("Personal"); }
        }
        /// <summary>
        /// windows用户开始菜单程序路径
        /// </summary>
        public static string ProgramsPath
        {
            get { return GetPath("Programs"); }
        }
        /// <summary>
        /// windows用户最近访问文档快捷方式目录
        /// </summary>
        public static string RecentPath
        {
            get { return GetPath("Recent"); }
        }
        /// <summary>
        /// windows用户发送到目录路径
        /// </summary>
        public static string SendToPath
        {
            get { return GetPath("Sendto"); }
        }
        /// <summary>
        /// windows用户开始菜单目录路径
        /// </summary>
        public static string StartMenuPath
        {
            get { return GetPath("StartMenu"); }
        }
        /// <summary>
        /// windows用户开始菜单启动项目路径
        /// </summary>
        public static string StartupPath
        {
            get { return GetPath("Startup"); }
        }
        /// <summary>
        /// windows用户收藏夹目录路径
        /// </summary>
        public static string FavoritesPath
        {
            get { return GetPath("Favorites"); }
        }
        /// <summary>
        /// windows用户网页历史目录路径
        /// </summary>
        public static string HistoryPath
        {
            get { return GetPath("History"); }
        }
        /// <summary>
        /// windows用户cookies目录路径
        /// </summary>
        public static string CookiePath
        {
            get { return GetPath("Cookies"); }
        }
        /// <summary>
        /// windows用户Cache目录路径 
        /// </summary>
        public static string CachePath
        {
            get { return GetPath("Cache"); }
        }
        /// <summary>
        /// windows用户应用程序数据目录
        /// </summary>
        public static string AppdataPath
        {
            get { return GetPath("Appdata"); }
        }
        /// <summary>
        /// windows用户打印目录路径
        /// </summary>
        public static string PrinthoodPath
        {
            get { return GetPath("Printhood"); }
        }
        #endregion

        #region 当前程序相关
        /// <summary>
        /// 当前应用程序目录
        /// </summary>
        public static string CurrentProgram
        {
            get { return Directory.GetCurrentDirectory(); }
        }
        /// <summary>
        /// 当前应用程序解决方案路径
        /// </summary>
        public static string CurrentSolution
        {
            get
            {
                string program = CurrentProgram;
                DirectoryInfo info = new DirectoryInfo(program);
                return info.Parent.Parent.FullName;
            }
        }
        /// <summary>
        /// 获取调用当前方法的dll的目录
        /// </summary>
        public static string CurrentDLL
        {
            get
            {
                Assembly assem = Assembly.GetCallingAssembly();
                FileInfo file = new FileInfo(assem.Location);
                return file.DirectoryName;
            }
        }
        /// <summary>
        /// 获取调用当前方法的dll的目录的子目录
        /// </summary>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static string GetCurrentDLLSub(string sub)
        {
            DirectoryInfo info = new DirectoryInfo(CurrentDLL);
          return  info.CreateSubdirectory(sub).FullName;
        }
        /// <summary>
        /// 获取当前程序的子目录
        /// </summary>
        /// <param name="sub"></param>
        /// <returns></returns>
        public static string GetCurrentProgramSub(string sub)
        {
            string path = CurrentProgram;
            path += "\\" + sub;
            //判断子目录是否存在
            if (Directory.Exists(path))
            {
                return path;
            }
            DirectoryInfo info = new DirectoryInfo(CurrentProgram);
            return info.CreateSubdirectory(sub).FullName;
        }
        /// <summary>
        /// 获取当前程序下的数据目录
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentProgramData()
        {
            return GetCurrentProgramSub("data");
        }
        #endregion


        #region 私有方法
        /// <summary>
        /// 获取键值对应的文件夹
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        private static string GetPath(string key)
        {
            string path = folders.GetValue(key).ToString();
            if (!string.IsNullOrEmpty(path))
            {
                if (Directory.Exists(path))
                {
                    return path;
                }
            }
            return "'" + key + "'对应的文件夹不存在";
        }
        //打开，指定根节点和路径的注册表项
        private static RegistryKey OpenRegistryKey(RegistryKey root, string str)
        {
            str = str.Remove(0, 1) + @"/";
            while (str.IndexOf(@"/") != -1)
            {
                root = root.OpenSubKey(str.Substring(0, str.IndexOf(@"/")));
                str = str.Remove(0, str.IndexOf(@"/") + 1);
            }
            return root;
        }
        #endregion
    }
}

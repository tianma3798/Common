using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 发送邮件，发件人定义
    /// </summary>
    public class SmtpSendInfo
    {
        /// <summary>
        /// 发件人用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 发件人，密码
        /// </summary>
        public string Passwrod { get; set; }
        /// <summary>
        /// 发送的服务器主机
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 发送的端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 获取配置文件中的发件人信息
        /// </summary>
        /// <returns></returns>
        public static SmtpSendInfo GetInfoFromConfig()
        {
            SmtpSendInfo info = new SmtpSendInfo();
            info.UserName = ConfigValue.Get("SmtpEmail");
            info.Passwrod = ConfigValue.Get("SmtpPwd");
            info.Host = ConfigValue.Get("SmtpHost");
            info.Port = Convert.ToInt32(ConfigValue.Get("SmtpPort"));
            return info;
        }
    }
}

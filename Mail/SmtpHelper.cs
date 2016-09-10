using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 发送邮件版主类，封装
    /// </summary>
    public class SmtpHelper
    {
        /// <summary>
        /// 收件人地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 发件人及服务器信息
        /// </summary>
        public SmtpSendInfo sendinfo { get; set; }

        /// <summary>
        /// 发送邮件成功
        /// </summary>
        public Action<string, string> OnSuccess;
        /// <summary>
        /// 发送邮件失败
        /// </summary>
        public Action<string, string> OnFailure;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="Address">收件人地址</param>
        /// <param name="Subject">主题</param>
        /// <param name="Body">内容</param>
        public SmtpHelper(string Address, string Subject, string Body)
        {
            this.Address = Address;
            this.Subject = Subject;
            this.Body = Body;
            this.sendinfo = SmtpSendInfo.GetInfoFromConfig();
        }
        /// <summary>
        /// 获取发件信息
        /// </summary>
        /// <returns></returns>
        public MailMessage GetMail()
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(sendinfo.UserName);
            mail.To.Add(new MailAddress(this.Address));
            //主题
            mail.Subject = this.Subject;
            //内容
            mail.Body = this.Body;
            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;
            mail.IsBodyHtml = true;
            //优先级
            mail.Priority = MailPriority.High;
            return mail;
        }
        /// <summary>
        /// 发送当前邮件
        /// </summary>
        /// <returns></returns>
        public bool SendMail()
        {
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(sendinfo.UserName, sendinfo.Passwrod);
            //邮件服务器，设置
            client.Host = sendinfo.Host;
            client.Port = sendinfo.Port;
            client.EnableSsl = true;

            MailMessage mail = GetMail();
            try
            {
                client.Send(mail);
                //发送邮件成功
                if (this.OnSuccess != null)
                {
                    this.OnSuccess(mail.To.First().Address, mail.Subject);
                }
                return true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                if (this.OnFailure != null)
                {
                    this.OnFailure(mail.To.First().Address, mail.Subject + "，失败描述：" + ex.Message);
                }
                return false;
            }
            catch (Exception ex)
            {
                if (this.OnFailure != null)
                {
                    this.OnFailure(mail.To.First().Address, mail.Subject + "，失败描述：" + ex.Message);
                }
                return false;
            }
        }

    }
}

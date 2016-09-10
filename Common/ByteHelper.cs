using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 常用字节，计算机存储换算等
    /// </summary>
    public class ByteHelper
    {
        /// <summary>
        /// 字节数
        /// </summary>
        public double ByteCount { get; set; }
        /// <summary>
        /// 默认构造器
        /// </summary>
        public ByteHelper()
        {

        }
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="ByteCount">字节数目</param>
        public ByteHelper(double ByteCount)
        {
            this.ByteCount = ByteCount;
        }
        /// <summary>
        /// 指定位数量
        /// </summary>
        /// <param name="bCount"></param>
        public void Setb(double bCount)
        {
            this.ByteCount = bCount / 8;
        }
        /// <summary>
        /// 指定kb数量，千字节
        /// </summary>
        /// <param name="kbCount"></param>
        public void SetKb(double kbCount) {
            this.ByteCount = kbCount * 1024;
        }
        /// <summary>
        /// 指定 mb数量，兆字节
        /// </summary>
        /// <param name="mbCount"></param>
        public void SetMb(double mbCount) {
            this.ByteCount = mbCount * 1024 * 1024;
        }
        /// <summary>
        /// 指定 gb 数量，吉字节
        /// </summary>
        /// <param name="gbCount"></param>
        public void SetGb(double gbCount)
        {
            this.ByteCount = gbCount * 1024 * 1024 * 1024;
        }
        /// <summary>
        /// 获取常用显示字符串名称,
        /// 四舍五入，2位小数
        /// </summary>
        /// <returns></returns>
        public string GetShow()
        {
            double bytes = this.ByteCount;
            //1kb=1024 byte
            //1mb=1024 kb   1048576   byte
            //1g=1024 mb    1073741824 byte
            //1t=1024 gb    1099511627776 byte
            if (bytes > 1099511627776)
            {
                double result = bytes / 1073741824;
                result = Math.Round(result, 2);
                return result.ToString() + "T";
            }
            else if (bytes > 1073741824)
            {
                double result = bytes / 1073741824;
                result = Math.Round(result, 2);
                return result.ToString() + "G";
            }
            else if (bytes > 1048576)
            {
                double result = bytes / 1048576;
                result = Math.Round(result, 2);
                return result.ToString() + "MB";
            }
            else
            {
                double result = bytes / 1024;
                result = Math.Round(result, 2);
                return result.ToString() + "KB";
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 分页参数传递
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// 空处理
        /// </summary>
        public PageInfo() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">页数据</param>
        public PageInfo(int PageIndex, int PageSize)
        {
            this.PageIndex = PageIndex;
            this.PageSize = PageSize;
        }
        /// <summary>
        /// 当前页 >=1
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页数据量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 数据记录总量
        /// </summary>
        public int RecordCount { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling(this.RecordCount / (double)PageSize);
            }
        }
        /// <summary>
        /// 数据库查询开始位置
        /// </summary>
        public int StartIndex
        {
            get
            {
                return (PageIndex - 1) * PageSize + 1;
            }
        }
        /// <summary>
        /// 数据库查询结束位置
        /// </summary>
        public int EndIndex
        {
            get
            {
                int endIndex = this.StartIndex + PageSize;
                return endIndex;
            }
        }
        /// <summary>
        /// 数据库查询，划过的个数
        /// </summary>
        public int SkipCount
        {
            get { return (PageIndex - 1) * PageSize; }
        }
    }
}

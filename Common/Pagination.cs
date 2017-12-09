using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class Pagination
    {

        public Pagination(int Offset, int PageSize)
        {
            this.Offset = Offset;
            this.PageSize = PageSize;
        }

        /// <summary>
        /// 划过个数
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// 获取个数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数量
        /// </summary>
        public int Total { get; set; }
    }
}

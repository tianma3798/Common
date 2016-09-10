using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 使用反射， 获取object 对象的属性
    /// </summary>
    public class ObjectHelper
    {
        /// <summary>
        /// 获取string 属性值
        /// </summary>
        /// <param name="obj">当前 对象</param>
        /// <param name="property">属性名</param>
        /// <returns></returns>
        public static string GetString(object obj, string property)
        {
            string val = obj.GetType().GetProperty(property).GetValue(obj) as string;
            if (string.IsNullOrEmpty(val))
                return "";
            return val;
        }
        /// <summary>
        /// 获取int 属性值
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="property">属性名</param>
        /// <returns></returns>
        public static int GetInt(object obj, string property)
        {
            object val = obj.GetType().GetProperty(property).GetValue(obj);
            if (val != null)
                return Convert.ToInt32(val);
            return -1;
        }
    }
}

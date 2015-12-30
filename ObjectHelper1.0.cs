using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class ObjectHelper
    {
        //获取 object 对象的 指定属性
        //返回字符串
        public static string GetString(object obj, string property)
        {
            string val = obj.GetType().GetProperty(property).GetValue(obj) as string;
            if (string.IsNullOrEmpty(val))
                return "";
            return val;
        }
        //获取int
        public static int GetInt(object obj, string property)
        {
            object val = obj.GetType().GetProperty(property).GetValue(obj);
            if (val != null)
                return Convert.ToInt32(val);
            return -1;
        }
    }
}

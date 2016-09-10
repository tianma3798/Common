using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Common
{
    /// <summary>
    /// Xml序列化帮助类
    /// </summary>
    public class XmlSerializerHelper
    {
        //对应的磁盘文件路径
        private string _FileName;
        /// <summary>
        /// 对应的磁盘文件路径
        /// </summary>
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        //Xml要序列化的类型
        private Type _Type;
        /// <summary>
        /// Xml要序列化的类型
        /// </summary>
        public Type Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private XmlSerializer _XmlSerializer;
        /// <summary>
        /// 构造器
        /// </summary>
        public XmlSerializerHelper()
        {

        }
        /// <summary>
        /// 构造器，指定文件地址
        /// </summary>
        /// <param name="filename"></param>
        public XmlSerializerHelper(string filename)
        {
            this.FileName = filename;
        }
        ///// <summary>
        ///// 序列化到磁盘
        ///// </summary>
        ///// <param name="obj">保存的对象</param>
        //public void Serialize(object obj)
        //{
        //    StreamWriter writer = new StreamWriter(FileName);
        //    try
        //    {
        //        _XmlSerializer.Serialize(writer, obj);
        //    }
        //    finally
        //    {
        //        writer.Close();
        //    }
        //}

        /// <summary>
        /// 序列化到磁盘
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">保存的对象</param>
        public void Serialize<T>(T obj)
        {
            if (_XmlSerializer == null)
            {
                _XmlSerializer = new XmlSerializer(typeof(T));
            }
            StreamWriter writer = new StreamWriter(FileName);
            try
            {
                _XmlSerializer.Serialize(writer, obj);
            }
            finally
            {
                writer.Close();
            }
        }
        /// <summary>
        /// 反序列化，返回
        /// </summary>
        /// <typeparam name="T">反序列化后的类型</typeparam>
        /// <returns></returns>
        public T Deserialize<T>()
        {
            if (_XmlSerializer == null)
            {
                _XmlSerializer = new XmlSerializer(typeof(T));
            }
            StreamReader reader = new StreamReader(FileName);
            T result;
            try
            {
                result = (T)_XmlSerializer.Deserialize(reader);
            }
            finally
            {
                reader.Close();
            }
            return result;
        }
    }
}
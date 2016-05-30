/* ========================================================================
* Copyright @  2016
* 作者：Fallstar       时间：2016/5/27 下午 3:56:11
* 说明：序列化帮助类
* ========================================================================
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CopyrightHelper.Core
{
    /// <summary>
    /// 序列化帮助类
    /// </summary>
    public static class SerializeHelper
    {
        /// <summary>
        /// 将对象以Xml方式序列化到文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="filePath"></param>
        public static void ToXmlFile<T>(T obj, string filePath)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");
            using (var fs = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            using (var ms = ToXmlStream(obj))
            {
                ms.CopyTo(fs);
            }
        }

        /// <summary>
        /// 序列化到流里面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static MemoryStream ToXmlStream<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            var ser = new XmlSerializer(typeof(T), string.Empty);
            var ms = new MemoryStream();
            ser.Serialize(ms, obj);
            return ms;
        }

        /// <summary>
        /// 从Xml文件里面读取内容并反序列化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T FromXmlFile<T>(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");
            if (!File.Exists(filePath))
                return default(T);

            var ser = new XmlSerializer(typeof(T), string.Empty);
            using (var fs = File.OpenRead(filePath))
            {
                return (T)ser.Deserialize(fs);
            }
        }
    }
}

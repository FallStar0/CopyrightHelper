/* ========================================================================
* Copyright @  2016
* 作者：Fallstar       时间：2016/5/27 下午 3:40:16
* 说明：用于保存数据的实体
* ========================================================================
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CopyrightHelper.Models
{
    /// <summary>
    /// 用于保存数据的实体
    /// </summary>
    [XmlRoot]
    public class StoreConfig : ICloneable
    {
        /// <summary>
        /// 配置的保存时间
        /// </summary>
        public DateTime SaveTime { get; set; }

        /// <summary>
        /// 你的名称
        /// </summary>
        public string YourName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 是否将结果插入到文件头
        /// </summary>
        public bool IsInsertToTop { get; set; }

        /// <summary>
        /// 实际保存的数据细节
        /// </summary>
        public List<StoreItemConfig> Configs { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public void Sort()
        {
            //if (Configs == null || Configs.Count < 2) return;
            //Configs = Configs.OrderByDescending(x => x.Order).ToList();
        }

        /// <summary>
        /// 获取某个关键字的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public StoreItemConfig GetItemByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if (Configs == null || Configs.Count == 0)
                return null;
            return Configs.FirstOrDefault(x => x.Key == key);
        }

        /// <summary>
        /// 通过拓展名，来获取最匹配的插入语句
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public StoreItemConfig GetItemByExtension(string ext)
        {
            if (string.IsNullOrEmpty(ext))
                return Configs.FirstOrDefault(x => x.IsContainsKey(".*"));
            foreach (var item in Configs)
            {
                if (item.IsContainsKey(ext))
                    return item;
            }
            return Configs.FirstOrDefault(x => x.IsContainsKey(".*"));
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            var cfg = MemberwiseClone() as StoreConfig;
            if (cfg.Configs == null)
                return cfg;

            cfg.Configs = new List<StoreItemConfig>();
            foreach (var item in Configs)
            {
                cfg.Configs.Add((StoreItemConfig)item.Clone());
            }

            return cfg;
        }
    }

    /// <summary>
    /// 每一条记录
    /// </summary>
    [XmlRoot]
    public class StoreItemConfig : ICloneable
    {
        /// <summary>
        /// 在列表中显示的内容，如*.*
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 排序，越大就越在上面，优先级更高
        /// </summary>
        //public int Order { get; set; }
        /// <summary>
        /// 编辑框里面的内容
        /// </summary>
        [XmlIgnore]
        public string Content { get; set; }

        /// <summary>
        /// 用于序列化的马甲
        /// </summary>
        [XmlElement("Content")]
        public XmlNode ContentCData
        {
            get
            {
                XmlNode node = new XmlDocument().CreateNode(XmlNodeType.CDATA, "", "");
                node.InnerText = Content;
                return node;
            }
            set { Content = value.Value; }
        }

        /// <summary>
        /// 是否匹配
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsContainsKey(string key)
        {
            if (string.IsNullOrEmpty(Key)) return false;
            if (Key.Contains(","))
            {
                var sp = Key.Split(',');
                foreach (var item in sp)
                {
                    if (string.IsNullOrEmpty(item)) continue;
                    if (item.Contains(key))
                        return true;
                }
            }
            else
            {
                if (Key.Contains(key))
                    return true;
                if (Key == "*.*")
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}

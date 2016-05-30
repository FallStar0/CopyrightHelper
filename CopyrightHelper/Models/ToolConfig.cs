/* ========================================================================
* Copyright @ 2016
* 作者：Fallstar       时间：2016/5/27 下午 3:45:50
* 说明：用于缓存到内存里面存储和匹配的配置实体。
* ========================================================================
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyrightHelper.Models
{
    /// <summary>
    /// 用于缓存到内存里面存储和匹配的配置实体。
    /// </summary>
    public class ToolConfig : ICloneable
    {
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
        /// 配置列表，已经排序了。
        /// </summary>
        public List<ToolItemConfig> Configs { get; set; }

        /// <summary>
        /// 通过拓展名，来获取最匹配的插入语句
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public ToolItemConfig GetItemByExtension(string ext)
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
            var cfg = MemberwiseClone() as ToolConfig;
            if (Configs == null)
                return cfg;

            cfg.Configs = new List<ToolItemConfig>();
            foreach (var item in Configs)
            {
                cfg.Configs.Add((ToolItemConfig)item.Clone());
            }

            return cfg;
        }
    }
    /// <summary>
    /// 单个配置实体
    /// </summary>
    public class ToolItemConfig : ICloneable
    {
        /// <summary>
        /// 主键集合，如{*.cs,*.txt}
        /// </summary>
        public string[] Keys { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否匹配
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsContainsKey(string key)
        {
            foreach (var item in Keys)
            {
                if (item == key)
                    return true;
                if (item == ".*")
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

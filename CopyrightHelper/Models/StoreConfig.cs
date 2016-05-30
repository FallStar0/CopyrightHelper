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

namespace CopyrightHelper.Models
{
    /// <summary>
    /// 用于保存数据的实体
    /// </summary>
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
        /// 实际保存的数据细节
        /// </summary>
        public List<StoreItemConfig> Configs { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public void Sort()
        {
            if (Configs == null || Configs.Count < 2) return;
            Configs = Configs.OrderByDescending(x => x.Order).ToList();
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
    public class StoreItemConfig : ICloneable
    {
        /// <summary>
        /// 在列表中显示的内容，如*.*
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 排序，越大就越在上面，优先级更高
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 编辑框里面的内容
        /// </summary>
        public string Content { get; set; }

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

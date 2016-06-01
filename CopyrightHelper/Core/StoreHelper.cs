/* ========================================================================
* Copyright @  2016
* 作者：Fallstar       时间：2016/5/27
* 说明：用于对配置文件的读写。
* ========================================================================
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CopyrightHelper.Models;
using System.IO;

namespace CopyrightHelper.Core
{
    /// <summary>
    /// 用于对配置文件的读写。
    /// </summary>
    internal static class StoreHelper
    {
        /// <summary>
        /// 从配置文件读取配置
        /// </summary>
        /// <returns></returns>
        public static StoreConfig Load()
        {
            if (!File.Exists(Constants.ConfigFilePath))
            {
                return GetDefaultConfig();
            }
            var store = SerializeHelper.FromXmlFile<StoreConfig>(Constants.ConfigFilePath);
            //返回默认
            if (store == null || store.Configs == null || store.Configs.Count == 0)
            {
                store = GetDefaultConfig();
            }

            //store.Sort();

            return store;
        }

        /// <summary>
        /// 生成默认配置
        /// </summary>
        /// <returns></returns>
        private static StoreConfig GetDefaultConfig()
        {
            var cfg = new StoreConfig()
            {
                YourName = "Your Name",
                CompanyName = "Your Company",
            };
            cfg.Configs = new List<StoreItemConfig>();
            var it = new StoreItemConfig()
            {
                Key = "*.*",
            };
            it.Content = @"//===================================================
//  Copyright @  @company @year
//  作者：@yourname
//  时间：@time
//  说明：
//===================================================
";
            cfg.Configs.Add(it);
            return cfg;
        }

        /// <summary>
        /// 保存到配置文件
        /// </summary>
        /// <param name="cfg"></param>
        public static void Save(StoreConfig cfg)
        {
            SerializeHelper.ToXmlFile(cfg, Constants.ConfigFilePath);
        }

    }
}

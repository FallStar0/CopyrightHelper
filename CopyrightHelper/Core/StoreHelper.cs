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
        /// 将配置转行为ToolConfig
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public static ToolConfig ConvertToToolConfig(StoreConfig store)
        {
            var cfg = new ToolConfig()
            {
                CompanyName = store.CompanyName,
                YourName = store.YourName,
                Configs = new List<ToolItemConfig>(),
                IsInsertToTop = store.IsInsertToTop,
            };

            foreach (var item in store.Configs)
            {
                if (string.IsNullOrEmpty(item.Key)) continue;
                var it = new ToolItemConfig()
                {
                    Content = item.Content,
                };
                var listKey = new List<string>();
                var key = item.Key.Trim().ToLower();
                if (item.Key.Contains(","))
                {
                    var keys = key.Split(',');
                    foreach (var k in keys)
                    {
                        var kt = k.Trim();
                        listKey.Add(kt.Substring(kt.LastIndexOf('.')));
                    }
                }
                else
                {
                    listKey.Add(key.Substring(key.LastIndexOf('.')));
                }
                it.Keys = listKey.ToArray();
                cfg.Configs.Add(it);
            }

            return cfg;
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

        /// <summary>
        /// 将ToolConfig转换为StoreConfig
        /// </summary>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static StoreConfig ConvertFromToolConfig(ToolConfig cfg)
        {
            var store = new StoreConfig()
            {
                CompanyName = cfg.CompanyName,
                YourName = cfg.YourName,
                SaveTime = DateTime.Now,
                Configs = new List<StoreItemConfig>(),
                IsInsertToTop = cfg.IsInsertToTop,
            };
            if (cfg.Configs == null || cfg.Configs.Count == 0)
                return store;
            var order = cfg.Configs.Count;
            foreach (var item in cfg.Configs)
            {
                var it = new StoreItemConfig()
                {
                    Content = item.Content,
                };
                store.Configs.Add(it);
                var key = string.Empty;
                foreach (var k in item.Keys)
                {
                    key += "*" + k + ",";
                }
                it.Key = key.TrimEnd(',');
                order--;
            }

            return store;
        }
    }
}

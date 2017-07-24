/* ========================================================================
* Copyright @  2016
* 作者：Fallstar       时间：2016/5/27 下午 4:32:20
* 说明：The core function of the plugin.
* ========================================================================
*/
using CopyrightHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace CopyrightHelper.Core
{
    /// <summary>
    /// The core function of the plugin.
    /// </summary>
    public static class CopyrightCore
    {
        #region Fileds
        /// <summary>
        /// The current store config
        /// </summary>
        internal static StoreConfig CurrentStoreConfig { get; set; }


        #endregion

        #region Load And Save
        /// <summary>
        /// Load from file
        /// </summary>
        public static void Load()
        {
            try
            {
                CurrentStoreConfig = StoreHelper.Load();
                //CurrentToolConfig = StoreHelper.ConvertToToolConfig(CurrentStoreConfig);
            }
            catch (Exception ex)
            {
                Trace.Fail(ex.Message + ex.StackTrace);
            }
        }
        /// <summary>
        /// Save to file
        /// </summary>
        public static void Save()
        {
            try
            {
                if (CurrentStoreConfig == null)
                    Load();
                CurrentStoreConfig.SaveTime = DateTime.Now;

                StoreHelper.Save(CurrentStoreConfig);
                //CurrentToolConfig = StoreHelper.ConvertToToolConfig(CurrentStoreConfig);
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Main function
        /// <summary>
        /// 通过文件拓展名，进行匹配，返回对应需要生成的语句
        /// </summary>
        /// <param name="ext">.cs</param>
        /// <returns></returns>
        public static string GetContentByExtension(string ext)
        {
            if (string.IsNullOrEmpty(ext)) return null;
            ext = ext.ToLower();
            if (CurrentStoreConfig == null)
            {
                Load();
            }
            var it = CurrentStoreConfig.GetItemByExtension(ext);
            if (it == null) return null;

            return ConvertToolItem(it);
        }
        /// <summary>
        /// 转换为用于插入的语句
        /// </summary>
        /// <param name="toolConfig"></param>
        /// <returns></returns>
        private static string ConvertToolItem(StoreItemConfig toolConfig)
        {
            var cfg = CurrentStoreConfig;
            var content = toolConfig.Content;
            if (string.IsNullOrEmpty(content)) return content;
            var tf = cfg.TimeFormat;
            if (tf == null || string.IsNullOrEmpty(tf))
                tf = "yyyy-MM-dd HH:mm:ss";
            content = content.Replace("@company", cfg.CompanyName);
            content = content.Replace("@yourname", cfg.YourName);
            content = content.Replace("@year", DateTime.Now.Year.ToString());
            content = content.Replace("@time", DateTime.Now.ToString(tf));
            //修正换行不对问题
            if (content.Contains("\r\n"))
                content.Replace("\r\n", Environment.NewLine);
            else if (content.Contains("\n"))
                content.Replace("\n", Environment.NewLine);

            return content;
        }
        #endregion
    }
}

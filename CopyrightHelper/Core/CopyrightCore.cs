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
        /// The current config of the function
        /// </summary>
        internal static ToolConfig CurrentToolConfig { get; set; }
        /// <summary>
        /// The current store config
        /// </summary>
        internal static StoreConfig CurrentStoreConfig { get; set; }


        #endregion

        #region Load And Save
        /// <summary>
        /// Load from file
        /// </summary>
        public void Load()
        {
            try
            {
                CurrentToolConfig = StoreHelper.Load();
                CurrentStoreConfig = StoreHelper.ConvertFromToolConfig(CurrentToolConfig);
            }
            catch (Exception ex)
            {
                Trace.Fail(ex.Message + ex.StackTrace);
            }
        }
        #endregion

        #region MyRegion

        #endregion
    }
}

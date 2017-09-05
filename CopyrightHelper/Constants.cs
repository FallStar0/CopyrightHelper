/* ========================================================================
* Copyright @ 2016
* 作者：Fallstar       时间：2016/5/27 下午 4:13:31
* 说明：内部静态变量
* ========================================================================
*/
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyrightHelper
{
    /// <summary>
    /// 内部静态变量
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// 文件版本
        /// </summary>
        public const string FileVersion = "1.3.5.0";
        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static readonly string ConfigFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "CopyrightTool\\Config.xml");
    }
}

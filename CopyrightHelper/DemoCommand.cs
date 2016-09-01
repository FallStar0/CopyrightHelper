//------------------------------------------------------------------------------
// <copyright file="DemoCommand.cs" company="Microsoft">
//     Copyright (c) Microsoft.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE;
using System.IO;
using CopyrightHelper.Core;

namespace CopyrightHelper
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class DemoCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("35664e0f-2d41-46aa-aa0a-42d160dfc50b");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private DemoCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static DemoCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new DemoCommand(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            //获取服务，这玩意儿……可以理解为vs的服务对象吧。
            var dte = this.ServiceProvider.GetService(typeof(DTE)) as DTE;
            var selection = dte.ActiveDocument.Selection as TextSelection;
            if (selection == null) return;
            //获取拓展名
            var ext = Path.GetExtension(dte.ActiveDocument.FullName);

            //删除所选内容
            if (!selection.IsEmpty)
                selection.Delete();

            var txt = CopyrightCore.GetContentByExtension(ext);
            if (txt == null) return;
            //Insert to top
            if(CopyrightCore.CurrentStoreConfig.IsInsertToTop)
                selection.StartOfDocument();
            selection.Insert(txt);
            
            //selection.Insert("//Demo test~~~" + Environment.NewLine + "//Test line 2");
            
        }
    }
}

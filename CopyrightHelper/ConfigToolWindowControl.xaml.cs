//------------------------------------------------------------------------------
// <copyright file="ConfigToolWindowControl.xaml.cs" company="Microsoft">
//     Copyright (c) Microsoft.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace CopyrightHelper
{
    using Core;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ConfigToolWindowControl.
    /// </summary>
    public partial class ConfigToolWindowControl : UserControl
    {
        #region 初始化
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigToolWindowControl"/> class.
        /// </summary>
        public ConfigToolWindowControl()
        {
            this.InitializeComponent();
            InitEvents();
            Loaded += WinLoaded;
        }

        private void WinLoaded(object sender, RoutedEventArgs e)
        {
            InitControls();

            var cfg = CopyrightCore.CurrentStoreConfig;

        }

        /// <summary>
        /// Init events and bindings
        /// </summary>
        private void InitEvents()
        {
            lbType.SelectionChanged += LbType_SelectionChanged;

        }

        /// <summary>
        /// Init controls
        /// </summary>
        private void InitControls()
        {
            txtInput.Text = string.Empty;
            txtContent.Text = string.Empty;
        }


        #endregion

        private void LbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #region Add Or Delete

        #endregion

        private void Cmd_CanExecuteChanged(object sender, System.EventArgs e)
        {

        }

        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {

        }
    }
}
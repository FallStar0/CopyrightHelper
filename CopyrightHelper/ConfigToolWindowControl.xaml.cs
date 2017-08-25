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
    using System.Linq;
    using Models;
    using System;

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

            lblVersion.Text = Constants.FileVersion;

            CopyrightCore.Load();
            var cfg = CopyrightCore.CurrentStoreConfig;

            txtCompanyName.Text = cfg.CompanyName;
            txtYourName.Text = cfg.YourName;
            cbIsInsertToTop.IsChecked = cfg.IsInsertToTop;
            txtTimeFormat.Text = cfg.TimeFormat;

            BindTypeList();

            lbType.SelectedIndex = 0;
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
        /// <summary>
        /// bind types to list
        /// </summary>
        private void BindTypeList()
        {
            var cfg = CopyrightCore.CurrentStoreConfig;
            lbType.Items.Clear();
            foreach (var item in cfg.Configs)
            {
                lbType.Items.Add(item.Key);
            }
        }
        #endregion

        private void LbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtContent.Clear();
            var lb = sender as ListBox;
            if (lb.SelectedIndex < 0)
            {
                return;
            }
            var selectItem = lb.SelectedItem.ToString();
            var it = CopyrightCore.CurrentStoreConfig.Configs.FirstOrDefault(x => x.Key == selectItem);
            if (it != null)
                txtContent.Text = it.Content;
        }

        #region Commands
        private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            var para = e.Parameter.ToString();
            try
            {
                switch (para)
                {
                    case "Clear": txtContent.Clear(); break;

                    case "Save": CmdSave(); break;

                    case "Add": CmdAdd(); break;

                    case "Del": CmdDel(); break;

                    case "MoveUp": MoveItem(-1); break;
                    case "MoveDown": MoveItem(1); break;

                    case "About": About(); break;

                    default: return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Del
        /// </summary>
        private void CmdDel()
        {
            if (lbType.SelectedIndex < 0)
                return;
            var cfg = CopyrightCore.CurrentStoreConfig;
            var selectItem = lbType.SelectedItem.ToString();
            var it = cfg.GetItemByKey(selectItem);
            if (it == null) return;
            cfg.Configs.Remove(it);

            lbType.Items.Remove(it.Key);
            if (lbType.Items.Count > 0)
                lbType.SelectedIndex = 0;
        }

        /// <summary>
        /// Add
        /// </summary>
        private void CmdAdd()
        {
            var key = txtInput.Text.Trim();
            var cfg = CopyrightCore.CurrentStoreConfig;
            var curr = cfg.GetItemByKey(key);
            if (curr != null)
            {
                MessageBox.Show("不能添加重复的Key!");
                return;
            }
            var it = new StoreItemConfig()
            {
                Content = txtContent.Text,
                Key = key,
            };
            cfg.Configs.Insert(0, it);
            lbType.Items.Insert(0, key);
            lbType.SelectedIndex = 0;
        }

        /// <summary>
        /// Save
        /// </summary>
        private void CmdSave()
        {
            var cfg = CopyrightCore.CurrentStoreConfig;
            var selectItem = lbType.SelectedItem.ToString();
            var it = cfg.GetItemByKey(selectItem);
            if (it != null)
                it.Content = txtContent.Text;
            cfg.CompanyName = txtCompanyName.Text.Trim();
            cfg.YourName = txtYourName.Text.Trim();
            cfg.IsInsertToTop = cbIsInsertToTop.IsChecked == true;
            var tf = txtTimeFormat.Text.Trim();
            if (!string.IsNullOrEmpty(tf))
            {
                try
                {
                    var t = DateTime.Now.ToString(tf);
                }
                catch
                {
                    MessageBox.Show(VSPackage.Err_TimeFormat);
                    return;
                }
            }
            cfg.TimeFormat = tf;

            CopyrightCore.Save();
        }

        /// <summary>
        /// 移动当前选项的顺序
        /// </summary>
        /// <param name="step">1=向下移动1位，-1表示向上移动一位</param>
        private void MoveItem(int step = 1)
        {
            var cfg = CopyrightCore.CurrentStoreConfig;
            var selectIndex = lbType.SelectedIndex;
            var targetIndex = selectIndex + step;
            if ((targetIndex > cfg.Configs.Count - 1) || (targetIndex < 0)) return;

            var selectItem = lbType.SelectedItem.ToString();
            lbType.Items.RemoveAt(selectIndex);
            lbType.Items.Insert(targetIndex, selectItem);
            lbType.SelectedIndex = targetIndex;

            var it = cfg.GetItemByKey(selectItem);
            cfg.Configs.Remove(it);
            cfg.Configs.Insert(targetIndex, it);
        }

        /// <summary>
        /// 关于
        /// </summary>
        private void About()
        {
            //var brow = new WebBrowser();
            //brow.Navigate("http://git.oschina.net/fallstar/CopyrightHelper");

            MessageBox.Show($"CopyrightHelper v{Constants.FileVersion} by Fallstar" + Environment.NewLine + "Home : http://git.oschina.net/fallstar/CopyrightHelper");
        }
        #endregion


    }
}
﻿using AurelienRibon.Ui.SyntaxHighlightBox;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace XenkoShaderExplorer
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
            Title = "Shader Explorer for Xenko " + Assembly.GetEntryAssembly().GetName().Version;
            codeView.CurrentHighlighter = HighlighterManager.Instance.Highlighters["XKSL"];
            XenkoDirMode.ItemsSource = Enum.GetValues(typeof(XenkoSourceDirMode)).Cast<XenkoSourceDirMode>();
            XenkoDirMode.SelectedIndex = 0;
            XenkoDirMode.SelectionChanged += XenkoDirMode_SelectionChanged;
        }

        private void XenkoDirMode_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ViewModel.XenkoDirMode = (XenkoSourceDirMode)XenkoDirMode.SelectedIndex;
            ViewModel.Refresh();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var shader = (Shader)e.NewValue;

            if (shader != null)
                codeView.Text = File.ReadAllText(shader.Path);
        }

        private void OnExpandAllButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ExpandAll(true);
        }

        private void OnCollapseAllButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.ExpandAll(false);
        }

        private void OnInfoButtonClick(object sender, RoutedEventArgs e)
        {
            new InfoWindow().ShowDialog();
        }
    }
}

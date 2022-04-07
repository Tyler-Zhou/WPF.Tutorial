using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfTreeView
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constrcutor
        public MainWindow()
        {
            InitializeComponent();
            DataContext=new DirectoryStructureViewModel();
        }
        #endregion
    }
}

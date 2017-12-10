using System;
using System.Windows;
using System.Windows.Input;

namespace TPIS.Command
{
    public partial class TPISCommand
    {
        public static RoutedCommand NewProject = new RoutedCommand();
        public static RoutedCommand OpenProject = new RoutedCommand();
        public static RoutedCommand CloseProject = new RoutedCommand();
        public static RoutedCommand Save = new RoutedCommand();
        public static RoutedCommand SaveAs = new RoutedCommand();
        public static RoutedCommand SaveAll = new RoutedCommand();
        public static RoutedCommand Print = new RoutedCommand();
        public static RoutedCommand RecentlyUsedProject = new RoutedCommand();
        public static RoutedCommand Exit = new RoutedCommand();

        public TPISCommand()
        {
            Clipboard.Clear();
        }

        #region 新建工程

        private void NewProject_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            Window new_project = new NewProject();
            new_project.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            new_project.ShowDialog();
        }

        #endregion

        #region 打开工程

        private void OpenProject_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "选择TPIS工程项目";
            openFileDialog.Filter = "TPIS工程项目(*.tpis)|*.tpis";
            //openFileDialog.FileName = "*.tpis";
            openFileDialog.FilterIndex = 1;
            openFileDialog.ValidateNames = false;
            openFileDialog.CheckFileExists = false;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = true;//允许同时选择多个文件 
            bool? result = openFileDialog.ShowDialog();
            if (result != true)
            {
                return;
            }
            else
            {
                string[] files = openFileDialog.FileNames;
                foreach (string file in files)
                {
                    System.Windows.MessageBox.Show("已选择文件:" + file, "选择文件提示");
                }
            }
        }

        #endregion

        #region 关闭工程

        private void CloseProject_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("NewProject");
        }

        #endregion

        #region 存储

        private void Save_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("NewProject");
        }

        #endregion

        #region 另存为

        private void SaveAs_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("NewProject");
        }

        #endregion

        #region 全部存储

        private void SaveAll_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("NewProject");
        }

        #endregion

        #region 打印

        private void Print_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("NewProject");
        }

        #endregion

        #region 最近使用过的工程

        private void RecentlyUsedProject_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("NewProject");
        }

        #endregion

        #region 退出

        private void Exit_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("NewProject");
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

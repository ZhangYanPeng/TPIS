using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TPIS.Project;

namespace TPIS.Command
{
    public partial class TPISCommand
    {
        public TPISCommand() : base()
        {
            System.Windows.Clipboard.Clear();
        }

        private void OnSubMenuOpened(object sender, RoutedEventArgs e)
        {
            MenuItem menuitem = sender as MenuItem;
            menuitem.IsSubmenuOpen = true;
            MainWindow mw = (MainWindow)System.Windows.Application.Current.MainWindow;
            if(mw.GetCurrentProject() == null)
            {
                SaveOpe.IsEnabled = false;
                SaveAsOpe.IsEnabled = false;
                SaveAllOpe.IsEnabled = false;
                CloseOpe.IsEnabled = false;
                PrintOpe.IsEnabled = false;
                CutOpe.IsEnabled = false;
                CopyOpe.IsEnabled = false;
                PasteOpe.IsEnabled = false;
                DelOpe.IsEnabled = false;
                SeltAllOpe.IsEnabled = false;
                SeltOpe.IsEnabled = false;
                RandSeltOpe.IsEnabled = false;
            }
            else
            {
                SaveOpe.IsEnabled = true;
                SaveAsOpe.IsEnabled = true;
                SaveAllOpe.IsEnabled = true;
                CloseOpe.IsEnabled = true;
                PrintOpe.IsEnabled = true;
                CutOpe.IsEnabled = true;
                CopyOpe.IsEnabled = true;
                PasteOpe.IsEnabled = true;
                DelOpe.IsEnabled = true;
                SeltAllOpe.IsEnabled = true;
                SeltOpe.IsEnabled = true;
                RandSeltOpe.IsEnabled = true;
            }
        }

        public static RoutedCommand NewProject = new RoutedCommand();
        public static RoutedCommand OpenProject = new RoutedCommand();
        public static RoutedCommand CloseProject = new RoutedCommand();
        public static RoutedCommand Copy = new RoutedCommand();
        public static RoutedCommand Paste = new RoutedCommand();
        public static RoutedCommand Del = new RoutedCommand();
        public static RoutedCommand Save = new RoutedCommand();
        public static RoutedCommand SaveAs = new RoutedCommand();
        public static RoutedCommand SaveAll = new RoutedCommand();
        public static RoutedCommand Print = new RoutedCommand();
        public static RoutedCommand RecentlyUsedProject = new RoutedCommand();
        public static RoutedCommand Exit = new RoutedCommand();
        

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
            openFileDialog.InitialDirectory = Path.GetFullPath(@".\WorkSpace");
            openFileDialog.FilterIndex = 1;
            openFileDialog.ValidateNames = false;
            openFileDialog.CheckFileExists = false;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;
            bool? result = openFileDialog.ShowDialog();
            if (result != true)
            {
                return;
            }
            else
            {
                string path = openFileDialog.FileName;
                try
                {
                    MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
                    //检查是否已经打开
                    foreach (ProjectItem project in mainwin.ProjectList.projects)
                    {
                        if (path == Path.GetFullPath(project.Path + "\\" + project.Name))
                        {
                            MessageBox.Show("工程已经打开！");
                            return;
                        }
                    }

                    FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    byte[] data = new byte[fileStream.Length];
                    fileStream.Read(data, 0, data.Length);
                    fileStream.Close();
                    object obj = CommonFunction.DeserializeWithBinary(data);

                    ((ProjectItem)obj).Num = mainwin.ProjectNum;
                    ((ProjectItem)obj).RebuildLink();
                    mainwin.ProjectList.projects.Add(obj as ProjectItem);
                    mainwin.projectTab.ItemsSource = mainwin.ProjectList.projects;
                    mainwin.projectTab.Items.Refresh();
                    mainwin.projectTab.SelectedItem = obj;
                    mainwin.ProjectNum++;
                }
                catch
                {
                    return;
                }
            }
        }

        #endregion

        #region 关闭工程

        private void CloseProject_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        #endregion

        #region 复制

        private void Copy_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.GetCurrentProject().CopySelection();
            PasteOpe.IsEnabled = true;
        }

        #endregion

        #region 粘贴

        private void Paste_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.GetCurrentProject().PasteSelection(5, 5);
        }

        #endregion

        #region 删除

        private void Del_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.GetCurrentProject().DeleteSelection();
        }

        #endregion

        #region 存储
        private void Save_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            mainwin.GetCurrentProject().SaveProject();
        }
        #endregion

        #region 另存为

        private void SaveAs_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("NewProject");
        }

        #endregion

        #region 全部存储

        private void SaveAll_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            foreach(ProjectItem project in mainwin.ProjectList.projects)
            {
                project.SaveProject();
            }
        }

        #endregion

        #region 打印

        private void Print_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("NewProject");
        }

        #endregion

        #region 最近使用过的工程

        private void RecentlyUsedProject_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("NewProject");
        }

        #endregion

        #region 退出

        private void Exit_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            mainwin.Close();
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

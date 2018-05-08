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
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() == null)
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
        public static RoutedCommand Cut = new RoutedCommand();
        public static RoutedCommand Up = new RoutedCommand();
        public static RoutedCommand Down = new RoutedCommand();
        public static RoutedCommand Left = new RoutedCommand();
        public static RoutedCommand Right = new RoutedCommand();
        public static RoutedCommand DrawGrid = new RoutedCommand();
        public static RoutedCommand Undo = new RoutedCommand();
        public static RoutedCommand Redo = new RoutedCommand();


        #region 新建工程

        public void NewProject_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            Window new_project = new NewProject();
            new_project.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            new_project.ShowDialog();
        }

        #endregion

        #region 打开工程

        public void OpenProject_Excuted(object sender, ExecutedRoutedEventArgs e)
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

        public void CloseProject_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {

            }

        }

        #endregion

        #region 复制

        public void Copy_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().CopySelection();
            }
            //PasteOpe.IsEnabled = true;
        }

        #endregion

        #region 粘贴

        public void Paste_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().PasteSelection(5, 5);
            }
        }

        #endregion

        #region 删除

        public void Del_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().DeleteSelection();
            }
        }

        #endregion

        #region 剪切

        public void Cut_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().CopySelection();
                mainwin.GetCurrentProject().DeleteSelection();
            }
        }

        #endregion

        #region 存储
        public void Save_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().SaveProject();
            }
        }
        #endregion

        #region 另存为

        public void SaveAs_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                MessageBox.Show("NewProject");
            }
        }

        #endregion

        #region 全部存储

        public void SaveAll_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                foreach (ProjectItem project in mainwin.ProjectList.projects)
                {
                    project.SaveProject();
                }
            }
        }

        #endregion

        #region 打印

        public void Print_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                MessageBox.Show("NewProject");
            }
        }

        #endregion

        #region 最近使用过的工程

        public void RecentlyUsedProject_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("NewProject");
        }

        #endregion

        #region 退出

        public void Exit_Excuted(object sender, ExecutedRoutedEventArgs e)
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

        #region 网格

        public void DrawGrid_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().DrawGridSelection();
            }
        }

        #endregion

        #region 上下左右移动

        public void Up_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().MoveSelection(0, -1);
            }
        }

        public void Down_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().MoveSelection(0, 1);
            }
        }

        public void Left_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().MoveSelection(-1, 0);
            }
        }

        public void Right_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().MoveSelection(1, 0);
            }
        }
        #endregion

        #region 撤销 重做
        public void Undo_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().Undo();
            }
        }
        
        public void Redo_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().Redo();
            }
        }
        #endregion
    }
}

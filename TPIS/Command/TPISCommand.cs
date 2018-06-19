using Database;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TPIS.Model;
using TPIS.Project;
using TPIS.Views;

namespace TPIS.Command
{
    public partial class TPISCommand
    {
        public TPISCommand() : base()
        {
            System.Windows.Clipboard.Clear();
        }

        #region 下拉菜单显示
        private void OnSubFileMenuOpened(object sender, RoutedEventArgs e)
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
            }
            else
            {
                SaveOpe.IsEnabled = true;
                SaveAsOpe.IsEnabled = true;
                SaveAllOpe.IsEnabled = true;
                CloseOpe.IsEnabled = true;
                PrintOpe.IsEnabled = true;
            }
        }

        private void OnSubEditMenuOpened(object sender, RoutedEventArgs e)
        {
            MenuItem menuitem = sender as MenuItem;
            menuitem.IsSubmenuOpen = true;
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() == null)
            {
                UndoOpe.IsEnabled = false;
                RedoOpe.IsEnabled = false;
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
                UndoOpe.IsEnabled = true;
                RedoOpe.IsEnabled = true;
                SaveAllOpe.IsEnabled = true;
                CloseOpe.IsEnabled = true;
                PrintOpe.IsEnabled = true;
                CutOpe.IsEnabled = true;
                CopyOpe.IsEnabled = true;
                //PasteOpe.IsEnabled = true;
                DelOpe.IsEnabled = true;
                SeltAllOpe.IsEnabled = true;
                SeltOpe.IsEnabled = true;
                RandSeltOpe.IsEnabled = true;
            }
        }

        private void OnSubViewMenuOpened(object sender, RoutedEventArgs e)
        {
            MenuItem menuitem = sender as MenuItem;
            menuitem.IsSubmenuOpen = true;
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() == null)
            {
                GridOpe.IsEnabled = false;
            }
            else
            {
                GridOpe.IsEnabled = true;
            }
        }

        private void OnSubSetMenuOpened(object sender, RoutedEventArgs e)
        {
            MenuItem menuitem = sender as MenuItem;
            menuitem.IsSubmenuOpen = true;
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() == null)
            {
                WorkspaceSizeOpe.IsEnabled = false;
                BackGroundColorOpe.IsEnabled = false;
                ViewOpe.IsEnabled = false;
            }
            else
            {
                WorkspaceSizeOpe.IsEnabled = true;
                BackGroundColorOpe.IsEnabled = true;
                ViewOpe.IsEnabled = true;
            }
        }
        #endregion

        #region 路由命令
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
        public static RoutedCommand SeltAll = new RoutedCommand();
        public static RoutedCommand WorkspaceSize = new RoutedCommand();
        public static RoutedCommand BackGroundColor = new RoutedCommand();
        public static RoutedCommand QuickModeSelect = new RoutedCommand();
        public static RoutedCommand CoalDataBaseOpe = new RoutedCommand();
        public static RoutedCommand GasDataBaseOpe = new RoutedCommand();
        public static RoutedCommand View = new RoutedCommand();
        #endregion

        #region 新建工程

        public void NewProject_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            Window new_project = new NewProject();
            new_project.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            new_project.ShowDialog();
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
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
                            //MessageBox.Show("工程已经打开！");
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
                    {//解决在无新建工程时打开已有项目，出现的透明背景
                        ((ProjectItem)obj).GridThickness = 0;//赋初值0，使初始画布为隐藏网格
                        ((ProjectItem)obj).GridUintLength = 20;//赋初值20，使初始网格单元为20×20
                        ((ProjectItem)obj).BackGroundColor = mainwin.TPISconfig.CANVAS_BACKGROUNDCOLOR;
                        //((ProjectItem)obj).BackGroundColor = Brushes.White;
                    }
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
                mainwin.GetCurrentProject().ProjectCloseSelection();
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
            //PasteOpe.IsEnabled = true;//先copy后paste
        }

        #endregion

        #region 粘贴

        public void Paste_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().PasteSelection(10, 10);
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
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.Title = "另存为";
                saveFileDialog.DefaultExt = ".tpis";
                saveFileDialog.Filter = "TPIS工程 (.tpis)|*.tpis";
                saveFileDialog.InitialDirectory = Path.GetFullPath(mainwin.GetCurrentProject().Path);
                saveFileDialog.FileName = mainwin.GetCurrentProject().Name;
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.ValidateNames = false;
                saveFileDialog.CheckFileExists = false;
                saveFileDialog.CheckPathExists = true;
                bool? result = saveFileDialog.ShowDialog();
                if (result != true)
                {
                    return;
                }
                else
                {
                    if (mainwin.GetCurrentProject().Name == saveFileDialog.FileName.Substring(saveFileDialog.FileName.LastIndexOf("\\") + 1))
                    {
                        if (MessageBox.Show(saveFileDialog.FileName + "已存在。/n要替换它吗？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            mainwin.GetCurrentProject().SaveProject();
                        }
                    }
                    else
                    {
                        saveFileDialog.RestoreDirectory = true;
                        mainwin.GetCurrentProject().Path = saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.LastIndexOf("\\"));
                        mainwin.GetCurrentProject().Name = saveFileDialog.FileName.Substring(saveFileDialog.FileName.LastIndexOf("\\") + 1);
                        mainwin.GetCurrentProject().SaveProject();
                    }
                }
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
                MessageBox.Show("所有项目已保存");
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
                mainwin.GetCurrentProject().MoveChange(0, -1);
            }
        }

        public void Down_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().MoveChange(0, 1);
            }
        }

        public void Left_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().MoveChange(-1, 0);
            }
        }

        public void Right_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().MoveChange(1, 0);
            }
        }
        #endregion

        #region 全部选择
        public void SeltAll_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                mainwin.GetCurrentProject().SelectAll();
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

        #region 改变工作区大小

        public void WorkspaceSize_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                Window workspaceSize = new WorkspaceSize();
                workspaceSize.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                workspaceSize.ShowDialog();
            }
        }

        #endregion

        #region 背景色

        public void BackGroundColor_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                MenuItem item = (MenuItem)e.OriginalSource;
                switch (item.Tag)
                {
                    case "White":
                        mainwin.GetCurrentProject().BackGroundColor = Brushes.White;
                        //存储工程配置
                        mainwin.TPISconfig.CANVAS_BACKGROUNDCOLOR = mainwin.GetCurrentProject().BackGroundColor;
                        mainwin.TPISconfig.SaveCfg();
                        break;
                    case "LightGray":
                        mainwin.GetCurrentProject().BackGroundColor = Brushes.LightGray;
                        mainwin.TPISconfig.CANVAS_BACKGROUNDCOLOR = mainwin.GetCurrentProject().BackGroundColor;
                        mainwin.TPISconfig.SaveCfg();
                        break;
                }
            }
        }

        #endregion

        #region 计算模式快速选择
        public void QuickModeSelect_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                QuickModeSelect window = new QuickModeSelect(mainwin.GetCurrentProject());
                window.ShowDialog();
            }
        }
        #endregion


        #region 数据库
        public void CoalDataBaseOpe_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            CoalDatabaseWin cdw = new CoalDatabaseWin();
            cdw.Owner = Application.Current.MainWindow;
            cdw.Show();
        }

        public void GasDataBaseOpe_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            GasDatabaseWin gdw = new GasDatabaseWin();
            gdw.Owner = Application.Current.MainWindow;
            gdw.Show();
        }
        #endregion

        #region 视图窗口
        public void View_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                if(!mainwin.GetCurrentProject().IsViewWindowsOpen)
                {
                    mainwin.GetCurrentProject().GetSelectedObjects();
                    ViewWindow viewwin = new ViewWindow();
                    viewwin.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    viewwin.Show();
                    if (mainwin.GetCurrentProject().SelectedObjects != null)
                    {
                        viewwin.ViewCenter();
                        viewwin.RemoveAllAnchorPoints();
                        mainwin.GetCurrentProject().IsViewWindowsOpen = true;
                    }
                }
            }
        }
        #endregion
    }
}
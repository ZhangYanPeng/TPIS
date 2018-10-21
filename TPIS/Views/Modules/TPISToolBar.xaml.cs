using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using TPIS.Model;
using TPIS.Model.Common;
using TPIS.Project;

namespace TPIS.Views.Modules
{
    partial class TPISToolBar : ResourceDictionary
    {

        #region 形变操作
        /// <summary>
        /// 垂直翻转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISVerReversed(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                mainwin.GetCurrentProject().VerticalReversedSelection();
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 水平翻转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISHorReversed(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                mainwin.GetCurrentProject().HorizentalReversedSelection();
            }
            catch
            {
                return;
            }

        }

        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISRotate(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                mainwin.GetCurrentProject().RotateSelection(1);
            }
            catch
            {
                return;
            }
        }
        #endregion

        #region 缩放操作
        /// <summary>
        /// 放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SupRate(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                mainwin.GetCurrentProject().SupRate();
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 缩小
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubRate(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                mainwin.GetCurrentProject().SubRate();
            }
            catch
            {
                return;
            }
        }
        #endregion

        #region 查找操作
        private void FindTargNo(object sender, RoutedEventArgs e)
        {
            String str = TargetNo.Text;
            try
            {
                int tn = int.Parse(str);
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                if (!mainwin.GetCurrentProject().FindComponent(tn))
                {
                    MessageBox.Show("未找到该元件!");
                }
            }
            catch
            {
                MessageBox.Show("请检查输入是否为整数！");
                return;
            }
        }
        #endregion

        private void ToSelectMode(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                mainwin.ToSelectMode();
                AddStraightLine.IsChecked = false;
                //AddLine.IsChecked = false;
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISLineTypeSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton currEle = sender as ToggleButton;
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                if (mainwin.GetCurrentProject() != null)
                {
                    mainwin.TPISLineTypeSelected(sender, e);
                    if (currEle.Name == "AddLine")
                    {
                        AddStraightLine.IsChecked = false;
                        //AddLine.IsChecked = true;
                    }
                    else
                    {
                        //AddLine.IsChecked = false;
                        AddStraightLine.IsChecked = true;
                    }
                }
                else
                {
                    currEle.IsChecked = false;
                }
            }
            catch
            {
                return;
            }
        }

        #region 计算
        private void CalculateResult(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            ProjectItem project = mainwin.GetCurrentProject();
            if(project != null)
            {
                foreach(CalWindow cw in mainwin.CalWins)
                {
                    if (cw.project == project)
                    {
                        cw.Show();
                        return;
                    }
                }
                CalWindow calWindow = new CalWindow(project);
                calWindow.Show();
                mainwin.CalWins.Add(calWindow);
            }
        }
        #endregion

        #region 网格
        private void ChangeGrid(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            ProjectItem project = mainwin.GetCurrentProject();
            if (project != null)
            {
                if(project.GridThickness == 1)
                {
                    project.GridThickness = 0;
                    tsbGrid.IsChecked = false;
                }
                else
                {
                    project.GridThickness = 1;
                    tsbGrid.IsChecked = true;
                }
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            catch (Exception exp)
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
            catch (Exception exp)
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
            catch (Exception exp)
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
            catch (Exception exp)
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
            catch (Exception exp)
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

        #region 计算
        private void CalculateResult(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                ProjectItem project = mainwin.GetCurrentProject();
                //Task t = new Task(() => CalculateCurrent(project));
                //t.Start();
                ProjectItem result = CalculateCurrent(project);
                for (int i = 0; i < mainwin.ProjectList.projects.Count; i++)
                {
                    if (mainwin.ProjectList.projects[i].Num == result.Num)
                    {
                        mainwin.ProjectList.projects[i].Objects = result.Objects;
                    }
                }
            }
            catch
            {
                return;
            }
        }

        public static ProjectItem CalculateCurrent(object data )
        {
            return CalculateInBackEnd.Calculate(data as ProjectItem);
        }

        #endregion
    }
}

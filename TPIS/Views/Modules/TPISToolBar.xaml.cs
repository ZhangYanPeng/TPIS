using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TPIS.Model;

namespace TPIS.Views.Modules
{
    partial class TPISToolBar : ResourceDictionary
    {
        //形变操作
        #region
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

        //缩放操作
        #region
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

        // 查找操作
        #region
        private void FindTargNo(object sender, RoutedEventArgs e)
        {
            String str = TargetNo.Text;
            try{
                int tn = int.Parse(str);
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                if( !mainwin.GetCurrentProject().FindComponent(tn))
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
    }
}

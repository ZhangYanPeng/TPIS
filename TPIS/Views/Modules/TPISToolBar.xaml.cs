using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TPIS.Model;

namespace TPIS.Views.Modules
{
    partial class TPISToolBar : ResourceDictionary
    {
        /// <summary>
        /// 垂直翻转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISVerReversed(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            for (int i = 0; i < mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Objects.Count; i++)
            {
                if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Objects[i] is TPISComponent)
                {
                    if (true)
                    {
                        ((TPISComponent)mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Objects[i]).VerticalReverse();
                    }
                }
            }
        }

        /// <summary>
        /// 水平翻转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISHorReversed(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            for (int i = 0; i < mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Objects.Count; i++)
            {
                if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Objects[i] is TPISComponent)
                {
                    if (true)
                    {
                        ((TPISComponent)mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Objects[i]).HorizentalReverse();
                    }
                }
            }
        }

        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISRotate(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            for (int i = 0; i < mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Objects.Count; i++)
            {
                if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Objects[i] is TPISComponent)
                {
                    if (true)
                    {
                        ((TPISComponent)mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Objects[i]).Rotate(1);
                    }
                }
            }
        }
    }
}

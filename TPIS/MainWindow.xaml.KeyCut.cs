using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.TPISCanvas;

namespace TPIS
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Window快捷键
        /// </summary>
        Command.TPISCommand tPISCommand = new Command.TPISCommand();

        #region 新建工程

        private void NewProject_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.NewProject_Excuted(sender,e);
        }

        #endregion

        #region 打开工程

        private void OpenProject_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.OpenProject_Excuted(sender, e);
        }

        #endregion

        #region 复制

        private void Copy_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.Copy_Excuted(sender, e);
        }

        #endregion

        #region 粘贴

        private void Paste_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.Paste_Excuted(sender, e);
        }

        #endregion

        #region 删除

        public void Del_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.Del_Excuted(sender, e);
        }

        #endregion

        #region 存储
        private void Save_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.Save_Excuted(sender, e);
        }
        #endregion

        #region 全部存储

        private void SaveAll_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.SaveAll_Excuted(sender, e);
        }

        #endregion

        #region 打印

        private void Print_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.Print_Excuted(sender, e);
        }

        #endregion

        #region 退出

        private void Exit_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.Exit_Excuted(sender, e);
        }

        #endregion

        #region 网格

        private void DrawGrid_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.DrawGrid_Excuted(sender, e);
        }

        #endregion

        #region 上下左右移动

        private void Up_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.Up_Excuted(sender, e);
        }

        private void Down_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.Down_Excuted(sender, e);
        }

        private void Left_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.Left_Excuted(sender, e);
        }

        private void Right_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.Right_Excuted(sender, e);
        }

        #endregion

        #region 剪切

        public void Cut_Excuted(object sender, ExecutedRoutedEventArgs e)
        {
            tPISCommand.Cut_Excuted(sender, e);
        }

        #endregion
    }
}

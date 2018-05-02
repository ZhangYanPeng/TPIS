using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
    }
}

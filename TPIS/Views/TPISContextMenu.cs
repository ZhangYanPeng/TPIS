using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TPIS.Views
{
    class TPISContextMenu : ContextMenu
    {
        public int point_x, point_y;
        public void SetPos(Point point)
        {
            point_x = (int)point.X;
            point_y = (int)point.Y;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"> 1: 控件右键 2：画布右键</param>
        public TPISContextMenu(int type)
        {
            MenuItem copyMenuItem = new MenuItem();
            copyMenuItem.Header = "复制";
            MenuItem pasteMenuItem = new MenuItem();
            pasteMenuItem.Header = "粘贴";
            MenuItem deleteMenuItem = new MenuItem();
            deleteMenuItem.Header = "删除";
            copyMenuItem.Click += btCopy_Click;
            pasteMenuItem.Click += btPaste_Click;
            deleteMenuItem.Click += btDel_Click;
            if (type != 2)
            {
                pasteMenuItem.IsEnabled = false;
                copyMenuItem.IsEnabled = true;
                deleteMenuItem.IsEnabled = true;
            }
            if (type != 1 )
            {
                copyMenuItem.IsEnabled = false;
                deleteMenuItem.IsEnabled = false;
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                if(mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].clipBoard.Objects.Count == 0)
                {
                    pasteMenuItem.IsEnabled = false;
                }
                else
                {
                    pasteMenuItem.IsEnabled = true;
                }
            }
            Items.Add(copyMenuItem);
            Items.Add(deleteMenuItem);
            Items.Add(pasteMenuItem);
        }

        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].DeleteSelection();
        }

        private void btPaste_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].PasteSelection(point_x, point_y);
        }

        private void btCopy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].CopySelection();
        }
    }
}

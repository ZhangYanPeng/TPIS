using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TPIS.Model;
using TPIS.Views.ViewWindows;

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

        public TPISLine line;
        public void  setLine(TPISLine l)
        {
            line = l;
            MenuItem addCross = new MenuItem();
            addCross.Header = "插入数据选项";

            addCross.Click += btAddCross_Click;

            Items.Add(new Separator());
            Items.Add(addCross);
        }

        private void btAddCross_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.GetCurrentProject().AddCross(line.InPort, new Point?(new Point(point_x, point_y)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"> 1: 控件右键 2：画布右键</param>
        public TPISContextMenu(int type)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            
            MenuItem copyMenuItem = new MenuItem();
            Image copyImage = new Image();
            copyImage.Source = new BitmapImage(new System.Uri(@"pack://application:,,,/Images/icon/icon_copy.png"));
            copyMenuItem.Header = "复制 Ctrl+C";
            copyMenuItem.Icon = copyImage;

            MenuItem pasteMenuItem = new MenuItem();
            Image pasteImage = new Image();
            pasteImage.Source = new BitmapImage(new System.Uri(@"pack://application:,,,/Images/icon/icon_paste.png"));
            pasteMenuItem.Header = "粘贴 Ctrl+V";
            pasteMenuItem.Icon = pasteImage;

            MenuItem deleteMenuItem = new MenuItem();
            Image deleteImage = new Image();
            deleteImage.Source = new BitmapImage(new System.Uri(@"pack://application:,,,/Images/icon/icon_delete.png"));
            deleteMenuItem.Header = "删除 Del";
            deleteMenuItem.Icon = deleteImage;

            MenuItem cutMenuItem = new MenuItem();
            Image cutImage = new Image();
            cutImage.Source = new BitmapImage(new System.Uri(@"pack://application:,,,/Images/icon/icon_cut.png"));
            cutMenuItem.Header = "剪切 Ctrl+X";
            cutMenuItem.Icon = cutImage;

            MenuItem undoMenuItem = new MenuItem();
            Image undoImage = new Image();
            undoImage.Source = new BitmapImage(new System.Uri(@"pack://application:,,,/Images/icon/icon_undo.png"));
            undoMenuItem.Header = "撤销 Ctrl+Z";
            undoMenuItem.Icon = undoImage;

            MenuItem redoMenuItem = new MenuItem();
            Image redoImage = new Image();
            redoImage.Source = new BitmapImage(new System.Uri(@"pack://application:,,,/Images/icon/icon_redo.png"));
            redoMenuItem.Header = "重做 Ctrl+Y";
            redoMenuItem.Icon = redoImage;

            MenuItem selectAllMenuItem = new MenuItem();
            Image selectImage = new Image();
            selectImage.Source = new BitmapImage(new System.Uri(@"pack://application:,,,/Images/icon/icon_allSelection.png"));
            selectAllMenuItem.Header = "全选 Ctrl+A";
            selectAllMenuItem.Icon = selectImage;

            MenuItem gridMenuItem = new MenuItem();
            gridMenuItem.Header = "网格 Ctrl+G";

            MenuItem portMenuItem = new MenuItem();
            portMenuItem.Header = "元件调整";

            copyMenuItem.Click += btCopy_Click;
            pasteMenuItem.Click += btPaste_Click;
            deleteMenuItem.Click += btDel_Click;
            cutMenuItem.Click += btCut_Click;
            undoMenuItem.Click += btUndo_Click;
            redoMenuItem.Click += btRedo_Click;
            selectAllMenuItem.Click += btSelectAll_Click;
            gridMenuItem.Click += btGrid_Click;
            portMenuItem.Click += btPort_Click;

            selectAllMenuItem.IsEnabled = true;
            gridMenuItem.IsEnabled = true;
            if (type != 2)
            {
                pasteMenuItem.IsEnabled = false;
                copyMenuItem.IsEnabled = true;
                deleteMenuItem.IsEnabled = true;
                cutMenuItem.IsEnabled = true;
                undoMenuItem.IsEnabled = false;
                redoMenuItem.IsEnabled = false;
                portMenuItem.IsEnabled = true;
            }
            if (type != 1)
            {
                copyMenuItem.IsEnabled = false;
                deleteMenuItem.IsEnabled = false;
                cutMenuItem.IsEnabled = false;
                portMenuItem.IsEnabled = false;

                if (mainwin.GetCurrentProject().Records.UndoStack.Count > 0)
                    undoMenuItem.IsEnabled = true;
                else
                    undoMenuItem.IsEnabled = false;

                if (mainwin.GetCurrentProject().Records.RedoStack.Count > 0)
                    redoMenuItem.IsEnabled = true;
                else
                    redoMenuItem.IsEnabled = false;

                if (mainwin.GetCurrentProject().clipBoard.Objects.Count == 0)
                {
                    pasteMenuItem.IsEnabled = false;

                }
                else
                {
                    pasteMenuItem.IsEnabled = true;
                }
            }

            Items.Add(copyMenuItem);
            Items.Add(cutMenuItem);
            Items.Add(deleteMenuItem);
            Items.Add(pasteMenuItem);
            Items.Add(new Separator());
            Items.Add(undoMenuItem);
            Items.Add(redoMenuItem);
            Items.Add(new Separator());
            Items.Add(selectAllMenuItem);
            Items.Add(new Separator());
            Items.Add(gridMenuItem);
            Items.Add(new Separator());
            Items.Add(portMenuItem);

        }

        private void btDel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.GetCurrentProject().DeleteSelection();
        }

        private void btPaste_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.GetCurrentProject().PasteSelection(point_x, point_y);
        }

        private void btCopy_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.GetCurrentProject().CopySelection();
        }

        private void btCut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.GetCurrentProject().CutSelection();
        }

        private void btUndo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.GetCurrentProject().Undo();
        }

        private void btRedo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.GetCurrentProject().Redo();
        }

        private void btSelectAll_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.GetCurrentProject().SelectAll();
        }

        private void btGrid_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.GetCurrentProject().DrawGridSelection();
        }

        private void btPort_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            mainwin.GetCurrentProject().GetSelectedObjects();
            PortSetWindow portSetWin = new PortSetWindow();
            portSetWin.Owner = Application.Current.MainWindow;
            portSetWin.Show();
            mainwin.GetCurrentProject().IsPortSetWindowOpen = true;
        }
    }
}

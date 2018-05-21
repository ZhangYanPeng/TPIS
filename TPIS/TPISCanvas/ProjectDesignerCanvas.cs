using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Command;
using Key = System.Windows.Forms;
using TPIS.Model;
using TPIS.Project;
using TPIS.Views;
using System.Windows.Controls.Primitives;

namespace TPIS.TPISCanvas
{
    public partial class ProjectDesignerCanvas : Canvas
    {
        //private Point? rubberbandSelectionStartPoint = null;

        private SelectionService selectionService;
        internal SelectionService SelectionService
        {
            get
            {
                if (selectionService == null)
                    selectionService = new SelectionService(this);

                return selectionService;
            }
        }

        /// <summary>  
        /// 画布背景  
        /// </summary>  

        public bool gridIsCheck = false;
        public bool GridIsCheck
        {
            get
            {
                return gridIsCheck;
            }
            set
            {
                gridIsCheck = value;
            }
        }

        /// <summary>
        /// 初始化函数，添加画布事件
        /// 进入离开画布形状事件
        /// </summary>
        public ProjectDesignerCanvas() : base()
        {
            base.MouseEnter += new MouseEventHandler(CanvasMouseEnter);
            base.MouseLeave += new MouseEventHandler(CanvasMouseLeave);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(ComponentMouseLButtonDown);
            base.MouseMove += new MouseEventHandler(ComponentMouseMove);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(MouseLBtnClickEmpty);
            base.MouseMove += new MouseEventHandler(MouseLBtnSelectMove);
            base.MouseRightButtonDown += new MouseButtonEventHandler(MouseCanvasRightButtonDown);
            base.MouseWheel += new MouseWheelEventHandler(Canvas_MouseWheel);

            pline = new Polyline();
            pline.Stroke = Brushes.Red;
            pline.StrokeThickness = 2;
            this.Children.Add(pline);

            mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.Canvas_MousePosition("0", "0");//状态栏显示工作区鼠标坐标
        }

        #region 滚轮操作
        public void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            ProjectItem item = mainwin.GetCurrentProject();
            //Ctrl+滚轮横纵等大小改变工作区大小
            if (Key.Control.ModifierKeys == Key.Keys.Control)
            {
                ScrollViewer scrollViewer = new ScrollViewer();
                scrollViewer = (ScrollViewer)this.Parent;
                //禁止窗口滑动，显示当前工作区域
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                if (e.Delta > 0)
                {
                    item.Canvas.Width = item.Canvas.Width + 10;
                    item.Canvas.Height = item.Canvas.Height + 10;
                }
                //缩小画布时，不能小于控件工作区边界
                else if (e.Delta < 0)
                {
                    if (item.Canvas.Width > 10 && item.Canvas.Height > 10)//画布最小10×10
                    {
                        if (item.WorkSpaceSize_RD().X <= item.Canvas.Width - 10)
                            item.Canvas.Width = item.Canvas.Width - 10;
                        if (item.WorkSpaceSize_RD().Y <= item.Canvas.Height - 10)
                            item.Canvas.Height = item.Canvas.Height - 10;
                    }
                }
                mainwin.CurWorkspaceSizeShow(item.Canvas.Width.ToString(), item.Canvas.Height.ToString());//状态栏显示工作区大小
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
            //Ctrl+滚轮实现工作区缩放(视图上的缩放，真实大小未变)
            //if (Key.Control.ModifierKeys == Key.Keys.Control)
            //{
            //    if (e.Delta > 0)
            //    {
            //        item.SupRate();
            //    }
            //    else
            //    {
            //        item.SubRate();
            //    }
            //    mainwin.CurWorkspaceSizeShow(item.Canvas.Width.ToString(), item.Canvas.Height.ToString());//状态栏显示工作区大小
            //}
        }
        #endregion


        #region 控件拖动放大工作区
        public void ChangeWorkSpaceSize()
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            ProjectItem item = mainwin.GetCurrentProject();
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer = (ScrollViewer)this.Parent;
            Point p = new Point();
            p = item.WorkSpaceSize_RD();
            if (p.X >= item.Canvas.Width)
            {
                item.Canvas.Width = (int)p.X + 10;
                scrollViewer.ScrollToRightEnd();
            }
            if (p.Y >= item.Canvas.Height)
            {
                item.Canvas.Height = (int)p.Y + 10;
                scrollViewer.ScrollToBottom();
            }
            mainwin.CurWorkspaceSizeShow(item.Canvas.Width.ToString(), item.Canvas.Height.ToString());//状态栏显示工作区大小
        }
        #endregion
        
        public void MouseCanvasRightButtonDown(object sender, MouseEventArgs e)
        {
            if (mainwin.GetCurrentProject().Canvas.Operation == OperationType.ADD_LINE)
            {
                flag = false;
                pline.Points.Clear();
            }

            if (mainwin.GetCurrentProject().Canvas.Operation != OperationType.SELECT)
            {
                this.Cursor = Cursors.Arrow;
                mainwin.ToSelectMode();
                this.Children.Remove(AddComponentImage);
                return;
            }
            mainwin.GetCurrentProject().Select();
            TPISContextMenu contextMenu = new TPISContextMenu(2);
            contextMenu.SetPos(e.GetPosition(this));
            ContextMenu = contextMenu;
            e.Handled = true;
        }
    }
}

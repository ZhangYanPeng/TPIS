using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Command;
using Key = System.Windows.Forms;
using TPIS.Project;
using TPIS.Views;

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
            base.MouseWheel += new MouseWheelEventHandler(Canvas_MouseWheelChangeSize);

            {//绘制中newTPISLine
                pline = new Polyline();
                pline.Stroke = Brushes.Red;
                pline.StrokeThickness = 2;
                this.Children.Add(pline);
            }

            mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.Canvas_MousePosition("0", "0");//状态栏显示工作区鼠标坐标

            Focusable = true;
        }

        #region 滚轮操作
        public void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            ProjectItem item = mainwin.GetCurrentProject();
            //Ctrl+滚轮横纵等大小改变工作区大小
            if (Key.Control.ModifierKeys == Key.Keys.Shift)
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
                        if (item.WorkSpaceSize_RD(mainwin.GetCurrentProject().Objects).X <= item.Canvas.Width - 10)
                            item.Canvas.Width = item.Canvas.Width - 10;
                        if (item.WorkSpaceSize_RD(mainwin.GetCurrentProject().Objects).Y <= item.Canvas.Height - 10)
                            item.Canvas.Height = item.Canvas.Height - 10;
                    }
                }
                mainwin.CurWorkspaceSizeShow(item.Canvas.Width.ToString(), item.Canvas.Height.ToString());//状态栏显示工作区大小
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
        }

        public void Canvas_MouseWheelChangeSize(object sender, MouseWheelEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            ProjectItem item = mainwin.GetCurrentProject();
            Point p = e.GetPosition(this);
            //Control+滚轮缩放
            if (Key.Control.ModifierKeys == Key.Keys.Control)
            {
                //记录原点
                if (p.X < 0)
                    p.X = 0;
                if (p.Y < 0)
                    p.Y = 0;
                if (p.X > mainwin.GetCurrentProject().Canvas.V_width)
                    p.X = mainwin.GetCurrentProject().Canvas.V_width;
                if (p.Y > mainwin.GetCurrentProject().Canvas.V_height)
                    p.Y = mainwin.GetCurrentProject().Canvas.V_height;
                p.X = p.X / mainwin.GetCurrentProject().Rate;
                p.Y = p.Y / mainwin.GetCurrentProject().Rate;
                ScrollContentPresenter scp = (ScrollContentPresenter)VisualTreeHelper.GetParent(this);
                Grid g = (Grid)VisualTreeHelper.GetParent(scp);
                ScrollViewer sv = (ScrollViewer)VisualTreeHelper.GetParent(g);
                Point lup = e.GetPosition(sv);

                //缩放
                if (e.Delta > 0)
                    try
                    {
                        mainwin.GetCurrentProject().SupRate();
                    }
                    catch
                    {
                        return;
                    }
                else if (e.Delta < 0)
                    try
                    {
                        mainwin.GetCurrentProject().SubRate();
                    }
                    catch
                    {
                        return;
                    }

                //重新计算点
                p.X = p.X * mainwin.GetCurrentProject().Rate - lup.X;
                p.Y = p.Y * mainwin.GetCurrentProject().Rate - lup.Y;
                sv.ScrollToHorizontalOffset(p.X);
                sv.ScrollToVerticalOffset(p.Y);

                mainwin.CurWorkspaceSizeShow(item.Canvas.Width.ToString(), item.Canvas.Height.ToString());//状态栏显示工作区大小
            }
        }
        #endregion

        public void MouseCanvasRightButtonDown(object sender, MouseEventArgs e)
        {
            if (mainwin.GetCurrentProject().Canvas.Operation == OperationType.ADD_LINE)
            {
                flag = false;
                pline.Points.Clear();
                mainwin.GetCurrentProject().Canvas.CanLink = false;//右键取消画线问题
                this.Cursor = Cursors.Arrow;
                mainwin.GetCurrentProject().Select();
                mainwin.ToSelectMode();
                ContextMenu = null;
            }
            else if (mainwin.GetCurrentProject().Canvas.Operation != OperationType.SELECT)
            {
                this.Cursor = Cursors.Arrow;
                this.Children.Remove(AddComponentImage);
                mainwin.GetCurrentProject().Select();
                mainwin.ToSelectMode();
                ContextMenu = null;
            }
            else if(mainwin.GetCurrentProject().Canvas.Operation == OperationType.SELECT)
            {
                mainwin.GetCurrentProject().Select();
                TPISContextMenu contextMenu = new TPISContextMenu(2);
                contextMenu.SetPos(e.GetPosition(this));
                ContextMenu = contextMenu;
            }
            e.Handled = true;
        }
    }
}

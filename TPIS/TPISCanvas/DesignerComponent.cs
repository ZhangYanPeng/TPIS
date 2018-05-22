using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TPIS.Model;
using TPIS.Project;
using TPIS.Views;

namespace TPIS.TPISCanvas
{
    public enum MoveType
    {
        pos,
        size
    }

    partial class DesignerComponent : Canvas
    {

        bool isDragDropInEffect = false;
        public MoveType moveType;

        Point pos = new Point();

        AnchorPointType sizeType;

        public DesignerComponent()
        {
            base.MouseMove += new MouseEventHandler(Element_MouseMove);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(Element_MouseLeftButtonDown);
            base.MouseLeftButtonUp += new MouseButtonEventHandler(Element_MouseLeftButtonUp);
            base.MouseRightButtonDown += new MouseButtonEventHandler(Element_MouseRightButtonDown);
            base.Loaded += new RoutedEventHandler(InitAnchorPoints);
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragDropInEffect)
            {
                FrameworkElement currEle = sender as FrameworkElement;
                Point tmp = e.GetPosition(null);
                double x = tmp.X - pos.X;
                double y = tmp.Y - pos.Y;
                pos = tmp;
                //移动位置
                if (this.moveType == MoveType.pos)
                {
                    MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                    mainwin.GetCurrentProject().MoveChange((int)x, (int)y, sender);
                }

                //改变大小
                if (this.moveType == MoveType.size)
                {
                    int no = ((TPISComponent)currEle.DataContext).No;
                    MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                    if (this.sizeType == AnchorPointType.D || this.sizeType == AnchorPointType.DL || this.sizeType == AnchorPointType.DR)
                    {
                        mainwin.GetCurrentProject().SizeChange(no, null, y, null, null);
                    }
                    if (this.sizeType == AnchorPointType.UR || this.sizeType == AnchorPointType.R || this.sizeType == AnchorPointType.DR)
                    {
                        mainwin.GetCurrentProject().SizeChange(no, x, null, null, null);
                    }
                    if (this.sizeType == AnchorPointType.UL || this.sizeType == AnchorPointType.L || this.sizeType == AnchorPointType.DL)
                    {
                        mainwin.GetCurrentProject().SizeChange(no, -x, null, x, null);
                    }
                    if (this.sizeType == AnchorPointType.UL || this.sizeType == AnchorPointType.U || this.sizeType == AnchorPointType.UR)
                    {
                        mainwin.GetCurrentProject().SizeChange(no, null, -y, null, y);
                    }
                }
            }
        }

        void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (e.ClickCount == 2 && DataContext is TPISComponent)
            {
                //双击时执行
                PropertyWindow propertyWindow = new PropertyWindow((TPISComponent)this.DataContext);
                mainwin.PropertyWindowVisible(true);
                mainwin.ResultWindowVisible(true);
                propertyWindow.Show();
            }

            if (mainwin.GetCurrentProject().Canvas.Operation != Project.OperationType.SELECT)
            {
                return;
            }
            FrameworkElement fEle = sender as FrameworkElement;
            pos = e.GetPosition(null);

            this.moveType = MoveType.pos;
            if (this.DataContext is TPISComponent && ((TPISComponent)DataContext).IsSelected)
            {
                //已被选中，不改变选择范围
                foreach (UIElement uie in this.Children)
                {
                    if (uie is AnchorPoint)
                    {
                        AnchorPoint ap = uie as AnchorPoint;
                        if (ap.IsMouseOver)
                        {
                            this.moveType = MoveType.size;
                            this.sizeType = ap.Type;
                            switch (ap.Type)
                            {
                                case AnchorPointType.U:
                                case AnchorPointType.D: fEle.Cursor = Cursors.SizeNS; break;
                                case AnchorPointType.L:
                                case AnchorPointType.R: fEle.Cursor = Cursors.SizeWE; break;
                                case AnchorPointType.UL:
                                case AnchorPointType.DR: fEle.Cursor = Cursors.SizeNWSE; break;
                                case AnchorPointType.UR:
                                case AnchorPointType.DL: fEle.Cursor = Cursors.SizeNESW; break;
                            }
                            Mouse.OverrideCursor = null;
                            break;
                        }
                    }
                }
            }
            if (!((ObjectBase)this.DataContext).isSelected || this.moveType != MoveType.pos)
            {
                //之前未被选中，或改为改变大小操作，单独选中该元件
                mainwin.GetCurrentProject().Select((ObjectBase)this.DataContext);
                if (this.DataContext is TPISComponent)
                    BindingAnchorPoints();
            }
            if (this.moveType == MoveType.pos)
                fEle.Cursor = Cursors.SizeAll;
            fEle.CaptureMouse();
            isDragDropInEffect = true;
            e.Handled = true;
        }

        void Element_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.GetCurrentProject().Canvas.Operation != Project.OperationType.SELECT)
            {
                return;
            }
            if (((TPISComponent)this.DataContext).IsSelected)
            {
                //已被选中，不改变选择范围
                ContextMenu contextMenu = new TPISContextMenu(1);
                this.ContextMenu = contextMenu;
            }
            if (!((TPISComponent)this.DataContext).IsSelected || this.moveType != MoveType.pos)
            {
                //之前未被选中，或改为改变大小操作，单独选中该元件
                mainwin.GetCurrentProject().Select((TPISComponent)this.DataContext);
                BindingAnchorPoints();
                ContextMenu contextMenu = new TPISContextMenu(1);
                this.ContextMenu = contextMenu;
            }
            isDragDropInEffect = false;
            e.Handled = true;
        }

        void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragDropInEffect)
            {
                FrameworkElement ele = sender as FrameworkElement;
                isDragDropInEffect = false;
                ele.ReleaseMouseCapture();
                ele.Cursor = Cursors.Arrow;
            }
        }
    }
}
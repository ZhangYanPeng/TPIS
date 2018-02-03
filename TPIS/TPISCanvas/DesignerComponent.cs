using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TPIS.Model;
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
                    mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].MoveSelection((int)x, (int)y);
                }

                //改变大小
                if (this.moveType == MoveType.size)
                {
                    if (this.sizeType == AnchorPointType.D || this.sizeType == AnchorPointType.DL || this.sizeType == AnchorPointType.DR)
                    {
                        ((TPISComponent)currEle.DataContext).SizeChange(null, (int)y);
                    }
                    if (this.sizeType == AnchorPointType.UR || this.sizeType == AnchorPointType.R || this.sizeType == AnchorPointType.DR)
                    {
                        ((TPISComponent)currEle.DataContext).SizeChange((int)x, null);
                    }
                    if (this.sizeType == AnchorPointType.UL || this.sizeType == AnchorPointType.L || this.sizeType == AnchorPointType.DL)
                    {
                        ((TPISComponent)currEle.DataContext).SizeChange(-(int)x, null);
                        ((TPISComponent)currEle.DataContext).PosChange((int)x, null);
                    }
                    if (this.sizeType == AnchorPointType.UL || this.sizeType == AnchorPointType.U || this.sizeType == AnchorPointType.UR)
                    {
                        ((TPISComponent)currEle.DataContext).SizeChange(null, -(int)y);
                        ((TPISComponent)currEle.DataContext).PosChange(null, (int)y);
                    }
                }
            }
        }

        void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                //双击时执行
                PropertyWindow propertyWindow = new PropertyWindow((TPISComponent)this.DataContext);
                propertyWindow.Show();
            }

            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.Operation != Project.OperationType.SELECT)
            {
                return;
            }
            FrameworkElement fEle = sender as FrameworkElement;
            pos = e.GetPosition(null);

            this.moveType = MoveType.pos;
            if (((TPISComponent)this.DataContext).IsSelected )
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
            if ( !((TPISComponent)this.DataContext).IsSelected || this.moveType != MoveType.pos)
            {
                //之前未被选中，或改为改变大小操作，单独选中该元件
                mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Select((TPISComponent)this.DataContext);
                BindingAnchorPoints();
            }
            if (this.moveType == MoveType.pos)
                fEle.Cursor = Cursors.SizeAll;
            fEle.CaptureMouse();
            isDragDropInEffect = true;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TPIS.Model;

namespace TPIS.TPISCanvas
{
    public enum MoveType
    {
        pos,
        size
    }

    partial class DesignerComponent : Canvas, ISelectable
    {

        bool isDragDropInEffect = false;
        public MoveType moveType;

        Point pos = new Point();
        Point location = new Point();

        AnchorPointType sizeType;
        int cheight;
        int cwidth;

        #region
        public bool IsSelected { get; set; }
        #endregion

        public DesignerComponent()
        {
            base.MouseMove += new MouseEventHandler(Element_MouseMove);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(Element_MouseLeftButtonDown);
            base.MouseLeftButtonUp += new MouseButtonEventHandler(Element_MouseLeftButtonUp);

        }

        /// <summary>
        /// 元件整体移动
        /// </summary>
        /// <param name="x">横向</param>
        /// <param name="y">纵向</param>
        public void Element_Move(int x, int y)
        {
            double xPos = x + location.X;
            double yPos = y + location.Y;
            ((TPISComponent)this.DataContext).Position.V_x = (int)xPos;
            ((TPISComponent)this.DataContext).Position.V_y = (int)yPos;
            return;
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
                double x = e.GetPosition(null).X - pos.X;
                double y = e.GetPosition(null).Y - pos.Y;
                //移动位置
                if (this.moveType == MoveType.pos)
                {
                    this.Element_Move((int)x, (int)y);

                }

                //改变大小
                if (this.moveType == MoveType.size)
                {
                    if (this.sizeType == AnchorPointType.D || this.sizeType == AnchorPointType.DL || this.sizeType == AnchorPointType.DR)
                    {
                        ((TPISComponent)currEle.DataContext).SizeChange(this.cwidth, null, this.cheight, (int)y);
                    }
                    if (this.sizeType == AnchorPointType.UR || this.sizeType == AnchorPointType.R || this.sizeType == AnchorPointType.DR)
                    {
                        ((TPISComponent)currEle.DataContext).SizeChange(this.cwidth, (int)x, this.cheight, null);
                    }
                    if (this.sizeType == AnchorPointType.UL || this.sizeType == AnchorPointType.L || this.sizeType == AnchorPointType.DL)
                    {
                        ((TPISComponent)currEle.DataContext).SizeChange(this.cwidth, -(int)x, this.cheight, null);
                        double xPos = x + location.X;
                        ((TPISComponent)currEle.DataContext).Position.V_x = (int)xPos;
                    }
                    if (this.sizeType == AnchorPointType.UL || this.sizeType == AnchorPointType.U || this.sizeType == AnchorPointType.UR)
                    {
                        ((TPISComponent)currEle.DataContext).SizeChange(this.cwidth, null, this.cheight, -(int)y);
                        double yPos = y + location.Y;
                        ((TPISComponent)currEle.DataContext).Position.V_y = (int)yPos;
                    }
                    RePosAnchorPoints();
                }
                ((TPISComponent)currEle.DataContext).RePos();
            }
        }

        void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.Operation != Project.OperationType.SELECT)
            {
                return;
            }
            FrameworkElement fEle = sender as FrameworkElement;
            pos = e.GetPosition(null);

            this.moveType = MoveType.pos;
            if (this.IsSelected == true)
            {
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
            if (this.moveType == MoveType.pos)
                fEle.Cursor = Cursors.SizeAll;
            fEle.CaptureMouse();

            Component_Select();
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
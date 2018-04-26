using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TPIS.Model;

namespace TPIS.TPISCanvas
{
    public partial class ProjectDesignerCanvas : Canvas
    {
        public Point? AddComponentStartPoint { get; set; }
        private Image AddComponentImage { get; set; }

        /// <summary>
        /// 判定画布内鼠标形状
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CanvasMouseEnter(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            try
            {
                if (mainwin.GetCurrentProject().Canvas.Operation != Project.OperationType.SELECT)
                {
                    this.Cursor = Cursors.Cross;

                    if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.ADD_COMPONENT)
                    {
                        int type = mainwin.GetCurrentProject().Canvas.OperationParam["type"];
                        ComponentType targetType = null;
                        foreach (BaseType bt in mainwin.TypeList)
                        {
                            foreach (ComponentType ct in bt.ComponentTypeList)
                            {
                                if (type == ct.Id)
                                {
                                    targetType = ct;
                                    break;
                                }
                            }
                        }
                        AddComponentImage = new Image();
                        AddComponentImage.Source = new BitmapImage(new Uri(targetType.PicPath, UriKind.RelativeOrAbsolute));
                        AddComponentImage.Width = targetType.Width;
                        AddComponentImage.Height = targetType.Height;
                        Children.Add(AddComponentImage);
                    }
                }
                else
                    this.Cursor = Cursors.Arrow;
            }
            catch
            {
                this.Cursor = Cursors.Cross;
            }
        }

        /// <summary>
        /// 离开画布恢复形状
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CanvasMouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            Children.Remove(AddComponentImage);
        }

        public void ComponentMouseLButtonDown(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.ADD_COMPONENT)
            {
                // in case that this click is the start of a drag operation we cache the start point
                this.AddComponentStartPoint = new Point?(e.GetPosition(this));

                // if you click directly on the canvas all selected items are 'de-selected'
                Focus();
                e.Handled = true;
            }

        }

        public void ComponentMouseLButtonUp(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.ADD_COMPONENT)
            {
                // in case that this click is the start of a drag operation we cache the start point
                Point? AddComponentEndPoint = new Point?(e.GetPosition(this));

                if (AddComponentStartPoint.HasValue && AddComponentEndPoint.HasValue)
                {
                    Point sp = AddComponentStartPoint.Value;
                    Point ep = AddComponentEndPoint.Value;
                    if (Math.Abs(ep.X - sp.X) < 5 && Math.Abs(ep.Y - sp.Y) < 5)
                    {

                        //添加元件
                        int type = mainwin.GetCurrentProject().Canvas.OperationParam["type"];
                        ComponentType targetType = null;
                        foreach (BaseType bt in mainwin.TypeList)
                        {
                            foreach (ComponentType ct in bt.ComponentTypeList)
                            {
                                if (type == ct.Id)
                                {
                                    targetType = ct;
                                    break;
                                }
                            }
                        }
                        mainwin.GetCurrentProject().AddComponent((int)sp.X, (int)sp.Y, targetType.Width, targetType.Height, targetType);
                    }
                }

                e.Handled = true;
            }

        }

        protected void ComponentMouseMove(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.ADD_COMPONENT)
            {

                if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.ADD_COMPONENT)
                {
                    Point pos = e.GetPosition(this);
                    AddComponentImage.SetValue(Canvas.LeftProperty, pos.X);
                    AddComponentImage.SetValue(Canvas.TopProperty, pos.Y);
                }

                // if mouse button is not pressed we have no drag operation, ...
                if (e.LeftButton != MouseButtonState.Pressed)
                    this.AddComponentStartPoint = null;

                // ... but if mouse button is pressed and start
                // point value is set we do have one
                if (this.AddComponentStartPoint.HasValue)
                {
                    // create rubberband adorner
                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                    if (adornerLayer != null)
                    {
                        int type = mainwin.GetCurrentProject().Canvas.OperationParam["type"];
                        ComponentType targetType = null;
                        foreach (BaseType bt in mainwin.TypeList)
                        {
                            foreach (ComponentType ct in bt.ComponentTypeList)
                            {
                                if (type == ct.Id)
                                {
                                    targetType = ct;
                                    break;
                                }
                            }
                        }
                        AddComponentAdorner adorner = new AddComponentAdorner(this, AddComponentStartPoint, targetType);
                        if (adorner != null)
                        {
                            adornerLayer.Add(adorner);
                        }
                    }
                }
                e.Handled = true;
            }
        }
    }
}

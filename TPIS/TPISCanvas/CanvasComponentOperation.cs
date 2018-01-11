using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using TPIS.Model;

namespace TPIS.TPISCanvas
{
    public partial class ProjectDesignerCanvas : Canvas
    {
        public Point? AddComponentStartPoint { get; set; }

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
        }

        /// <summary>
        /// 判定画布内鼠标形状
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CanvasMouseEnter(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.Operation != Project.OperationType.SELECT)
                this.Cursor = Cursors.Cross;
            else
                this.Cursor = Cursors.Arrow;
        }

        /// <summary>
        /// 离开画布恢复形状
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CanvasMouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        public void ComponentMouseLButtonDown(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.Operation == Project.OperationType.ADD_COMPONENT)
            {
                if (e.Source == this)
                {
                    // in case that this click is the start of a drag operation we cache the start point
                    this.AddComponentStartPoint = new Point?(e.GetPosition(this));

                    // if you click directly on the canvas all selected items are 'de-selected'
                    Focus();
                    e.Handled = true;
                }
            }
            
        }

        protected void ComponentMouseMove(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.Operation == Project.OperationType.ADD_COMPONENT)
            {
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
                        int type = mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.OperationParam["type"];
                        ComponentType targetType = null;
                        foreach (BaseType bt in mainwin.TypeList)
                        {
                            foreach (ComponentType ct in bt.ComponentTypeList)
                            {
                                if( type == ct.Id)
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

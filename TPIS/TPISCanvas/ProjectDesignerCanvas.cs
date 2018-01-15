using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using TPIS.Command;

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

        //protected override void OnMouseDown(MouseButtonEventArgs e)
        //{
        //    base.OnMouseDown(e);
        //    if (e.Source == this)
        //    {
        //        // in case that this click is the start of a 
        //        // drag operation we cache the start point
        //        this.rubberbandSelectionStartPoint = new Point?(e.GetPosition(this));

        //        // if you click directly on the canvas all 
        //        // selected items are 'de-selected'
        //        SelectionService.ClearSelection();
        //        Focus();
        //        e.Handled = true;
        //    }
        //}

        //protected override void OnMouseMove(MouseEventArgs e)
        //{
        //    base.OnMouseMove(e);

        //    // if mouse button is not pressed we have no drag operation, ...
        //    if (e.LeftButton != MouseButtonState.Pressed)
        //        this.rubberbandSelectionStartPoint = null;

        //    // ... but if mouse button is pressed and start
        //    // point value is set we do have one
        //    if (this.rubberbandSelectionStartPoint.HasValue)
        //    {
        //        // create rubberband adorner
        //        AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
        //        if (adornerLayer != null)
        //        {
        //            RubberbandAdorner adorner = new RubberbandAdorner(this, rubberbandSelectionStartPoint);
        //            if (adorner != null)
        //            {
        //                adornerLayer.Add(adorner);
        //            }
        //        }
        //    }
        //    e.Handled = true;
        //}

        //protected override void OnDrop(DragEventArgs e)
        //{
        //    base.OnDrop(e);
        //    DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;
        //    if (dragObject != null && !String.IsNullOrEmpty(dragObject.Xaml))
        //    {
        //        ProjectDesignerItem newItem = null;
        //        Object content = XamlReader.Load(XmlReader.Create(new StringReader(dragObject.Xaml)));

        //        if (content != null)
        //        {
        //            newItem = new ProjectDesignerItem();
        //            newItem.Content = content;

        //            Point position = e.GetPosition(this);

        //            if (dragObject.DesiredSize.HasValue)
        //            {
        //                Size desiredSize = dragObject.DesiredSize.Value;
        //                newItem.Width = desiredSize.Width;
        //                newItem.Height = desiredSize.Height;

        //                ProjectDesignerCanvas.SetLeft(newItem, Math.Max(0, position.X - newItem.Width / 2));
        //                ProjectDesignerCanvas.SetTop(newItem, Math.Max(0, position.Y - newItem.Height / 2));
        //            }
        //            else
        //            {
        //                ProjectDesignerCanvas.SetLeft(newItem, Math.Max(0, position.X));
        //                ProjectDesignerCanvas.SetTop(newItem, Math.Max(0, position.Y));
        //            }

        //            Canvas.SetZIndex(newItem, this.Children.Count);
        //            this.Children.Add(newItem);
        //            SetConnectorDecoratorTemplate(newItem);

        //            //update selection
        //            //this.SelectionService.SelectItem(newItem);
        //            newItem.Focus();
        //        }

        //        e.Handled = true;
        //    }
        //}

        //protected override Size MeasureOverride(Size constraint)
        //{
        //    Size size = new Size();

        //    foreach (UIElement element in this.InternalChildren)
        //    {
        //        double left = Canvas.GetLeft(element);
        //        double top = Canvas.GetTop(element);
        //        left = double.IsNaN(left) ? 0 : left;
        //        top = double.IsNaN(top) ? 0 : top;

        //        //measure desired size for each child
        //        element.Measure(constraint);

        //        Size desiredSize = element.DesiredSize;
        //        if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
        //        {
        //            size.Width = Math.Max(size.Width, left + desiredSize.Width);
        //            size.Height = Math.Max(size.Height, top + desiredSize.Height);
        //        }
        //    }
        //    // add margin 
        //    size.Width += 10;
        //    size.Height += 10;
        //    return size;
        //}

        //private void SetConnectorDecoratorTemplate(ProjectDesignerItem item)
        //{
        //    if (item.ApplyTemplate() && item.Content is UIElement)
        //    {
        //        ControlTemplate template = ProjectDesignerItem.GetConnectorDecoratorTemplate(item.Content as UIElement);
        //        Control decorator = item.Template.FindName("PART_ConnectorDecorator", item) as Control;
        //        if (decorator != null && template != null)
        //            decorator.Template = template;
        //    }
        //}


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

            checkBox = new CheckBox();
            checkBox.Content = "画线";
            this.Children.Add(checkBox);

            plines = new List<Polyline>();
        }
        

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            base.MouseMove += new MouseEventHandler(Canvas_MouseMove);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(Canvas_MouseLeftButtonDown);
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            base.MouseRightButtonDown += new MouseButtonEventHandler(Canvas_MouseRightButtonDown);
        }

        /// <summary>
        /// 画折线
        /// </summary>
        /// 

        bool flag = false;
        public CheckBox checkBox;//是否画线
        public List<Polyline> plines;//画多条折线
        private int count = -1;

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == false)
                return;
            plines[count].Points[plines[count].Points.Count - 1] = e.GetPosition(this);
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (checkBox.IsChecked==true)
            {
                if (flag == false)
                {
                    flag = true;//开始画线
                    count++;//只有首击左键时计数增1
                    Polyline pline = new Polyline();
                    pline.Stroke = Brushes.Black;
                    pline.StrokeThickness = 2;
                    this.Children.Add(pline);
                    plines.Add(pline);
                }
                plines[count].Points.Add(e.GetPosition(this));
                if (plines[count].Points.Count == 1)
                    plines[count].Points.Add(e.GetPosition(this));                
            }
        }

        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            flag = false;//结束画线
        }
    }
}

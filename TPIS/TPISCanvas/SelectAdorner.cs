using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TPIS.Model;
using TPIS.Project;
using TPIS.TPISCanvas;

namespace TPIS.TPISCanvas
{
    public class SelectAdorner : Adorner
    {
        private Point? startPoint;
        private Point? endPoint;
        private Pen rubberbandPen;

        private ProjectDesignerCanvas designerCanvas;

        public SelectAdorner(ProjectDesignerCanvas designerCanvas, Point? dragStartPoint) : base(designerCanvas)
        {
            this.designerCanvas = designerCanvas;
            this.startPoint = dragStartPoint;
            rubberbandPen = new Pen(Brushes.LightSlateGray, 1);
            rubberbandPen.DashStyle = new DashStyle(new double[] { 2 }, 1);
        }

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured)
                    this.CaptureMouse();
                endPoint = e.GetPosition(this);
                
                UpdateSelection();

                this.InvalidateVisual();
            }
            else
            {
                if (this.IsMouseCaptured) this.ReleaseMouseCapture();
            }
            e.Handled = true;
        }

        protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            // release mouse capture
            if (this.IsMouseCaptured) this.ReleaseMouseCapture();

            // remove this adorner from adorner layer
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.designerCanvas);
            if (adornerLayer != null)
                adornerLayer.Remove(this);
            e.Handled = true;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            // without a background the OnMouseMove event would not be fired!
            // Alternative: implement a Canvas as a child of this adorner, like
            // the ConnectionAdorner does.
            dc.DrawRectangle(Brushes.Transparent, rubberbandPen, new Rect(RenderSize));

            if (this.startPoint.HasValue && this.endPoint.HasValue)
                dc.DrawRectangle(Brushes.Transparent, rubberbandPen, new Rect(startPoint.Value,endPoint.Value));
        }

        private void UpdateSelection()
        {
            List<TPISComponent> selection = new List<TPISComponent>();
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;

            Rect rubberBand = new Rect(startPoint.Value, endPoint.Value);
            foreach (ObjectBase obj in mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Objects)
            {
                if(obj is TPISComponent)
                {
                    if (((TPISComponent)obj).Position.V_x > startPoint.Value.X && ((TPISComponent)obj).Position.V_y > startPoint.Value.Y
                        && ((TPISComponent)obj).Position.V_x + ((TPISComponent)obj).Position.V_width < endPoint.Value.X
                        && ((TPISComponent)obj).Position.V_y + ((TPISComponent)obj).Position.V_height < endPoint.Value.Y)
                        selection.Add((TPISComponent)obj);
                }
            }
            mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Select(selection);
        }
    }
    
}

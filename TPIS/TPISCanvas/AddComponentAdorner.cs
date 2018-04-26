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
using TPIS.TPISCanvas;

namespace TPIS.TPISCanvas
{
    public class AddComponentAdorner : Adorner
    {
        private Point? startPoint;
        private Point? endPoint;
        private Pen rubberbandPen;
        private ComponentType targetType;

        private ProjectDesignerCanvas designerCanvas;

        public AddComponentAdorner(ProjectDesignerCanvas designerCanvas, Point? dragStartPoint, ComponentType ct) : base(designerCanvas)
        {
            this.designerCanvas = designerCanvas;
            this.startPoint = dragStartPoint;
            this.targetType = ct;
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
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.ADD_COMPONENT)
            {
                if (startPoint.HasValue && endPoint.HasValue)
                {
                    Point sp = startPoint.Value;
                    Point ep = endPoint.Value;
                    int width = (int)(ep.X - sp.X) > 20 ? (int)(ep.X - sp.X) : 20;
                    int height = (int)(ep.Y - sp.Y) > 20 ? (int)(ep.Y - sp.Y) : 20;
                    //添加元件
                    mainwin.GetCurrentProject().AddComponent((int)sp.X, (int)sp.Y, width, height, targetType);
                }
                e.Handled = true;
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            // without a background the OnMouseMove event would not be fired!
            // Alternative: implement a Canvas as a child of this adorner, like
            // the ConnectionAdorner does.
            dc.DrawRectangle(Brushes.Transparent, null, new Rect(RenderSize));

            if (this.startPoint.HasValue && this.endPoint.HasValue)
                dc.DrawImage(new BitmapImage(new Uri(targetType.PicPath, UriKind.RelativeOrAbsolute)), new Rect(this.startPoint.Value, this.endPoint.Value));
        }
    }
}

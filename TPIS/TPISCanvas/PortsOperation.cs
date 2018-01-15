using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Model;

namespace TPIS.TPISCanvas
{
    public class DesignerPort : Canvas
    {
        public double X { get; set; }
        public double Y { get; set; }

        public DesignerPort(Port p)
        {
            Ellipse ellipse = new Ellipse()
            {
                Width = 12,
                Height = 12,
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };
            if(p.type == false)
                ellipse.Fill = Brushes.White;

            this.X = p.x;
            this.Y = p.y;
            this.Width = 12;
            this.Height = 12;
            this.Children.Add(ellipse);
        }
    }

    public partial class DesignerComponent : Canvas, ISelectable
    {
        public void InitPort()
        {
            Console.WriteLine(((TPISComponent)this.DataContext).Ports.Count);
            foreach (Port p in ((TPISComponent)this.DataContext).Ports)
            {
                this.Children.Add(new DesignerPort(p));
            }
            RePosPorts();
        }

        public void RePosPorts()
        {
            foreach (UIElement uie in this.Children)
            {
                if (uie is DesignerPort)
                {
                    DesignerPort dp = (DesignerPort)uie;
                    dp.SetValue(Canvas.TopProperty, this.Height * dp.Y - 6);
                    dp.SetValue(Canvas.LeftProperty, this.Width * dp.X - 6);
                }
            }
        }
    }
}

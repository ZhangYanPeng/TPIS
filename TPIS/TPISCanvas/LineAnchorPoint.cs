using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Project;

namespace TPIS.TPISCanvas
{

    /// <summary>
    /// 自定义事件转换
    /// </summary>
    public class AnchorPosConverter : IValueConverter
    {
        public int index { get; set; }
        public string path { get; set; }
        public AnchorPosConverter(int i, string p)
        {
            index = i;
            path = p;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            else
            {
                ObservableCollection<Point> points = value as ObservableCollection<Point>;
                if (path == "x")
                    return (double)points[index].X - 4;
                else
                    return (double)points[index].Y - 4;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class LineAnchorPoint : UserControl
    {
        long lineID;
        int LineAnchorPointID;
        bool IsDrag { get; set; }

        public LineAnchorPoint() : base()
        {
            Rectangle rect = new Rectangle()
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.LightGoldenrodYellow,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };
            this.Width = 8;
            this.Height = 8;
            this.Content = rect;
            base.MouseEnter += new MouseEventHandler(Element_MouseEnter);
            base.MouseLeave += new MouseEventHandler(Element_MouseLeave);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(Anchor_MouseLeftDown);
            base.MouseMove += new MouseEventHandler(Anchor_MouseMove);
            base.MouseUp += new MouseButtonEventHandler(Anchor_MouseUp);
            return;
        }

        public LineAnchorPoint(long lID, int apID)
        {
            lineID = lID;
            LineAnchorPointID = apID;
            Rectangle rect = new Rectangle()
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.LightGoldenrodYellow,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };
            this.Width = 8;
            this.Height = 8;
            this.Content = rect;
            base.MouseEnter += new MouseEventHandler(Element_MouseEnter);
            base.MouseLeave += new MouseEventHandler(Element_MouseLeave);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(Anchor_MouseLeftDown);
            base.MouseMove += new MouseEventHandler(Anchor_MouseMove);
            base.MouseUp += new MouseButtonEventHandler(Anchor_MouseUp);
        }

        void Element_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.SizeAll;
            Mouse.OverrideCursor = null;
        }

        public void Element_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        void Anchor_MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            IsDrag = true;
            this.CaptureMouse();
            e.Handled = true;
        }

        void Anchor_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured)
                    this.CaptureMouse();
                if (!IsDrag)
                    return;
                Point endPoint = e.GetPosition((ProjectDesignerCanvas)this.Parent);

                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;

                foreach (ObjectBase obj in mainwin.GetCurrentProject().Objects)
                {
                    if (obj.GetType() == typeof(TPISLine))
                    {
                        TPISLine line = (TPISLine)obj;
                        if (this.lineID == line.No)//确定线
                        {
                            //解决移动线条时出现的问题
                            line.PointTo(LineAnchorPointID + 1, endPoint);
                        }
                    }
                }
            }
            else
            {
                if (this.IsMouseCaptured) this.ReleaseMouseCapture();
            }

            e.Handled = true;
        }

        void Anchor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured) this.ReleaseMouseCapture();
            IsDrag = false;
            e.Handled = true;
        }
    }

    partial class ProjectDesignerCanvas : Canvas
    {
        public List<LineAnchorPoint> laps;
        public void InitLineAnchorPoints(long lID, TPISLine line)
        {
            laps = new List<LineAnchorPoint>();
            for (int i = 0; i < line.Points.Count - 2; i++)
            {
                laps.Add(new LineAnchorPoint(lID, i));
                this.Children.Add(laps[i]);
            }
            RePosLineAnchorPoints(line);
        }

        public void RePosLineAnchorPoints(TPISLine line)
        {//重置锚点
            int i = 0;
            foreach (LineAnchorPoint lap in laps)
            {
                i++;
                {
                    //laps[i].SetValue(Canvas.TopProperty, line.Points[i + 1].Y);
                    Binding binding = new Binding();
                    binding.Source = line;
                    binding.Path = new PropertyPath("Points");
                    binding.Converter = new AnchorPosConverter(i, "y");
                    binding.Mode = BindingMode.OneWay;
                    lap.SetBinding(Canvas.TopProperty, binding);
                }
                {
                    //laps[i].SetValue(Canvas.TopProperty, line.Points[i + 1].X);
                    Binding binding = new Binding();
                    binding.Source = line;
                    binding.Path = new PropertyPath("Points");
                    binding.Converter = new AnchorPosConverter(i, "x");
                    binding.Mode = BindingMode.OneWay;
                    lap.SetBinding(Canvas.LeftProperty, binding);
                }
                {
                    Binding binding = new Binding();
                    binding.Source = line;
                    binding.Path = new PropertyPath("IsSelected");
                    binding.Converter = new SelectConverter();
                    lap.SetBinding(AnchorPoint.VisibilityProperty, binding);
                }
            }
        }
    }
}

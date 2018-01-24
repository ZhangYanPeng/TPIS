using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            else
                return (double)value - 4;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class LineAnchorPoint : UserControl
    {
        long lineID;
        public LineAnchorPoint(long lID)
        {
            lineID = lID;
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
        }

        void Element_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.SizeAll;
            Mouse.OverrideCursor = null;
        }
    }

    partial class ProjectDesignerCanvas : Canvas
    {
        public List<LineAnchorPoint> laps;
        public void InitLineAnchorPoints(long lID)
        {
            laps = new List<LineAnchorPoint>();
            for (int i = 0; i < line.Points.Count - 2; i++)
            {
                laps.Add(new LineAnchorPoint(lID));
                this.Children.Add(laps[i]);
                
            }
            RePosLineAnchorPoints();
        }

        public void RePosLineAnchorPoints()
        {//重置锚点
            int i = 0;
            foreach (LineAnchorPoint lap in laps)
            {
                i++;
                {
                    //laps[i].SetValue(Canvas.TopProperty, line.Points[i + 1].Y);
                    Binding binding = new Binding();
                    binding.Source = line.Points[i];
                    binding.Path = new PropertyPath("Y");
                    binding.Converter = new AnchorPosConverter();
                    binding.Mode = BindingMode.OneWay;
                    lap.SetBinding(Canvas.TopProperty, binding);
                }
                {
                    //laps[i].SetValue(Canvas.TopProperty, line.Points[i + 1].X);
                    Binding binding = new Binding();
                    binding.Source = line.Points[i];
                    binding.Path = new PropertyPath("X");
                    binding.Converter = new AnchorPosConverter();
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
            //foreach (UIElement uil in this.Children)
            //{
            //    if (uil is LineAnchorPoint)
            //    {
            //        for (int i = 0; i < line.Points.Count - 2; i++)
            //        {
            //            laps[i] = (LineAnchorPoint)uil;//Count-2个
            //            {
            //                //laps[i].SetValue(Canvas.TopProperty, line.Points[i + 1].Y);
            //                Binding binding = new Binding();
            //                binding.Source = line.Points[i + 1];
            //                binding.Path = new PropertyPath("Y");
            //                binding.Converter = new AnchorPosConverter();
            //                binding.Mode = BindingMode.OneWay;
            //                laps[i].SetBinding(Canvas.TopProperty, binding);
            //            }
            //            {
            //                //laps[i].SetValue(Canvas.TopProperty, line.Points[i + 1].X);
            //                Binding binding = new Binding();
            //                binding.Source = line.Points[i + 1];
            //                binding.Path = new PropertyPath("X");
            //                binding.Converter = new AnchorPosConverter();
            //                binding.Mode = BindingMode.OneWay;
            //                laps[i].SetBinding(Canvas.LeftProperty, binding);
            //            }
            //            Console.WriteLine(i);
            //        }

            //    }
            //}
        }
    }
}

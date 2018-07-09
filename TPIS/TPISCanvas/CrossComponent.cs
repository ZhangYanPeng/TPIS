using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Model;

namespace TPIS.TPISCanvas
{
    partial class DesignerComponent : Canvas
    {
        private void InitCrossIndicator(object sender, RoutedEventArgs e)
        {
            IsIndicatorDrag = false;
            if (DataContext is ResultCross)
            {
                Line line = new Line();
                {
                    Binding posbinding = new Binding();
                    posbinding.Source = DataContext;
                    posbinding.Path = new PropertyPath("Indicator");
                    posbinding.Converter = new PointXConverter();
                    posbinding.Mode = BindingMode.OneWay;
                    line.SetBinding(Line.X1Property, posbinding);
                }
                {
                    Binding posbinding = new Binding();
                    posbinding.Source = DataContext;
                    posbinding.Path = new PropertyPath("Indicator");
                    posbinding.Converter = new PointYConverter();
                    posbinding.Mode = BindingMode.OneWay;
                    line.SetBinding(Line.Y1Property, posbinding);
                }
                {
                    Binding posbinding = new Binding();
                    posbinding.Source = DataContext;
                    posbinding.Path = new PropertyPath("IndicatorRelated");
                    posbinding.Converter = new PointXConverter();
                    posbinding.Mode = BindingMode.OneWay;
                    line.SetBinding(Line.X2Property, posbinding);
                }
                {
                    Binding posbinding = new Binding();
                    posbinding.Source = DataContext;
                    posbinding.Path = new PropertyPath("IndicatorRelated");
                    posbinding.Converter = new PointYConverter();
                    posbinding.Mode = BindingMode.OneWay;
                    line.SetBinding(Line.Y2Property, posbinding);
                }
                line.StrokeThickness = 2;
                line.Stroke = Brushes.Black;
                Ellipse dot = new Ellipse();
                {
                    Binding posbinding = new Binding();
                    posbinding.Source = DataContext;
                    posbinding.Path = new PropertyPath("Indicator");
                    posbinding.Converter = new CircleXConverter();
                    posbinding.Mode = BindingMode.OneWay;
                    dot.SetBinding(Canvas.LeftProperty, posbinding);
                }
                {
                    Binding posbinding = new Binding();
                    posbinding.Source = DataContext;
                    posbinding.Path = new PropertyPath("Indicator");
                    posbinding.Converter = new CircleYConverter();
                    posbinding.Mode = BindingMode.OneWay;
                    dot.SetBinding(Canvas.TopProperty, posbinding);
                }
                dot.Width = 8;
                dot.Height = 8;
                dot.Stroke = Brushes.Black;
                dot.StrokeThickness = 2;
                dot.Fill = Brushes.Black;

                dot.MouseLeftButtonDown += new MouseButtonEventHandler(IndicatorLMouseBtnDown);
                dot.MouseLeftButtonUp += new MouseButtonEventHandler(IndicatorLMouseBtnUp);
                dot.MouseMove += new MouseEventHandler(IndicatorMouseMove);

                Children.Add(line);
                Children.Add(dot);
            }
        }

        private bool IsIndicatorDrag;

        private void IndicatorMouseMove(object sender, MouseEventArgs e)
        {
            if (IsIndicatorDrag)
            {
                Point tmp = e.GetPosition(this);
                ((ResultCross)DataContext).Indicator = tmp;
                e.Handled = true;
            }
        }

        private void IndicatorLMouseBtnUp(object sender, MouseButtonEventArgs e)
        {
            Ellipse ellipse = sender as Ellipse;
            ellipse.Cursor = Cursors.Arrow;
            ellipse.ReleaseMouseCapture();
            IsIndicatorDrag = false;
            e.Handled = true;
        }

        private void IndicatorLMouseBtnDown(object sender, MouseButtonEventArgs e)
        {
            Ellipse ellipse = sender as Ellipse;
            ellipse.Cursor = Cursors.SizeAll;
            ellipse.CaptureMouse();
            IsIndicatorDrag = true;
            e.Handled = true;
        }
    }

    public class PointXConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;
            return ((Point)value).X;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class PointYConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;
            return ((Point)value).Y;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class CircleXConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;
            return ((Point)value).X-4;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class CircleYConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return 0;
            return ((Point)value).Y-4;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
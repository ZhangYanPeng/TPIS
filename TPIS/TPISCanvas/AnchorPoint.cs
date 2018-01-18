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

    public enum AnchorPointType
    {
        UL, U, UR,
        L, R,
        DL, D, DR
    }

    /// <summary>
    /// 自定义事件转换
    /// </summary>
    public class SelectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            Console.WriteLine(value);
            if ((bool)value)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                Visibility visibility = (Visibility)value;
                if (visibility == Visibility.Visible)
                    return true;
                else
                    return false;
            }
            return DependencyProperty.UnsetValue;
        }
    }

    public class AnchorPoint : Canvas
    {
        public AnchorPointType Type { get; set; }

        public AnchorPoint(AnchorPointType type)
        {
            Type = type;
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
            this.Children.Add(rect);
            base.MouseEnter += new MouseEventHandler(Element_MouseEnter);
        }

        void Element_MouseEnter(object sender, MouseEventArgs e)
        {
            switch (Type)
            {
                case AnchorPointType.U:
                case AnchorPointType.D: this.Cursor = Cursors.SizeNS; break;
                case AnchorPointType.L:
                case AnchorPointType.R: this.Cursor = Cursors.SizeWE; break;
                case AnchorPointType.UL:
                case AnchorPointType.DR: this.Cursor = Cursors.SizeNWSE; break;
                case AnchorPointType.UR:
                case AnchorPointType.DL: this.Cursor = Cursors.SizeNESW; break;
            }
            Mouse.OverrideCursor = null;
        }
    }

    partial class DesignerComponent : Canvas
    {
        public void InitAnchorPoints(object sender,  RoutedEventArgs e)
        {

            this.Children.Add(new AnchorPoint(AnchorPointType.UL));
            this.Children.Add(new AnchorPoint(AnchorPointType.U));
            this.Children.Add(new AnchorPoint(AnchorPointType.UR));
            this.Children.Add(new AnchorPoint(AnchorPointType.L));
            this.Children.Add(new AnchorPoint(AnchorPointType.R));
            this.Children.Add(new AnchorPoint(AnchorPointType.DL));
            this.Children.Add(new AnchorPoint(AnchorPointType.D));
            this.Children.Add(new AnchorPoint(AnchorPointType.DR));
            RePosAnchorPoints();
        }

        public void RePosAnchorPoints()
        {
            foreach (UIElement uie in this.Children)
            {
                if (uie is AnchorPoint)
                {
                    AnchorPoint ap = (AnchorPoint)uie;
                    if (ap.Type == AnchorPointType.UL || ap.Type == AnchorPointType.U || ap.Type == AnchorPointType.UR)
                        ap.SetValue(Canvas.TopProperty, 0.0);
                    else if (ap.Type == AnchorPointType.L || ap.Type == AnchorPointType.R)
                    {
                        ap.SetValue(Canvas.TopProperty, this.Height / 2 - 4);
                    }
                    else
                    {
                        ap.SetValue(Canvas.TopProperty, this.Height - 8);
                    }

                    if (ap.Type == AnchorPointType.UL || ap.Type == AnchorPointType.L || ap.Type == AnchorPointType.DL)
                        ap.SetValue(Canvas.LeftProperty, 0.0);
                    else if (ap.Type == AnchorPointType.U || ap.Type == AnchorPointType.D)
                    {
                        ap.SetValue(Canvas.LeftProperty, this.Width / 2 - 4);
                    }
                    else
                    {
                        ap.SetValue(Canvas.LeftProperty, this.Width - 8);
                    }

                    Binding binding = new Binding();
                    binding.Source = this.DataContext;
                    binding.Path = new PropertyPath("IsSelected");
                    binding.Converter = new SelectConverter();
                    ap.SetBinding(AnchorPoint.VisibilityProperty, binding);
                }
            }
        }

        public void Component_Select()
        {
            ((TPISComponent)this.DataContext).IsSelected = true;
        }

        public void Component_UnSelect()
        {
            ((TPISComponent)this.DataContext).IsSelected = false;
        }
    }
}

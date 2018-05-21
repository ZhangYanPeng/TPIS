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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Model.Common;

namespace TPIS.Views
{
    /// <summary>
    /// PropertyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PropertyWindow : Window
    {
        public TPISComponent Component { get; set; }

        public PropertyWindow(TPISComponent component)
        {
            InitializeComponent();
            Component = component;
            Mode.ItemsSource = component.Mode;
            ModePropeties.ItemsSource = component.PropertyGroups;

            Binding modeBinding = new Binding();
            modeBinding.Source = Component;
            modeBinding.Path = new PropertyPath("SelectedMode");
            Mode.SetBinding(ComboBox.SelectedIndexProperty, modeBinding);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OpenCurveWindow_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            CurvesData.FittingWin ftw = new CurvesData.FittingWin(btn.DataContext as CurvesData.Curves);
            ftw.Owner = this;
            ftw.ShowDialog();
        }
    }

    public class ModeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            switch ((SelMode)value)
            {
                    case SelMode.None: return "全部";
                    case SelMode.DesignMode: return "设计模式"; 
                    case SelMode.CalMode: return "计算模式"; 
                    case SelMode.InterMode: return "插值模式";
             }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    //控制显示样式
    public class ValVisualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Visible;
            else
            {
                if ((P_Type)value != P_Type.ToSelect && (P_Type)value != P_Type.ToLine)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    //控制显示样式
    public class MeasureVisualConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return Visibility.Visible;
            else
            {
                if ((P_Type)values[1] == P_Type.ToLine)
                    return Visibility.Collapsed;
                string[] tmp = (string[])values[0];
                if (tmp.Count<String>() == 1 && tmp[0] == "")
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    //控制显示样式
    public class LineVisualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Visible;
            else
            {
                if ((P_Type)value == P_Type.ToLine)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }


}


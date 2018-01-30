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

}


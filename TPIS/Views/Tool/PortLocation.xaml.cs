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
using TPIS.Project;

namespace TPIS.Views.Tool
{
    /// <summary>
    /// PortLocation.xaml 的交互逻辑
    /// </summary>
    public partial class PortLocation : Window
    {
        public Port port;
        public TPISComponent component;

        public PortLocation(Port p)
        {
            InitializeComponent();
            SetText(p);
            port = p;
        }

        public void SetText(Port p)
        {
            Port_X.Text = p.x.ToString();
            Port_Y.Text = p.y.ToString();
        }

        public void PortMove(object sender, RoutedEventArgs e)
        {
            if ((bool)Text.IsChecked)
            {//按文本框改变
                port.x = Convert.ToDouble(Port_X.Text);
                port.y = Convert.ToDouble(Port_Y.Text);
            }

            if ((bool)Position.IsChecked)
            {
                Position_PortSet();
                SetText(port);
            }
            ViewRePosPort();
        }

        public void ViewRePosPort()
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            foreach (ObjectBase obj in mainwin.GetCurrentProject().SelectedObjects)
            {
                if (obj is TPISComponent)
                {
                    ((TPISComponent)obj).RePosPort();
                }
            }
        }

        public void Position_PortSet()
        {
            if ((bool)LU.IsChecked)
            {
                port.x = 0;
                port.y = 0;
            }
            if ((bool)MU.IsChecked)
            {
                port.x = 0.5;
                port.y = 0;
            }
            if ((bool)RU.IsChecked)
            {
                port.x = 1;
                port.y = 0;
            }
            if ((bool)LM.IsChecked)
            {
                port.x = 0;
                port.y = 0.5;
            }
            if ((bool)RM.IsChecked)
            {
                port.x = 1;
                port.y = 0.5;
            }
            if ((bool)LD.IsChecked)
            {
                port.x = 0;
                port.y = 1;
            }
            if ((bool)MD.IsChecked)
            {
                port.x = 0.5;
                port.y = 1;
            }
            if ((bool)RD.IsChecked)
            {
                port.x = 1;
                port.y = 1;
            }
        }
    }

    //public class PositionConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        int data = (int)value;
    //        string name = parameter.ToString();
    //        switch (name)
    //        {
    //            case "0":
    //                return data == 0;
    //            case "1":
    //                return data == 1;
    //            default:
    //                return false;
    //        }
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        return null;
    //    }
    //}
}

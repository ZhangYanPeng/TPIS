using System;
using System.Collections.Generic;
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

namespace TPIS.Views.Tool
{
    /// <summary>
    /// PortLocation.xaml 的交互逻辑
    /// </summary>
    public partial class PortLocation : Window
    {
        public Port port;
        public PortLocation(Port p)
        {
            InitializeComponent();
            Port_X.Text = p.P_x.ToString();
            Port_Y.Text = p.P_y.ToString();
            port = p;
        }

        public void PortMove(object sender, RoutedEventArgs e)
        {
            port.P_x = Convert.ToDouble(Port_X.Text);
            port.P_y = Convert.ToDouble(Port_Y.Text);
        }
    }
}

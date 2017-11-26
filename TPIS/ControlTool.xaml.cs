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

namespace TPIS
{
    /// <summary>
    /// ControlTool.xaml 的交互逻辑
    /// </summary>
    public partial class ControlTool : Window
    {
        MainWindow mainwin = (MainWindow)Application.Current.MainWindow;

        public ControlTool()
        {
            InitializeComponent();
        }

        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = new Rectangle();
            rect.Width = 50;
            rect.Height = 50;
            rect.Stroke = Brushes.Black;

            mainwin.cs.Children.Add(rect);
        }
    }
}

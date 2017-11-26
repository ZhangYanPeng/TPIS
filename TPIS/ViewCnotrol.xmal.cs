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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TPIS.Project;

namespace TPIS
{
    /// <summary>
    /// 视图的网格线、画图控件（工具箱）
    /// </summary>
    public partial class MainWindow : Window
    {
        //打开工具箱
        private void ControlTool(object sender, RoutedEventArgs e)
        {
            Window control_tool = new ControlTool();
            control_tool.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            control_tool.ShowDialog();
        }
    }
}

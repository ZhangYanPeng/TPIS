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

namespace TPIS.Views
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            cw.Text = mainwin.TPISconfig.CANVAS_WIDTH.ToString();
            ch.Text = mainwin.TPISconfig.CANVAS_HEIGHT.ToString();
            cg.SelectedIndex = mainwin.TPISconfig.CANVAS_GRID;
            lt.Text = mainwin.TPISconfig.LINE_THICKNESS.ToString();
            mi.Text = mainwin.TPISconfig.MAX_ITER.ToString();
            gs.SelectedIndex = mainwin.TPISconfig.GAS_STAND;
            ws.SelectedIndex = mainwin.TPISconfig.WATER_STAND;
        }

        private void SaveSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int.Parse(cw.Text);
                int.Parse(ch.Text);
                int.Parse(lt.Text);
                int.Parse(mi.Text);
            }
            catch
            {
                MessageBox.Show("参数必须为整数！");
                return;
            }

            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            mainwin.TPISconfig.CANVAS_WIDTH = int.Parse(cw.Text);
            mainwin.TPISconfig.CANVAS_HEIGHT = int.Parse(ch.Text);
            mainwin.TPISconfig.CANVAS_GRID = cg.SelectedIndex;
            mainwin.TPISconfig.LINE_THICKNESS = int.Parse(lt.Text);
            mainwin.TPISconfig.MAX_ITER = int.Parse(mi.Text);
            mainwin.TPISconfig.GAS_STAND = gs.SelectedIndex;
            mainwin.TPISconfig.WATER_STAND = ws.SelectedIndex;
            mainwin.TPISconfig.SaveCfg();
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

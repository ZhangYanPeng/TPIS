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
using TPIS.Project;

namespace TPIS.Views
{
    /// <summary>
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class CurrentSetting : Window
    {
        ProjectItem pi;

        public CurrentSetting(ProjectItem cpi)
        {
            pi = cpi;
            InitializeComponent();
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            cw.Text = cpi.Canvas.Width.ToString();
            ch.Text = cpi.Canvas.Height.ToString();
            cg.SelectedIndex = (int)cpi.GridThickness;
            lt.Text = cpi.LineThickness.ToString();
            mi.Text = cpi.MaxIter.ToString();
            gs.SelectedIndex = cpi.GasStand;
            ws.SelectedIndex = cpi.WaterStand;
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
            pi.Canvas.Width = int.Parse(cw.Text);
            pi.Canvas.Height = int.Parse(ch.Text);
            pi.GridThickness = cg.SelectedIndex;
            pi.LineThickness = int.Parse(lt.Text);
            pi.MaxIter = int.Parse(mi.Text);
            pi.GasStand = gs.SelectedIndex;
            pi.WaterStand = ws.SelectedIndex;
            Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

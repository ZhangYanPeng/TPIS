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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ProjectControl projectList;

        public MainWindow()
        {
            InitializeComponent();
            projectList = new ProjectControl();
        }

        private void New_Project(object sender, RoutedEventArgs e)
        {
            Window new_project = new NewProject();
            new_project.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            new_project.ShowDialog();
        }
        
    }
}

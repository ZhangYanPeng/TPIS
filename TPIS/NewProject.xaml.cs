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

namespace TPIS
{
    /// <summary>
    /// NewProject.xaml 的交互逻辑
    /// </summary>
    public partial class NewProject : Window
    {
        public NewProject()
        {
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            try
            {
                int.Parse(canvas_height.Text);
                int.Parse(canvas_width.Text);
            }
            catch(Exception exp)
            {
                MessageBoxResult dr = MessageBox.Show("高度和宽度只能为整数", "提示", MessageBoxButton.OKCancel);
                return;
            }

            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.projectList.projects.Add(new ProjectItem(int.Parse(canvas_height.Text), int.Parse(canvas_width.Text), project_name.Text));
            Console.WriteLine(mainwin.projectList.projects.Count());
            this.Close();
        }


    }
}

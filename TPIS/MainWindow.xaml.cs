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
        public ProjectItem currentPoject;

        public MainWindow()
        {
            InitializeComponent();
            projectList = new ProjectControl();
            tab_project.SelectionChanged += new SelectionChangedEventHandler(Project_Change);
        }

        public void Project_Change(object sender, RoutedEventArgs e)
        {
            TabItem tis = (TabItem)tab_project.SelectedItem;
            foreach(TabItem ti in tab_project.Items)
            {
                DockPanel dp = (DockPanel)ti.Header;
                foreach (DependencyObject child in dp.Children)
                {
                    if (child is Button)
                    {
                        if (ti.Name == tis.Name)
                        {
                            //选中tab
                            ((Button)child).Visibility = Visibility.Visible;
                        }
                        else {
                            //非选中tab
                            ((Button)child).Visibility = Visibility.Hidden;
                        }
                    }
                }
            }
            //设置当前工程
            currentPoject = projectList.projects[tab_project.SelectedIndex];
        }

        private void New_Project(object sender, RoutedEventArgs e)
        {
            Window new_project = new NewProject();
            new_project.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            new_project.ShowDialog();
        }
        
        public void Active_Project(int num)
        {
            //激活Tab
            tab_project.SelectedIndex = num;
        }
    }
}

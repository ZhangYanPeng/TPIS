using System;
using System.Collections.Generic;
using System.ComponentModel;
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


namespace TPIS
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ProjectSpace projectList;//工程列表
        public ProjectItem currentPoject;//当前激活工程

        public MainWindow()
        {
            InitializeComponent();
            Window_Loaded();
            projectList = new ProjectSpace();

            //projectTab.SelectionChanged += new SelectionChangedEventHandler(onProjectChange);
        }

        /// <summary>
        /// 窗体自适应电脑工作区大小
        /// </summary>
        private void Window_Loaded()
        {
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;
        }

        public void AddProject( string pName, int width, int height)
        {
            ProjectCanvas pCanvas = new ProjectCanvas(width, height);
            ProjectItem project = new ProjectItem(pName, pCanvas);
            this.projectList.projects.Add(project);
            this.projectTab.ItemsSource = projectList.projects;
            this.projectTab.Items.Refresh();
            foreach(ProjectItem pi in projectList.projects)
            {
                Test(pi);
            }
        }

        public void Test(ProjectItem p)
        {
            TPISComponent c = new TPISComponent(10,20,1);
            TPISComponent c1 = new TPISComponent(200, 300, 2);
            p.Components.Add(c);
            p.Components.Add(c1);
        }
    }
}

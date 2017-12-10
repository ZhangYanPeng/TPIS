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
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ProjectSpace projectList;//工程列表
        public ProjectItem currentPoject;//当前激活工程
        //public TabControl projectTab;
        public Canvas cs;

        public MainWindow()
        {
            InitializeComponent();
            projectList = new ProjectSpace();
            //MessageBox.Show((string)tab.FindName("tab_project"));
            //MessageBox.Show(tab.Content.ToString());
            //projectTab.SelectionChanged += new SelectionChangedEventHandler(onProjectChange);
        }

    }
}

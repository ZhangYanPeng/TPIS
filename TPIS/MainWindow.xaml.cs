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
        public ProjectSpace projectList;//工程列表
        public ProjectItem currentPoject;//当前激活工程
        public Canvas cs;

        public MainWindow()
        {
            InitializeComponent();
            projectList = new ProjectSpace();
            tab_project.SelectionChanged += new SelectionChangedEventHandler(onProjectChange);
        }

        public void Test(object sender, RoutedEventArgs e)
        {
            currentPoject.canvas.Height = currentPoject.canvas.Height + 100;
            currentPoject.canvas.Width = currentPoject.canvas.Width + 100;
        }
        public void Rect(object sender, RoutedEventArgs e)  //*提示修改测试
        {
            Rectangle rect = new Rectangle();
            rect.Width = 50;
            rect.Height = 50;
            rect.Stroke = Brushes.Black;
            cs.Children.Add(rect);
        }
    }
}

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
        MainWindow mainwin = (MainWindow)Application.Current.MainWindow;

        public NewProject()
        {
            InitializeComponent();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //新建程序
        private void Create(object sender, RoutedEventArgs e)
        {
            try
            {
                int.Parse(canvas_height.Text);
                int.Parse(canvas_width.Text);
            }
            catch (Exception exp)
            {
                MessageBoxResult dr = MessageBox.Show("高度和宽度只能为整数", "提示", MessageBoxButton.OKCancel);
                return;
            }

            mainwin.projectList.projects.Add(new ProjectItem(int.Parse(canvas_height.Text), int.Parse(canvas_width.Text), project_name.Text + ".tpis"));
            //插入tab
            TabItem ti = AddTab(mainwin.projectList.projects.Last(), mainwin.projectList.projects.Count-1);
            mainwin.tab_project.Items.Add(ti);

            mainwin.ActiveProject(mainwin.projectList.projects.Count-1);
            this.Close();
        }

        //新增 工程tab
        private TabItem AddTab(ProjectItem project, int num)
        {
            TabItem ti = new TabItem();//< TabItem Header = "project.tpis" Name = "tabItem1" >
            ti.Name = "tabItem" + num.ToString();

            TextBlock tb = new TextBlock();
            tb.Text = project.name;

            tb.TextTrimming = TextTrimming.CharacterEllipsis;//TextBlock...显示

            Button bt = new Button();
            Image img = new Image();
            img.Source = new BitmapImage(new Uri("res/icon/tab_close.png", UriKind.Relative));
            img.Width = 10;
            img.Height = 10;
            bt.Content = img;
            bt.Visibility = Visibility.Hidden;

            DockPanel dp = new DockPanel();
            dp.Name = "project_header" + num.ToString();
            DockPanel.SetDock(bt, Dock.Right);
            dp.Children.Add(bt);
            dp.Children.Add(tb);
            dp.MinWidth = 30;
            dp.Width = 90;
            ti.Header = dp;

           
            Canvas cs = new Canvas();//< Canvas Name = "MainCanvas" Background = "Gray" ></ Canvas >
            cs.Name = "canvas" + num.ToString();
            cs.Background = new SolidColorBrush(Colors.Gray);
            cs.MinHeight = project.canvas.Whole_height;
            cs.MinWidth = project.canvas.Whole_width;
            cs.VerticalAlignment = VerticalAlignment.Stretch;

            Rectangle rect = new Rectangle();
            rect.Fill = new SolidColorBrush(Colors.White);
            rect.Stroke = new SolidColorBrush(Colors.White);
            rect.SetValue(Canvas.LeftProperty, (double)0);
            rect.SetValue(Canvas.TopProperty, (double)0);
            rect.Height = project.canvas.V_height;
            rect.Width = project.canvas.V_width;
            cs.Children.Add(rect);

            mainwin.CanvasBinding(project.canvas, cs, rect);

            DockPanel sp = new DockPanel();
            sp.Children.Add(cs);

            ScrollViewer sv = new ScrollViewer();
            sv.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            sv.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            sv.Content = sp;

            ti.Content = sv;

            return ti;
        }

        
    }
}

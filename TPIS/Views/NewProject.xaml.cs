using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WinForms = System.Windows.Forms;
using TPIS.Project;
using TPIS.TPISCanvas;
using System.Windows.Media;

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

        /// <summary>
        /// 新建
        /// </summary>
        private void Create(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(proj_name.Text) && string.IsNullOrWhiteSpace(proj_location.Text))
                MessageBox.Show("项目名和储位置不能为空！", "提示", MessageBoxButton.OKCancel);
            else if (string.IsNullOrWhiteSpace(proj_name.Text))
                MessageBox.Show("项目名不能为空！", "提示", MessageBoxButton.OKCancel);
            else if (string.IsNullOrWhiteSpace(proj_location.Text))
                MessageBox.Show("存储位置不能为空！", "提示", MessageBoxButton.OKCancel);
            else
            {
                string[] temp = proj_dpi.Text.Split('X');
                int projectCanvasWidth = int.Parse(temp[0]);
                int projectCanvasHeight = int.Parse(temp[1]);
                mainwin.projectList.projects.Add(new ProjectItem(proj_name.Text + ".tpis"));
                TabItem projecTabItem = AddTab(mainwin.projectList.projects.Last(), mainwin.projectList.projects.Count - 1, projectCanvasWidth, projectCanvasHeight);
                mainwin.projectTab.Items.Add(projecTabItem);
                Close();
            }
            //mainwin.ActiveProject(mainwin.projectList.projects.Count-1);
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 新增
        /// </summary>
        private TabItem AddTab(ProjectItem project, int num, int width, int height)
        {
            //工作区项目标签名（项目名）
            TextBlock textBlock = new TextBlock
            {
                Text = project.name,
                TextTrimming = TextTrimming.CharacterEllipsis//TextBlock...显示
            };

            //工作区项目标签关闭按钮
            Button closeButton = new Button();
            Image closeImg = new Image
            {
                Source = new BitmapImage(new Uri("Images/icon/tab_close.png", UriKind.Relative)),
                Width = 10,
                Height = 10
            };
            closeButton.Content = closeImg;

            //工作区项目标签面板
            DockPanel projectPanel = new DockPanel
            {
                Name = "project_header" + num.ToString(),
                MinWidth = 30,
                Width = 90
            };
            DockPanel.SetDock(closeButton, Dock.Right);
            projectPanel.Children.Add(closeButton);
            projectPanel.Children.Add(textBlock);


            //工作区< TabItem Header = "project.tpis" Name = "tabItem1" >
            TabItem projectTabItem = new TabItem
            {
                Name = "tabItem" + num.ToString(),
                Header = projectPanel
            };


            //工作区画布< Canvas Name = "MainCanvas" Background = "Gray" ></ Canvas >
            //Canvas projectCanvas = new Canvas
            //{
            //    Name = "canvas" + num.ToString(),
            //    Background = new SolidColorBrush(Colors.White),


            //    //MinWidth = mainwin.projectTab.Width,//auto
            //    //MinHeight = mainwin.projectTab.Height
            //    //MinWidth = SystemParameters.WorkArea.Width,
            //    //MinHeight= SystemParameters.WorkArea.Height
            //};
            ProjectDesignerCanvas projectCanvas = new ProjectDesignerCanvas
            {
                Focusable = true,
                Background = new SolidColorBrush(Colors.White),
                Width = width,
                Height = height
            };
            // mainwin.CanvasBinding(project.canvas, mainwin.cs);

            //工作区附加滚动条
            ScrollViewer projectScroll = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Content = projectCanvas
            };

            projectTabItem.Content = projectScroll;
            return projectTabItem;
        }

        /// <summary>
        /// 浏览文件夹
        /// </summary>
        private void FolderBrowse(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog m_Dialog = new WinForms.FolderBrowserDialog();
            WinForms.DialogResult result = m_Dialog.ShowDialog();
            if (result == WinForms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            proj_location.Text = m_Dir;
        }
    }
}

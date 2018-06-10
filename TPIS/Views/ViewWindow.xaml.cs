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
using TPIS.Project;
using TPIS.TPISCanvas;

namespace TPIS.Views
{
    /// <summary>
    /// ViewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ViewWindow : Window
    {
        public ProjectSpace ProjectList { get; set; } //工程列表
        public ViewWindow()
        {
            InitializeComponent();
            //Window_Loaded();
            ProjectList = new ProjectSpace();//初始化工作空间
            InitilView();
        }
        private void Window_Loaded()
        {
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;
        }

        #region 视图居中
        public void ViewCenter()
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            Point p = new Point();
            Grid tabGrid = mainwin.FindVisualChild<Grid>(this.viewTab);
            Border contentBorder = tabGrid.FindName("ContentPanel") as Border;
            ContentPresenter contentPresenter = mainwin.FindVisualChild<ContentPresenter>(contentBorder);
            ScrollViewer sv = contentPresenter.ContentTemplate.FindName("View_ScrollViewer", contentPresenter) as ScrollViewer;
            p = mainwin.GetCurrentProject().WorkSpaceSize_Center(mainwin.GetCurrentProject().SelectedObjects);
            sv.ScrollToHorizontalOffset(p.X - sv.ActualWidth / 2);
            sv.ScrollToVerticalOffset(p.Y - sv.ActualHeight / 2);
        }
        #endregion

        public void InitilView()
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            ProjectItem project = mainwin.GetCurrentProject();
            this.ProjectList.projects.Add(project);
            this.viewTab.ItemsSource = ProjectList.projects;
            this.viewTab.Items.Refresh();
            this.viewTab.SelectedItem = project;
            SetWindows();
        }

        public void SetWindows()
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            Point p = new Point();
            p = mainwin.GetCurrentProject().WorkSpaceSize_Size(mainwin.GetCurrentProject().SelectedObjects);
            this.Width = p.X;
            this.Height = p.Y;
            this.MaxWidth = SystemParameters.WorkArea.Width;
            this.MaxHeight = SystemParameters.WorkArea.Height;
        }
    }
}

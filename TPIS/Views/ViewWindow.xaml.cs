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

        #region 视图居中
        public void ViewCenter()
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            ScrollViewer sv = GetViewScrollViewer();
            Point p = new Point();
            p = mainwin.GetCurrentProject().WorkSpaceSize_Center(mainwin.GetCurrentProject().SelectedObjects);
            sv.ScrollToHorizontalOffset(p.X - sv.ActualWidth / 2);
            sv.ScrollToVerticalOffset(p.Y - sv.ActualHeight / 2);
        }
        #endregion

        public ScrollViewer GetViewScrollViewer()
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            Grid tabGrid = mainwin.FindVisualChild<Grid>(this.viewTab);
            Border contentBorder = tabGrid.FindName("ContentPanel") as Border;
            ContentPresenter contentPresenter = mainwin.FindVisualChild<ContentPresenter>(contentBorder);
            ScrollViewer sv = contentPresenter.ContentTemplate.FindName("View_ScrollViewer", contentPresenter) as ScrollViewer;
            return sv;
        }

        public void RemoveAllAnchorPoints()
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            ScrollViewer sv = GetViewScrollViewer();
            ItemsPresenter itemsPresenter = mainwin.FindVisualChild<ItemsPresenter>(sv);
            Canvas canvas = mainwin.FindVisualChild<Canvas>(itemsPresenter);
            for (int i = 0; i < canvas.Children.Count; i++)
            {
                ContentPresenter contentPresenter1 = (ContentPresenter)(canvas.Children[i]);
                DependencyObject obj = VisualTreeHelper.GetChild(contentPresenter1, 0);
                if(obj is DesignerComponent)
                {
                    for (int j = 0; j < ((DesignerComponent)obj).Children.Count; j++)
                    {
                        if (((DesignerComponent)obj).Children[j] is AnchorPoint)
                        {
                            ((DesignerComponent)obj).Children.RemoveAt(j);
                            j--;
                        }
                    }
                }

                
            }
        }

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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            mainwin.GetCurrentProject().IsViewWindowsOpen = false;
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            mainwin.GetCurrentProject().IsViewsMouseEnter = true;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            mainwin.GetCurrentProject().IsViewsMouseEnter = false;
        }
    }
}

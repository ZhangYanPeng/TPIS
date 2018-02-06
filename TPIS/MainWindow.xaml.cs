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
// TPIS.TPISCanvas;


namespace TPIS
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<BaseType> TypeList { get; set; } //所有元件列表
        public ProjectSpace ProjectList { get; set; } //工程列表
        public int CurrentPojectIndex { get; set; } //当前激活工程
        public int ProjectNum { get; set; } // 新工程编号

        public MainWindow()
        {
            InitializeComponent();
            Window_Loaded();
            ProjectList = new ProjectSpace();//初始化工作空间
            loadComponentType();//初始化元件类型
            InitializeMessage();//初始化主窗口事件
            //projectTab.SelectionChanged += new SelectionChangedEventHandler(onProjectChange);

            ProjectNum = 0;
        }

        /// <summary>
        /// 窗体自适应电脑工作区大小
        /// </summary>
        private void Window_Loaded()
        {
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;
        }

        public void AddProject(string pName, int width, int height)
        {
            ProjectCanvas pCanvas = new ProjectCanvas(width, height);
            ProjectItem project = new ProjectItem(pName, pCanvas, ProjectNum);
            this.ProjectList.projects.Add(project);
            this.projectTab.ItemsSource = ProjectList.projects;
            this.projectTab.Items.Refresh();

            //设置当前工程为激活工程
            this.projectTab.SelectedItem = project;

            ProjectNum++;
        }


        /// <summary>
        /// 获取当前工程
        /// </summary>
        private void ProjectTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = projectTab.SelectedIndex;//当前工程索引
            if (projectTab.Items.Count == 0)
                CurrentPojectIndex = 0;
            else
            {
                CurrentPojectIndex = ProjectList.projects.IndexOf((ProjectItem)projectTab.Items[index]);
                UpdateRate();
            }
        }

        /// <summary>
        /// 关闭工程（当前或其他工程）
        /// </summary>
        private void ProjectItem_Close(object sender, RoutedEventArgs e)
        {
            Button closeButton = sender as Button;
            string name = closeButton.Tag.ToString();
            foreach (ProjectItem item in projectTab.Items)
            {
                if (item.Num == CurrentPojectIndex)
                {
                    MessageBox.Show(name);//所点击关闭按钮对应工程为item
                    ProjectList.projects.Remove(item);
                    projectTab.ItemsSource = ProjectList.projects;
                    projectTab.Items.Refresh();
                    break;
                }
                //if (item.Name.ToString() == name)
                //{
                //    MessageBox.Show(name);//所点击关闭按钮对应工程为item
                //    ProjectList.projects.Remove(item);
                //    projectTab.ItemsSource = ProjectList.projects;
                //    projectTab.Items.Refresh();
                //    break;
                //}
            }
        }

    }

    /// <summary>
    /// 模板选择器
    /// </summary>
    #region
    public class SelectionOrComponentSelector : DataTemplateSelector
    {
        private DataTemplate _componentTemplate = null;

        public DataTemplate ComponentTemplate
        {
            get { return _componentTemplate; }
            set { _componentTemplate = value; }
        }

        private DataTemplate _selectionTemplate = null;
        public DataTemplate SelectionTemplate
        {
            get { return _selectionTemplate; }
            set { _selectionTemplate = value; }
        }

        private DataTemplate _lineTemplate = null;
        public DataTemplate LineTemplate
        {
            get { return _lineTemplate; }
            set { _lineTemplate = value; }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TPISComponent)
            {
                return _componentTemplate;
            }
            if(item is TPISLine)

            {
                return _lineTemplate;
            }
            else
            {
                return _selectionTemplate;
            }
        }
    }
    #endregion
}



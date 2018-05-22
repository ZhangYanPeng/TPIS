using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using MouseWheelEventArgs = System.Windows.Forms;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Project;
using TPIS.TPISCanvas;
using TPIS.Views.Modules;
using System.Windows.Controls.Primitives;
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
        public int CurrentPojectIndex { get; set; }//当前激活工程
        public int ProjectNum { get; set; } // 新工程编号
        public ScrollViewer scrollViewer { get; set; } // 工作区

        public ProjectItem GetCurrentProject()
        {
            try
            {
                return ProjectList.projects[CurrentPojectIndex];
            }
            catch
            {
                return null;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            Window_Loaded();
            ProjectList = new ProjectSpace();//初始化工作空间
            loadComponentType();//初始化元件类型
            InitializeMessage();//初始化主窗口事件
            //projectTab.SelectionChanged += new SelectionChangedEventHandler(onProjectChange);
            //初始化日志窗
            InitLogs();
            InitWorkSpace();
            ProjectNum = 0;
        }

        private void InitLogs()
        {
            Binding binding = new Binding();
            binding.Source = ProjectList;
            binding.Path = new PropertyPath("LogStr");
            Logs.SetBinding(TextBox.TextProperty, binding);
        }

        /// <summary>
        /// 窗体自适应电脑工作区大小
        /// </summary>
        private void Window_Loaded()
        {
            Width = SystemParameters.WorkArea.Width;
            Height = SystemParameters.WorkArea.Height;
        }

        public Boolean AddProject(string pName, string path, int width, int height)
        {
            string directoryPath = @".\WorkSpace\" + pName;
            if (!Directory.Exists(directoryPath))//如果路径不存在
            {
                Directory.CreateDirectory(directoryPath);//创建一个路径的文件夹
            }
            else
            {
                return false;
            }
            ProjectCanvas pCanvas = new ProjectCanvas(width, height);
            ProjectItem project = new ProjectItem(pName, pCanvas, ProjectNum, System.IO.Path.GetFullPath(directoryPath));
            this.ProjectList.projects.Add(project);
            this.projectTab.ItemsSource = ProjectList.projects;
            this.projectTab.Items.Refresh();
            

            //设置当前工程为激活工程
            this.projectTab.SelectedItem = project;
            ProjectNum++;
            return true;
        }


        /// <summary>
        /// 获取当前工程(工程切换)
        /// </summary>
        public void ProjectTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = projectTab.SelectedIndex;//当前工程索引
            if (projectTab.Items.Count == 0)
                CurrentPojectIndex = 0;
            else
            {
                try
                {
                    CurrentPojectIndex = ProjectList.projects.IndexOf((ProjectItem)projectTab.Items[index]);
                    UpdateRate();
                    MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
                    ProjectItem item = mainwin.GetCurrentProject();
                    mainwin.CurWorkspaceSizeShow(item.Canvas.Width.ToString(), mainwin.GetCurrentProject().Canvas.Height.ToString());//状态栏显示工作区大小
                    mainwin.CurProjectShow(item.Name.Split('.')[0]);//状态栏显示当前工程
                    mainwin.CurProjectAddressShow(item.Path, item.Path.Split('\\')[item.Path.Split('\\').Length - 2]);//状态栏显示当前工程地址
                }
                catch
                {
                    return;
                }
            }
        }

        internal void ProjectTab_SelectionChanged()
        {
            int index = projectTab.SelectedIndex;//当前工程索引
            if (projectTab.Items.Count == 0)
                CurrentPojectIndex = 0;
            else
            {
                try
                {
                    CurrentPojectIndex = ProjectList.projects.IndexOf((ProjectItem)projectTab.Items[index]);
                    UpdateRate();
                }
                catch
                {
                    return;
                }
            }
        }

        /// <summary>
        /// 关闭工程（当前或其他工程）
        /// </summary>
        public void ProjectItem_Close(object sender, RoutedEventArgs e)
        {
            Button closeButton = sender as Button;
            string num = closeButton.Tag.ToString();
            foreach (ProjectItem item in projectTab.Items)
            {
                if (item.Num == long.Parse(num))
                {
                    if (MessageBox.Show("是否保存当前工程？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                    {
                        item.SaveProject();//先保存后关闭
                        MessageBox.Show("项目已保存");
                    }
                    ProjectList.projects.Remove(item);
                    ProjectTab_SelectionChanged();//解决关闭左侧工程，出现当前工程索引溢出；以及画布背景透明
                    projectTab.ItemsSource = ProjectList.projects;
                    projectTab.Items.Refresh();
                    if (ProjectList.projects.Count == 0)
                    {//若无工程，状态栏显示空
                        this.CurProjectShow("Null");//工程名为空
                        this.CurWorkspaceSizeShow("0", "0");//工作区大小为空
                        this.CurProjectAddressShow("Null", "Null");//工程地址为空
                    }
                    return;
                }
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

        private DataTemplate _crossTemplate = null;
        public DataTemplate CrossTemplate
        {
            get { return _crossTemplate; }
            set { _crossTemplate = value; }
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TPISComponent)
            {
                return _componentTemplate;
            }
            if (item is TPISLine)
            {
                return _lineTemplate;
            }
            if (item is ResultCross)
            {
                return _crossTemplate;
            }
            else
            {
                return _selectionTemplate;
            }
        }
    }
    #endregion
}



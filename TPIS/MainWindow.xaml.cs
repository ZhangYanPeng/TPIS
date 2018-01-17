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
// TPIS.TPISCanvas;


namespace TPIS
{
    /// <summary>
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<BaseType> TypeList { get; set;  } //所有元件列表
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

        private void loadComponentType()
        {
            ComponentType ct = new ComponentType { Id = 1, PicPath = "Images/element/Turbin1.png" , Name="元件1"};
            ComponentType ct1 = new ComponentType { Id = 2, PicPath = "Images/element/TeeValve.png", Name = "元件2" };
            BaseType bt = new BaseType( );
            bt.Name = "元件簇1";
            bt.ComponentTypeList.Add(ct);
            bt.ComponentTypeList.Add(ct1);

            ComponentType ct2 = new ComponentType { Id = 3, PicPath = "Images/element/Calorifier.png", Name = "元件3" };
            ComponentType ct3 = new ComponentType { Id = 4, PicPath = "Images/element/Chimney.png", Name = "元件4" };
            BaseType bt1 = new BaseType();
            bt1.Name = "元件簇2";
            bt1.ComponentTypeList.Add(ct2);
            bt1.ComponentTypeList.Add(ct3);

            TypeList = new List<BaseType>();
            TypeList.Add(bt);
            TypeList.Add(bt1);
            this.ElementList.ItemsSource = TypeList;
            this.ElementList.Items.Refresh();
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
            int index= projectTab.SelectedIndex;//当前工程索引
            if (projectTab.Items.Count == 0)
                CurrentPojectIndex = 0;
            else
                CurrentPojectIndex = ProjectList.projects.IndexOf((ProjectItem)projectTab.Items[index]);
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
                if (item.Num== CurrentPojectIndex)
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
            if(item is Model.TPISLine)
            {
                return _lineTemplate;
            }
            else
            {
                return _selectionTemplate;
            }
        }
    }
}



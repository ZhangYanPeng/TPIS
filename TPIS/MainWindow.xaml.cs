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
        public ProjectItem CurrentPoject { get; set; } //当前激活工程

        /// <summary>
        /// 光标操作
        /// 0：默认操作
        /// 1：绘制元件
        /// </summary>
        public int OperationType { get; set; }



        public MainWindow()
        {
            InitializeComponent();
            Window_Loaded();
            ProjectList = new ProjectSpace();
            loadComponentType();

            //projectTab.SelectionChanged += new SelectionChangedEventHandler(onProjectChange);
        }

        private void loadComponentType()
        {
            ComponentType ct = new ComponentType { Id = 1, PicPath = "Images/element/Turbin1.png" };
            ComponentType ct1 = new ComponentType { Id = 2, PicPath = "Images/element/TeeValve.png" };
            BaseType bt = new BaseType( );
            bt.Name = "元件簇1";
            bt.ComponentTypeList.Add(ct);
            bt.ComponentTypeList.Add(ct1);

            ComponentType ct2 = new ComponentType { Id = 3, PicPath = "Images/element/Calorifier.png" };
            ComponentType ct3 = new ComponentType { Id = 4, PicPath = "Images/element/Chimeney.png" };
            BaseType bt1 = new BaseType();
            bt.Name = "元件簇2";
            bt.ComponentTypeList.Add(ct2);
            bt.ComponentTypeList.Add(ct3);
            
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
            this.ProjectList.projects.Add(project);
            this.projectTab.ItemsSource = ProjectList.projects;
            this.projectTab.Items.Refresh();
            foreach(ProjectItem pi in projectTab.Items)
            {
                Test(pi);
            }
        }

        public void Test(ProjectItem p)
        {
            TPISComponent c = new TPISComponent(100,-100,1);
            TPISComponent c1 = new TPISComponent(200, 300, 2);
            p.Components.Add(c);
            //p.Components.Add(c1);
        }

        /// <summary>
        /// 获取当前工程
        /// </summary>
        private void ProjectTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index= projectTab.SelectedIndex;//当前工程索引
            foreach (ProjectItem item in projectTab.Items)
            {
                if (item == projectTab.Items[index])
                {
                    MessageBox.Show("当前工程索引： " + index.ToString());//当前工程为item
                    break;
                }
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
                if (item.Name.ToString() == name)
                {
                    MessageBox.Show(name);//所点击关闭按钮对应工程为item
                    ProjectList.projects.Remove(item);
                    projectTab.ItemsSource = ProjectList.projects;
                    projectTab.Items.Refresh();
                    break;
                }
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

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is TPISComponent)
            {
                return _componentTemplate;
            }
            else
            {
                return _selectionTemplate;
            }
        }
    }
}



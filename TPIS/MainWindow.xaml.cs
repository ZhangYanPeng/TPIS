﻿using System;
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
using TPIS.ConfigFile;
using System.Collections.ObjectModel;
using TPIS.Views;
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
        public TPISConfig TPISconfig { get; set; }
        public List<CalWindow> CalWins { get; set; }

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
            InitWorkSpace();
            ProjectNum = 0;
            //载入工程配置
            TPISconfig = new TPISConfig();
            CalWins = new List<CalWindow>();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
            InitTextCombox();
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
            string directoryPath = path+"\\" + pName + ".tpis";
            if (File.Exists(directoryPath))//如果路径不存在
                return false;
            ProjectCanvas pCanvas = new ProjectCanvas(width, height);
            ProjectItem project = new ProjectItem(pName, pCanvas, ProjectNum, System.IO.Path.GetFullPath(directoryPath));
            //TPISconfig.LoadCfg(project);
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
                    UpdateGrid();
                    ProjectItem item = GetCurrentProject();
                    CurWorkspaceSizeShow(item.Canvas.Width.ToString(), GetCurrentProject().Canvas.Height.ToString());//状态栏显示工作区大小
                    CurProjectShow(item.Name.Split('.')[0]);//状态栏显示当前工程
                    CurProjectAddressShow(item.Path, item.Path.Split('\\')[item.Path.Split('\\').Length - 2]);//状态栏显示当前工程地址
                    foreach(CalWindow cw in CalWins)
                    {
                        if (cw.project == GetCurrentProject())
                            cw.Show();
                        else
                            cw.Hide();
                    }
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
                    foreach (CalWindow cw in CalWins)
                    {
                        if (cw.project == GetCurrentProject())
                            cw.Show();
                        else
                            cw.Hide();
                    }
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
                    Record record = item.Records.PopUndo();
                    if (record != null)
                    {
                        //项目变更时，关闭工程提醒是否保存
                        if (MessageBox.Show("是否保存当前工程？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            item.SaveProject();//先保存后关闭
                            MessageBox.Show("项目已保存");
                        }
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

        #region 元件模块窗口折叠
        double m_WidthCache;

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //获取GridSplitterr的cotrolTemplate中的按钮btn，必须在Loaded之后才能获取到
            Button btnGrdSplitter = gsSplitterr.Template.FindName("btnExpend", gsSplitterr) as Button;
            if (btnGrdSplitter != null)
                btnGrdSplitter.Click += new RoutedEventHandler(btnGrdSplitter_Click);
        }
        
        void btnGrdSplitter_Click(object sender, RoutedEventArgs e)
        {
            double temp = grdWorkbench.Width;
            double def = 0;
            if (temp.Equals(def))
            {
                //恢复
                grdWorkbench.Width = m_WidthCache;
            }
            else
            {
                //折叠
                m_WidthCache = grdWorkbench.Width;
                grdWorkbench.Width = def;
            }
        }
        #endregion

        #region 获取ScrollViewer
        public ScrollViewer SelectScrollViewer()
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            Grid tabGrid = FindVisualChild<Grid>(mainwin.projectTab);
            Border contentBorder = tabGrid.FindName("ContentPanel") as Border;
            ContentPresenter contentPresenter = FindVisualChild<ContentPresenter>(contentBorder);
            ScrollViewer sv = contentPresenter.ContentTemplate.FindName("TPIS_ScrollViewer", contentPresenter) as ScrollViewer;
            return sv;
        }

        public childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        #endregion

        //private void View_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
        //    //ObservableCollection<TPIS.Project.ObjectBase> SelectedObjects = new ObservableCollection<TPIS.Project.ObjectBase>();
        //    //SelectedObjects = mainwin.GetCurrentProject().GetSelectedObjects();
        //    //ScrollViewer sv = SelectViewScrollViewer();
        //    mainwin.GetCurrentProject().GetSelectedObjects();
        //}
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


        private DataTemplate _textTemplate = null;
        public DataTemplate TextTemplate
        {
            get { return _textTemplate; }
            set { _textTemplate = value; }
        }

        private DataTemplate _hiddenLinkTemplate = null;
        public DataTemplate HiddenLinkTemplate
        {
            get { return _hiddenLinkTemplate; }
            set { _hiddenLinkTemplate = value; }
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
            if (item is TPISText)
            {
                return _textTemplate;
            }
            if (item is HiddenLink)
            {
                return _hiddenLinkTemplate;
            }
            else
            {
                return _selectionTemplate;
            }
        }
    }
    #endregion
}



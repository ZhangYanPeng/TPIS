using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using TPIS.Model.Common;
using TPIS.Project;

namespace TPIS.Views
{
    /// <summary>
    /// CalWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CalWindow : Window
    {
        public ProjectItem project { get; set; }
        Task<ProjectItem> task;
        CancellationTokenSource cancellationTokenSource;

        public CalWindow(ProjectItem pi)
        {
            InitializeComponent();
            project = pi;
            this.Title += " - " + pi.Name;
            CalProject.DataContext = project;
            this.Owner = (MainWindow)Application.Current.MainWindow;
            InitComponentType();
        }

        #region 选择属性
        private void InitComponentType()
        {
            List<TPISNet.EleType> types = new List<TPISNet.EleType>();
            foreach(ObjectBase obj in project.Objects)
            {
                if(obj is TPISComponent)
                {
                    TPISComponent c = obj as TPISComponent;
                    if (!types.Contains(c.eleType))
                        types.Add(c.eleType);
                }
            }
            foreach(TPISNet.EleType t in types)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = CommonTypeService.InitComponentName(t);
                comboBoxItem.DataContext = t;
                ComponentType.Items.Add(comboBoxItem);
            }
            ComponentType.Items.Refresh();
        }

        private void ComponentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                ComboBoxItem si = ComponentType.SelectedItem as ComboBoxItem;
                ComponentSel.Items.Clear();

                ComboBoxItem def = new ComboBoxItem();
                def.Content = "--请选择--";
                def.DataContext = null;
                def.IsSelected = true;
                ComponentSel.Items.Add(def);

                if (si.DataContext == null)
                    return;
                foreach (ObjectBase obj in project.Objects)
                {
                    if(obj is TPISComponent && ((TPISComponent)obj).eleType == (TPISNet.EleType)si.DataContext)
                    {
                        ComboBoxItem cbi = new ComboBoxItem();
                        cbi.Content = "#"+((TPISComponent)obj).No + " "+((TPISComponent)obj).Name;
                        cbi.DataContext = obj;
                        ComponentSel.Items.Add(cbi);
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void Component_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem si = ComponentSel.SelectedItem as ComboBoxItem;
                PropSel.Items.Clear();

                ComboBoxItem def = new ComboBoxItem();
                def.Content = "--请选择--";
                def.DataContext = null;
                def.IsSelected = true;
                PropSel.Items.Add(def);

                if (si.DataContext == null)
                    return;
                foreach (PropertyGroup pg in ((TPISComponent)si.DataContext).PropertyGroups)
                {
                    foreach(Property p in pg.Properties)
                    {
                        ComboBoxItem cbi = new ComboBoxItem();
                        cbi.Content = p.Name;
                        cbi.DataContext = p;
                        PropSel.Items.Add(cbi);
                    }
                }
            }
            catch
            {
                return;
            }
        }
        #endregion

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if(project.CalculateState)
                e.Cancel = true;
        }

        #region 计算
        public void CalculateResult()
        {
            try
            {
                //设置结果
                bool RTextExist = false;
                for (int i = 0; i < project.Objects.Count; i++)
                {
                    if (project.Objects[i] is TPISText)
                    {
                        TPISText rtext = (TPISText)project.Objects[i];
                        if (rtext.IsResult == true)
                        {
                            RTextExist = true;
                        }
                    }
                }
                if(!RTextExist){
                    project.AddResultText((int)(project.Canvas.V_width-200), 20, 20, "");
                }

                SetCalState(true);
                cancellationTokenSource = new CancellationTokenSource();
                ProjectItem pi = (ProjectItem)project.Clone();
                task = new Task<ProjectItem>(() => CalculateCurrent(pi, cancellationTokenSource.Token), cancellationTokenSource.Token);
                task.Start();
                Task cwt = task.ContinueWith(t => {
                    pi = t.Result;
                    if (pi != null)
                    {
                        //获取元件结果
                        foreach (ObjectBase obj in project.Objects)
                        {
                            foreach (ObjectBase objr in pi.Objects)
                            {
                                if (obj is TPISComponent && objr is TPISComponent && obj.No == objr.No)
                                {
                                    ((TPISComponent)obj).ResultGroups = ((TPISComponent)objr).ResultGroups;
                                    //获取接口计算结果
                                    foreach (Port port in ((TPISComponent)obj).Ports)
                                    {
                                        foreach (Port portr in ((TPISComponent)objr).Ports)
                                        {
                                            if (port.DicName == portr.DicName)
                                                port.Results = portr.Results;
                                        }
                                    }
                                    ((TPISComponent)obj).OnCaculateFinished();
                                }
                            }
                        }
                        //获取系统结果
                        project.ResultGroup = pi.ResultGroup;
                        project.Logs = pi.logs;
                        project.CalculateState = false;
                        //展示结果
                        for(int i=0; i < project.Objects.Count; i++)
                        {
                            if (project.Objects[i] is TPISText)
                            {
                                TPISText rtext = (TPISText)project.Objects[i];
                                if (rtext.IsResult == true)
                                {
                                    RTextExist = true;
                                    String result = "";
                                    int rw = 0, rh = 0;
                                    foreach (PropertyGroup pg in project.resultGroup)
                                    {
                                        foreach(Property p in pg.Properties)
                                        {
                                            if (p.ShowValue == "")
                                               continue;
                                            rh = rh + 25;
                                            rw = Math.Max(rw, (p.Name.Length + p.ShowValue.Length + 1) * 20);
                                            result += p.Name + ":" + p.ShowValue + "\r\n";
                                        }
                                    }
                                    rtext.Text = result;
                                    rtext.Position.V_width = rw;
                                    rtext.Position.V_height = rh;
                                    if (rtext.Position.V_x + rtext.Position.V_width > project.Canvas.V_width)
                                        rtext.Position.V_x = project.Canvas.V_width - rtext.Position.V_width;
                                }
                            }
                        }
                        project.OnCaculateFinished();
                    }
                });
            }
            catch
            {
                return;
            }
        }

        private void SetCalState(bool v)
        {
            if (v)
            {
                project.CalculateState = true;
            }
            else
            {
                project.CalculateState = false;
            }
        }

        public ProjectItem CalculateCurrent(object data, CancellationToken token)
        {
            try {

                ProjectItem pi = data as ProjectItem;
                pi.RebuildLink();
                CalculateInBackEnd  cal = new CalculateInBackEnd(pi.BackEnd);
                pi = cal.Calculate(pi);
                token.ThrowIfCancellationRequested();
                return pi;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        private void StartCalBtn(object sender, RoutedEventArgs e)
        {
            if (!MaxIter_Check())
                return;
            CalculateResult();
        }

        private void EndCalBtn(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource.Cancel();
            task = null;
            project.CalculateState = false;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.CalWins.Remove(this);
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Rect workArea = SystemParameters.WorkArea;
            Left = (workArea.Width - Width) / 2 + workArea.Left;
            Top = (workArea.Height - Height) / 2 + workArea.Top;
        }

        /// <summary>
        /// 检查最大迭代次数输入合法性
        /// </summary>
        /// <returns></returns>
        private bool MaxIter_Check()
        {
            try
            {
                int.Parse(MaxIter.Text);
                return true;
            }
            catch
            {
                MessageBox.Show("请输入整数");
                return false;
            }
        }
    }

    //按钮控制
    public class StartCalVisualConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //按钮控制
    public class EndCalVisualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    
}

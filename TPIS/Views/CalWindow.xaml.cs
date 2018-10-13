using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        ObservableCollection<MonitorData> LMonitors;

        public CalWindow(ProjectItem pi)
        {
            InitializeComponent();
            project = pi;
            this.Title += " - " + pi.Name;
            CalProject.DataContext = project;
            this.Owner = (MainWindow)Application.Current.MainWindow;
            InitComponentType();
            LMonitors = new ObservableCollection<MonitorData>();
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

        //选择元件类型
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
                PropertyType.SelectedIndex = 0;
                TypeSection.Visibility = Visibility.Collapsed;

                if (si.DataContext == null)
                {
                    return;
                }
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

        //选择元件
        private void Component_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem si = ComponentSel.SelectedItem as ComboBoxItem;
                if (si.DataContext == null)
                {
                    PropertyType.SelectedIndex = 0;
                    TypeSection.Visibility = Visibility.Collapsed;
                    return;
                }
                TypeSection.Visibility = Visibility.Visible;
                PropertyType.SelectedIndex = 0;
                PortSection.Visibility = Visibility.Collapsed;
                PropSection.Visibility = Visibility.Collapsed;
                btn_Mon.Visibility = Visibility.Collapsed;
            }
            catch
            {
                return;
            }
        }

        //选择属性类型
        private void PropertyType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem si = ComponentSel.SelectedItem as ComboBoxItem;

                if (PropertyType.SelectedIndex == 0)
                {
                    PortSection.Visibility = Visibility.Collapsed;
                    PropSection.Visibility = Visibility.Collapsed;
                    btn_Mon.Visibility = Visibility.Collapsed;
                }
                else if (PropertyType.SelectedIndex == 1)
                {
                    PortSection.Visibility = Visibility.Collapsed;
                    PropSection.Visibility = Visibility.Visible;
                    btn_Mon.Visibility = Visibility.Visible;

                    PropSel.Items.Clear();
                    ComboBoxItem def = new ComboBoxItem();
                    def.Content = "--请选择--";
                    def.DataContext = null;
                    def.IsSelected = true;
                    PropSel.Items.Add(def);
                    if (si.DataContext != null)
                    {
                        foreach (PropertyGroup pg in ((TPISComponent)si.DataContext).ResultGroups)
                        {
                            foreach (Property p in pg.Properties)
                            {
                                ComboBoxItem cbi = new ComboBoxItem();
                                cbi.Content = p.Name;
                                cbi.DataContext = p;
                                PropSel.Items.Add(cbi);
                            }
                        }
                    }
                }
                else
                {
                    PortSection.Visibility = Visibility.Visible;
                    if (si.DataContext != null)
                    {
                        foreach (Port port in ((TPISComponent)si.DataContext).Ports)
                        {
                            ComboBoxItem cbi = new ComboBoxItem();
                            cbi.Content = port.Name;
                            cbi.DataContext = port;
                            PortSel.Items.Add(cbi);
                        }
                    }
                    PropSection.Visibility = Visibility.Collapsed;
                    btn_Mon.Visibility = Visibility.Collapsed;
                }
            }
            catch
            {
                return;
            }
        }

        //节点变更
        private void Port_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem si = PortSel.SelectedItem as ComboBoxItem;
            try
            {
                if (si.DataContext == null)
                {
                    PropSection.Visibility = Visibility.Collapsed;
                    btn_Mon.Visibility = Visibility.Collapsed;
                    return;
                }
                PropSection.Visibility = Visibility.Visible;
                btn_Mon.Visibility = Visibility.Visible;

                PropSel.Items.Clear();
                ComboBoxItem def = new ComboBoxItem();
                def.Content = "--请选择--";
                def.DataContext = null;
                def.IsSelected = true;
                PropSel.Items.Add(def);
                if (si.DataContext != null)
                {
                    foreach (Property p in ((Port)si.DataContext).Results)
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

        #region 更新监视
        private void AddMonitor(object sender, RoutedEventArgs e)
        {
            Property p = (PropSel.SelectedItem as ComboBoxItem).DataContext as Property;
            TPISComponent c = (ComponentSel.SelectedItem as ComboBoxItem).DataContext as TPISComponent;

            MonitorData data;
            if (PropertyType.SelectedIndex == 1) {
                data = new MonitorData(c, null, p);
            }
            else
            {
                Port port = (PortSel.SelectedItem as ComboBoxItem).DataContext as Port;
                data = new MonitorData(c,port, p);
            }
            Expander expander = new Expander();
            TextBlock title = new TextBlock();
            title.Text = data.Name;
            expander.Header = title;
            DynamicPolyline fig = new DynamicPolyline();
            fig.Height = 200;
            fig.Width = 500;
            fig.Name = "Monitor" + LMonitors.Count;
            expander.Content = fig;

            Monitor.Items.Add(expander);
            Monitor.RegisterName(fig.Name, fig);
            Monitor.Items.Refresh();
            LMonitors.Add(data);
        }

        public void UpdateMonitorData(List<double> datum)
        {
            for (int i = 0; i < LMonitors.Count; i++)
            {
                LMonitors[i].AddNewData(datum[i]);
            }
            Dispatcher.InvokeAsync(ReDrawFig);
        }

        private void ReDrawFig()
        {
            for (int i = 0; i < LMonitors.Count; i++)
            {
                try
                {
                    DynamicPolyline Fig = Monitor.FindName("Monitor" + i) as DynamicPolyline;
                    Fig.Data = LMonitors[i].Data;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        
        #endregion

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


    public class MonitorData
    {
        public string Name { get; set; }
        public List<double> Data { get; set; }
        public int CNo { get; set; }
        public string PName { get; set; }
        public string PortName { get; set; }

        public MonitorData(TPISComponent c,Port port, Property p)
        {
            Name = "#" + c.No + " " + c.Name + " " + p.Name;
            CNo = c.No;
            PName = p.DicName;
            if (port == null)
                PortName = null;
            else
                PortName = port.DicName;
            Data = new List<double>();
        }

        public void AddNewData(double v)
        {
            Data.Add(v);
        }

        internal void ClearData()
        {
            Data = new List<double>();
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

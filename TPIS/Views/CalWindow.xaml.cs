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
        }
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
                                }
                            }
                        }
                        //获取系统结果
                        project.ResultGroup = pi.ResultGroup;
                        project.Logs = pi.logs;
                        project.CalculateState = false;
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
                pi = CalculateInBackEnd.Calculate(pi);
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
            CalculateResult();
        }

        private void EndCalBtn(object sender, RoutedEventArgs e)
        {
            cancellationTokenSource.Cancel();
            task = null;
            project.CalculateState = false;
        }
        
    }

    //颜色控制
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

    //颜色控制
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

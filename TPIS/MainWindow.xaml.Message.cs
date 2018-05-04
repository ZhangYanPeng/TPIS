using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Model.Common;
using TPIS.Project;
using TPIS.TPISCommand;
using TPIS.Views.Modules;

namespace TPIS
{
    partial class MainWindow : Window
    {
        private void InitializeMessage()
        {
        }

        //左侧工具栏切换
        #region

        public void ToSelectMode()
        {
            GetCurrentProject().Canvas.Operation = OperationType.SELECT;
            GetCurrentProject().Canvas.OperationParam.Clear();
            AddStraightLine.IsChecked = false;
            AddLine.IsChecked = false;
            GetCurrentProject().Select();
            foreach (BaseType bt in TypeList)
            {
                foreach (ComponentType ct in bt.ComponentTypeList)
                {
                    ct.IsChecked = false;
                }
            }
            projectTab.Focus();//WorkSpace获取焦点使方向键使能
        }

        /// <summary>
        /// 选中添加元件类型
        /// 无当前工程则不设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISComponentTypeSelected(object sender, RoutedEventArgs e)
        {
            TPISCompentView currEle = sender as TPISCompentView;
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                //工程不空可选择
                GetCurrentProject().Select();
                try
                {
                    this.GetCurrentProject().Canvas.Operation = OperationType.ADD_COMPONENT;
                    this.GetCurrentProject().Canvas.OperationParam.Clear();
                    this.GetCurrentProject().Canvas.OperationParam.Add("type", (int)currEle.Tag);

                    this.AddLine.IsChecked = false;
                    this.AddStraightLine.IsChecked = false;
                    //取消其他选中
                    foreach (BaseType bt in this.TypeList)
                    {
                        foreach (ComponentType ct in bt.ComponentTypeList)
                        {
                            if (ct.Id != (int)currEle.Tag)
                            {
                                ct.IsChecked = false;
                            }
                        }
                    }
                }
                catch
                {
                    //取消选中和焦点
                    currEle.IsChecked = false;
                    currEle.Focusable = false;
                    return;
                }
            }
            else
            {
                //取消选中和焦点
                currEle.IsChecked = false;
                currEle.Focusable = false;
            }
        }

        /// <summary>mainwin.ProjectList.projects[mainwin.CurrentPojectIndex]
        /// 取消添加元件
        /// 改为选中操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISComponentTypeUnSelected(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                TPISCompentView currEle = sender as TPISCompentView;
                GetCurrentProject().Select();
                try
                {
                    if (this.GetCurrentProject().Canvas.Operation != OperationType.ADD_COMPONENT)
                        return;
                    foreach (BaseType bt in this.TypeList)
                    {
                        foreach (ComponentType ct in bt.ComponentTypeList)
                        {
                            if (ct.IsChecked == true)
                                return;
                        }
                    }
                    this.GetCurrentProject().Canvas.Operation = OperationType.SELECT;
                    this.GetCurrentProject().Canvas.OperationParam.Clear();
                }
                catch
                {
                    currEle.IsChecked = false;
                    currEle.Focusable = false;
                    return;
                }
            }
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISLineTypeSelected(object sender, RoutedEventArgs e)
        {
            ToggleButton currEle = sender as ToggleButton;
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                GetCurrentProject().Select();
                try
                {
                    this.GetCurrentProject().Canvas.Operation = OperationType.ADD_LINE;
                    this.GetCurrentProject().Canvas.OperationParam.Clear();
                    if (currEle.Name == "AddLine")
                    {
                        this.GetCurrentProject().Canvas.OperationParam.Add("type", 1);
                        this.AddStraightLine.IsChecked = false;
                    }
                    else
                    {
                        this.GetCurrentProject().Canvas.OperationParam.Add("type", 0);

                        this.AddLine.IsChecked = false;
                    }
                    foreach (BaseType bt in this.TypeList)
                    {
                        foreach (ComponentType ct in bt.ComponentTypeList)
                        {
                            ct.IsChecked = false;
                        }
                    }
                }
                catch
                {
                    currEle.IsChecked = false;
                    currEle.Focusable = false;
                    return;
                }
            }
            else
            {
                currEle.IsChecked = false;
                currEle.Focusable = false;
            }
        }

        /// <summary>
        /// 取消画线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISLineTypeUnSelected(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                ToggleButton currEle = sender as ToggleButton;
                GetCurrentProject().Select();
                try
                {
                    if (this.GetCurrentProject().Canvas.Operation == OperationType.ADD_LINE && this.AddStraightLine.IsChecked == false && this.AddLine.IsChecked == false)
                    {
                        this.GetCurrentProject().Canvas.Operation = OperationType.SELECT;
                        this.GetCurrentProject().Canvas.OperationParam.Clear();
                    }
                }
                catch
                {
                    currEle.IsChecked = false;
                    currEle.Focusable = false;
                    return;
                }
            }
        }
        #endregion

        /// <summary>
        /// binding 工具栏rate显示
        /// </summary>
        private void UpdateRate()
        {
            Binding binding = new Binding();
            binding.Source = ProjectList.projects[CurrentPojectIndex];
            binding.Mode = BindingMode.OneWay;
            binding.Path = new PropertyPath("Rate");
            binding.Converter = new RateStrConverter();
            ToolBar toolBar = Resources["TPISToolBar"] as ToolBar;
            foreach (object item in toolBar.Items)
            {
                if (item is TextBox)
                {
                    if (((TextBox)item).Name == "CurRate")
                    {
                        ((TextBox)item).SetBinding(TextBox.TextProperty, binding);
                    }
                }
            }
        }

        public class RateStrConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value == null)
                    return DependencyProperty.UnsetValue;
                int val = (int)((double)value * 100.0);
                return val + "%";
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return null;
            }
        }

        //显示隐藏属性窗口
        private void btn_PropertyStateChange(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Tag.ToString() == "show")
            {
                btn.Tag = "hide";
                btn.ToolTip = "显示属性窗";
                PropertyWindow.Visibility = Visibility.Collapsed;
                PropertyStateChangeFig.Source = new BitmapImage(new Uri(@"Images\icon\window_show.png", UriKind.Relative));
            }
            else
            {
                btn.Tag = "show";
                btn.ToolTip = "隐藏属性窗";
                PropertyWindow.Visibility = Visibility.Visible;
                PropertyStateChangeFig.Source = new BitmapImage(new Uri(@"Images\icon\window_hide.png", UriKind.Relative));
            }
        }

        private void btn_ResultStateChange(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Tag.ToString() == "show")
            {
                btn.Tag = "hide";
                btn.ToolTip = "显示结果窗";
                ResultWindow.Visibility = Visibility.Collapsed;
                PortResults.Visibility = Visibility.Collapsed;
                ResultStateChangeFig.Source = new BitmapImage(new Uri(@"Images\icon\window_show.png", UriKind.Relative));
            }
            else
            {
                btn.Tag = "show";
                btn.ToolTip = "隐藏结果窗";
                ResultWindow.Visibility = Visibility.Visible;
                PortResults.Visibility = Visibility.Visible;
                ResultStateChangeFig.Source = new BitmapImage(new Uri(@"Images\icon\window_hide.png", UriKind.Relative));
            }
        }

        private void OpenCurveWindow_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            CurvesData.FittingWin ftw = new CurvesData.FittingWin(btn.DataContext as CurvesData.Curves);
            ftw.Owner = this;
            ftw.ShowDialog();
        }

    }

    //控制显示样式
    public class ValVisualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Visible;
            else
            {
                if ((P_Type)value != P_Type.ToSelect && (P_Type)value != P_Type.ToLine)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    //控制显示样式
    public class MeasureVisualConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return Visibility.Visible;
            else
            {
                if ((P_Type)values[1] == P_Type.ToLine)
                    return Visibility.Collapsed;
                string[] tmp = (string[])values[0];
                if (tmp.Count<String>() == 1 && tmp[0] == "")
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    //控制显示样式
    public class LineVisualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Visible;
            else
            {
                if ((P_Type)value == P_Type.ToLine)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    //控制是否在计算
    public class CalStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            else
            {
                if ((bool)value)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    //网格缩放
    public class RectConverter : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            if (mainwin.GetCurrentProject() != null)
            {
                return new System.Windows.Rect(0, 0, mainwin.GetCurrentProject().GridUintLength, mainwin.GetCurrentProject().GridUintLength);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

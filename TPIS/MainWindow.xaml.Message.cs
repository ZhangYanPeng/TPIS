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

        internal void UnselectLine()
        {
            ToolBar toolBar = ToolBar.Content as ToolBar;
            for (int i = 0; i < toolBar.Items.Count; i++)
            {
                if (toolBar.Items.GetItemAt(i) is ToggleButton)
                {
                    ToggleButton toggleButton = toolBar.Items.GetItemAt(i) as ToggleButton;
                    toggleButton.IsChecked = false;
                }
            }

            ToolBar textToolBar = TextToolBar.Content as ToolBar;
            for (int i = 0; i < textToolBar.Items.Count; i++)
            {
                if (textToolBar.Items.GetItemAt(i) is ToggleButton)
                {
                    ToggleButton toggleButton = textToolBar.Items.GetItemAt(i) as ToggleButton;
                    toggleButton.IsChecked = false;
                }
            }
        }

        internal void SetToolBarFontSize(int op)
        {
            ToolBar textToolBar = TextToolBar.Content as ToolBar;
            for (int i = 0; i < textToolBar.Items.Count; i++)
            {
                if (textToolBar.Items.GetItemAt(i) is ComboBox)
                {
                    ComboBox comboBox = textToolBar.Items.GetItemAt(i) as ComboBox;
                    if (op >= 0)
                        comboBox.SelectedIndex = op;
                    else
                        comboBox.SelectedIndex = -1;
                    comboBox.Items.Refresh();
                }
            }
        }

        internal void UnselectText()
        {
            ToolBar textToolBar = TextToolBar.Content as ToolBar;
            for (int i = 0; i < textToolBar.Items.Count; i++)
            {
                if (textToolBar.Items.GetItemAt(i) is ToggleButton)
                {
                    ToggleButton toggleButton = textToolBar.Items.GetItemAt(i) as ToggleButton;
                    toggleButton.IsChecked = false;
                }
            }
        }

        //左侧工具栏切换
        #region

        public void ToSelectMode()
        {
            GetCurrentProject().Canvas.Operation = OperationType.SELECT;
            GetCurrentProject().Canvas.OperationParam.Clear();
            UnselectLine();
            UnselectText();
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
            if (GetCurrentProject() != null)
            {
                //工程不空可选择
                GetCurrentProject().Select();
                try
                {
                    GetCurrentProject().Canvas.Operation = OperationType.ADD_COMPONENT;
                    GetCurrentProject().Canvas.OperationParam.Clear();
                    GetCurrentProject().Canvas.OperationParam.Add("type", (int)currEle.Tag);
                    UnselectLine();
                    UnselectText();
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
        public void TPISLineTypeSelected(object sender, RoutedEventArgs e)
        {
            ToggleButton currEle = sender as ToggleButton;
            if (GetCurrentProject() != null)
            {
                GetCurrentProject().Select();
                UnselectText();
                try
                {
                    GetCurrentProject().Canvas.Operation = OperationType.ADD_LINE;
                    GetCurrentProject().Canvas.OperationParam.Clear();
                    if (currEle.Name == "AddLine")
                    {
                        GetCurrentProject().Canvas.OperationParam.Add("type", 1);
                    }
                    else
                    {
                        GetCurrentProject().Canvas.OperationParam.Add("type", 0);
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

        public void TPISTextSelected(object sender, RoutedEventArgs e)
        {
            ToggleButton currEle = sender as ToggleButton;
            if (GetCurrentProject() != null)
            {
                GetCurrentProject().Select();
                try
                {
                    GetCurrentProject().Canvas.Operation = OperationType.ADD_TEXT;
                    GetCurrentProject().Canvas.OperationParam.Clear();
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
                    UnselectLine();
                    return;
                }
            }
            else
            {
                currEle.IsChecked = false;
                currEle.Focusable = false;
            }
        }

        #endregion

        #region
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

        /// <summary>
        /// binding 工具栏网格
        /// </summary>
        private void UpdateGrid()
        {
            ToolBar toolBar = Resources["TPISToolBar"] as ToolBar;
            foreach (object item in toolBar.Items)
            {
                if (item is ToggleButton)
                {
                    if (((ToggleButton)item).Name == "tsbGrid")
                    {
                        try
                        {
                            if (GetCurrentProject().GridThickness == 1)
                                ((ToggleButton)item).IsChecked = true;
                            else
                                ((ToggleButton)item).IsChecked = false;
                        } catch
                        {
                            ((ToggleButton)item).IsChecked = false;
                        }
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
        #endregion

        #region 显示隐藏属性窗口

        private void DetailsWindowClose(object sender, RoutedEventArgs e)
        {
            DetailsWindowVisible(false);
        }

        public void DetailsWindowVisible(bool v)
        {
            if(v)
                DetailsWindow.Visibility = Visibility.Visible;
            else
                DetailsWindow.Visibility = Visibility.Collapsed;
        }

        #endregion


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
            return null;
        }

        #endregion
    }

    public class ModeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            if (value is string && (string)value == "")
                return DependencyProperty.UnsetValue;
            switch ((SelMode)value)
            {
                case SelMode.None: return "全部";
                case SelMode.DesignMode: return "设计模式";
                case SelMode.CalMode: return "计算模式";
                case SelMode.InterMode: return "插值模式";
            }
            return "全部";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}

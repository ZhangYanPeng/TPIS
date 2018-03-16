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
using TPIS.Project;
using TPIS.TPISCommand;

namespace TPIS
{
    partial class MainWindow : Window
    {
        private void InitializeMessage()
        {
        }

        //左侧工具栏切换
        #region
        /// <summary>
        /// 选中添加元件类型
        /// 无当前工程则不设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISComponentTypeSelected(object sender, RoutedEventArgs e)
        {
            TPISCompentView currEle = sender as TPISCompentView;
            try
            {
                this.ProjectList.projects[this.CurrentPojectIndex].Canvas.Operation = OperationType.ADD_COMPONENT;
                this.ProjectList.projects[this.CurrentPojectIndex].Canvas.OperationParam.Clear();
                this.ProjectList.projects[this.CurrentPojectIndex].Canvas.OperationParam.Add("type", (int)currEle.Tag);

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
            catch (Exception exp)
            {
                currEle.IsChecked = false;
                return;
            }

        }

        /// <summary>
        /// 取消添加元件
        /// 改为选中操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISComponentTypeUnSelected(object sender, RoutedEventArgs e)
        {
            TPISCompentView currEle = sender as TPISCompentView;
            try
            {
                if (this.ProjectList.projects[this.CurrentPojectIndex].Canvas.Operation != OperationType.ADD_COMPONENT)
                    return;
                foreach (BaseType bt in this.TypeList)
                {
                    foreach (ComponentType ct in bt.ComponentTypeList)
                    {
                        if (ct.IsChecked == true)
                            return;
                    }
                }
                this.ProjectList.projects[this.CurrentPojectIndex].Canvas.Operation = OperationType.SELECT;
                this.ProjectList.projects[this.CurrentPojectIndex].Canvas.OperationParam.Clear();
            }
            catch (Exception exp)
            {
                currEle.IsChecked = false;
                return;
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
            try
            {
                this.ProjectList.projects[this.CurrentPojectIndex].Canvas.Operation = OperationType.ADD_LINE;
                this.ProjectList.projects[this.CurrentPojectIndex].Canvas.OperationParam.Clear();
                if (currEle.Name == "AddLine")
                {
                    this.ProjectList.projects[this.CurrentPojectIndex].Canvas.OperationParam.Add("type", 1);
                    this.AddStraightLine.IsChecked = false;
                }
                else
                {
                    this.ProjectList.projects[this.CurrentPojectIndex].Canvas.OperationParam.Add("type", 0);

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
            catch (Exception exp)
            {
                currEle.IsChecked = false;
                return;
            }
        }

        /// <summary>
        /// 取消画线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISLineTypeUnSelected(object sender, RoutedEventArgs e)
        {
            ToggleButton currEle = sender as ToggleButton;
            try
            {
                if (this.ProjectList.projects[this.CurrentPojectIndex].Canvas.Operation == OperationType.ADD_LINE && this.AddStraightLine.IsChecked == false && this.AddLine.IsChecked == false)
                {
                    this.ProjectList.projects[this.CurrentPojectIndex].Canvas.Operation = OperationType.SELECT;
                    this.ProjectList.projects[this.CurrentPojectIndex].Canvas.OperationParam.Clear();
                }
            }
            catch (Exception exp)
            {
                currEle.IsChecked = false;
                return;
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
                int val = (int) ((double)value*100.0);
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
            if(btn.Tag.ToString() == "show")
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
                ResultStateChangeFig.Source = new BitmapImage(new Uri(@"Images\icon\window_show.png", UriKind.Relative));
            }
            else
            {
                btn.Tag = "show";
                btn.ToolTip = "隐藏结果窗";
                ResultWindow.Visibility = Visibility.Visible;
                ResultStateChangeFig.Source = new BitmapImage(new Uri(@"Images\icon\window_hide.png", UriKind.Relative));
            }
        }
    }
}

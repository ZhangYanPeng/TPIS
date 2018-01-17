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
                    foreach(ComponentType ct in bt.ComponentTypeList){
                        if(ct.Id != (int)currEle.Tag)
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
                if( this.ProjectList.projects[this.CurrentPojectIndex].Canvas.Operation == OperationType.ADD_LINE && this.AddStraightLine.IsChecked == false && this.AddLine.IsChecked == false)
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

        
    }
}

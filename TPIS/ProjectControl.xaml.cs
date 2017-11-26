using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TPIS.Project;

namespace TPIS
{
    /// <summary>
    /// 工程的创建、打开、切换、存储
    /// </summary>
    public partial class MainWindow : Window
    {
        //关闭工程监听
        public void onProjectClose(object sender, RoutedEventArgs e)
        {
            //判断是否保存
        }

        //更改当前工程监听
        public void onProjectChange(object sender, RoutedEventArgs e)
        {
            TabItem tis = (TabItem)tab_project.SelectedItem;
            foreach (TabItem ti in tab_project.Items)
            {
                DockPanel dp = (DockPanel)ti.Header;
                foreach (DependencyObject child in dp.Children)
                {
                    if (child is Button)
                    {
                        if (ti.Name == tis.Name)
                        {
                            //选中tab
                            ((Button)child).Visibility = Visibility.Visible;
                        }
                        else
                        {
                            //非选中tab
                            ((Button)child).Visibility = Visibility.Hidden;
                            
                        }
                    }
                }
            }
            //设置当前工程
            currentPoject = projectList.projects[tab_project.SelectedIndex];
            currentPoject.Draw();
        }

        //建立新工程
        private void NewProject(object sender, RoutedEventArgs e)
        {
            Window new_project = new NewProject();
            new_project.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            new_project.ShowDialog();
        }

        //激活工程
        public void ActiveProject(int num)
        {
            //激活Tab
            tab_project.SelectedIndex = num;
        }

        //binding 画布
        public void CanvasBinding(ProjectCanvas pc, Canvas canvas)
        {
            Binding binding_cw = new Binding();
            binding_cw.Source = pc;
            binding_cw.Path = new PropertyPath("V_width");
            BindingOperations.SetBinding(canvas, Canvas.WidthProperty, binding_cw);

            Binding binding_ch = new Binding();
            binding_ch.Source = pc;
            binding_ch.Path = new PropertyPath("V_height");
            BindingOperations.SetBinding(canvas, Canvas.HeightProperty, binding_ch);

        }
    }
}

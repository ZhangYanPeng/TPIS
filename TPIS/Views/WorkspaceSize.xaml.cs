using System;
using System.Collections.Generic;
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
using TPIS.Project;

namespace TPIS.Views
{
    /// <summary>
    /// WorkspaceSize.xaml 的交互逻辑
    /// </summary>
    public partial class WorkspaceSize : Window
    {
        public WorkspaceSize()
        {
            InitializeComponent();
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            this.Owner = mainwin;

            string[] strArray = mainwin.GetCurrentProject().Name.Split('.');
            string projectName = strArray[0];
            proj_name.Text = projectName;
            //获取当前工程工作区大小
            canvas_width.Text = mainwin.GetCurrentProject().Canvas.Width.ToString();
            canvas_height.Text = mainwin.GetCurrentProject().Canvas.Height.ToString();
        }

        public void Reset(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            ProjectItem item = mainwin.GetCurrentProject();
            try
            {
                if (int.Parse(canvas_width.Text) <= 0 || int.Parse(canvas_height.Text) <= 0)
                {
                    MessageBox.Show("画布像素只能为正整数！", "提示", MessageBoxButton.OKCancel);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("画布像素只能为正整数！", "提示", MessageBoxButton.OKCancel);
                return;
            }
            if (item.Canvas.Width > 10 && item.Canvas.Height > 10)//画布最小10×10
            {
                item.Canvas.Width = item.WorkSpaceSize_RD(mainwin.GetCurrentProject().Objects).X < int.Parse(canvas_width.Text) ? int.Parse(canvas_width.Text) : (int)item.WorkSpaceSize_RD(mainwin.GetCurrentProject().Objects).X;
                item.Canvas.Height = item.WorkSpaceSize_RD(mainwin.GetCurrentProject().Objects).Y < int.Parse(canvas_height.Text) ? int.Parse(canvas_height.Text) : (int)item.WorkSpaceSize_RD(mainwin.GetCurrentProject().Objects).Y;
                //存储工程配置
                mainwin.TPISconfig.CANVAS_WIDTH = int.Parse(canvas_width.Text);
                mainwin.TPISconfig.CANVAS_HEIGHT = int.Parse(canvas_height.Text);
                mainwin.TPISconfig.SaveCfg();
            }
            mainwin.CurWorkspaceSizeShow(item.Canvas.Width.ToString(), item.Canvas.Height.ToString());//状态栏显示工作区大小
            this.Close();
        }

        public void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

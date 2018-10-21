using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using TPIS.Project;

namespace TPIS.Views.Warning
{
    /// <summary>
    /// Opening.xaml 的交互逻辑
    /// </summary>
    public partial class Opening : Window
    {
        public string path { get; set; }

        public Opening(string p)
        {
            InitializeComponent();
            this.ContentRendered += Opening_ContentRendered;
            path = p;
        }

        private void Opening_ContentRendered(object sender, EventArgs e)
        {
            try
            {
                MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4 * 1024 * 1024,true);
                ProjectItem obj = CommonFunction.DeserializeWithBinary(fileStream) as ProjectItem;

                obj.Num = mainwin.ProjectNum;
                obj.Path = path;
                obj.Name = path.Split('\\').Last();
                obj.RebuildLink();
                {//解决在无新建工程时打开已有项目，出现的透明背景
                    obj.GridThickness = 1;//赋初值0，使初始画布为隐藏网格
                    obj.GridUintLength = 20;//赋初值20，使初始网格单元为20×20
                    obj.BackGroundColor = mainwin.TPISconfig.CANVAS_BACKGROUNDCOLOR;
                    //((ProjectItem)obj).BackGroundColor = Brushes.White;
                }
                mainwin.ProjectList.projects.Add(obj);
                mainwin.projectTab.ItemsSource = mainwin.ProjectList.projects;
                mainwin.projectTab.Items.Refresh();
                mainwin.projectTab.SelectedItem = obj;
                mainwin.ProjectNum++;
            }
            catch
            {
                this.Close();
            }
            //关闭
            this.Close();
        }
    }
}

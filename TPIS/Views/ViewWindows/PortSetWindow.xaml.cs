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
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Model.Common;
using TPIS.Project;

namespace TPIS.Views.ViewWindows
{
    /// <summary>
    /// PortSetWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PortSetWindow : Window
    {
        public Port port;
        public TPISComponent component;
        //public ProjectSpace ProjectList { get; set; } //工程列表

        public PortSetWindow()
        {
            InitializeComponent();
            InitilView();
            Component.DataContext = component;
        }

        public void InitilView()
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            this.Owner = (MainWindow)Application.Current.MainWindow;
            foreach (ObjectBase obj in mainwin.GetCurrentProject().SelectedObjects)
            {
                if (obj is TPISComponent)
                {
                    component = obj as TPISComponent;
                    view_Item.Content = component;
                    break;
                }
            }
        }

        public void ViewRePosPort()
        {
            component.RePosPort();
        }

        public void ViewPort_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.SELECT)
            {
                Ellipse uIElement = new Ellipse();
                uIElement = (Ellipse)sender;
                Port p = (Port)(uIElement.DataContext);
                if (p.type == NodType.DefIn || p.type == NodType.Undef || p.type == NodType.DefOut && p.link == null)
                {
                    PortContext pcontext = new PortContext(p);
                    uIElement.ContextMenu = pcontext;
                }
                if (mainwin.GetCurrentProject().IsPortSetWindowOpen)
                {//Can only be changed in the view.
                    port = p;
                    PortBinding(port);
                }
                e.Handled = true;
            }
        }

        //节点binding
        public void PortBinding(Port p)
        {
            if (p != null && p is Port)
                PortEdited.DataContext = p;
            else
                PortEdited.DataContext = null;
        }


        public void ViewPort_MouseEnter(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            FrameworkElement frameworkElement = new FrameworkElement();
            frameworkElement = (FrameworkElement)sender;
            if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.SELECT)
            {

                frameworkElement.Cursor = Cursors.Hand;
                Mouse.OverrideCursor = null;
            }
        }

        #region 贴紧
        private void LU_Click(object sender, RoutedEventArgs e)
        {
            port.X = 0;
            port.Y = 0;
            ViewRePosPort();
        }

        private void LM_Click(object sender, RoutedEventArgs e)
        {
            port.X = 0;
            ViewRePosPort();
        }

        private void MU_Click(object sender, RoutedEventArgs e)
        {
            port.Y = 0;
            ViewRePosPort();
        }

        private void RU_Click(object sender, RoutedEventArgs e)
        {
            port.X = 1;
            port.Y = 0;
            ViewRePosPort();
        }

        private void RM_Click(object sender, RoutedEventArgs e)
        {
            port.X = 1;
            ViewRePosPort();
        }

        private void LD_Click(object sender, RoutedEventArgs e)
        {
            port.X = 0;
            port.Y = 1;
            ViewRePosPort();
        }

        private void MD_Click(object sender, RoutedEventArgs e)
        {
            port.Y = 1;
            ViewRePosPort();
        }

        private void RD_Click(object sender, RoutedEventArgs e)
        {
            port.X = 1;
            port.Y = 1;
            ViewRePosPort();
        }
        #endregion

        #region 编辑节点位置
        private void Port_X_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            try
            {
                double x = double.Parse(textBox.Text);
                if (x > 1)
                    x = 1;
                if(port.X != x)
                    port.X = x;
                ViewRePosPort();
            }
            catch
            {
                return;
            }
        }

        private void Port_Y_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            try
            {
                double y = double.Parse(textBox.Text);
                if (y > 1)
                    y = 1;
                if (port.Y != y)
                    port.Y = y;
                ViewRePosPort();
            }
            catch
            {
                return;
            }
        }
        #endregion

        #region 编辑元件信息
        private void C_width_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            try
            {
                int w = int.Parse(textBox.Text);
                if (w < 5)
                    w = 5;
                component.Position.Width = w;
                ViewRePosPort();
            }
            catch
            {
                return;
            }
        }

        private void C_height_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            try
            {
                int h = int.Parse(textBox.Text);
                if (h < 5)
                    h = 5;
                component.Position.Height = h;
                ViewRePosPort();
            }
            catch
            {
                return;
            }
        }

        private void PicEdit_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "选择图片文件";
            openFileDialog.Filter = "图片工程项目(*.jpg)|*.jpg";
            openFileDialog.InitialDirectory = System.IO.Path.GetFullPath(@".\WorkSpace");
            openFileDialog.FilterIndex = 1;
            openFileDialog.ValidateNames = false;
            openFileDialog.CheckFileExists = false;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;
            bool? result = openFileDialog.ShowDialog();
            if (result != true)
            {
                return;
            }
            else
            {
                string path = openFileDialog.FileName;
                component.Pic = path;
            }
        }
        #endregion

    }
}

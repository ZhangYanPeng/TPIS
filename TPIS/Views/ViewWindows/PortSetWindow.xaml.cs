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
        public ObjectBase ViewObjects;
        //public ProjectSpace ProjectList { get; set; } //工程列表

        public PortSetWindow()
        {
            InitializeComponent();
            InitilView();

        }

        public void InitilView()
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            this.Owner = (MainWindow)Application.Current.MainWindow;
            foreach (ObjectBase obj in mainwin.GetCurrentProject().SelectedObjects)
            {
                if (obj is TPISComponent)
                {
                    ViewObjects = obj;
                    view_Item.Content = ViewObjects;
                    break;
                }
            }
        }

        public void SetText(Port p)
        {
            Port_X.Text = p.x.ToString();
            Port_Y.Text = p.y.ToString();
        }

        public void PortMove(object sender, RoutedEventArgs e)
        {
            if ((bool)Text.IsChecked)
            {//按文本框改变
                port.x = Convert.ToDouble(Port_X.Text);
                port.y = Convert.ToDouble(Port_Y.Text);
            }

            if ((bool)Position.IsChecked)
            {
                Position_PortSet();
                SetText(port);
            }
            ViewRePosPort();
        }

        public void ViewRePosPort()
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            foreach (ObjectBase obj in mainwin.GetCurrentProject().SelectedObjects)
            {
                if (obj is TPISComponent)
                {
                    ((TPISComponent)obj).RePosPort();
                }
            }
        }

        public void Position_PortSet()
        {
            if ((bool)LU.IsChecked)
            {
                port.x = 0;
                port.y = 0;
            }
            if ((bool)MU.IsChecked)
            {
                port.x = 0.5;
                port.y = 0;
            }
            if ((bool)RU.IsChecked)
            {
                port.x = 1;
                port.y = 0;
            }
            if ((bool)LM.IsChecked)
            {
                port.x = 0;
                port.y = 0.5;
            }
            if ((bool)RM.IsChecked)
            {
                port.x = 1;
                port.y = 0.5;
            }
            if ((bool)LD.IsChecked)
            {
                port.x = 0;
                port.y = 1;
            }
            if ((bool)MD.IsChecked)
            {
                port.x = 0.5;
                port.y = 1;
            }
            if ((bool)RD.IsChecked)
            {
                port.x = 1;
                port.y = 1;
            }
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
                    SetText(p);
                }
                e.Handled = true;
            }
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
    }
}

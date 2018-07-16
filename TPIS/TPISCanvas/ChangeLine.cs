using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Forms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Model.Common;
using TPIS.Project;
using TPIS.TPISCanvas;
using TPIS.Views;

namespace TPIS.TPISCanvas
{
    public partial class ChangeLine
    {
        public void LineSelect(object sender, MouseButtonEventArgs e)
        {
            Polyline polyLine = sender as Polyline;
            TPISLine line = new TPISLine();
            line = (TPISLine)polyLine.DataContext;
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.GetCurrentProject().Select((ObjectBase)polyLine.DataContext);
            e.Handled = true;
        }

        public void Port_MouseEnter(object sender, MouseEventArgs e)
        {//Port感应
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            FrameworkElement frameworkElement = new FrameworkElement();
            frameworkElement = (FrameworkElement)sender;
            if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.SELECT)
            {

                frameworkElement.Cursor = Cursors.Hand;
                Mouse.OverrideCursor = null;
            }
        }

        public void Port_MouseRightButtonDown(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.SELECT)
            {
                Ellipse uIElement = new Ellipse();
                uIElement = (Ellipse)sender;
                Port port = (Port)(uIElement.DataContext);
                if(port.type == NodType.DefIn || port.type == NodType.Undef || port.type == NodType.DefOut && port.link == null)
                {
                    PortContext pcontext = new PortContext(port);
                    uIElement.ContextMenu = pcontext;
                }
                mainwin.GetCurrentProject().MovePort(port);//change port x,y move port
                e.Handled = true;
            }
        }

        public void Port_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {//Port选择
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.ADD_LINE)
            {
                Ellipse uIElement = new Ellipse();
                uIElement = (Ellipse)sender;
                Port port = (Port)(uIElement.DataContext);
                DependencyObject obj = new DependencyObject();
                obj = VisualTreeHelper.GetParent(uIElement);
                while (obj.GetType() != typeof(ProjectDesignerCanvas))
                {
                    obj = VisualTreeHelper.GetParent(obj);
                }
                ProjectDesignerCanvas designer = obj as ProjectDesignerCanvas;

                Point point = uIElement.TranslatePoint(new Point(), designer);//控件左上点
                point.X = point.X + 5;//求中心点
                point.Y = point.Y + 5;
                if (port.link == null && mainwin.GetCurrentProject().Canvas.CanLink == false)
                {
                    mainwin.GetCurrentProject().Canvas.statrPoint = point;//折线起点
                    mainwin.GetCurrentProject().Canvas.StartPort = port;//起始Port
                    mainwin.GetCurrentProject().Canvas.CanLink = true;//可以开始画线
                }
                else if (port.link == null && mainwin.GetCurrentProject().Canvas.CanLink == true)
                {
                    if ( port.MaterialType == mainwin.GetCurrentProject().Canvas.StartPort.MaterialType || port.MaterialType == TPISNet.Material.NA || mainwin.GetCurrentProject().Canvas.StartPort.MaterialType == TPISNet.Material.NA)
                    {
                        if(CheckPort(port.Type, mainwin.GetCurrentProject().Canvas.StartPort.Type)) { 
                            mainwin.GetCurrentProject().Canvas.endPoint = point;//折线终点
                            mainwin.GetCurrentProject().Canvas.EndPort = port;//终止Port
                            mainwin.GetCurrentProject().Canvas.CanLink = false;//可以终止画线
                        }
                        else
                        {
                            MessageBox.Show("两节点同为出口或入口，无法连接！");
                            mainwin.GetCurrentProject().Canvas.CanLink = false;//可以终止画线
                            mainwin.GetCurrentProject().Canvas.EndPort = null;//终止Port
                        }
                    }
                    else
                    {
                        MessageBox.Show("两节点材质不同，无法连接！");
                        mainwin.GetCurrentProject().Canvas.CanLink = false;//可以终止画线
                        mainwin.GetCurrentProject().Canvas.EndPort = null;//终止Port
                    }
                }
            }
        }

        internal bool CheckPort(NodType t1, NodType t2)
        {
            if ((t1 == NodType.Inlet || t1 == NodType.Undef) && ((t2 == NodType.Outlet || t2 == NodType.Undef)))
                return true;
            if ((t1 == NodType.Outlet || t1 == NodType.Undef) && ((t2 == NodType.Inlet || t2 == NodType.Undef)))
                return true;
            return false;
        }

        public void Port_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            Ellipse uIElement = new Ellipse();
            uIElement = (Ellipse)sender;
        }
    }
}

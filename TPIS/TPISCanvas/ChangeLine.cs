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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Project;
using TPIS.TPISCanvas;

namespace TPIS.TPISCanvas
{
    public partial class ChangeLine
    {
        public void LineSelect(object sender, MouseButtonEventArgs e)
        {
            Polyline polyLine = sender as Polyline;
            //TPISLine line = new TPISLine();
            //line = (TPISLine)polyLine.DataContext;
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Select((ObjectBase)polyLine.DataContext);
            e.Handled = true;
        }

        public void Port_MouseEnter(object sender, MouseEventArgs e)
        {//Port感应
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.Operation == Project.OperationType.ADD_LINE)
            {
                FrameworkElement frameworkElement = new FrameworkElement();
                frameworkElement = (FrameworkElement)sender;
                frameworkElement.Cursor = Cursors.Hand;
                Mouse.OverrideCursor = null;
            }
        }

        public void Port_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {//Port选择
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.Operation == Project.OperationType.ADD_LINE)
            {
                Ellipse uIElement = new Ellipse();
                uIElement = (Ellipse)sender;
                if (uIElement.Fill == Brushes.Green && mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.LinkStartPoint == false)
                {
                    mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.CanLink = true;
                    mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.LinkStartPoint = true;
                }
                    
                else if (uIElement.Fill == Brushes.Red || mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.LinkStartPoint == true)
                    mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.CanLink = false;
            }
        }

        public void Port_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            Ellipse uIElement = new Ellipse();
                uIElement = (Ellipse)sender;
            if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.LinkStartPoint == true)
            {
                mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.CanLink = false;
                mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.CanLink = true;
            }
        }

        public void Port_MouseRightButtonDown(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.Operation == Project.OperationType.ADD_LINE)
            {
                Ellipse uIElement = new Ellipse();
                uIElement = (Ellipse)sender;
                if (uIElement.Fill == Brushes.Red)
                {
                    mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.LinkStartPoint = false;
                    mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.CanLink = false;
                    mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.CanStopLink = true;
                }
            }
        }
    }
}

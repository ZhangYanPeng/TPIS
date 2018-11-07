using System;
using System.Collections.Generic;
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
using TPIS.Views;

namespace TPIS.TPISCanvas
{
    partial class DesignerLine : Canvas
    {
        public DesignerLine()
        {
            base.Loaded += new RoutedEventHandler(ReInitLineAnchorPoints);
            base.MouseRightButtonDown += new MouseButtonEventHandler(Element_MouseRightButtonDown);
        }
        public List<LineAnchorPoint> laps;
        public void InitLineAnchorPoints(long lID, TPISLine line)
        {
            laps = new List<LineAnchorPoint>();
            foreach (Object obj in this.Children)
            {
                if (obj is LineAnchorPoint)
                {
                    LineAnchorPoint ap = obj as LineAnchorPoint;
                    if (ap.lineID == lID)
                        return;
                }
            }
            for (int i = 0; i < line.Points.Count - 2; i++)
            {
                laps.Add(new LineAnchorPoint(lID, i));
                Panel.SetZIndex(laps[i], 2);
                this.Children.Add(laps[i]);
            }
            RePosLineAnchorPoints(line);
        }

        public void ReInitLineAnchorPoints(object sender, RoutedEventArgs e)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            for (int i = 0; i < mainwin.GetCurrentProject().Objects.Count; i++)
            {
                ObjectBase obj = mainwin.GetCurrentProject().Objects[i];
                if (obj is TPISLine)
                {
                    //if (!((TPISLine)obj).IsInitiAnchorPoints)
                    // {
                    //    ((TPISLine)obj).IsInitiAnchorPoints = true;
                    InitLineAnchorPoints(((TPISLine)obj).No, ((TPISLine)obj));
                    //}
                }
            }
        }
        public void RePosLineAnchorPoints(TPISLine line)
        {//重置锚点
            int i = 0;
            foreach (LineAnchorPoint lap in laps)
            {
                i++;
                {
                    //laps[i].SetValue(Canvas.TopProperty, line.Points[i + 1].Y);
                    Binding binding = new Binding();
                    binding.Source = line;
                    binding.Path = new PropertyPath("V_points");
                    binding.Converter = new AnchorPosConverter(i, "y");
                    binding.Mode = BindingMode.OneWay;
                    lap.SetBinding(Canvas.TopProperty, binding);
                }
                {
                    //laps[i].SetValue(Canvas.TopProperty, line.Points[i + 1].X);
                    Binding binding = new Binding();
                    binding.Source = line;
                    binding.Path = new PropertyPath("V_points");
                    binding.Converter = new AnchorPosConverter(i, "x");
                    binding.Mode = BindingMode.OneWay;
                    lap.SetBinding(Canvas.LeftProperty, binding);
                }
                {
                    Binding binding = new Binding();
                    binding.Source = line;
                    binding.Path = new PropertyPath("IsSelected");
                    binding.Converter = new SelectConverter();
                    lap.SetBinding(AnchorPoint.VisibilityProperty, binding);
                }
            }
        }

        void Element_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.GetCurrentProject().Canvas.Operation != Project.OperationType.SELECT)
            {
                return;
            }
            if (((TPISLine)DataContext).IsSelected)
            {
                //已被选中，不改变选择范围
                TPISContextMenu contextMenu = new TPISContextMenu(1);
                contextMenu.SetPos(e.GetPosition((Canvas)VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(this))));
                contextMenu.setLine(DataContext as TPISLine);
                this.ContextMenu = contextMenu;
            }
            else if (!((TPISLine)DataContext).IsSelected)
            {
                //之前未被选中，或改为改变大小操作，单独选中该元件
                mainwin.GetCurrentProject().Select((ObjectBase)DataContext);
                TPISContextMenu contextMenu = new TPISContextMenu(1);
                contextMenu.SetPos(e.GetPosition((Canvas)VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(this))));
                contextMenu.setLine(DataContext as TPISLine);
                this.ContextMenu = contextMenu;
            }
            e.Handled = true;
        }
    }
}

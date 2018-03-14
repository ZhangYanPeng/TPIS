using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Command;
using TPIS.Model;
using TPIS.Project;
using TPIS.Views;

namespace TPIS.TPISCanvas
{
    public partial class ProjectDesignerCanvas : Canvas
    {
        //private Point? rubberbandSelectionStartPoint = null;

        private SelectionService selectionService;
        internal SelectionService SelectionService
        {
            get
            {
                if (selectionService == null)
                    selectionService = new SelectionService(this);

                return selectionService;
            }
        }


        /// <summary>
        /// 初始化函数，添加画布事件
        /// 进入离开画布形状事件
        /// </summary>
        public ProjectDesignerCanvas() : base()
        {
            base.MouseEnter += new MouseEventHandler(CanvasMouseEnter);
            base.MouseLeave += new MouseEventHandler(CanvasMouseLeave);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(ComponentMouseLButtonDown);
            base.MouseMove += new MouseEventHandler(ComponentMouseMove);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(MouseLBtnClickEmpty);
            base.MouseMove += new MouseEventHandler(MouseLBtnSelectMove);
            base.MouseRightButtonDown += new MouseButtonEventHandler(MouseCanvasRightButtonDown);

            pline = new Polyline();
            pline.Stroke = Brushes.Red;
            pline.StrokeThickness = 2;
            this.Children.Add(pline);

            mainwin = (MainWindow)Application.Current.MainWindow;
        }

        public void MouseCanvasRightButtonDown(object sender, MouseEventArgs e)
        {
            if (mainwin.GetCurrentProject().Canvas.Operation != Project.OperationType.SELECT)
            {
                return;
            }
            mainwin.GetCurrentProject().Select();
            TPISContextMenu contextMenu = new TPISContextMenu(2);
            contextMenu.SetPos(e.GetPosition(this));
            ContextMenu = contextMenu;
            e.Handled = true;
        }
    }
}

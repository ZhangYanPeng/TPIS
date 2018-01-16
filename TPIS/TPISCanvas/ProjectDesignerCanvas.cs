﻿using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using TPIS.Command;

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
            checkBox = new CheckBox();
            checkBox.Content = "画线";
            this.Children.Add(checkBox);

            polyLineCB = new CheckBox();
            polyLineCB.Content = "画斜线";
            SetTop(polyLineCB, 100.0);
            this.Children.Add(polyLineCB);

            plines = new List<Polyline>();
        }  
    }
}

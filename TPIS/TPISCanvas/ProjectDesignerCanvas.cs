using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using TPIS.Command;

namespace TPIS.TPISCanvas
{
    public partial class ProjectDesignerCanvas : Canvas
    {
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
        public ProjectDesignerCanvas()
        {
            checkBox = new CheckBox();
            checkBox.Content = "画线";
            this.Children.Add(checkBox);

            plines = new List<Polyline>();

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            base.MouseMove += new MouseEventHandler(Canvas_MouseMove);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(Canvas_MouseLeftButtonDown);
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            base.MouseRightButtonDown += new MouseButtonEventHandler(Canvas_MouseRightButtonDown);
        }

        /// <summary>
        /// 画折线
        /// </summary>
        /// 

        bool flag = false;
        public CheckBox checkBox;//是否画线
        public List<Polyline> plines;//画多条折线
        private int count = -1;

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == false)
                return;
            plines[count].Points[plines[count].Points.Count - 1] = e.GetPosition(this);
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (checkBox.IsChecked==true)
            {
                if (flag == false)
                {
                    flag = true;//开始画线
                    count++;//只有首击左键时计数增1
                    Polyline pline = new Polyline();
                    pline.Stroke = Brushes.Black;
                    pline.StrokeThickness = 2;
                    this.Children.Add(pline);
                    plines.Add(pline);
                }
                plines[count].Points.Add(e.GetPosition(this));
                if (plines[count].Points.Count == 1)
                    plines[count].Points.Add(e.GetPosition(this));                
            }
        }

        private void Canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            flag = false;//结束画线
        }
    }
}

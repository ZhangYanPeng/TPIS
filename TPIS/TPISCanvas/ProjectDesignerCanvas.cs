﻿using System;
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
        bool flag = false;
        public CheckBox checkBox;//画线选择框
        public List<Polyline> plines;//折线集合
        private int count = -1;
        public Point p1, p2, tmp;

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

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            //base.MouseLeftButtonDown += new MouseButtonEventHandler(Canvas_MouseLeftButtonDown);//会出现线性进入问题
            if (checkBox.IsChecked == true)
            {
                /*首击左键确定起点*/
                if (flag == false)
                {
                    flag = true;//开始画线
                    count++;//一条折线画完-计数增1

                    Polyline pline = new Polyline();
                    pline.Stroke = Brushes.Black;
                    pline.StrokeThickness = 2;

                    plines.Add(pline);
                    this.Children.Add(plines[count]);

                    p1 = e.GetPosition(this);
                    plines[count].Points.Add(p1);//起点
                    plines[count].Points.Add(p1);//初始化折线（四个点）
                    plines[count].Points.Add(p1);
                    plines[count].Points.Add(p1);
                }
                /*再击左键固定终点*/
                else
                {
                    flag = false;//结束画线
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //base.MouseMove += new MouseEventHandler(Canvas_MouseMove);
            if (flag == false)
                return;
            /*移动中确定拐点和终点*/
            p2 = e.GetPosition(this);
            tmp.X = (p1.X + p2.X) / 2;
            tmp.Y = p1.Y;
            plines[count].Points[1] = tmp;//拐点1
            tmp.Y = p2.Y;
            plines[count].Points[2] = tmp;//拐点2
            plines[count].Points[3] = e.GetPosition(this);//终点
        }
    }
}

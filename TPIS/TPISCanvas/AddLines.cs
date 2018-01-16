﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using static System.Math;
using System;
using Forms = System.Windows.Forms;

namespace TPIS.TPISCanvas
{
    public partial class ProjectDesignerCanvas : Canvas
    {
        /// <summary>
        /// 画折线
        /// </summary>
        /// 

        bool flag = false;
        public CheckBox checkBox, polyLineCB;//是否画线
        public List<Polyline> plines;//画多条折线
        private int count = -1;
        Point p1, p2, tmp;

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
                    p1 = e.GetPosition(this);
                    InitalLine(e);
                }
                /*再击左键续线*/
                else
                {
                    p1 = p2;
                    InitalLine(e);
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
            if ( || (Forms.Control.ModifierKeys & Forms.Keys.Shift) == Forms.Keys.Shift )
            {
                DrawLine(e);//Shift直线
            }
            else
            {
                //DrawPolyLine(e);
            }
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            flag = false;//结束画线
        }

        /*初始化线段*/
        private void InitalLine(MouseButtonEventArgs e)
        {
            count++;
            Polyline pline = new Polyline();
            pline.Stroke = Brushes.Black;
            pline.StrokeThickness = 2;

            plines.Add(pline);
            this.Children.Add(plines[count]);

            plines[count].Points.Add(p1);//起点
            plines[count].Points.Add(p1);//初始化折线（四个点）
            plines[count].Points.Add(p1);
            plines[count].Points.Add(p1);
        }

        /*折线*/
        //private void DrawPolyLine(MouseEventArgs e)
        //{
        //    p2 = e.GetPosition(this);
        //    tmp.X = (p1.X + p2.X) / 2;
        //    tmp.Y = p1.Y;
        //    plines[count].Points[1] = tmp;//拐点1
        //    tmp.Y = p2.Y;
        //    plines[count].Points[2] = tmp;//拐点2
        //    plines[count].Points[3] = e.GetPosition(this);//终点
        //}

        /*直线*/
        private void DrawLine(MouseEventArgs e)
        {
            p2 = e.GetPosition(this);
            if (Abs(p2.X - p1.X) >= Abs(p2.Y - p1.Y)) p2.Y = p1.Y;
            else p2.X = p1.X;
            plines[count].Points[1] = p2;//拐点1
            plines[count].Points[2] = p2;//拐点2
            plines[count].Points[3] = p2;//终点
        }
    }
}

using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using static System.Math;
using System;
using Forms = System.Windows.Forms;
using TPIS.Model;
using TPIS.Project;

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
        Point p1, p2;
        public Polyline pline;
        public long count=0;

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (checkBox.IsChecked == true)
            {
                /*首击左键确定起点*/
                if (flag == false)
                {
                    count++;
                    flag = true;//开始画线
                    p1 = e.GetPosition(this);
                    pline.Points.Add(p1);
                }
                /*再击左键续线*/
                else
                    p1 = p2;//衔接
                pline.Points.Add(p1);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (flag == false)
                return;
            /*移动中确定拐点和终点*/
            p2 = e.GetPosition(this);
            if (polyLineCB.IsChecked==false || (Forms.Control.ModifierKeys & Forms.Keys.Shift) == Forms.Keys.Shift)
            {
                if (Abs(p2.X - p1.X) >= Abs(p2.Y - p1.Y)) p2.Y = p1.Y;
                else p2.X = p1.X;
            }
            pline.Points[pline.Points.Count - 1] = p2;
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            flag = false;//结束画线
            SubstitutionLine();
        }

        private void SubstitutionLine()
        {
            TPISLine line = new TPISLine();
            line.LNum = count;
            foreach (Point p in pline.Points)
                line.Points.Add(p);
            //line.Points = pline.Points;
            pline.Points.Clear();
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Objects.Add(line);
        }

        //private void InitalLine(MouseButtonEventArgs e)
        //{
        //    count++;
        //    Polyline pline = new Polyline();
        //    pline.Stroke = Brushes.Black;
        //    pline.StrokeThickness = 2;

        //    plines.Add(pline);
        //    this.Children.Add(plines[count]);

        //    plines[count].Points.Add(p1);//起点
        //    if (pline.Points.Count  == 1)
        //        pline.Points.Add(p1);
        //}

        /*初始化线段*/
        //private void InitalLine(MouseButtonEventArgs e)
        //{
        //    count++;
        //    Polyline pline = new Polyline();
        //    pline.Stroke = Brushes.Black;
        //    pline.StrokeThickness = 2;

        //    plines.Add(pline);
        //    this.Children.Add(plines[count]);

        //    plines[count].Points.Add(p1);//起点
        //    plines[count].Points.Add(p1);//初始化折线（四个点）
        //    plines[count].Points.Add(p1);
        //    plines[count].Points.Add(p1);
        //}

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
        //private void DrawLine(MouseEventArgs e)
        //{
        //    p2 = e.GetPosition(this);
        //    if (Abs(p2.X - p1.X) >= Abs(p2.Y - p1.Y)) p2.Y = p1.Y;
        //    else p2.X = p1.X;
        //    plines[count].Points[1] = p2;//拐点1
        //    plines[count].Points[2] = p2;//拐点2
        //    plines[count].Points[3] = p2;//终点
        //}
    }
}

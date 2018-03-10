using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using static System.Math;
using Forms = System.Windows.Forms;
using TPIS.Model;
using static TPIS.Model.TPISLine;
using System;

namespace TPIS.TPISCanvas
{
    public partial class ProjectDesignerCanvas : Canvas
    {
        /// <summary>
        /// 画折线
        /// </summary>
        /// 
        bool flag;
        public bool IsStraight { get; set; } //是否直线
        Point p1, p2;
        public Polyline pline;
        public long count=0;
        public MainWindow mainwin;
        
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            if ( mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.Operation == Project.OperationType.ADD_LINE &&
                mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.CanLink==true)
            {
                if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.OperationParam["type"] == 0)
                    this.IsStraight = true;
                else
                    this.IsStraight = false;

                /*首击左键确定起点*/
                if (flag == false)
                {
                    count++;
                    flag = true;//开始画线
                    //p1 = e.GetPosition(this);
                    p1 = mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.statrPoint;//起点
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
            if ( IsStraight || (Forms.Control.ModifierKeys & Forms.Keys.Shift) == Forms.Keys.Shift)
            {
                if (Abs(p2.X - p1.X) >= Abs(p2.Y - p1.Y)) p2.Y = p1.Y;
                else p2.X = p1.X;
            }
            pline.Points[pline.Points.Count - 1] = p2;
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.CanStopLink==true)
            {
                //pline.Points.Add(e.GetPosition(this));
                pline.Points.Add(mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.endPoint);//终点
                flag = false;//结束画线
                SubstitutionLine();
                mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.CanStopLink = false;
            }
        }

        private void SubstitutionLine()
        {
            TPISLine line = new TPISLine();
            line.LNum = count;
            if (IsStraight)
                line.LType = LineType.Straight;
            else
                line.LType = LineType.Slash;
            line.isSelected = false;
            foreach (Point p in pline.Points)
                line.Points.Add(p);
            //line.Points = pline.Points;
            pline.Points.Clear();
            mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Objects.Add(line);
            InitLineAnchorPoints(line.LNum, line);//初始化锚点
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

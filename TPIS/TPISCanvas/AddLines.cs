using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace TPIS.TPISCanvas
{
    public partial class ProjectDesignerCanvas : Canvas
    {
        /// <summary>
        /// 画折线
        /// </summary>
        /// 

        bool flag = false;
        public System.Windows.Controls.CheckBox checkBox;//是否画线
        public List<Polyline> plines;//画多条折线
        private int count = -1;
        Point p1, p2, tmp;
        ConsoleKeyInfo cki;
        
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
                    //count++;//一条折线画完-计数增1
                    InitalLine(e);
                }
                /*再击左键续线*/
                else
                {
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
            p2 = e.GetPosition(this);
            tmp.X = (p1.X + p2.X) / 2;
            tmp.Y = p1.Y;
            plines[count].Points[1] = tmp;//拐点1
            tmp.Y = p2.Y;
            plines[count].Points[2] = tmp;//拐点2
            plines[count].Points[3] = e.GetPosition(this);//终点
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            flag = false;//结束画线
        }

        private void InitalLine(MouseButtonEventArgs e)
        {
            count++;
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
    }
}

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
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Project;

namespace TPIS.TPISCanvas
{

    /// <summary>
    /// 自定义事件转换
    /// </summary>
    public class AnchorPosConverter : IValueConverter
    {
        public int index { get; set; }
        public string path { get; set; }
        public AnchorPosConverter(int i, string p)
        {
            index = i;
            path = p;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            else
            {
                ObservableCollection<Point> points = value as ObservableCollection<Point>;
                if (path == "x")
                    return (double)points[index].X - 4;
                else
                    return (double)points[index].Y - 4;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class LineAnchorPoint : UserControl
    {
        long lineID;
        int LineAnchorPointID;
        bool IsDrag { get; set; }
        public LineAnchorPoint(long lID, int apID)
        {
            lineID = lID;
            LineAnchorPointID = apID;
            Rectangle rect = new Rectangle()
            {
                Width = 8,
                Height = 8,
                Fill = Brushes.LightGoldenrodYellow,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
            };
            this.Width = 8;
            this.Height = 8;
            this.Content = rect;
            base.MouseEnter += new MouseEventHandler(Element_MouseEnter);
            base.MouseLeave += new MouseEventHandler(Element_MouseLeave);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(Anchor_MouseLeftDown);
            base.MouseMove += new MouseEventHandler(Anchor_MouseMove);
            base.MouseUp += new MouseButtonEventHandler(Anchor_MouseUp);
        }

        void Element_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.SizeAll;
            Mouse.OverrideCursor = null;
        }

        public void Element_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        void Anchor_MouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            IsDrag = true;
            this.CaptureMouse();
            e.Handled = true;
        }

        void Anchor_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured)
                    this.CaptureMouse();
                if (!IsDrag)
                    return;
                Point endPoint = e.GetPosition((ProjectDesignerCanvas)this.Parent);

                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;

                foreach (ObjectBase obj in mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Objects)
                {
                    if (obj.GetType() == typeof(TPISLine))
                    {
                        TPISLine line = (TPISLine)obj;
                        if (this.lineID == line.LNum)//确定线
                        {
                            if (line.Points.Count == 3)//一个锚点
                            {
                                line.PointTo(1, endPoint);//可移动
                                //return; ;
                            }
                            else if (line.LType == TPISLine.LineType.Straight)
                            {
                                if (this.LineAnchorPointID == 0)//前端点
                                {
                                    Point tmp = new Point();
                                    tmp = line.Points[1];
                                    if (line.Points[0].Y == line.Points[1].Y && line.Points[2].Y != line.Points[1].Y)//前两点在水平线上,防止三点共线时的Bug
                                    {
                                        tmp.X = endPoint.X;
                                        line.PointTo(1, tmp);
                                        tmp.Y = line.Points[2].Y;
                                        line.PointTo(2, tmp);
                                    }
                                    else//前两点在垂直线上
                                    {
                                        tmp.Y = endPoint.Y;
                                        line.PointTo(1, tmp);
                                        tmp.X = line.Points[2].X;
                                        line.PointTo(2, tmp);
                                    }
                                }
                                else if (LineAnchorPointID + 3 == line.Points.Count)//后端点
                                {
                                    Point tmp = new Point();
                                    tmp = line.Points[line.Points.Count - 2];
                                    if (line.Points[line.Points.Count - 1].Y == line.Points[line.Points.Count - 2].Y &&
                                        line.Points[line.Points.Count - 2].Y != line.Points[line.Points.Count - 3].Y)//后两点在水平线上,防止三点共线时的Bug
                                    {//当终点和后端点所在直线为坐标线时，可执行
                                        tmp.X = endPoint.X;
                                        line.PointTo(line.Points.Count - 2, tmp);
                                        tmp.Y = line.Points[line.Points.Count - 3].Y;
                                        line.PointTo(line.Points.Count - 3, tmp);
                                    }
                                    else//后两点在垂直线上
                                    {
                                        tmp.Y = endPoint.Y;
                                        line.PointTo(line.Points.Count - 2, tmp);
                                        tmp.X = line.Points[line.Points.Count - 3].X;
                                        line.PointTo(line.Points.Count - 3, tmp);
                                    }
                                }
                                else//中间端点
                                {
                                    Point p1, p2, p3;
                                    p1 = line.Points[this.LineAnchorPointID];
                                    p2 = line.Points[this.LineAnchorPointID + 1];
                                    p3 = line.Points[this.LineAnchorPointID + 2];
                                    Point tmp = new Point();
                                    tmp = p1;
                                    if ((p1.X == p2.X && p2.X != p3.X))//前两点在垂直线上线，后两点不在同一条垂直线上
                                    {
                                        tmp.X = endPoint.X;
                                        line.PointTo(this.LineAnchorPointID, tmp);
                                        line.PointTo(this.LineAnchorPointID + 1, endPoint);
                                        tmp = p3;
                                        tmp.Y = endPoint.Y;
                                        line.PointTo(this.LineAnchorPointID + 2, tmp);
                                    }
                                    else if ((p1.Y == p2.Y && p2.Y != p3.Y))//前两点在垂直线上线，或后两点不在在同一条垂直线上
                                    {
                                        tmp.Y = endPoint.Y;
                                        line.PointTo(this.LineAnchorPointID, tmp);
                                        line.PointTo(this.LineAnchorPointID + 1, endPoint);
                                        tmp = p3;
                                        tmp.X = endPoint.X;
                                        line.PointTo(this.LineAnchorPointID + 2, tmp);
                                    }
                                    else if (p1.X == p2.X && p2.X == p3.X)//三点共垂直线
                                    {
                                        tmp.X = endPoint.X;
                                        line.PointTo(this.LineAnchorPointID, tmp);
                                        line.PointTo(this.LineAnchorPointID + 1, endPoint);
                                        tmp = p3;
                                        tmp.Y = endPoint.Y;
                                        line.PointTo(this.LineAnchorPointID + 2, tmp);
                                    }
                                    else if (p1.Y == p2.Y && p2.Y == p3.Y)//三点共水平线
                                    {
                                        tmp.Y = endPoint.Y;
                                        line.PointTo(this.LineAnchorPointID, tmp);
                                        line.PointTo(this.LineAnchorPointID + 1, endPoint);
                                        tmp = p3;
                                        tmp.X = endPoint.X;
                                        line.PointTo(this.LineAnchorPointID + 2, tmp);
                                    }
                                }
                            }
                            else
                                line.PointTo(this.LineAnchorPointID + 1, endPoint);//确定点
                        }
                    }
                }
            }
            else
            {
                if (this.IsMouseCaptured) this.ReleaseMouseCapture();
            }

            e.Handled = true;
        }

        void Anchor_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured) this.ReleaseMouseCapture();
            IsDrag = false;
            e.Handled = true;
        }
    }

    partial class ProjectDesignerCanvas : Canvas
    {
        public List<LineAnchorPoint> laps;
        public void InitLineAnchorPoints(long lID, TPISLine line)
        {
            laps = new List<LineAnchorPoint>();
            for (int i = 0; i < line.Points.Count - 2; i++)
            {
                laps.Add(new LineAnchorPoint(lID, i));
                this.Children.Add(laps[i]);
            }
            RePosLineAnchorPoints(line);
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
                    binding.Path = new PropertyPath("Points");
                    binding.Converter = new AnchorPosConverter(i, "y");
                    binding.Mode = BindingMode.OneWay;
                    lap.SetBinding(Canvas.TopProperty, binding);
                }
                {
                    //laps[i].SetValue(Canvas.TopProperty, line.Points[i + 1].X);
                    Binding binding = new Binding();
                    binding.Source = line;
                    binding.Path = new PropertyPath("Points");
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
    }
}

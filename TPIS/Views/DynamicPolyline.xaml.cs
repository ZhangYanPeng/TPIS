using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TPIS.Views
{
    /// <summary>
    /// DynamicPolyline.xaml 的交互逻辑
    /// </summary>
    public partial class DynamicPolyline : UserControl
    {
        List<double> data;
        public List<double> Data {
            get=>data;
            set {
                data = value;
                ReDraw();
            }
        }
        
        double X_Offset;
        double Y_Offset;
        double CWidth;
        double CHeight;
        List<Point> points;

        //重画
        private void ReDraw()
        {
            CalAxis();
            DrawBackGround();
            TransformToView();
        }

        private void DrawBackGround()
        {
            CWidth = Width - 70;
            CHeight = Height - 50;
            X_Offset = 0;
            Y_Offset = Height - 50;
            LineCanvas.Width = Width - 70;
            LineCanvas.Height = Height - 50;

            DrawYAxis();
        }

        private void DrawYAxis()
        {
            for( int i= BackgoundCanvas.Children.Count-1; i >= 0; i--)
            {
                UIElement uie = BackgoundCanvas.Children[i];
                if ( !(uie is Canvas))
                {
                    BackgoundCanvas.Children.Remove(uie);
                }
            }
            //画边框
            Rectangle rect = new Rectangle();
            Canvas.SetLeft(rect, 49);
            Canvas.SetTop(rect, 19);
            rect.Width = CWidth+2;
            rect.Height = CHeight+2;
            rect.Stroke = Brushes.Black;
            rect.StrokeThickness = 2;
            BackgoundCanvas.Children.Add(rect);

            //横刻度
            for(int i = 0; i <= 6; i++)
            {
                Line l = new Line();
                l.X1 = 50 + i * CWidth / 6;
                l.X2 = 50 + i * CWidth / 6;
                l.Y1 = Height - 30;
                l.Y2 = Height - 26;
                l.Stroke = Brushes.Black;
                l.StrokeThickness = 2;
                BackgoundCanvas.Children.Add(l);

                TextBlock text = new TextBlock();
                text.Text = ((i * 10) + TimeStart).ToString();
                text.FontSize = 10;
                Canvas.SetLeft(text, 50 + i * CWidth / 6 - 5);
                Canvas.SetTop(text, Height - 24);
                BackgoundCanvas.Children.Add(text);
            }

            //纵刻度
            for (int i = 0; ; i++)
            {
                if (i * MinMeasure + Minium > Maxium)
                    break;
                Line l = new Line();
                l.X1 = 46;
                l.X2 = 50;
                l.Y1 = Height - 30 - i * CHeight / ((Maxium - Minium) / MinMeasure);
                l.Y2 = Height - 30 - i * CHeight / ((Maxium - Minium) / MinMeasure);
                l.Stroke = Brushes.Black;
                l.StrokeThickness = 2;
                BackgoundCanvas.Children.Add(l);

                TextBlock text = new TextBlock();
                text.Text = (((i * MinMeasure) + Minium)/MinMeasure).ToString("0.0");
                text.FontSize = 10;
                Canvas.SetLeft(text,20);
                Canvas.SetTop(text, Height - 30 - i * CHeight / ((Maxium - Minium) / MinMeasure)-5);
                BackgoundCanvas.Children.Add(text);
            }
        }

        //转化为坐标
        private void TransformToView()
        {
            points = new List<Point>();
            for(int i= TimeStart; i <= TimeEnd; i++)
            {
                if (i >= data.Count)
                    break;
                double tx = X_Offset + (i - TimeStart) * CWidth / 60;
                double ty = Y_Offset - (data[i] - Minium) / (Maxium - Minium) * CHeight;
                points.Add(new Point(tx, ty));
            }
            TrendLine.Points = new PointCollection(points);
        }

        public double MinMeasure { get; set; }
        public double Maxium { get; set; }
        public double Minium { get; set; }
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }

        public DynamicPolyline()
        {
            InitializeComponent();
        }

        #region 确定坐标轴
        private void CalAxis()
        {
            CalYAxisParam();
            CalXAxisParam();
        }

        private void CalXAxisParam()
        {
            if (Data == null || Data.Count < 61)
            {
                TimeStart = 0;
                TimeEnd = 60;
            }
            else
            {
                TimeStart = Data.Count - 61;
                TimeEnd = Data.Count;
            }
        }

        internal void CalYAxisParam()
        {
            if (Data == null || Data.Count == 0)
            {
                Maxium = 1;
                Minium = 0;
                MinMeasure = 0.1;
                return;
            }

            double max = Data[0], min = Data[0];
            for(int i=1; i<Data.Count; i++)
            {
                max = Math.Max(max, Data[i]);
                min = Math.Min(min, Data[i]);
            }

            int measurepow = 0;
            if(max == min) {
                //确定最小分度和最大最小值
                measurepow = CalMinMeasure(max);
                MinMeasure = Math.Pow(10, measurepow);
            }
            else
            {
                //确定最小分度和最大最小值
                measurepow = CalMinMeasure(max-min);
                MinMeasure = Math.Pow(10, measurepow);
            }

            CalMaxMinAxis(max, min);

            for (double i = Minium; i <= Maxium; i += MinMeasure)
            {
                System.Console.WriteLine(i);
            }
        }

        private void CalMaxMinAxis(double max, double min)
        {
            if(max == min)
            {
                double tmp = (Math.Floor(max / MinMeasure)) * MinMeasure;
                Minium = tmp - 5 * MinMeasure;
                Maxium = tmp + 5 * MinMeasure;
            }
            else
            {
                Minium = (Math.Floor(min / MinMeasure)) * MinMeasure;
                Maxium = (Math.Ceiling(max / MinMeasure)) * MinMeasure;
            }
        }

        internal int CalMinMeasure(double value)
        {
            if (value == 0)
                return -1;
            int pow = 0;
            if (Math.Abs(value) >= 10)
            {
                while (Math.Abs(value) >= 10)
                {
                    value = value / 10;
                    pow++;
                }
            }
            if(Math.Abs(value) < 1)
            {
                while (Math.Abs(value) < 1)
                {
                    value = value * 10;
                    pow--;
                }
            }
            return pow;
        }
        #endregion
    }
}

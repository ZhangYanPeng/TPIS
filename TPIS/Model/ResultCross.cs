using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using TPIS.Project;

namespace TPIS.Model
{
    public static class CrossSize
    {
        public static double WIDTH = 90;
        public static double HEIGHT = 40;
    }

    [Serializable]
    public class ResultCross : ObjectBase, INotifyPropertyChanged, ISerializable
    {
        #region 属性更新
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public Port LinkPort { get; set; }

        public Position Position { get; set; }

        Point indicator;
        Point indicatorRelated;
        public Point Indicator
        {
            get => indicator;
            set
            {
                indicator = value;
                IndicatorRelated = GetIndicatorOtherPoint();
                if (Math.Abs(indicator.X - indicatorRelated.X) < 5 && indicator.Y != indicatorRelated.Y && (indicator.X - indicatorRelated.X) / (indicator.Y - indicatorRelated.Y) < 0.2)
                    indicator.X = indicatorRelated.X;
                if (Math.Abs(indicator.Y - indicatorRelated.Y) < 5 && indicator.X != indicatorRelated.X && (indicator.Y - indicatorRelated.Y) / (indicator.X - indicatorRelated.X) < 0.2)
                    indicator.Y = indicatorRelated.Y;
                OnPropertyChanged("Indicator");
            }
        }
        public Point IndicatorRelated
        {
            get => indicatorRelated;
            set
            {
                indicatorRelated = value;
                OnPropertyChanged("IndicatorRelated");
            }
        }

        public Point GetIndicatorOtherPoint()
        {
            Point[] points = new Point[] { new Point(0, Position.V_height/2), new Point(Position.V_width / 2, 0), new Point(Position.V_width, Position.V_height / 2), new Point(Position.V_width / 2, Position.V_height) };
            double mind = CalDis(points[0], Indicator);
            int minIndex = 0;
            for (int i=1; i<4; i++)
            {
                double dis= CalDis(points[i], Indicator);
                if(mind > dis)
                {
                    mind = dis;
                    minIndex = i;
                }
            }
            return points[minIndex];
        }

        internal double CalDis(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X,2) + Math.Pow(a.Y - b.Y,2));
        }

        public ResultCross(Port port, int no, double rate, double vx, double vy)
        {
            LinkPort = port;
            No = no;
            port.CrossNo = no;
            this.Position = new Position { Rate = rate };
            this.Position.V_x = vx;
            this.Position.V_y = vy;
            this.Position.Width = 90;
            this.Position.Height = 40;
            Indicator = new Point(-Position.V_width / 2, Position.V_height);
        }

        internal void PosChange(double? x, double? y)
        {
            if (x.HasValue)
                Position.V_x += x.Value;
            if (y.HasValue)
                Position.V_y += y.Value;
        }

        internal void SetRate(double rate)
        {
            Position.Rate = rate;
            Indicator = new Point(-Position.V_width / 2, Position.V_height);
        }

        public override object Clone()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, this);
            stream.Position = 0;
            Object obj = bf.Deserialize(stream);
            stream.Close();
            return obj;
        }

        #region 序列化与反序列化
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("no", No);
            info.AddValue("position", Position);
            info.AddValue("indicator",Indicator);
        }

        public ResultCross(SerializationInfo info, StreamingContext context)
        {
            this.No = info.GetInt32("no");
            this.Position = (Position)info.GetValue("position", typeof(Object));
            this.Indicator = (Point)info.GetValue("indicator", typeof(Object));
        }
        #endregion
    }
}
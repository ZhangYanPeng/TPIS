﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Project;
using TPIS.TPISCanvas;
using TPISNet;

namespace TPIS.Model
{
    /// <summary>
    /// polyline更改
    /// </summary>
    public class LinePosConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            else
            {
                PointCollection pc = new PointCollection();
                ObservableCollection<Point> points = value as ObservableCollection<Point>;
                foreach (Point p in points)
                {
                    pc.Add(p);
                }
                return pc;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public struct Node
    {
        public int x;
        public int y;
    }

    [Serializable]
    public class TPISLine : ObjectBase, INotifyPropertyChanged, ISerializable
    {
        Port inport;
        Port outport;
        public Port InPort {
            get =>inport;
            set
            {
                inport = value;
                if (OutPort == null)
                    return;
                if (InPort.MaterialType == Material.NA && OutPort.MaterialType != Material.NA)
                    LineColor = GetColor(OutPort.MaterialType);
                else if (InPort.MaterialType != Material.NA && OutPort.MaterialType == Material.NA)
                    LineColor = GetColor(inport.MaterialType);
                else if(InPort.MaterialType == OutPort.MaterialType)
                    LineColor = GetColor(OutPort.MaterialType);
                OnPropertyChanged("InPort");
            }
        }
        public Port OutPort
        {
            get => outport;
            set
            {
                outport = value;
                if (inport == null)
                    return;
                if (InPort.MaterialType == Material.NA && OutPort.MaterialType != Material.NA)
                    LineColor = GetColor(OutPort.MaterialType);
                else if (InPort.MaterialType != Material.NA && OutPort.MaterialType == Material.NA)
                    LineColor = GetColor(inport.MaterialType);
                else if (InPort.MaterialType == OutPort.MaterialType)
                    LineColor = GetColor(OutPort.MaterialType);
            }
        }

        internal Brush GetColor(Material m)
        {
            switch (m)
            {
                case Material.air: return Brushes.LightSkyBlue;
                case Material.ash: return Brushes.Gray;
                case Material.coal: return Brushes.Black;
                case Material.fluegas: return Brushes.Wheat;
                case Material.gas: return Brushes.Cyan;
                case Material.NA: return Brushes.LimeGreen;
                case Material.oil: return Brushes.Orange;
                case Material.power: return Brushes.Red;
                case Material.water: return Brushes.DodgerBlue;
            }
            return Brushes.LimeGreen;
        }

        public Brush lineColor;
        public Brush LineColor {
            get=>lineColor;
            set {
                lineColor = value;
                OnPropertyChanged("LineColor");
            }
        }

        #region 序列化与反序列化
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("no", No);
            info.AddValue("isCompleted", IsCompleted);
            info.AddValue("lineType", LType);
            info.AddValue("lNum", LNum);
            info.AddValue("points", Points);
            info.AddValue("vorH", VorH);
        }

        public TPISLine(SerializationInfo info, StreamingContext context)
        {
            this.No = info.GetInt32("no");
            this.IsCompleted = info.GetBoolean("isCompleted");
            this.LType = (LineType)info.GetValue("lineType", typeof(LineType));
            this.LNum = (long)info.GetValue("lNum", typeof(long));
            this.Points = (ObservableCollection<Point>)info.GetValue("points", typeof(ObservableCollection<Point>));
            this.VorH = (List<Boolean>)info.GetValue("vorH", typeof(List<Boolean>));
            GridForm();
        }
        #endregion

        public bool IsCompleted { get; set; }
        public bool IsInitiAnchorPoints { get; set; }

        public enum LineType
        {
            Straight,
            Slash
        }
        public LineType lType;
        public LineType LType
        {
            get => lType;
            set
            {
                lType = value;
            }
        }
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public long lNum;
        public long LNum
        {
            get
            {
                return lNum;
            }
            set
            {
                this.lNum = value;
                OnPropertyChanged("LNum");
            }
        }

        public ObservableCollection<Point> points { get; set; }

        public ObservableCollection<LineAnchorPoint>  lAnchorPoints { get; set; }

        public List<Boolean> VorH { get; set; } //true 横 false 纵

        public ObservableCollection<Point> Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
                GridForm();
                OnPropertyChanged("Points");
            }
        }

        public ObservableCollection<Point> v_points;
        public ObservableCollection<Point> V_points
        {
            get
            {
                return v_points;
            }
            set
            {
                v_points = value;
                OnPropertyChanged("V_points");
            }
        }
        
        internal void GridForm()
        {
            if(v_points == null)
            {
                V_points = new ObservableCollection<Point>();
            }
            if (v_points.Count != points.Count)
            {
                v_points.Clear();
                for (int i = 0; i < points.Count; i++)
                {
                    Point p = new Point(points[i].X, points[i].Y);
                    V_points.Add(p);
                }
            }
            if (isGrid)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    Point p = new Point(points[i].X, points[i].Y);
                    p.X = p.X - p.X % MainWindow.GRID_WIDTH;
                    p.Y = p.Y - p.Y % MainWindow.GRID_WIDTH;
                    V_points[i] = p;
                }
            }
            else
            {
                for (int i = 0; i < points.Count; i++)
                {
                    Point p = new Point(points[i].X, points[i].Y);
                    V_points[i] = p;
                }
            }
            OnPropertyChanged("V_points");
        }

        public bool IsGrid
        {
            get => isGrid; set
            {
                isGrid = value;
                GridForm();
            }
        }

        public Boolean PointTo(int pn, Point p)
        {
            int tn = points.Count;
            if (LType == LineType.Straight)
            {
                if (pn == 0)
                {
                    if (points.Count == 2)//只有两点，加入两点改折线
                    {
                        double tmpX = (points[1].X + p.X) / 2;
                        double tmpY = (points[1].Y + p.Y) / 2;
                        if (points[0].Y == points[1].Y)
                        {
                            points[0] = p;
                            if (points[0].Y != points[1].Y && points[0].X != points[1].X) //移动后不在一条直线上
                            {
                                tmpY = points[1].Y;
                                points.Insert(1, new Point(tmpX, p.Y));
                                points.Insert(2, new Point(tmpX, tmpY));
                            }
                        }
                        else if (points[0].X == points[1].X)
                        {
                            points[0] = p;
                            if (points[0].Y != points[1].Y && points[0].X != points[1].X) //移动后不在一条直线上
                            {
                                tmpX = points[1].X;
                                points.Insert(1, new Point(p.X, tmpY));
                                points.Insert(2, new Point(tmpX, tmpY));
                            }
                        }
                    }
                    else
                    {
                        points[0] = p;
                        if (points[1].X == points[2].X)
                            points[1] = new Point(points[2].X, points[0].Y);
                        else
                            points[1] = new Point(points[0].X, points[2].Y);
                    }
                }
                else if (pn == points.Count - 1)//后端点
                {
                    if (points.Count == 2)//只有两点，加入两点改折线
                    {
                        double tmpX = (points[1].X + p.X) / 2;
                        double tmpY = (points[1].Y + p.Y) / 2;
                        if (points[0].Y == points[1].Y)
                        {
                            points[1] = p;
                            if (points[0].Y != points[1].Y && points[0].X != points[1].X) //移动后不在一条直线上
                            {
                                points.Insert(1, new Point(tmpX, points[0].Y));
                                points.Insert(2, new Point(tmpX, p.Y));
                            }
                        }
                        else if (points[0].X == points[1].X)
                        {
                            points[1] = p;
                            if (points[0].Y != points[1].Y && points[0].X != points[1].X) //移动后不在一条直线上
                            {
                                points.Insert(1, new Point(points[0].X, tmpY));
                                points.Insert(2, new Point(p.X, tmpY));
                            }
                        }
                    }
                    else
                    {
                        points[points.Count - 1] = p;
                        if (points[points.Count - 2].X == points[points.Count - 3].X)
                            points[points.Count - 2] = new Point(points[points.Count - 2].X, points[points.Count - 1].Y);
                        else
                            points[points.Count - 2] = new Point(points[points.Count - 1].X, points[points.Count - 2].Y);
                    }
                }
                else//中间端点
                {
                    double tmpX = p.X, tmpY = p.Y;
                    if (pn == 1)
                    {
                        if(points[pn].X == points[0].X)
                        {
                            tmpX = points[0].X;
                        }
                        else if (points[pn].Y == points[0].Y)
                        {
                            tmpY = points[0].Y;
                        }
                    }
                    if (pn == points.Count - 2)
                    {
                        if (points[pn].X == points[points.Count - 1].X)
                        {
                            tmpX = points[points.Count - 1].X;
                        }
                        else if (points[pn].Y == points[points.Count - 1].Y)
                        {
                            tmpY = points[points.Count - 1].Y;
                        }
                    }
                    if( !VorH[pn - 1] )
                    {
                        points[pn-1] = new Point(tmpX, points[pn - 1].Y);
                    }
                    else if (VorH[pn - 1])
                    {
                        points[pn - 1] = new Point(points[pn - 1].X, tmpY);
                    }
                    if (!VorH[pn])
                    {
                        points[pn + 1] = new Point(tmpX, points[pn + 1].Y);
                    }
                    else if (VorH[pn])
                    {
                        points[pn + 1] = new Point(points[pn + 1].X, tmpY);
                    }
                    points[pn] = new Point(tmpX, tmpY);
                }
            }
            else
            {
                points[pn] = p; ;//确定点
            }
            GridForm();
            if (tn > points.Count)
                return true;
            return false;
        }

        internal void SetRate(double lrate)
        {
            for (int i=0;i<Points.Count;i++)
            {
                points[i] = new Point(points[i].X * lrate, points[i].Y * lrate);
            }
            GridForm();
        }

        public void ReSetRate(double lrate)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                points[i] = new Point(points[i].X / lrate, points[i].Y / lrate);
            }
            GridForm();
        }
        
        //整体平移
        internal void PosChange(double vx, double vy)
        {
            for( int i=0; i<Points.Count; i++)
            {
                Points[i] = new Point(Points[i].X + vx, Points[i].Y + vy);
            }
            GridForm();
        }

        public TPISLine()
        {
            points = new ObservableCollection<Point>();
            V_points = new ObservableCollection<Point>();
            LineColor = Brushes.LimeGreen;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
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

        public static implicit operator TPISLine(System.Windows.Shapes.Line v)
        {
            throw new NotImplementedException();
        }
    }
}

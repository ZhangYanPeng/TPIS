using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Project;

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
    public class TPISLine : ObjectBase, INotifyPropertyChanged
    {
        public bool IsCompleted { get; set; }

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
        public long LNum {
            get {
                return lNum;
            }
            set {
                this.lNum = value;
                OnPropertyChanged("LNum");
            }
        }

        public ObservableCollection<Point> points { get; set; }
        
        public ObservableCollection<Point> Points
        {
            get
            {
                return points;
            }
            set{
                this.points = value;
                OnPropertyChanged("Points");
            }
        }

        public void PointTo(int pn, Point p)
        {
            Points[pn] = p;
            OnPropertyChanged("Points");
        }

        public TPISLine()
        {
            points = new ObservableCollection<Point>();
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

        public static implicit operator TPISLine(System.Windows.Shapes.Line v)
        {
            throw new NotImplementedException();
        }
    }
}

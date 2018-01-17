using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Project;

namespace TPIS.Model
{
    public struct Node
    {
        public int x;
        public int y;
    }
    public class TPISLine : ObjectBase, INotifyPropertyChanged
    {
        public bool IsCompleted { get; set; }
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
        public PointCollection points;
        public PointCollection Points{
            get
            {
                return points;
            }
            set{
                this.points = value;
                OnPropertyChanged("Points");
            }
        }

        public TPISLine()
        {
            points = new PointCollection();
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

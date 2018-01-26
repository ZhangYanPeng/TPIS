using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TPIS.Model.Common;

namespace TPIS.Model
{
    public class Port : INotifyPropertyChanged
    {
        public double x;
        public double p_x;
        public double P_x
        {
            get => p_x;
            set
            {
                p_x = value;
                OnPropertyChanged("P_x");
            }
        }//横坐标比例（0~1）

        public double y;
        public double p_y;
        public double P_y
        {
            get => p_y;
            set
            {
                p_y = value;
                OnPropertyChanged("P_y");
            }
        }//纵坐标比例（0~1)

        public Node node;
        public TPISLine link;

        public string DicName { get; set; }
        public string Name { get; set; }
        public Material MaterialType { get; set; }
        public NodType type;
        public NodType Type {
            get => type;
            set
            {
                type = value;
                switch (type)
                {
                    case NodType.Inlet:
                    case NodType.DefIn: PortColor = Brushes.Green; break;
                    case NodType.Outlet:
                    case NodType.DefOut: PortColor = Brushes.Red; break;
                    case NodType.Undef: PortColor = Brushes.Gray; break;
                }
                OnPropertyChanged("PortColor");
            }
        }
        public Brush portColor;
        public Brush PortColor
        {
            get => portColor;
            set
            {
                portColor = value;
                OnPropertyChanged("PortColor");
            }
        }
        public bool CanLink { get; set; }//受否可以连接
        public bool CanCancel { get; set; }

        //构造函数
        #region
        public Port(string dicName, string name, double xpos, double ypos, Material material, NodType nodType, bool canlink, bool cancancel)
        {
            DicName = dicName;
            Name = name;
            x = xpos;
            y = ypos;
            MaterialType = material;
            Type = nodType;
            CanLink = canlink;
            CanCancel = cancancel;
        }
        public Port(string dicName, string name, double xpos, double ypos, Material material, NodType nodType, bool canlink)
        {
            DicName = dicName;
            Name = name;
            x = xpos;
            y = ypos;
            MaterialType = material;
            Type = nodType;
            CanLink = canlink;
            CanCancel = canlink;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}

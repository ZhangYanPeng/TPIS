using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TPIS.Model
{
    public class Position : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int x;//中心横坐标
        private int y;//中心纵坐标
        private int angle;//旋转角度 0 90 180 270
        private int scale_x;//水平翻转
        private int scale_y;//垂直翻转
        private int width;//宽度
        private int height;//高度

        private int v_x;
        private int v_y;
        private int v_width;
        private int v_height;
        private double rate;
        
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public double Rate { get => rate;  set
            {
                rate = value;
                this.v_x = (int)(value * this.x);
                this.v_y = (int)(value * this.y);
                this.v_width = (int)(value * this.width);
                this.v_height = (int)(value * this.height);
                OnPropertyChanged("V_x");
                OnPropertyChanged("V_y");
                OnPropertyChanged("V_width");
                OnPropertyChanged("V_height");
            }
        }

        public int X { get => x; set
            {
                x = value;
                this.v_x = (int)(value * this.Rate);
                OnPropertyChanged("V_x");
            }
        }
        public int Y { get => y; set
            {
                y = value;
                this.v_y = (int)(value * this.Rate);
                OnPropertyChanged("V_y");
            }
        }
        public int Angle { get => angle; set
            {
                angle = value;
                OnPropertyChanged("Angle");
            }
        }
        public int Width { get => width; set
            {
                width = value;
                this.v_width = (int)(value * this.Rate);
                OnPropertyChanged("V_width");
            }
        }
        public int Height { get => height; set
            {
                height = value;
                this.v_height = (int)(value * this.Rate);
                OnPropertyChanged("V_height");
            }
        }

        public int V_x { get => v_x; set
            {
                v_x = value;
                x = (int)(value / this.Rate);
                Console.WriteLine(this.V_x);
                OnPropertyChanged("V_x");
            }
        }
        public int V_y { get => v_y; set
            {
                v_y = value;
                y = (int)(value / this.Rate);
                OnPropertyChanged("V_y");
            }
        }
        public int V_width { get => v_width; set
            {
                v_width = value;
                this.Width = (int)(value / this.Rate);
                OnPropertyChanged("V_width");
            }
        }
        public int V_height { get => v_height; set
            {
                v_height = value;
                this.Height = (int)(value / this.Rate);
                OnPropertyChanged("V_height");
            }
        }

        public int Scale_x { get => scale_x; set
            {
                scale_x = value;
                OnPropertyChanged("Scale_x");
            }
        }

        public int Scale_y { get => scale_y; set
            {
                scale_y = value;
                if (this.PropertyChanged != null)
                OnPropertyChanged("Scale_y");
            }
        }
    }

    public struct Port
    {
        public int x;//横坐标比例（0~1）
        public int y;//纵坐标比例（0~1)
        public Node node;
        public Link link;
    }

    public class TPISComponent : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Position Position { get; set; }
        public List<Port> Ports { get; set; }
        public long id;
        public string Pic { get; set; }


        public TPISComponent(int tx, int ty, long id)
        {
            this.Position = new Position { Rate = 1 };
            this.Ports = new List<Port>();
            if(id == 1)
            {
                this.Position.X = tx;
                this.Position.Y = ty;
                this.Position.Width = 10;
                this.Position.Height = 20;
                this.Pic = "Images/element/Turbin1.png";
            }
            else
            {
                this.Position.X = tx;
                this.Position.Y = ty;
                this.Position.Width = 100;
                this.Position.Height = 100;
                this.Pic = "Images/element/TeeValve.png";
            }
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Pic"));
            }
        }
    }
}

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
    //位置，宽高
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

        public double Rate
        {
            get => rate; set
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

        public int X
        {
            get => x; set
            {
                x = value;
                this.v_x = (int)(value * this.Rate);
                OnPropertyChanged("V_x");
            }
        }
        public int Y
        {
            get => y; set
            {
                y = value;
                this.v_y = (int)(value * this.Rate);
                OnPropertyChanged("V_y");
            }
        }
        public int Width
        {
            get => width; set
            {
                width = value;
                this.v_width = (int)(value * this.Rate);
                OnPropertyChanged("V_width");
            }
        }
        public int Height
        {
            get => height; set
            {
                height = value;
                this.v_height = (int)(value * this.Rate);
                OnPropertyChanged("V_height");
            }
        }

        public int V_x
        {
            get => v_x; set
            {
                v_x = value;
                x = (int)(value / this.Rate);
                OnPropertyChanged("V_x");
            }
        }
        public int V_y
        {
            get => v_y; set
            {
                v_y = value;
                y = (int)(value / this.Rate);
                OnPropertyChanged("V_y");
            }
        }
        public int V_width
        {
            get => v_width; set
            {
                v_width = value;
                this.Width = (int)(value / this.Rate);
                OnPropertyChanged("V_width");
            }
        }
        public int V_height
        {
            get => v_height; set
            {
                v_height = value;
                this.Height = (int)(value / this.Rate);
                OnPropertyChanged("V_height");
            }
        }
    }

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
        public bool type; // true : in  false: out
        public Node node;
        public Link link;

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

    public class TPISComponent : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Position Position { get; set; }
        public List<Port> Ports { get; set; }
        public long id;
        public string Pic { get; set; }


        public int Angel
        {
            get => Angel;
            set
            {
                if (Angel == 90 || Angel == 270)
                {
                    //交换宽高
                    int tmp = this.Position.Width;
                    this.Position.Width = this.Position.Height;
                    this.Position.Height = tmp;
                }
                //更新port 位置
            }

        } //旋转角度
        public bool IsVerticalReversed { get; set; }//垂直翻转
        public bool IsHorizontalReversed { get; set; } //水平翻转



        public TPISComponent(int tx, int ty, int width, int height, long id)
        {
            this.Ports = new List<Port>();
            Port p = new Port { x = 0, y = 0.3, type = true };
            Port p1 = new Port { x = 1, y = 0.5, type = true };
            Port p2 = new Port { x = 0, y = 0.6, type = false };
            this.Ports.Add(p);
            this.Ports.Add(p1);
            this.Ports.Add(p2);

            this.Position = new Position { Rate = 1 };
            if (id == 1)
            {
                this.Position.V_x = tx;
                this.Position.V_y = ty;
                this.Position.V_width = width;
                this.Position.V_height = height;
                this.Pic = "Images/element/Turbin1.png";
            }
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Pic"));
            }

            RePosPort();
        }

        /// <summary>
        /// 在改变大小、反转后，重新定位Port
        /// </summary>
        private void RePosPort()
        {
            foreach (Port p in this.Ports)
            {
                p.P_x = this.Position.V_width * p.x - 5;
                p.P_y = this.Position.V_height * p.y - 5;
            }
        }

        internal void RePos()
        {
            //发生位移或者形变，需要改变相连线段
            //throw new NotImplementedException();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 大小改变
        /// </summary>
        /// <param name="cwidth">原有实际高度</param>
        /// <param name="width">视觉宽度变量</param>
        /// <param name="cheight">原有实际宽度</param>
        /// <param name="height">视觉高度增量</param>
        internal void SizeChange(int cwidth, int? width, int cheight, int? height)
        {
            if (width.HasValue)
                this.Position.V_width = cwidth + width.Value > 0 ? cwidth + width.Value : 1;
            if (height.HasValue)
                this.Position.V_height = cheight + height.Value > 0 ? cheight + height.Value : 1;
            RePosPort();
        }
    }
}

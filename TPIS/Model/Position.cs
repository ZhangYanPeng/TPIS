using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model
{
    //位置，宽高
    public class Position : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private int x;//中心横坐标
        private int y;//中心纵坐标
        private int width;//宽度
        private int height;//高度

        private int v_x;
        private int v_y;
        private int v_width;
        private int v_height;
        private double rate;

        public int angle;
        public int Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
                ReSizeAll();
                OnPropertyChanged("Angle");
            }

        } //旋转角度

        public int isVerticalReversed;
        public int IsVerticalReversed
        {
            get
            {
                return isVerticalReversed;
            }
            set
            {
                isVerticalReversed = value;
                OnPropertyChanged("IsVerticalReversed");
            }
        }//垂直翻转
        public int isHorizentalReversed;
        public int IsHorizentalReversed
        {
            get
            {
                return isHorizentalReversed;
            }
            set
            {
                isHorizentalReversed = value;
                OnPropertyChanged("IsHorizentalReversed");
            }
        } //水平翻转


        public double Rate
        {
            get => rate; set
            {
                rate = value;
                this.v_x = (int)(value * this.x);
                this.v_y = (int)(value * this.y);
                OnPropertyChanged("V_x");
                OnPropertyChanged("V_y");
                ReSizeAll();
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

        public int Width
        {
            get => width; set
            {
                width = value;
                ReSizeAll();
            }
        }
        public int Height
        {
            get => height; set
            {
                height = value;
                ReSizeAll();
            }
        }

        
        public int V_width
        {
            get => v_width; set
            {
                v_width = value;
                OnPropertyChanged("V_width");
                ReSizeAllBack();
            }
        }
        public int V_height
        {
            get => v_height; set
            {
                v_height = value;
                ReSizeAllBack();
                OnPropertyChanged("V_height");
            }
        }

        /// <summary>
        /// 更改宽高后，转化为视觉宽高更改
        /// </summary>
        private void ReSizeAll()
        {
            if(Angle == 90 || Angle == 270)
            {
                v_width = (int)(Height * Rate);
                v_height = (int)(Width * Rate);
            }
            else
            {
                v_width = (int)(Width * Rate);
                v_height = (int)(Height * Rate);
            }
            OnPropertyChanged("V_width");
            OnPropertyChanged("V_height");
        }

        /// <summary>
        /// 更改视觉宽高后，改写真实宽高
        /// </summary>
        private void ReSizeAllBack()
        {
            if (Angle == 90 || Angle == 270)
            {
                width = (int)(V_height / Rate);
                height = (int)(V_width / Rate);
            }
            else
            {
                width = (int)(V_width / Rate);
                height = (int)(V_height / Rate);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model
{
    //位置，宽高
    [Serializable]
    public class Position : INotifyPropertyChanged, ISerializable
    {
        /// <summary>
        /// 序列化与反序列化
        /// </summary>
        #region
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("x", x);
            info.AddValue("y", y);
            info.AddValue("width", width);
            info.AddValue("height", height);
            info.AddValue("angle", angle);
            info.AddValue("isVerticalReversed", isVerticalReversed);
            info.AddValue("isHorizentalReversed", isHorizentalReversed);
        }

        public Position(SerializationInfo info, StreamingContext context)
        {
            this.X = info.GetDouble("x");
            this.Y = info.GetDouble("y");
            this.Width = info.GetDouble("width");
            this.Height = info.GetDouble("height");
            this.Angle = info.GetInt32("angle");

            this.IsVerticalReversed = info.GetInt32("isVerticalReversed");
            this.IsHorizentalReversed = info.GetInt32("isHorizentalReversed");

            this.Rate = 1;
            ReSizeAll();
        }
        #endregion

        public Position()
        {
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

        private double x;//中心横坐标
        private double y;//中心纵坐标
        private double width;//宽度
        private double height;//高度

        private double v_x;
        private double v_y;
        private double v_width;
        private double v_height;
        private double rate;
        public bool isGrid;
        public bool IsGrid {
            get =>isGrid;
            set {
                isGrid = value;
                GridForm();
            }
        }

        public void GridForm() {
            ReSizeAll();
            if (IsGrid)
            {
                this.v_x = this.v_x - this.v_x % MainWindow.GRID_WIDTH;
                this.v_y = this.v_y - this.v_y % MainWindow.GRID_WIDTH;
                OnPropertyChanged("V_y");
                OnPropertyChanged("V_x");
            }
        }

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

        public double X
        {
            get => x; set
            {
                x = value;
                this.v_x = (int)(value * this.Rate);
                if (IsGrid)
                    this.v_x = this.v_x - this.v_x % MainWindow.GRID_WIDTH;
                OnPropertyChanged("V_x");
            }
        }
        public double Y
        {
            get => y; set
            {
                y = value;
                this.v_y = (int)(value * this.Rate);
                if (IsGrid)
                    this.v_y = this.v_y - this.v_y % MainWindow.GRID_WIDTH;
                OnPropertyChanged("V_y");
            }
        }
        public double V_x
        {
            get => v_x; set
            {
                v_x = value;
                x = (int)(value / this.Rate);
                OnPropertyChanged("V_x");
            }
        }
        public double V_y
        {
            get => v_y; set
            {
                v_y = value;
                y = (int)(value / this.Rate);
                OnPropertyChanged("V_y");
            }
        }

        public double Width
        {
            get => width; set
            {
                width = value;
                ReSizeAll();
            }
        }
        public double Height
        {
            get => height; set
            {
                height = value;
                ReSizeAll();
            }
        }


        public double V_width
        {
            get => v_width; set
            {
                v_width = value;
                OnPropertyChanged("V_width");
                ReSizeAllBack();
            }
        }
        public double V_height
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
            if (Angle == 90 || Angle == 270)
            {
                v_width = (int)(Height * Rate);
                v_height = (int)(Width * Rate);
            }
            else
            {
                v_width = (int)(Width * Rate);
                v_height = (int)(Height * Rate);
            }

            if (IsGrid) {
                this.v_width = this.v_width - this.v_width % MainWindow.GRID_WIDTH;
                this.v_height = this.v_height - this.v_height % MainWindow.GRID_WIDTH;
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

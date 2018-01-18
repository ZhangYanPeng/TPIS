using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TPIS.Project;

namespace TPIS.Model
{
    public class TPISComponent : ObjectBase, INotifyPropertyChanged
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

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public Position Position { get; set; }
        public List<Port> Ports { get; set; }
        public long id;
        public string Pic { get; set; }

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
                this.Position.IsVerticalReversed = 1;
                this.Position.IsHorizentalReversed = 1;
                this.Position.Angle = 0;
                this.Pic = "Images/element/Turbin1.png";
            }
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Pic"));
            }

            RePosPort();
        }

        /// <summary>
        /// 水平和垂直翻转\旋转
        /// </summary>
        internal void VerticalReverse()
        {
            if (Position.IsVerticalReversed == 1)
                Position.IsVerticalReversed = -1;
            else
                Position.IsVerticalReversed = 1;
            RePosPort();
        }
        internal void HorizentalReverse()
        {
            if (Position.IsHorizentalReversed == 1)
                Position.IsHorizentalReversed = -1;
            else
                Position.IsHorizentalReversed = 1;
            RePosPort();
        }
        internal void Rotate(int v)
        {
            int tmp = Position.Angle + v * 90;
            this.Position.Angle = tmp % 360;
            RePosPort();
        }

        /// <summary>
        /// 大小改变
        /// </summary>
        /// <param name="cwidth">原有实际高度</param>
        /// <param name="width">视觉宽度变量</param>
        /// <param name="cheight">原有实际宽度</param>
        /// <param name="height">视觉高度增量</param>
        internal void SizeChange(int? width, int? height)
        {
            if (width.HasValue)
                Position.V_width = Position.V_width + width.Value > 0 ? Position.V_width + width.Value : 1;
            if (height.HasValue)
                Position.V_height = Position.V_height + height.Value > 0 ? Position.V_height + height.Value : 1;
            RePosPort();
        }

        /// <summary>
        /// 图片水平垂直方向移动
        /// </summary>
        /// <param name="d_vx"></param>
        /// <param name="d_vy"></param>
        internal void PosChange(int? x, int? y)
        {
            if( x.HasValue)
                Position.V_x += x.Value;
            if (y.HasValue)
                Position.V_y += y.Value;
        }

        /// <summary>
        /// 在改变大小、反转后，重新定位Port
        /// </summary>
        private void RePosPort()
        {
            foreach (Port p in this.Ports)
            {
                double tx = 0;
                double ty = 0;
                //考虑旋转
                switch (Position.Angle)
                {
                    case 0:
                        {
                            tx = Position.V_width * p.x;
                            ty = Position.V_height * p.y;
                        }
                        break;
                    case 90:
                        {
                            ty = Position.V_height * p.x;
                            tx = Position.V_width * (1 - p.y);
                        }
                        break;
                    case 180:
                        {
                            ty = Position.V_height * (1 - p.y);
                            tx = Position.V_width * (1 - p.x);
                        }
                        break;
                    case 270:
                        {
                            double t = ty;
                            ty = Position.V_height * (1 - p.x);
                            tx = Position.V_width * p.y;
                        }
                        break;
                    default: break;
                }
                //考虑翻转
                if (Position.IsHorizentalReversed == -1)
                {
                    tx = Position.V_width - tx;
                }
                if (Position.isVerticalReversed == -1)
                {
                    ty = Position.V_height - ty;
                }
                //修正偏移
                p.P_x = tx - 5;
                p.P_y = ty - 5;
            }
        }

        internal void RePos()
        {
            //发生位移或者形变，需要改变相连线段
            //throw new NotImplementedException();
        }
    }
}

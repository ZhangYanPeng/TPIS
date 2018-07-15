using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TPIS.Model;
using TPIS.Model.Common;

namespace TPIS.Project
{
    public partial class ProjectItem : INotifyPropertyChanged, ISerializable
    {
        #region 缩放比率
        public double rate;
        public double Rate
        {
            get => rate;
            set
            {
                double lrate = value / rate;
                rate = value;
                //画布缩放
                Canvas.Rate = rate;
                foreach (ObjectBase obj in Objects)
                {

                    //元件缩放
                    if (obj is TPISComponent)
                    {
                        ((TPISComponent)obj).SetRate(rate);
                    }
                    if (obj is TPISLine)
                    {
                        ((TPISLine)obj).SetRate(lrate);
                    }
                    if (obj is ResultCross)
                    {
                        ((ResultCross)obj).SetRate(rate);
                    }
                }
                OnPropertyChanged("Rate");
            }
        }
        #endregion

        #region 缩放操作
        /// 放大
        internal void SupRate()
        {
            Rate = RateService.GetSupRate(Rate);
            GridUintLength = 20 * Rate;
        }
        
        /// 缩小
        internal void SubRate()
        {
            Rate = RateService.GetSubRate(Rate);
            GridUintLength = 20 * Rate;
        }
        #endregion

        #region 选中形变操作
        internal bool LinkOrNot(ObjectBase obj)
        {
            if (obj is TPISComponent)
            {
                TPISComponent c = obj as TPISComponent;
                foreach (Port p in c.Ports)
                {
                    if (p.link != null)
                        return false;
                }
            }
            return true;
        }
        
        /// 翻转选中
        public void VerticalReversedSelection(bool record = true)
        {
            Record rec = new Record();
            rec.Param.Add("Operation", "VerticalReverse");
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent)
                {
                    if (((TPISComponent)obj).IsSelected && LinkOrNot(obj))//选中，切不含连接关系
                    {
                        if (obj is TPISComponent)
                        {
                            ((TPISComponent)Objects[i]).VerticalReverse();
                            rec.ObjectsNo.Add(Objects[i].No);
                        }
                    }
                }
            }
            if (record && rec.ObjectsNo.Count > 0)
            {
                Records.Push(rec);
            }
        }
        
        /// 翻转选中
        public void HorizentalReversedSelection(bool record = true)
        {
            Record rec = new Record();
            rec.Param.Add("Operation", "HorizentalReverse");
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent)
                {
                    if (((TPISComponent)obj).IsSelected && LinkOrNot(obj))//选中，切不含连接关系
                    {
                        if (obj is TPISComponent)
                        {
                            ((TPISComponent)Objects[i]).HorizentalReverse();
                            rec.ObjectsNo.Add(Objects[i].No);
                        }
                    }
                }
            }
            if (record && rec.ObjectsNo.Count > 0)
            {
                Records.Push(rec);
            }
        }
        
        /// 旋转选中
        /// <param name="n">n*90 为顺时针旋转角度</param>
        public void RotateSelection(int n, bool record = true)
        {
            Record rec = new Record();
            rec.Param.Add("Operation", "Rotate");
            rec.Param.Add("angle", n.ToString());
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent)
                {
                    if (((TPISComponent)obj).IsSelected && LinkOrNot(obj))//选中，切不含连接关系
                    {
                        ((TPISComponent)Objects[i]).Rotate(n);
                        rec.ObjectsNo.Add(Objects[i].No);
                    }
                }
            }
            if (record && rec.ObjectsNo.Count > 0)
            {
                Records.Push(rec);
            }
        }

        internal string DoubleNullToString(double? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString();
            }
            else
            {
                return "null";
            }
        }
        
        /// 改变大小
        public void SizeChange(int np, double? width, double? height, double? x, double? y, bool record = true)
        {
            Record rec = new Record();
            rec.Param.Add("Operation", "SizeChange");
            rec.Param.Add("width", DoubleNullToString(width));
            rec.Param.Add("height", DoubleNullToString(height));
            rec.Param.Add("x", DoubleNullToString(x));
            rec.Param.Add("y", DoubleNullToString(y));
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent)
                {
                    if (((TPISComponent)obj).IsSelected && obj.No == np && LinkOrNot(obj))//选中，切不含连接关系
                    {
                        ((TPISComponent)Objects[i]).SizeChange(width, height);
                        ((TPISComponent)Objects[i]).PosChange(x, y);
                        rec.ObjectsNo.Add(np);
                    }
                }
                if (obj is TPISText && ((TPISText)obj).IsSelected)
                {
                    ((TPISText)Objects[i]).SizeChange(width, height);
                    ((TPISText)Objects[i]).PosChange(x, y);
                    rec.ObjectsNo.Add(np);
                }
            }
            if (record && rec.ObjectsNo.Count > 0)
            {
                Records.Push(rec);
            }
        }
        
        /// 水平和垂直方向移动
        public void MoveSelection(double d_vx, double d_vy, bool record = true)
        {
            if (this.IsViewsMouseEnter == true)
                return;
            Record rec = new Record();
            rec.Param.Add("Operation", "Move");
            rec.Param.Add("x", (d_vx / Rate).ToString());
            rec.Param.Add("y", (d_vy / Rate).ToString());
            bool flag = false;//解决选中线时，方向键只移动线
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent)
                {
                    if (((TPISComponent)obj).IsSelected)//选中
                    {
                        flag = true;
                        ((TPISComponent)Objects[i]).PosChange(d_vx, d_vy);
                        rec.ObjectsNo.Add(Objects[i].No);
                        foreach (Port port in ((TPISComponent)obj).Ports)
                        {
                            if (port.link != null && !port.link.IsSelected)
                            {
                                if (port.Type == Model.Common.NodType.DefOut || port.Type == Model.Common.NodType.Outlet)
                                {
                                    port.link.PointTo(0, new Point(port.link.Points[0].X + d_vx, port.link.Points[0].Y + d_vy));
                                }
                                else
                                {
                                    //port.link.PointTo(port.link.Points.Count - 1, new Point(port.link.Points[port.link.Points.Count - 1].X + d_vx, port.link.Points[port.link.Points.Count - 1].Y + d_vy));
                                    Point p = new Point();
                                    p.X = ((TPISComponent)obj).Position.V_x + port.P_x+5;
                                    p.Y = ((TPISComponent)obj).Position.V_y + port.P_y+5;
                                    port.link.PointTo(port.link.Points.Count - 1, p);
                                }
                            }
                        }
                    }
                }
                if (obj is TPISLine)
                {
                    if (((TPISLine)obj).IsSelected && flag)//选中
                    {
                        ((TPISLine)Objects[i]).PosChange(d_vx, d_vy);
                        rec.ObjectsNo.Add(Objects[i].No);
                    }
                }
                if (obj is ResultCross)
                {
                    if (((ResultCross)obj).isSelected)//选中
                    {
                        ((ResultCross)Objects[i]).PosChange(d_vx, d_vy);
                        rec.ObjectsNo.Add(Objects[i].No);
                    }
                }
                if (obj is TPISText)
                {
                    if (((TPISText)obj).isSelected)//选中
                    {
                        ((TPISText)Objects[i]).PosChange(d_vx, d_vy);
                        rec.ObjectsNo.Add(Objects[i].No);
                    }
                }
            }
            if (record && rec.ObjectsNo.Count > 0)
            {
                Records.Push(rec);
            }
        }
        #endregion
    }
}

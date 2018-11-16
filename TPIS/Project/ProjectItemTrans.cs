using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
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
        
        protected CancellationTokenSource cts = new CancellationTokenSource();
        public double Rate
        {
            get => rate;
            set
            {
                rate = value;
                //画布缩放
                Canvas.Rate = rate;
                cts.Cancel();
                cts = new CancellationTokenSource();
                var ct = cts.Token;
                Task taskUpadateRate = new Task(() => { UpdateRate(rate, ct); }, ct);
                taskUpadateRate.Start();
            }
        }

        private void UpdateRate(double r, CancellationToken ct)
        {
            foreach (ObjectBase obj in Objects)
            {
                //元件缩放
                if (obj is TPISLine)
                {
                    ((TPISLine)obj).SetRate(r);
                    ct.ThrowIfCancellationRequested();
                }
            }
            foreach (ObjectBase obj in Objects)
            {
                //元件缩放
                if (obj is TPISComponent)
                {
                    ((TPISComponent)obj).SetRate(r);
                    ct.ThrowIfCancellationRequested();
                }
                if (obj is ResultCross)
                {
                    ((ResultCross)obj).SetRate(r);
                    ct.ThrowIfCancellationRequested();
                }
                if (obj is TPISText)
                {
                    ((TPISText)obj).SetRate(r);
                    ct.ThrowIfCancellationRequested();
                }
            }
            OnPropertyChanged("Rate");
        }
        #endregion

        #region 缩放操作
        /// 放大
        internal void SupRate()
        {
            Rate = RateService.GetSupRate(Rate);
            GridUintLength = 20 * Rate;
        }

        internal void SupRate(double r)
        {
            Rate = Rate + r;
            GridUintLength = 20 * Rate;
        }

        /// 缩小
        internal void SubRate()
        {
            Rate = RateService.GetSubRate(Rate);
            GridUintLength = 20 * Rate;
        }

        internal void SubRate(double r)
        {
            if (Rate - r >= 0.01)
                Rate = Rate - r;
            else
                Rate = 0.01;
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
            List<ObjectBase> selection = new List<ObjectBase>();
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i].isSelected)//选中
                {
                    if(Objects[i] is TPISComponent)
                        flag = true;
                    selection.Add(Objects[i]);
                }
            }

            for (int i = 0; i < selection.Count; i++)
            {
                if (selection[i] is TPISLine && flag)
                {
                    ((TPISLine)selection[i]).PosChange(d_vx, d_vy);
                    rec.ObjectsNo.Add(Objects[i].No);
                }
            }

            for (int i = 0; i < selection.Count; i++)
            {
                if (selection[i] is TPISComponent)
                {
                    ((TPISComponent)selection[i]).PosChange(d_vx, d_vy);
                    rec.ObjectsNo.Add(Objects[i].No);
                }
                if (selection[i] is ResultCross)
                {
                    ((ResultCross)selection[i]).PosChange(d_vx, d_vy);
                    rec.ObjectsNo.Add(Objects[i].No);
                }
                if (selection[i] is TPISText)
                {
                    ((TPISText)selection[i]).PosChange(d_vx, d_vy);
                    rec.ObjectsNo.Add(Objects[i].No);
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

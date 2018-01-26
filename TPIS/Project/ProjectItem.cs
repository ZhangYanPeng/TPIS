using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.TPISCanvas;

namespace TPIS.Project
{
    public class ObjectBase
    {
        public bool isSelected;
    }

    /// <summary>
    /// 倍率设置
    /// </summary>
    #region
    public static class RateService
    {
        public static double GetRate(int i)
        {
            switch (i)
            {
                case 1: return 0.1;
                case 2: return 0.25;
                case 3: return 0.5;
                case 4: return 0.75;
                case 5: return 1;
                case 6: return 1.25;
                case 7: return 1.5;
                case 8: return 2;
                case 9: return 3;
                case 10: return 4;
                case 11: return 5;
                default: return i - 6;
            }
        }

        public static double GetSupRate(double cr)
        {
            switch (cr)
            {
                case 0.1: return 0.25;
                case 0.25: return 0.5;
                case 0.5: return 0.75;
                case 0.75: return 1;
                case 1: return 1.25;
                case 1.25: return 1.5;
                case 1.5: return 2;
                default: return cr + 1;
            }
        }

        public static double GetSubRate(double cr)
        {
            switch (cr)
            {
                case 0.1: return 0.1;
                case 0.25: return 0.1;
                case 0.5: return 0.25;
                case 0.75: return 0.5;
                case 1: return 0.75;
                case 1.25: return 1;
                case 1.5: return 1.25;
                case 2: return 1.5;
                default: return cr - 1;
            }
        }
    }
    #endregion

    public class ProjectItem : INotifyPropertyChanged
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

        public String Name { get; set; }
        public String V_name { get; set; }
        public long Num { get; set; }

        //缩放比率
        #region
        public double rate;
        public double Rate
        {
            get => rate;
            set
            {
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
                    //连线缩放

                }
                OnPropertyChanged("Rate");
            }
        }
        #endregion

        public ObservableCollection<ObjectBase> Objects { get; set; }

        public ProjectCanvas Canvas { get; set; }

        public ProjectItem(string name, ProjectCanvas pCanvas, long num)
        {
            this.Name = name + ".tpis";
            this.Num = num;
            this.Canvas = pCanvas;
            Objects = new ObservableCollection<ObjectBase>();
            this.Rate = 1;
            return;
        }

        //缩放操作
        #region
        /// <summary>
        /// 放大
        /// </summary>
        internal void SupRate()
        {
            Rate = RateService.GetSupRate(Rate);
        }

        /// <summary>
        /// 缩小
        /// </summary>
        internal void SubRate()
        {
            Rate = RateService.GetSubRate(Rate);
        }
        #endregion

        //选中形变操作
        #region
        /// <summary>
        /// 翻转选中
        /// </summary>
        public void VerticalReversedSelection()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent)
                {
                    if (((TPISComponent)obj).IsSelected && true)//选中，切不含连接关系
                    {
                        if (obj is TPISComponent)
                        {
                            ((TPISComponent)Objects[i]).VerticalReverse();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 翻转选中
        /// </summary>
        public void HorizentalReversedSelection()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent)
                {
                    if (((TPISComponent)obj).IsSelected && true)//选中，切不含连接关系
                    {
                        if (obj is TPISComponent)
                        {
                            ((TPISComponent)Objects[i]).HorizentalReverse();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 旋转选中
        /// </summary>
        /// <param name="n">n*90 为顺时针旋转角度</param>
        public void RotateSelection(int n)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent)
                {
                    if (((TPISComponent)obj).IsSelected && true)//选中，切不含连接关系
                    {
                        if (obj is TPISComponent)
                        {
                            ((TPISComponent)Objects[i]).Rotate(n);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 水平和垂直方向移动
        /// </summary>
        /// <param name="d_vx"></param>
        /// <param name="d_vy"></param>
        public void MoveSelection(int d_vx, int d_vy)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent)
                {
                    if (((TPISComponent)obj).IsSelected && true)//选中
                    {
                        if (obj is TPISComponent)
                        {
                            ((TPISComponent)Objects[i]).PosChange(d_vx, d_vy);
                        }
                    }
                }
            }
        }
        #endregion

        //选择操作
        #region
        /// <summary>
        /// 选中
        /// </summary>
        /// <param name="components"></param>
        internal void Select(List<TPISComponent> components)
        {
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    if (components.Contains(obj as TPISComponent))
                    {
                        ((TPISComponent)obj).IsSelected = true;
                        MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                        mainwin.PropertyWindow.ItemsSource = ((TPISComponent)obj).PropertyGroups;
                        mainwin.PropertyWindow.Items.Refresh();
                    }
                    else
                    {
                        ((TPISComponent)obj).IsSelected = false;
                    }
                }
            }
        }
        /// <summary>
        /// 重载
        /// </summary>
        /// <param name="component"></param>
        internal void Select(ObjectBase objectBase)
        {
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    if (objectBase == obj)
                    {
                        ((TPISComponent)obj).IsSelected = true;
                        //设置属性框显示该属性
                        MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                        mainwin.PropertyWindow.ItemsSource = ((TPISComponent)obj).PropertyGroups;
                        mainwin.PropertyWindow.Items.Refresh();
                    }
                    else
                    {
                        ((TPISComponent)obj).IsSelected = false;
                    }
                }
                else
                {
                    //是连线，同上
                    if (objectBase == obj)
                    {
                        ((TPISLine)obj).IsSelected = true;
                    }
                    else
                    {
                        ((TPISLine)obj).IsSelected = false;
                    }
                }
            }
        }
        internal void Select()
        {
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    ((TPISComponent)obj).IsSelected = false;
                }
            }
        }
        #endregion
    }
}

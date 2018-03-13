using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.TPISCanvas;

namespace TPIS.Project
{
    [Serializable]
    public abstract class ObjectBase: ISerializable
    {
        public bool isSelected;

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
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

    [Serializable]
    public class ProjectItem : INotifyPropertyChanged, ISerializable
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
        public ClipBoard clipBoard { get; set; }

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
            this.clipBoard = new ClipBoard();
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
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (components.Count >0)
            {
                if (mainwin.PropertyWindow.ItemsSource != components[0].PropertyGroups)
                {
                    mainwin.PropertyWindow.ItemsSource = components[0].PropertyGroups;
                    mainwin.PropertyWindow.Items.Refresh();
                    Console.WriteLine("Change");
                }
            }
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    if (components.Contains(obj as TPISComponent))
                    {
                        if( !((TPISComponent)obj).IsSelected)
                            ((TPISComponent)obj).IsSelected = true;
                    }
                    else
                    {
                        if (((TPISComponent)obj).IsSelected)
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

        //添加元件
        #region
        public void AddComponent(int tx, int ty, int width, int height, ComponentType ct)
        {
            int n = 0;
            foreach (ObjectBase obj in this.Objects)
            {
                if (obj is TPISComponent)
                {
                    if (((TPISComponent)obj).No > n)
                        n = ((TPISComponent)obj).No;
                }
            }
            n++;
            Objects.Add(new TPISComponent(n, tx, ty, width, height, ct));
        }
        #endregion

        /// <summary>
        /// 选择后续操作
        /// </summary>
        #region
        //复制到clipboard
        public void CopySelection()
        {
            clipBoard.Objects = new List<ObjectBase>();
            foreach (ObjectBase obj in this.Objects)
            {
                if (obj.isSelected)
                {
                    clipBoard.Objects.Add(obj);
                }
            }
        }

        //删除
        public void DeleteSelection()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent)
                {
                    if (((TPISComponent)obj).IsSelected)
                    {
                        //删除并删除连线
                        Objects.Remove(obj);
                    }
                }
            }
        }

        //粘贴
        public void PasteSelection(double x, double y)
        {
            Boolean init = false;
            double min_x=0, min_y=0;
            //计算粘贴后偏移量
            foreach (ObjectBase obj in clipBoard.Objects)
            {
                if(obj is TPISComponent)
                {
                    if (!init)
                    {
                        min_x = ((TPISComponent)obj).Position.V_x;
                        min_y = ((TPISComponent)obj).Position.V_y;
                        init = true;
                    }
                    else {
                        min_x = Math.Min(((TPISComponent)obj).Position.V_x, min_x);
                        min_y = Math.Min(((TPISComponent)obj).Position.V_y, min_y);
                    }
                }
            }
            double offset_x = x - min_x;
            double offset_y = y - min_y;

            //按偏移量粘贴并选中
            Console.WriteLine(clipBoard.Objects.Count);
            foreach (ObjectBase obj in clipBoard.Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = ((TPISComponent)obj).Clone() as TPISComponent;
                    component.PosChange((int)offset_x, (int)offset_y);
                    
                    Objects.Add(component);
                }
            }
            return;
        }
        #endregion


        /// <summary>
        /// 序列化与反序列化
        /// </summary>
        #region
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", Name);
            info.AddValue("canvas", Canvas);
            info.AddValue("objects", Objects);
        }

        public ProjectItem(SerializationInfo info, StreamingContext context)
        {
            this.Name = info.GetString("name");
            this.Num = 2;
            this.Canvas = (ProjectCanvas)info.GetValue("canvas", typeof(Object));
            this.Objects = (ObservableCollection<ObjectBase>)info.GetValue("objects", typeof(Object));
            this.Rate = 1;
            this.clipBoard = new ClipBoard();
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Model.Common;
using TPIS.TPISCanvas;

namespace TPIS.Project
{
    [Serializable]
    public abstract class ObjectBase : ISerializable
    {
        public bool isSelected;
        public int No { get; set; }

        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
    }

    #region 倍率设置
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
        #region 属性更新
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public String Name { get; set; }
        public String V_name { get; set; }
        public long Num { get; set; }
        public ClipBoard clipBoard { get; set; }
        public String Path { get; set; }
        public bool calculateState;
        public bool CalculateState
        {
            get { return calculateState; }
            set {
                calculateState = value;
                OnPropertyChanged("CalculateState");
            }
        }

        #region 缩放比率
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
        public ObservableCollection<PropertyGroup> PropertyGroup { get; set; }
        public ObservableCollection<ResultCross> ResultCross { get; set; }

        public ProjectCanvas Canvas { get; set; }

        public ProjectItem(string name, ProjectCanvas pCanvas, long num, string p)
        {
            this.Name = name + ".tpis";
            this.Num = num;
            this.Canvas = pCanvas;
            Objects = new ObservableCollection<ObjectBase>();
            ResultCross = new ObservableCollection<Model.ResultCross>();
            this.Rate = 1;
            this.clipBoard = new ClipBoard();
            this.CalculateState = false;
            Path = p;
            PropertyGroup = CommonTypeService.InitProject();
            SaveProject();
            return;
        }

        #region 存储工程
        public void SaveProject()
        {
            string path = Path + "\\" + Name;
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            byte[] data = CommonFunction.SerializeToBinary(this);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(data);
            bw.Close();
            fs.Close();
        }
        #endregion

        #region 缩放操作
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

        #region 选中形变操作
        internal bool LinkOrNot(ObjectBase obj)
        {
            if(obj is TPISComponent)
            {
                TPISComponent c = obj as TPISComponent;
                foreach(Port p in c.Ports)
                {
                    if (p.link != null)
                        return false;
                }
            }
            return true;
        }

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
                    if (((TPISComponent)obj).IsSelected && LinkOrNot(obj))//选中，切不含连接关系
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
                    if (((TPISComponent)obj).IsSelected && LinkOrNot(obj))//选中，切不含连接关系
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
                    if (((TPISComponent)obj).IsSelected && LinkOrNot(obj))//选中，切不含连接关系
                    {
                        ((TPISComponent)Objects[i]).Rotate(n);
                    }
                }
            }
        }

        /// <summary>
        /// 旋转选中
        /// </summary>
        /// <param name="n">n*90 为顺时针旋转角度</param>
        public void SizeChange(int np, int? width, int? height, int? x, int? y)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent)
                {
                    if (((TPISComponent)obj).IsSelected && obj.No == np && LinkOrNot(obj))//选中，切不含连接关系
                    {
                        ((TPISComponent)Objects[i]).SizeChange(width, height);
                        ((TPISComponent)Objects[i]).PosChange(x, y);
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
                        ((TPISComponent)Objects[i]).PosChange(d_vx, d_vy);

                        foreach(Port port in ((TPISComponent)obj).Ports)
                        {
                            if (port.link !=null && !port.link.IsSelected)
                            {
                                if (port.Type == Model.Common.NodType.DefOut || port.Type == Model.Common.NodType.Outlet)
                                {
                                    port.link.PointTo(0, new Point(port.link.Points[0].X + d_vx, port.link.Points[0].Y + d_vy));
                                }
                                else
                                {
                                    port.link.PointTo(port.link.Points.Count-1, new Point(port.link.Points[port.link.Points.Count - 1].X + d_vx, port.link.Points[port.link.Points.Count - 1].Y + d_vy));
                                }
                            }
                        }
                    }
                }
                if (obj is TPISLine)
                {
                    if (((TPISLine)obj).IsSelected && true)//选中
                    {
                        ((TPISLine)Objects[i]).PosChange(d_vx, d_vy);
                    }
                }
            }
        }
        #endregion

        #region 选择操作
        /// <summary>
        /// 选中
        /// </summary>
        /// <param name="components"></param>
        internal void Select(List<TPISComponent> components)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (components.Count > 0)
            {
                if (mainwin.PropertyWindow.ItemsSource != components[0].PropertyGroups)
                {
                    mainwin.PropertyWindow.ItemsSource = components[0].PropertyGroups;
                    mainwin.PropertyWindow.Items.Refresh();
                    mainwin.ResultWindow.ItemsSource = components[0].ResultGroups;
                    mainwin.ResultWindow.Items.Refresh();
                    mainwin.PortResults.ItemsSource = components[0].Ports;
                    mainwin.PortResults.Items.Refresh();
                }
            }
            else
            {
                mainwin.PropertyWindow.ItemsSource = PropertyGroup;
                mainwin.PropertyWindow.Items.Refresh();
                mainwin.ResultWindow.ItemsSource = null;
            }

            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    if (components.Contains(obj as TPISComponent))
                    {
                        if (!((TPISComponent)obj).IsSelected)
                            ((TPISComponent)obj).IsSelected = true;
                    }
                    else
                    {
                        if (((TPISComponent)obj).IsSelected)
                            ((TPISComponent)obj).IsSelected = false;
                    }
                }
                if(obj is TPISLine)
                {
                    Boolean checkin = false, checkout = false;
                    foreach(TPISComponent cp in components)
                    {
                        if (cp.Ports.Contains(((TPISLine)obj).inPort))
                        {
                            checkin = true;
                        }
                        if (cp.Ports.Contains(((TPISLine)obj).outPort))
                        {
                            checkout = true;
                        }
                        if(checkin && checkout)
                        {
                            break;
                        }
                    }
                    if (checkin && checkout)
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
                        mainwin.ResultWindow.ItemsSource = ((TPISComponent)obj).ResultGroups;
                        mainwin.ResultWindow.Items.Refresh();
                        mainwin.PortResults.ItemsSource = ((TPISComponent)obj).Ports;
                        mainwin.PortResults.Items.Refresh();
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
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.PropertyWindow.ItemsSource = PropertyGroup;
            mainwin.PropertyWindow.Items.Refresh();
            mainwin.ResultWindow.ItemsSource = null;
            mainwin.ResultWindow.Items.Refresh();
            mainwin.PortResults.ItemsSource = null;
            mainwin.PortResults.Items.Refresh();
        }
        #endregion

        #region 添加
        //添加元件
        public void AddComponent(int tx, int ty, int width, int height, ComponentType ct)
        {
            int n = 0;
            foreach (ObjectBase obj in this.Objects)
            {
                if (obj.No > n)
                    n = obj.No;
            }
            n++;
            Objects.Add(new TPISComponent(n, tx, ty, width, height, ct));
        }

        //添加线
        public void AddLine(TPISLine line)
        {
            int n = 0;
            foreach (ObjectBase obj in this.Objects)
            {
                if (obj.No > n)
                    n = obj.No;
            }
            n++;
            line.No = n;
            Objects.Add(line);
            return;
        }
        #endregion

        #region 选择后续操作 复制 删除 剪切
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
                if (obj is TPISComponent && obj.isSelected)
                {
                    foreach(Port p in ((TPISComponent)obj).Ports)
                    {
                        if(p.link != null)
                            p.link.IsSelected = true;
                    }
                }
            }
            for (int i = 0; i < Objects.Count; )
            {
                ObjectBase obj = Objects[i];
                if (obj.isSelected)
                {
                    Objects.Remove(obj);
                }
                else
                {
                    i++;
                }
            }
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent)
                {
                    foreach (Port p in ((TPISComponent)obj).Ports)
                    {
                        if(p.link == null)
                        {
                            if (p.Type == NodType.DefIn || p.Type == NodType.DefOut)
                                p.Type = NodType.Undef;
                        }
                    }
                }
            }
        }

        //粘贴
        public void PasteSelection(double x, double y)
        {
            Boolean init = false;
            double min_x = 0, min_y = 0;
            //计算粘贴后偏移量
            foreach (ObjectBase obj in clipBoard.Objects)
            {
                if (obj is TPISComponent)
                {
                    if (!init)
                    {
                        min_x = ((TPISComponent)obj).Position.V_x;
                        min_y = ((TPISComponent)obj).Position.V_y;
                        init = true;
                    }
                    else
                    {
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

        #region 查找元件并选中居中
        internal bool FindComponent(int tn)
        {
            Boolean findOrNot = false;
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    if (obj.No == tn)
                    {
                        findOrNot = true;
                        ((TPISComponent)obj).IsSelected = true;

                        MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                        Grid tabGrid = FindVisualChild<Grid>(mainwin.projectTab);
                        Border contentBorder = tabGrid.FindName("ContentPanel") as Border;
                        ContentPresenter contentPresenter = FindVisualChild<ContentPresenter>(contentBorder);
                        ScrollViewer sv = contentPresenter.ContentTemplate.FindName("CanvasScrollViewer", contentPresenter) as ScrollViewer;

                        //移动坐标
                        sv.ScrollToHorizontalOffset(((TPISComponent)obj).Position.V_x - sv.ActualWidth / 2 + ((TPISComponent)obj).Position.V_width / 2);
                        sv.ScrollToVerticalOffset(((TPISComponent)obj).Position.V_y - sv.ActualHeight / 2 + ((TPISComponent)obj).Position.V_height / 2);
                    }
                    else
                    {
                        ((TPISComponent)obj).IsSelected = false;
                    }
                }
            }
            return findOrNot;
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        #endregion

        #region 序列化与反序列化
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", Name);
            info.AddValue("canvas", Canvas);
            info.AddValue("objects", Objects);
            info.AddValue("path", Path);
            info.AddValue("properties", PropertyGroup);
            info.AddValue("resultcross", ResultCross);
        }

        public ProjectItem(SerializationInfo info, StreamingContext context)
        {
            this.Name = info.GetString("name");
            this.Num = 2;
            this.Path = info.GetString("path");
            this.Canvas = (ProjectCanvas)info.GetValue("canvas", typeof(Object));
            this.Objects = (ObservableCollection<ObjectBase>)info.GetValue("objects", typeof(Object));
            this.ResultCross = (ObservableCollection<ResultCross>)info.GetValue("resultcross", typeof(Object));
            this.PropertyGroup = (ObservableCollection<PropertyGroup>)info.GetValue("properties", typeof(Object));
            this.Rate = 1;
            this.clipBoard = new ClipBoard();
        }

        public void RebuildLink()
        {
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = obj as TPISComponent;
                    foreach (Port port in component.Ports)
                    {
                        if (port.LinkNo >= 0)
                        {
                            foreach (ObjectBase lobj in Objects)
                            {
                                if (lobj is TPISLine)
                                {
                                    TPISLine line = lobj as TPISLine;
                                    if (line.No == port.LinkNo)
                                    {
                                        if (port.Type == Model.Common.NodType.DefOut || port.Type == Model.Common.NodType.Outlet)
                                            line.inPort = port;
                                        else
                                            line.outPort = port;
                                        port.link = line;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

    }
}

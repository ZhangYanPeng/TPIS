using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Model.Common;

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
    public partial class ProjectItem : INotifyPropertyChanged, ISerializable
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
        public long Num { get; set; }
        public ClipBoard clipBoard { get; set; }
        public String Path { get; set; }
        public bool calculateState;
        public bool CalculateState
        {
            get { return calculateState; }
            set
            {
                calculateState = value;
                OnPropertyChanged("CalculateState");
            }
        }
        public RecordStack Records { get; set; }

        #region 画布网格缩放
        public double gridUintLength;
        public double GridUintLength
        {
            get { return gridUintLength; }
            set
            {
                gridUintLength = value;
                OnPropertyChanged("GridUintLength");
            }
        }
        #endregion

        #region 画布网格线粗
        public double gridThickness;
        public double GridThickness
        {
            get { return gridThickness; }
            set
            {
                gridThickness = value;
                OnPropertyChanged("GridThickness");
            }
        }
        #endregion

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

        public ObservableCollection<ObjectBase> Objects { get; set; }
        public ObservableCollection<PropertyGroup> PropertyGroup { get; set; }

        public ProjectCanvas Canvas { get; set; }

        public ProjectItem(string name, ProjectCanvas pCanvas, long num, string p)
        {

            this.Name = name + ".tpis";
            this.Num = num;
            this.Canvas = pCanvas;
            Objects = new ObservableCollection<ObjectBase>();
            this.Rate = 1;
            this.clipBoard = new ClipBoard();
            Records = new RecordStack();
            this.CalculateState = false;
            this.GridThickness = 0;//赋初值0，使初始画布为隐藏网格
            this.GridUintLength = 20;//赋初值20，使初始网格单元为20×20
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
            GridUintLength = 20 * Rate;
        }

        /// <summary>
        /// 缩小
        /// </summary>
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

        /// <summary>
        /// 翻转选中
        /// </summary>
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

        /// <summary>
        /// 翻转选中
        /// </summary>
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

        /// <summary>
        /// 旋转选中
        /// </summary>
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

        /// <summary>
        /// 改变大小
        /// </summary>
        public void SizeChange(int np, double? width, double? height, double? x, double? y, bool record = true)
        {
            Record rec = new Record();
            rec.Param.Add("Operation", "SizeChange");
            rec.Param.Add("width", width.HasValue?"null":width.Value.ToString());
            rec.Param.Add("height", height.HasValue ? "null" : height.Value.ToString());
            rec.Param.Add("x", x.HasValue ? "null" : x.Value.ToString());
            rec.Param.Add("y", y.HasValue ? "null" : y.Value.ToString());
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
            }
            if (record && rec.ObjectsNo.Count > 0)
            {
                Records.Push(rec);
            }
        }

        /// <summary>
        /// 水平和垂直方向移动
        /// </summary>
        /// <param name="d_vx"></param>
        /// <param name="d_vy"></param>
        public void MoveSelection(double d_vx, double d_vy, bool record = true)
        {
            Record rec = new Record();
            rec.Param.Add("Operation", "Move");
            rec.Param.Add("x", (d_vx/Rate).ToString());
            rec.Param.Add("y", (d_vy/Rate).ToString());
            bool flag=false;//解决选中线时，方向键只移动线
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
                                    port.link.PointTo(port.link.Points.Count - 1, new Point(port.link.Points[port.link.Points.Count - 1].X + d_vx, port.link.Points[port.link.Points.Count - 1].Y + d_vy));
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
            }
            if (record && rec.ObjectsNo.Count > 0)
            {
                Records.Push(rec);
            }
        }
        #endregion

        #region 选择操作
        /// <summary>
        /// 选中
        /// </summary>
        /// <param name="components"></param>

        public void SelectAll()
        {
            List<TPISComponent> components = new List<TPISComponent>();
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    components.Add((TPISComponent)obj);
                }
            }
            Select(components);
        }

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
                obj.isSelected = false;
            }

            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    if (components.Contains(obj as TPISComponent))
                    {
                        if (!((TPISComponent)obj).IsSelected)
                        {
                            ((TPISComponent)obj).IsSelected = true;
                            foreach (Port p in ((TPISComponent)obj).Ports)
                            {
                                if (p.CrossNo <= 0)
                                {
                                    foreach (ObjectBase objt in Objects)
                                    {
                                        if (objt.No == p.CrossNo && objt is ResultCross)
                                            objt.isSelected = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (obj is TPISLine)
                {
                    Boolean checkin = false, checkout = false;
                    foreach (TPISComponent cp in components)
                    {
                        if (cp.Ports.Contains(((TPISLine)obj).inPort))
                        {
                            checkin = true;
                        }
                        if (cp.Ports.Contains(((TPISLine)obj).outPort))
                        {
                            checkout = true;
                        }
                        if (checkin && checkout)
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
                if (obj is ResultCross)
                    obj.isSelected = false;
            }

            foreach (ObjectBase obj in Objects)
            {
                if (obj is ResultCross)
                {
                    if (objectBase == obj)
                    {
                        ((ResultCross)obj).isSelected = true;
                    }
                }
                else if (obj is TPISComponent)
                {
                    if (objectBase == obj)
                    {
                        ((TPISComponent)obj).IsSelected = true;
                        foreach (Port p in ((TPISComponent)obj).Ports)
                        {
                            if (p.CrossNo <= 0)
                            {
                                foreach (ObjectBase objt in Objects)
                                {
                                    if (objt.No == p.CrossNo && objt is ResultCross)
                                        objt.isSelected = true;
                                }
                            }
                        }
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
                        foreach (Port p in ((TPISComponent)obj).Ports)
                        {
                            if (p.CrossNo >= 0)
                            {
                                foreach (ObjectBase objt in Objects)
                                {
                                    if (objt.No == p.CrossNo && objt is ResultCross)
                                        objt.isSelected = false;
                                }
                            }
                        }
                    }
                }
                else if (obj is TPISLine)
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
        public void AddComponent(int tx, int ty, int width, int height, ComponentType ct, bool record = true)
        {
            Record rec = new Record();
            rec.Param.Add("Operation", "AddComponent");
            int n = 0;
            foreach (ObjectBase obj in this.Objects)
            {
                if (obj.No > n)
                    n = obj.No;
            }
            n++;
            Objects.Add(new TPISComponent(n, Rate, tx, ty, width, height, ct));
            rec.ObjectsNo.Add(n);
            if(record)
                Records.Push(rec);
        }

        //添加线
        public void AddLine(TPISLine line, bool record = true)
        {
            Record rec = new Record();
            rec.Param.Add("Operation", "AddLine");
            int n = 0;
            foreach (ObjectBase obj in this.Objects)
            {
                if (obj.No < n)
                    n = obj.No;
            }
            n--;
            line.No = n;
            Objects.Add(line);
            rec.ObjectsNo.Add(n);
            if (record)
                Records.Push(rec);
        }
        #endregion

        #region 选择后续操作 复制 删除 剪切
        //复制到clipboard
        public void CopySelection()
        {
            clipBoard.Objects = new List<ObjectBase>();
            foreach (ObjectBase obj in this.Objects)
            {
                if (obj.isSelected && (obj is TPISComponent || obj is TPISLine))
                {
                    clipBoard.Objects.Add(obj);
                }
            }
        }

        //删除
        public void DeleteSelection(bool record = true)
        {
            Record rec = new Record();
            rec.Param.Add("Operation", "Delete");
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent && obj.isSelected)
                {
                    foreach (Port p in ((TPISComponent)obj).Ports)
                    {
                        if (p.link != null && Objects.Contains(p.link))//去TPISLine的锚点
                            p.link.IsSelected = true;
                    }
                }
            }
            for (int i = 0; i < Objects.Count;)
            {
                ObjectBase obj = Objects[i];
                if (obj.isSelected)
                {
                    if (obj is TPISLine)//去TPISLine的锚点
                        ((TPISLine)obj).IsSelected = false;
                    Objects.Remove(obj);
                    rec.Objects.Add(obj);
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
                        if (p.link == null)
                        {
                            if (p.Type == NodType.DefIn || p.Type == NodType.DefOut)
                                p.Type = NodType.Undef;
                        }
                    }
                }
            }
            if(record && rec.Objects.Count > 0)
                Records.Push(rec);
        }

        //粘贴
        public void PasteSelection(double x, double y, bool record = true)
        {
            Record rec = new Record();
            rec.Param.Add("Operation", "Paste");
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
            foreach (ObjectBase obj in clipBoard.Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = ((TPISComponent)obj).Clone() as TPISComponent;
                    component.SetRate(Rate);
                    int n = 0;
                    foreach (ObjectBase objc in this.Objects)
                    {
                        if (objc.No > n)
                            n = objc.No;
                    }
                    n++;
                    component.No = n;
                    component.PosChange((int)offset_x, (int)offset_y);
                    foreach (Port port in component.Ports)
                    {
                        port.CrossNo = 1;
                    }
                    Objects.Add(component);
                    rec.ObjectsNo.Add(component.No);
                }
            }

            if (record && rec.ObjectsNo.Count > 0)
                Records.Push(rec);
        }
        #endregion

        #region 网格
        public void DrawGridSelection()
        {
            if (GridThickness == 0)
                GridThickness = 0.2;
            else
                GridThickness = 0;
        }
        #endregion

        #region 关闭工程
        public void ProjectCloseSelection()
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            mainwin.ProjectList.projects.Remove(this);
            mainwin.projectTab.ItemsSource = mainwin.ProjectList.projects;
            mainwin.projectTab.Items.Refresh();
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

        #region cross操作 
        public void AddCross(Port port)
        {
            int no = 0;
            foreach (ObjectBase obj in Objects)
            {
                if (obj.No <= no)
                {
                    no = obj.No - 1;
                }
            }
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = obj as TPISComponent;
                    if (component.Ports.Contains(port))
                    {
                        double vx, vy;
                        for (double d = 20; ; d += 10)
                        {
                            for (int angle = 0; angle < 360; angle += 5)
                            {
                                vx = component.Position.V_x + component.Position.V_width / 2 + d * Math.Sin(angle);
                                vy = component.Position.V_y + component.Position.V_height / 2 + d * Math.Cos(angle);
                                if (!CoverOrNot(vx, vy, CrossSize.WIDTH * Rate, CrossSize.HEIGHT * Rate))
                                {
                                    Objects.Add(new ResultCross(port, no, Rate, vx, vy));
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool CoverOrNot(double x, double y, double w, double h)
        {
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = obj as TPISComponent;
                    if (WithinOrNot(x, y, w, h, component.Position.V_x - 5, component.Position.V_y - 5, component.Position.V_width + 5, component.Position.V_height + 5))
                        return true;
                }
                if (obj is ResultCross)
                {
                    ResultCross cross = obj as ResultCross;
                    if (WithinOrNot(x, y, w, h, cross.Position.V_x - 5, cross.Position.V_y - 5, cross.Position.V_width + 5, cross.Position.V_height + 5))
                        return true;
                }
            }
            return false;
        }

        internal bool WithinOrNot(double x1, double y1, double w1, double h1, double x2, double y2, double w2, double h2)
        {
            if (x1 + w1 > x2 && x2 + w2 > x1 && y1 + h1 > y2 && y2 + h2 > y1)
                return true;
            else
                return false;

        }

        public void RemoveCross(int no)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is ResultCross && obj.No == no)
                {
                    Objects.Remove(obj);
                    return;
                }
            }
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
        }

        public ProjectItem(SerializationInfo info, StreamingContext context)
        {
            this.Name = info.GetString("name");
            this.Num = 2;
            this.Path = info.GetString("path");
            this.Canvas = (ProjectCanvas)info.GetValue("canvas", typeof(Object));
            this.Objects = (ObservableCollection<ObjectBase>)info.GetValue("objects", typeof(Object));
            this.PropertyGroup = (ObservableCollection<PropertyGroup>)info.GetValue("properties", typeof(Object));
            this.Rate = 1;
            this.clipBoard = new ClipBoard();
            this.Records = new RecordStack();
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
                        if (port.LinkNo <= 0)
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
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    foreach (Port port in ((TPISComponent)obj).Ports)
                    {
                        if (port.CrossNo <= 0)
                        {
                            foreach (ObjectBase objt in Objects)
                            {
                                if (obj is ResultCross && objt.No == port.CrossNo)
                                {
                                    ((ResultCross)objt).LinkPort = port;
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

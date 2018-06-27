﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Model.Common;
using TPIS.TPISCanvas;
using TPIS.Views;

namespace TPIS.Project
{
    [Serializable]
    public abstract class ObjectBase : ISerializable, ICloneable
    {
        public bool isSelected;
        public int No { get; set; }
        public bool isGrid;

        public abstract object Clone();
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
    public partial class ProjectItem : INotifyPropertyChanged, ISerializable, ICloneable
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

        public ObservableCollection<string> logs { get; set; }
        public ObservableCollection<string> Logs
        {
            get
            {
                return logs;
            }
            set
            {
                logs = value;
                OnPropertyChanged("Logs");
            }
        }

        public String Name { get; set; }
        public bool IsViewsMouseEnter { get; set; }
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

        #region 画布网格线粗（0：关闭网格线；1：打开网格线）
        public double gridThickness;
        public double GridThickness
        {
            get { return gridThickness; }
            set
            {
                gridThickness = value;
                foreach (ObjectBase obj in Objects)
                {
                    if (value != 0)
                        obj.isGrid = true;
                }
                OnPropertyChanged("GridThickness");
            }
        }
        #endregion

        #region 画布背景色
        public Brush backGroundColor;
        public Brush BackGroundColor
        {
            get { return backGroundColor; }
            set
            {
                backGroundColor = value;
                OnPropertyChanged("BackGroundColor");
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
        public ObservableCollection<ObjectBase> CopyObjects { get; set; }
        public ObservableCollection<ObjectBase> selectedObjects;
        public ObservableCollection<ObjectBase> SelectedObjects
        {
            get { return selectedObjects; }
            set
            {
                selectedObjects = value;
                OnPropertyChanged("SelectedObjects");
            }
        }
        public ObservableCollection<PropertyGroup> PropertyGroup { get; set; }
        public ObservableCollection<PropertyGroup> ResultGroup { get; set; }

        public bool IsViewWindowsOpen { get; set; }

        public ProjectCanvas Canvas { get; set; }

        public ProjectItem(string name, ProjectCanvas pCanvas, long num, string p)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            this.Name = name + ".tpis";
            this.Num = num;
            this.Canvas = pCanvas;
            Objects = new ObservableCollection<ObjectBase>();
            //SelectedObjects = new ObservableCollection<ObjectBase>();
            this.Rate = 1;
            this.clipBoard = new ClipBoard();
            Records = new RecordStack();
            this.CalculateState = false;
            this.GridThickness = 0;//赋初值0，使初始画布为隐藏网格
            this.GridUintLength = 20;//赋初值20，使初始网格单元为20×20
            //this.BackGroundColor = Brushes.White;
            this.BackGroundColor = mainwin.TPISconfig.CANVAS_BACKGROUNDCOLOR;
            Path = p;
            PropertyGroup = CommonTypeService.InitProjectProperty();
            ResultGroup = new ObservableCollection<PropertyGroup>();
            Logs = new ObservableCollection<string>();
            IsViewWindowsOpen = false;
            SaveProject();
            return;
        }

        #region 获取已选中对象
        public void ReSelect(TPISComponent component)
        {
            ObservableCollection<TPISLine> lines = new ObservableCollection<TPISLine>();
            component.isSelected = true;
            foreach (Port port in component.Ports)
            {
                if (port.link != null && !port.link.IsSelected)
                    lines.Add(port.link);
            }
            foreach (ObjectBase obj in Objects)
            {
                if (obj.isSelected && obj is TPISComponent)
                    if ((TPISComponent)obj != component)
                        foreach (Port port in ((TPISComponent)obj).Ports)
                            if (port.link != null && !port.link.IsSelected)
                                foreach (TPISLine line in lines)
                                    if (line.No == port.link.No)
                                        port.link.IsSelected = true;
            }
        }

        public void GetSelectedObjects()
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            SelectedObjects = new ObservableCollection<ObjectBase>();
            bool flag = mainwin.GetCurrentProject().IsViewWindowsOpen;
            if (SelectedObjects != null)
                SelectedObjects.Clear();
            foreach (ObjectBase obj in Objects)
            {
                if (obj.isSelected)
                    SelectedObjects.Add(obj);
            }
        }
        #endregion

        #region 存储工程
        public void SaveProject()
        {
            for (int i = 0; i < this.Objects.Count; i++)
            {//在缩放上，后台与显示一致
                ObjectBase obj = this.Objects[i];
                if (obj is TPISLine)
                {
                    ((TPISLine)obj).ReSetRate(this.Rate);
                }
            }
            string path = Path + "\\" + Name;
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            byte[] data = CommonFunction.SerializeToBinary(this);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(data);
            bw.Close();
            fs.Close();
            for (int i = 0; i < this.Objects.Count; i++)
            {//在缩放上，后台与显示一致
                ObjectBase obj = this.Objects[i];
                if (obj is TPISLine)
                {
                    ((TPISLine)obj).SetRate(this.Rate);
                }
            }
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
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

        /// <summary>
        /// 改变大小
        /// </summary>
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

        /// <summary>
        /// 水平和垂直方向移动
        /// </summary>
        /// <param name="d_vx"></param>
        /// <param name="d_vy"></param>
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

        #region 文字操作
        public void AmpText(bool record = true)
        {
            Record rec = new Record();
            String origin = "";
            String current = "";
            foreach (TPISText text in Objects)
            {
                if (text.IsSelected)
                {
                    double cf = FonsSizeTransfor(text.FontSize, true);
                    if (cf > 0)
                    {
                        origin += text.FontSize + "||";
                        text.FontSize = cf;
                        rec.ObjectsNo.Add(text.No);
                        current += text.FontSize + "||";
                    }
                }
            }
            if (record && rec.Objects.Count != 0)
            {
                rec.Param.Add("origin", origin);
                rec.Param.Add("current", current);
                Records.Push(rec);
            }
        }


        public void ShkText(bool record = true)
        {
            Record rec = new Record();
            String origin = "";
            String current = "";
            foreach (TPISText text in Objects)
            {
                if (text.IsSelected)
                {
                    double cf = FonsSizeTransfor(text.FontSize, false);
                    if (cf > 0)
                    {
                        origin += text.FontSize + "||";
                        text.FontSize = cf;
                        rec.ObjectsNo.Add(text.No);
                        current += text.FontSize + "||";
                    }
                }
            }
            if (record && rec.Objects.Count != 0)
            {
                rec.Param.Add("origin", origin);
                rec.Param.Add("current", current);
                Records.Push(rec);
            }
        }

        public void ToSizeText(double size, bool record = true)
        {
            Record rec = new Record();
            String origin = "";
            String current = "";
            foreach (TPISText text in Objects)
            {
                if (text.IsSelected)
                {
                    origin += text.FontSize + "||";
                    text.FontSize = size;
                    rec.ObjectsNo.Add(text.No);
                    current += text.FontSize + "||";
                }
            }
            if (record && rec.Objects.Count != 0)
            {
                rec.Param.Add("origin", origin);
                rec.Param.Add("current", current);
                Records.Push(rec);
            }
        }

        public static double[] FONSIZE = { 5, 5.5, 6.5, 7.5, 8, 9, 10, 10.5, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

        internal double FonsSizeTransfor(double cs, bool amp)
        {
            for (int i = 0; i < FONSIZE.Length; i++)
            {
                if (cs == FONSIZE[i])
                {
                    if (amp)
                    {
                        if (i + 1 < FONSIZE.Length)
                            return FONSIZE[i + 1];
                        else
                            return -1;
                    }
                    if (!amp)
                    {
                        if (i > 0)
                            return FONSIZE[i - 1];
                        else
                            return -1;
                    }
                }
            }
            return -1;
        }

        internal void UpdateText()
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            double fsize = -1;
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISText && obj.isSelected)
                {
                    TPISText text = (TPISText)obj;
                    if (fsize < 0)
                        fsize = text.fontSize;
                    else
                    {
                        if (fsize != text.fontSize)
                        {
                            mainwin.SetToolBarFontSize(-1);
                            return;
                        }
                    }
                }
            }
            for (int i = 0; i < FONSIZE.Length; i++)
            {
                if (FONSIZE[i] == fsize)
                {
                    mainwin.SetToolBarFontSize(i);
                    return;
                }
            }
            mainwin.SetToolBarFontSize(-1);
        }
        #endregion

        #region 选择操作
        /// <summary>
        /// 选中
        /// </summary>
        /// <param name="components"></param>

        public void SelectAll()
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            List<TPISComponent> components = new List<TPISComponent>();
            List<TPISText> texts = new List<TPISText>();
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    components.Add((TPISComponent)obj);
                }
                if (obj is TPISText)
                {
                    texts.Add((TPISText)obj);
                }
            }
<<<<<<< HEAD
            Select(components, texts);
            GetSelectedObjects();
            bool flag = mainwin.GetCurrentProject().IsViewWindowsOpen;
            if (!flag)
                mainwin.GetCurrentProject().GetSelectedObjects();
            UpdateText();
=======
            Select(components);
            //GetSelectedObjects();
            //bool flag = mainwin.GetCurrentProject().IsViewWindowsOpen;
            //if (!flag)
            //    mainwin.GetCurrentProject().GetSelectedObjects();
>>>>>>> 7f2c780c1bb991cd3512a2c6efd1dc2f9a84225d
        }

        internal void Select(List<TPISComponent> components, List<TPISText> texts)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (components.Count > 0)
            {
                if (mainwin.PropertyContent.ItemsSource != components[0].PropertyGroups)
                {
                    BindingPropertyWindow(components[0]);
                }
            }
            else
            {
                BindingPropertyWindow(null);
            }

            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                    ((TPISComponent)obj).IsSelected = false;
                if (obj is TPISLine)
                    ((TPISLine)obj).IsSelected = false;
                if (obj is TPISText)
                    ((TPISText)obj).IsSelected = false;
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
                else if (obj is TPISText)
                {
                    if (texts.Contains(obj as TPISText))
                    {
                        if (!((TPISText)obj).IsSelected)
                        {
                            ((TPISText)obj).IsSelected = true;
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
            //GetSelectedObjects();
        }

        private void BindingPropertyWindow(TPISComponent component)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (component == null)
            {
                mainwin.PropertyMode.DataContext = null;
                mainwin.PropertyContent.ItemsSource = PropertyGroup;
                mainwin.PropertyContent.Items.Refresh();
                mainwin.ResultWindow.ItemsSource = null;
                mainwin.PortResults.ItemsSource = null;
                mainwin.PortResults.Items.Refresh();
                return;
            }
            BindingOperations.ClearBinding(mainwin.PropertyMode, ComboBox.SelectedIndexProperty);

            mainwin.PropertyMode.ItemsSource = component.Mode;
            mainwin.PropertyMode.Items.Refresh();

            mainwin.PropertyContent.ItemsSource = component.PropertyGroups;
            mainwin.PropertyContent.Items.Refresh();

            mainwin.PropertyMode.SelectedIndex = component.selectedMode;
            Binding modeBinding = new Binding();
            modeBinding.Source = component;
            modeBinding.Path = new PropertyPath("SelectedMode");
            mainwin.PropertyMode.SetBinding(ComboBox.SelectedIndexProperty, modeBinding);

            mainwin.ResultWindow.ItemsSource = component.ResultGroups;
            mainwin.ResultWindow.Items.Refresh();
            mainwin.PortResults.ItemsSource = component.Ports;
            mainwin.PortResults.Items.Refresh();
            UpdateText();
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
                else if (obj is TPISText)
                {
                    if (objectBase == obj)
                    {
                        ((TPISText)obj).IsSelected = true;
                    }
                    else
                    {
                        ((TPISText)obj).IsSelected = false;
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
                        BindingPropertyWindow(obj as TPISComponent);
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
            //GetSelectedObjects();
            UpdateText();
        }

        internal void Select()
        {
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                    ((TPISComponent)obj).IsSelected = false;
                if (obj is TPISLine)
                    ((TPISLine)obj).IsSelected = false;
                if (obj is TPISText)
                    ((TPISText)obj).IsSelected = false;
                obj.isSelected = false;
            }
            BindingPropertyWindow(null);
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
            TPISComponent component = new TPISComponent(n, Rate, tx, ty, width, height, ct);
            if (this.GridThickness == 0)
                component.IsGrid = false;
            else
                component.IsGrid = true;
            if(component.eleType == TPISNet.EleType.WaterTag)
            {
                for(string pn ="A"; ; pn = IncreasePN(pn))
                {
                    int sum = 0;
                    foreach(ObjectBase obj in Objects)
                    {
                        if (obj is TPISComponent && ((TPISComponent)obj).eleType == TPISNet.EleType.WaterTag && ((TPISComponent)obj).PairName == pn)
                            sum++;
                    }
                    if(sum < 2)
                    {
                        component.PairName = pn;
                        break;
                    }
                }
            }
            else
            {
                component.PairName = "";
            }

            Objects.Add(component);
            rec.ObjectsNo.Add(n);
            if (record)
                Records.Push(rec);
        }
        internal string IncreasePN(string pn)
        {
            string result = "";
            int en = 1;
            for(int i=pn.Length-1; i>=0; i--)
            {
                if (en == 0) {
                    char c = pn[i];
                    result = c + result;
                    en = 0;
                }
                else
                {
                    char c = (char)(pn[i] + 1);
                    if (c > 'Z')
                    {
                        c = 'A';
                        en = 1;
                    }
                    else
                    {
                        en = 0;
                    }
                    result = c + result;
                }
            }
            if(en == 1)
            {
                result = 'A' + result;
            }
            return result;
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
            CopyObjects = new ObservableCollection<ObjectBase>();
            if (CopyObjects != null)
            {
                foreach (ObjectBase obj in CopyObjects)
                {
                    CopyObjects.Remove(obj);
                }
            }
            clipBoard.Objects = new List<ObjectBase>();
            List<TPISComponent> sl = new List<TPISComponent>();
            List<TPISText> texts = new List<TPISText>();
            foreach (ObjectBase obj in Objects)
            {
                if (obj.isSelected && obj is TPISComponent)
                {
                    sl.Add(obj as TPISComponent);
                }
                if (obj.isSelected && obj is TPISText)
                {
                    texts.Add((TPISText)obj);
                }
            }
            Select(sl,texts);

            foreach (ObjectBase obj in Objects)
            {
                if (obj.isSelected)
                {
                    CopyObjects.Add(obj.Clone() as ObjectBase);//剪贴板对象集用于计算复制控件的有效边界
                    clipBoard.Objects.Add(obj.Clone() as ObjectBase);
                }
            }
        }

        //删除
        public List<ObjectBase> DeleteSelection(bool record = true)
        {
            List<ObjectBase> DeleteContent = new List<ObjectBase>();
            Record rec = new Record();
            rec.Param.Add("Operation", "Delete");
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent && obj.isSelected)
                {
                    foreach (Port p in ((TPISComponent)obj).Ports)
                    {
                        if (p.link != null && Objects.Contains(p.link))
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
                    DeleteContent.Add(obj);
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
                        if (p.link == null || !Objects.Contains(p.link))
                        {
                            p.link = null;
                            if (p.Type == NodType.DefIn || p.Type == NodType.DefOut)
                                p.Type = NodType.Undef;
                        }
                    }
                }
            }
            if (record && rec.Objects.Count > 0)
                Records.Push(rec);
            return DeleteContent;
        }

        //粘贴
        public void PasteSelection(double x, double y, bool record = true)
        {
            Record rec = new Record();
            rec.Param.Add("Operation", "Paste");
            //Boolean init = false;
            //double min_x = 0, min_y = 0;
            ////计算粘贴后偏移量
            //foreach (ObjectBase obj in clipBoard.Objects)
            //{
            //    if (obj is TPISComponent)
            //    {
            //        if (!init)
            //        {
            //            min_x = ((TPISComponent)obj).Position.V_x;
            //            min_y = ((TPISComponent)obj).Position.V_y;
            //            init = true;
            //        }
            //        else
            //        {
            //            min_x = Math.Min(((TPISComponent)obj).Position.V_x, min_x);
            //            min_y = Math.Min(((TPISComponent)obj).Position.V_y, min_y);
            //        }
            //    }
            //}
            //double offset_x = x - min_x;
            //double offset_y = y - min_y;

            double offset_x = x - this.WorkSpaceSize_LU(CopyObjects).X;
            double offset_y = y - this.WorkSpaceSize_LU(CopyObjects).Y;

            //按偏移量粘贴并选中
            Dictionary<int, int> NoMap = new Dictionary<int, int>();
            foreach (ObjectBase obj in clipBoard.Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = (obj as TPISComponent).Clone() as TPISComponent;
                    component.SetRate(Rate);
                    int n = 0;
                    foreach (ObjectBase objc in this.Objects)
                    {
                        if (objc.No > n)
                            n = objc.No;
                    }
                    n++;
                    NoMap.Add(component.No, n);
                    component.No = n;
                    component.PosChange((int)offset_x, (int)offset_y);
                    Objects.Add(component);
                    rec.ObjectsNo.Add(component.No);
                }
                if (obj is TPISLine)
                {
                    TPISLine line = obj.Clone() as TPISLine;
                    line.SetRate(Rate);
                    int n = 0;
                    foreach (ObjectBase objc in Objects)
                    {
                        if (objc.No < n)
                            n = objc.No;
                    }
                    n--;
                    NoMap.Add(line.No, n);
                    line.No = n;
                    line.PosChange((int)offset_x, (int)offset_y);
                    Objects.Add(line);
                    rec.ObjectsNo.Add(line.No);
                }
                if (obj is ResultCross)
                {
                    ResultCross cross = obj.Clone() as ResultCross;
                    cross.SetRate(Rate);
                    int n = 0;
                    foreach (ObjectBase objc in Objects)
                    {
                        if (objc.No < n)
                            n = objc.No;
                    }
                    n--;
                    NoMap.Add(cross.No, n);
                    cross.No = n;
                    cross.PosChange((int)offset_x, (int)offset_y);
                    Objects.Add(cross);
                    rec.ObjectsNo.Add(cross.No);
                }
            }

            //更改所有no
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    if (NoMap.ContainsValue(obj.No))
                    {
                        TPISComponent component = obj as TPISComponent;
                        foreach (Port port in component.Ports)
                        {
                            if (port.LinkNo <= 0)
                            {
                                if (NoMap.ContainsKey(port.LinkNo))
                                    port.LinkNo = NoMap[port.LinkNo];
                                else
                                    port.LinkNo = 1;
                            }
                            if (port.CrossNo <= 0)
                            {
                                if (NoMap.ContainsKey(port.CrossNo))
                                    port.CrossNo = NoMap[port.CrossNo];
                                else
                                    port.CrossNo = 1;
                            }
                        }
                    }
                }
            }

            RebuildLink();

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
            Record record = Records.PopUndo();
            if (record != null)
            {
                //项目变更时，关闭工程提醒是否保存
                if (MessageBox.Show("是否保存当前工程？", "提示", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    SaveProject();//先保存后关闭
                    MessageBox.Show("项目已保存");
                }
            }
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            mainwin.ProjectList.projects.Remove(this);
            mainwin.projectTab.ItemsSource = mainwin.ProjectList.projects;
            mainwin.projectTab.Items.Refresh();
            if (mainwin.ProjectList.projects.Count == 0)
            {//若无工程，状态栏显示空
                mainwin.CurProjectShow("Null");//工程名为空
                mainwin.CurWorkspaceSizeShow("0", "0");//工作区大小为空
                mainwin.CurProjectAddressShow("Null", "Null");//工程地址为空
            }
        }
        #endregion

        #region 有效工作区大小-右下点
        public Point WorkSpaceSize_RD(ObservableCollection<ObjectBase> SelectObjects)
        {
            Point p = new Point(0, 0);//无控件，边界为0×0
            for (int i = 0; i < SelectObjects.Count; i++)
            {
                int x = (int)this.Canvas.Width;
                int y = (int)this.Canvas.Height;
                ObjectBase obj = SelectObjects[i];
                if (obj is TPISComponent)
                {
                    x = (int)(((TPISComponent)obj).Position.X + ((TPISComponent)obj).Position.Width + 10);
                    y = (int)(((TPISComponent)obj).Position.Y + ((TPISComponent)obj).Position.Height + 10);
                }
                else if (obj is TPISLine)
                {
                    x = (int)((TPISLine)obj).Points[0].X + 10;
                    y = (int)((TPISLine)obj).Points[0].Y + 10;
                    foreach (Point p1 in ((TPISLine)obj).Points)
                    {
                        x = (int)(x > p1.X ? x : p1.X + 10);
                        y = (int)(y > p1.Y ? y : p1.Y + 10);
                    }
                    {//拖动控件、缩放画布问题
                        x = (int)(x / this.Rate);
                        y = (int)(y / this.Rate);
                    }
                }
                else if (obj is ResultCross)
                {
                    x = (int)(((ResultCross)obj).Position.X + ((ResultCross)obj).Position.Width + 10);
                    y = (int)(((ResultCross)obj).Position.Y + ((ResultCross)obj).Position.Height + 10);
                }
                else if (obj is TPISText)
                {
                    x = (int)(((TPISText)obj).Position.X + ((TPISText)obj).Position.Width + 10);
                    y = (int)(((TPISText)obj).Position.Y + ((TPISText)obj).Position.Height + 10);
                }
                p.X = p.X > x ? p.X : x;
                p.Y = p.Y > y ? p.Y : y;
            }
            return p;
        }
        #endregion

        #region 有效工作区大小-左上点
        public Point WorkSpaceSize_LU(ObservableCollection<ObjectBase> SelectObjects)
        {
            Point p = new Point(0, 0);//无控件，边界为0×0

            if (SelectObjects.Count > 0)
            {
                int x = 0;
                int y = 0;
                ObjectBase obj0 = SelectObjects[0];
                if (obj0 is TPISComponent)
                {
                    x = (int)((TPISComponent)obj0).Position.X;
                    y = (int)((TPISComponent)obj0).Position.Y;
                }
                else if (obj0 is TPISLine)
                {
                    x = (int)((TPISLine)obj0).Points[0].X;
                    y = (int)((TPISLine)obj0).Points[0].Y;
                    foreach (Point p1 in ((TPISLine)obj0).Points)
                    {
                        x = (int)(x < p1.X ? x : p1.X);
                        y = (int)(y < p1.Y ? y : p1.Y);
                    }
                }
                p.X = x;
                p.Y = y;
                for (int i = 0; i < SelectObjects.Count; i++)
                {
                    ObjectBase obj = SelectObjects[i];
                    if (obj is TPISComponent)
                    {
                        x = (int)((TPISComponent)obj).Position.X;
                        y = (int)((TPISComponent)obj).Position.Y;
                    }
                    else if (obj is TPISLine)
                    {
                        x = (int)((TPISLine)obj).Points[0].X;
                        y = (int)((TPISLine)obj).Points[0].Y;
                        foreach (Point p1 in ((TPISLine)obj).Points)
                        {
                            x = (int)(x < p1.X ? x : p1.X);
                            y = (int)(y < p1.Y ? y : p1.Y);
                        }
                    }
                    p.X = p.X < x ? p.X : x;
                    p.Y = p.Y < y ? p.Y : y;
                }
            }
            return p;
        }
        #endregion

        #region 有效工作区大小-中心点
        public Point WorkSpaceSize_Center(ObservableCollection<ObjectBase> SelectObjects)
        {
            Point p1 = WorkSpaceSize_RD(SelectObjects);
            Point p2 = WorkSpaceSize_LU(SelectObjects);
            Point p = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            return p;
        }
        #endregion

        #region 有效工作区大小(宽高)
        public Point WorkSpaceSize_Size(ObservableCollection<ObjectBase> SelectObjects)
        {
            Point p1 = WorkSpaceSize_RD(SelectObjects);
            Point p2 = WorkSpaceSize_LU(SelectObjects);
            Point p = new Point((p1.X - p2.X) + 100, (p1.Y - p2.Y) + 100);
            return p;
        }
        #endregion

        //#region 已选对象-左上点
        //public Point WorkSpaceSize_LU(ObservableCollection<ObjectBase> LUObjects)
        //{
        //    Point p = new Point(0, 0);//无控件，边界为0×0

        //    if (LUObjects != null)
        //    {
        //        if (LUObjects.Count > 0)
        //        {
        //            int x = 0;
        //            int y = 0;
        //            ObjectBase obj0 = LUObjects[0];
        //            if (obj0 is TPISComponent)
        //            {
        //                x = (int)((TPISComponent)obj0).Position.X;
        //                y = (int)((TPISComponent)obj0).Position.Y;
        //            }
        //            else if (obj0 is TPISLine)
        //            {
        //                x = (int)((TPISLine)obj0).Points[0].X;
        //                y = (int)((TPISLine)obj0).Points[0].Y;
        //                foreach (Point p1 in ((TPISLine)obj0).Points)
        //                {
        //                    x = (int)(x < p1.X ? x : p1.X);
        //                    y = (int)(y < p1.Y ? y : p1.Y);
        //                }
        //            }
        //            p.X = x;
        //            p.Y = y;
        //            for (int i = 0; i < LUObjects.Count; i++)
        //            {
        //                ObjectBase obj = LUObjects[i];
        //                if (obj is TPISComponent)
        //                {
        //                    x = (int)((TPISComponent)obj).Position.X;
        //                    y = (int)((TPISComponent)obj).Position.Y;
        //                }
        //                else if (obj is TPISLine)
        //                {
        //                    x = (int)((TPISLine)obj).Points[0].X;
        //                    y = (int)((TPISLine)obj).Points[0].Y;
        //                    foreach (Point p1 in ((TPISLine)obj).Points)
        //                    {
        //                        x = (int)(x < p1.X ? x : p1.X);
        //                        y = (int)(y < p1.Y ? y : p1.Y);
        //                    }
        //                }
        //                p.X = p.X < x ? p.X : x;
        //                p.Y = p.Y < y ? p.Y : y;
        //            }
        //        }
        //    }
        //    return p;
        //}
        //#endregion

        #region 元件移动-边界限制
        public void MoveChange(double x, double y)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            Point tmp;
            tmp = this.WorkSpaceSize_LU(mainwin.GetCurrentProject().Objects);
            if (tmp.X < 0 && tmp.Y < 0)
            {//限制超出左上边界
                this.MoveSelection(1, 1);
            }
            else if (tmp.X < 0)
            {//限制超出左边界
                this.MoveSelection(1, y);
            }
            else if (tmp.Y < 0)
            {//限制超出上边界
                this.MoveSelection(x, 1);
            }
            else
            {
                this.MoveSelection(x, y);
            }
            ChangeWorkSpaceSize();//移动控件时，超过边界自动改变画布大小
        }
        #endregion

        #region 线锚点移动-边界限制
        public void LineAnchorPointsMoveChange(TPISLine line, Point endPoint, int LineAnchorPointID, object sender)
        {
            if (endPoint.X - 10 < 0 && endPoint.Y - 10 < 0)
            {
                endPoint.X = 10;
                endPoint.Y = 10;
            }
            else if (endPoint.X - 10 < 0)
                endPoint.X = 10;
            else if (endPoint.Y - 10 < 0)
                endPoint.Y = 10;
            line.PointTo(LineAnchorPointID + 1, endPoint);
            ChangeWorkSpaceSize();//移动控件时，超过边界自动改变画布大小
        }
        #endregion

        #region 控件拖动放大工作区
        private void ChangeWorkSpaceSize()
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            ScrollViewer scrollViewer = mainwin.SelectScrollViewer();
            Point p = new Point();
            p = this.WorkSpaceSize_RD(mainwin.GetCurrentProject().Objects);
            if (p.X >= this.Canvas.Width)
            {
                this.Canvas.Width = (int)p.X + 10;
                scrollViewer.ScrollToRightEnd();
            }
            if (p.Y >= this.Canvas.Height)
            {
                this.Canvas.Height = (int)p.Y + 10;
                scrollViewer.ScrollToBottom();
            }
            mainwin.CurWorkspaceSizeShow(this.Canvas.Width.ToString(), this.Canvas.Height.ToString());//状态栏显示工作区大小
        }
        #endregion

        //#region 获取ProjectDesignerCanvas
        //private ProjectDesignerCanvas SelectProjectDesignerCanvas(object sender)
        //{
        //    if (sender is LineAnchorPoint)
        //    {
        //        DependencyObject obj = ((DesignerLine)((LineAnchorPoint)sender).Parent).TemplatedParent;
        //        do
        //            obj = VisualTreeHelper.GetParent(obj);
        //        while (obj.GetType() != typeof(ProjectDesignerCanvas));
        //        return (ProjectDesignerCanvas)obj;
        //        //return (ProjectDesignerCanvas)((LineAnchorPoint)sender).Parent;
        //    }
        //    else if (sender is DesignerComponent)
        //    {
        //        DependencyObject obj = ((DesignerComponent)sender).TemplatedParent;
        //        do
        //            obj = VisualTreeHelper.GetParent(obj);
        //        while (obj.GetType() != typeof(ProjectDesignerCanvas));
        //        return (ProjectDesignerCanvas)obj;
        //    }
        //    return null;
        //}
        //#endregion

        #region 查找元件并选中居中
        internal bool FindComponent(int tn)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            Boolean findOrNot = false;
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    if (obj.No == tn)
                    {
                        findOrNot = true;
                        ((TPISComponent)obj).IsSelected = true;
                        ScrollViewer sv = mainwin.SelectScrollViewer();
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


        public void AddText(int vx, int vy, double fontsize)
        {
            int no = 0;
            foreach (ObjectBase obj in Objects)
            {
                if (obj.No <= no)
                {
                    no = obj.No - 1;
                }
            }
            Objects.Add(new TPISText(fontsize, no, Rate, vx, vy));
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
            //info.AddValue("selectedObjects", SelectedObjects);
            info.AddValue("path", Path);
            info.AddValue("properties", PropertyGroup);
            info.AddValue("result", ResultGroup);
        }

        public ProjectItem(SerializationInfo info, StreamingContext context)
        {
            this.Name = info.GetString("name");
            //this.Num = 2;
            this.Path = info.GetString("path");
            this.Canvas = (ProjectCanvas)info.GetValue("canvas", typeof(Object));
            this.Objects = (ObservableCollection<ObjectBase>)info.GetValue("objects", typeof(Object));
            //this.SelectedObjects = (ObservableCollection<ObjectBase>)info.GetValue("selectedObjects", typeof(Object));
            this.PropertyGroup = (ObservableCollection<PropertyGroup>)info.GetValue("properties", typeof(Object));
            this.ResultGroup = (ObservableCollection<PropertyGroup>)info.GetValue("result", typeof(Object));
            this.clipBoard = new ClipBoard();
            this.Records = new RecordStack();
            this.Rate = 1;
            this.CalculateState = false;
            Logs = new ObservableCollection<string>();

            this.GridThickness = 0;//赋初值0，使初始画布为隐藏网格
            this.GridUintLength = 20;//赋初值20，使初始网格单元为20×20

            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            this.BackGroundColor = mainwin.TPISconfig.CANVAS_BACKGROUNDCOLOR;


            return;
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

        public object Clone()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, this);
            stream.Position = 0;
            Object obj = bf.Deserialize(stream);
            stream.Close();
            return obj;
        }
        #endregion
    }
}

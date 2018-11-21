using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using TPIS.Model;
using TPIS.Model.Common;

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
            if (cr < 0.1)
                return 0.1;
            else if (cr < 0.25)
                return 0.25;
            else if (cr < 0.5)
                return 0.5;
            else if (cr < 0.75)
                return 0.75;
            else if (cr < 1)
                return 1;
            else if (cr < 1.25)
                return 1.25;
            else if (cr < 1.5)
                return 1.5;
            else
                return Math.Floor(cr) + 1;
        }

        public static double GetSubRate(double cr)
        {
            if (cr <= 0.25)
                return 0.1;
            else if (cr <= 0.5)
                return 0.25;
            else if (cr <= 0.75)
                return 0.5;
            else if (cr <= 1)
                return 0.75;
            else if (cr <= 1.25)
                return 1;
            else if (cr <= 1.5)
                return 1.25;
            else if (cr <= 2)
                return 1.5;
            else
            {
                if (Math.Floor(cr) == cr)
                    return cr - 1;
                else
                    return Math.Floor(cr);
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

        public ObservableCollection<string> logs;
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

        public int MaxIter { get; set; }
        public int WaterStand { get; set; }
        public int GasStand { get; set; }
        public int LineThickness { get; set; }
        public String name;
        public String Name {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public long Num { get; set; }
        public String Path { get; set; }

        public ObservableCollection<ObjectBase> Objects { get; set; }
        public ObservableCollection<PropertyGroup> propertyGroup;
        public ObservableCollection<PropertyGroup> PropertyGroup {
            get => propertyGroup;
            set {
                propertyGroup = value;
                OnPropertyChanged("PropertyGroup");
            }
        }
        public ObservableCollection<PropertyGroup> resultGroup;
        public ObservableCollection<PropertyGroup> ResultGroup
        {
            get => resultGroup;
            set
            {
                resultGroup = value;
                OnPropertyChanged("ResultGroup");
            }
        }

        public void OnCaculateFinished()
        {
            OnPropertyChanged("PropertyGroup");
            OnPropertyChanged("ResultGroup");
        }

        public ProjectCanvas Canvas { get; set; }

        public ProjectItem(string name, ProjectCanvas pCanvas, long num, string p)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            MaxIter = mainwin.TPISconfig.MAX_ITER;
            GasStand = mainwin.TPISconfig.GAS_STAND;
            WaterStand = mainwin.TPISconfig.WATER_STAND;
            this.Name = name + ".tpis";
            this.Num = num;
            this.Canvas = pCanvas;
            Objects = new ObservableCollection<ObjectBase>();
            //SelectedObjects = new ObservableCollection<ObjectBase>();
            this.Rate = 1;
            this.clipBoard = new ClipBoard();
            Records = new RecordStack();
            this.CalculateState = false;
            this.GridThickness = mainwin.TPISconfig.CANVAS_GRID;//赋初值0，使初始画布为隐藏网格
            this.GridUintLength = 40;//赋初值40，使初始网格单元为40×40
            //this.BackGroundColor = Brushes.White;
            this.BackGroundColor = mainwin.TPISconfig.CANVAS_BACKGROUNDCOLOR;
            Path = p;
            PropertyGroup = CommonTypeService.InitProjectProperty();
            ResultGroup = new ObservableCollection<PropertyGroup>();
            LineThickness = mainwin.TPISconfig.LINE_THICKNESS;
            Logs = new ObservableCollection<string>();
            IsViewWindowsOpen = false;
            IsPortSetWindowOpen = false;
            InitBackEnd();
            SaveProject();
        }
        
        public TPISNet.Net BackEnd { get; set; }

        private void InitBackEnd()
        {
            BackEnd = new TPISNet.Net();
        }

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
            string path = Path;
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
            info.AddValue("LResultCal", BackEnd.LResultCal);
            info.AddValue("gasstand", GasStand);
            info.AddValue("waterstand", WaterStand);
            info.AddValue("maxiter", MaxIter);
            info.AddValue("LineThickness", LineThickness);
        }

        public ProjectItem(SerializationInfo info, StreamingContext context)
        {
            this.Name = info.GetString("name");
            GasStand = info.GetInt32("gasstand");
            WaterStand = info.GetInt32("waterstand");
            MaxIter = info.GetInt32("maxiter");
            LineThickness = info.GetInt32("LineThickness");
            //this.Num = 2;
            this.Path = info.GetString("path");
            this.Canvas = (ProjectCanvas)info.GetValue("canvas", typeof(Object));
            this.Objects = (ObservableCollection<ObjectBase>)info.GetValue("objects", typeof(Object));
            //this.SelectedObjects = (ObservableCollection<ObjectBase>)info.GetValue("selectedObjects", typeof(Object));
            this.PropertyGroup = (ObservableCollection<PropertyGroup>)info.GetValue("properties", typeof(Object));
            this.ResultGroup = (ObservableCollection<PropertyGroup>)info.GetValue("result", typeof(Object));
            InitBackEnd();
            BackEnd.LResultCal = (System.Collections.Generic.List<TPISNet.Calculator>)info.GetValue("LResultCal", typeof(Object));
            this.clipBoard = new ClipBoard();
            this.Records = new RecordStack();
            this.Rate = 1;
            this.CalculateState = false;
            Logs = new ObservableCollection<string>();

            this.GridThickness = 1;//赋初值0，使初始画布为隐藏网格
            this.GridUintLength = 40;//赋初值40，使初始网格单元为40×40

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
                                        if (port.Type == NodType.DefOut || port.Type == NodType.Outlet)
                                            line.InPort = port;
                                        else
                                            line.OutPort = port;
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
            for (int i = 0; i < Objects.Count;)
            {
                ObjectBase obj = Objects[i];
                if (obj is HiddenLink)
                {
                    Objects.Remove(obj);
                }
                else
                {
                    i++;
                }
            }
            List<HiddenLink> tmphls = new List<HiddenLink>();
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent && (obj as TPISComponent).eleType == TPISNet.EleType.WaterTag)
                {
                    foreach (ObjectBase objt in Objects)
                    {
                        if (objt is TPISComponent && (objt as TPISComponent).eleType == TPISNet.EleType.WaterTag && (objt as TPISComponent).PairName == (obj as TPISComponent).PairName)
                        {
                            tmphls.Add(new HiddenLink(obj as TPISComponent, objt as TPISComponent));
                        }
                    }
                }
            }
            foreach (HiddenLink hl in tmphls)
            {
                Objects.Add(hl);
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

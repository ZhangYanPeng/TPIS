﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TPIS.Model.Common;
using TPIS.Project;

namespace TPIS.Model
{
    [Serializable]
    public class Port : INotifyPropertyChanged , ISerializable
    {
        /// <summary>
        /// 序列化与反序列化
        /// </summary>
        #region
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("dicName", DicName);
            info.AddValue("name", Name);
            info.AddValue("xpos", x);
            info.AddValue("ypos", y);
            info.AddValue("material", MaterialType);
            info.AddValue("nodType", Type);
            info.AddValue("canlink", CanLink);
            info.AddValue("cancancel", CanCancel);
            info.AddValue("results", Results);
            info.AddValue("crossNo", CrossNo);
            info.AddValue("showResult", ShowResult);
            if (link == null)
            {
                if(LinkNo<=0)
                    info.AddValue("linkNo", LinkNo);
                else
                    info.AddValue("linkNo", 1);
            }
            else
                info.AddValue("linkNo", link.No);
        }

        public Port(SerializationInfo info, StreamingContext context)
        {
            this.DicName = info.GetString("dicName");
            this.Name = info.GetString("name");
            this.x = info.GetDouble("xpos");
            this.y = info.GetDouble("ypos");
            this.MaterialType = (TPISNet.Material)info.GetValue("material", typeof(Object));
            this.Type = (NodType)info.GetValue("nodType", typeof(Object));
            this.CanLink = info.GetBoolean("canlink");
            this.CanCancel = info.GetBoolean("cancancel");
            this.Results = (ObservableCollection<Property>)info.GetValue("results", typeof(Object));
            this.LinkNo = info.GetInt32("linkNo");
            this.CrossNo = info.GetInt32("crossNo");
            this.ShowResult = info.GetBoolean("showResult");
        }
        #endregion

        public ObservableCollection<Property> results;
        public ObservableCollection<Property> Results {
            get=>results;
            set
            {
                results = value;
                OnPropertyChanged("Results");
            }
        }

        public double x;//横坐标比例（0~1）
        public double X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }
        public double p_x;
        public double P_x
        {
            get => p_x;
            set
            {
                p_x = value;
                OnPropertyChanged("P_x");
            }
        }
        public double a_x;
        public double A_x
        {
            get => a_x;
            set
            {
                a_x = value;
                OnPropertyChanged("A_x");
            }
        }

        public double y;//纵坐标比例（0~1)
        public double Y
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
        }
        public double p_y;
        public double P_y
        {
            get => p_y;
            set
            {
                p_y = value;
                OnPropertyChanged("P_y");
            }
        }
        public double a_y;
        public double A_y
        {
            get => a_y;
            set
            {
                a_y = value;
                OnPropertyChanged("A_y");
            }
        }

        public Node node;
        public TPISLine link;
        public int LinkNo { get; set; }
        public int CrossNo { get; set; }
        public bool showResult;
        public bool ShowResult {
            get => showResult;
            set{
                showResult = value;
                if (showResult && CrossNo>0)
                {
                    //添加Cross
                    MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                    ProjectItem pi = mainwin.GetRelateProject(this);
                    if( pi != null)
                    {
                        pi.AddCross(this);
                    }
                }
                else if (!showResult && CrossNo <= 0)
                {
                    //删除Cross
                    MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                    ProjectItem pi = mainwin.GetRelateProject(this);
                    if (pi != null)
                    {
                        pi.RemoveCross(CrossNo);
                        CrossNo = 1;
                    }
                }
                OnPropertyChanged("ShowResult");
            }
        }

        public string DicName { get; set; }
        public string Name { get; set; }
        public TPISNet.Material MaterialType { get; set; }
        public NodType type;
        public NodType Type {
            get => type;
            set
            {
                type = value;
                switch (type)
                {
                    case NodType.Inlet:
                    case NodType.DefIn: PortColor = Brushes.Green; break;
                    case NodType.Outlet:
                    case NodType.DefOut: PortColor = Brushes.Red; break;
                    case NodType.Undef: PortColor = Brushes.Gray; break;
                }
                OnPropertyChanged("PortColor");
            }
        }
        public Brush portColor;
        public Brush PortColor
        {
            get => portColor;
            set
            {
                portColor = value;
                OnPropertyChanged("PortColor");
            }
        }
        public bool CanLink { get; set; }//受否可以连接
        public bool CanCancel { get; set; }

        //构造函数
        #region
        public Port()
        {
            Results = new ObservableCollection<Property>();
            CrossNo = 1;
            ShowResult = false;
        }

        public Port(string dicName, string name, double xpos, double ypos, TPISNet.Material material, NodType nodType, bool canlink, bool cancancel)
        {
            DicName = dicName;
            Name = name;
            x = xpos;
            y = ypos;
            MaterialType = material;
            Type = nodType;
            CanLink = canlink;
            CanCancel = cancancel;
            Results = new ObservableCollection<Property>();
            CrossNo = 1;
            ShowResult = false;
        }
        public Port(string dicName, string name, double xpos, double ypos, TPISNet.Material material, NodType nodType, bool canlink)
        {
            DicName = dicName;
            Name = name;
            x = xpos;
            y = ypos;
            MaterialType = material;
            Type = nodType;
            CanLink = canlink;
            CanCancel = canlink;
            Results = new ObservableCollection<Property>();
            CrossNo = 1;
            ShowResult = false;
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
    }
}

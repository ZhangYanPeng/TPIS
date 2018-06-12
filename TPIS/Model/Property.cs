
using CurvesData;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows;
using TPIS.Model.Common;

namespace TPIS.Model
{
    /// <summary>
    /// Range定义
    /// </summary>
    #region
    public class ValueRange
    {
        double Min;
        double Max;
        public double DefaultV;
        public bool Doing;
        public ValueRange()
        {
            Doing = false;
        }
        public ValueRange(double min, double max, double defaultV)
        {
            Min = min;
            Max = max;
            DefaultV = defaultV;
            Doing = true;
        }
        public bool Contain(double v)
        {
            if (Doing)
            {
                if (v >= Min && v <= Max)
                    return true;
                else
                    return false;
            }
            return true;
        }
    }
    #endregion

    [Serializable]
    public class Property : INotifyPropertyChanged, ISerializable
    {
        /// <summary>
        /// 序列化与反序列化
        /// </summary>
        #region
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("dicName", DicName);
            info.AddValue("name", Name);
            info.AddValue("units", Units);
            info.AddValue("type", Type);
            info.AddValue("tips", Tips);
            info.AddValue("unitNum", UnitNum);
            info.AddValue("isStrOrNum", IsStrOrNum);
            info.AddValue("valStr", valStr);
            info.AddValue("valNum", valNum);
            info.AddValue("modes", Modes);
            info.AddValue("isKnown", IsKnown);
            info.AddValue("visible", visible);
            info.AddValue("color", Color);
            info.AddValue("isLine", IsLine);
            if(IsLine)
            {
                CurveDatas curveDatas = new CurveDatas(Curve);
                info.AddValue("curve", curveDatas);
            }
        }

        public Property(SerializationInfo info, StreamingContext context)
        {
            DicName = info.GetString("dicName"); ;
            Name = info.GetString("name");
            Units = (string[])info.GetValue("units", typeof(Object));
            Type = (P_Type)info.GetValue("type", typeof(Object));
            Color = (TPISNet.PColor)info.GetValue("color", typeof(TPISNet.PColor));
            Tips = info.GetString("tips");
            IsStrOrNum = info.GetBoolean("isStrOrNum");
            bool vb = info.GetBoolean("visible");
            if (vb)
                Visibility = Visibility.Visible;
            else
                Visibility = Visibility.Collapsed;

            valStr = info.GetString("valStr");
            valNum = info.GetDouble("valNum");


            IsLine = info.GetBoolean("isLine");
            if (IsLine)
            {
                Curve = new Curves(Name, Units[0], Units[1]);
                Curve.Lx1x2.Add(new XYDataLine());
                CurveDatas curveDatas = (CurveDatas)info.GetValue("curve", typeof(Object));
                curveDatas.read(Curve);
            }
            else
                Curve = null;
            
            if (IsStrOrNum)
            {
                showValue = valStr;
            }
            else
            {
                showValue = ValueConvert(valNum, Units[0], Units[0]);
            }
            IsKnown = info.GetBoolean("isKnown");
            UnitNum = info.GetInt32("unitNum");
            Modes = (ObservableCollection<SelMode>)info.GetValue("modes", typeof(Object));
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

        public string DicName { get; set; }
        public string Name { get; set; }
        public string Tips { get; set; }
        public string[] Units { get; set; }
        public Curves Curve { get; set; }
        public TPISNet.PColor Color { get; set; }

        public int unitNum;
        public int UnitNum
        {
            get => unitNum;
            set
            {
                unitNum = value;
                if (!IsStrOrNum)
                {
                    if (!IsKnown)
                        showValue = "";
                    else
                        showValue = ValueConvert(valNum, Units[0], Units[unitNum]);
                }
                OnPropertyChanged("ShowValue");
            }
        }

        public P_Type Type { get; set; }

        public bool IsStrOrNum { get; set; } // true string false num
        public string valStr;
        public double valNum;
        public ObservableCollection<SelMode> Modes { get; set; }

        public string showValue;
        public string ShowValue
        {
            get => showValue;
            set
            {
                if (Name == "干度")
                {
                    try
                    {
                        double.Parse(value as string);
                        if (value == "" || value == null)
                            IsKnown = false;
                        else
                            IsKnown = true;
                        valNum = double.Parse(value as string);
                        if (valNum > 1)
                            showValue = "过热汽";
                        else if (valNum < 0)
                            showValue = "过冷水";
                        else
                            showValue = value;
                    }
                    catch
                    {
                        showValue = value;
                    }
                }
                else
                {
                    showValue = value;
                    if (value == "" || value == null)
                        IsKnown = false;
                    else
                        IsKnown = true;
                    if (IsStrOrNum)
                        valStr = (string)value;
                    else
                        valNum = ValueConvertBack(value, Units[UnitNum], Units[0]);
                }
                OnPropertyChanged("ShowValue");
            }
        }

        public Boolean IsKnown { get; set; }
        public Boolean IsLine { get; set; }

        //属性可见性
        #region
        public bool visible;
        public Visibility Visibility
        {
            get
            {
                if (visible)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }

            set
            {
                if (value == Visibility.Visible)
                {
                    visible = true;
                }
                else
                {
                    visible = false;
                }
                OnPropertyChanged("Visibility");
            }
        }

        public void SelectProperty(SelMode selMode, bool type)
        {
            if (type)
                visible = true;
            else
            {
                if (Modes.Contains(selMode))
                    visible = true;
                else
                    visible = false;
            }
            OnPropertyChanged("Visibility");
        }
        #endregion

        public Property(string dicName, string d_str, string name, string value, string[] units, P_Type type, TPISNet.PColor pcolor, ObservableCollection<SelMode> modes = null, string tips = "")
        {
            IsLine = false;
            SetKnown(d_str);
            DicName = dicName;
            Name = name;
            Units = units;
            Type = type;
            Tips = tips;
            UnitNum = 0;
            IsStrOrNum = true;
            Color = pcolor;

            valStr = value;
            showValue = value;
            Curve = null;

            if (modes != null)
                Modes = modes;
            else
                Modes = new ObservableCollection<SelMode>() { SelMode.None };
        }

        public Property(string dicName, string d_str, string name, double value, string[] units, P_Type type, TPISNet.PColor pcolor, ObservableCollection<SelMode> modes = null, string tips = "")
        {
            IsLine = false;
            SetKnown(d_str);
            DicName = dicName;
            Name = name;
            Units = units;
            Type = type;
            Tips = tips;
            UnitNum = 0;
            IsStrOrNum = false;
            Color = pcolor;

            valNum = value;
            if (IsKnown)
            {
                showValue = ValueConvert(value, Units[0], Units[0]);
            }
            else
                showValue = "";
            Curve = null;

            if (modes != null)
                Modes = modes;
            else
                Modes = new ObservableCollection<SelMode>() { SelMode.None };
        }

        public Property(string dicName, string name, string[] units, string tips = "")
        {
            IsLine = true;
            IsKnown = false;
            DicName = dicName;
            Name = name;
            Units = units;
            Color = TPISNet.PColor.Whatever;
            Type = P_Type.ToLine;
            Tips = tips;
            Modes = new ObservableCollection<SelMode>() { SelMode.InterMode };
            Curve = new Curves(name, units[0], units[1]);
            Curve.Lx1x2.Add(new XYDataLine());
        }

        internal void SetKnown(string str)
        {
            IsKnown = false;
            if (str != "" && str != null)
                IsKnown = true;
        }

        //属性间的转化
        #region
        internal string ValueConvert(double value, string unit_src, string unit_desc)
        {
            if (unit_src != unit_desc)
                return UnitValueCal(value, unit_src, unit_desc).ToString();
            else return value.ToString();
        }

        internal double ValueConvertBack(string value, string unit_src, string unit_desc)
        {
            try
            {
                if (unit_src != unit_desc)
                    return UnitValueCal(double.Parse(value), unit_desc, unit_src);
                else return double.Parse(value);
            }
            catch
            {
                return 0;
            }
        }

        private double UnitValueCal(double value, string unit_src, string unit_desc)
        {
            return value;
            //switch (unit_src)
            //{
            //    //温度
            //    case "℃":
            //        {
            //            if (unit_desc == "K")
            //                return value * 1.8 + 32;
            //            else
            //                return value;
            //        }
            //    case "K":
            //        {
            //            if (unit_desc == "℃")
            //                return (value - 32) / 1.8;
            //            else
            //                return value;
            //        }

            //    //压力
            //    case "MPa":
            //        {
            //            if (unit_desc == "kPa")
            //                return 1000 * value;
            //            else if(unit_desc == "Pa")
            //                return 1000000 * value;
            //            else if(unit_desc == "Pa(g)")
            //                return value;
            //            else
            //                return value;
            //        }
            //    case "kPa":
            //        {

            //            if (unit_desc == "MPa")
            //                return 0.001 * value;
            //            else if (unit_desc == "Pa")
            //                return 1000 * value;
            //            else if(unit_desc == "Pa(g)")
            //                return value;
            //            else
            //                return value;
            //        }
            //    case "Pa":
            //        {
            //            if (unit_desc == "MPa")
            //                return 0.000001 * value;
            //            else if(unit_desc == "kPa")
            //                return 0.001 * value;
            //            else if(unit_desc == "Pa(g)")
            //                return value;
            //            else
            //                return value;
            //        }
            //    case "Pa(g)":
            //        {
            //            if (unit_desc == "MPa")
            //                return value;
            //            else if(unit_desc == "kPa")
            //                return value;
            //            else if(unit_desc == "Pa")
            //                return value;
            //            else
            //                return value;
            //        }

            //    //WaterQ Gas Q
            //    case "t/h":
            //        {
            //            if (unit_desc == "kg/s")
            //                return value;
            //            else
            //                return value;
            //        }
            //    case "kg/s":
            //        {
            //            if (unit_desc == "t/h")
            //                return value;
            //            else
            //                return value;
            //        }
            //    case "Nm3/h":
            //        {
            //            if (unit_desc == "t/h")
            //                return value;
            //            else if (unit_desc == "kg/s")
            //                return value;
            //            else
            //                return value;
            //        }

            //    //百分比
            //    case "%":
            //        {
            //            if (unit_desc == "1")
            //                return value/100;
            //            else
            //                return value;
            //        }
            //    case "1":
            //        {
            //            if (unit_desc == "%")
            //                return value*100;
            //            else
            //                return value;
            //        }
            //}
        }
        #endregion
    }
}

using System.Collections.ObjectModel;
using System.ComponentModel;
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

    public class Property : INotifyPropertyChanged
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

        public string DicName { get; set; }
        public string Name { get; set; }
        public string Tips { get; set; }
        public string[] Units { get; set; }

        public int unitNum;
        public int UnitNum
        {
            get => unitNum;
            set
            {
                unitNum = value;
                if (!IsStrOrNum)
                {
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
                showValue = value;
                if (IsStrOrNum)
                    valStr = (string)value;
                else
                    valNum = ValueConvertBack(value, Units[UnitNum], Units[0]);
                OnPropertyChanged("ShowValue");
            }
        }

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
        public void SelectProperty(SelMode selMode)
        {
            if (Modes.Contains(selMode))
                visible = true;
            else
                visible = false;
            OnPropertyChanged("Visibility");
        }
        #endregion

        public Property(string dicName, string name, string value, string[] units, P_Type type, ObservableCollection<SelMode> modes = null, string tips = "")
        {
            DicName = dicName;
            Name = name;
            Units = units;
            Type = type;
            Tips = tips;
            UnitNum = 0;
            IsStrOrNum = true;

            valStr = value;
            showValue = value;

            if (modes != null)
                Modes = modes;
            else
                Modes = new ObservableCollection<SelMode>() { SelMode.None };
        }

        public Property(string dicName, string name, double value, string[] units, P_Type type, ObservableCollection<SelMode> modes = null, string tips = "")
        {
            DicName = dicName;
            Name = name;
            Units = units;
            Type = type;
            Tips = tips;
            UnitNum = 0;
            IsStrOrNum = false;

            valNum = value;
            showValue = ValueConvert(value, Units[0], Units[0]);

            if (modes != null)
                Modes = modes;
            else
                Modes = new ObservableCollection<SelMode>() { SelMode.None };
        }

        //属性间的转化
        #region
        internal string ValueConvert(double value, string unit_src, string unit_desc)
        {
            if (unit_src != unit_desc)
                return (value + 1).ToString();
            else return value.ToString();
        }

        internal double ValueConvertBack(string value, string unit_src, string unit_desc)
        {
            if (unit_src != unit_desc)
                return double.Parse(value) - 1;
            else return double.Parse(value);
        }
        #endregion
    }
}
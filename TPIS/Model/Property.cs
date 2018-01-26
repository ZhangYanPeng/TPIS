
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

    public class Property
    {
        public string DicName { get; set; }
        public string Name { get; set; }
        public string ValStr { get; set; }
        public double Value { get; set; }
        public string Tips { get; set; }
        public string[] Units { get; set; }
        public int UnitNum { get; set; }
        public P_Type Type { get; set; }

        public Property(string dicName,  string name, string value, string[] units, P_Type type, string tips = "")
        {
            DicName = dicName;
            Name = name;
            ValStr = value;
            Units = units;
            Type = type;
            Tips = tips;
            UnitNum = 0;
        }

        public Property(string dicName, string name, double value, string[] units, P_Type type, string tips = "")
        {
            DicName = dicName;
            Name = name;
            Value = value;
            Units = units;
            Type = type;
            Tips = tips;
            UnitNum = 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model.Common
{
    public static class Units
    {
        public static string[] Kg_Nm3 = { "Kg/Nm3" };
        public static string[] Kg_m3 = { "Kg/m3" };
        public static string[] KJ_Nm3 = { "KJ/Nm3" };
        public static string[] KJ_KG = { "KJ/Kg" };
        public static string[] Percent = { "%" };
        public static string[] Nm3_Kg = { "Nm3/kg" };
        public static string[] NA = {""};
        public static string[] t_h = { "t/h" };
        public static string[] m3_h = { "m3/h" };
        public static string[] MPa = { "MPa" };
        public static string[] MPaa = { "MPa(a)" };//表示绝对压力
        public static string[] MPag = { "MPa(g)" };//表示表压
        public static string[] Pa = { "Pa" };
        public static string[] T = { "℃" };
        public static string[] g_Nm3 = { "g/Nm3" };
        public static string[] Nm3_Nm3 = { "Nm3/Nm3" };
        public static string[] Nm3_h = { "Nm3/h" };
        public static string[] Tab = { "标签" };
        public static string[] kW = { "kW" };
        public static string[] MW = { "MW" };
        public static string[] W_m2k = { "W/(m2·K)" };
        public static string[] W_mk = { "W/(m·K)" };
        public static string[] m3_kg = { "m3/kg" };
        public static string[] m_s = { "m/s" };
        public static string[] mm = { "mm" };
        public static string[] m = { "m" };
        public static string[] r_min = { "r/min" };
        public static string[] Vnumber = { "个" };
        public static string[] jiaodu = { "角度" };
        public static string[] jishu = { "级" };
        public static string[] m2 = { "m2" };
        public static string[] m2k_w = { "m2·K/W" };
        public static string[] T_m = { "℃/m" };//烟囱温降
        public static string[] m3 = { "m3" };
        public static string[] hour = { "h" };
        public static string[] GJ = { "GJ" };
        public static string[] kwh = { "kWh" };
        public static string[] mg_Nm3 = { "mg/Nm3" };
        public static string[] g_kwh = { "g/kwh" };
        public static string[] kJ_kwh = { "kJ/kwh" };

        //可转换单位
        public static string[] Temperate = { "℃", "K" };
        public static string[] Pressure = { "MPa", "kPa", "Pa", "Pa(g)" };
        public static string[] WaterQ = { "t/h", "kg/s" };
        public static string[] GasQ = { "t/h", "kg/s", "Nm3/h" };
        public static string[] Percents = { "1", "%" };

        public static List<string[]> ListAllUnits()
        {
            List<string[]> AllUnits = new List<string[]>();
            AllUnits.Add(Kg_Nm3);
            AllUnits.Add(Kg_m3);
            AllUnits.Add(KJ_Nm3);
            AllUnits.Add(KJ_KG);
            AllUnits.Add(Percent);
            AllUnits.Add(Nm3_Kg);
            AllUnits.Add(NA);
            AllUnits.Add(t_h);
            AllUnits.Add(m3_h);
            AllUnits.Add(MPa);
            AllUnits.Add(MPaa);
            AllUnits.Add(MPag);
            AllUnits.Add(Pa);
            AllUnits.Add(T);
            AllUnits.Add(g_Nm3);
            AllUnits.Add(Nm3_Nm3);
            AllUnits.Add(Nm3_h);
            AllUnits.Add(Tab);
            AllUnits.Add(kW);
            AllUnits.Add(MW);
            AllUnits.Add(W_m2k);
            AllUnits.Add(W_mk);
            AllUnits.Add(m3_kg);
            AllUnits.Add(m_s);
            AllUnits.Add(mm);
            AllUnits.Add(m);
            AllUnits.Add(r_min);
            AllUnits.Add(Vnumber);
            AllUnits.Add(jiaodu);
            AllUnits.Add(jishu);
            AllUnits.Add(m2);
            AllUnits.Add(m2k_w);
            AllUnits.Add(T_m);
            AllUnits.Add(m3);
            AllUnits.Add(hour);
            AllUnits.Add(GJ);
            AllUnits.Add(kwh);
            AllUnits.Add(mg_Nm3);
            AllUnits.Add(g_kwh);
            AllUnits.Add(kJ_kwh);
            return AllUnits;
        }

        public static List<string[]> ListAllUnitsEnum()
        {
            List<string[]> UnitsEnum = new List<string[]>();
            UnitsEnum.Add(Temperate);
            UnitsEnum.Add(Pressure);
            UnitsEnum.Add(WaterQ);
            UnitsEnum.Add(GasQ);
            UnitsEnum.Add(Percents);
            return UnitsEnum;
        }
    }

    public enum P_Type
    {
        ToLine,
        ToSelect,
        ToSetAsString,
        ToSetAsDouble,
        ToCal,
        NotNeed,
        Notshow
    }
}

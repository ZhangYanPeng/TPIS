using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPISNet;

namespace TPIS.Model.Common
{
    public static partial class CommonTypeService
    {
        //包含各个元件的属性的初始化

        public static ObservableCollection<PropertyGroup> InitComponentResult(EleType eleType)
        {
            return null;
        }

       

        private static ObservableCollection<PropertyGroup> InitTurbinResult()
        {
            ObservableCollection<PropertyGroup> ResultGroups = new ObservableCollection<PropertyGroup>();
            PropertyGroup noneGroup = new PropertyGroup() { Flag = "" };
            noneGroup.Properties.Add(new Property("EPower", "轴功率", 0, Units.kW, P_Type.ToCal, new ObservableCollection<SelMode>() { SelMode.CalMode, SelMode.DesignMode, SelMode.InterMode }));
            noneGroup.Properties.Add(new Property("Eff", "内效率", 0, Units.Percent, P_Type.ToCal, new ObservableCollection<SelMode>() { SelMode.CalMode, SelMode.DesignMode, SelMode.InterMode }));
            noneGroup.Properties.Add(new Property("Xa", "速比", 0, Units.NA, P_Type.ToCal, new ObservableCollection<SelMode>() { SelMode.CalMode }));
            noneGroup.Properties.Add(new Property("RL", "叶高", 0, Units.m, P_Type.ToCal, new ObservableCollection<SelMode>() { SelMode.CalMode }));
            ResultGroups.Add(noneGroup);

            PropertyGroup assistGroup = new PropertyGroup() { Flag = "辅助设计" };
            assistGroup.Properties.Add(new Property("Mu", "修正系数", 0, Units.NA, P_Type.ToCal, new ObservableCollection<SelMode>() { SelMode.CalMode }));
            assistGroup.Properties.Add(new Property("QV", "平均容积流量", 0, new string[] { "m3/h" }, P_Type.ToCal, new ObservableCollection<SelMode>() { SelMode.DesignMode, SelMode.InterMode }));
            ResultGroups.Add(noneGroup);

            return ResultGroups;
        }
        

        private static ObservableCollection<PropertyGroup> InitCFBResult()
        {
            ObservableCollection<PropertyGroup> ResultGroups = new ObservableCollection<PropertyGroup>();
            PropertyGroup assistGroup = new PropertyGroup() { Flag = "辅助设计" };
            assistGroup.Properties.Add(new Property("ExPower", "锅炉热负荷", 0, Units.kW, P_Type.ToCal));
            ResultGroups.Add(assistGroup);

            return ResultGroups;
        }
    }
}

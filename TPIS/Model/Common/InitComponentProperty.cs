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

        public static ObservableCollection<PropertyGroup> InitComponentProperty(EleType eleType)
        {
            Element element = CommonTypeService.LoadElement(eleType);
            ObservableCollection<PropertyGroup> PropertyGroups = new ObservableCollection<PropertyGroup>();
            foreach (string key in element.DProperty.Keys)
            {
                TPISNet.Property property = element.DProperty[key];

                ObservableCollection<SelMode> SelModes = new ObservableCollection<SelMode>();
                foreach(CalMode cm in element.LCalMode)
                {
                    if (cm.LDProperty.Contains(property))
                    {
                        SelModes.Add(TransSelMode(cm.Mode));
                    }
                }
                if(SelModes.Count == 0)
                {
                    SelModes = null;
                }

                bool check = false;
                foreach (PropertyGroup pg in PropertyGroups)
                {
                    if (pg.Flag == property.GroupFlag)
                    {
                        check = true;
                        Property p = InitProperty(key, property, SelModes);
                        pg.Properties.Add(p);
                        break;
                    }
                }
                if (!check)
                {
                    Property p = InitProperty(key, property, SelModes);
                    PropertyGroup baseGroup = new PropertyGroup() { Flag = property.GroupFlag };
                    baseGroup.Properties.Add(p);
                    PropertyGroups.Add(baseGroup);
                }
            }
            return PropertyGroups;
        }

        public static Property InitProperty(string key, TPISNet.Property property, ObservableCollection<SelMode> sm)
        {
            bool units;
            if (property.SelectList.Count > 0)
                units = true;
            else
                units = false;

            if (property.type == TPISNet.P_Type.ToSetAsDouble)
            {
                Property p = new Property(key, property.Name, property.Data, TransformUnit(property.Unit, units), P_Type.ToSetAsString, sm, property.Tips);
                return p;
            }
            else if (property.type == TPISNet.P_Type.ToSetAsString)
            {
                Property p = new Property(key, property.Name, property.data_string, TransformUnit(property.Unit, units), P_Type.ToSetAsString, sm, property.Tips);
                return p;
            }
            else
            {
                Property p = new Property(key, property.Name, property.data_string, TransformUnit(property.Unit, units), P_Type.ToSelect, sm, property.Tips);
                return p;
            }
        }

        internal static SelMode TransSelMode(TPISNet.SelMode sm)
        {
            switch (sm)
            {
                case TPISNet.SelMode.插值模式:  return SelMode.InterMode;
                case TPISNet.SelMode.计算模式: return SelMode.CalMode;
                case TPISNet.SelMode.设计模式: return SelMode.DesignMode;
            }
            return SelMode.None;
        }

        internal static string[] TransformUnit(string u, bool several)
        {
            if (several)
            {
                foreach (string[] units in Units.ListAllUnitsEnum())
                {
                    if (units.Contains(u))
                        return units;
                }
            }
            else { 
                foreach (string[] units in Units.ListAllUnits())
                {
                    if (units.Contains(u))
                        return units;
                }
            }
            return Units.NA;
        }

        private static ObservableCollection<PropertyGroup> InitTurbinProperty()
        {
            ObservableCollection<PropertyGroup> PropertyGroups = new ObservableCollection<PropertyGroup>();
            PropertyGroup baseGroup = new PropertyGroup() { Flag = "基本" };
            baseGroup.Properties.Add(new Property("Name", "元件名称", "汽轮机", Units.NA, P_Type.ToSetAsString, new ObservableCollection<SelMode>() { SelMode.DesignMode, SelMode.CalMode, SelMode.InterMode }));
            baseGroup.Properties.Add(new Property("OutsteamP", "排汽压力", 0, Units.MPaa, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.DesignMode }));
            baseGroup.Properties.Add(new Property("OutsteamH", "出口焓值", 0, Units.KJ_KG, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.DesignMode }, "至少要精确到小数点后一位"));
            baseGroup.Properties.Add(new Property("OutsteamT", "出口温度", 0, Units.Temperate, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.DesignMode }, "有焓值输入则此输入值无效\r\n过热度需大于15℃\r\n最好精确到小数点后三位"));
            PropertyGroups.Add(baseGroup);

            PropertyGroup designGroup = new PropertyGroup() { Flag = "汽轮机设计" };
            designGroup.Properties.Add(new Property("QVselect", "效率插值选择", 0, new string[] { "质量流量效率", "体积流量效率", "压比效率", "固定效率" }, P_Type.ToSelect, new ObservableCollection<SelMode>() { SelMode.InterMode }, "默认为质量流量效率\r\n体积流量为级组进出口体积流量的平均值"));
            designGroup.Properties.Add(new Property("QPselect", "压力插值选择", 0, new string[] { "流量压力", "流量压比" }, P_Type.ToSelect, new ObservableCollection<SelMode>() { SelMode.InterMode }, "默认为质量流量进汽压力\r\n流量都为质量流量"));
            designGroup.Properties.Add(new Property("PN", "内部级数", 0, Units.jishu, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.CalMode }));
            designGroup.Properties.Add(new Property("ARFA", "重热系数", 0, Units.NA, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.CalMode }, "一般为1.02 - 1.08"));
            designGroup.Properties.Add(new Property("PE", "部分进汽度", 0, Units.NA, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.CalMode }));
            designGroup.Properties.Add(new Property("PitchD", "节圆直径", 0, Units.m, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.CalMode }));
            designGroup.Properties.Add(new Property("OM0", "反动度", 0.05, Units.NA, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.CalMode }, "默认值为0.05"));
            designGroup.Properties.Add(new Property("RP", "转速", 3000, Units.NA, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.CalMode }, "默认值为3000"));
            designGroup.Properties.Add(new Property("PHI", "喷嘴速度系数", 0.97, Units.NA, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.CalMode }, "默认值为0.97"));
            designGroup.Properties.Add(new Property("PSI", "动叶速度系数", 0.93, Units.NA, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.CalMode }, "默认值为0.93"));
            designGroup.Properties.Add(new Property("ALPHA1", "静叶出口汽流角", 15, Units.jiaodu, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.CalMode }, "默认值为15"));
            designGroup.Properties.Add(new Property("BETA2", "动叶出口汽流角", 18, Units.jiaodu, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.CalMode }, "默认值为18"));
            PropertyGroups.Add(designGroup);

            PropertyGroup assistGroup = new PropertyGroup() { Flag = "辅助" };
            assistGroup.Properties.Add(new Property("Eff", "内效率", 0, Units.Percent, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.DesignMode, SelMode.InterMode }));
            PropertyGroups.Add(assistGroup);

            //intemode 未添加
            //InterM.LDProperty.AddRange(new List<Property> { Name, QVselect, PEff, QPselect });
            //XYDataLine qveff = new XYDataLine();
            //XYDataLine qeff = new XYDataLine();
            //XYDataLine paieff = new XYDataLine();
            //XYDataLine Inpl = new XYDataLine();
            //XYDataLine qpai = new XYDataLine();
            //InterM.Add("qvEff", "体积流量-效率", "m3/h", Units.Percent, qveff, DLines);
            //InterM.Add("Eff", "质量流量-效率", Units.t_h, Units.Percent, qeff, DLines);
            //InterM.Add("paiEff", "压比-效率", "出/进", Units.Percent, paieff, DLines);//压比写上谁比谁
            //InterM.Add("InP", "质量流量-进汽压力", Units.t_h, Units.MPa, Inpl, DLines);
            //InterM.Add("qpai", "体积流量-压比", "m3/h", "出/进", qpai, DLines);
            return PropertyGroups;
        }

       

        private static ObservableCollection<PropertyGroup> InitCFBProperty()
        {
            ObservableCollection<PropertyGroup> PropertyGroups = new ObservableCollection<PropertyGroup>();
            PropertyGroup baseGroup = new PropertyGroup() { Flag = "基本" };
            baseGroup.Properties.Add(new Property("Name", "元件名称", "循环流化床", Units.NA, P_Type.ToSetAsString));
            baseGroup.Properties.Add(new Property("NCR", "额定蒸发量", 0, Units.WaterQ, P_Type.ToSetAsDouble));
            baseGroup.Properties.Add(new Property("MainP", "主蒸汽压力", 0, Units.Pressure, P_Type.ToSetAsDouble));
            baseGroup.Properties.Add(new Property("MainT", "主蒸汽温度", 0, Units.Temperate, P_Type.ToSetAsDouble));
            baseGroup.Properties.Add(new Property("DrumP", "汽包压力", 0, Units.Pressure, P_Type.ToSetAsDouble));
            baseGroup.Properties.Add(new Property("AAir_APH", "过量空气系数", 0, Units.NA, P_Type.ToSetAsDouble, null, "炉膛出口"));
            PropertyGroups.Add(baseGroup);

            PropertyGroup otherGroup = new PropertyGroup() { Flag = "其它" };
            otherGroup.Properties.Add(new Property("Eff", "锅炉效率", 0, Units.Percent, P_Type.ToSetAsDouble));
            otherGroup.Properties.Add(new Property("FluegasT", "排烟温度", 0, Units.Temperate, P_Type.ToSetAsDouble));
            otherGroup.Properties.Add(new Property("BAshT", "排渣温度", 0, Units.Temperate, P_Type.ToSetAsDouble));
            otherGroup.Properties.Add(new Property("FluegasP", "烟气压力", 0, Units.Pa, P_Type.ToSetAsDouble));
            otherGroup.Properties.Add(new Property("BlowdownRatio", "排污比例", 0, Units.Percent, P_Type.ToSetAsDouble));
            otherGroup.Properties.Add(new Property("FlyashP", "飞灰份额", 0, Units.Percent, P_Type.ToSetAsDouble));
            otherGroup.Properties.Add(new Property("q4", "机械损失q4", 0, Units.Percent, P_Type.ToSetAsDouble));
            otherGroup.Properties.Add(new Property("PA_FA", "一次风比例", 0, Units.Percent, P_Type.ToSetAsDouble, null, "一次风(含播煤热风)占总燃烧风的比例"));
            PropertyGroups.Add(otherGroup);

            PropertyGroup unSulfurGroup = new PropertyGroup() { Flag = "石灰石脱硫" };
            unSulfurGroup.Properties.Add(new Property("CaS", "钙硫摩尔比", 0, Units.NA, P_Type.ToSetAsDouble));
            unSulfurGroup.Properties.Add(new Property("CaCO3_p", "石灰石纯度", 0, Units.Percent, P_Type.ToSetAsDouble));
            unSulfurGroup.Properties.Add(new Property("Eff_S", "CaCO3炉内脱硫效率", 0, Units.Percent, P_Type.ToSetAsDouble));
            PropertyGroups.Add(unSulfurGroup);

            return PropertyGroups;
        }
    }
}

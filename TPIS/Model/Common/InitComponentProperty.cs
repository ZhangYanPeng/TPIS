using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model.Common
{
    public static partial class CommonTypeService
    {
        //包含各个元件的属性的初始化

        public static ObservableCollection<PropertyGroup> InitComponentProperty(EleType eleType)
        {
            if (eleType == EleType.CFB)
                return InitCFBProperty();
            else if (eleType == EleType.SuperHeater)
                return InitSuperHeaterProperty();
            else if (eleType == EleType.EVAP)
                return InitEVAPProperty();
            else if (eleType == EleType.EVAP2)
                return InitEVAP2Property();
            else if (eleType == EleType.ECON)
                return InitECONProperty();
            else if (eleType == EleType.WaterPool)
                return InitWaterPoolProperty();
            else if (eleType == EleType.WaterSource)
                return InitWaterSourceProperty();
            else if (eleType == EleType.CoalSource)
                return InitCoalSourceProperty();
            else if (eleType == EleType.GasSoource)
                return InitGasSourceProperty();
            else if (eleType == EleType.TeeGas)
                return InitTeeGasProperty();
            else if (eleType == EleType.Compressor)
                return InitCompressorProperty();
            else if (eleType == EleType.Pump)
                return InitPumpProperty();
            else if (eleType == EleType.SteamHeader)
                return InitSteamHeaderProperty();
            else if (eleType == EleType.TeeWater)
                return InitTeeWaterProperty();
            else if (eleType == EleType.PipeEle)
                return InitPipeEleProperty();
            else if (eleType == EleType.GasTurbin)
                return InitGasTurbinProperty();
            else if (eleType == EleType.GasMidTurbin)
                return InitGasMidTurbinProperty();
            else if (eleType == EleType.GasLastTurbin)
                return InitGasLastTurbinProperty();
            else if (eleType == EleType.Controllingstage)
                return InitControllingstageProperty();
            else if (eleType == EleType.Turbin)
                return InitTurbinProperty();
            else if (eleType == EleType.SmallTurbin)
                return InitSmallTurbinProperty();
            else if (eleType == EleType.Laststage)
                return InitLaststageProperty();
            else if (eleType == EleType.Condenser)
                return InitCondenserProperty();
            else if (eleType == EleType.Airisland)
                return InitAirislandProperty();
            else if (eleType == EleType.Deaerator)
                return InitDeaeratorProperty();
            else if (eleType == EleType.WaterHeater)
                return InitWaterHeaterProperty();
            else if (eleType == EleType.GasHeater)
                return InitGasHeaterProperty();
            else if (eleType == EleType.PumpSteam)
                return InitPumpSteamProperty();
            else if (eleType == EleType.PTReducer)
                return InitPTReducerProperty();
            else if (eleType == EleType.Boiler)//
                return InitBoilerProperty();
            else if (eleType == EleType.Calorifier)
                return InitCalorifierProperty();
            else if (eleType == EleType.GasBoiler)
                return InitGasBoilerProperty();
            else if (eleType == EleType.Fan)
                return InitFanProperty();
            else if (eleType == EleType.FanSteam)
                return InitFanSteamProperty();
            else if (eleType == EleType.Chimney)
                return InitChimneyProperty();
            else if (eleType == EleType.ConTank)
                return InitConTankProperty();
            else if (eleType == EleType.Ejector)
                return InitEjectorProperty();
            else if (eleType == EleType.Generator)
                return InitGeneratorProperty();
            else if (eleType == EleType.Motor)
                return InitMotorProperty();
            else if (eleType == EleType.TeePower)
                return InitTeePowerProperty();
            else if (eleType == EleType.GasBurner)
                return InitGasBurnerProperty();
            else if (eleType == EleType.MixedHeatExchanger)
                return InitMixedHeatExchangerProperty();
            else if (eleType == EleType.SurfaceHeatExchanger)
                return InitSurfaceHeatExchangerProperty();
            else if (eleType == EleType.ControlDot)
                return InitControlDotProperty();
            else if (eleType == EleType.TeeCoal)
                return InitTeeCoalProperty();
            else if (eleType == EleType.GasHeatExchanger)
                return InitGasHeatExchangerProperty();
            else if (eleType == EleType.ControlDotGas)
                return InitControlDotGasProperty();
            else if (eleType == EleType.IterateDot)
                return InitIterateDotProperty();
            else if (eleType == EleType.IterateDotGas)
                return InitIterateDotGasProperty();
            else if (eleType == EleType.SlagCooler)
                return InitSlagCoolerProperty();
            else if (eleType == EleType.BagFilter)
                return InitBagFilterProperty();
            else if (eleType == EleType.Thionizer)
                return InitThionizerProperty();
            else if (eleType == EleType.WaterTag)
                return InitWaterTagProperty();
            else if (eleType == EleType.WaterValve)
                return InitWaterValveProperty();
            else if (eleType == EleType.TeeValve)
                return InitTeeValveProperty();
            else if (eleType == EleType.ControlValve)
                return InitControlValveProperty();
            else if (eleType == EleType.Throttle)
                return InitThrottleProperty();
            else if (eleType == EleType.HeatSupply)
                return InitHeatSupplyProperty();
            return null;
        }

        private static ObservableCollection<PropertyGroup> InitHeatSupplyProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitThrottleProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitControlValveProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitTeeValveProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitWaterValveProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitWaterTagProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitThionizerProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitBagFilterProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitSlagCoolerProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitIterateDotGasProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitIterateDotProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitControlDotGasProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasHeatExchangerProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitTeeCoalProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitControlDotProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitSurfaceHeatExchangerProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitMixedHeatExchangerProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasBurnerProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitTeePowerProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitMotorProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGeneratorProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitEjectorProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitConTankProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitChimneyProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitFanSteamProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitFanProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasBoilerProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitCalorifierProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitBoilerProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitPTReducerProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitPumpSteamProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasHeaterProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitWaterHeaterProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitDeaeratorProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitAirislandProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitCondenserProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitLaststageProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitSmallTurbinProperty()
        {
            throw new NotImplementedException();
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
            designGroup.Properties.Add(new Property("QVselect", "效率插值选择", 0, new string[] { "质量流量效率", "体积流量效率", "压比效率", "固定效率" }, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.InterMode }, "默认为质量流量效率\r\n体积流量为级组进出口体积流量的平均值"));
            designGroup.Properties.Add(new Property("QPselect", "压力插值选择", 0, new string[] { "流量压力", "流量压比" }, P_Type.ToSetAsDouble, new ObservableCollection<SelMode>() { SelMode.InterMode }, "默认为质量流量进汽压力\r\n流量都为质量流量"));
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

        private static ObservableCollection<PropertyGroup> InitControllingstageProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasLastTurbinProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasMidTurbinProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasTurbinProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitPipeEleProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitTeeWaterProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitSteamHeaderProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitPumpProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitCompressorProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitTeeGasProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasSourceProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitCoalSourceProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitWaterSourceProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitWaterPoolProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitECONProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitEVAP2Property()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitEVAPProperty()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitSuperHeaterProperty()
        {
            throw new NotImplementedException();
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

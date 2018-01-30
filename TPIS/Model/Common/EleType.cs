using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model.Common
{
    public enum EleType
    {
        CFB,
        ECON,
        EVAP,
        EVAP2,
        Boiler,
        GasBoiler,
        SuperHeater,
        WaterSource,
        WaterPool,
        CoalSource,
        GasSoource,
        AshPool,
        Pipe,
        TeeGas,
        TeeFuel,
        TeeWater,
        TeeCoal,
        Compressor,
        Pump,
        GasTurbin,
        Controllingstage,
        Turbin,
        Laststage,
        Condenser,
        Deaerator,
        WaterHeater,
        PumpSteam,
        PTReducer,
        Fan,
        Chimney,
        ConTank,
        Generator,
        GasBurner,
        SurfaceHeatExchanger,
        ControlDot,
        GasHeatExchanger,
        ControlDotGas,
        SlagCooler,
        BagFilter,
        WaterTag,
        Throttle,
        GasMidTurbin,
        GasLastTurbin,
        MixedHeatExchanger,
        SteamHeader,
        WaterValve,
        TeeValve,
        SmallTurbin,
        FanSteam,
        Ejector,
        Thionizer,
        HeatSupply,
        Motor,
        TeePower,
        IterateDot,
        PipeEle,
        ControlValve,
        Calorifier,
        Airisland,
        GasHeater,
        IterateDotGas,
        None
    }

    public enum SelMode
    {
        None,
        DesignMode,
        InterMode,
        CalMode
    }

    public static partial class CommonTypeService
    {

        /// <summary>
        /// 根据类型返回图片路径与元件名称
        /// </summary>
        /// <param name="eleType"></param>
        public static string[] GetComponentType(EleType eleType)
        {
            if (eleType == EleType.CFB)
                return new string[] { "元件簇1", "/PNG/CFB.png", "循环流化床" };
            else if (eleType == EleType.SuperHeater)
                return new string[] { "元件簇1", "/PNG/SuperHeater.png", "过热器" };
            else if (eleType == EleType.EVAP)
                return new string[] { "元件簇1", "/PNG/EVAP.png", "蒸发器" };
            else if (eleType == EleType.EVAP2)
                return new string[] { "元件簇1", "/PNG/EVAp2.png", "蒸发器" };
            else if (eleType == EleType.ECON)
                return new string[] { "元件簇1", "/PNG/ECON.png", "省煤器" };
            else if (eleType == EleType.WaterPool)
                return new string[] { "元件簇1", "/PNG/WaterPool.png", "水/蒸汽池" };
            else if (eleType == EleType.WaterSource)
                return new string[] { "元件簇1", "/PNG/WaterSource.png", "水/蒸汽源" };
            else if (eleType == EleType.CoalSource)
                return new string[] { "元件簇1", "/PNG/CoalSource.png", "煤/燃料" };
            else if (eleType == EleType.GasSoource)
                return new string[] { "元件簇1", "/PNG/GasSource.png", "气体源" };
            else if (eleType == EleType.TeeGas)
                return new string[] { "元件簇1", "/PNG/TeeGas.png", "气体三通" };
            else if (eleType == EleType.Compressor)
                return new string[] { "元件簇1", "/PNG/Compressor.png", "压气机" };
            else if (eleType == EleType.Pump)
                return new string[] { "元件簇1", "/PNG/Pump.png", "水泵" };
            else if (eleType == EleType.SteamHeader)
                return new string[] { "元件簇1", "/PNG/SteamHeader.png", "蒸汽联箱" };
            else if (eleType == EleType.TeeWater)
                return new string[] { "元件簇1", "/PNG/TeeWater.png", "汽水三通" };
            else if (eleType == EleType.PipeEle)
                return new string[] { "元件簇1", "/PNG/PipeEle.png", "管道元件" };
            else if (eleType == EleType.GasTurbin)
                return new string[] { "元件簇1", "/PNG/GasTurbin.png", "燃气轮机" };
            else if (eleType == EleType.GasMidTurbin)
                return new string[] { "元件簇1", "/PNG/GasTurbin1.png", "燃气轮机" };
            else if (eleType == EleType.GasLastTurbin)
                return new string[] { "元件簇1", "/PNG/GasLastTurbin.png", "燃气轮机" };
            else if (eleType == EleType.Controllingstage)
                return new string[] { "元件簇1", "/PNG/Controllingstage.png", "调节级" };
            else if (eleType == EleType.Turbin)
                return new string[] { "元件簇1", "/PNG/Turbin1.png", "汽轮机" };
            else if (eleType == EleType.SmallTurbin)
                return new string[] { "元件簇1", "/PNG/SmallTurbin.png", "小汽轮机" };
            else if (eleType == EleType.Laststage)
                return new string[] { "元件簇1", "/PNG/Laststage1.png", "末级" };
            else if (eleType == EleType.Condenser)
                return new string[] { "元件簇1", "/PNG/Condenser.png", "凝汽器" };
            else if (eleType == EleType.Airisland)
                return new string[] { "元件簇1", "/PNG/Airisland.png", "空冷岛" };
            else if (eleType == EleType.Deaerator)
                return new string[] { "元件簇1", "/PNG/Deaerator.png", "热力除氧器" };
            else if (eleType == EleType.WaterHeater)
                return new string[] { "元件簇1", "/PNG/WaterHeater.png", "加热器" };
            else if (eleType == EleType.GasHeater)
                return new string[] { "元件簇1", "/PNG/GasHeater.png", "加热器" };
            else if (eleType == EleType.PumpSteam)
                return new string[] { "元件簇1", "/PNG/Pump.png", "泵" };
            else if (eleType == EleType.PTReducer)
                return new string[] { "元件簇1", "/PNG/PTReducer.png", "减温减压器" };
            else if (eleType == EleType.Boiler)
                return new string[] { "元件簇1", "/PNG/Boiler.png", "锅炉" };
            else if (eleType == EleType.Calorifier)
                return new string[] { "元件簇1", "/PNG/Calorifier.png", "蒸汽发生器" };
            else if (eleType == EleType.GasBoiler)
                return new string[] { "元件簇1", "/PNG/Boiler.png", "燃气锅炉" };
            else if (eleType == EleType.Fan)
                return new string[] { "元件簇1", "/PNG/Fan.png", "风机" };
            else if (eleType == EleType.FanSteam)
                return new string[] { "元件簇1", "/PNG/Fan.png", "风机" };
            else if (eleType == EleType.Chimney)
                return new string[] { "元件簇1", "/PNG/Chimney.png", "烟囱1" };
            else if (eleType == EleType.ConTank)
                return new string[] { "元件簇1", "/PNG/ConTank.png", "排污扩容器" };
            else if (eleType == EleType.Ejector)
                return new string[] { "元件簇1", "/PNG/Ejector.png", "引射器" };
            else if (eleType == EleType.Generator)
                return new string[] { "元件簇1", "/PNG/Generator.png", "发电机" };
            else if (eleType == EleType.Motor)
                return new string[] { "元件簇1", "/PNG/Motor.png", "电动机" };
            else if (eleType == EleType.TeePower)
                return new string[] { "元件簇1", "/PNG/TeePower.png", "机械三通" };
            else if (eleType == EleType.GasBurner)
                return new string[] { "元件簇1", "/PNG/GasBurner.png", "燃气燃烧器" };
            else if (eleType == EleType.MixedHeatExchanger)
                return new string[] { "元件簇1", "/PNG/MixedHeatExchanger.png", "混合式换热器" };
            else if (eleType == EleType.SurfaceHeatExchanger)
                return new string[] { "元件簇1", "/PNG/SurfaceHeatExchanger.png", "表面式换热器" };
            else if (eleType == EleType.ControlDot)
                return new string[] { "元件簇1", "/PNG/ControlDot2.png", "参数控制点" };
            else if (eleType == EleType.TeeCoal)
                return new string[] { "元件簇1", "/PNG/TeeGas.png", "燃料三通" };
            else if (eleType == EleType.GasHeatExchanger)
                return new string[] { "元件簇1", "/PNG/GasHeatExchanger.png", "空预器" };
            else if (eleType == EleType.ControlDotGas)
                return new string[] { "元件簇1", "/PNG/ControlDotGas.png", "参数控制点" };
            else if (eleType == EleType.IterateDot)
                return new string[] { "元件簇1", "/PNG/IterateDot.png", "参数迭代点" };
            else if (eleType == EleType.IterateDotGas)
                return new string[] { "元件簇1", "/PNG/IterateDot.png", "参数迭代点" };
            else if (eleType == EleType.SlagCooler)
                return new string[] { "元件簇1", "/PNG/SlagCooler.png", "冷渣器" };
            else if (eleType == EleType.BagFilter)
                return new string[] { "元件簇1", "/PNG/BagFilter.png", "布袋除尘器" };
            else if (eleType == EleType.Thionizer)
                return new string[] { "元件簇1", "/PNG/Thionizer.png", "脱硫塔" };
            else if (eleType == EleType.WaterTag)
                return new string[] { "元件簇1", "/PNG/WaterTag.png", "A" };
            else if (eleType == EleType.WaterValve)
                return new string[] { "元件簇1", "/PNG/WaterValve.png", "节流阀" };
            else if (eleType == EleType.TeeValve)
                return new string[] { "元件簇1", "/PNG/TeeValve.png", "三通阀" };
            else if (eleType == EleType.ControlValve)
                return new string[] { "元件簇1", "/PNG/ControlValve.png", "控制阀" };
            else if (eleType == EleType.Throttle)
                return new string[] { "元件簇2", "/PNG/Throttle2.png", "节流孔板" };
            else if (eleType == EleType.HeatSupply)
                return new string[] { "元件簇2", "/PNG/HeatSupply.png", "供热系统" };
            return null;
        }
    }
}

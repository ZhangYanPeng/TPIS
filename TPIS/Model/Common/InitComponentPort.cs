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

        public static ObservableCollection<Port> InitComponentPort(EleType eleType)
        {
            if (eleType == EleType.CFB)
                return InitCFBPort();
            else if (eleType == EleType.SuperHeater)
                return InitSuperHeaterPort();
            else if (eleType == EleType.EVAP)
                return InitEVAPPort();
            else if (eleType == EleType.EVAP2)
                return InitEVAP2Port();
            else if (eleType == EleType.ECON)
                return InitECONPort();
            else if (eleType == EleType.WaterPool)
                return InitWaterPoolPort();
            else if (eleType == EleType.WaterSource)
                return InitWaterSourcePort();
            else if (eleType == EleType.CoalSource)
                return InitCoalSourcePort();
            else if (eleType == EleType.GasSoource)
                return InitGasSourcePort();
            else if (eleType == EleType.TeeGas)
                return InitTeeGasPort();
            else if (eleType == EleType.Compressor)
                return InitCompressorPort();
            else if (eleType == EleType.Pump)
                return InitPumpPort();
            else if (eleType == EleType.SteamHeader)
                return InitSteamHeaderPort();
            else if (eleType == EleType.TeeWater)
                return InitTeeWaterPort();
            else if (eleType == EleType.PipeEle)
                return InitPipeElePort();
            else if (eleType == EleType.GasTurbin)
                return InitGasTurbinPort();
            else if (eleType == EleType.GasMidTurbin)
                return InitGasMidTurbinPort();
            else if (eleType == EleType.GasLastTurbin)
                return InitGasLastTurbinPort();
            else if (eleType == EleType.Controllingstage)
                return InitControllingstagePort();
            else if (eleType == EleType.Turbin)
                return InitTurbinPort();
            else if (eleType == EleType.SmallTurbin)
                return InitSmallTurbinPort();
            else if (eleType == EleType.Laststage)
                return InitLaststagePort();
            else if (eleType == EleType.Condenser)
                return InitCondenserPort();
            else if (eleType == EleType.Airisland)
                return InitAirislandPort();
            else if (eleType == EleType.Deaerator)
                return InitDeaeratorPort();
            else if (eleType == EleType.WaterHeater)
                return InitWaterHeaterPort();
            else if (eleType == EleType.GasHeater)
                return InitGasHeaterPort();
            else if (eleType == EleType.PumpSteam)
                return InitPumpSteamPort();
            else if (eleType == EleType.PTReducer)
                return InitPTReducerPort();
            else if (eleType == EleType.Boiler)//
                return InitBoilerPort();
            else if (eleType == EleType.Calorifier)
                return InitCalorifierPort();
            else if (eleType == EleType.GasBoiler)
                return InitGasBoilerPort();
            else if (eleType == EleType.Fan)
                return InitFanPort();
            else if (eleType == EleType.FanSteam)
                return InitFanSteamPort();
            else if (eleType == EleType.Chimney)
                return InitChimneyPort();
            else if (eleType == EleType.ConTank)
                return InitConTankPort();
            else if (eleType == EleType.Ejector)
                return InitEjectorPort();
            else if (eleType == EleType.Generator)
                return InitGeneratorPort();
            else if (eleType == EleType.Motor)
                return InitMotorPort();
            else if (eleType == EleType.TeePower)
                return InitTeePowerPort();
            else if (eleType == EleType.GasBurner)
                return InitGasBurnerPort();
            else if (eleType == EleType.MixedHeatExchanger)
                return InitMixedHeatExchangerPort();
            else if (eleType == EleType.SurfaceHeatExchanger)
                return InitSurfaceHeatExchangerPort();
            else if (eleType == EleType.ControlDot)
                return InitControlDotPort();
            else if (eleType == EleType.TeeCoal)
                return InitTeeCoalPort();
            else if (eleType == EleType.GasHeatExchanger)
                return InitGasHeatExchangerPort();
            else if (eleType == EleType.ControlDotGas)
                return InitControlDotGasPort();
            else if (eleType == EleType.IterateDot)
                return InitIterateDotPort();
            else if (eleType == EleType.IterateDotGas)
                return InitIterateDotGasPort();
            else if (eleType == EleType.SlagCooler)
                return InitSlagCoolerPort();
            else if (eleType == EleType.BagFilter)
                return InitBagFilterPort();
            else if (eleType == EleType.Thionizer)
                return InitThionizerPort();
            else if (eleType == EleType.WaterTag)
                return InitWaterTagPort();
            else if (eleType == EleType.WaterValve)
                return InitWaterValvePort();
            else if (eleType == EleType.TeeValve)
                return InitTeeValvePort();
            else if (eleType == EleType.ControlValve)
                return InitControlValvePort();
            else if (eleType == EleType.Throttle)
                return InitThrottlePort();
            else if (eleType == EleType.HeatSupply)
                return InitHeatSupplyPort();
            return null;
        }

        private static ObservableCollection<Port> InitHeatSupplyPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitThrottlePort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitControlValvePort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitTeeValvePort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitWaterValvePort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitWaterTagPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitThionizerPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitBagFilterPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitSlagCoolerPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitIterateDotGasPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitIterateDotPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitControlDotGasPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitGasHeatExchangerPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitTeeCoalPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitControlDotPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitSurfaceHeatExchangerPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitMixedHeatExchangerPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitGasBurnerPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitTeePowerPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitMotorPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitGeneratorPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitEjectorPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitConTankPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitChimneyPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitFanSteamPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitFanPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitGasBoilerPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitCalorifierPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitBoilerPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitPTReducerPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitPumpSteamPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitGasHeaterPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitWaterHeaterPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitDeaeratorPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitAirislandPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitCondenserPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitLaststagePort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitSmallTurbinPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitTurbinPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitControllingstagePort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitGasLastTurbinPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitGasMidTurbinPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitGasTurbinPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitPipeElePort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitTeeWaterPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitSteamHeaderPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitPumpPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitCompressorPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitTeeGasPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitGasSourcePort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitCoalSourcePort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitWaterSourcePort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitWaterPoolPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitECONPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitEVAP2Port()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitEVAPPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitSuperHeaterPort()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<Port> InitCFBPort()
        {
            ObservableCollection<Port> ports = new ObservableCollection<Port>();
            ports.Add(new Port("coal", "燃料", 0, 0.6, Material.coal, NodType.Inlet, false));
            ports.Add(new Port("PAir", "一次风", 1, 0.7, Material.gas, NodType.Inlet, false));
            ports.Add(new Port("FAir", "二次风", 1, 0.6, Material.gas, NodType.Inlet, false));
            ports.Add(new Port("BAir", "回料风", 0.5, 1, Material.gas, NodType.Inlet, false));
            ports.Add(new Port("BottomAsh", "渣", 0.2, 1, Material.ash, NodType.Outlet, true));
            ports.Add(new Port("FlueGas", "烟气", 1, 0.9, Material.gas, NodType.Outlet, false));
            ports.Add(new Port("FeedWater", "给水", 1, 0.4, Material.water, NodType.Inlet, false));
            ports.Add(new Port("MainSteam", "主蒸汽", 1, 0, Material.water, NodType.Outlet, false));
            ports.Add(new Port("DrainWater", "排污", 0.4, 1, Material.water, NodType.Outlet, true));
            return ports;
        }
    }
}

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

        public static ObservableCollection<PropertyGroup> InitComponentResult(EleType eleType)
        {
            if (eleType == EleType.CFB)
                return InitCFBResult();
            else if (eleType == EleType.SuperHeater)
                return InitSuperHeaterResult();
            else if (eleType == EleType.EVAP)
                return InitEVAPResult();
            else if (eleType == EleType.EVAP2)
                return InitEVAP2Result();
            else if (eleType == EleType.ECON)
                return InitECONResult();
            else if (eleType == EleType.WaterPool)
                return InitWaterPoolResult();
            else if (eleType == EleType.WaterSource)
                return InitWaterSourceResult();
            else if (eleType == EleType.CoalSource)
                return InitCoalSourceResult();
            else if (eleType == EleType.GasSoource)
                return InitGasSourceResult();
            else if (eleType == EleType.TeeGas)
                return InitTeeGasResult();
            else if (eleType == EleType.Compressor)
                return InitCompressorResult();
            else if (eleType == EleType.Pump)
                return InitPumpResult();
            else if (eleType == EleType.SteamHeader)
                return InitSteamHeaderResult();
            else if (eleType == EleType.TeeWater)
                return InitTeeWaterResult();
            else if (eleType == EleType.PipeEle)
                return InitPipeEleResult();
            else if (eleType == EleType.GasTurbin)
                return InitGasTurbinResult();
            else if (eleType == EleType.GasMidTurbin)
                return InitGasMidTurbinResult();
            else if (eleType == EleType.GasLastTurbin)
                return InitGasLastTurbinResult();
            else if (eleType == EleType.Controllingstage)
                return InitControllingstageResult();
            else if (eleType == EleType.Turbin)
                return InitTurbinResult();
            else if (eleType == EleType.SmallTurbin)
                return InitSmallTurbinResult();
            else if (eleType == EleType.Laststage)
                return InitLaststageResult();
            else if (eleType == EleType.Condenser)
                return InitCondenserResult();
            else if (eleType == EleType.Airisland)
                return InitAirislandResult();
            else if (eleType == EleType.Deaerator)
                return InitDeaeratorResult();
            else if (eleType == EleType.WaterHeater)
                return InitWaterHeaterResult();
            else if (eleType == EleType.GasHeater)
                return InitGasHeaterResult();
            else if (eleType == EleType.PumpSteam)
                return InitPumpSteamResult();
            else if (eleType == EleType.PTReducer)
                return InitPTReducerResult();
            else if (eleType == EleType.Boiler)//
                return InitBoilerResult();
            else if (eleType == EleType.Calorifier)
                return InitCalorifierResult();
            else if (eleType == EleType.GasBoiler)
                return InitGasBoilerResult();
            else if (eleType == EleType.Fan)
                return InitFanResult();
            else if (eleType == EleType.FanSteam)
                return InitFanSteamResult();
            else if (eleType == EleType.Chimney)
                return InitChimneyResult();
            else if (eleType == EleType.ConTank)
                return InitConTankResult();
            else if (eleType == EleType.Ejector)
                return InitEjectorResult();
            else if (eleType == EleType.Generator)
                return InitGeneratorResult();
            else if (eleType == EleType.Motor)
                return InitMotorResult();
            else if (eleType == EleType.TeePower)
                return InitTeePowerResult();
            else if (eleType == EleType.GasBurner)
                return InitGasBurnerResult();
            else if (eleType == EleType.MixedHeatExchanger)
                return InitMixedHeatExchangerResult();
            else if (eleType == EleType.SurfaceHeatExchanger)
                return InitSurfaceHeatExchangerResult();
            else if (eleType == EleType.ControlDot)
                return InitControlDotResult();
            else if (eleType == EleType.TeeCoal)
                return InitTeeCoalResult();
            else if (eleType == EleType.GasHeatExchanger)
                return InitGasHeatExchangerResult();
            else if (eleType == EleType.ControlDotGas)
                return InitControlDotGasResult();
            else if (eleType == EleType.IterateDot)
                return InitIterateDotResult();
            else if (eleType == EleType.IterateDotGas)
                return InitIterateDotGasResult();
            else if (eleType == EleType.SlagCooler)
                return InitSlagCoolerResult();
            else if (eleType == EleType.BagFilter)
                return InitBagFilterResult();
            else if (eleType == EleType.Thionizer)
                return InitThionizerResult();
            else if (eleType == EleType.WaterTag)
                return InitWaterTagResult();
            else if (eleType == EleType.WaterValve)
                return InitWaterValveResult();
            else if (eleType == EleType.TeeValve)
                return InitTeeValveResult();
            else if (eleType == EleType.ControlValve)
                return InitControlValveResult();
            else if (eleType == EleType.Throttle)
                return InitThrottleResult();
            else if (eleType == EleType.HeatSupply)
                return InitHeatSupplyResult();
            return null;
        }

        private static ObservableCollection<PropertyGroup> InitHeatSupplyResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitThrottleResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitControlValveResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitTeeValveResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitWaterValveResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitWaterTagResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitThionizerResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitBagFilterResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitSlagCoolerResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitIterateDotGasResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitIterateDotResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitControlDotGasResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasHeatExchangerResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitTeeCoalResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitControlDotResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitSurfaceHeatExchangerResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitMixedHeatExchangerResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasBurnerResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitTeePowerResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitMotorResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGeneratorResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitEjectorResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitConTankResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitChimneyResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitFanSteamResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitFanResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasBoilerResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitCalorifierResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitBoilerResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitPTReducerResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitPumpSteamResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasHeaterResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitWaterHeaterResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitDeaeratorResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitAirislandResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitCondenserResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitLaststageResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitSmallTurbinResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitTurbinResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitControllingstageResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasLastTurbinResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasMidTurbinResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasTurbinResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitPipeEleResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitTeeWaterResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitSteamHeaderResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitPumpResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitCompressorResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitTeeGasResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitGasSourceResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitCoalSourceResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitWaterSourceResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitWaterPoolResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitECONResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitEVAP2Result()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitEVAPResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitSuperHeaterResult()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<PropertyGroup> InitCFBResult()
        {
            ObservableCollection<PropertyGroup> ResultGroups = new ObservableCollection<PropertyGroup>();
            PropertyGroup assitGroup = new PropertyGroup() { Flag = "辅助设计" };
            assitGroup.Properties.Add(new Property("ExPower", "元件名称", "锅炉热负荷", Units.kW, P_Type.ToCal));
            ResultGroups.Add(assitGroup);

            return ResultGroups;
        }
    }
}

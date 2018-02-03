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
        //包含各个元件的模式的初始化
        public static ObservableCollection<SelMode> InitComponentMode(EleType eleType)
        {
            if (eleType == EleType.CFB)
                return InitCFBMode();
            else if (eleType == EleType.SuperHeater)
                return InitSuperHeaterMode();
            else if (eleType == EleType.EVAP)
                return InitEVAPMode();
            else if (eleType == EleType.EVAP2)
                return InitEVAP2Mode();
            else if (eleType == EleType.ECON)
                return InitECONMode();
            else if (eleType == EleType.WaterPool)
                return InitWaterPoolMode();
            else if (eleType == EleType.WaterSource)
                return InitWaterSourceMode();
            else if (eleType == EleType.CoalSource)
                return InitCoalSourceMode();
            else if (eleType == EleType.GasSoource)
                return InitGasSourceMode();
            else if (eleType == EleType.TeeGas)
                return InitTeeGasMode();
            else if (eleType == EleType.Compressor)
                return InitCompressorMode();
            else if (eleType == EleType.Pump)
                return InitPumpMode();
            else if (eleType == EleType.SteamHeader)
                return InitSteamHeaderMode();
            else if (eleType == EleType.TeeWater)
                return InitTeeWaterMode();
            else if (eleType == EleType.PipeEle)
                return InitPipeEleMode();
            else if (eleType == EleType.GasTurbin)
                return InitGasTurbinMode();
            else if (eleType == EleType.GasMidTurbin)
                return InitGasMidTurbinMode();
            else if (eleType == EleType.GasLastTurbin)
                return InitGasLastTurbinMode();
            else if (eleType == EleType.Controllingstage)
                return InitControllingstageMode();
            else if (eleType == EleType.Turbin)
                return InitTurbinMode();
            else if (eleType == EleType.SmallTurbin)
                return InitSmallTurbinMode();
            else if (eleType == EleType.Laststage)
                return InitLaststageMode();
            else if (eleType == EleType.Condenser)
                return InitCondenserMode();
            else if (eleType == EleType.Airisland)
                return InitAirislandMode();
            else if (eleType == EleType.Deaerator)
                return InitDeaeratorMode();
            else if (eleType == EleType.WaterHeater)
                return InitWaterHeaterMode();
            else if (eleType == EleType.GasHeater)
                return InitGasHeaterMode();
            else if (eleType == EleType.PumpSteam)
                return InitPumpSteamMode();
            else if (eleType == EleType.PTReducer)
                return InitPTReducerMode();
            else if (eleType == EleType.Boiler)//
                return InitBoilerMode();
            else if (eleType == EleType.Calorifier)
                return InitCalorifierMode();
            else if (eleType == EleType.GasBoiler)
                return InitGasBoilerMode();
            else if (eleType == EleType.Fan)
                return InitFanMode();
            else if (eleType == EleType.FanSteam)
                return InitFanSteamMode();
            else if (eleType == EleType.Chimney)
                return InitChimneyMode();
            else if (eleType == EleType.ConTank)
                return InitConTankMode();
            else if (eleType == EleType.Ejector)
                return InitEjectorMode();
            else if (eleType == EleType.Generator)
                return InitGeneratorMode();
            else if (eleType == EleType.Motor)
                return InitMotorMode();
            else if (eleType == EleType.TeePower)
                return InitTeePowerMode();
            else if (eleType == EleType.GasBurner)
                return InitGasBurnerMode();
            else if (eleType == EleType.MixedHeatExchanger)
                return InitMixedHeatExchangerMode();
            else if (eleType == EleType.SurfaceHeatExchanger)
                return InitSurfaceHeatExchangerMode();
            else if (eleType == EleType.ControlDot)
                return InitControlDotMode();
            else if (eleType == EleType.TeeCoal)
                return InitTeeCoalMode();
            else if (eleType == EleType.GasHeatExchanger)
                return InitGasHeatExchangerMode();
            else if (eleType == EleType.ControlDotGas)
                return InitControlDotGasMode();
            else if (eleType == EleType.IterateDot)
                return InitIterateDotMode();
            else if (eleType == EleType.IterateDotGas)
                return InitIterateDotGasMode();
            else if (eleType == EleType.SlagCooler)
                return InitSlagCoolerMode();
            else if (eleType == EleType.BagFilter)
                return InitBagFilterMode();
            else if (eleType == EleType.Thionizer)
                return InitThionizerMode();
            else if (eleType == EleType.WaterTag)
                return InitWaterTagMode();
            else if (eleType == EleType.WaterValve)
                return InitWaterValveMode();
            else if (eleType == EleType.TeeValve)
                return InitTeeValveMode();
            else if (eleType == EleType.ControlValve)
                return InitControlValveMode();
            else if (eleType == EleType.Throttle)
                return InitThrottleMode();
            else if (eleType == EleType.HeatSupply)
                return InitHeatSupplyMode();
            return null;
        }

        private static ObservableCollection<SelMode> InitHeatSupplyMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitThrottleMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitControlValveMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitTeeValveMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitWaterValveMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitWaterTagMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitThionizerMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitBagFilterMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitSlagCoolerMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitIterateDotGasMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitIterateDotMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitControlDotGasMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitGasHeatExchangerMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitTeeCoalMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitControlDotMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitSurfaceHeatExchangerMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitMixedHeatExchangerMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitGasBurnerMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitTeePowerMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitMotorMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitGeneratorMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitEjectorMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitConTankMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitChimneyMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitFanSteamMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitFanMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitGasBoilerMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitCalorifierMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitBoilerMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitPTReducerMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitPumpSteamMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitGasHeaterMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitWaterHeaterMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitDeaeratorMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitAirislandMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitCondenserMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitLaststageMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitSmallTurbinMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitTurbinMode()
        {
            ObservableCollection<SelMode> Modes = new ObservableCollection<SelMode>();
            Modes.Add(SelMode.DesignMode);
            Modes.Add(SelMode.CalMode);
            Modes.Add(SelMode.InterMode);
            return Modes;
        }

        private static ObservableCollection<SelMode> InitControllingstageMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitGasLastTurbinMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitGasMidTurbinMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitGasTurbinMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitPipeEleMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitTeeWaterMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitSteamHeaderMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitPumpMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitCompressorMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitTeeGasMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitGasSourceMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitCoalSourceMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitWaterSourceMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitWaterPoolMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitECONMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitEVAP2Mode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitEVAPMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitSuperHeaterMode()
        {
            throw new NotImplementedException();
        }

        private static ObservableCollection<SelMode> InitCFBMode()
        {
            ObservableCollection<SelMode> Modes = new ObservableCollection<SelMode>();
            Modes.Add(SelMode.None);
            return Modes;
        }
    }
}

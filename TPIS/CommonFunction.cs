using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TPIS.Model;
using TPISNet;

namespace TPIS
{
    class CommonFunction
    {
        /// <summary>
        /// 新建原有模型元件
        /// </summary>
        /// <param name="s"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Element NewOriginElement(EleType s, int id)
        {
            Element e = null;
            if (s == EleType.CFB)
                e = new CFB(id);
            else if (s == EleType.SuperHeater)
                e = new SuperHeater(id);
            else if (s == EleType.EVAP)
                e = new EVAP(id);
            else if (s == EleType.EVAP2)
                e = new EVAP2(id);
            else if (s == EleType.ECON)
                e = new ECON(id);
            else if (s == EleType.WaterPool)
                e = new WaterPool(id);
            else if (s == EleType.WaterSource)
                e = new WaterSource(id);
            else if (s == EleType.CoalSource)
                e = new CoalSource(id);
            else if (s == EleType.GasSoource)
                e = new GasSource(id);
            else if (s == EleType.TeeGas)
                e = new TeeGas(id);
            else if (s == EleType.Compressor)
                e = new Compressor(id);
            else if (s == EleType.Pump)
                e = new Pump(id);
            else if (s == EleType.WaterPool)
                e = new WaterPool(id);
            else if (s == EleType.SteamHeader)
                e = new SteamHeader(id);
            else if (s == EleType.TeeWater)
                e = new TeeWater(id);
            else if (s == EleType.PipeEle)
                e = new PipeEle(id);
            else if (s == EleType.GasTurbin)
                e = new GasTurbin(id);
            else if (s == EleType.GasMidTurbin)
                e = new GasMidTurbin(id);
            else if (s == EleType.GasLastTurbin)
                e = new GasLastTurbin(id);
            else if (s == EleType.Controllingstage)
                e = new Controllingstage(id);
            else if (s == EleType.Turbin)
                e = new Turbin(id);
            else if (s == EleType.SmallTurbin)
                e = new SmallTurbin(id);
            else if (s == EleType.Laststage)
                e = new Laststage(id);
            else if (s == EleType.Condenser)
                e = new Condenser(id);
            else if (s == EleType.Airisland)
                e = new Airisland(id);
            else if (s == EleType.Deaerator)
                e = new Deaerator(id);
            else if (s == EleType.WaterHeater)
                e = new WaterHeater(id);
            else if (s == EleType.GasHeater)
                e = new GasHeater(id);
            else if (s == EleType.PumpSteam)
                e = new PumpSteam(id);
            else if (s == EleType.PTReducer)
                e = new PTReducer(id);
            else if (s == EleType.CoalSource)
                e = new CoalSource(id);
            else if (s == EleType.Boiler)//
                e = new Boiler(id);
            else if (s == EleType.Calorifier)
                e = new Calorifier(id);
            else if (s == EleType.GasBoiler)
                e = new GasBoiler(id);
            else if (s == EleType.Fan)
                e = new Fan(id);
            else if (s == EleType.FanSteam)
                e = new FanSteam(id);
            else if (s == EleType.Chimney)
                e = new Chimney(id);
            else if (s == EleType.ConTank)
                e = new ConTank(id);
            else if (s == EleType.Ejector)
                e = new Ejector(id);
            else if (s == EleType.Generator)
                e = new Generator(id);
            else if (s == EleType.Motor)
                e = new Motor(id);
            else if (s == EleType.TeePower)
                e = new TeePower(id);
            else if (s == EleType.GasBurner)
                e = new GasBurner(id);
            else if (s == EleType.MixedHeatExchanger)
                e = new MixedHeatExchanger(id);
            else if (s == EleType.SurfaceHeatExchanger)
                e = new SurfaceHeatExchanger(id);
            else if (s == EleType.ControlDot)
                e = new ControlDot(id);
            else if (s == EleType.TeeCoal)
                e = new TeeCoal(id);
            else if (s == EleType.GasHeatExchanger)
                e = new GasHeatExchanger(id);
            else if (s == EleType.ControlDotGas)
                e = new ControlDotGas(id);
            else if (s == EleType.IterateDot)
                e = new IterateDot(id);
            else if (s == EleType.IterateDotGas)
                e = new IterateDotGas(id);
            else if (s == EleType.SlagCooler)
                e = new SlagCooler(id);
            else if (s == EleType.BagFilter)
                e = new BagFilter(id);
            else if (s == EleType.Thionizer)
                e = new Thionizer(id);
            else if (s == EleType.WaterTag)
                e = new WaterTag(id);
            else if (s == EleType.WaterValve)
                e = new WaterValve(id);
            else if (s == EleType.TeeValve)
                e = new TeeValve(id);
            else if (s == EleType.ControlValve)
                e = new ControlValve(id);
            else if (s == EleType.Throttle)
                e = new Throttle(id);
            else if (s == EleType.HeatSupply)
                e = new HeatSupply(id);
            return e;
        }


        public static TPISComponent NewTPISComponent(int x, int y, int width, int height, ComponentType ct)
        {
            TPISComponent c = new TPISComponent(0,x, y, 1, width, height, ct);
            return c;
        }



        public static object DeserializeWithBinary(byte[] data)
        {
            MemoryStream stream = new MemoryStream();
            stream.Write(data, 0, data.Length);
            stream.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            object obj = bf.Deserialize(stream);

            stream.Close();

            return obj;
        }

        public static byte[] SerializeToBinary(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, obj);

            byte[] data = stream.ToArray();
            stream.Close();

            return data;
        }
    }
}

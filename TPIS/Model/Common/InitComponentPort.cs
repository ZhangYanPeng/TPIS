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

        public static ObservableCollection<Port> InitComponentPort(EleType eleType)
        {
            Element element = CommonTypeService.LoadElement(eleType);
            return InitPort(element);
        }

        private static ObservableCollection<Port> InitPort(Element element)
        {
            ObservableCollection<Port> ports = new ObservableCollection<Port>();
            foreach ( string key in element.IOPoints.Keys)
            {
                Nozzle n = element.IOPoints[key];
                if (n.IsInner)
                    continue;
                if(n.CanCancel == true)
                {
                    ports.Add(new Port(key, n.Name, n.NX[0]/element.Nwidth[0], n.NY[0] / element.Nheight[0], n.material, TransformNodType(n.nodtype), n.CanNotLink, true));
                }
                else
                {
                    ports.Add(new Port(key, n.Name, n.NX[0] / element.Nwidth[0], n.NY[0] / element.Nheight[0], n.material, TransformNodType(n.nodtype), n.CanNotLink));
                }
            }
            return ports;
        }

        internal static NodType TransformNodType(TPISNet.NodType type)
        {
            switch (type)
            {
                case TPISNet.NodType.Inlet: return NodType.Inlet;
                case TPISNet.NodType.Outlet: return NodType.Outlet;
                case TPISNet.NodType.Undef: return NodType.Undef;
            }
            return NodType.Undef;
        }


        private static ObservableCollection<Port> InitTurbinPort()
        {
            ObservableCollection<Port> ports = new ObservableCollection<Port>();
            ports.Add(new Port("InSteam", "进口蒸汽", 0, 0.125, Material.water, NodType.Inlet, false));
            ports.Add(new Port("OutSteam", "出口蒸汽", 1, 0, Material.water, NodType.Outlet, false));
            ports.Add(new Port("InPower", "输入功率", 0, 0.5, Material.power, NodType.Inlet, true));
            ports.Add(new Port("Outpower", "输出功率", 1, 0.5, Material.power, NodType.Outlet, false));
            ports.Add(new Port("ExtractSteam", "抽汽点", 1, 1, Material.water, NodType.Outlet, true, true));
            return ports;
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

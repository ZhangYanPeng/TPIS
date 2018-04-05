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
            foreach (string key in element.IOPoints.Keys)
            {
                Nozzle n = element.IOPoints[key];
                if (n.IsInner)
                    continue;
                if (n.CanCancel == true)
                {
                    ports.Add(new Port(key, n.Name, (double)n.NX[0] / (double)element.Nwidth[0], (double)n.NY[0] / (double)element.Nheight[0], n.material, TransformNodType(n.nodtype), n.CanNotLink, true));
                }
                else
                {
                    ports.Add(new Port(key, n.Name, (double)n.NX[0] / (double)element.Nwidth[0], (double)n.NY[0] / (double)element.Nheight[0], n.material, TransformNodType(n.nodtype), n.CanNotLink));
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
        
    }
}

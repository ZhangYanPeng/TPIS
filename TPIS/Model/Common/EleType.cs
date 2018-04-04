using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPISNet;

namespace TPIS.Model.Common
{

    public enum SelMode
    {
        None,
        DesignMode,
        InterMode,
        CalMode
    }

    public static partial class CommonTypeService
    {

        public static Element LoadElement(EleType eleType)
        {
            Interface inface = new Interface();
            return inface.NewElement(eleType, 0);
        }
    }
}

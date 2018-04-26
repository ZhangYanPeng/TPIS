using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPISNet;

namespace TPIS.Model.Common
{
    public static partial class CommonTypeService
    {
        public static string InitComponentPic(EleType eleType)
        {
            return "pack://SiteofOrigin:,,," + Interface.GetPNGstr(eleType);
            //return "pack://application:,,,/PNG/Airisland.png";
        }
    }
}

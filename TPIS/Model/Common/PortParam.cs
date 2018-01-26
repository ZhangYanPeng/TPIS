using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model.Common
{
    public enum Material
    {
        water,//未饱和水或者过热蒸汽
        coal,
        gas,
        fluegas,
        air,
        ash,
        oil,
        power,
        NA
    }

    public enum NodType
    {
        Inlet,
        Outlet,
        Undef,
        DefIn,
        DefOut
    }
}

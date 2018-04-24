using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model.Common
{
    interface IViewModel
    {
        TPISNet.Element ViewToModel(TPISComponent component);
        TPISComponent ModelToView(ComponentType ct);
    }
}

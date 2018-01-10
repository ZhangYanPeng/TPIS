using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model
{
    public class BaseType
    {
        public String Name { get; set; }
        public List<ComponentType> ComponentTypeList { get; set;  }

        public BaseType()
        {
            ComponentTypeList = new List<ComponentType>();
        }
    }
}

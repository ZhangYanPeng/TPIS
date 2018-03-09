using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Project
{
    [Serializable]
    public class ClipBoard
    {
        public List<ObjectBase> Objects { get; set; }

        public ClipBoard()
        {
            Objects = new List<ObjectBase>();
        }
    }
}

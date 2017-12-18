using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPIS.Model;

namespace TPIS.TPISCanvas
{
    public class Choosen
    {
        public List<Component> components;
        public List<Link> links;
        public Position position;

        public void Move(int x, int y)
        {
            foreach (Link l in links)
            {
                //选中区域连接线移动
                l.Move(x, y);
            }

            foreach (Component c in components)
            {
                
            }
        }
    }
}

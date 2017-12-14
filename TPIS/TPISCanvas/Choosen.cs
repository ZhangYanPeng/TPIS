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
                //移动组件
                c.Move(x, y);
                foreach(Port p in c.ports)
                {
                    if( !links.Contains(p.link))//连接线是否已经整体移动
                    {
                        //移动连接线的一端
                        p.link.Move(p.node, x, y);
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model
{
    public struct Position
    {
        public int x;//左上角横坐标
        public int y;//左上角纵坐标
        public int angle;//旋转角度 0 90 180 270
        public int width;//宽度
        public int height;//高度
    }

    public struct Port
    {
        public int x;//横坐标比例（0~1）
        public int y;//纵坐标比例（0~1)
        public Node node;
        public Link link;
    }

    public class Component
    {
        public Position Position { get; set; }
        public List<Port> Ports { get; set; }
        public 

        public Component(int tx, int ty)
        {
            this.Position = new Position { x = tx, y = ty, angle = 0, width = 0, height = 0 };
            this.Ports = new List<Port>();
        }
    }

}

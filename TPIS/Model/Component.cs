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
        public Position position;
        public List<Port> ports;

        public virtual void Draw()
        {
            //绘图函数
            return;
        }

        public virtual void Calculate()
        {
            //计算函数
            return;
        }
        
        /**
         * type: 0:左上 1:左下 2:右上 3:右下
         * x,y: 拖动长度
         * */
        public void Stretch(int type, int x, int y)
        {
            //改变大小
            return;
        }

        public void Move(int x, int y) //x,y: 拖动长度
        {
            //移动位置
            return;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPIS.Model;

namespace TPIS.Project
{
    public class ProjectItem
    {
        public String name;

        public List<Component> components;
        public List<Link> link;
        public ProjectCanvas canvas;

        public ProjectItem(int height, int width, string name)
        {
            this.name = name;
            this.canvas = new ProjectCanvas(height, width);
            this.components = null;
            this.link = null;
            return;
        }

        public void Draw()
        {
            canvas.Draw();
        }

    }
}

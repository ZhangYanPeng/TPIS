using System;
using System.Collections.Generic;
using TPIS.TPISCanvas;
using TPIS.Model;

namespace TPIS.Project
{
    public class ProjectItem
    {
        public String name;

        public List<Component> components;
        public List<Link> link;
        public ProjectCanvas canvas;

        public ProjectItem(string name)
        {
            this.name = name;
            this.canvas = new ProjectCanvas();
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

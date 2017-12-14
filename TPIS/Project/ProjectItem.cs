using System;
using System.Collections.Generic;
using TPIS.Model;

namespace TPIS.Project
{
    public class ProjectItem
    {
        public String Name { get; set; }
        public String V_name { get; set; }

        public List<Component> Components { get; set; }
        public List<Link> link;
        public ProjectCanvas Canvas { get; set; }

        public ProjectItem(string name, ProjectCanvas pCanvas)
        {
            this.Name = name +".tpis";
            this.Canvas = pCanvas;
            this.Components = null;
            this.link = null;
            return;
        }

        public void Draw()
        {
        }

    }
}

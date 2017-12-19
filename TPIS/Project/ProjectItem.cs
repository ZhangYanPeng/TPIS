using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using TPIS.Model;

namespace TPIS.Project
{
    public class ProjectItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public String Name { get; set; }
        public String V_name { get; set; }

        public ObservableCollection<TPISComponent> Components { get; set; }
        public ObservableCollection<Link> link;
        public ProjectCanvas Canvas { get; set; }

        public ProjectItem(string name, ProjectCanvas pCanvas)
        {
            this.Name = name +".tpis";
            this.Canvas = pCanvas;
            this.Components = new ObservableCollection<TPISComponent>();
            this.link = new ObservableCollection<Link>();
            return;
        }

        public void Draw()
        {

        }

    }
}

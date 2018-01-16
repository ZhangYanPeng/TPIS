using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using TPIS.Model;

namespace TPIS.Project
{
    public class ObjectBase
    {
    }

    public class ProjectItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public String Name { get; set; }
        public String V_name { get; set; }
        public long Num { get; set; }
        
        public ObservableCollection<ObjectBase> Objects { get; set; }

        public ProjectCanvas Canvas { get; set; }

        public ProjectItem(string name, ProjectCanvas pCanvas,long num)
        {
            this.Name = name +".tpis";
            this.Num = num;
            this.Canvas = pCanvas;
            Objects = new ObservableCollection<ObjectBase>();
            return;
        }

        public void Draw()
        {

        }

    }
}

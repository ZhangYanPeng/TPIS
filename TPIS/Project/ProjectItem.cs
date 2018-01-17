using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
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
            //TPISLine l = new TPISLine();
            //PointCollection pc = new PointCollection();
            //Point p1 = new Point(10.3333333, 100);
            //Point p2 = new Point(50, 200);
            //Point p3 = new Point(70, 900);
            //pc.Add(p1);
            //pc.Add(p2);
            //pc.Add(p3);
            //l.Points = pc;
            //Objects.Add(l);
            return;
        }

        public void Draw()
        {

        }

    }
}

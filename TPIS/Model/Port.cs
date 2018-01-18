using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model
{
    public class Port : INotifyPropertyChanged
    {
        public double x;
        public double p_x;
        public double P_x
        {
            get => p_x;
            set
            {
                p_x = value;
                OnPropertyChanged("P_x");
            }
        }//横坐标比例（0~1）

        public double y;
        public double p_y;
        public double P_y
        {
            get => p_y;
            set
            {
                p_y = value;
                OnPropertyChanged("P_y");
            }
        }//纵坐标比例（0~1)
        public bool type; // true : in  false: out
        public Node node;
        public TPISLine link;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}

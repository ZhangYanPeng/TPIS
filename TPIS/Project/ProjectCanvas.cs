using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TPIS.Project
{
    public class ProjectCanvas : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private int height;
        private int width;
        private double rate;
        private double v_height;
        private double v_width;

        public int Height
        {
            get { return height; }
            set
            {
                this.height = value;
                this.v_height = height * this.rate;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Whole_height"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V_height"));
                }
            }
        }

        public int Width
        {
            get { return width; }
            set
            {
                this.width = value;
                this.v_width = width * this.rate;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Whole_width"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V_width"));
                }
            }
        }

        public double Rate
        {
            get { return rate; }
            set
            {
                this.rate = value;
                this.v_height = height * this.rate;
                this.v_width = width * this.rate;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Whole_width"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V_width"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Whole_height"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V_height"));
                }
                //更改所有component 和 link 的rate

            }
        }

        public double V_height { get { return v_height; } set { v_height = value; } }
        public double V_width { get { return v_width; } set { v_width = value; } }

        public ProjectCanvas(int w, int h)
        {
            this.width = w;
            this.v_width = w;
            this.height = h;
            this.v_height = h;
            this.rate = 1;
        }

    }
}

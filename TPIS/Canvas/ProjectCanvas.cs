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
        private double whole_width;
        private double whole_height;

        public int Height
        {
            get { return height; }
            set
            {
                this.height = value;
                this.v_height = height * this.rate;
                this.whole_height = this.v_height + 200;
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
                this.whole_width = this.v_width + 200;
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
                this.whole_height = this.v_height + 200;
                this.v_width = width * this.rate;
                this.whole_width = this.v_width + 200;
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
        public double Whole_width { get { return whole_width; } set { whole_width = value; } }
        public double Whole_height { get { return whole_height; } set { whole_height = value; } }

        public ProjectCanvas(int height, int width)
        {
            this.height = height;
            this.width = width;
            rate = 1.0;
            this.v_height = height * rate;
            this.v_width = width * rate;
            this.whole_width = height * rate + 200;
            this.whole_height = width * rate + 200;
        }


        public void Draw()
        {

        }
    }
}

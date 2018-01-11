using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model
{
    public class ComponentType : System.ComponentModel.INotifyPropertyChanged
    {
        public int Id { get; set;  }
        public String PicPath { get; set; }
        public String Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// 判定按钮是否按下
        /// </summary>
        public bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
    }
}

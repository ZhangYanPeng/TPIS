using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TPIS.Model
{
    [Serializable]
    public class PropertyGroup : ISerializable, INotifyPropertyChanged
    {
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
        /// 序列化与反序列化
        /// </summary>
        #region
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("flag", Flag);
            info.AddValue("properties", Properties);
            if (visible == Visibility.Visible)
                info.AddValue("visible", true);
            else
                info.AddValue("visible", false);
        }

        public PropertyGroup(SerializationInfo info, StreamingContext context)
        {
            this.Flag = info.GetString("flag");
            this.Properties = (ObservableCollection<Property>)info.GetValue("properties", typeof(Object));
            bool vi = info.GetBoolean("visible");
            if (vi)
                Visible = Visibility.Visible;
            else
                Visible = Visibility.Collapsed;
        }
        #endregion

        public ObservableCollection<Property> Properties { get; set; } //该类属性字典
        public string Flag { get; set; }
        public Visibility visible;
        public Visibility Visible
        {
            get {
                return visible;
            }
            set
            {
                visible = value;
                OnPropertyChanged("Visible");
            }
        }

        public PropertyGroup()
        {
            Properties = new ObservableCollection<Property>();
            Visible = Visibility.Visible;
        }

        internal void SelectProperty(bool type)
        {
            if (type)
            {
                Visible = Visibility.Visible;
                return;
            }
            bool vi = false;
            foreach(Property p in Properties)
            {
                vi = vi || p.visible;
            }
            if (vi)
                Visible = Visibility.Visible;
            else
                Visible = Visibility.Collapsed;
        }
    }
}

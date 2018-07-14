using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model
{
    [Serializable]
    public class Coal : INotifyPropertyChanged, ISerializable
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
        #region 序列化
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("P_C", P_C);
            info.AddValue("P_H", P_H);
            info.AddValue("P_O", P_O);
            info.AddValue("P_N", P_N);
            info.AddValue("P_S", P_S);
            info.AddValue("P_A", P_A);
            info.AddValue("P_M", P_M);
            info.AddValue("P_V", P_V);
            info.AddValue("P_LHV", P_LHV);
        }

        public Coal(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            P_C = info.GetDouble("P_C");
            P_H = info.GetDouble("P_H");
            P_O = info.GetDouble("P_O");
            P_N = info.GetDouble("P_N");
            P_S = info.GetDouble("P_S");
            P_A = info.GetDouble("P_A");
            P_M = info.GetDouble("P_M");
            P_V = info.GetDouble("P_V");
            P_LHV = info.GetDouble("P_LHV");
        }
        #endregion


        public Coal()
        {
            Name = "新煤种";
            P_C = 0.0;
            P_H = 0.0;
            P_O = 0.0;
            P_N = 0.0;
            P_S = 0.0;
            P_A = 0.0;
            P_M = 0.0;
            P_V = 0.0;
            P_LHV = 0.0;
        }

        public String Name { get; set; }
        public double P_C { get; set; }
        public double P_H { get; set; }
        public double P_O { get; set; }
        public double P_N { get; set; }
        public double P_S { get; set; }
        public double P_A { get; set; }
        public double P_M { get; set; }
        public double P_V { get; set; }
        public double P_LHV { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Views.Modules
{
    [Serializable]
    public partial class Gas : INotifyPropertyChanged, ISerializable
    {
        #region 属性更新
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        public double N2 { get; set; }
        public double O2 { get; set; }
        public double CO2 { get; set; }
        public double H2O { get; set; }
        public double CO { get; set; }
        public double H2S { get; set; }
        public double H2 { get; set; }
        public double He { get; set; }
        public double Ar { get; set; }
        public double SO2 { get; set; }
        public double CH4 { get; set; }
        public double C2H6 { get; set; }
        public double C3H8 { get; set; }
        public double C4H10 { get; set; }
        public double C5H12 { get; set; }
        public double LHV_KJ_Nm3 { get; set; }
        public double HHV_KJ_Nm3 { get; set; }

        public Gas()
        {
            this.N2 = 0.0;
            this.O2 = 0.0;
            this.CO2 = 0.0;
            this.H2O = 0.0;
            this.H2S = 0.0;
            this.He = 0.0;
            this.Ar = 0.0;
            this.SO2 = 0.0;
            this.CH4 = 0.0;
            this.C2H6 = 0.0;
            this.C3H8 = 0.0;
            this.C4H10 = 0.0;
            this.C5H12 = 0.0;
            this.LHV_KJ_Nm3 = 0.0;
            this.HHV_KJ_Nm3 = 0.0;
            this.CO = 0.0;
            this.H2 = 0.0;
        }

        #region 序列化与反序列化
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("N2", N2);
            info.AddValue("O2", O2);
            info.AddValue("CO2", CO2);
            info.AddValue("H2O", H2O);
            info.AddValue("H2S", H2S);
            info.AddValue("He", He);
            info.AddValue("Ar", Ar);
            info.AddValue("SO2", SO2);
            info.AddValue("CH4", CH4);
            info.AddValue("C2H6", C2H6);
            info.AddValue("C3H8", C3H8);
            info.AddValue("C4H10", C4H10);
            info.AddValue("C5H12", C5H12);
            info.AddValue("LHV_KJ_Nm3", LHV_KJ_Nm3);
            info.AddValue("HHV_KJ_Nm3", HHV_KJ_Nm3);
            info.AddValue("CO", CO);
            info.AddValue("H2", H2);
        }

        public Gas(SerializationInfo info, StreamingContext context)
        {
            this.N2 = info.GetDouble("N2");
            this.O2 = info.GetDouble("O2");
            this.CO2 = info.GetDouble("CO2");
            this.H2O = info.GetDouble("H2O");
            this.H2S = info.GetDouble("H2S");
            this.He = info.GetDouble("He");
            this.Ar = info.GetDouble("Ar");
            this.SO2 = info.GetDouble("SO2");
            this.CH4 = info.GetDouble("CH4");
            this.C2H6 = info.GetDouble("C2H6");
            this.C3H8 = info.GetDouble("C3H8");
            this.C4H10 = info.GetDouble("C4H10");
            this.C5H12 = info.GetDouble("C5H12");
            this.LHV_KJ_Nm3 = info.GetDouble("LHV_KJ_Nm3");
            this.HHV_KJ_Nm3 = info.GetDouble("HHV_KJ_Nm3");
            this.CO = info.GetDouble("CO");
            this.H2 = info.GetDouble("H2");
            return;
        }
        #endregion
    }
}

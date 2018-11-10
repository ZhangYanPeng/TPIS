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
    public class Gas : INotifyPropertyChanged, ISerializable
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

        public string gas_EvalResult;
        public string Gas_EvalResult
        {
            get => gas_EvalResult; set
            {
                gas_EvalResult = value;
                OnPropertyChanged("Gas_EvalResult");
            }
        }

        public string name;
        public String Name
        {
            get => name;
            set
            {
                name = value;
                Evaluation();
            }
        }

        public double gas_N2;
        public double N2
        {
            get => gas_N2;
            set
            {
                gas_N2 = value;
                Evaluation();
            }
        }
        public double gas_O2;
        public double O2
        {
            get => gas_O2;
            set
            {
                gas_O2 = value;
                Evaluation();
            }
        }
        public double gas_CO2;
        public double CO2
        {
            get => gas_CO2;
            set
            {
                gas_CO2 = value;
                Evaluation();
            }
        }
        public double gas_H2O;
        public double H2O
        {
            get => gas_H2O;
            set
            {
                gas_H2O = value;
                Evaluation();
            }
        }

        public double gas_CO;
        public double CO
        {
            get => gas_CO;
            set
            {
                gas_CO = value;
                Evaluation();
            }
        }
        public double gas_H2S;
        public double H2S
        {
            get => gas_H2S;
            set
            {
                gas_H2S = value;
                Evaluation();
            }
        }
        public double gas_H2;
        public double H2
        {
            get => gas_H2;
            set
            {
                gas_H2 = value;
                Evaluation();
            }
        }
        public double gas_He;
        public double He
        {
            get => gas_He;
            set
            {
                gas_He = value;
                Evaluation();
            }
        }
        public double gas_Ar;
        public double Ar
        {
            get => gas_Ar;
            set
            {
                gas_Ar = value;
                Evaluation();
            }
        }
        public double gas_SO2;
        public double SO2
        {
            get => gas_SO2;
            set
            {
                gas_SO2 = value;
                Evaluation();
            }
        }
        public double gas_CH4;
        public double CH4
        {
            get => gas_CH4;
            set
            {
                gas_CH4 = value;
                Evaluation();
            }
        }
        public double gas_C2H6;
        public double C2H6
        {
            get => gas_C2H6;
            set
            {
                gas_C2H6 = value;
                Evaluation();
            }
        }
        public double gas_C3H8;
        public double C3H8
        {
            get => gas_C3H8;
            set
            {
                gas_C3H8 = value;
                Evaluation();
            }
        }
        public double gas_C4H10;
        public double C4H10
        {
            get => gas_C4H10;
            set
            {
                gas_C4H10 = value;
                Evaluation();
            }
        }
        public double gas_C5H12;
        public double C5H12
        {
            get => gas_C5H12;
            set
            {
                gas_C5H12 = value;
                Evaluation();
            }
        }

        public double gas_LHV_KJ_Nm3;
        public double LHV_KJ_Nm3
        {
            get => gas_LHV_KJ_Nm3;
            set
            {
                gas_LHV_KJ_Nm3 = value;
                Evaluation();
            }
        }

        public double gas_HHV_KJ_Nm3;
        public double HHV_KJ_Nm3
        {
            get => gas_HHV_KJ_Nm3;
            set
            {
                gas_HHV_KJ_Nm3 = value;
                Evaluation();
            }
        }

        private void Evaluation()
        {
            TPISNet.Gas g = new TPISNet.Gas();
            g.Pv["N2"].Data = N2;
            g.Pv["O2"].Data = O2;
            g.Pv["CO2"].Data = CO2;
            g.Pv["H2O"].Data = H2O;
            g.Pv["CO"].Data = CO;
            g.Pv["H2S"].Data = H2S;
            g.Pv["H2"].Data = H2;
            g.Pv["He"].Data = He;
            g.Pv["Ar"].Data = Ar;
            g.Pv["SO2"].Data = SO2;
            g.Pv["CH4"].Data = CH4;
            g.Pv["C2H6"].Data = C2H6;
            g.Pv["C3H8"].Data = C3H8;
            g.Pv["C4H10"].Data = C4H10;
            g.Pv["C5H12"].Data = C5H12;
            g.Pv["LHV"].Data = LHV_KJ_Nm3;
            g.Pv["HHV"].Data = HHV_KJ_Nm3;
            string warning = "";
            g.IsHundred(warning);
            Gas_EvalResult = warning;
        }

        public Gas()
        {
            Name = "气体种类";
            N2 = 0.0;
            O2 = 0.0;
            CO2 = 0.0;
            H2O = 0.0;
            H2S = 0.0;
            He = 0.0;
            Ar = 0.0;
            SO2 = 0.0;
            CH4 = 0.0;
            C2H6 = 0.0;
            C3H8 = 0.0;
            C4H10 = 0.0;
            C5H12 = 0.0;
            LHV_KJ_Nm3 = 0.0;
            HHV_KJ_Nm3 = 0.0;
            CO = 0.0;
            H2 = 0.0;
        }

        #region 序列化与反序列化
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
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
            Name = info.GetString("Name");
            N2 = info.GetDouble("N2");
            O2 = info.GetDouble("O2");
            CO2 = info.GetDouble("CO2");
            H2O = info.GetDouble("H2O");
            H2S = info.GetDouble("H2S");
            He = info.GetDouble("He");
            Ar = info.GetDouble("Ar");
            SO2 = info.GetDouble("SO2");
            CH4 = info.GetDouble("CH4");
            C2H6 = info.GetDouble("C2H6");
            C3H8 = info.GetDouble("C3H8");
            C4H10 = info.GetDouble("C4H10");
            C5H12 = info.GetDouble("C5H12");
            LHV_KJ_Nm3 = info.GetDouble("LHV_KJ_Nm3");
            HHV_KJ_Nm3 = info.GetDouble("HHV_KJ_Nm3");
            CO = info.GetDouble("CO");
            H2 = info.GetDouble("H2");
            return;
        }
        #endregion
    }
}

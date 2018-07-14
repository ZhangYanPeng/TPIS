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

        private void Evaluation()
        {
            TPISNet.Coal c = new TPISNet.Coal();
            c.DProperty["C"].Data = P_C;
            c.DProperty["H"].Data = P_H;
            c.DProperty["O"].Data = P_O;
            c.DProperty["N"].Data = P_N;
            c.DProperty["S"].Data = P_S;
            c.DProperty["A"].Data = P_A;
            c.DProperty["M"].Data = P_M;
            c.DProperty["V"].Data = P_V;
            c.DProperty["LHV"].Data = P_LHV;
            string warning = "";
            c.IsHundred(out warning);
            EvalResult = warning;
        }

        public string evalResult;
        public string EvalResult { get=>evalResult; set
            {
                evalResult = value;
                OnPropertyChanged("EvalResult");
            }
        }

        public string name;
        public String Name {
            get => name;
            set {
                name = value;
                Evaluation();
            }
        }

        public double p_c;
        public double P_C
        {
            get => p_c;
            set
            {
                p_c = value;
                Evaluation();
            }
        }

        public double p_h;
        public double P_H
        {
            get => p_h;
            set
            {
                p_h = value;
                Evaluation();
            }
        }

        public double p_o;
        public double P_O
        {
            get => p_o;
            set
            {
                p_o = value;
                Evaluation();
            }
        }

        public double p_n;
        public double P_N
        {
            get => p_n;
            set
            {
                p_n = value;
                Evaluation();
            }
        }

        public double p_s;
        public double P_S
        {
            get => p_s;
            set
            {
                p_s = value;
                Evaluation();
            }
        }

        public double p_a;
        public double P_A
        {
            get => p_a;
            set
            {
                p_a = value;
                Evaluation();
            }
        }

        public double p_m;
        public double P_M
        {
            get => p_m;
            set
            {
                p_m = value;
                Evaluation();
            }
        }

        public double p_v;
        public double P_V
        {
            get => p_v;
            set
            {
                p_v = value;
                Evaluation();
            }
        }

        public double p_lhv;
        public double P_LHV
        {
            get => p_lhv;
            set
            {
                p_lhv = value;
                Evaluation();
            }
        }
    }
}

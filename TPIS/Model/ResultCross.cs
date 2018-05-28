using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using TPIS.Project;

namespace TPIS.Model
{
    public static class CrossSize
    {
        public static double WIDTH = 90;
        public static double HEIGHT = 20;
    }

    [Serializable]
    public class ResultCross : ObjectBase, INotifyPropertyChanged, ISerializable
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

        public Port LinkPort { get; set; }
        
        public Position Position { get; set; }

        public ResultCross(Port port, int no, double rate, double vx, double vy)
        {
            LinkPort = port;
            No = no;
            port.CrossNo = no;
            this.Position = new Position { Rate = rate };
            this.Position.V_x = vx;
            this.Position.V_y = vy;
            this.Position.Width = 90;
            this.Position.V_height = 30;
        }

        internal void PosChange(double? x, double? y)
        {
            if (x.HasValue)
                Position.V_x = x.Value;
            if (y.HasValue)
                Position.V_y = y.Value;
        }

        internal void SetRate(double rate)
        {
            Position.Rate = rate;
        }

        public override object Clone()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream, this);
            stream.Position = 0;
            Object obj = bf.Deserialize(stream);
            stream.Close();
            return obj;
        }

        #region 序列化与反序列化
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("no", No);
            info.AddValue("position", Position);
        }

        public ResultCross(SerializationInfo info, StreamingContext context)
        {
            this.No = info.GetInt32("no");
            this.Position = (Position)info.GetValue("position", typeof(Object));
        }
        #endregion
    }
}
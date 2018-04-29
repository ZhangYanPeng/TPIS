using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TPIS.Project;

namespace TPIS.Model
{
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

        public ResultCross(Port port, int no, double rate, Point pos)
        {
            LinkPort = port;
            No = no;
            port.CrossNo = no;
            this.Position = new Position { Rate = rate };
            this.Position.X = pos.X;
            this.Position.X = pos.Y;
            this.Position.Width = 90;
            this.Position.V_height = 30;
        }

        #region 序列化与反序列化
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public ResultCross(SerializationInfo info, StreamingContext context)
        {
            //this.Name = info.GetString("name");
            //this.Objects = (ObservableCollection<ObjectBase>)info.GetValue("objects", typeof(Object));
        }

        internal void PosChange(double? x, double? y)
        {
            if (x.HasValue)
                Position.V_x += x.Value;
            if (y.HasValue)
                Position.V_y += y.Value;
        }

        internal void SetRate(double rate)
        {
            Position.Rate = rate;
        }
        #endregion
    }
}
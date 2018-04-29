using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model
{
    public class ResultCross : INotifyPropertyChanged, ISerializable
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

        public int No { get; set; }
        public Point Pos { get; set; }

        public ResultCross(Port port, int no, Point pos)
        {
            LinkPort = port;
            No = no;
            port.CrossNo = no;
            Pos = pos;
        }

        #region 序列化与反序列化
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //info.AddValue("name", Name);
        }

        public ResultCross(SerializationInfo info, StreamingContext context)
        {
            //this.Name = info.GetString("name");
            //this.Objects = (ObservableCollection<ObjectBase>)info.GetValue("objects", typeof(Object));
        }
        #endregion
    }
}
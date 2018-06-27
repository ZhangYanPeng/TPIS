using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using TPIS.Project;

namespace TPIS.Model
{
    [Serializable]
    public class TPISText : ObjectBase, INotifyPropertyChanged, ISerializable
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

        public string text;
        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public double fontSize;
        public double FontSize
        {
            get => fontSize;
            set
            {
                fontSize = value;
                OnPropertyChanged("FontSize");
            }
        }
        public Position Position { get; set; }

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public TPISText(double fontSize, int no, double rate, double vx, double vy)
        {
            Text = "请输入文本";
            this.Position = new Position { Rate = rate };
            this.Position.V_x = vx;
            this.Position.V_y = vy;
            this.Position.Width = fontSize*5;
            this.Position.Height = fontSize*1.2;
            FontSize = fontSize;
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

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("no", No);
            info.AddValue("position", Position);
            info.AddValue("text", Text);
            info.AddValue("fontSize", FontSize);
        }

        public TPISText(SerializationInfo info, StreamingContext context)
        {
            this.No = info.GetInt32("no");
            this.Position = (Position)info.GetValue("position", typeof(Object));
            this.FontSize = info.GetInt32("fontSize");
            this.Text = info.GetString("text");
        }

        internal void PosChange(double? x, double? y)
        {
            if (x.HasValue)
                Position.V_x += x.Value;
            if (y.HasValue)
                Position.V_y += y.Value;
        }

        internal void SizeChange(double? width, double? height)
        {
            if (width.HasValue)
                Position.Width = Position.Width + width.Value / Position.Rate > 0 ? Position.Width + width.Value / Position.Rate : 1;
            if (height.HasValue)
                Position.Height = Position.Height + height.Value / Position.Rate > 0 ? Position.Height + height.Value / Position.Rate : 1;
        }

        internal void SetRate(double rate)
        {
            Position.Rate = rate;
        }
    }
}

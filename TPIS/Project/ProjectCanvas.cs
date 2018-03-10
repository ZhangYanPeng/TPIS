using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using TPIS.Model;

namespace TPIS.Project
{
    public enum OperationType
    {
        ADD_COMPONENT,
        SELECT,
        ADD_LINE
    }

    [Serializable]
    public class ProjectCanvas : INotifyPropertyChanged, ISerializable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int height;
        private int width;
        private double rate;
        private double v_height;
        private double v_width;

        //光标操作
        public OperationType Operation { get; set; } // 类型
        public Dictionary<String, int> OperationParam { get; set; } //参数

        public bool CanLink { get; set; }
        public bool CanStopLink { get; set; }
        public bool LinkStartPoint { get; set; }
        public Point statrPoint;
        public Port startPort { get; set; }
        public Port endPort { get; set; }
        public Point endPoint;

        public int Height
        {
            get { return height; }
            set
            {
                this.height = value;
                this.v_height = height * this.rate;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V_height"));
                }
            }
        }

        public int Width
        {
            get { return width; }
            set
            {
                this.width = value;
                this.v_width = width * this.rate;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V_width"));
                }
            }
        }

        public double Rate
        {
            get { return rate; }
            set
            {
                this.rate = value;
                this.v_height = height * this.rate;
                this.v_width = width * this.rate;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V_width"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("V_height"));
                }
            }
        }

        public double V_height { get { return v_height; } set { v_height = value; } }
        public double V_width { get { return v_width; } set { v_width = value; } }

        public ProjectCanvas(int w, int h)
        {
            this.width = w;
            this.v_width = w;
            this.height = h;
            this.v_height = h;
            this.rate = 1;
            this.Operation = OperationType.SELECT;
            this.OperationParam = new Dictionary<String, int>();
        }

        /// <summary>
        /// 序列化与反序列化
        /// </summary>
        #region
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("width", width);
            info.AddValue("height", height);
            this.Operation = OperationType.SELECT;
            this.OperationParam = new Dictionary<String, int>();
        }

        public ProjectCanvas(SerializationInfo info, StreamingContext context)
        {
            this.width = info.GetInt32("width");
            this.v_width = info.GetInt32("width"); 
            this.height = info.GetInt32("height"); 
            this.v_height = info.GetInt32("height");
            this.rate = 1;
            this.Operation = OperationType.SELECT;
            this.OperationParam = new Dictionary<String, int>();
        }
        #endregion
    }
}

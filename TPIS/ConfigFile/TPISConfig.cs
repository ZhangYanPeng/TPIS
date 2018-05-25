using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TPIS.Project;

namespace TPIS.ConfigFile
{
    [Serializable]
    public abstract class ObjectBase : ISerializable, ICloneable
    {
        public bool isSelected;
        public int No { get; set; }

        public abstract object Clone();
        public abstract void GetObjectData(SerializationInfo info, StreamingContext context);
    }

    [Serializable]
    public partial class TPISConfig : ISerializable
    {
        public Brush CANVAS_BACKGROUNDCOLOR;
        public int CANVAS_WIDTH;
        public int CANVAS_HEIGHT;

        public void LoadCfg()
        {
            InitCfg();
            string path = @".\ConfigFile\" + "\\Config";
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            object obj = CommonFunction.DeserializeWithBinary(data);
            this.CANVAS_BACKGROUNDCOLOR = ((TPISConfig)obj).CANVAS_BACKGROUNDCOLOR;
            this.CANVAS_WIDTH = ((TPISConfig)obj).CANVAS_WIDTH;
            this.CANVAS_HEIGHT = ((TPISConfig)obj).CANVAS_HEIGHT;
        }
        public void SaveCfg()
        {
            //this.CANVAS_BACKGROUNDCOLOR = item.BackGroundColor;
            //this.CANVAS_WIDTH = item.Canvas.Width;
            //this.CANVAS_HEIGHT = item.Canvas.Height;
            string path = @".\ConfigFile\" + "\\Config";
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            byte[] data = CommonFunction.SerializeToBinary(this);
            BinaryWriter bw = new BinaryWriter(fileStream);
            bw.Write(data);
            bw.Close();
            fileStream.Close();
        }

        public void InitCfg()
        {
            string directoryPath = @".\ConfigFile\";
            if (!Directory.Exists(directoryPath))//如果路径不存在
            {
                this.CANVAS_BACKGROUNDCOLOR = Brushes.White;
                this.CANVAS_WIDTH = 1600;
                this.CANVAS_HEIGHT = 1000;
                Directory.CreateDirectory(directoryPath);//创建一个路径的文件夹
                string path = @".\ConfigFile\" + "\\Config";
                FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
                byte[] data = CommonFunction.SerializeToBinary(this);
                BinaryWriter bw = new BinaryWriter(fileStream);
                bw.Write(data);
                bw.Close();
                fileStream.Close();
            }
        }

        #region 序列化与反序列化
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("backGroundColor", CANVAS_BACKGROUNDCOLOR.ToString());
            info.AddValue("canvas_width", CANVAS_WIDTH);
            info.AddValue("canvas_height", CANVAS_HEIGHT);
        }

        public TPISConfig(SerializationInfo info, StreamingContext context)
        {
            BrushConverter brushConverter = new BrushConverter();
            this.CANVAS_BACKGROUNDCOLOR = (Brush)brushConverter.ConvertFromString(info.GetString("backGroundColor"));
            this.CANVAS_WIDTH = info.GetInt32("canvas_width");
            this.CANVAS_HEIGHT = info.GetInt32("canvas_height");
        }

        public TPISConfig()
        {
            LoadCfg();
        }
        #endregion
    }
}

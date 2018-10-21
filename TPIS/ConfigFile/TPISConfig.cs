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
        public int CANVAS_GRID;

        public int MAX_ITER;
        public int WATER_STAND;
        public int GAS_STAND;
        public int LINE_THICKNESS;

        public void LoadCfg()
        {
            InitCfg();
            string path = @".\ConfigFile\" + "\\Config";
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            //byte[] data = new byte[fileStream.Length];
            //fileStream.Read(data, 0, data.Length);
            //fileStream.Close();
            object obj = CommonFunction.DeserializeWithBinary(fileStream);
            this.CANVAS_BACKGROUNDCOLOR = ((TPISConfig)obj).CANVAS_BACKGROUNDCOLOR;
            this.CANVAS_WIDTH = ((TPISConfig)obj).CANVAS_WIDTH;
            this.CANVAS_HEIGHT = ((TPISConfig)obj).CANVAS_HEIGHT;
            this.MAX_ITER = ((TPISConfig)obj).MAX_ITER;
            this.WATER_STAND = ((TPISConfig)obj).WATER_STAND;
            this.GAS_STAND = ((TPISConfig)obj).GAS_STAND;
            this.LINE_THICKNESS = ((TPISConfig)obj).LINE_THICKNESS;
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
                MAX_ITER = 60;
                WATER_STAND = 0;
                GAS_STAND = 0;
                LINE_THICKNESS = 3;

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
            info.AddValue("MAX_ITER", MAX_ITER);
            info.AddValue("WATER_STAND", WATER_STAND);
            info.AddValue("GAS_STAND", GAS_STAND);
            info.AddValue("LINE_THICKNESS", LINE_THICKNESS);
        }

        public TPISConfig(SerializationInfo info, StreamingContext context)
        {
            BrushConverter brushConverter = new BrushConverter();
            this.CANVAS_BACKGROUNDCOLOR = (Brush)brushConverter.ConvertFromString(info.GetString("backGroundColor"));
            this.CANVAS_WIDTH = info.GetInt32("canvas_width");
            this.CANVAS_HEIGHT = info.GetInt32("canvas_height");
            this.MAX_ITER = info.GetInt32("MAX_ITER");
            this.WATER_STAND = info.GetInt32("WATER_STAND");
            this.GAS_STAND = info.GetInt32("GAS_STAND");
            this.LINE_THICKNESS = info.GetInt32("LINE_THICKNESS");
        }

        public TPISConfig()
        {
            LoadCfg();
        }
        #endregion
    }
}

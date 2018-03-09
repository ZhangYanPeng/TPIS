using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TPIS.Model
{
    [Serializable]
    public class PropertyGroup : ISerializable
    {
        /// <summary>
        /// 序列化与反序列化
        /// </summary>
        #region
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("flag", Flag);
            info.AddValue("properties", Properties);
        }

        public PropertyGroup(SerializationInfo info, StreamingContext context)
        {
            this.Flag = info.GetString("flag");
            this.Properties = (ObservableCollection<Property>)info.GetValue("properties", typeof(Object));
        }
        #endregion

        public ObservableCollection<Property> Properties { get; set; } //该类属性字典
        public string Flag { get; set; }

        public PropertyGroup()
        {
            Properties = new ObservableCollection<Property>();
        }
    }
}

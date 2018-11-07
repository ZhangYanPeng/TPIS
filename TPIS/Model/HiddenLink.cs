using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TPIS.Project;

namespace TPIS.Model
{
    [Serializable]
    class HiddenLink : ObjectBase, INotifyPropertyChanged, ISerializable, ICloneable
    {
        public TPISComponent WT1 { get; set; }
        public TPISComponent WT2 { get; set; }
        public int wt1_no { get; set; }
        public int wt2_no { get; set; }

        #region binding通知
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

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public HiddenLink(TPISComponent wt1, TPISComponent wt2)
        {
            WT1 = wt1;
            wt1_no = wt1.No;
            WT2 = wt2;
            wt2_no = wt2.No;
            isSelected = false;
        }

        public override object Clone()
        {
            return null;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }

        public HiddenLink(SerializationInfo info, StreamingContext context)
        {
        }
    }
}

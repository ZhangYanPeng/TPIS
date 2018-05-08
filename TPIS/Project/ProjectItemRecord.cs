using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TPIS.Model;

namespace TPIS.Project
{
    public partial class ProjectItem : INotifyPropertyChanged, ISerializable
    {
        #region 撤销 和 重做
        public void Undo()
        {
            Record record = Records.PopUndo();
            //撤销record
            if (record != null)
            {
                UndoByRecord(record);
            }
        }

        private void UndoByRecord(Record record)
        {
            switch (record.Param["Operation"])
            {
                case "VerticalReverse":
                    {
                        SelectByNo(record.ObjectsNo);
                        VerticalReversedSelection(false);
                        break;
                    }
                case "HorizentalReverse":
                    {
                        SelectByNo(record.ObjectsNo);
                        HorizentalReversedSelection(false);
                        break;
                    }
                case "Rotate":
                    {
                        SelectByNo(record.ObjectsNo);
                        int n = int.Parse(record.Param["angle"]);
                        RotateSelection(4-n,false);
                        break;
                    }
                case "SizeChange":
                    {
                        break;
                    }
                case "Move":
                    {
                        break;
                    }
                case "AddComponent":
                    {
                        break;
                    }
                case "AddLine":
                    {
                        break;
                    }
                case "Delete":
                    {
                        break;
                    }
                case "Paste":
                    {
                        break;
                    }
            }
        }

        private void SelectByNo(List<int> objectsNo)
        {
            if(objectsNo.Count > 1)
            {
                List<TPISComponent> cl = new List<TPISComponent>();
                foreach(ObjectBase obj in Objects)
                {
                    if(obj is TPISComponent && objectsNo.Contains(obj.No))
                    {
                        cl.Add(obj as TPISComponent);
                    }
                }
                this.Select(cl);
                return;
            }
            else if(objectsNo.Count == 1)
            {
                foreach (ObjectBase obj in Objects)
                {
                    if ( obj.No == objectsNo[0])
                    {
                        this.Select(obj);
                        return;
                    }
                }
            }
        }

        public void Redo()
        {
            Record record = Records.PopRedo();
            //撤销record
            if (record != null)
            {
                RedoByRecord(record);
            }
        }

        private void RedoByRecord(Record record)
        {
            switch (record.Param["Operation"])
            {
                case "VerticalReverse":
                    {
                        SelectByNo(record.ObjectsNo);
                        VerticalReversedSelection(false);
                        break;
                    }
                case "HorizentalReverse":
                    {
                        SelectByNo(record.ObjectsNo);
                        HorizentalReversedSelection(false);
                        break;
                    }
                case "Rotate":
                    {
                        SelectByNo(record.ObjectsNo);
                        int n = int.Parse(record.Param["angle"]);
                        RotateSelection(n, false);
                        break;
                    }
                case "SizeChange":
                    {
                        break;
                    }
                case "Move":
                    {
                        break;
                    }
                case "AddComponent":
                    {
                        break;
                    }
                case "AddLine":
                    {
                        break;
                    }
                case "Delete":
                    {
                        break;
                    }
                case "Paste":
                    {
                        break;
                    }
            }
        }
        #endregion
    }
}

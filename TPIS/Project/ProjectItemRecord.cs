﻿using System;
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
                        SelectByNo(record.ObjectsNo);
                        int np = record.ObjectsNo[0];
                        double? width = -ParseDoubleNull(record.Param["width"])*Rate;
                        double? height = -ParseDoubleNull(record.Param["height"]) * Rate;
                        double? x = -ParseDoubleNull(record.Param["x"]) * Rate;
                        double? y = -ParseDoubleNull(record.Param["y"]) * Rate;
                        SizeChange(np, width, height,x, y, false);
                        break;
                    }
                case "Move":
                    {
                        SelectByNo(record.ObjectsNo);
                        int np = record.ObjectsNo[0];
                        double x = -ParseDouble(record.Param["x"]) * Rate;
                        double y = -ParseDouble(record.Param["y"]) * Rate;
                        MoveSelection(x, y, false);
                        break;
                    }
                case "AddComponent":
                    {
                        SelectByNo(record.ObjectsNo);
                        Records.RedoStack[Records.RedoStack.Count-1].Objects = DeleteSelection(false);
                        break;
                    }
                case "AddLine":
                    {
                        SelectByNo(record.ObjectsNo);
                        Records.RedoStack[Records.RedoStack.Count - 1].Objects = DeleteSelection(false);
                        break;
                    }
                case "Delete":
                    {
                        foreach (ObjectBase obj in record.Objects)
                        {
                            Objects.Add(obj);
                            if (obj is TPISLine)
                            {
                                TPISLine line = obj as TPISLine;
                                line.inPort.link = line;
                                if (line.inPort.Type == Model.Common.NodType.Undef)
                                {
                                    line.inPort.Type = Model.Common.NodType.DefOut;
                                }
                                line.outPort.link = line;
                                if (line.outPort.Type == Model.Common.NodType.Undef)
                                {
                                    line.outPort.Type = Model.Common.NodType.DefIn;
                                }
                            }
                        }
                        break;
                    }
                case "Paste":
                    {
                        SelectByNo(record.ObjectsNo);
                        Records.RedoStack[Records.RedoStack.Count - 1].Objects = DeleteSelection(false);
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
                        SelectByNo(record.ObjectsNo);
                        int np = record.ObjectsNo[0];
                        double? width = ParseDoubleNull(record.Param["width"]) * Rate;
                        double? height = ParseDoubleNull(record.Param["height"]) * Rate;
                        double? x = ParseDoubleNull(record.Param["x"]) * Rate;
                        double? y = ParseDoubleNull(record.Param["y"]) * Rate;
                        SizeChange(np, width, height, x, y, false);
                        break;
                    }
                case "Move":
                    {
                        SelectByNo(record.ObjectsNo);
                        int np = record.ObjectsNo[0];
                        double x = -ParseDouble(record.Param["x"]) * Rate;
                        double y = -ParseDouble(record.Param["y"]) * Rate;
                        MoveSelection(x, y, false);
                        break;
                    }
                case "AddComponent":
                    {
                        foreach(ObjectBase obj in record.Objects){
                            Objects.Add(obj);
                            if(obj is TPISLine)
                            {
                                TPISLine line = obj as TPISLine;
                                line.inPort.link = line;
                                if(line.inPort.Type == Model.Common.NodType.Undef)
                                {
                                    line.inPort.Type = Model.Common.NodType.DefOut;
                                }
                                line.outPort.link = line;
                                if (line.outPort.Type == Model.Common.NodType.Undef)
                                {
                                    line.outPort.Type = Model.Common.NodType.DefIn;
                                }
                            }
                        }
                        break;
                    }
                case "AddLine":
                    {
                        foreach (ObjectBase obj in record.Objects)
                        {
                            Objects.Add(obj);
                            if (obj is TPISLine)
                            {
                                TPISLine line = obj as TPISLine;
                                line.inPort.link = line;
                                if (line.inPort.Type == Model.Common.NodType.Undef)
                                {
                                    line.inPort.Type = Model.Common.NodType.DefOut;
                                }
                                line.outPort.link = line;
                                if (line.outPort.Type == Model.Common.NodType.Undef)
                                {
                                    line.outPort.Type = Model.Common.NodType.DefIn;
                                }
                            }
                        }
                        break;
                    }
                case "Delete":
                    {
                        List<TPISComponent> deleteContent = new List<TPISComponent>();
                        foreach(ObjectBase obj in record.Objects)
                        {
                            if(obj is TPISComponent)
                            {
                                deleteContent.Add(obj as TPISComponent);
                            }
                        }
                        Select(deleteContent);
                        DeleteSelection(false);
                        break;
                    }
                case "Paste":
                    {
                        foreach (ObjectBase obj in record.Objects)
                        {
                            Objects.Add(obj);
                            if (obj is TPISLine)
                            {
                                TPISLine line = obj as TPISLine;
                                line.inPort.link = line;
                                if (line.inPort.Type == Model.Common.NodType.Undef)
                                {
                                    line.inPort.Type = Model.Common.NodType.DefOut;
                                }
                                line.outPort.link = line;
                                if (line.outPort.Type == Model.Common.NodType.Undef)
                                {
                                    line.outPort.Type = Model.Common.NodType.DefIn;
                                }
                            }
                        }
                        break;
                    }
            }
        }
        #endregion


        internal double? ParseDoubleNull(string str)
        {
            if (str == "null")
            {
                return new double?();
            }
            else
            {
                return new double?(double.Parse(str));
            }
        }

        internal double ParseDouble(string str)
        {
            if (str == "null")
            {
                return 0;
            }
            else
            {
                return double.Parse(str);
            }
        }
    }
}

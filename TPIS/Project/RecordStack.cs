using System;
using System.Collections.Generic;

namespace TPIS.Project
{
    public class RecordStack
    {
        public static int MAX_SIZE = 50;
        public List<Record> UndoStack { get; set; }
        public List<Record> RedoStack { get; set; }

        public RecordStack()
        {
            UndoStack = new List<Record>();
            RedoStack = new List<Record>();
        }

        public void Push(Record record)
        {
            if (UndoStack.Count > 0 && record.Param["Operation"] == "SizeChange" && UndoStack[UndoStack.Count - 1].Param["Operation"] == "SizeChange")
            {
                if (record.ObjectsNo.Count == UndoStack[UndoStack.Count - 1].ObjectsNo.Count)
                {
                    if (UndoStack[UndoStack.Count - 1].ObjectsNo[0] != record.ObjectsNo[0])
                    {
                        UndoStack.Add(record);
                        RedoStack.Clear();
                        return;
                    }
                    UndoStack[UndoStack.Count - 1].Param["width"] = MergeValue(record.Param["width"], UndoStack[UndoStack.Count - 1].Param["width"]);
                    UndoStack[UndoStack.Count - 1].Param["height"] = MergeValue(record.Param["height"], UndoStack[UndoStack.Count - 1].Param["height"]);
                    UndoStack[UndoStack.Count - 1].Param["x"] = MergeValue(record.Param["x"], UndoStack[UndoStack.Count - 1].Param["x"]);
                    UndoStack[UndoStack.Count - 1].Param["y"] = MergeValue(record.Param["y"], UndoStack[UndoStack.Count - 1].Param["y"]);

                }
            }
            else if (UndoStack.Count > 0 && record.Param["Operation"] == "Move" && UndoStack[UndoStack.Count - 1].Param["Operation"] == "Move")
            {
                if (record.ObjectsNo.Count == UndoStack[UndoStack.Count - 1].ObjectsNo.Count)
                {
                    foreach (int no in record.ObjectsNo)
                    {
                        if (!UndoStack[UndoStack.Count - 1].ObjectsNo.Contains(no))
                        {
                            UndoStack.Add(record);
                            RedoStack.Clear();
                            return;
                        }
                    }
                    UndoStack[UndoStack.Count - 1].Param["x"] = MergeValue(record.Param["x"], UndoStack[UndoStack.Count - 1].Param["x"]);
                    UndoStack[UndoStack.Count - 1].Param["y"] = MergeValue(record.Param["y"], UndoStack[UndoStack.Count - 1].Param["y"]);
                }
            }
            else
            {
                UndoStack.Add(record);
                RedoStack.Clear();
            }
            ReSize();
        }

        private void ReSize()
        {
            while (UndoStack.Count > MAX_SIZE)
            {
                UndoStack.RemoveAt(0);
            } while (RedoStack.Count > MAX_SIZE)
            {
                RedoStack.RemoveAt(0);
            }
        }

        public Record PopUndo()
        {
            if (UndoStack.Count == 0)
                return null;
            Record record = UndoStack[UndoStack.Count - 1];
            UndoStack.Remove(record);
            RedoStack.Add(record);
            return record;
        }

        public Record PopRedo()
        {
            if (RedoStack.Count == 0)
                return null;
            Record record = RedoStack[RedoStack.Count - 1];
            RedoStack.Remove(record);
            UndoStack.Add(record);
            return record;
        }

        public double? ParseDouble(string str)
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

        internal string MergeValue(string str1, string str2)
        {

            if (str1 == "null")
            {
                return str2;
            }
            else if (str2 == "null")
            {
                return str1;
            }
            else
            {
                return (double.Parse(str1) + double.Parse(str2)).ToString();
            }
        }
    }

    public class Record
    {
        public List<ObjectBase> Objects { get; set; }
        public Dictionary<string, string> Param { get; set; }
        public List<int> ObjectsNo { get; set; }

        public Record()
        {
            Objects = new List<ObjectBase>();
            Param = new Dictionary<string, string>();
            ObjectsNo = new List<int>();
        }
    }
}
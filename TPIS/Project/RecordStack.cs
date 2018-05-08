using System.Collections.Generic;

namespace TPIS.Project
{
    public class RecordStack
    {
        List<Record> UndoStack { get; set; }
        List<Record> RedoStack { get; set; }

        public RecordStack()
        {
            UndoStack = new List<Record>();
            RedoStack = new List<Record>();
        }

        public void Push( Record record )
        {
            UndoStack.Add(record);
            RedoStack.Clear();
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
    }

    public class Record
    {
        public List<ObjectBase> Objects { get; set; }
        public Dictionary<string,string> Param { get; set; }
        public List<int> ObjectsNo { get; set; }

        public Record()
        {
            Objects = new List<ObjectBase>();
            Param = new Dictionary<string, string>();
            ObjectsNo = new List<int>();
        }
    }
}
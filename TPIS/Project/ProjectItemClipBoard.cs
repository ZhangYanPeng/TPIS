using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TPIS.Model;
using TPIS.Model.Common;

namespace TPIS.Project
{
    public partial class ProjectItem : INotifyPropertyChanged, ISerializable
    {
        public ClipBoard clipBoard { get; set; }
        public ObservableCollection<ObjectBase> CopyObjects { get; set; }

        #region 选择后续操作:复制、粘贴、删除、剪切
        //复制
        public void CopySelection()
        {
            CopyObjects = new ObservableCollection<ObjectBase>();
            if (CopyObjects != null)
            {
                foreach (ObjectBase obj in CopyObjects)
                {
                    CopyObjects.Remove(obj);
                }
            }
            clipBoard.Objects = new List<ObjectBase>();
            List<TPISComponent> sl = new List<TPISComponent>();
            List<TPISText> texts = new List<TPISText>();
            foreach (ObjectBase obj in Objects)
            {
                if (obj.isSelected && obj is TPISComponent)
                {
                    sl.Add(obj as TPISComponent);
                }
                if (obj.isSelected && obj is TPISText)
                {
                    texts.Add((TPISText)obj);
                }
            }
            Select(sl, texts);

            foreach (ObjectBase obj in Objects)
            {
                if (obj.isSelected)
                {
                    CopyObjects.Add(obj.Clone() as ObjectBase);//剪贴板对象集用于计算复制控件的有效边界
                    clipBoard.Objects.Add(obj.Clone() as ObjectBase);
                }
            }
        }

        //删除
        public List<ObjectBase> DeleteSelection(bool record = true)
        {
            List<ObjectBase> DeleteContent = new List<ObjectBase>();
            Record rec = new Record();
            rec.Param.Add("Operation", "Delete");
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent && obj.isSelected)
                {
                    foreach (Port p in ((TPISComponent)obj).Ports)
                    {
                        if (p.link != null && Objects.Contains(p.link))
                            p.link.IsSelected = true;
                    }
                }
                if (obj is HiddenLink && ( (obj as HiddenLink).WT1.IsSelected || (obj as HiddenLink).WT2.IsSelected))
                {
                    obj .isSelected = true;
                }
            }
            for (int i = 0; i < Objects.Count;)
            {
                ObjectBase obj = Objects[i];
                if (obj.isSelected)
                {
                    if (obj is TPISLine)//去TPISLine的锚点
                        ((TPISLine)obj).IsSelected = false;
                    Objects.Remove(obj);
                    rec.Objects.Add(obj);
                    DeleteContent.Add(obj);
                }
                else
                {
                    i++;
                }
            }
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is TPISComponent)
                {
                    foreach (Port p in ((TPISComponent)obj).Ports)
                    {
                        if (p.link == null || !Objects.Contains(p.link))
                        {
                            p.link = null;
                            if (p.Type == NodType.DefIn || p.Type == NodType.DefOut)
                                p.Type = NodType.Undef;
                        }
                    }
                }
            }
            if (record && rec.Objects.Count > 0)
                Records.Push(rec);
            return DeleteContent;
        }

        //粘贴
        public void PasteSelection(double x, double y, bool record = true)
        {
            if (clipBoard.Objects.Count <= 0)
                return;
            Record rec = new Record();
            rec.Param.Add("Operation", "Paste");
            double offset_x = x - this.WorkSpaceSize_LU(CopyObjects).X;
            double offset_y = y - this.WorkSpaceSize_LU(CopyObjects).Y;

            //按偏移量粘贴并选中
            Dictionary<int, int> NoMap = new Dictionary<int, int>();
            foreach (ObjectBase obj in clipBoard.Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = (obj as TPISComponent).Clone() as TPISComponent;
                    component.SetRate(Rate);
                    int n = 0;
                    foreach (ObjectBase objc in this.Objects)
                    {
                        if (objc.No > n)
                            n = objc.No;
                    }
                    n++;
                    NoMap.Add(component.No, n);
                    component.No = n;
                    component.PosChange((int)offset_x, (int)offset_y);
                    Objects.Add(component);
                    rec.ObjectsNo.Add(component.No);
                }
                if (obj is TPISLine)
                {
                    TPISLine line = obj.Clone() as TPISLine;
                    line.SetRate(Rate);
                    int n = 0;
                    foreach (ObjectBase objc in Objects)
                    {
                        if (objc.No < n)
                            n = objc.No;
                    }
                    n--;
                    NoMap.Add(line.No, n);
                    line.No = n;
                    line.PosChange((int)offset_x, (int)offset_y);
                    Objects.Add(line);
                    rec.ObjectsNo.Add(line.No);
                }
                if (obj is ResultCross)
                {
                    ResultCross cross = obj.Clone() as ResultCross;
                    cross.SetRate(Rate);
                    int n = 0;
                    foreach (ObjectBase objc in Objects)
                    {
                        if (objc.No < n)
                            n = objc.No;
                    }
                    n--;
                    NoMap.Add(cross.No, n);
                    cross.No = n;
                    cross.PosChange((int)offset_x, (int)offset_y);
                    Objects.Add(cross);
                    rec.ObjectsNo.Add(cross.No);
                }
            }

            //更改所有no
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    if (NoMap.ContainsValue(obj.No))
                    {
                        TPISComponent component = obj as TPISComponent;
                        foreach (Port port in component.Ports)
                        {
                            if (port.LinkNo <= 0)
                            {
                                if (NoMap.ContainsKey(port.LinkNo))
                                    port.LinkNo = NoMap[port.LinkNo];
                                else
                                    port.LinkNo = 1;
                            }
                            if (port.CrossNo <= 0)
                            {
                                if (NoMap.ContainsKey(port.CrossNo))
                                    port.CrossNo = NoMap[port.CrossNo];
                                else
                                    port.CrossNo = 1;
                            }
                        }
                    }
                }
            }

            RebuildLink();

            if (record && rec.ObjectsNo.Count > 0)
                Records.Push(rec);
        }

        //剪切
        public void CutSelection()
        {
            CopySelection();
            DeleteSelection();
        }

        #endregion
    }
}

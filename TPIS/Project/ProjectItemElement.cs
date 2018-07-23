using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TPIS.Model;
using TPIS.Model.Common;
using TPIS.Views.Tool;

namespace TPIS.Project
{
    public partial class ProjectItem : INotifyPropertyChanged, ISerializable
    {
        #region 元件操作
        //添加元件
        public void AddComponent(int tx, int ty, int width, int height, ComponentType ct, bool record = true)
        {
            Record rec = new Record();
            rec.Param.Add("Operation", "AddComponent");
            int n = 0;
            foreach (ObjectBase obj in this.Objects)
            {
                if (obj.No > n)
                    n = obj.No;
            }
            n++;
            TPISComponent component = new TPISComponent(n, Rate, tx, ty, width, height, ct);
            if (this.GridThickness == 0)
                component.IsGrid = false;
            else
                component.IsGrid = true;
            if (component.eleType == TPISNet.EleType.WaterTag)
            {
                for (string pn = "A"; ; pn = IncreasePN(pn))
                {
                    int sum = 0;
                    foreach (ObjectBase obj in Objects)
                    {
                        if (obj is TPISComponent && ((TPISComponent)obj).eleType == TPISNet.EleType.WaterTag && ((TPISComponent)obj).PairName == pn)
                            sum++;
                    }
                    if (sum < 2)
                    {
                        component.PairName = pn;
                        break;
                    }
                }
            }
            else
            {
                component.PairName = "";
            }
            if (GridThickness != 0)
                component.isGrid = true;
            else
                component.isGrid = false;
            component.GridForm();
            Objects.Add(component);
            rec.ObjectsNo.Add(n);
            if (record)
                Records.Push(rec);
        }
        internal string IncreasePN(string pn)
        {
            string result = "";
            int en = 1;
            for (int i = pn.Length - 1; i >= 0; i--)
            {
                if (en == 0)
                {
                    char c = pn[i];
                    result = c + result;
                    en = 0;
                }
                else
                {
                    char c = (char)(pn[i] + 1);
                    if (c > 'Z')
                    {
                        c = 'A';
                        en = 1;
                    }
                    else
                    {
                        en = 0;
                    }
                    result = c + result;
                }
            }
            if (en == 1)
            {
                result = 'A' + result;
            }
            return result;
        }

        //元件移动-边界限制
        public void MoveChange(double x, double y)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            Point tmp;
            tmp = this.WorkSpaceSize_LU(mainwin.GetCurrentProject().Objects);
            if (tmp.X < 0 && tmp.Y < 0)
            {//限制超出左上边界
                this.MoveSelection(1, 1);
            }
            else if (tmp.X < 0)
            {//限制超出左边界
                this.MoveSelection(1, y);
            }
            else if (tmp.Y < 0)
            {//限制超出上边界
                this.MoveSelection(x, 1);
            }
            else
            {
                this.MoveSelection(x, y);
            }
            ChangeWorkSpaceSize();//移动控件时，超过边界自动改变画布大小
        }

        //MovePort
        public void MovePort(Port p)
        {
            PortLocation plw = new PortLocation(p);
            plw.Owner = Application.Current.MainWindow;
            plw.Show();
        }
        #endregion

        #region 线条操作
        //添加线
        public void AddLine(TPISLine line, bool record = true)
        {
            Record rec = new Record();
            rec.Param.Add("Operation", "AddLine");
            int n = 0;
            foreach (ObjectBase obj in this.Objects)
            {
                if (obj.No < n)
                    n = obj.No;
            }
            n--;
            line.No = n;
            if (GridThickness != 0)
                line.isGrid = true;
            else
                line.isGrid = false;
            line.LineThickness = LineThickness;
            Objects.Add(line);
            rec.ObjectsNo.Add(n);
            if (record)
                Records.Push(rec);
        }

        //线锚点移动-边界限制
        public void LineAnchorPointsMoveChange(TPISLine line, Point endPoint, int LineAnchorPointID, object sender)
        {
            if (endPoint.X - 10 < 0 && endPoint.Y - 10 < 0)
            {
                endPoint.X = 10;
                endPoint.Y = 10;
            }
            else if (endPoint.X - 10 < 0)
                endPoint.X = 10;
            else if (endPoint.Y - 10 < 0)
                endPoint.Y = 10;
            line.PointTo(LineAnchorPointID + 1, endPoint);
            ChangeWorkSpaceSize();//移动控件时，超过边界自动改变画布大小
        }
        #endregion

        #region 文字操作
        public void AmpText(bool record = true)
        {
            Record rec = new Record();
            String origin = "";
            String current = "";
            foreach (ObjectBase obj in Objects)
            {
                if(obj is TPISText) {
                    TPISText text = obj as TPISText;
                    if (text.IsSelected)
                    {
                        double cf = FonsSizeTransfor(text.FontSize, true);
                        if (cf > 0)
                        {
                            origin += text.FontSize + "||";
                            text.FontSize = cf;
                            rec.ObjectsNo.Add(text.No);
                            current += text.FontSize + "||";
                        }
                    }
                }
            }
            if (record && rec.Objects.Count != 0)
            {
                rec.Param.Add("origin", origin);
                rec.Param.Add("current", current);
                Records.Push(rec);
            }
        }


        public void ShkText(bool record = true)
        {
            Record rec = new Record();
            String origin = "";
            String current = "";
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISText)
                {
                    TPISText text = obj as TPISText;
                    if (text.IsSelected)
                    {
                        double cf = FonsSizeTransfor(text.FontSize, false);
                        if (cf > 0)
                        {
                            origin += text.FontSize + "||";
                            text.FontSize = cf;
                            rec.ObjectsNo.Add(text.No);
                            current += text.FontSize + "||";
                        }
                    }
                }
            }
            if (record && rec.Objects.Count != 0)
            {
                rec.Param.Add("origin", origin);
                rec.Param.Add("current", current);
                Records.Push(rec);
            }
        }

        public void ToSizeText(double size, bool record = true)
        {
            Record rec = new Record();
            String origin = "";
            String current = "";

            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISText)
                {
                    TPISText text = obj as TPISText;
                    if (text.IsSelected)
                    {
                        origin += text.FontSize + "||";
                        text.FontSize = size;
                        rec.ObjectsNo.Add(text.No);
                        current += text.FontSize + "||";
                    }
                }
            }
            if (record && rec.Objects.Count != 0)
            {
                rec.Param.Add("origin", origin);
                rec.Param.Add("current", current);
                Records.Push(rec);
            }
        }

        public static double[] FONSIZE = { 5, 5.5, 6.5, 7.5, 8, 9, 10, 10.5, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };

        internal double FonsSizeTransfor(double cs, bool amp)
        {
            for (int i = 0; i < FONSIZE.Length; i++)
            {
                if (cs == FONSIZE[i])
                {
                    if (amp)
                    {
                        if (i + 1 < FONSIZE.Length)
                            return FONSIZE[i + 1];
                        else
                            return -1;
                    }
                    if (!amp)
                    {
                        if (i > 0)
                            return FONSIZE[i - 1];
                        else
                            return -1;
                    }
                }
            }
            return -1;
        }

        internal void UpdateText()
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            double fsize = -1;
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISText && obj.isSelected)
                {
                    TPISText text = (TPISText)obj;
                    if (fsize < 0)
                        fsize = text.fontSize;
                    else
                    {
                        if (fsize != text.fontSize)
                        {
                            mainwin.SetToolBarFontSize(-1);
                            return;
                        }
                    }
                }
            }
            for (int i = 0; i < FONSIZE.Length; i++)
            {
                if (FONSIZE[i] == fsize)
                {
                    mainwin.SetToolBarFontSize(i);
                    return;
                }
            }
            mainwin.SetToolBarFontSize(-1);
        }
        #endregion

        #region cross操作 
        public void AddCross(Port port, Point? point = null)
        {
            int no = 0;
            foreach (ObjectBase obj in Objects)
            {
                if (obj.No <= no)
                {
                    no = obj.No - 1;
                }
            }
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = obj as TPISComponent;
                    if (component.Ports.Contains(port))
                    {
                        if( point == null) {
                            double vx, vy;
                            for (double d = 20; ; d += 10)
                            {
                                for (int angle = 0; angle < 360; angle += 5)
                                {
                                    vx = component.Position.V_x + component.Position.V_width / 2 + d * Math.Sin(angle);
                                    vy = component.Position.V_y + component.Position.V_height / 2 + d * Math.Cos(angle);
                                    if (!CoverOrNot(vx, vy, CrossSize.WIDTH * Rate, CrossSize.HEIGHT * Rate))
                                    {
                                        Objects.Add(new ResultCross(port, no, Rate, vx , vy));
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Objects.Add(new ResultCross(port, no, Rate, point.Value.X + 45 / Rate, point.Value.Y - 40 / Rate));
                            return;
                        }
                    }
                }
            }
        }


        public void AddText(int vx, int vy, double fontsize)
        {
            int no = 0;
            foreach (ObjectBase obj in Objects)
            {
                if (obj.No <= no)
                {
                    no = obj.No - 1;
                }
            }
            Objects.Add(new TPISText(fontsize, no, Rate, vx, vy));
        }

        public bool CoverOrNot(double x, double y, double w, double h)
        {
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = obj as TPISComponent;
                    if (WithinOrNot(x, y, w, h, component.Position.V_x - 5, component.Position.V_y - 5, component.Position.V_width + 5, component.Position.V_height + 5))
                        return true;
                }
                if (obj is ResultCross)
                {
                    ResultCross cross = obj as ResultCross;
                    if (WithinOrNot(x, y, w, h, cross.Position.V_x - 5, cross.Position.V_y - 5, cross.Position.V_width + 5, cross.Position.V_height + 5))
                        return true;
                }
            }
            return false;
        }

        internal bool WithinOrNot(double x1, double y1, double w1, double h1, double x2, double y2, double w2, double h2)
        {
            if (x1 + w1 > x2 && x2 + w2 > x1 && y1 + h1 > y2 && y2 + h2 > y1)
                return true;
            else
                return false;

        }

        public void RemoveCross(int no)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                ObjectBase obj = Objects[i];
                if (obj is ResultCross && obj.No == no)
                {
                    Objects.Remove(obj);
                    return;
                }
            }
        }
        #endregion

    }
}

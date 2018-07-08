using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TPIS.Model;
using TPIS.Model.Common;

namespace TPIS.Project
{
    public partial class ProjectItem : INotifyPropertyChanged, ISerializable
    {
        #region 视图标志
        public bool IsViewsMouseEnter { get; set; }
        public bool IsViewWindowsOpen { get; set; }
        #endregion

        #region 画布网格缩放
        public double gridUintLength;
        public double GridUintLength
        {
            get { return gridUintLength; }
            set
            {
                gridUintLength = value;
                OnPropertyChanged("GridUintLength");
            }
        }
        #endregion

        #region 画布网格线粗（0：关闭网格线；1：打开网格线）
        public double gridThickness;
        public double GridThickness
        {
            get { return gridThickness; }
            set
            {
                gridThickness = value;
                foreach (ObjectBase obj in Objects)
                {
                    if (value != 0)
                        obj.isGrid = true;
                }
                OnPropertyChanged("GridThickness");
            }
        }
        #endregion

        #region 画布背景色
        public Brush backGroundColor;
        public Brush BackGroundColor
        {
            get { return backGroundColor; }
            set
            {
                backGroundColor = value;
                OnPropertyChanged("BackGroundColor");
            }
        }
        #endregion
        
        #region 网格绘制
        public void DrawGridSelection()
        {
            if (GridThickness == 0)
                GridThickness = 0.2;
            else
                GridThickness = 0;
        }
        #endregion

        #region 有效工作区大小-右下点
        public Point WorkSpaceSize_RD(ObservableCollection<ObjectBase> SelectObjects)
        {
            Point p = new Point(0, 0);//无控件，边界为0×0
            for (int i = 0; i < SelectObjects.Count; i++)
            {
                int x = (int)this.Canvas.Width;
                int y = (int)this.Canvas.Height;
                ObjectBase obj = SelectObjects[i];
                if (obj is TPISComponent)
                {
                    x = (int)(((TPISComponent)obj).Position.X + ((TPISComponent)obj).Position.Width + 10);
                    y = (int)(((TPISComponent)obj).Position.Y + ((TPISComponent)obj).Position.Height + 10);
                }
                else if (obj is TPISLine)
                {
                    x = (int)((TPISLine)obj).Points[0].X + 10;
                    y = (int)((TPISLine)obj).Points[0].Y + 10;
                    foreach (Point p1 in ((TPISLine)obj).Points)
                    {
                        x = (int)(x > p1.X ? x : p1.X + 10);
                        y = (int)(y > p1.Y ? y : p1.Y + 10);
                    }
                    {//拖动控件、缩放画布问题
                        x = (int)(x / this.Rate);
                        y = (int)(y / this.Rate);
                    }
                }
                else if (obj is ResultCross)
                {
                    x = (int)(((ResultCross)obj).Position.X + ((ResultCross)obj).Position.Width + 10);
                    y = (int)(((ResultCross)obj).Position.Y + ((ResultCross)obj).Position.Height + 10);
                }
                else if (obj is TPISText)
                {
                    x = (int)(((TPISText)obj).Position.X + ((TPISText)obj).Position.Width + 10);
                    y = (int)(((TPISText)obj).Position.Y + ((TPISText)obj).Position.Height + 10);
                }
                p.X = p.X > x ? p.X : x;
                p.Y = p.Y > y ? p.Y : y;
            }
            return p;
        }
        #endregion

        #region 有效工作区大小-左上点
        public Point WorkSpaceSize_LU(ObservableCollection<ObjectBase> SelectObjects)
        {
            Point p = new Point(0, 0);//无控件，边界为0×0

            if (SelectObjects.Count > 0)
            {
                int x = 0;
                int y = 0;
                ObjectBase obj0 = SelectObjects[0];
                if (obj0 is TPISComponent)
                {
                    x = (int)((TPISComponent)obj0).Position.X;
                    y = (int)((TPISComponent)obj0).Position.Y;
                }
                else if (obj0 is TPISLine)
                {
                    x = (int)((TPISLine)obj0).Points[0].X;
                    y = (int)((TPISLine)obj0).Points[0].Y;
                    foreach (Point p1 in ((TPISLine)obj0).Points)
                    {
                        x = (int)(x < p1.X ? x : p1.X);
                        y = (int)(y < p1.Y ? y : p1.Y);
                    }
                }
                p.X = x;
                p.Y = y;
                for (int i = 0; i < SelectObjects.Count; i++)
                {
                    ObjectBase obj = SelectObjects[i];
                    if (obj is TPISComponent)
                    {
                        x = (int)((TPISComponent)obj).Position.X;
                        y = (int)((TPISComponent)obj).Position.Y;
                    }
                    else if (obj is TPISLine)
                    {
                        x = (int)((TPISLine)obj).Points[0].X;
                        y = (int)((TPISLine)obj).Points[0].Y;
                        foreach (Point p1 in ((TPISLine)obj).Points)
                        {
                            x = (int)(x < p1.X ? x : p1.X);
                            y = (int)(y < p1.Y ? y : p1.Y);
                        }
                    }
                    p.X = p.X < x ? p.X : x;
                    p.Y = p.Y < y ? p.Y : y;
                }
            }
            return p;
        }
        #endregion

        #region 有效工作区大小-中心点
        public Point WorkSpaceSize_Center(ObservableCollection<ObjectBase> SelectObjects)
        {
            Point p1 = WorkSpaceSize_RD(SelectObjects);
            Point p2 = WorkSpaceSize_LU(SelectObjects);
            Point p = new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
            return p;
        }
        #endregion

        #region 有效工作区大小(宽高)
        public Point WorkSpaceSize_Size(ObservableCollection<ObjectBase> SelectObjects)
        {
            Point p1 = WorkSpaceSize_RD(SelectObjects);
            Point p2 = WorkSpaceSize_LU(SelectObjects);
            Point p = new Point((p1.X - p2.X) + 100, (p1.Y - p2.Y) + 100);
            return p;
        }
        #endregion

        #region 控件拖动放大工作区
        private void ChangeWorkSpaceSize()
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            ScrollViewer scrollViewer = mainwin.SelectScrollViewer();
            Point p = new Point();
            p = this.WorkSpaceSize_RD(mainwin.GetCurrentProject().Objects);
            if (p.X >= this.Canvas.Width)
            {
                this.Canvas.Width = (int)p.X + 10;
                scrollViewer.ScrollToRightEnd();
            }
            if (p.Y >= this.Canvas.Height)
            {
                this.Canvas.Height = (int)p.Y + 10;
                scrollViewer.ScrollToBottom();
            }
            mainwin.CurWorkspaceSizeShow(this.Canvas.Width.ToString(), this.Canvas.Height.ToString());//状态栏显示工作区大小
        }
        #endregion



        #region 查找元件并选中居中
        internal bool FindComponent(int tn)
        {
            MainWindow mainwin = (MainWindow)System.Windows.Application.Current.MainWindow;
            Boolean findOrNot = false;
            foreach (ObjectBase obj in Objects)
            {
                if (obj is TPISComponent)
                {
                    if (obj.No == tn)
                    {
                        findOrNot = true;
                        ((TPISComponent)obj).IsSelected = true;
                        ScrollViewer sv = mainwin.SelectScrollViewer();
                        //移动坐标
                        sv.ScrollToHorizontalOffset(((TPISComponent)obj).Position.V_x - sv.ActualWidth / 2 + ((TPISComponent)obj).Position.V_width / 2);
                        sv.ScrollToVerticalOffset(((TPISComponent)obj).Position.V_y - sv.ActualHeight / 2 + ((TPISComponent)obj).Position.V_height / 2);
                    }
                    else
                    {
                        ((TPISComponent)obj).IsSelected = false;
                    }
                }
            }
            return findOrNot;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TPIS.Model;

namespace TPIS.TPISCanvas
{
    public partial class ProjectDesignerCanvas : Canvas
    {
        private Image AddComponentImage { get; set; }

        /// <summary>
        /// 判定画布内鼠标形状
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CanvasMouseEnter(object sender, MouseEventArgs e)
        {
            //ReInitLineAnchorPoints();
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            try
            {
                if (mainwin.GetCurrentProject().Canvas.Operation != Project.OperationType.SELECT)
                {
                    this.Cursor = Cursors.Cross;

                    if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.ADD_COMPONENT)
                    {
                        int type = mainwin.GetCurrentProject().Canvas.OperationParam["type"];
                        ComponentType targetType = null;
                        foreach (BaseType bt in mainwin.TypeList)
                        {
                            foreach (ComponentType ct in bt.ComponentTypeList)
                            {
                                if (type == ct.Id)
                                {
                                    targetType = ct;
                                    break;
                                }
                            }
                        }
                        AddComponentImage = new Image();
                        AddComponentImage.Source = new BitmapImage(new Uri(targetType.PicPath, UriKind.RelativeOrAbsolute));
                        AddComponentImage.Width = targetType.Width;
                        AddComponentImage.Height = targetType.Height;
                        Children.Add(AddComponentImage);
                    }
                }
                else
                    this.Cursor = Cursors.Arrow;
            }
            catch
            {
                this.Cursor = Cursors.Cross;
            }
        }

        /// <summary>
        /// 离开画布恢复形状
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CanvasMouseLeave(object sender, MouseEventArgs e)
        {
            mainwin.Canvas_MousePosition("0", "0");//状态栏显示工作区鼠标坐标
            this.Cursor = Cursors.Arrow;
            Children.Remove(AddComponentImage);
        }

        public void ComponentMouseLButtonDown(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.ADD_COMPONENT)
            {
                // in case that this click is the start of a drag operation we cache the start point
                Point sp = e.GetPosition(this);
                int type = mainwin.GetCurrentProject().Canvas.OperationParam["type"];
                ComponentType targetType = null;
                foreach (BaseType bt in mainwin.TypeList)
                {
                    foreach (ComponentType ct in bt.ComponentTypeList)
                    {
                        if (type == ct.Id)
                        {
                            targetType = ct;
                            break;
                        }
                    }
                }
                mainwin.GetCurrentProject().AddComponent((int)sp.X, (int)sp.Y, targetType.Width, targetType.Height, targetType);
                e.Handled = true;
            }
        }

        protected void ComponentMouseMove(object sender, MouseEventArgs e)
        {
            //ReInitLineAnchorPoints();//创建锚点
            mainwin.Canvas_MousePosition(((int)((e.GetPosition(this).X) / mainwin.GetCurrentProject().Rate)).ToString(), ((int)((e.GetPosition(this).Y) / mainwin.GetCurrentProject().Rate)).ToString());//状态栏显示工作区鼠标坐标
            //ChangeWorkSpaceSize();//移动控件时，超过边界自动改变画布大小
            if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.ADD_COMPONENT)
            {

                if (mainwin.GetCurrentProject().Canvas.Operation == Project.OperationType.ADD_COMPONENT)
                {
                    Point pos = e.GetPosition(this);
                    AddComponentImage.SetValue(Canvas.LeftProperty, pos.X);
                    AddComponentImage.SetValue(Canvas.TopProperty, pos.Y);
                }
                e.Handled = true;
            }
        }
    }
}

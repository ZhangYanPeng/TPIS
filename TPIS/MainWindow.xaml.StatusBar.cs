using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace TPIS
{
    partial class MainWindow : Window
    {
        #region 状态栏显示工作区大小
        public void CurWorkspaceSizeShow(string x, string y)
        {
            foreach (object item in this.TPISStatusBar.Items)
            {
                if (((StatusBarItem)item).Name == "CurCanvasSize")
                {
                    ((StatusBarItem)item).ToolTip = "工作区：" + x + "×" + y;
                    if (((StatusBarItem)item).Content is TextBlock)
                    {
                        if (((TextBlock)((StatusBarItem)item).Content).Name == "CurCanvasSizeTB")
                        {
                            ((TextBlock)((StatusBarItem)item).Content).Text = "工作区大小：" + x + "×" + y;
                        }
                    }
                }
            }
        }
        #endregion

        #region 状态栏显示工作区鼠标坐标
        public void Canvas_MousePosition(string x, string y)
        {
            foreach (object item in this.TPISStatusBar.Items)
            {
                if (((StatusBarItem)item).Name == "CurCanvasPosition")
                {
                    if (((StatusBarItem)item).Content is TextBlock)
                    {
                        if (((TextBlock)((StatusBarItem)item).Content).Name == "CurCanvasPositionTB")
                        {
                            ((TextBlock)((StatusBarItem)item).Content).Text = "当前坐标：" + "(" + x + "," + y + ")";
                        }
                    }
                }
            }
        }
        #endregion

        #region 状态栏显示当前工程
        public void CurProjectShow(string name)
        {
            foreach (object item in this.TPISStatusBar.Items)
            {
                if (((StatusBarItem)item).Name == "CurProject")
                {
                    ((StatusBarItem)item).ToolTip = "工程：" + name;
                    if (((StatusBarItem)item).Content is TextBlock)
                    {
                        if (((TextBlock)((StatusBarItem)item).Content).Name == "CurProjectTB")
                        {
                            ((TextBlock)((StatusBarItem)item).Content).Text = "当前工程：" + name;
                        }
                    }
                }
            }
        }
        #endregion

        #region 状态栏显示当前工程路径
        public void CurProjectAddressShow(string path, string address)
        {
            foreach (object item in this.TPISStatusBar.Items)
            {
                if (((StatusBarItem)item).Name == "CurProjectAddres")
                {
                    ((StatusBarItem)item).ToolTip = "路径："+path;
                    if (((StatusBarItem)item).Content is TextBlock)
                    {
                        if (((TextBlock)((StatusBarItem)item).Content).Name == "CurProjectAddresTB")
                        {
                            ((TextBlock)((StatusBarItem)item).Content).Text = "当前工程路径：" + address;
                        }
                    }
                }
            }
        }
        #endregion
    }
}

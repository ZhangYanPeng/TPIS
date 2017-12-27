using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TPIS.Model;

namespace TPIS.TPISCanvas
{
    class DesignerComponent : Canvas
    {
        bool isDragDropInEffect = false;
        Point pos = new Point();
        Point location = new Point();

        public DesignerComponent()
        {
            base.AddHandler(Button.MouseLeftButtonDownEvent, new MouseButtonEventHandler(Element_MouseLeftButtonDown), true);
            base.AddHandler(Button.MouseMoveEvent, new MouseEventHandler(Element_MouseMove), true);
            base.AddHandler(Button.MouseLeftButtonUpEvent, new MouseButtonEventHandler(Element_MouseLeftButtonUp), true);
            base.MouseMove += new MouseEventHandler(Element_MouseMove);
            base.MouseLeftButtonDown += new MouseButtonEventHandler(Element_MouseLeftButtonDown);
            base.MouseLeftButtonUp += new MouseButtonEventHandler(Element_MouseLeftButtonUp);
        }

        void Element_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragDropInEffect)
            {
                FrameworkElement currEle = sender as FrameworkElement;
                double xPos = e.GetPosition(null).X - pos.X + location.X;
                double yPos = e.GetPosition(null).Y - pos.Y + location.Y;
                ((TPISComponent)currEle.DataContext).Position.V_x = (int)xPos;
                ((TPISComponent)currEle.DataContext).Position.V_y = (int)yPos;
                ((TPISComponent)currEle.DataContext).Position.Width = ((TPISComponent)currEle.DataContext).Position.Width +(int) (e.GetPosition(null).X - pos.X);

            }
        }

        void Element_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            FrameworkElement fEle = sender as FrameworkElement;
            isDragDropInEffect = true;
            pos = e.GetPosition(null);
            fEle.CaptureMouse();
            fEle.Cursor = Cursors.Hand;
            location.X = ((TPISComponent)fEle.DataContext).Position.V_x;
            location.Y = ((TPISComponent)fEle.DataContext).Position.V_y;
        }

        void Element_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragDropInEffect)
            {
                FrameworkElement ele = sender as FrameworkElement;
                isDragDropInEffect = false;
                ele.ReleaseMouseCapture();
            }
        }
    }
}

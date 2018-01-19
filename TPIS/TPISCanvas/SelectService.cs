using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace TPIS.TPISCanvas
{
    public partial class ProjectDesignerCanvas : Canvas
    {
        public Point? SelectStartPoint { get; set; }
        public void MouseLBtnClickEmpty(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.Operation != Project.OperationType.SELECT)
            {
                return;
            }
            mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Select();
            this.SelectStartPoint = new Point?(e.GetPosition(this));

            // if you click directly on the canvas all selected items are 'de-selected'
            Focus();
            e.Handled = true;
        }

        public void MouseLBtnSelectMove(object sender, MouseEventArgs e)
        {
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            if (mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Canvas.Operation == Project.OperationType.SELECT)
            {
                // if mouse button is not pressed we have no drag operation, ...
                if (e.LeftButton != MouseButtonState.Pressed)
                    SelectStartPoint = null;

                // ... but if mouse button is pressed and start
                // point value is set we do have one
                if (this.SelectStartPoint.HasValue)
                {
                    // create rubberband adorner
                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                    if (adornerLayer != null)
                    {
                        SelectAdorner adorner = new SelectAdorner(this, SelectStartPoint);
                        if (adorner != null)
                        {
                            adornerLayer.Add(adorner);
                        }
                    }
                }
                e.Handled = true;
            }
        }
    }


}

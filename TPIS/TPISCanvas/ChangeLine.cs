using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Project;
using TPIS.TPISCanvas;

namespace TPIS.TPISCanvas
{
    public partial class ChangeLine
    {
        public void LineSelect(object sender, MouseButtonEventArgs e)
        {
            Polyline polyLine = sender as Polyline;
            TPISLine line = new TPISLine();
            line = (TPISLine)polyLine.DataContext;
            MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
            mainwin.ProjectList.projects[mainwin.CurrentPojectIndex].Select((ObjectBase)polyLine.DataContext);
            e.Handled = true;
        }

    }
}

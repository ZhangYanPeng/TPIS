using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using TPIS.Model.Common;

namespace TPIS.Views.Modules
{
    partial class PropertyTemplate : ResourceDictionary
    {
        private void OpenCurveWindow_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            CurvesData.FittingWin ftw = new CurvesData.FittingWin(btn.DataContext as CurvesData.Curves);
            ftw.Owner = Application.Current.MainWindow;
            ftw.ShowDialog();
        }
    }

}

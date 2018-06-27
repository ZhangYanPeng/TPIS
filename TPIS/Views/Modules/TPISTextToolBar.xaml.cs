using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace TPIS.Views.Modules
{
    partial class TPISTextToolBar : ResourceDictionary
    {

        

        /// <summary>
        /// 插入文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TPISTextSelected(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton currEle = sender as ToggleButton;
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                if (mainwin.GetCurrentProject() != null)
                {
                    mainwin.TPISTextSelected(sender,e) ;
                    AddText.IsChecked = true;
                }
                else
                {
                    currEle.IsChecked = false;
                }
            }
            catch
            {
                return;
            }
        }

        private void TPISTextAmp(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton currEle = sender as ToggleButton;
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                if (mainwin.GetCurrentProject() != null)
                {
                    mainwin.GetCurrentProject().AmpText(true);
                }
                else
                {
                    currEle.IsChecked = false;
                }
            }
            catch
            {
                return;
            }
        }



        private void TPISTextShk(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton currEle = sender as ToggleButton;
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                if (mainwin.GetCurrentProject() != null)
                {
                    mainwin.GetCurrentProject().ShkText(true);
                }
                else
                {
                    currEle.IsChecked = false;
                }
            }
            catch
            {
                return;
            }
        }

        private void ToFontSize(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton currEle = sender as ToggleButton;
                MainWindow mainwin = (MainWindow)Application.Current.MainWindow;
                if (mainwin.GetCurrentProject() != null)
                {
                    if (Fonsize.SelectedIndex == -1)
                        return;
                    ComboBoxItem si = (ComboBoxItem)Fonsize.SelectedItem;
                    mainwin.GetCurrentProject().ToSizeText(double.Parse(si.Content.ToString()),true);
                }
                else
                {
                    currEle.IsChecked = false;
                }
            }
            catch
            {
                return;
            }
        }
    }
}

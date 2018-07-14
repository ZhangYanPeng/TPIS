using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TPIS.Model;
using TPIS.Model.Common;
using TPIS.Project;

namespace TPIS.Views
{
    /// <summary>
    /// QuickModeSelect.xaml 的交互逻辑
    /// </summary>
    public partial class QuickModeSelect : Window
    {
        List<TPISComponent> DesignComponents;
        List<TPISComponent> CalComponents;
        List<TPISComponent> InterComponents;

        public QuickModeSelect(ProjectItem pi)
        {
            InitializeComponent();

            this.Owner = (MainWindow)Application.Current.MainWindow;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Topmost = true;
            DesignComponents = new List<TPISComponent>();
            CalComponents = new List<TPISComponent>();
            InterComponents = new List<TPISComponent>();
            foreach (ObjectBase obj in pi.Objects)
            {
                if (obj is TPISComponent)
                {
                    TPISComponent component = obj as TPISComponent;
                    if (component.Mode.Contains(SelMode.DesignMode))
                        DesignComponents.Add(component);
                    if (component.Mode.Contains(SelMode.CalMode))
                        CalComponents.Add(component);
                    if (component.Mode.Contains(SelMode.InterMode))
                        InterComponents.Add(component);
                }
            }
            if (DesignComponents.Count > 0)
                this.DesignModeList.DataContext = DesignComponents;
            else
                DesignModeList.Visibility = Visibility.Collapsed;
            if (CalComponents.Count > 0)
                this.CalModeList.DataContext = CalComponents;
            else
                CalModeList.Visibility = Visibility.Collapsed;
            if (InterComponents.Count > 0)
                this.InterModeList.DataContext = InterComponents;
            else
                InterModeList.Visibility = Visibility.Collapsed;
            InitTotalCheck();
        }

        private void TotalChecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            checkBox.IsChecked = true;
            SelMode sm = new SelMode();
            switch (checkBox.Tag)
            {
                case "Design": sm = SelMode.DesignMode; break;
                case "Cal": sm = SelMode.CalMode; break;
                case "Inter": sm = SelMode.InterMode; break;
            }
            foreach (TPISComponent c in DesignComponents)
            {
                for (int i = 0; i < c.Mode.Count; i++)
                {
                    if (c.Mode[i] == sm)
                    {
                        c.SelectedMode = i;
                        break;
                    }
                }
            }
            InitTotalCheck();
        }

        #region 整体的勾选
        private void InitTotalCheck()
        {
            DesignCheckBox.IsChecked = TotalChecked(DesignComponents, SelMode.DesignMode);
            CalCheckBox.IsChecked = TotalChecked(CalComponents, SelMode.CalMode);
            InterCheckBox.IsChecked = TotalChecked(InterComponents, SelMode.InterMode);
        }

        private bool? TotalChecked(List<TPISComponent> list, SelMode mode)
        {
            if (list.Count == 0)
                return false;
            else
            {
                int i = 0;
                foreach (TPISComponent c in list)
                {
                    if (c.Mode[c.selectedMode] == mode)
                        i++;
                }
                if (i == 0)
                    return false;
                else if (i == list.Count)
                    return true;
                else
                    return null;
            }

        }
        #endregion

        private void ItemCheck(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            checkBox.IsChecked = true;
            SelMode sm = new SelMode();
            switch (checkBox.Tag)
            {
                case "Design": sm = SelMode.DesignMode; break;
                case "Cal": sm = SelMode.CalMode; break;
                case "Inter": sm = SelMode.InterMode; break;
            }
            TPISComponent c = checkBox.DataContext as TPISComponent;
            for (int i = 0; i < c.Mode.Count; i++)
            {
                if (c.Mode[i] == sm)
                {
                    c.SelectedMode = i;
                    break;
                }
            }
            InitTotalCheck();
        }
    }

    #region 单个显示的勾选
    public class DMCheckConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return false;
            else
            {
                ObservableCollection<SelMode> modes = values[0] as ObservableCollection<SelMode>;
                int sel = (int)values[1];
                if (modes[sel] == SelMode.DesignMode)
                    return true;
                else
                    return false;
            }
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class CMCheckConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return false;
            else
            {
                ObservableCollection<SelMode> modes = values[0] as ObservableCollection<SelMode>;
                int sel = (int)values[1];
                if (modes[sel] == SelMode.CalMode)
                    return true;
                else
                    return false;
            }
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class IMCheckConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return false;
            else
            {
                ObservableCollection<SelMode> modes = values[0] as ObservableCollection<SelMode>;
                int sel = (int)values[1];
                if (modes[sel] == SelMode.InterMode)
                    return true;
                else
                    return false;
            }
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    #endregion
}

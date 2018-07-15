using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using TPIS.Model.Common;

namespace TPIS.Views.Modules
{
    //控制显示样式
    public class ValVisualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Visible;
            else
            {
                if ((P_Type)value != P_Type.ToSelect && (P_Type)value != P_Type.ToLine)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
        
    }

    //控制显示样式
    public class MeasureVisualConverter : IMultiValueConverter
    {
        object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return Visibility.Visible;
            else
            {
                if ((P_Type)values[1] == P_Type.ToLine)
                    return Visibility.Collapsed;
                string[] tmp = (string[])values[0];
                if (tmp.Count<String>() == 1 && tmp[0] == "")
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //控制显示样式
    public class LineVisualConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Visible;
            else
            {
                if ((P_Type)value == P_Type.ToLine)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    //颜色控制
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Brushes.LightYellow;
            else
            {
                TPISNet.PColor color = (TPISNet.PColor)value;
                switch (color)
                {
                    case TPISNet.PColor.Weak: return Brushes.LightYellow;
                    case TPISNet.PColor.Whatever: return Brushes.PaleTurquoise;
                    case TPISNet.PColor.Super: return Brushes.DarkOrange;
                }
            }
            return Brushes.LightYellow;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }



    //控制显示样式
    public class MaterialConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                TPISNet.Material material = (TPISNet.Material)value;
                switch (material)
                {
                    case TPISNet.Material.air: return "工质类型：空气";
                    case TPISNet.Material.ash: return "工质类型：灰";
                    case TPISNet.Material.coal: return "工质类型：煤";
                    case TPISNet.Material.fluegas: return "工质类型：排烟";
                    case TPISNet.Material.gas: return "工质类型：气体";
                    case TPISNet.Material.NA: return "工质类型：未定义";
                    case TPISNet.Material.oil: return "工质类型：油";
                    case TPISNet.Material.power: return "工质类型：功率";
                    case TPISNet.Material.water: return "工质类型：汽水";

                }
            }
            return "工质类型：未定义";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    //线性
    public class LineTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return new DoubleCollection() { 1, 0 };
            else
            {
                bool s = (bool)value;
                if (s)
                    return new DoubleCollection() { 2, 3 };
                else
                    return new DoubleCollection() { 1, 0 };
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }


    public class SelectConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return DependencyProperty.UnsetValue;
            if ((bool)value)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                Visibility visibility = (Visibility)value;
                if (visibility == Visibility.Visible)
                    return true;
                else
                    return false;
            }
            return DependencyProperty.UnsetValue;
        }
    }
}

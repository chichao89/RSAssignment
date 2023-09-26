using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RSAssignment.ViewModels
{
    public class CheckStateToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CheckState state)
            {
                return state == CheckState.Checked ? true :
                       state == CheckState.Unchecked ? false :
                       DependencyProperty.UnsetValue;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked)
            {
                return isChecked ? CheckState.Checked : CheckState.Unchecked;
            }
            return CheckState.Unchecked;
        }

    }
}
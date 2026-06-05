using Microsoft.UI.Xaml.Data; 
using System;

namespace Frontend.Views
{
    public class StringToDecimalConverter : Windows.UI.Xaml.Data.IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is decimal number)
            {
                return number == 0 ? string.Empty : number.ToString("0.##");
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string text && decimal.TryParse(text, out decimal result))
            {
                return result;
            }
            return 0m;
        }
    }
}
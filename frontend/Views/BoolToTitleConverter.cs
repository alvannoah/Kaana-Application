using Microsoft.UI.Xaml.Data;
using System;
using Windows.UI.Xaml.Data;

namespace Frontend.Views
{
    public class BoolToTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isEditMode && isEditMode)
            {
                return "Edit Record";
            }

            return "Add Record";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
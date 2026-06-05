using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Frontend.Views
{

    public class BoolToStatusTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isClosed)
            {
                return isClosed ? "Closed" : "Active Running";
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToStatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isClosed)
            {
                string component = parameter as string ?? "Bg";

                if (isClosed)
                {

                    return component == "Bg"
                        ? new SolidColorBrush(Color.FromArgb(255, 238, 238, 238))
                        : new SolidColorBrush(Color.FromArgb(255, 117, 117, 117));
                }
                else
                {

                    return component == "Bg"
                        ? new SolidColorBrush(Color.FromArgb(255, 232, 245, 233))
                        : new SolidColorBrush(Color.FromArgb(255, 46, 125, 50));
                }
            }

            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            bool invert = parameter as string == "Invert";


            bool isNull = value == null;

            if (invert)
            {

                return isNull ? Visibility.Visible : Visibility.Collapsed;
            }


            return isNull ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool boolValue)
            {
                bool invert = parameter as string == "Invert";

                if (invert)
                {
                    return boolValue ? Visibility.Collapsed : Visibility.Visible;
                }
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
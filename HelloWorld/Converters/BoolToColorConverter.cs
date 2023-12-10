using System;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace HelloWorld.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool isEven && isEven)
            {
                return new SolidColorBrush(Colors.Orange);
            }

            return new SolidColorBrush(Colors.Yellow);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
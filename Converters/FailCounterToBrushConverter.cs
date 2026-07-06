using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Pinger.Converters;

public class FailCounterToBrushConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int failCounter)
        {
            if (failCounter == 0)
                return Brushes.LightGreen;

            if (failCounter < 3)
                return Brushes.Khaki;

            return Brushes.LightCoral;
        }

        return Brushes.Transparent;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
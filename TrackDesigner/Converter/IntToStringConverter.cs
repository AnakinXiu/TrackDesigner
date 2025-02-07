using System.Globalization;
using System.Windows.Data;

namespace TrackDesigner.Converter;

public class IntToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is int ? value.ToString() : string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string str && int.TryParse(str, out var result))
            return result;

        return 0;
    }
}
using System;
using System.Data;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Avalonia.DeskApp.Converters;

public class DateTimeConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var datetime = (DateTime)value;
        DateTimeFormatInfo fmt = (new CultureInfo("en-us")).DateTimeFormat;
        return datetime.ToString("ddd, MMMM dd",fmt);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
using System.Globalization;

namespace Mythfall.Client.Converters;

/// <summary>
/// Converts HpPercent (0.0–1.0) to a width for HP bar (max 50).
/// </summary>
public class HpBarWidthConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is double percent)
            return Math.Max(0, percent * 50);
        return 0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}


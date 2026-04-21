using System.Globalization;

namespace Mythfall.Client.Converters;

/// <summary>
/// Returns 0.3 opacity if defeated (true), 1.0 if alive (false).
/// </summary>
public class DefeatedToOpacityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool defeated && defeated)
            return 0.3;
        return 1.0;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}


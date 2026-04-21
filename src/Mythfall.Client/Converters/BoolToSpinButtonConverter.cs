using System.Globalization;

namespace Mythfall.Client.Converters;

public class BoolToSpinButtonConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool canSpin && canSpin)
            return Color.FromArgb("#2a4a2a");
        return Color.FromArgb("#1a2a1a");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


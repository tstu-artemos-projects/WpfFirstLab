using System.Globalization;
using CodingSeb.Localization;
using System.Windows.Data;
using System.Windows.Media;
using WpfFirstLab.Resources;

namespace WpfFirstLab.Converters;

/// <summary>
/// Число → строка «42 %» (ConverterParameter — максимум шкалы).
/// </summary>
public sealed class PercentConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var max = double.TryParse(parameter as string, NumberStyles.Float, CultureInfo.InvariantCulture, out var m) ? m : 100;
        return value is double v ? $"{v / max * 100:0} %" : string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

/// <summary>
/// Число 0–100 → кисть цветовой роли: ошибка, информация или успех.
/// </summary>
public sealed class ValueToBrushConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var v = value is double d ? d : 0;
        return v < 33 ? Palette.Error.Brush : v < 66 ? Palette.Info.Brush : Palette.Success.Brush;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

/// <summary>
/// Фамилия + имя → «Фамилия Имя» (MultiBinding).
/// </summary>
public sealed class FullNameConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var parts = values.OfType<string>().Where(s => !string.IsNullOrWhiteSpace(s));
        return string.Join(" ", parts);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

public sealed class LocalizedTextConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var key = parameter as string ?? string.Empty;
        return Loc.Tr(key, key);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

public sealed class LocalizedFormatConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var key = parameter as string ?? string.Empty;
        var format = Loc.Tr(key, key);
        var args = values.Length > 1 ? values.Skip(1).ToArray() : Array.Empty<object>();
        return string.Format(culture, format, args);
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

public sealed class LocalizedValueConverter : IMultiValueConverter
{
    private const string NullSentinel = "__NULL__";
    private const string MissingSentinel = "__MISSING__";

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        var key = parameter as string ?? string.Empty;
        var value = values.Length > 1 ? values[1] : null;
        return value switch
        {
            null => Loc.Tr(key, key),
            string s when s is NullSentinel or MissingSentinel => Loc.Tr(key, key),
            _ => value,
        };
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

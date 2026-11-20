using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using WpfFirstLab.Resources;

namespace WpfFirstLab.Converters;

/// <summary>Число → строка «42 %» (ConverterParameter — максимум шкалы).</summary>
public sealed class PercentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var max = double.TryParse(parameter as string, NumberStyles.Float, CultureInfo.InvariantCulture, out var m) ? m : 100;
        return value is double v ? $"{v / max * 100:0} %" : string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

/// <summary>Число 0–100 → кисть цветовой роли: ошибка, информация или успех.</summary>
public sealed class ValueToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var v = value is double d ? d : 0;
        return v < 33 ? Palette.Error.Brush : v < 66 ? Palette.Info.Brush : Palette.Success.Brush;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotSupportedException();
}

/// <summary>Фамилия + имя → «Фамилия Имя» (MultiBinding).</summary>
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

using System.ComponentModel;
using CodingSeb.Localization;
using System.Windows.Media;

namespace WpfFirstLab.Resources;

/// <summary>Один оттенок цвета в палитре (строка таблицы).</summary>
public sealed class ColorShade : INotifyPropertyChanged
{
    private readonly string _familyKey;

    public ColorShade(string familyKey, string label, string hex, string rgb, Brush brush)
    {
        _familyKey = familyKey;
        Label = label;
        Hex = hex;
        Rgb = rgb;
        Brush = brush;
        Loc.Instance.PropertyChanged += OnLocPropertyChanged;
    }

    public string Name => $"{Loc.Tr(_familyKey, _familyKey)} {Label}";
    public string Label { get; }
    public string Hex { get; }
    public string Rgb { get; }
    public Brush Brush { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnLocPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Loc.CurrentLanguage))
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
    }
}

/// <summary>
/// Цветовая роль приложения (основной, акцентный, успех, ошибка, информация)
/// и 10 её оттенков: 50, 100, 200 … 900.
/// </summary>
public sealed class ColorRole : INotifyPropertyChanged
{
    private readonly string _titleKey;
    private readonly string _familyKey;

    public ColorRole(string titleKey, string familyKey, string[] hexShades)
    {
        _titleKey = titleKey;
        _familyKey = familyKey;
        Loc.Instance.PropertyChanged += OnLocPropertyChanged;

        var shades = new List<ColorShade>(hexShades.Length);
        for (var i = 0; i < hexShades.Length; i++)
        {
            var color = (Color)ColorConverter.ConvertFromString(hexShades[i]);
            var brush = new SolidColorBrush(color);
            brush.Freeze();

            var label = i == 0 ? "50" : (i * 100).ToString();
            shades.Add(new ColorShade(
                familyKey,
                label,
                hexShades[i].ToUpperInvariant(),
                $"{color.R}, {color.G}, {color.B}",
                brush));
        }

        Shades = shades;
    }

    public string Title => Loc.Tr(_titleKey, _titleKey);
    public string Family => Loc.Tr(_familyKey, _familyKey);
    public IReadOnlyList<ColorShade> Shades { get; }
    public string TableTitle => $"{Title} — {Family}";

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>Рабочий оттенок роли (600).</summary>
    public ColorShade Main => Shades[6];

    public Brush Brush => Main.Brush;
    public Brush LightBrush => Shades[1].Brush;   // 100 — светлая подложка
    public Brush DarkBrush => Shades[8].Brush;    // 800 — тёмный текст на светлой подложке
    public string Hex => Main.Hex;
    public string Rgb => $"RGB({Main.Rgb})";

    private void OnLocPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(Loc.CurrentLanguage)) return;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Family)));
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TableTitle)));
    }
}

/// <summary>Палитра светлой темы приложения.</summary>
public static class Palette
{
    /// <summary>Название текущей темы (выбор темы в приложении без логики — тема фиксирована).</summary>
    public static string ThemeName => Loc.Tr("ThemeLight", "Светлая");

    public static ColorRole Primary { get; } = new("PaletteColorPrimary", "PaletteFamilyBlue", new[]
    {
        "#EFF6FF", "#DBEAFE", "#BFDBFE", "#93C5FD", "#60A5FA",
        "#3B82F6", "#2563EB", "#1D4ED8", "#1E40AF", "#1E3A8A",
    });

    public static ColorRole Accent { get; } = new("PaletteColorAccent", "PaletteFamilyPurple", new[]
    {
        "#F5F3FF", "#EDE9FE", "#DDD6FE", "#C4B5FD", "#A78BFA",
        "#8B5CF6", "#7C3AED", "#6D28D9", "#5B21B6", "#4C1D95",
    });

    public static ColorRole Success { get; } = new("PaletteColorSuccess", "PaletteFamilyGreen", new[]
    {
        "#F0FDF4", "#DCFCE7", "#BBF7D0", "#86EFAC", "#4ADE80",
        "#22C55E", "#16A34A", "#15803D", "#166534", "#14532D",
    });

    public static ColorRole Error { get; } = new("PaletteColorError", "PaletteFamilyRed", new[]
    {
        "#FEF2F2", "#FEE2E2", "#FECACA", "#FCA5A5", "#F87171",
        "#EF4444", "#DC2626", "#B91C1C", "#991B1B", "#7F1D1D",
    });

    public static ColorRole Info { get; } = new("PaletteColorInfo", "PaletteFamilyTeal", new[]
    {
        "#ECFEFF", "#CFFAFE", "#A5F3FC", "#67E8F9", "#22D3EE",
        "#06B6D4", "#0891B2", "#0E7490", "#155E75", "#164E63",
    });

    public static IReadOnlyList<ColorRole> BaseRoles { get; } = new[] { Primary, Accent };
    public static IReadOnlyList<ColorRole> StatusRoles { get; } = new[] { Success, Error, Info };
    public static IReadOnlyList<ColorRole> Roles { get; } = new[] { Primary, Accent, Success, Error, Info };
}

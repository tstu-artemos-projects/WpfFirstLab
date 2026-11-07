using System.Windows.Media;

namespace WpfFirstLab;

/// <summary>Один оттенок цвета в палитре (строка таблицы).</summary>
public sealed record ColorShade(string Name, string Hex, string Rgb, Brush Brush);

/// <summary>
/// Цветовая роль приложения (основной, акцентный, успех, ошибка, информация)
/// и 10 её оттенков: 50, 100, 200 … 900.
/// </summary>
public sealed class ColorRole
{
    public ColorRole(string title, string family, string[] hexShades)
    {
        Title = title;
        Family = family;

        var shades = new List<ColorShade>(hexShades.Length);
        for (var i = 0; i < hexShades.Length; i++)
        {
            var color = (Color)ColorConverter.ConvertFromString(hexShades[i]);
            var brush = new SolidColorBrush(color);
            brush.Freeze();

            var label = i == 0 ? "50" : (i * 100).ToString();
            shades.Add(new ColorShade(
                $"{family} {label}",
                hexShades[i].ToUpperInvariant(),
                $"{color.R}, {color.G}, {color.B}",
                brush));
        }

        Shades = shades;
    }

    public string Title { get; }
    public string Family { get; }
    public IReadOnlyList<ColorShade> Shades { get; }

    public string TableTitle => $"{Title} — {Family}";

    /// <summary>Рабочий оттенок роли (600).</summary>
    public ColorShade Main => Shades[6];

    public Brush Brush => Main.Brush;
    public Brush LightBrush => Shades[1].Brush;   // 100 — светлая подложка
    public Brush DarkBrush => Shades[8].Brush;    // 800 — тёмный текст на светлой подложке
    public string Hex => Main.Hex;
    public string Rgb => $"RGB({Main.Rgb})";
}

/// <summary>Палитра светлой темы приложения.</summary>
public static class Palette
{
    /// <summary>Название текущей темы (выбор темы в приложении без логики — тема фиксирована).</summary>
    public static string ThemeName => "Светлая";

    public static ColorRole Primary { get; } = new("Основной", "Синий", new[]
    {
        "#EFF6FF", "#DBEAFE", "#BFDBFE", "#93C5FD", "#60A5FA",
        "#3B82F6", "#2563EB", "#1D4ED8", "#1E40AF", "#1E3A8A",
    });

    public static ColorRole Accent { get; } = new("Акцентный", "Фиолетовый", new[]
    {
        "#F5F3FF", "#EDE9FE", "#DDD6FE", "#C4B5FD", "#A78BFA",
        "#8B5CF6", "#7C3AED", "#6D28D9", "#5B21B6", "#4C1D95",
    });

    public static ColorRole Success { get; } = new("Успех", "Зелёный", new[]
    {
        "#F0FDF4", "#DCFCE7", "#BBF7D0", "#86EFAC", "#4ADE80",
        "#22C55E", "#16A34A", "#15803D", "#166534", "#14532D",
    });

    public static ColorRole Error { get; } = new("Ошибка", "Красный", new[]
    {
        "#FEF2F2", "#FEE2E2", "#FECACA", "#FCA5A5", "#F87171",
        "#EF4444", "#DC2626", "#B91C1C", "#991B1B", "#7F1D1D",
    });

    public static ColorRole Info { get; } = new("Информация", "Бирюзовый", new[]
    {
        "#ECFEFF", "#CFFAFE", "#A5F3FC", "#67E8F9", "#22D3EE",
        "#06B6D4", "#0891B2", "#0E7490", "#155E75", "#164E63",
    });

    public static IReadOnlyList<ColorRole> BaseRoles { get; } = new[] { Primary, Accent };
    public static IReadOnlyList<ColorRole> StatusRoles { get; } = new[] { Success, Error, Info };
    public static IReadOnlyList<ColorRole> Roles { get; } = new[] { Primary, Accent, Success, Error, Info };
}

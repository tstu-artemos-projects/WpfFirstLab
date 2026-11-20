using System.Windows;
using Microsoft.Win32;

namespace WpfFirstLab.Services;

/// <summary>
/// Применяет тему: переключает стандартные элементы WPF на тему Fluent (Application.ThemeMode)
/// и подменяет словарь с кистями Light.xaml / Dark.xaml из библиотеки ресурсов.
/// </summary>
public sealed class ThemeService : IThemeService
{
    private const string Prefix = "pack://application:,,,/WpfFirstLab.Resources;component/Themes/";

    public void Apply(AppTheme theme)
    {
        var app = Application.Current;
        if (app is null) return;

        var dark = theme switch
        {
            AppTheme.Dark => true,
            AppTheme.Light => false,
            _ => !SystemUsesLightTheme(),
        };

        app.ThemeMode = dark ? ThemeMode.Dark : ThemeMode.Light;

        var dictionaries = app.Resources.MergedDictionaries;
        var current = dictionaries.FirstOrDefault(d => d.Source?.OriginalString.StartsWith(Prefix) == true);
        var replacement = new ResourceDictionary { Source = new Uri(Prefix + (dark ? "Dark.xaml" : "Light.xaml")) };

        if (current is null) dictionaries.Insert(0, replacement);
        else dictionaries[dictionaries.IndexOf(current)] = replacement;
    }

    private static bool SystemUsesLightTheme()
    {
        var value = Registry.GetValue(
            @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
            "AppsUseLightTheme", 1);
        return value is not int i || i != 0;
    }
}

using CodingSeb.Localization;
using WpfFirstLab.Infrastructure;
using WpfFirstLab.Services;

namespace WpfFirstLab.ViewModels;

/// <summary>Элемент списка с названием, зависящим от языка: название перечитывается при смене языка.</summary>
public sealed class LocalizedItem<T> : ViewModelBase
{
    private readonly string _key;

    public LocalizedItem(T value, string key)
    {
        Value = value;
        _key = key;
    }

    public T Value { get; }

    /// <summary>Название на текущем языке (Loc.Tr из CodingSeb.Localization).</summary>
    public string Name => Loc.Tr(_key, _key);

    /// <summary>Перечитывает название после смены языка.</summary>
    public void Refresh() => OnPropertyChanged(nameof(Name));
}

/// <summary>Язык интерфейса: код для CodingSeb.Localization («ru», «en») и название на этом языке.</summary>
public sealed record LanguageOption(string Code, string NativeName);

/// <summary>Настройки вкладки «Основное»: тема, язык, шрифт и его размер.</summary>
public sealed class SettingsViewModel : ViewModelBase
{
    private readonly IThemeService _themeService;
    private LocalizedItem<AppTheme> _selectedTheme;
    private LanguageOption _selectedLanguage;
    private string _selectedFont = "Segoe UI";
    private double _fontSize = 14;

    public SettingsViewModel(IThemeService themeService)
    {
        _themeService = themeService;

        Themes = new[]
        {
            new LocalizedItem<AppTheme>(AppTheme.Light, "ThemeLight"),
            new LocalizedItem<AppTheme>(AppTheme.Dark, "ThemeDark"),
            new LocalizedItem<AppTheme>(AppTheme.System, "ThemeSystem"),
        };
        Languages = new[]
        {
            new LanguageOption("ru", "Русский"),
            new LanguageOption("en", "English"),
        };
        Fonts = new[] { "Segoe UI", "Arial", "Times New Roman", "Consolas", "Calibri" };

        _selectedTheme = Themes[0];
        _selectedLanguage = Languages[0];
        _themeService.Apply(_selectedTheme.Value);
    }

    public IReadOnlyList<LocalizedItem<AppTheme>> Themes { get; }
    public IReadOnlyList<LanguageOption> Languages { get; }
    public IReadOnlyList<string> Fonts { get; }

    public LocalizedItem<AppTheme> SelectedTheme
    {
        get => _selectedTheme;
        set
        {
            if (value is null || !SetProperty(ref _selectedTheme, value)) return;
            _themeService.Apply(value.Value);
        }
    }

    public LanguageOption SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            if (value is null || !SetProperty(ref _selectedLanguage, value)) return;
            Loc.Instance.CurrentLanguage = value.Code;
            foreach (var theme in Themes) theme.Refresh();
        }
    }

    public string SelectedFont
    {
        get => _selectedFont;
        set => SetProperty(ref _selectedFont, value ?? "Segoe UI");
    }

    public double FontSize
    {
        get => _fontSize;
        set => SetProperty(ref _fontSize, value);
    }
}

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
        LocalizationService.Instance.PropertyChanged += (_, _) => OnPropertyChanged(nameof(Name));
    }

    public T Value { get; }
    public string Name => LocalizationService.Instance[_key];
}

public sealed record LanguageOption(AppLanguage Language, string NativeName);

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
            new LanguageOption(AppLanguage.Russian, "Русский"),
            new LanguageOption(AppLanguage.English, "English"),
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
            LocalizationService.Instance.Language = value.Language;
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

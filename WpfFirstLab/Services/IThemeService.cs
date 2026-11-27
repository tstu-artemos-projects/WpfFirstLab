namespace WpfFirstLab.Services;

public enum AppTheme { Light, Dark, System }

/// <summary>
/// Сервис смены темы.
/// </summary>
public interface IThemeService
{
    void Apply(AppTheme theme);
}

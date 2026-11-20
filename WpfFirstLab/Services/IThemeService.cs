namespace WpfFirstLab.Services;

public enum AppTheme { Light, Dark, System }

/// <summary>Сервис смены темы. Модель представления работает только с интерфейсом и не знает о WPF.</summary>
public interface IThemeService
{
    void Apply(AppTheme theme);
}

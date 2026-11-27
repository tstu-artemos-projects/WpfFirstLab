using WpfFirstLab.Infrastructure;
using WpfFirstLab.Services;

namespace WpfFirstLab.ViewModels;

/// <summary>
/// Корневая модель представления.
/// По одной дочерней модели на вкладку.
/// </summary>
public sealed class MainViewModel(IThemeService themeService) : ViewModelBase
{
    public SettingsViewModel Settings { get; } = new(themeService);
    public TableViewModel Table { get; } = new();
    public BindingsViewModel Bindings { get; } = new();
}

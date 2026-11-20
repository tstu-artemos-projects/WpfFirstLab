using WpfFirstLab.Infrastructure;
using WpfFirstLab.Services;

namespace WpfFirstLab.ViewModels;

/// <summary>Корневая модель представления: по одной дочерней модели на вкладку.</summary>
public sealed class MainViewModel : ViewModelBase
{
    public MainViewModel(IThemeService themeService)
    {
        Settings = new SettingsViewModel(themeService);
        Table = new TableViewModel();
        Bindings = new BindingsViewModel();
    }

    public SettingsViewModel Settings { get; }
    public TableViewModel Table { get; }
    public BindingsViewModel Bindings { get; }
}

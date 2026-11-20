using System.Windows;
using WpfFirstLab.Services;
using WpfFirstLab.ViewModels;

namespace WpfFirstLab;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        // Единственное место, где связываются представление и модель; сервисы передаются снаружи.
        DataContext = new MainViewModel(new ThemeService());
    }
}

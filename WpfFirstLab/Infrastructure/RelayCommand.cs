using System.Windows.Input;

namespace WpfFirstLab.Infrastructure;

/// <summary>
/// Команда без параметра.
///
/// Делегат выполнения и необязательный делегат доступности.
/// </summary>
public sealed class RelayCommand(Action execute, Func<bool>? canExecute = null) : ICommand
{
    private readonly Action _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    // Доступность команд перепроверяется WPF при каждом действии пользователя.
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter) => canExecute?.Invoke() ?? true;
    public void Execute(object? parameter) => _execute();
}

/// <summary>
/// Команда с параметром типа T (передаётся через CommandParameter).
/// </summary>
public sealed class RelayCommand<T>(Action<T?> execute, Predicate<T?>? canExecute = null) : ICommand
{
    private readonly Action<T?> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter) => canExecute?.Invoke(Cast(parameter)) ?? true;
    public void Execute(object? parameter) => _execute(Cast(parameter));

    private static T? Cast(object? parameter) => parameter is T value ? value : default;
}

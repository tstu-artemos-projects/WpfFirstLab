using System.Windows;
using System.Windows.Input;

namespace WpfFirstLab.Behaviors;

/// <summary>
/// Выполняет команду, когда элемент (или любой его потомок) получает фокус клавиатуры.
/// Позволяет модели представления узнать, какая таблица активна, без обработчиков в code-behind.
/// </summary>
public static class FocusBehavior
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(FocusBehavior),
            new PropertyMetadata(null, OnCommandChanged));

    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(FocusBehavior),
            new PropertyMetadata(null));

    public static ICommand? GetCommand(DependencyObject o) => (ICommand?)o.GetValue(CommandProperty);
    public static void SetCommand(DependencyObject o, ICommand? v) => o.SetValue(CommandProperty, v);
    public static object? GetCommandParameter(DependencyObject o) => o.GetValue(CommandParameterProperty);
    public static void SetCommandParameter(DependencyObject o, object? v) => o.SetValue(CommandParameterProperty, v);

    private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not UIElement element) return;
        element.GotKeyboardFocus -= OnGotKeyboardFocus;
        if (e.NewValue is not null)
            element.GotKeyboardFocus += OnGotKeyboardFocus;
    }

    private static void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        var element = (DependencyObject)sender;
        var command = GetCommand(element);
        var parameter = GetCommandParameter(element);
        if (command?.CanExecute(parameter) == true)
            command.Execute(parameter);
    }
}

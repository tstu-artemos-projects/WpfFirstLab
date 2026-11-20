using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfFirstLab.Infrastructure;

/// <summary>Базовый класс моделей представления: уведомляет интерфейс об изменении свойств (INotifyPropertyChanged).</summary>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    /// <summary>Записывает значение в поле и уведомляет интерфейс, только если значение изменилось.</summary>
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? name = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(name);
        return true;
    }
}

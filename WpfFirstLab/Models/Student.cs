using WpfFirstLab.Infrastructure;

namespace WpfFirstLab.Models;

/// <summary>
/// Запись таблицы.
/// Свойства уведомляют интерфейс, поэтому правка в DataGrid и в форме синхронна.
/// </summary>
public sealed class Student(int id, string name, string group, double score)
    : ViewModelBase
{
    public int Id { get; } = id;

    public string Name
    {
        get;
        set => SetProperty(ref field, value);
    } = name;

    public string Group
    {
        get;
        set => SetProperty(ref field, value);
    } = group;

    public double Score
    {
        get;
        set => SetProperty(ref field, value);
    } = score;

    public Student Clone(int newId) => new(newId, Name, Group, Score);
}

using WpfFirstLab.Infrastructure;

namespace WpfFirstLab.Models;

/// <summary>Запись таблицы. Свойства уведомляют интерфейс, поэтому правка в DataGrid и в форме синхронна.</summary>
public sealed class Student : ViewModelBase
{
    private string _name;
    private string _group;
    private double _score;

    public Student(int id, string name, string group, double score)
    {
        Id = id;
        _name = name;
        _group = group;
        _score = score;
    }

    public int Id { get; }

    public string Name { get => _name; set => SetProperty(ref _name, value); }
    public string Group { get => _group; set => SetProperty(ref _group, value); }
    public double Score { get => _score; set => SetProperty(ref _score, value); }

    public Student Clone(int newId) => new(newId, Name, Group, Score);
}

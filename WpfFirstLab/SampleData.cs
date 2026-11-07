namespace WpfFirstLab;

/// <summary>Строка демонстрационной таблицы DataGrid.</summary>
public sealed record Student(int Id, string Name, string Group, double Score);

/// <summary>Статические данные для демонстрации элементов управления.</summary>
public static class SampleData
{
    public static IReadOnlyList<Student> Students { get; } = new[]
    {
        new Student(1, "Иванов И. И.", "Б.ПИН.ИИ.25.16", 4.5),
        new Student(2, "Петрова А. С.", "Б.ПИН.ИИ.25.16", 4.8),
        new Student(3, "Сидоров П. В.", "Б.ПИН.ИИ.25.15", 3.9),
        new Student(4, "Кузнецова М. Д.", "Б.ПИН.ИИ.25.15", 4.2),
    };
}

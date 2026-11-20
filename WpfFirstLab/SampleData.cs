using WpfFirstLab.Models;

namespace WpfFirstLab;

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

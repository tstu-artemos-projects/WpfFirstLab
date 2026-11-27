using System.Globalization;
using System.IO;
using CodingSeb.Localization;
using CodingSeb.Localization.Loaders;
using System.Windows;
using System.Windows.Markup;

namespace WpfFirstLab;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        // По умолчанию привязки WPF используют культуру en-US, и число «4,5» в русской
        // раскладке не распознаётся. Переключаем язык разметки на культуру системы.
        FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

        // Локализация (CodingSeb.Localization): загрузчик JSON-файлов и каталог с переводами.
        // Выполняется до создания окна, чтобы разметка {Tr 'Ключ'} сразу получила строки.
        LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
        LocalizationLoader.Instance.AddDirectory(Path.Combine(AppContext.BaseDirectory, "Localization"));
        Loc.Instance.CurrentLanguage = "ru";

        base.OnStartup(e);
    }
}

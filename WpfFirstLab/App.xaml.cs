using System.Globalization;
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

        base.OnStartup(e);
    }
}

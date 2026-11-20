using System.ComponentModel;

namespace WpfFirstLab.Services;

public enum AppLanguage { Russian, English }

/// <summary>
/// Простая локализация: строки хранятся в словаре, а XAML обращается к ним через индексатор
/// {Binding [Ключ], Source={x:Static services:LocalizationService.Instance}}.
/// При смене языка вызывается уведомление «Item[]», и все привязки обновляются.
/// </summary>
public sealed class LocalizationService : INotifyPropertyChanged
{
    public static LocalizationService Instance { get; } = new();

    private AppLanguage _language = AppLanguage.Russian;

    private static readonly Dictionary<string, (string Ru, string En)> Strings = new()
    {
        ["TabCommon"] = ("Основное", "General"),
        ["TabPalette"] = ("Цветовая палитра", "Color palette"),
        ["TabControls"] = ("Элементы управления", "Controls"),
        ["TabLayout"] = ("Контейнеры компоновки", "Layout containers"),
        ["TabTable"] = ("Таблицы", "Tables"),
        ["TabBindings"] = ("Привязки", "Bindings"),

        ["GroupPersonalization"] = ("Персонализация", "Personalization"),
        ["GroupFonts"] = ("Шрифты", "Fonts"),
        ["LabelTheme"] = ("Тема приложения:", "Application theme:"),
        ["LabelLanguage"] = ("Язык приложения:", "Application language:"),
        ["LabelFont"] = ("Шрифт приложения:", "Application font:"),
        ["LabelFontSize"] = ("Размер шрифта:", "Font size:"),
        ["ThemeLight"] = ("Светлая", "Light"),
        ["ThemeDark"] = ("Тёмная", "Dark"),
        ["ThemeSystem"] = ("Системная", "System"),

        ["CurrentTheme"] = ("Текущая тема приложения: ", "Current application theme: "),
        ["GroupBaseColors"] = ("Основные цвета", "Base colors"),
        ["GroupStatusColors"] = ("Индикаторные цвета", "Status colors"),
        ["GroupShades"] = ("Оттенки цветов (10 оттенков для каждого цвета)", "Color shades (10 shades per color)"),
        ["ColColor"] = ("Цвет", "Color"),
        ["ColRgb"] = ("RGB-код", "RGB code"),
        ["ColName"] = ("Наименование", "Name"),

        ["ExpButtons"] = ("1. Кнопки", "1. Buttons"),
        ["ExpInputs"] = ("2. Поля ввода и списки", "2. Inputs and lists"),
        ["ExpText"] = ("3. TextBlock и Label", "3. TextBlock and Label"),
        ["ExpTree"] = ("4. TreeView", "4. TreeView"),
        ["ExpGrid"] = ("5. DataGrid", "5. DataGrid"),
        ["ExpProgress"] = ("6. ProgressBar", "6. ProgressBar"),
        ["ExpToolTips"] = ("7. Всплывающие подсказки", "7. Tooltips"),
        ["ExpMenus"] = ("8. Меню и контекстное меню", "8. Menus and context menu"),

        ["TableSource"] = ("Таблица 1 (источник)", "Table 1 (source)"),
        ["TableTarget"] = ("Таблица 2 (приёмник)", "Table 2 (target)"),
        ["Active"] = ("● активна", "● active"),
        ["Add"] = ("Добавить", "Add"),
        ["Update"] = ("Изменить", "Update"),
        ["Delete"] = ("Удалить", "Delete"),
        ["Copy"] = ("Копировать", "Copy"),
        ["Paste"] = ("Вставить", "Paste"),
        ["GroupEditor"] = ("Данные записи", "Record data"),
        ["FieldName"] = ("ФИО:", "Full name:"),
        ["FieldGroup"] = ("Группа:", "Group:"),
        ["FieldScore"] = ("Балл (0–5):", "Score (0–5):"),
        ["ColStudent"] = ("Студент", "Student"),
        ["ColGroup"] = ("Группа", "Group"),
        ["ColScore"] = ("Балл", "Score"),
        ["TableHint"] = ("Щёлкните по таблице, чтобы сделать её активной: кнопки панели инструментов и форма работают с активной таблицей.",
                         "Click a table to make it active: the toolbar buttons and the form work with the active table."),

        ["BindModes"] = ("1. Режимы привязки (Mode)", "1. Binding modes (Mode)"),
        ["BindTriggers"] = ("2. Режимы обновления источника (UpdateSourceTrigger)", "2. Source update modes (UpdateSourceTrigger)"),
        ["BindSources"] = ("3. Источники привязки (ElementName, RelativeSource, Source)", "3. Binding sources (ElementName, RelativeSource, Source)"),
        ["BindConverters"] = ("4. Преобразование и форматирование", "4. Conversion and formatting"),
        ["BindNotify"] = ("5. Уведомление об изменении (INotifyPropertyChanged)", "5. Change notification (INotifyPropertyChanged)"),
        ["BindValidation"] = ("6. Проверка вводимых данных", "6. Input validation"),
    };

    public string this[string key] =>
        Strings.TryGetValue(key, out var pair) ? (_language == AppLanguage.Russian ? pair.Ru : pair.En) : $"[{key}]";

    public AppLanguage Language
    {
        get => _language;
        set
        {
            if (_language == value) return;
            _language = value;
            // «Item[]» — стандартное имя индексатора: привязки вида [Ключ] перечитываются.
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item[]"));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Language)));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
}

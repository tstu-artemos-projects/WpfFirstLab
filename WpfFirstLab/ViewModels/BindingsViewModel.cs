using System.Windows.Input;
using System.Windows.Threading;
using WpfFirstLab.Infrastructure;

namespace WpfFirstLab.ViewModels;

/// <summary>Обычный класс без INotifyPropertyChanged: интерфейс не узнаёт об изменении Value.</summary>
public sealed class PlainModel
{
    public int Value { get; set; }
}

/// <summary>Источник данных для вкладки «Привязки».</summary>
public sealed class BindingsViewModel : ViewModelBase
{
    private string _clock = DateTime.Now.ToString("HH:mm:ss");
    private string _sharedText = "Общий текст";
    private string _sink = "Значение источника";
    private string _defaultModeText = "Режим по умолчанию";
    private string _propertyChangedText = "Привет";
    private string _lostFocusText = "Привет";
    private string _explicitText = "Привет";
    private string _defaultTriggerText = "Привет";
    private string _delayedText = "Привет";
    private string _firstName = "Иван";
    private string _lastName = "Иванов";
    private bool _showDetails = true;
    private int _counter;
    private int _age = 20;

    public BindingsViewModel()
    {
        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        timer.Tick += (_, _) => Clock = DateTime.Now.ToString("HH:mm:ss");
        timer.Start();

        ChangeFromCodeCommand = new RelayCommand(() =>
        {
            SharedText = $"Изменено из модели в {DateTime.Now:HH:mm:ss}";
            DefaultModeText = SharedText;
            PropertyChangedText = LostFocusText = ExplicitText = DefaultTriggerText = DelayedText = "Из модели";
        });
        IncrementCommand = new RelayCommand(() =>
        {
            Counter++;
            Plain.Value++;
        });
    }

    // 1. Режимы привязки
    public string Clock { get => _clock; private set => SetProperty(ref _clock, value); }
    public string SharedText { get => _sharedText; set => SetProperty(ref _sharedText, value); }
    public string Sink { get => _sink; set => SetProperty(ref _sink, value); }
    public string DefaultModeText { get => _defaultModeText; set => SetProperty(ref _defaultModeText, value); }

    // 2. Режимы обновления источника
    public string PropertyChangedText { get => _propertyChangedText; set => SetProperty(ref _propertyChangedText, value); }
    public string LostFocusText { get => _lostFocusText; set => SetProperty(ref _lostFocusText, value); }
    public string ExplicitText { get => _explicitText; set => SetProperty(ref _explicitText, value); }
    public string DefaultTriggerText { get => _defaultTriggerText; set => SetProperty(ref _defaultTriggerText, value); }
    public string DelayedText { get => _delayedText; set => SetProperty(ref _delayedText, value); }

    // 4. Преобразование и форматирование
    public string FirstName { get => _firstName; set => SetProperty(ref _firstName, value); }
    public string LastName { get => _lastName; set => SetProperty(ref _lastName, value); }
    public bool ShowDetails { get => _showDetails; set => SetProperty(ref _showDetails, value); }
    public string? NullableText => null;

    // 5. Уведомление об изменении
    public int Counter { get => _counter; private set => SetProperty(ref _counter, value); }
    public PlainModel Plain { get; } = new();

    // 6. Проверка данных: исключение в set-методе показывается как ошибка привязки
    public int Age
    {
        get => _age;
        set
        {
            if (value is < 0 or > 150)
                throw new ArgumentOutOfRangeException(nameof(value), "Возраст должен быть от 0 до 150.");
            SetProperty(ref _age, value);
        }
    }

    public ICommand ChangeFromCodeCommand { get; }
    public ICommand IncrementCommand { get; }
}

using System.Globalization;
using CodingSeb.Localization;
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
    public BindingsViewModel()
    {
        var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        timer.Tick += (_, _) => Clock = DateTime.Now.ToString("HH:mm:ss");
        timer.Start();

        Loc.Instance.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(Loc.CurrentLanguage)) Refresh();
        };

        ChangeFromCodeCommand = new RelayCommand(() =>
        {
            SharedText = string.Format(CultureInfo.CurrentCulture, Loc.Tr("BindChangedFromModelFormat", "Изменено из модели в {0:HH:mm:ss}"), DateTime.Now);
            DefaultModeText = SharedText;
            PropertyChangedText = LostFocusText = ExplicitText = DefaultTriggerText = DelayedText = Loc.Tr("BindFromModel", "Из модели");
        });
        IncrementCommand = new RelayCommand(() =>
        {
            Counter++;
            Plain.Value++;
        });
    }

    public string Clock
    {
        get;
        private set => SetProperty(ref field, value);
    } = DateTime.Now.ToString("HH:mm:ss");

    public string SharedText
    {
        get;
        set => SetProperty(ref field, value);
    } = Loc.Tr("BindSharedText", "Общий текст");

    public string Sink
    {
        get;
        set => SetProperty(ref field, value);
    } = Loc.Tr("BindSink", "Значение источника");

    public string DefaultModeText
    {
        get;
        set => SetProperty(ref field, value);
    } = Loc.Tr("BindDefaultMode", "Режим по умолчанию");

    public string PropertyChangedText
    {
        get;
        set => SetProperty(ref field, value);
    } = Loc.Tr("BindHello", "Привет");

    public string LostFocusText
    {
        get;
        set => SetProperty(ref field, value);
    } = Loc.Tr("BindHello", "Привет");

    public string ExplicitText
    {
        get;
        set => SetProperty(ref field, value);
    } = Loc.Tr("BindHello", "Привет");

    public string DefaultTriggerText
    {
        get;
        set => SetProperty(ref field, value);
    } = Loc.Tr("BindHello", "Привет");

    public string DelayedText
    {
        get;
        set => SetProperty(ref field, value);
    } = Loc.Tr("BindHello", "Привет");

    public string FirstName
    {
        get;
        set => SetProperty(ref field, value);
    } = Loc.Tr("BindFirstName", "Иван");

    public string LastName
    {
        get;
        set => SetProperty(ref field, value);
    } = Loc.Tr("BindLastName", "Иванов");

    public bool ShowDetails
    {
        get;
        set => SetProperty(ref field, value);
    } = true;

    public string? NullableText => null;

    public int Counter
    {
        get;
        private set => SetProperty(ref field, value);
    }

    public PlainModel Plain { get; } = new();

    public int Age
    {
        get;
        set
        {
            if (value is < 0 or > 150)
                throw new ArgumentOutOfRangeException(nameof(value), Loc.Tr("BindValidationAge", "Возраст должен быть от 0 до 150."));
            SetProperty(ref field, value);
        }
    } = 20;

    public ICommand ChangeFromCodeCommand { get; }
    public ICommand IncrementCommand { get; }

    private void Refresh()
    {
        SharedText = Loc.Tr("BindSharedText", "Общий текст");
        Sink = Loc.Tr("BindSink", "Значение источника");
        DefaultModeText = Loc.Tr("BindDefaultMode", "Режим по умолчанию");
        PropertyChangedText = LostFocusText = ExplicitText = DefaultTriggerText = DelayedText = Loc.Tr("BindHello", "Привет");
        FirstName = Loc.Tr("BindFirstName", "Иван");
        LastName = Loc.Tr("BindLastName", "Иванов");
    }
}

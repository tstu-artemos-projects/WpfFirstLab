using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfFirstLab.Infrastructure;
using WpfFirstLab.Models;

namespace WpfFirstLab.ViewModels;

public enum TableSide { Left, Right }

/// <summary>
/// Вкладка «Таблицы»: две таблицы, форма редактирования записи и команды
/// добавления, изменения, удаления, копирования и вставки.
/// </summary>
public sealed class TableViewModel : ViewModelBase
{
    private int _nextId = 100;
    private Student? _selectedLeft;
    private Student? _selectedRight;
    private TableSide _activeSide = TableSide.Left;
    private Student? _buffer;
    private TableSide _bufferSource;

    public TableViewModel()
    {
        Left = new ObservableCollection<Student>
        {
            new(1, "Иванов И. И.", "Б.ПИН.ИИ.25.16", 4.5),
            new(2, "Петрова А. С.", "Б.ПИН.ИИ.25.16", 4.8),
            new(3, "Сидоров П. В.", "Б.ПИН.ИИ.25.15", 3.9),
            new(4, "Кузнецова М. Д.", "Б.ПИН.ИИ.25.15", 4.2),
        };
        Right = new ObservableCollection<Student>();

        AddCommand = new RelayCommand(Add, CanEdit);
        UpdateCommand = new RelayCommand(UpdateSelected, () => ActiveSelected is not null && CanEdit());
        DeleteCommand = new RelayCommand(DeleteSelected, () => ActiveSelected is not null);
        CopyCommand = new RelayCommand(Copy, () => ActiveSelected is not null);
        PasteCommand = new RelayCommand(Paste, () => _buffer is not null);
        ActivateCommand = new RelayCommand<TableSide>(side => ActiveSide = side);
    }

    public ObservableCollection<Student> Left { get; }
    public ObservableCollection<Student> Right { get; }

    public Student? SelectedLeft
    {
        get => _selectedLeft;
        set
        {
            if (!SetProperty(ref _selectedLeft, value)) return;
            if (_activeSide == TableSide.Left) SelectionChanged();
        }
    }

    public Student? SelectedRight
    {
        get => _selectedRight;
        set
        {
            if (!SetProperty(ref _selectedRight, value)) return;
            if (_activeSide == TableSide.Right) SelectionChanged();
        }
    }

    public TableSide ActiveSide
    {
        get => _activeSide;
        private set
        {
            if (!SetProperty(ref _activeSide, value)) return;
            OnPropertyChanged(nameof(IsLeftActive));
            OnPropertyChanged(nameof(IsRightActive));
            SelectionChanged();
        }
    }

    public bool IsLeftActive => _activeSide == TableSide.Left;
    public bool IsRightActive => _activeSide == TableSide.Right;

    private ObservableCollection<Student> ActiveTable => _activeSide == TableSide.Left ? Left : Right;
    private Student? ActiveSelected => _activeSide == TableSide.Left ? _selectedLeft : _selectedRight;

    // Поля формы
    public string EditName
    {
        get;
        set => SetProperty(ref field, value);
    } = string.Empty;

    public string EditGroup
    {
        get;
        set => SetProperty(ref field, value);
    } = string.Empty;

    public double EditScore
    {
        get;
        set => SetProperty(ref field, value);
    } = 4;

    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand CopyCommand { get; }
    public ICommand PasteCommand { get; }
    public ICommand ActivateCommand { get; }

    private bool CanEdit() =>
        !string.IsNullOrWhiteSpace(EditName) && EditScore is >= 0 and <= 5;

    // При выборе записи в активной таблице её данные попадают в форму.
    private void SelectionChanged()
    {
        if (ActiveSelected is not { } s) return;
        EditName = s.Name;
        EditGroup = s.Group;
        EditScore = s.Score;
    }

    private void Add()
    {
        var item = new Student(_nextId++, EditName.Trim(), EditGroup.Trim(), EditScore);
        ActiveTable.Add(item);
        Select(item);
    }

    private void UpdateSelected()
    {
        if (ActiveSelected is not { } s) return;
        s.Name = EditName.Trim();
        s.Group = EditGroup.Trim();
        s.Score = EditScore;
    }

    private void DeleteSelected()
    {
        if (ActiveSelected is { } s) ActiveTable.Remove(s);
    }

    private void Copy()
    {
        if (ActiveSelected is not { } s) return;
        _buffer = s.Clone(0);
        _bufferSource = _activeSide;
    }

    // Запись вставляется в таблицу, отличную от той, откуда её скопировали («из одной таблицы в другую»).
    // Если пользователь щёлкнул по другой таблице, она и становится целью; иначе целью служит вторая таблица.
    private void Paste()
    {
        if (_buffer is null) return;

        var target = _activeSide != _bufferSource
            ? _activeSide
            : (_bufferSource == TableSide.Left ? TableSide.Right : TableSide.Left);

        ActiveSide = target;
        var item = _buffer.Clone(_nextId++);
        ActiveTable.Add(item);
        Select(item);
    }

    private void Select(Student item)
    {
        if (_activeSide == TableSide.Left) SelectedLeft = item;
        else SelectedRight = item;
    }
}

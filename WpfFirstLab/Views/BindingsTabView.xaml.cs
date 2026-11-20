using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfFirstLab;

public partial class BindingsTabView : UserControl
{
    public BindingsTabView()
    {
        InitializeComponent();
    }

    // UpdateSourceTrigger=Explicit: источник обновляется только по явному вызову UpdateSource().
    private void ApplyExplicit_Click(object sender, RoutedEventArgs e) =>
        ExplicitBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();

    // Привязка к объекту без INotifyPropertyChanged сама не обновляется — перечитываем вручную.
    private void RefreshPlain_Click(object sender, RoutedEventArgs e) =>
        PlainText.GetBindingExpression(TextBlock.TextProperty)?.UpdateTarget();
}

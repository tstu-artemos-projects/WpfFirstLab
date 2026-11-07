using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfFirstLab;

/// <summary>
/// Присоединяемые свойства для раскраски элементов управления.
/// Ui.Brush наследуется по дереву элементов: достаточно задать цвет контейнеру,
/// и все вложенные кнопки, поля ввода, списки и т. д. окрасятся в этот цвет.
/// </summary>
public static class Ui
{
    // Цвет по умолчанию — основной цвет темы.
    public static readonly DependencyProperty BrushProperty =
        DependencyProperty.RegisterAttached(
            "Brush", typeof(Brush), typeof(Ui),
            new FrameworkPropertyMetadata(Palette.Primary.Brush, FrameworkPropertyMetadataOptions.Inherits));

    public static Brush GetBrush(DependencyObject obj) => (Brush)obj.GetValue(BrushProperty);
    public static void SetBrush(DependencyObject obj, Brush value) => obj.SetValue(BrushProperty, value);

    // Непрозрачность заливки кнопки: 1 — основная, 0.15 — вторичная, 0 — третичная.
    public static readonly DependencyProperty FillOpacityProperty =
        DependencyProperty.RegisterAttached(
            "FillOpacity", typeof(double), typeof(Ui), new PropertyMetadata(1.0));

    public static double GetFillOpacity(DependencyObject obj) => (double)obj.GetValue(FillOpacityProperty);
    public static void SetFillOpacity(DependencyObject obj, double value) => obj.SetValue(FillOpacityProperty, value);

    // Вложенные ListBox, DataGrid и т. п. перехватывают колесо мыши и «замораживают»
    // прокрутку страницы. Это свойство отдаёт прокрутку внешнему ScrollViewer.
    public static readonly DependencyProperty ForwardWheelProperty =
        DependencyProperty.RegisterAttached(
            "ForwardWheel", typeof(bool), typeof(Ui), new PropertyMetadata(false, OnForwardWheelChanged));

    public static bool GetForwardWheel(DependencyObject obj) => (bool)obj.GetValue(ForwardWheelProperty);
    public static void SetForwardWheel(DependencyObject obj, bool value) => obj.SetValue(ForwardWheelProperty, value);

    private static void OnForwardWheelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not ScrollViewer viewer) return;

        viewer.PreviewMouseWheel -= OnPreviewMouseWheel;
        if ((bool)e.NewValue)
            viewer.PreviewMouseWheel += OnPreviewMouseWheel;
    }

    private static void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        var viewer = (ScrollViewer)sender;
        viewer.ScrollToVerticalOffset(viewer.VerticalOffset - e.Delta);
        e.Handled = true;
    }
}

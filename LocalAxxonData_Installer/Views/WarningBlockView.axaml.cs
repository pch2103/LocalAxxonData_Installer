using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LocalAxxonData_Installer.Views;

public partial class WarningBlockView : UserControl
{
    public static readonly StyledProperty<string> HeaderTextProperty =
        AvaloniaProperty.Register<WarningBlockView, string>(nameof(HeaderText), "");

    public static readonly StyledProperty<string> BodyTextProperty =
        AvaloniaProperty.Register<WarningBlockView, string>(nameof(BodyText), "");

    public string HeaderText
    {
        get => GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    public string BodyText
    {
        get => GetValue(BodyTextProperty);
        set => SetValue(BodyTextProperty, value);
    }

    public WarningBlockView()
    {
        InitializeComponent();
    }
}

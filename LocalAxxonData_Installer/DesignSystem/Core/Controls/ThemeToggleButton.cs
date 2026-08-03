using Avalonia;
using Avalonia.Automation;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using IconPath = Avalonia.Controls.Shapes.Path;

namespace ONE.PSIM.DesignSystem.Controls;

public sealed class ThemeToggleButton : Button
{
    public static readonly StyledProperty<string> SwitchToLightLabelProperty =
        AvaloniaProperty.Register<ThemeToggleButton, string>(
            nameof(SwitchToLightLabel),
            "Switch to Light");

    public static readonly StyledProperty<string> SwitchToDarkLabelProperty =
        AvaloniaProperty.Register<ThemeToggleButton, string>(
            nameof(SwitchToDarkLabel),
            "Switch to Dark");

    public ThemeToggleButton()
    {
        Classes.Add("Subtle");
        Classes.Add("IconOnlyLarge");
        Focusable = false;
        FocusAdorner = null;

        IconElement = new IconPath();
        IconElement.Width = 24;
        IconElement.Height = 24;
        IconElement.Stretch = Stretch.None;
        IconElement.Classes.Add("IconGlyph");
        IconElement.Classes.Add("IconNatural");
        Content = IconElement;

        UpdatePresentation();
    }

    public string SwitchToLightLabel
    {
        get => GetValue(SwitchToLightLabelProperty);
        set => SetValue(SwitchToLightLabelProperty, value);
    }

    public string SwitchToDarkLabel
    {
        get => GetValue(SwitchToDarkLabelProperty);
        set => SetValue(SwitchToDarkLabelProperty, value);
    }

    public IconPath IconElement { get; }

    protected override void OnClick()
    {
        if (Application.Current is { } application)
        {
            application.RequestedThemeVariant = ActualThemeVariant == ThemeVariant.Dark
                ? ThemeVariant.Light
                : ThemeVariant.Dark;
        }

        UpdatePresentation();
        base.OnClick();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property.Name == nameof(ActualThemeVariant) ||
            change.Property == SwitchToLightLabelProperty ||
            change.Property == SwitchToDarkLabelProperty)
        {
            UpdatePresentation();
        }
    }

    private void UpdatePresentation()
    {
        var isDark = ActualThemeVariant == ThemeVariant.Dark;
        var iconKey = isDark ? "LightThemeIcon" : "DarkThemeIcon";
        var label = isDark ? SwitchToLightLabel : SwitchToDarkLabel;

        if (Application.Current?.TryFindResource(iconKey, out var icon) == true &&
            icon is StreamGeometry geometry)
        {
            IconElement.Data = geometry;
        }

        ToolTip.SetTip(this, label);
        AutomationProperties.SetName(this, label);
    }
}

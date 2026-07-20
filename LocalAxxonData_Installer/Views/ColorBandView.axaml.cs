using System;
using System.Globalization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace LocalAxxonData_Installer.Views;

public partial class ColorBandView : UserControl
{
    public static readonly StyledProperty<IBrush> HeaderBrushProperty =
        AvaloniaProperty.Register<ColorBandView, IBrush>(nameof(HeaderBrush), new SolidColorBrush(Colors.DodgerBlue));

    public static readonly StyledProperty<string> HeaderTextProperty =
        AvaloniaProperty.Register<ColorBandView, string>(nameof(HeaderText), "");

    public static readonly StyledProperty<string> BodyTextProperty =
        AvaloniaProperty.Register<ColorBandView, string>(nameof(BodyText), "");

    public static readonly StyledProperty<bool> ShowLogoProperty =
        AvaloniaProperty.Register<ColorBandView, bool>(nameof(ShowLogo), true);

    public IBrush HeaderBrush
    {
        get => GetValue(HeaderBrushProperty);
        set => SetValue(HeaderBrushProperty, value);
    }

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

    public bool ShowLogo
    {
        get => GetValue(ShowLogoProperty);
        set => SetValue(ShowLogoProperty, value);
    }

    public ColorBandView()
    {
        InitializeComponent();
    }
}

public class StringNotEmptyConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is string s && !string.IsNullOrEmpty(s);

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

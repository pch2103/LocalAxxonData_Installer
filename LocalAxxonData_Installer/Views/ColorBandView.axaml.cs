using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace LocalAxxonData_Installer.Views;

public partial class ColorBandView : UserControl
{
    public static readonly StyledProperty<IBrush> HeaderBrushProperty =
        AvaloniaProperty.Register<ColorBandView, IBrush>(nameof(HeaderBrush), Brushes.Transparent);

    public static readonly StyledProperty<string> HeaderTextProperty =
        AvaloniaProperty.Register<ColorBandView, string>(nameof(HeaderText), "");

    public static readonly StyledProperty<string> BodyTextProperty =
        AvaloniaProperty.Register<ColorBandView, string>(nameof(BodyText), "");

    public static readonly StyledProperty<bool> ShowLogoProperty =
        AvaloniaProperty.Register<ColorBandView, bool>(nameof(ShowLogo), true);

    public static readonly StyledProperty<string> BackgroundImageSourceProperty =
        AvaloniaProperty.Register<ColorBandView, string>(nameof(BackgroundImageSource), "");

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

    public string BackgroundImageSource
    {
        get => GetValue(BackgroundImageSourceProperty);
        set => SetValue(BackgroundImageSourceProperty, value);
    }

    public ColorBandView()
    {
        InitializeComponent();
        Loaded += (s, e) => UpdateBackgroundImage();
        BackgroundImageSourceProperty.Changed.AddClassHandler<ColorBandView>(
            (view, e) => view.UpdateBackgroundImage());
    }

    private void UpdateBackgroundImage()
    {
        var name = BackgroundImageSource;
        if (string.IsNullOrEmpty(name))
        {
            BackgroundImage.Source = null;
            return;
        }
        var uri = new Uri($"avares://LocalAxxonData_Installer/Assets/{name}");
        BackgroundImage.Source = new Bitmap(AssetLoader.Open(uri));
    }
}

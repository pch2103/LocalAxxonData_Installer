using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LocalAxxonData_Installer.Localization;

namespace LocalAxxonData_Installer.Views;

public partial class StageTitleBar : UserControl
{
    public StageTitleBar()
    {
        InitializeComponent();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        TitleText.Text = LocStrings.StageTitleBarText;
        ThemeToggleButton.SwitchToLightLabel = LocStrings.SwitchToLightTheme;
        ThemeToggleButton.SwitchToDarkLabel = LocStrings.SwitchToDarkTheme;
    }

    private void CloseButton_Click(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowExitConfirmPage();
        }
    }

    private void TitleBar_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.BeginWindowDrag(e);
        }
    }
}

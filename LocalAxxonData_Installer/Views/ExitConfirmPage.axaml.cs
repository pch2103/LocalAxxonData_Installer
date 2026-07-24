using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LocalAxxonData_Installer.Localization;

namespace LocalAxxonData_Installer.Views;

public partial class ExitConfirmPage : UserControl
{
    public ExitConfirmPage()
    {
        InitializeComponent();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        TitleText.Text = LocStrings.ExitConfirmHeading;
        DescText.Text = LocStrings.ExitConfirmBody;
        ExitButton.Content = LocStrings.Exit;
        ContinueButton.Content = LocStrings.ExitConfirmContinue;
    }

    private void OnContinueClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.RestorePreviousPage();
        }
    }

    private void OnExitClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.CloseWindow();
        }
    }
}

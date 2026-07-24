using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LocalAxxonData_Installer.Localization;

namespace LocalAxxonData_Installer.Views;

public partial class AlreadyInstalledPage : UserControl
{
    public AlreadyInstalledPage()
    {
        InitializeComponent();
        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        TitleText.Text = LocStrings.AlreadyInstalledHeading;
        DescText.Text = LocStrings.AlreadyInstalledBody;
        RestoreButton.Content = LocStrings.AlreadyInstalledRestore;
        UninstallButton.Content = LocStrings.AlreadyInstalledUninstall;
        ExitButton.Content = LocStrings.Exit;
    }

    private void OnRestoreClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowProgressPage(ProgressMode.Restore);
        }
    }

    private void OnUninstallClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowUninstallPage();
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


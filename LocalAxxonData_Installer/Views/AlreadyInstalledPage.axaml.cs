using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace LocalAxxonData_Installer.Views;

public partial class AlreadyInstalledPage : UserControl
{
    public AlreadyInstalledPage()
    {
        InitializeComponent();
    }

    private void OnRestoreClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowRestoreProgressPage();
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

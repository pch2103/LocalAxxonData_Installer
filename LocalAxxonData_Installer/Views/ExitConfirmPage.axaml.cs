using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace LocalAxxonData_Installer.Views;

public partial class ExitConfirmPage : UserControl
{
    public ExitConfirmPage()
    {
        InitializeComponent();
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

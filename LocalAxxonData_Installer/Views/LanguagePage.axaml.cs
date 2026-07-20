using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace LocalAxxonData_Installer.Views;

public partial class LanguagePage : UserControl
{
    public LanguagePage()
    {
        InitializeComponent();
    }

    private void OnRussianClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowWelcomePage();
        }
    }

    private void OnEnglishClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
        {
            mainWindow.ShowWelcomePage();
        }
    }
}

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
        SelectRussian();
    }

    private void SelectRussian()
    {
        RussianButton.Classes.Remove("Secondary");
        RussianButton.Classes.Add("Primary");
        EnglishButton.Classes.Remove("Primary");
        EnglishButton.Classes.Add("Secondary");
        NextButton.IsEnabled = true;
    }

    private void OnRussianClick(object? sender, RoutedEventArgs e)
    {
        RussianButton.Classes.Remove("Secondary");
        RussianButton.Classes.Add("Primary");
        EnglishButton.Classes.Remove("Primary");
        EnglishButton.Classes.Add("Secondary");
        NextButton.IsEnabled = true;
    }

    private void OnEnglishClick(object? sender, RoutedEventArgs e)
    {
        EnglishButton.Classes.Remove("Secondary");
        EnglishButton.Classes.Add("Primary");
        RussianButton.Classes.Remove("Primary");
        RussianButton.Classes.Add("Secondary");
        NextButton.IsEnabled = true;
    }

    private void OnNextClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.ShowWelcomePage();
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e)
    {
        if (VisualRoot is MainWindow mainWindow)
            mainWindow.ShowExitConfirmPage();
    }
}
